using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class LineaPuntoMedio : IAlgoritmoLinea
    {
        public string Nombre => "Punto medio";

        public IEnumerable<Point> Calcular(Point inicio, Point fin)
        {
            int x = inicio.X;
            int y = inicio.Y;
            int dx = Math.Abs(fin.X - x);
            int dy = Math.Abs(fin.Y - y);
            int pasoX = fin.X >= x ? 1 : -1;
            int pasoY = fin.Y >= y ? 1 : -1;

            if (dx >= dy)
            {
                foreach (var punto in CalcularPendienteSuave(x, y, dx, dy, pasoX, pasoY))
                {
                    yield return punto;
                }
            }
            else
            {
                foreach (var punto in CalcularPendientePronunciada(x, y, dx, dy, pasoX, pasoY))
                {
                    yield return punto;
                }
            }
        }

        private static IEnumerable<Point> CalcularPendienteSuave(
            int x,
            int y,
            int dx,
            int dy,
            int pasoX,
            int pasoY)
        {
            int decision = 2 * dy - dx;

            for (int i = 0; i <= dx; i++)
            {
                yield return new Point(x, y);

                x += pasoX;

                if (decision >= 0)
                {
                    y += pasoY;
                    decision -= 2 * dx;
                }

                decision += 2 * dy;
            }
        }

        private static IEnumerable<Point> CalcularPendientePronunciada(
            int x,
            int y,
            int dx,
            int dy,
            int pasoX,
            int pasoY)
        {
            int decision = 2 * dx - dy;

            for (int i = 0; i <= dy; i++)
            {
                yield return new Point(x, y);

                y += pasoY;

                if (decision >= 0)
                {
                    x += pasoX;
                    decision -= 2 * dy;
                }

                decision += 2 * dx;
            }
        }
    }
}
