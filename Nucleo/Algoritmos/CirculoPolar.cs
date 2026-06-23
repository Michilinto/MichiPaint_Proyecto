using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CirculoPolar : IAlgoritmoCirculo
    {
        public string Nombre => "Polar";

        public IEnumerable<Point> Calcular(Point centro, int radio)
        {
            int radioAbsoluto = Math.Abs(radio);
            int pasos = Math.Max(24, (int)(2 * Math.PI * radioAbsoluto));
            var vistos = new HashSet<Point>();

            for (int i = 0; i <= pasos; i++)
            {
                double angulo = 2 * Math.PI * i / pasos;

                var punto = new Point(
                    centro.X + (int)Math.Round(radioAbsoluto * Math.Cos(angulo)),
                    centro.Y + (int)Math.Round(radioAbsoluto * Math.Sin(angulo)));

                if (vistos.Add(punto))
                {
                    yield return punto;
                }
            }
        }
    }
}
