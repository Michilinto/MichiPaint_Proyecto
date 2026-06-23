using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CirculoPuntoMedio : IAlgoritmoCirculo
    {
        public string Nombre => "Punto medio";

        public IEnumerable<Point> Calcular(Point centro, int radio)
        {
            int x = 0;
            int y = Math.Abs(radio);
            int decision = 1 - Math.Abs(radio);
            var vistos = new HashSet<Point>();

            while (x <= y)
            {
                foreach (var punto in Simetricos(centro, x, y))
                {
                    if (vistos.Add(punto))
                    {
                        yield return punto;
                    }
                }

                x++;

                if (decision < 0)
                {
                    decision += 2 * x + 1;
                }
                else
                {
                    y--;
                    decision += 2 * (x - y) + 1;
                }
            }
        }

        internal static IEnumerable<Point> Simetricos(Point centro, int x, int y)
        {
            yield return new Point(centro.X + x, centro.Y + y);
            yield return new Point(centro.X - x, centro.Y + y);
            yield return new Point(centro.X + x, centro.Y - y);
            yield return new Point(centro.X - x, centro.Y - y);
            yield return new Point(centro.X + y, centro.Y + x);
            yield return new Point(centro.X - y, centro.Y + x);
            yield return new Point(centro.X + y, centro.Y - x);
            yield return new Point(centro.X - y, centro.Y - x);
        }
    }
}
