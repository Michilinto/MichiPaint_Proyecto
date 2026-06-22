using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public class RectanguloFigura : PoligonoFigura
    {
        public override string Tipo=>Redondeado?"rectangulo_redondeado":"rectangulo";
        public bool Redondeado{get;set;}
        public RectanguloFigura(){}
        public RectanguloFigura(RectangleF r,EstiloFigura e,bool redondeado):base(Crear(r,redondeado),e){Redondeado=redondeado;}
        protected static IEnumerable<PointF> Crear(RectangleF r,bool redondeado)
        {
            if(!redondeado)return new[]{new PointF(r.Left,r.Top),new PointF(r.Right,r.Top),new PointF(r.Right,r.Bottom),new PointF(r.Left,r.Bottom)};var pts=new List<PointF>();float radio=System.Math.Min(16,System.Math.Min(r.Width,r.Height)/4f);AgregarArco(pts,r.Right-radio,r.Top+radio,radio,-90,0);AgregarArco(pts,r.Right-radio,r.Bottom-radio,radio,0,90);AgregarArco(pts,r.Left+radio,r.Bottom-radio,radio,90,180);AgregarArco(pts,r.Left+radio,r.Top+radio,radio,180,270);return pts;
        }
        private static void AgregarArco(List<PointF> p,float cx,float cy,float r,int a0,int a1){for(int a=a0;a<=a1;a+=15){double rad=a*System.Math.PI/180;p.Add(new PointF(cx+(float)System.Math.Cos(rad)*r,cy+(float)System.Math.Sin(rad)*r));}}
    }
}
