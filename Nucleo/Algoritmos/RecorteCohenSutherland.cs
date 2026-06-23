using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RecorteCohenSutherland : IAlgoritmoRecorteLinea
    {
        private const int Izquierda = 1;
        private const int Derecha = 2;
        private const int Arriba = 4;
        private const int Abajo = 8;

        public string Nombre => "Cohen-Sutherland";

        public bool Recortar(Rectangle ventana, ref PointF punto0, ref PointF punto1)
        {
            int codigo0 = Codigo(ventana, punto0);
            int codigo1 = Codigo(ventana, punto1);

            while (true)
            {
                if ((codigo0 | codigo1) == 0)
                {
                    return true;
                }

                if ((codigo0 & codigo1) != 0)
                {
                    return false;
                }

                int codigoFuera = codigo0 != 0 ? codigo0 : codigo1;
                PointF interseccion = CalcularInterseccion(ventana, punto0, punto1, codigoFuera);

                if (codigoFuera == codigo0)
                {
                    punto0 = interseccion;
                    codigo0 = Codigo(ventana, punto0);
                }
                else
                {
                    punto1 = interseccion;
                    codigo1 = Codigo(ventana, punto1);
                }
            }
        }

        private static PointF CalcularInterseccion(
            Rectangle ventana,
            PointF punto0,
            PointF punto1,
            int codigo)
        {
            float x;
            float y;

            if ((codigo & Abajo) != 0)
            {
                x = punto0.X + (punto1.X - punto0.X) * (ventana.Bottom - punto0.Y) / (punto1.Y - punto0.Y);
                y = ventana.Bottom;
            }
            else if ((codigo & Arriba) != 0)
            {
                x = punto0.X + (punto1.X - punto0.X) * (ventana.Top - punto0.Y) / (punto1.Y - punto0.Y);
                y = ventana.Top;
            }
            else if ((codigo & Derecha) != 0)
            {
                y = punto0.Y + (punto1.Y - punto0.Y) * (ventana.Right - punto0.X) / (punto1.X - punto0.X);
                x = ventana.Right;
            }
            else
            {
                y = punto0.Y + (punto1.Y - punto0.Y) * (ventana.Left - punto0.X) / (punto1.X - punto0.X);
                x = ventana.Left;
            }

            return new PointF(x, y);
        }

        private static int Codigo(Rectangle ventana, PointF punto)
        {
            int codigo = 0;

            if (punto.X < ventana.Left)
            {
                codigo |= Izquierda;
            }
            else if (punto.X > ventana.Right)
            {
                codigo |= Derecha;
            }

            if (punto.Y < ventana.Top)
            {
                codigo |= Arriba;
            }
            else if (punto.Y > ventana.Bottom)
            {
                codigo |= Abajo;
            }

            return codigo;
        }
    }
}
