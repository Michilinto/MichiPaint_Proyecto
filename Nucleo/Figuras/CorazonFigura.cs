using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CorazonFigura : FiguraPersonalizadaBase
    {
        public CorazonFigura()
            : base(FormaPersonalizada.Corazon)
        {
        }

        public CorazonFigura(RectangleF rectangulo, EstiloFigura estilo)
            : base(FormaPersonalizada.Corazon, estilo)
        {
            EstablecerPuntos(Generar(rectangulo));
        }

        private static IEnumerable<PointF> Generar(RectangleF rectangulo)
        {
            const int muestras = 96;
            var baseCorazon = new List<PointF>(muestras);

            for (int i = 0; i < muestras; i++)
            {
                double t = 2d * Math.PI * i / muestras;
                double seno = Math.Sin(t);

                float x = (float)(16d * seno * seno * seno);
                float y = (float)(
                    13d * Math.Cos(t) -
                    5d * Math.Cos(2d * t) -
                    2d * Math.Cos(3d * t) -
                    Math.Cos(4d * t));

                baseCorazon.Add(new PointF(x, y));
            }

            return NormalizarAlRectangulo(baseCorazon, rectangulo);
        }

        private static List<PointF> NormalizarAlRectangulo(
            IReadOnlyList<PointF> puntosBase,
            RectangleF rectangulo)
        {
            float minimoX = puntosBase.Min(punto => punto.X);
            float maximoX = puntosBase.Max(punto => punto.X);
            float minimoY = puntosBase.Min(punto => punto.Y);
            float maximoY = puntosBase.Max(punto => punto.Y);
            float margenX = rectangulo.Width * .04f;
            float margenY = rectangulo.Height * .04f;
            float ancho = Math.Max(1f, rectangulo.Width - 2f * margenX);
            float alto = Math.Max(1f, rectangulo.Height - 2f * margenY);

            return puntosBase
                .Select(punto => new PointF(
                    rectangulo.Left + margenX + (punto.X - minimoX) * ancho / (maximoX - minimoX),
                    rectangulo.Top + margenY + (maximoY - punto.Y) * alto / (maximoY - minimoY)))
                .ToList();
        }
    }
}
