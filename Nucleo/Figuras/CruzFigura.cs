using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CruzFigura : FiguraPersonalizadaBase
    {
        public CruzFigura()
            : base(FormaPersonalizada.Cruz)
        {
        }

        public CruzFigura(RectangleF rectangulo, EstiloFigura estilo)
            : base(FormaPersonalizada.Cruz, estilo)
        {
            EstablecerPuntos(Generar(rectangulo));
        }

        private static IEnumerable<PointF> Generar(RectangleF rectangulo)
        {
            float centroX = rectangulo.Left + rectangulo.Width / 2f;
            float centroY = rectangulo.Top + rectangulo.Height / 2f;
            float anchoBrazo = rectangulo.Width * .15f;
            float altoBrazo = rectangulo.Height * .15f;

            return new[]
            {
                new PointF(centroX - anchoBrazo, rectangulo.Top),
                new PointF(centroX + anchoBrazo, rectangulo.Top),
                new PointF(centroX + anchoBrazo, centroY - altoBrazo),
                new PointF(rectangulo.Right, centroY - altoBrazo),
                new PointF(rectangulo.Right, centroY + altoBrazo),
                new PointF(centroX + anchoBrazo, centroY + altoBrazo),
                new PointF(centroX + anchoBrazo, rectangulo.Bottom),
                new PointF(centroX - anchoBrazo, rectangulo.Bottom),
                new PointF(centroX - anchoBrazo, centroY + altoBrazo),
                new PointF(rectangulo.Left, centroY + altoBrazo),
                new PointF(rectangulo.Left, centroY - altoBrazo),
                new PointF(centroX - anchoBrazo, centroY - altoBrazo)
            };
        }
    }
}
