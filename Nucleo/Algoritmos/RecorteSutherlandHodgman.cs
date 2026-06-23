using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RecorteSutherlandHodgman : IAlgoritmoRecortePoligono
    {
        public string Nombre => "Sutherland-Hodgman";

        public List<Point> Recortar(IEnumerable<Point> poligono, Rectangle ventana)
        {
            var salida = poligono
                .Select(punto => new PointF(punto.X, punto.Y))
                .ToList();

            for (int borde = 0; borde < 4; borde++)
            {
                salida = RecortarContraBorde(salida, ventana, borde);

                if (salida.Count == 0)
                {
                    break;
                }
            }

            return salida.Select(Point.Round).ToList();
        }

        private static List<PointF> RecortarContraBorde(
            IReadOnlyList<PointF> entrada,
            Rectangle ventana,
            int borde)
        {
            var salida = new List<PointF>();

            if (entrada.Count == 0)
            {
                return salida;
            }

            PointF anterior = entrada[entrada.Count - 1];

            foreach (var actual in entrada)
            {
                bool anteriorDentro = Dentro(anterior, ventana, borde);
                bool actualDentro = Dentro(actual, ventana, borde);

                if (actualDentro)
                {
                    if (!anteriorDentro)
                    {
                        salida.Add(Interseccion(anterior, actual, ventana, borde));
                    }

                    salida.Add(actual);
                }
                else if (anteriorDentro)
                {
                    salida.Add(Interseccion(anterior, actual, ventana, borde));
                }

                anterior = actual;
            }

            return salida;
        }

        private static bool Dentro(PointF punto, Rectangle ventana, int borde)
        {
            if (borde == 0)
            {
                return punto.X >= ventana.Left;
            }

            if (borde == 1)
            {
                return punto.X <= ventana.Right;
            }

            if (borde == 2)
            {
                return punto.Y >= ventana.Top;
            }

            return punto.Y <= ventana.Bottom;
        }

        private static PointF Interseccion(PointF inicio, PointF fin, Rectangle ventana, int borde)
        {
            float dx = fin.X - inicio.X;
            float dy = fin.Y - inicio.Y;
            float x = inicio.X;
            float y = inicio.Y;

            if (borde == 0)
            {
                x = ventana.Left;
                y = Math.Abs(dx) < .0001f
                    ? inicio.Y
                    : inicio.Y + dy * (ventana.Left - inicio.X) / dx;
            }
            else if (borde == 1)
            {
                x = ventana.Right;
                y = Math.Abs(dx) < .0001f
                    ? inicio.Y
                    : inicio.Y + dy * (ventana.Right - inicio.X) / dx;
            }
            else if (borde == 2)
            {
                y = ventana.Top;
                x = Math.Abs(dy) < .0001f
                    ? inicio.X
                    : inicio.X + dx * (ventana.Top - inicio.Y) / dy;
            }
            else
            {
                y = ventana.Bottom;
                x = Math.Abs(dy) < .0001f
                    ? inicio.X
                    : inicio.X + dx * (ventana.Bottom - inicio.Y) / dy;
            }

            return new PointF(x, y);
        }
    }
}
