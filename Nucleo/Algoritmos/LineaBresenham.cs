using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class LineaBresenham : IAlgoritmoLinea
    {
        public string Nombre => "Bresenham";

        public IEnumerable<Point> Calcular(Point inicio, Point fin)
        {
            int x = inicio.X;
            int y = inicio.Y;
            int dx = Math.Abs(fin.X - x);
            int dy = Math.Abs(fin.Y - y);
            int pasoX = x < fin.X ? 1 : -1;
            int pasoY = y < fin.Y ? 1 : -1;
            int error = dx - dy;

            while (true)
            {
                yield return new Point(x, y);

                if (x == fin.X && y == fin.Y)
                {
                    break;
                }

                int dobleError = 2 * error;

                if (dobleError > -dy)
                {
                    error -= dy;
                    x += pasoX;
                }

                if (dobleError < dx)
                {
                    error += dx;
                    y += pasoY;
                }
            }
        }
    }
}
