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
        public bool EsPincel{get;set;}
        public TrazoFigura(){}
        public TrazoFigura(IEnumerable<PointF> p,EstiloFigura e,bool borrador,bool pincel=false){puntos.AddRange(p);Estilo=e.Clonar();EsBorrador=borrador;EsPincel=pincel;}
        public void EstablecerPuntos(IEnumerable<PointF> p){puntos.Clear();puntos.AddRange(p);}
        public void AgregarPunto(PointF p){puntos.Add(p);}
        public override void Rasterizar(ContextoRaster c)
        {
            if(puntos.Count==0)return;Color color=EsBorrador?c.Fondo:Estilo.ColorLinea;var t=PuntosTransformados;var linea=new LineaBresenham();
            DibujarPunta(c.Buffer,Point.Round(t[0]),color);
            for(int i=1;i<t.Count;i++)foreach(var punto in linea.Calcular(Point.Round(t[i-1]),Point.Round(t[i])))DibujarPunta(c.Buffer,punto,color);
        }
        private void DibujarPunta(BufferPixeles buffer,Point punto,Color color)
        {
            if(EsPincel||EsBorrador)buffer.PonerPixelGrueso(punto.X,punto.Y,color,Estilo.Grosor);
            else buffer.PonerPixelCuadrado(punto.X,punto.Y,color,Estilo.Grosor);
        }
    }
}
