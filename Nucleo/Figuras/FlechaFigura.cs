using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class FlechaFigura : FiguraPersonalizadaBase
    {
        public FlechaFigura():base(FormaPersonalizada.Flecha){}
        public FlechaFigura(RectangleF r,EstiloFigura e):base(FormaPersonalizada.Flecha,e){EstablecerPuntos(Generar(r));}
        private static IEnumerable<PointF> Generar(RectangleF r){float cx=r.Left+r.Width/2f,cy=r.Top+r.Height/2f;return new[]{new PointF(r.Left,cy-r.Height*.15f),new PointF(cx,cy-r.Height*.15f),new PointF(cx,r.Top),new PointF(r.Right,cy),new PointF(cx,r.Bottom),new PointF(cx,cy+r.Height*.15f),new PointF(r.Left,cy+r.Height*.15f)};}
    }
}
