using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RecorteSutherlandHodgman : IAlgoritmoRecortePoligono
    {
        public string Nombre=>"Sutherland-Hodgman";
        public List<Point> Recortar(IEnumerable<Point> poligono,Rectangle ventana)
        {
            var salida=poligono.Select(p=>new PointF(p.X,p.Y)).ToList();
            for(int borde=0;borde<4;borde++){var entrada=salida;salida=new List<PointF>();if(entrada.Count==0)break;PointF s=entrada[entrada.Count-1];foreach(var e in entrada){bool sd=Dentro(s,ventana,borde),ed=Dentro(e,ventana,borde);if(ed){if(!sd)salida.Add(Interseccion(s,e,ventana,borde));salida.Add(e);}else if(sd)salida.Add(Interseccion(s,e,ventana,borde));s=e;}}return salida.Select(Point.Round).ToList();
        }
        private static bool Dentro(PointF p,Rectangle r,int b){if(b==0)return p.X>=r.Left;if(b==1)return p.X<=r.Right;if(b==2)return p.Y>=r.Top;return p.Y<=r.Bottom;}
        private static PointF Interseccion(PointF s,PointF e,Rectangle r,int b){float dx=e.X-s.X,dy=e.Y-s.Y,x=s.X,y=s.Y;if(b==0){x=r.Left;y=Math.Abs(dx)<.0001f?s.Y:s.Y+dy*(r.Left-s.X)/dx;}else if(b==1){x=r.Right;y=Math.Abs(dx)<.0001f?s.Y:s.Y+dy*(r.Right-s.X)/dx;}else if(b==2){y=r.Top;x=Math.Abs(dy)<.0001f?s.X:s.X+dx*(r.Top-s.Y)/dy;}else{y=r.Bottom;x=Math.Abs(dy)<.0001f?s.X:s.X+dx*(r.Bottom-s.Y)/dy;}return new PointF(x,y);}
    }
}
