using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public abstract class FiguraPersonalizadaBase : PoligonoFigura
    {
        protected FiguraPersonalizadaBase(FormaPersonalizada forma){Forma=forma;}
        protected FiguraPersonalizadaBase(FormaPersonalizada forma,EstiloFigura estilo){Forma=forma;Estilo=estilo.Clonar();}
    }
}
