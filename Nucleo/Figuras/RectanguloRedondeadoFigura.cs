using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RectanguloRedondeadoFigura : RectanguloFigura
    {
        public RectanguloRedondeadoFigura()
        {
            Redondeado = true;
        }

        public RectanguloRedondeadoFigura(RectangleF rectangulo, EstiloFigura estilo)
            : base(rectangulo, estilo, true)
        {
        }
    }
}
