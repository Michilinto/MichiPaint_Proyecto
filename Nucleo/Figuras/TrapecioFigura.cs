using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class TrapecioFigura : FiguraPersonalizadaBase
    {
        public TrapecioFigura()
            : base(FormaPersonalizada.Trapecio)
        {
        }

        public TrapecioFigura(RectangleF rectangulo, EstiloFigura estilo)
            : base(FormaPersonalizada.Trapecio, estilo)
        {
            EstablecerPuntos(Generar(rectangulo));
        }

        private static IEnumerable<PointF> Generar(RectangleF rectangulo)
        {
            return new[]
            {
                new PointF(rectangulo.Left + rectangulo.Width * .2f, rectangulo.Top),
                new PointF(rectangulo.Right - rectangulo.Width * .2f, rectangulo.Top),
                new PointF(rectangulo.Right, rectangulo.Bottom),
                new PointF(rectangulo.Left, rectangulo.Bottom)
            };
        }
    }
}
