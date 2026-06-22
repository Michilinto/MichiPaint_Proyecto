using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CirculoPolar : IAlgoritmoCirculo
    {
        public string Nombre=>"Polar";
        public IEnumerable<Point> Calcular(Point centro,int radio)
        {
            int r=Math.Abs(radio),pasos=Math.Max(24,(int)(2*Math.PI*r));var vistos=new HashSet<Point>();
            for(int i=0;i<=pasos;i++){double a=2*Math.PI*i/pasos;var p=new Point(centro.X+(int)Math.Round(r*Math.Cos(a)),centro.Y+(int)Math.Round(r*Math.Sin(a)));if(vistos.Add(p))yield return p;}
        }
    }
}
