using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class BezierFigura : Figura2D
    {
        private readonly List<PointF> puntos = new List<PointF>();
        private readonly IAlgoritmoCurva algoritmo = new CurvaBezier();

        public override string Tipo => "bezier";
        public override IReadOnlyList<PointF> PuntosBase => puntos;

        public BezierFigura()
        {
        }

        public BezierFigura(IEnumerable<PointF> puntos, EstiloFigura estilo)
        {
            this.puntos.AddRange(puntos);
            Estilo = estilo.Clonar();
        }

        public void EstablecerPuntos(IEnumerable<PointF> nuevosPuntos)
        {
            puntos.Clear();
            puntos.AddRange(nuevosPuntos);
        }

        public override void Rasterizar(ContextoRaster contexto)
        {
            var curva = algoritmo.Calcular(PuntosTransformados);
            contexto.Linea = new LineaBresenham();

            for (int i = 1; i < curva.Count; i++)
            {
                contexto.DibujarSegmento(
                    curva[i - 1],
                    curva[i],
                    Estilo.ColorLinea,
                    Estilo.Grosor);
            }
        }
    }
}
