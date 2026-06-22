using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class BezierFigura : Figura2D
    {
        private readonly List<PointF> puntos=new List<PointF>();
        private readonly IAlgoritmoCurva algoritmo=new CurvaBezier();
        public override string Tipo=>"bezier";
        public override IReadOnlyList<PointF> PuntosBase=>puntos;
        public BezierFigura(){}
        public BezierFigura(IEnumerable<PointF> p,EstiloFigura e){puntos.AddRange(p);Estilo=e.Clonar();}
        public void EstablecerPuntos(IEnumerable<PointF> p){puntos.Clear();puntos.AddRange(p);}
        public override void Rasterizar(ContextoRaster c){var curva=algoritmo.Calcular(PuntosTransformados);c.Linea=new LineaBresenham();for(int i=1;i<curva.Count;i++)c.DibujarSegmento(curva[i-1],curva[i],Estilo.ColorLinea,Estilo.Grosor);}
    }
}
