using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class ContextoRaster
    {
        public BufferPixeles Buffer{get;}
        public IAlgoritmoLinea Linea{get;set;}
        public IAlgoritmoCirculo Circulo{get;set;}
        public IAlgoritmoRecorteLinea RecorteLinea{get;set;}
        public Color Fondo{get;}

        public ContextoRaster(BufferPixeles buffer,Color fondo)
        {
            Buffer=buffer;Fondo=fondo;Linea=new LineaBresenham();Circulo=new CirculoPuntoMedio();RecorteLinea=new RecorteCohenSutherland();
        }

        public void DibujarSegmento(PointF a,PointF b,Color color,int grosor)
        {
            var p0=a;var p1=b;if(!RecorteLinea.Recortar(new Rectangle(0,0,Buffer.Ancho-1,Buffer.Alto-1),ref p0,ref p1))return;
            foreach(var p in Linea.Calcular(Point.Round(p0),Point.Round(p1)))Buffer.PonerPixelGrueso(p.X,p.Y,color,grosor);
        }
    }
}
