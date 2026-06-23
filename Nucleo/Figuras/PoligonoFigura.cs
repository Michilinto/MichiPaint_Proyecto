using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public class PoligonoFigura : Figura2D
    {
        protected readonly List<PointF> puntos = new List<PointF>();

        public override string Tipo => "poligono";
        public override IReadOnlyList<PointF> PuntosBase => puntos;
        public FormaPersonalizada Forma { get; set; } = FormaPersonalizada.Poligono;
        public bool Cerrado { get; set; } = true;

        public PoligonoFigura()
        {
        }

        public PoligonoFigura(
            IEnumerable<PointF> puntos,
            EstiloFigura estilo,
            FormaPersonalizada forma = FormaPersonalizada.Poligono)
        {
            this.puntos.AddRange(puntos);
            Estilo = estilo.Clonar();
            Forma = forma;
        }

        public void EstablecerPuntos(IEnumerable<PointF> nuevosPuntos)
        {
            puntos.Clear();
            puntos.AddRange(nuevosPuntos);
        }

        public override void Rasterizar(ContextoRaster contexto)
        {
            var puntosRaster = PuntosTransformados.Select(Point.Round).ToList();

            if (puntosRaster.Count < 2)
            {
                return;
            }

            puntosRaster = new RecorteSutherlandHodgman().Recortar(
                puntosRaster,
                new Rectangle(0, 0, contexto.Buffer.Ancho - 1, contexto.Buffer.Alto - 1));

            if (puntosRaster.Count < 2)
            {
                return;
            }

            if (Cerrado && Estilo.TieneRelleno && puntosRaster.Count >= 3)
            {
                new RellenoScanline().Rellenar(contexto.Buffer, puntosRaster, Estilo.ColorRelleno);
            }

            contexto.Linea = new LineaBresenham();

            for (int i = 1; i < puntosRaster.Count; i++)
            {
                contexto.DibujarSegmento(
                    puntosRaster[i - 1],
                    puntosRaster[i],
                    Estilo.ColorLinea,
                    Estilo.Grosor);
            }

            if (Cerrado && puntosRaster.Count >= 3)
            {
                contexto.DibujarSegmento(
                    puntosRaster[puntosRaster.Count - 1],
                    puntosRaster[0],
                    Estilo.ColorLinea,
                    Estilo.Grosor);
            }
        }

        public override bool Contiene(PointF punto, float tolerancia = 5)
        {
            var transformados = PuntosTransformados;

            if (transformados.Count == 0)
            {
                return false;
            }

            if (PuntoDentroDelPoligono(punto, transformados))
            {
                return true;
            }

            for (int i = 0; i < transformados.Count; i++)
            {
                PointF inicio = transformados[i];
                PointF fin = transformados[(i + 1) % transformados.Count];

                if (LineaFigura.DistanciaSegmento(punto, inicio, fin) <= tolerancia)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool PuntoDentroDelPoligono(PointF punto, IReadOnlyList<PointF> poligono)
        {
            bool dentro = false;

            for (int i = 0, j = poligono.Count - 1; i < poligono.Count; j = i++)
            {
                bool cruza =
                    (poligono[i].Y > punto.Y) != (poligono[j].Y > punto.Y) &&
                    punto.X <
                    (poligono[j].X - poligono[i].X) *
                    (punto.Y - poligono[i].Y) /
                    (poligono[j].Y - poligono[i].Y) +
                    poligono[i].X;

                if (cruza)
                {
                    dentro = !dentro;
                }
            }

            return dentro;
        }
    }
}
