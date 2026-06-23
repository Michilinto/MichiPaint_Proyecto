using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class LineaFigura : Figura2D
    {
        private readonly List<PointF> puntos = new List<PointF>();

        public override string Tipo => "linea";
        public override IReadOnlyList<PointF> PuntosBase => puntos;
        public AlgoritmoLineaTipo Algoritmo { get; set; } = AlgoritmoLineaTipo.Bresenham;

        public LineaFigura()
        {
        }

        public LineaFigura(
            PointF inicio,
            PointF fin,
            EstiloFigura estilo,
            AlgoritmoLineaTipo algoritmo)
        {
            puntos.Add(inicio);
            puntos.Add(fin);
            Estilo = estilo.Clonar();
            Algoritmo = algoritmo;
        }

        public void EstablecerPuntos(IEnumerable<PointF> nuevosPuntos)
        {
            puntos.Clear();
            puntos.AddRange(nuevosPuntos);
        }

        public override void Rasterizar(ContextoRaster contexto)
        {
            if (puntos.Count < 2)
            {
                return;
            }

            contexto.Linea = new FabricaAlgoritmos().CrearLinea(Algoritmo);
            contexto.DibujarSegmento(
                Transformacion.Aplicar(puntos[0]),
                Transformacion.Aplicar(puntos[1]),
                Estilo.ColorLinea,
                Estilo.Grosor);
        }

        public override bool Contiene(PointF punto, float tolerancia = 5)
        {
            var transformados = PuntosTransformados;

            if (transformados.Count < 2)
            {
                return false;
            }

            float distancia = DistanciaSegmento(punto, transformados[0], transformados[1]);
            return distancia <= tolerancia + Estilo.Grosor / 2f;
        }

        internal static float DistanciaSegmento(PointF punto, PointF inicio, PointF fin)
        {
            float dx = fin.X - inicio.X;
            float dy = fin.Y - inicio.Y;

            if (dx == 0 && dy == 0)
            {
                return Distancia(punto, inicio);
            }

            float numerador =
                (punto.X - inicio.X) * dx +
                (punto.Y - inicio.Y) * dy;

            float denominador = dx * dx + dy * dy;
            float proporcion = System.Math.Max(0, System.Math.Min(1, numerador / denominador));

            var proyeccion = new PointF(
                inicio.X + proporcion * dx,
                inicio.Y + proporcion * dy);

            return Distancia(punto, proyeccion);
        }

        internal static float Distancia(PointF a, PointF b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;

            return (float)System.Math.Sqrt(dx * dx + dy * dy);
        }
    }
}
