using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class TrapecioFigura : FiguraPersonalizadaBase
    {
        public TrapecioFigura():base(FormaPersonalizada.Trapecio){}
        public TrapecioFigura(RectangleF r,EstiloFigura e):base(FormaPersonalizada.Trapecio,e){EstablecerPuntos(Generar(r));}
        private static IEnumerable<PointF> Generar(RectangleF r){return new[]{new PointF(r.Left+r.Width*.2f,r.Top),new PointF(r.Right-r.Width*.2f,r.Top),new PointF(r.Right,r.Bottom),new PointF(r.Left,r.Bottom)};}
    }
}
