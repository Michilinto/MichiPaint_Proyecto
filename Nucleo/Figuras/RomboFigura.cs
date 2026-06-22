using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RomboFigura : FiguraPersonalizadaBase
    {
        public RomboFigura():base(FormaPersonalizada.Rombo){}
        public RomboFigura(RectangleF r,EstiloFigura e):base(FormaPersonalizada.Rombo,e){EstablecerPuntos(Generar(r));}
        private static IEnumerable<PointF> Generar(RectangleF r){float cx=r.Left+r.Width/2f,cy=r.Top+r.Height/2f;return new[]{new PointF(cx,r.Top),new PointF(r.Right,cy),new PointF(cx,r.Bottom),new PointF(r.Left,cy)};}
    }
}
