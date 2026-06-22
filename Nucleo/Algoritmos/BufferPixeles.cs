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
        private int[] pixeles;
        public int Ancho { get; }
        public int Alto { get; }

        public BufferPixeles(int ancho, int alto, Color fondo)
        {
            Ancho=ancho;Alto=alto;pixeles=Enumerable.Repeat(fondo.ToArgb(),ancho*alto).ToArray();
        }

        public bool Dentro(int x,int y)=>x>=0&&y>=0&&x<Ancho&&y<Alto;
        public Color ObtenerPixel(int x,int y)=>Dentro(x,y)?Color.FromArgb(pixeles[y*Ancho+x]):Color.Empty;
        public void PonerPixel(int x,int y,Color color){if(Dentro(x,y))pixeles[y*Ancho+x]=color.ToArgb();}

        public void PonerPixelGrueso(int x,int y,Color color,int grosor)
        {
            int radio=Math.Max(0,grosor-1)/2;
            for(int dy=-radio;dy<=radio;dy++)for(int dx=-radio;dx<=radio;dx++)
                if(dx*dx+dy*dy<=radio*radio+radio)PonerPixel(x+dx,y+dy,color);
        }

        public Bitmap CrearBitmap()
        {
            var bmp=new Bitmap(Ancho,Alto,PixelFormat.Format32bppArgb);
            var datos=bmp.LockBits(new Rectangle(0,0,Ancho,Alto),ImageLockMode.WriteOnly,PixelFormat.Format32bppArgb);
            Marshal.Copy(pixeles,0,datos.Scan0,pixeles.Length);bmp.UnlockBits(datos);return bmp;
        }

        public void CargarBitmap(Bitmap bmp)
        {
            var datos=bmp.LockBits(new Rectangle(0,0,Ancho,Alto),ImageLockMode.ReadOnly,PixelFormat.Format32bppArgb);
            Marshal.Copy(datos.Scan0,pixeles,0,pixeles.Length);bmp.UnlockBits(datos);
        }

        public void DibujarTextoTransformado(string texto,Font fuente,Color color,PointF posicion,MatrizTransformacion transformacion)
        {
            using(var bmp=CrearBitmap())
            {
                using(var g=Graphics.FromImage(bmp))using(var brocha=new SolidBrush(color))using(var matriz=CrearMatriz(transformacion))
                {
                    g.TextRenderingHint=System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;g.Transform=matriz;g.DrawString(texto??string.Empty,fuente,brocha,posicion);
                }
                CargarBitmap(bmp);
            }
        }

        private static Matrix CrearMatriz(MatrizTransformacion transformacion)
        {
            double[] m=transformacion.AArreglo();return new Matrix((float)m[0],(float)m[3],(float)m[1],(float)m[4],(float)m[2],(float)m[5]);
        }
    }
}
