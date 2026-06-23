using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class CirculoEcuacionDirecta : IAlgoritmoCirculo
    {
        public string Nombre => "Ecuación directa";

        public IEnumerable<Point> Calcular(Point centro, int radio)
        {
            int radioAbsoluto = Math.Abs(radio);
            var vistos = new HashSet<Point>();

            for (int x = 0; x <= radioAbsoluto; x++)
            {
                int y = (int)Math.Round(Math.Sqrt(Math.Max(0, radioAbsoluto * radioAbsoluto - x * x)));

                foreach (var punto in CirculoPuntoMedio.Simetricos(centro, x, y))
                {
                    if (vistos.Add(punto))
                    {
                        yield return punto;
                    }
                }
            }
        }
    }
}
