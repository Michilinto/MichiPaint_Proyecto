using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class BufferPixeles
    {
        private readonly int[] pixeles;

        public int Ancho { get; }
        public int Alto { get; }

        public BufferPixeles(int ancho, int alto, Color fondo)
        {
            Ancho = ancho;
            Alto = alto;
            pixeles = Enumerable.Repeat(fondo.ToArgb(), ancho * alto).ToArray();
        }

        public bool Dentro(int x, int y)
        {
            return x >= 0 && y >= 0 && x < Ancho && y < Alto;
        }

        public Color ObtenerPixel(int x, int y)
        {
            return Dentro(x, y)
                ? Color.FromArgb(pixeles[y * Ancho + x])
                : Color.Empty;
        }

        public void PonerPixel(int x, int y, Color color)
        {
            if (Dentro(x, y))
            {
                pixeles[y * Ancho + x] = color.ToArgb();
            }
        }

        public void PonerPixelGrueso(int x, int y, Color color, int grosor)
        {
            grosor = Math.Max(1, grosor);

            float centro = (grosor - 1) / 2f;
            float radio = Math.Max(.5f, grosor / 2f - .25f);
            int origen = -(grosor / 2);

            for (int fila = 0; fila < grosor; fila++)
            {
                for (int columna = 0; columna < grosor; columna++)
                {
                    float dx = columna - centro;
                    float dy = fila - centro;

                    if (dx * dx + dy * dy <= radio * radio)
                    {
                        PonerPixel(x + origen + columna, y + origen + fila, color);
                    }
                }
            }
        }

        public void PonerPixelCuadrado(int x, int y, Color color, int grosor)
        {
            grosor = Math.Max(1, grosor);
            int origen = -(grosor / 2);

            for (int fila = 0; fila < grosor; fila++)
            {
                for (int columna = 0; columna < grosor; columna++)
                {
                    PonerPixel(x + origen + columna, y + origen + fila, color);
                }
            }
        }

        public Bitmap CrearBitmap()
        {
            var bitmap = new Bitmap(Ancho, Alto, PixelFormat.Format32bppArgb);
            var datos = bitmap.LockBits(
                new Rectangle(0, 0, Ancho, Alto),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);

            Marshal.Copy(pixeles, 0, datos.Scan0, pixeles.Length);
            bitmap.UnlockBits(datos);

            return bitmap;
        }

        public void CargarBitmap(Bitmap bitmap)
        {
            var datos = bitmap.LockBits(
                new Rectangle(0, 0, Ancho, Alto),
                ImageLockMode.ReadOnly,
                PixelFormat.Format32bppArgb);

            Marshal.Copy(datos.Scan0, pixeles, 0, pixeles.Length);
            bitmap.UnlockBits(datos);
        }

        public void DibujarTextoTransformado(
            string texto,
            Font fuente,
            Color color,
            PointF posicion,
            MatrizTransformacion transformacion)
        {
            using (var bitmap = CrearBitmap())
            {
                using (var graphics = Graphics.FromImage(bitmap))
                using (var brocha = new SolidBrush(color))
                using (var matriz = CrearMatriz(transformacion))
                {
                    graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
                    graphics.Transform = matriz;
                    graphics.DrawString(texto ?? string.Empty, fuente, brocha, posicion);
                }

                CargarBitmap(bitmap);
            }
        }

        private static Matrix CrearMatriz(MatrizTransformacion transformacion)
        {
            double[] matriz = transformacion.AArreglo();

            return new Matrix(
                (float)matriz[0],
                (float)matriz[3],
                (float)matriz[1],
                (float)matriz[4],
                (float)matriz[2],
                (float)matriz[5]);
        }
    }
}
