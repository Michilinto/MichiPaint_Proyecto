using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RomboFigura : FiguraPersonalizadaBase
    {
        public RomboFigura()
            : base(FormaPersonalizada.Rombo)
        {
        }

        public RomboFigura(RectangleF rectangulo, EstiloFigura estilo)
            : base(FormaPersonalizada.Rombo, estilo)
        {
            EstablecerPuntos(Generar(rectangulo));
        }

        private static IEnumerable<PointF> Generar(RectangleF rectangulo)
        {
            float centroX = rectangulo.Left + rectangulo.Width / 2f;
            float centroY = rectangulo.Top + rectangulo.Height / 2f;

            return new[]
            {
                new PointF(centroX, rectangulo.Top),
                new PointF(rectangulo.Right, centroY),
                new PointF(centroX, rectangulo.Bottom),
                new PointF(rectangulo.Left, centroY)
            };
        }
    }
}
