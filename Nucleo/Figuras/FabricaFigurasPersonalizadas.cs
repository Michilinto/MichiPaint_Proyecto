using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class FabricaFigurasPersonalizadas
    {
        public PoligonoFigura Crear(FormaPersonalizada forma, RectangleF limites, EstiloFigura estilo)
        {
            switch (forma)
            {
                case FormaPersonalizada.Corazon:
                    return new CorazonFigura(limites, estilo);

                case FormaPersonalizada.Estrella:
                    return new EstrellaFigura(limites, estilo);

                case FormaPersonalizada.Flecha:
                    return new FlechaFigura(limites, estilo);

                case FormaPersonalizada.Cruz:
                    return new CruzFigura(limites, estilo);

                case FormaPersonalizada.Rombo:
                    return new RomboFigura(limites, estilo);

                case FormaPersonalizada.Trapecio:
                    return new TrapecioFigura(limites, estilo);

                default:
                    return CrearPoligonoRectangular(limites, estilo);
            }
        }

        public PoligonoFigura CrearVacia(FormaPersonalizada forma)
        {
            switch (forma)
            {
                case FormaPersonalizada.Corazon:
                    return new CorazonFigura();

                case FormaPersonalizada.Estrella:
                    return new EstrellaFigura();

                case FormaPersonalizada.Flecha:
                    return new FlechaFigura();

                case FormaPersonalizada.Cruz:
                    return new CruzFigura();

                case FormaPersonalizada.Rombo:
                    return new RomboFigura();

                case FormaPersonalizada.Trapecio:
                    return new TrapecioFigura();

                default:
                    return new PoligonoFigura();
            }
        }

        private static PoligonoFigura CrearPoligonoRectangular(RectangleF limites, EstiloFigura estilo)
        {
            var puntos = new[]
            {
                new PointF(limites.Left, limites.Top),
                new PointF(limites.Right, limites.Top),
                new PointF(limites.Right, limites.Bottom),
                new PointF(limites.Left, limites.Bottom)
            };

            return new PoligonoFigura(puntos, estilo);
        }
    }
}
