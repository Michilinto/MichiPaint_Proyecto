using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CruzFigura : FiguraPersonalizadaBase
    {
        public CruzFigura():base(FormaPersonalizada.Cruz){}
        public CruzFigura(RectangleF r,EstiloFigura e):base(FormaPersonalizada.Cruz,e){EstablecerPuntos(Generar(r));}
        private static IEnumerable<PointF> Generar(RectangleF r){float cx=r.Left+r.Width/2f,cy=r.Top+r.Height/2f,dx=r.Width*.15f,dy=r.Height*.15f;return new[]{new PointF(cx-dx,r.Top),new PointF(cx+dx,r.Top),new PointF(cx+dx,cy-dy),new PointF(r.Right,cy-dy),new PointF(r.Right,cy+dy),new PointF(cx+dx,cy+dy),new PointF(cx+dx,r.Bottom),new PointF(cx-dx,r.Bottom),new PointF(cx-dx,cy+dy),new PointF(r.Left,cy+dy),new PointF(r.Left,cy-dy),new PointF(cx-dx,cy-dy)};}
    }
}
