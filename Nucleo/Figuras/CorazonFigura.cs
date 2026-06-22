using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CorazonFigura : FiguraPersonalizadaBase
    {
        public CorazonFigura():base(FormaPersonalizada.Corazon){}
        public CorazonFigura(RectangleF r,EstiloFigura e):base(FormaPersonalizada.Corazon,e){EstablecerPuntos(Generar(r));}
        private static IEnumerable<PointF> Generar(RectangleF r){float cx=r.Left+r.Width/2f,cy=r.Top+r.Height/2f;return new[]{new PointF(cx,r.Bottom),new PointF(r.Left,cy),new PointF(r.Left,r.Top+r.Height*.25f),new PointF(cx-r.Width*.25f,r.Top),new PointF(cx,r.Top+r.Height*.25f),new PointF(cx+r.Width*.25f,r.Top),new PointF(r.Right,r.Top+r.Height*.25f),new PointF(r.Right,cy)};}
    }
}
