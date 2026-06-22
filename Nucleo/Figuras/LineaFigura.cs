using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class LineaFigura : Figura2D
    {
        private readonly List<PointF> puntos=new List<PointF>();
        public override string Tipo=>"linea";
        public override IReadOnlyList<PointF> PuntosBase=>puntos;
        public AlgoritmoLineaTipo Algoritmo{get;set;}=AlgoritmoLineaTipo.Bresenham;
        public LineaFigura(){}
        public LineaFigura(PointF a,PointF b,EstiloFigura estilo,AlgoritmoLineaTipo algoritmo){puntos.Add(a);puntos.Add(b);Estilo=estilo.Clonar();Algoritmo=algoritmo;}
        public void EstablecerPuntos(IEnumerable<PointF> p){puntos.Clear();puntos.AddRange(p);}
        public override void Rasterizar(ContextoRaster c){if(puntos.Count<2)return;c.Linea=new FabricaAlgoritmos().CrearLinea(Algoritmo);c.DibujarSegmento(Transformacion.Aplicar(puntos[0]),Transformacion.Aplicar(puntos[1]),Estilo.ColorLinea,Estilo.Grosor);}
        public override bool Contiene(PointF p,float t=5){var q=PuntosTransformados;if(q.Count<2)return false;return DistanciaSegmento(p,q[0],q[1])<=t+Estilo.Grosor/2f;}
        internal static float DistanciaSegmento(PointF p,PointF a,PointF b){float dx=b.X-a.X,dy=b.Y-a.Y;if(dx==0&&dy==0)return Distancia(p,a);float u=System.Math.Max(0,System.Math.Min(1,((p.X-a.X)*dx+(p.Y-a.Y)*dy)/(dx*dx+dy*dy)));return Distancia(p,new PointF(a.X+u*dx,a.Y+u*dy));}
        internal static float Distancia(PointF a,PointF b){float dx=a.X-b.X,dy=a.Y-b.Y;return(float)System.Math.Sqrt(dx*dx+dy*dy);}
    }
}
