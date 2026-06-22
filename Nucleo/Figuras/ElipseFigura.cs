using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class ElipseFigura : Figura2D
    {
        private readonly List<PointF> puntos=new List<PointF>();
        public override string Tipo=>"elipse";
        public override IReadOnlyList<PointF> PuntosBase=>puntos;
        public AlgoritmoCirculoTipo Algoritmo{get;set;}=AlgoritmoCirculoTipo.PuntoMedio;
        public ElipseFigura(){}
        public ElipseFigura(RectangleF r,EstiloFigura e,AlgoritmoCirculoTipo algoritmo){puntos.Add(new PointF(r.Left,r.Top));puntos.Add(new PointF(r.Right,r.Bottom));Estilo=e.Clonar();Algoritmo=algoritmo;}
        public void EstablecerPuntos(IEnumerable<PointF> p){puntos.Clear();puntos.AddRange(p);}
        public override void Rasterizar(ContextoRaster c)
        {
            if(puntos.Count<2)return;float left=Math.Min(puntos[0].X,puntos[1].X),right=Math.Max(puntos[0].X,puntos[1].X),top=Math.Min(puntos[0].Y,puntos[1].Y),bottom=Math.Max(puntos[0].Y,puntos[1].Y),rx=(right-left)/2f,ry=(bottom-top)/2f;PointF centro=new PointF(left+rx,top+ry);var baseContorno=new List<PointF>();
            if(Math.Abs(rx-ry)<1.1f)baseContorno.AddRange(new FabricaAlgoritmos().CrearCirculo(Algoritmo).Calcular(Point.Round(centro),(int)Math.Round(rx)).Select(x=>new PointF(x.X,x.Y)));else for(int i=0;i<180;i++){double a=2*Math.PI*i/180;baseContorno.Add(new PointF(centro.X+rx*(float)Math.Cos(a),centro.Y+ry*(float)Math.Sin(a)));}
            var contorno=baseContorno.Select(Transformacion.Aplicar).Select(Point.Round).ToList();if(Estilo.TieneRelleno)new RellenoScanline().Rellenar(c.Buffer,contorno,Estilo.ColorRelleno);c.Linea=new LineaBresenham();for(int i=1;i<contorno.Count;i++)c.DibujarSegmento(contorno[i-1],contorno[i],Estilo.ColorLinea,Estilo.Grosor);if(contorno.Count>1)c.DibujarSegmento(contorno[contorno.Count-1],contorno[0],Estilo.ColorLinea,Estilo.Grosor);
        }
        public override RectangleF ObtenerLimites(){if(puntos.Count<2)return RectangleF.Empty;var e=new[]{puntos[0],new PointF(puntos[1].X,puntos[0].Y),puntos[1],new PointF(puntos[0].X,puntos[1].Y)}.Select(Transformacion.Aplicar).ToList();return RectangleF.FromLTRB(e.Min(x=>x.X),e.Min(x=>x.Y),e.Max(x=>x.X),e.Max(x=>x.Y));}
    }
}
