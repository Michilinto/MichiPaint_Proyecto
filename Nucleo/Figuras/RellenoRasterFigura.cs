using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RellenoRasterFigura : Figura2D
    {
        private readonly List<PointF> puntos = new List<PointF>();

        public override string Tipo => "relleno_raster";
        public override IReadOnlyList<PointF> PuntosBase => puntos;

        public RellenoRasterFigura()
        {
        }

        public RellenoRasterFigura(IEnumerable<TramoRelleno> tramos, Color color)
        {
            foreach (var tramo in tramos)
            {
                puntos.Add(new PointF(tramo.Inicio, tramo.Y));
                puntos.Add(new PointF(tramo.Fin, tramo.Y));
            }

            Estilo.ColorRelleno = color;
            Estilo.TieneRelleno = true;
        }

        public void EstablecerPuntos(IEnumerable<PointF> nuevosPuntos)
        {
            puntos.Clear();
            puntos.AddRange(nuevosPuntos);
        }

        public override void Rasterizar(ContextoRaster contexto)
        {
            var transformados = PuntosTransformados;

            for (int i = 0; i + 1 < transformados.Count; i += 2)
            {
                int y = (int)Math.Round(transformados[i].Y);
                int inicio = (int)Math.Round(Math.Min(transformados[i].X, transformados[i + 1].X));
                int fin = (int)Math.Round(Math.Max(transformados[i].X, transformados[i + 1].X));

                for (int x = inicio; x <= fin; x++)
                {
                    contexto.Buffer.PonerPixel(x, y, Estilo.ColorRelleno);
                }
            }
        }

        public override bool Contiene(PointF punto, float tolerancia = 5)
        {
            return false;
        }
    }
}
