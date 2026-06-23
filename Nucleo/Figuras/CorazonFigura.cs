using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CorazonFigura : FiguraPersonalizadaBase
    {
        public CorazonFigura():base(FormaPersonalizada.Corazon){}
        public CorazonFigura(RectangleF r,EstiloFigura e):base(FormaPersonalizada.Corazon,e){EstablecerPuntos(Generar(r));}

        private static IEnumerable<PointF> Generar(RectangleF r)
        {
            // Curva paramétrica de corazón muestreada como polígono. Se
            // normaliza al rectángulo para conservar la edición y el relleno.
            const int muestras = 96;
            var baseCorazon = new List<PointF>(muestras);
            for (int i = 0; i < muestras; i++)
            {
                double t = 2d * Math.PI * i / muestras;
                double seno = Math.Sin(t);
                float x = (float)(16d * seno * seno * seno);
                float y = (float)(13d * Math.Cos(t) - 5d * Math.Cos(2d * t)
                    - 2d * Math.Cos(3d * t) - Math.Cos(4d * t));
                baseCorazon.Add(new PointF(x, y));
            }

            float minX = baseCorazon.Min(p => p.X), maxX = baseCorazon.Max(p => p.X);
            float minY = baseCorazon.Min(p => p.Y), maxY = baseCorazon.Max(p => p.Y);
            float margenX = r.Width * .04f, margenY = r.Height * .04f;
            float ancho = Math.Max(1f, r.Width - 2f * margenX);
            float alto = Math.Max(1f, r.Height - 2f * margenY);

            return baseCorazon.Select(p => new PointF(
                r.Left + margenX + (p.X - minX) * ancho / (maxX - minX),
                r.Top + margenY + (maxY - p.Y) * alto / (maxY - minY))).ToList();
        }
    }
}
