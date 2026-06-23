using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class FlechaFigura : FiguraPersonalizadaBase
    {
        public FlechaFigura()
            : base(FormaPersonalizada.Flecha)
        {
        }

        public FlechaFigura(RectangleF rectangulo, EstiloFigura estilo)
            : base(FormaPersonalizada.Flecha, estilo)
        {
            EstablecerPuntos(Generar(rectangulo));
        }

        private static IEnumerable<PointF> Generar(RectangleF rectangulo)
        {
            float centroX = rectangulo.Left + rectangulo.Width / 2f;
            float centroY = rectangulo.Top + rectangulo.Height / 2f;
            float alturaCuerpo = rectangulo.Height * .15f;

            return new[]
            {
                new PointF(rectangulo.Left, centroY - alturaCuerpo),
                new PointF(centroX, centroY - alturaCuerpo),
                new PointF(centroX, rectangulo.Top),
                new PointF(rectangulo.Right, centroY),
                new PointF(centroX, rectangulo.Bottom),
                new PointF(centroX, centroY + alturaCuerpo),
                new PointF(rectangulo.Left, centroY + alturaCuerpo)
            };
        }
    }
}
