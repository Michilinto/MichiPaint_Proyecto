using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CurvaBezier : IAlgoritmoCurva
    {
        public string Nombre => "Bézier";

        public List<Point> Calcular(IReadOnlyList<PointF> control, int muestras = 120)
        {
            var resultado = new List<Point>();

            if (control == null || control.Count == 0)
            {
                return resultado;
            }

            int grado = control.Count - 1;
            muestras = Math.Max(30, muestras);

            for (int k = 0; k <= muestras; k++)
            {
                double t = k / (double)muestras;
                double x = 0;
                double y = 0;

                for (int i = 0; i <= grado; i++)
                {
                    double baseBernstein =
                        Binomial(grado, i) *
                        Math.Pow(1 - t, grado - i) *
                        Math.Pow(t, i);

                    x += baseBernstein * control[i].X;
                    y += baseBernstein * control[i].Y;
                }

                resultado.Add(new Point((int)Math.Round(x), (int)Math.Round(y)));
            }

            return resultado;
        }

        private static double Binomial(int n, int k)
        {
            double resultado = 1;

            for (int i = 1; i <= k; i++)
            {
                resultado = resultado * (n - k + i) / i;
            }

            return resultado;
        }
    }
}
