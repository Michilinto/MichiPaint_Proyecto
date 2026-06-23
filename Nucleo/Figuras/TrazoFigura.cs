using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class TrazoFigura : Figura2D
    {
        private readonly List<PointF> puntos = new List<PointF>();

        public override string Tipo => EsBorrador ? "borrador" : "trazo";
        public override IReadOnlyList<PointF> PuntosBase => puntos;

        public bool EsBorrador { get; set; }
        public bool EsPincel { get; set; }

        public TrazoFigura()
        {
        }

        public TrazoFigura(
            IEnumerable<PointF> puntos,
            EstiloFigura estilo,
            bool borrador,
            bool pincel = false)
        {
            this.puntos.AddRange(puntos);
            Estilo = estilo.Clonar();
            EsBorrador = borrador;
            EsPincel = pincel;
        }

        public void EstablecerPuntos(IEnumerable<PointF> nuevosPuntos)
        {
            puntos.Clear();
            puntos.AddRange(nuevosPuntos);
        }

        public void AgregarPunto(PointF punto)
        {
            puntos.Add(punto);
        }

        public override void Rasterizar(ContextoRaster contexto)
        {
            if (puntos.Count == 0)
            {
                return;
            }

            Color color = EsBorrador ? contexto.Fondo : Estilo.ColorLinea;
            var transformados = PuntosTransformados;
            var algoritmoLinea = new LineaBresenham();

            DibujarPunta(contexto.Buffer, Point.Round(transformados[0]), color);

            for (int i = 1; i < transformados.Count; i++)
            {
                var inicio = Point.Round(transformados[i - 1]);
                var fin = Point.Round(transformados[i]);

                foreach (var punto in algoritmoLinea.Calcular(inicio, fin))
                {
                    DibujarPunta(contexto.Buffer, punto, color);
                }
            }
        }

        private void DibujarPunta(BufferPixeles buffer, Point punto, Color color)
        {
            if (EsPincel || EsBorrador)
            {
                buffer.PonerPixelGrueso(punto.X, punto.Y, color, Estilo.Grosor);
            }
            else
            {
                buffer.PonerPixelCuadrado(punto.X, punto.Y, color, Estilo.Grosor);
            }
        }
    }
}
