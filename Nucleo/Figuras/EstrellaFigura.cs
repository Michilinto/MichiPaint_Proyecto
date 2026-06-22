using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class EstrellaFigura : FiguraPersonalizadaBase
    {
        public EstrellaFigura():base(FormaPersonalizada.Estrella){}
        public EstrellaFigura(RectangleF r,EstiloFigura e):base(FormaPersonalizada.Estrella,e){EstablecerPuntos(Generar(r));}
        private static IEnumerable<PointF> Generar(RectangleF r){var p=new List<PointF>();float cx=r.Left+r.Width/2f,cy=r.Top+r.Height/2f,ro=Math.Min(r.Width,r.Height)/2f,ri=ro*.45f;for(int i=0;i<10;i++){double a=-Math.PI/2+i*Math.PI/5;float radio=i%2==0?ro:ri;p.Add(new PointF(cx+radio*(float)Math.Cos(a),cy+radio*(float)Math.Sin(a)));}return p;}
    }
}
