using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class EstrellaFigura : FiguraPersonalizadaBase
    {
        public EstrellaFigura()
            : base(FormaPersonalizada.Estrella)
        {
        }

        public EstrellaFigura(RectangleF rectangulo, EstiloFigura estilo)
            : base(FormaPersonalizada.Estrella, estilo)
        {
            EstablecerPuntos(Generar(rectangulo));
        }

        private static IEnumerable<PointF> Generar(RectangleF rectangulo)
        {
            var puntos = new List<PointF>();
            float centroX = rectangulo.Left + rectangulo.Width / 2f;
            float centroY = rectangulo.Top + rectangulo.Height / 2f;
            float radioExterno = Math.Min(rectangulo.Width, rectangulo.Height) / 2f;
            float radioInterno = radioExterno * .45f;

            for (int i = 0; i < 10; i++)
            {
                double angulo = -Math.PI / 2 + i * Math.PI / 5;
                float radio = i % 2 == 0 ? radioExterno : radioInterno;

                puntos.Add(new PointF(
                    centroX + radio * (float)Math.Cos(angulo),
                    centroY + radio * (float)Math.Sin(angulo)));
            }

            return puntos;
        }
    }
}
