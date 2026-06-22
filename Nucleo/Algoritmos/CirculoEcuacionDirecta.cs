using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CirculoEcuacionDirecta : IAlgoritmoCirculo
    {
        public string Nombre=>"Ecuacion directa";
        public IEnumerable<Point> Calcular(Point centro,int radio)
        {
            int r=Math.Abs(radio);var vistos=new HashSet<Point>();
            for(int x=0;x<=r;x++){int y=(int)Math.Round(Math.Sqrt(Math.Max(0,r*r-x*x)));foreach(var p in CirculoPuntoMedio.Simetricos(centro,x,y))if(vistos.Add(p))yield return p;}
        }
    }
}
