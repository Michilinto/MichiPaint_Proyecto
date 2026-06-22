using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class TrazoFigura : Figura2D
    {
        private readonly List<PointF> puntos=new List<PointF>();
        public override string Tipo=>EsBorrador?"borrador":"trazo";
        public override IReadOnlyList<PointF> PuntosBase=>puntos;
        public bool EsBorrador{get;set;}
        public TrazoFigura(){}
        public TrazoFigura(IEnumerable<PointF> p,EstiloFigura e,bool borrador){puntos.AddRange(p);Estilo=e.Clonar();EsBorrador=borrador;}
        public void EstablecerPuntos(IEnumerable<PointF> p){puntos.Clear();puntos.AddRange(p);}
        public void AgregarPunto(PointF p){puntos.Add(p);}
        public override void Rasterizar(ContextoRaster c){if(puntos.Count==0)return;c.Linea=new LineaBresenham();Color color=EsBorrador?c.Fondo:Estilo.ColorLinea;var t=PuntosTransformados;if(t.Count==1)c.Buffer.PonerPixelGrueso((int)t[0].X,(int)t[0].Y,color,Estilo.Grosor);for(int i=1;i<t.Count;i++)c.DibujarSegmento(t[i-1],t[i],color,Estilo.Grosor);}
    }
}
