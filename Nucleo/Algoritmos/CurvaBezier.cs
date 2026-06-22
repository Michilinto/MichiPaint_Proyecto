using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CurvaBezier : IAlgoritmoCurva
    {
        public string Nombre=>"Bezier Bernstein";
        public List<Point> Calcular(IReadOnlyList<PointF> control,int muestras=120)
        {
            var resultado=new List<Point>();if(control==null||control.Count<2)return resultado;int n=control.Count-1;
            for(int k=0;k<=muestras;k++){double t=k/(double)muestras,x=0,y=0;for(int i=0;i<=n;i++){double b=Binomial(n,i)*Math.Pow(1-t,n-i)*Math.Pow(t,i);x+=b*control[i].X;y+=b*control[i].Y;}resultado.Add(new Point((int)Math.Round(x),(int)Math.Round(y)));}return resultado;
        }
        private static double Binomial(int n,int k){double r=1;for(int i=1;i<=k;i++)r=r*(n-k+i)/i;return r;}
    }
}
