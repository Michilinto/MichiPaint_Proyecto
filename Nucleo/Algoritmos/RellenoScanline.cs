using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RellenoScanline : IAlgoritmoRellenoPoligono
    {
        public string Nombre=>"Scanline";
        public void Rellenar(BufferPixeles buffer,IReadOnlyList<Point> vertices,Color color)
        {
            if(vertices==null||vertices.Count<3)return;int minY=Math.Max(0,MinimoY(vertices)),maxY=Math.Min(buffer.Alto-1,MaximoY(vertices));
            for(int y=minY;y<=maxY;y++){var xs=new List<int>();for(int i=0,j=vertices.Count-1;i<vertices.Count;j=i++){Point a=vertices[j],b=vertices[i];if((a.Y<y&&b.Y>=y)||(b.Y<y&&a.Y>=y))xs.Add((int)Math.Round(a.X+(y-a.Y)*(b.X-a.X)/(double)(b.Y-a.Y)));}xs.Sort();for(int i=0;i+1<xs.Count;i+=2)for(int x=xs[i];x<=xs[i+1];x++)buffer.PonerPixel(x,y,color);}
        }
        private static int MinimoY(IReadOnlyList<Point> p){int v=p[0].Y;for(int i=1;i<p.Count;i++)if(p[i].Y<v)v=p[i].Y;return v;}
        private static int MaximoY(IReadOnlyList<Point> p){int v=p[0].Y;for(int i=1;i<p.Count;i++)if(p[i].Y>v)v=p[i].Y;return v;}
    }
}
