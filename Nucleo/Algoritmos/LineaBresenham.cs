using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class LineaBresenham : IAlgoritmoLinea
    {
        public string Nombre=>"Bresenham";
        public IEnumerable<Point> Calcular(Point inicio,Point fin)
        {
            int x=inicio.X,y=inicio.Y,dx=Math.Abs(fin.X-x),dy=Math.Abs(fin.Y-y),sx=x<fin.X?1:-1,sy=y<fin.Y?1:-1,error=dx-dy;
            while(true){yield return new Point(x,y);if(x==fin.X&&y==fin.Y)break;int e2=2*error;if(e2>-dy){error-=dy;x+=sx;}if(e2<dx){error+=dx;y+=sy;}}
        }
    }
}
