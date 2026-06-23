namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class FabricaAlgoritmos
    {
        public IAlgoritmoLinea CrearLinea(AlgoritmoLineaTipo tipo)
        {
            switch (tipo)
            {
                case AlgoritmoLineaTipo.DDA:
                    return new LineaDDA();

                case AlgoritmoLineaTipo.PuntoMedio:
                    return new LineaPuntoMedio();

                default:
                    return new LineaBresenham();
            }
        }

        public IAlgoritmoCirculo CrearCirculo(AlgoritmoCirculoTipo tipo)
        {
            switch (tipo)
            {
                case AlgoritmoCirculoTipo.Polar:
                    return new CirculoPolar();

                case AlgoritmoCirculoTipo.EcuacionDirecta:
                    return new CirculoEcuacionDirecta();

                default:
                    return new CirculoPuntoMedio();
            }
        }
    }
}
