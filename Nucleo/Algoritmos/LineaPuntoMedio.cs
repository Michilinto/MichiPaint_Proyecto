using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class LineaPuntoMedio : IAlgoritmoLinea
    {
        public string Nombre=>"Punto medio";
        public IEnumerable<Point> Calcular(Point inicio,Point fin)
        {
            int x=inicio.X,y=inicio.Y,dx=Math.Abs(fin.X-x),dy=Math.Abs(fin.Y-y),sx=fin.X>=x?1:-1,sy=fin.Y>=y?1:-1;
            if(dx>=dy){int p=2*dy-dx;for(int i=0;i<=dx;i++){yield return new Point(x,y);x+=sx;if(p>=0){y+=sy;p-=2*dx;}p+=2*dy;}}
            else{int p=2*dx-dy;for(int i=0;i<=dy;i++){yield return new Point(x,y);y+=sy;if(p>=0){x+=sx;p-=2*dy;}p+=2*dx;}}
        }
    }
}
