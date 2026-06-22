using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class LineaDDA : IAlgoritmoLinea
    {
        public string Nombre=>"DDA";
        public IEnumerable<Point> Calcular(Point inicio,Point fin)
        {
            int dx=fin.X-inicio.X,dy=fin.Y-inicio.Y,pasos=Math.Max(Math.Abs(dx),Math.Abs(dy));if(pasos==0){yield return inicio;yield break;}
            double x=inicio.X,y=inicio.Y,ix=dx/(double)pasos,iy=dy/(double)pasos;
            for(int i=0;i<=pasos;i++,x+=ix,y+=iy)yield return new Point((int)Math.Round(x),(int)Math.Round(y));
        }
    }
}
