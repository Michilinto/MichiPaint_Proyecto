using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RellenoRasterFigura : Figura2D
    {
        private readonly List<PointF> puntos=new List<PointF>();
        public override string Tipo=>"relleno_raster";
        public override IReadOnlyList<PointF> PuntosBase=>puntos;
        public RellenoRasterFigura(){}
        public RellenoRasterFigura(IEnumerable<TramoRelleno> tramos,Color color){foreach(var t in tramos){puntos.Add(new PointF(t.Inicio,t.Y));puntos.Add(new PointF(t.Fin,t.Y));}Estilo.ColorRelleno=color;Estilo.TieneRelleno=true;}
        public void EstablecerPuntos(IEnumerable<PointF> p){puntos.Clear();puntos.AddRange(p);}
        public override void Rasterizar(ContextoRaster c){var p=PuntosTransformados;for(int i=0;i+1<p.Count;i+=2){int y=(int)Math.Round(p[i].Y),x0=(int)Math.Round(Math.Min(p[i].X,p[i+1].X)),x1=(int)Math.Round(Math.Max(p[i].X,p[i+1].X));for(int x=x0;x<=x1;x++)c.Buffer.PonerPixel(x,y,Estilo.ColorRelleno);}}
        public override bool Contiene(PointF punto,float tolerancia=5)=>false;
    }
}
