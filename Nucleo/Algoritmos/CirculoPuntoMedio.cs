using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CirculoPuntoMedio : IAlgoritmoCirculo
    {
        public string Nombre=>"Punto medio";
        public IEnumerable<Point> Calcular(Point centro,int radio)
        {
            int x=0,y=Math.Abs(radio),p=1-Math.Abs(radio);var vistos=new HashSet<Point>();
            while(x<=y){foreach(var punto in Simetricos(centro,x,y))if(vistos.Add(punto))yield return punto;x++;if(p<0)p+=2*x+1;else{y--;p+=2*(x-y)+1;}}
        }
        internal static IEnumerable<Point> Simetricos(Point c,int x,int y)
        {
            yield return new Point(c.X+x,c.Y+y);yield return new Point(c.X-x,c.Y+y);yield return new Point(c.X+x,c.Y-y);yield return new Point(c.X-x,c.Y-y);
            yield return new Point(c.X+y,c.Y+x);yield return new Point(c.X-y,c.Y+x);yield return new Point(c.X+y,c.Y-x);yield return new Point(c.X-y,c.Y-x);
        }
    }
}
