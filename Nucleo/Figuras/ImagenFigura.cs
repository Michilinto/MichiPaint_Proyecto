using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;

namespace Paint_Bola\u00f1os_Flores_Venegas.Nucleo
{
    public sealed class ImagenFigura : Figura2D
    {
        private readonly List<PointF> puntos = new List<PointF>();

        public override string Tipo => "imagen";
        public override IReadOnlyList<PointF> PuntosBase => puntos;

        public byte[] DatosPng { get; set; }

        public ImagenFigura()
        {
        }

        public ImagenFigura(Image imagen, RectangleF destino)
        {
            if (imagen == null)
            {
                throw new ArgumentNullException(nameof(imagen));
            }

            EstablecerPuntos(new[]
            {
                new PointF(destino.Left, destino.Top),
                new PointF(destino.Right, destino.Bottom)
            });

            DatosPng = ConvertirAPng(imagen);
        }

        public void EstablecerPuntos(IEnumerable<PointF> nuevosPuntos)
        {
            puntos.Clear();
            puntos.AddRange((nuevosPuntos ?? Enumerable.Empty<PointF>()).Take(2));
        }

        public override void Rasterizar(ContextoRaster contexto)
        {
            if (contexto == null || DatosPng == null || DatosPng.Length == 0 || puntos.Count < 2)
            {
                return;
            }

            RectangleF destino = ObtenerDestinoBase();

            using (var lienzo = contexto.Buffer.CrearBitmap())
            using (var imagen = CrearImagen())
            using (var grafico = Graphics.FromImage(lienzo))
            using (var matriz = CrearMatriz(Transformacion))
            {
                grafico.InterpolationMode = InterpolationMode.NearestNeighbor;
                grafico.PixelOffsetMode = PixelOffsetMode.Half;
                grafico.CompositingMode = CompositingMode.SourceOver;
                grafico.Transform = matriz;
                grafico.DrawImage(imagen, destino);
                grafico.ResetTransform();
                contexto.Buffer.CargarBitmap(lienzo);
            }
        }

        public override IReadOnlyList<PointF> ObtenerMarcoSeleccion()
        {
            if (puntos.Count < 2)
            {
                return new List<PointF>();
            }

            RectangleF destino = ObtenerDestinoBase();

            return new[]
            {
                Transformacion.Aplicar(new PointF(destino.Left, destino.Top)),
                Transformacion.Aplicar(new PointF(destino.Right, destino.Top)),
                Transformacion.Aplicar(new PointF(destino.Right, destino.Bottom)),
                Transformacion.Aplicar(new PointF(destino.Left, destino.Bottom))
            };
        }

        public override RectangleF ObtenerLimites()
        {
            var marco = ObtenerMarcoSeleccion();

            if (marco.Count == 0)
            {
                return RectangleF.Empty;
            }

            return RectangleF.FromLTRB(
                marco.Min(punto => punto.X),
                marco.Min(punto => punto.Y),
                marco.Max(punto => punto.X),
                marco.Max(punto => punto.Y));
        }

        private RectangleF ObtenerDestinoBase()
        {
            float izquierda = Math.Min(puntos[0].X, puntos[1].X);
            float derecha = Math.Max(puntos[0].X, puntos[1].X);
            float arriba = Math.Min(puntos[0].Y, puntos[1].Y);
            float abajo = Math.Max(puntos[0].Y, puntos[1].Y);

            return RectangleF.FromLTRB(izquierda, arriba, derecha, abajo);
        }

        private Image CrearImagen()
        {
            using (var memoria = new MemoryStream(DatosPng))
            using (var temporal = Image.FromStream(memoria))
            {
                return new Bitmap(temporal);
            }
        }

        private static byte[] ConvertirAPng(Image imagen)
        {
            using (var memoria = new MemoryStream())
            using (var copia = new Bitmap(imagen))
            {
                copia.Save(memoria, System.Drawing.Imaging.ImageFormat.Png);
                return memoria.ToArray();
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
