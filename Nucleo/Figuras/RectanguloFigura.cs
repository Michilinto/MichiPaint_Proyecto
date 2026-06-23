using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public class RectanguloFigura : PoligonoFigura
    {
        public override string Tipo => Redondeado ? "rectangulo_redondeado" : "rectangulo";
        public bool Redondeado { get; set; }

        public RectanguloFigura()
        {
        }

        public RectanguloFigura(RectangleF rectangulo, EstiloFigura estilo, bool redondeado)
            : base(Crear(rectangulo, redondeado), estilo)
        {
            Redondeado = redondeado;
        }

        protected static IEnumerable<PointF> Crear(RectangleF rectangulo, bool redondeado)
        {
            if (!redondeado)
            {
                return new[]
                {
                    new PointF(rectangulo.Left, rectangulo.Top),
                    new PointF(rectangulo.Right, rectangulo.Top),
                    new PointF(rectangulo.Right, rectangulo.Bottom),
                    new PointF(rectangulo.Left, rectangulo.Bottom)
                };
            }

            var puntos = new List<PointF>();
            float radio = System.Math.Min(16, System.Math.Min(rectangulo.Width, rectangulo.Height) / 4f);

            AgregarArco(puntos, rectangulo.Right - radio, rectangulo.Top + radio, radio, -90, 0);
            AgregarArco(puntos, rectangulo.Right - radio, rectangulo.Bottom - radio, radio, 0, 90);
            AgregarArco(puntos, rectangulo.Left + radio, rectangulo.Bottom - radio, radio, 90, 180);
            AgregarArco(puntos, rectangulo.Left + radio, rectangulo.Top + radio, radio, 180, 270);

            return puntos;
        }

        private static void AgregarArco(
            List<PointF> puntos,
            float centroX,
            float centroY,
            float radio,
            int anguloInicial,
            int anguloFinal)
        {
            for (int angulo = anguloInicial; angulo <= anguloFinal; angulo += 15)
            {
                double radianes = angulo * System.Math.PI / 180;

                puntos.Add(new PointF(
                    centroX + (float)System.Math.Cos(radianes) * radio,
                    centroY + (float)System.Math.Sin(radianes) * radio));
            }
        }
    }
}
