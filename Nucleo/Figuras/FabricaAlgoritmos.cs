namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class FabricaAlgoritmos
    {
        public IAlgoritmoLinea CrearLinea(AlgoritmoLineaTipo tipo)
        {
            if(tipo==AlgoritmoLineaTipo.DDA)return new LineaDDA();if(tipo==AlgoritmoLineaTipo.PuntoMedio)return new LineaPuntoMedio();return new LineaBresenham();
        }
        public IAlgoritmoCirculo CrearCirculo(AlgoritmoCirculoTipo tipo)
        {
            if(tipo==AlgoritmoCirculoTipo.Polar)return new CirculoPolar();if(tipo==AlgoritmoCirculoTipo.EcuacionDirecta)return new CirculoEcuacionDirecta();return new CirculoPuntoMedio();
        }
    }
}
