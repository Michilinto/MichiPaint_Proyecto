using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RellenoScanline : IAlgoritmoRellenoPoligono
    {
        public string Nombre => "Scanline";

        public void Rellenar(BufferPixeles buffer, IReadOnlyList<Point> vertices, Color color)
        {
            if (vertices == null || vertices.Count < 3)
            {
                return;
            }

            int minimoY = Math.Max(0, MinimoY(vertices));
            int maximoY = Math.Min(buffer.Alto - 1, MaximoY(vertices));

            for (int y = minimoY; y <= maximoY; y++)
            {
                var intersecciones = CalcularIntersecciones(vertices, y);
                intersecciones.Sort();

                for (int i = 0; i + 1 < intersecciones.Count; i += 2)
                {
                    for (int x = intersecciones[i]; x <= intersecciones[i + 1]; x++)
                    {
                        buffer.PonerPixel(x, y, color);
                    }
                }
            }
        }

        private static List<int> CalcularIntersecciones(IReadOnlyList<Point> vertices, int y)
        {
            var intersecciones = new List<int>();

            for (int i = 0, j = vertices.Count - 1; i < vertices.Count; j = i++)
            {
                Point anterior = vertices[j];
                Point actual = vertices[i];

                bool cruza =
                    (anterior.Y < y && actual.Y >= y) ||
                    (actual.Y < y && anterior.Y >= y);

                if (cruza)
                {
                    int x = (int)Math.Round(
                        anterior.X +
                        (y - anterior.Y) *
                        (actual.X - anterior.X) /
                        (double)(actual.Y - anterior.Y));

                    intersecciones.Add(x);
                }
            }

            return intersecciones;
        }

        private static int MinimoY(IReadOnlyList<Point> puntos)
        {
            int valor = puntos[0].Y;

            for (int i = 1; i < puntos.Count; i++)
            {
                if (puntos[i].Y < valor)
                {
                    valor = puntos[i].Y;
                }
            }

            return valor;
        }

        private static int MaximoY(IReadOnlyList<Point> puntos)
        {
            int valor = puntos[0].Y;

            for (int i = 1; i < puntos.Count; i++)
            {
                if (puntos[i].Y > valor)
                {
                    valor = puntos[i].Y;
                }
            }

            return valor;
        }
    }
}
