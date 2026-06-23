using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class LineaDDA : IAlgoritmoLinea
    {
        public string Nombre => "DDA";

        public IEnumerable<Point> Calcular(Point inicio, Point fin)
        {
            int dx = fin.X - inicio.X;
            int dy = fin.Y - inicio.Y;
            int pasos = Math.Max(Math.Abs(dx), Math.Abs(dy));

            if (pasos == 0)
            {
                yield return inicio;
                yield break;
            }

            double x = inicio.X;
            double y = inicio.Y;
            double incrementoX = dx / (double)pasos;
            double incrementoY = dy / (double)pasos;

            for (int i = 0; i <= pasos; i++)
            {
                yield return new Point((int)Math.Round(x), (int)Math.Round(y));

                x += incrementoX;
                y += incrementoY;
            }
        }
    }
}
