using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class TextoFigura : Figura2D
    {
        private readonly List<PointF> puntos=new List<PointF>();
        public override string Tipo=>"texto";
        public override IReadOnlyList<PointF> PuntosBase=>puntos;
        public string Texto{get;set;}=string.Empty;public string Fuente{get;set;}="Microsoft Sans Serif";public float TamanoFuente{get;set;}=12;public bool Negrita{get;set;}public bool Cursiva{get;set;}
        public TextoFigura(){}
        public TextoFigura(PointF p,string texto,Font fuente,Color color){puntos.Add(p);Texto=texto;Fuente=fuente.FontFamily.Name;TamanoFuente=fuente.Size;Negrita=fuente.Bold;Cursiva=fuente.Italic;Estilo.ColorLinea=color;}
        public void EstablecerPuntos(IEnumerable<PointF> p){puntos.Clear();puntos.AddRange(p);}
        public override void Rasterizar(ContextoRaster c){if(puntos.Count==0||string.IsNullOrEmpty(Texto))return;FontStyle s=FontStyle.Regular;if(Negrita)s|=FontStyle.Bold;if(Cursiva)s|=FontStyle.Italic;try{using(var f=new Font(Fuente,TamanoFuente,s))c.Buffer.DibujarTextoTransformado(Texto,f,Estilo.ColorLinea,puntos[0],Transformacion);}catch{using(var f=new Font("Microsoft Sans Serif",TamanoFuente,s))c.Buffer.DibujarTextoTransformado(Texto,f,Estilo.ColorLinea,puntos[0],Transformacion);}}
        public override IReadOnlyList<PointF> ObtenerMarcoSeleccion(){if(puntos.Count==0)return new List<PointF>();PointF p=puntos[0];float ancho=Math.Max(20,Texto.Length*TamanoFuente*.65f),alto=TamanoFuente*1.6f;return new[]{Transformacion.Aplicar(p),Transformacion.Aplicar(new PointF(p.X+ancho,p.Y)),Transformacion.Aplicar(new PointF(p.X+ancho,p.Y+alto)),Transformacion.Aplicar(new PointF(p.X,p.Y+alto))};}
        public override RectangleF ObtenerLimites(){var m=ObtenerMarcoSeleccion();if(m.Count==0)return RectangleF.Empty;return RectangleF.FromLTRB(m.Min(p=>p.X),m.Min(p=>p.Y),m.Max(p=>p.X),m.Max(p=>p.Y));}
        public override bool Contiene(PointF punto,float tolerancia=5){var m=ObtenerMarcoSeleccion();if(m.Count<4)return false;bool dentro=false;for(int i=0,j=m.Count-1;i<m.Count;j=i++)if(((m[i].Y>punto.Y)!=(m[j].Y>punto.Y))&&punto.X<(m[j].X-m[i].X)*(punto.Y-m[i].Y)/(m[j].Y-m[i].Y)+m[i].X)dentro=!dentro;return dentro;}
    }
}
