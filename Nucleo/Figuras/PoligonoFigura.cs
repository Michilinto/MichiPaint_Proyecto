using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public class PoligonoFigura : Figura2D
    {
        protected readonly List<PointF> puntos=new List<PointF>();
        public override string Tipo=>"poligono";
        public override IReadOnlyList<PointF> PuntosBase=>puntos;
        public FormaPersonalizada Forma{get;set;}=FormaPersonalizada.Poligono;
        public bool Cerrado{get;set;}=true;
        public PoligonoFigura(){}
        public PoligonoFigura(IEnumerable<PointF> p,EstiloFigura e,FormaPersonalizada forma=FormaPersonalizada.Poligono){puntos.AddRange(p);Estilo=e.Clonar();Forma=forma;}
        public void EstablecerPuntos(IEnumerable<PointF> p){puntos.Clear();puntos.AddRange(p);}
        public override void Rasterizar(ContextoRaster c)
        {
            var p=PuntosTransformados.Select(Point.Round).ToList();if(p.Count<2)return;p=new RecorteSutherlandHodgman().Recortar(p,new Rectangle(0,0,c.Buffer.Ancho-1,c.Buffer.Alto-1));if(p.Count<2)return;
            if(Cerrado&&Estilo.TieneRelleno&&p.Count>=3)new RellenoScanline().Rellenar(c.Buffer,p,Estilo.ColorRelleno);c.Linea=new LineaBresenham();for(int i=1;i<p.Count;i++)c.DibujarSegmento(p[i-1],p[i],Estilo.ColorLinea,Estilo.Grosor);if(Cerrado&&p.Count>=3)c.DibujarSegmento(p[p.Count-1],p[0],Estilo.ColorLinea,Estilo.Grosor);
        }
        public override bool Contiene(PointF punto,float tolerancia=5){var p=PuntosTransformados;bool dentro=false;for(int i=0,j=p.Count-1;i<p.Count;j=i++)if(((p[i].Y>punto.Y)!=(p[j].Y>punto.Y))&&punto.X<(p[j].X-p[i].X)*(punto.Y-p[i].Y)/(p[j].Y-p[i].Y)+p[i].X)dentro=!dentro;if(dentro)return true;for(int i=0;i<p.Count;i++)if(LineaFigura.DistanciaSegmento(punto,p[i],p[(i+1)%p.Count])<=tolerancia)return true;return false;}
    }
}
