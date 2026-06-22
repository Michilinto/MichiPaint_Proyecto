using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RecorteCohenSutherland : IAlgoritmoRecorteLinea
    {
        public string Nombre=>"Cohen-Sutherland";
        public bool Recortar(Rectangle ventana,ref PointF p0,ref PointF p1)
        {
            int c0=Codigo(ventana,p0),c1=Codigo(ventana,p1);
            while(true){if((c0|c1)==0)return true;if((c0&c1)!=0)return false;int c=c0!=0?c0:c1;float x=0,y=0;if((c&8)!=0){x=p0.X+(p1.X-p0.X)*(ventana.Bottom-p0.Y)/(p1.Y-p0.Y);y=ventana.Bottom;}else if((c&4)!=0){x=p0.X+(p1.X-p0.X)*(ventana.Top-p0.Y)/(p1.Y-p0.Y);y=ventana.Top;}else if((c&2)!=0){y=p0.Y+(p1.Y-p0.Y)*(ventana.Right-p0.X)/(p1.X-p0.X);x=ventana.Right;}else{y=p0.Y+(p1.Y-p0.Y)*(ventana.Left-p0.X)/(p1.X-p0.X);x=ventana.Left;}if(c==c0){p0=new PointF(x,y);c0=Codigo(ventana,p0);}else{p1=new PointF(x,y);c1=Codigo(ventana,p1);}}
        }
        private static int Codigo(Rectangle r,PointF p){int c=0;if(p.X<r.Left)c|=1;else if(p.X>r.Right)c|=2;if(p.Y<r.Top)c|=4;else if(p.Y>r.Bottom)c|=8;return c;}
    }
}
