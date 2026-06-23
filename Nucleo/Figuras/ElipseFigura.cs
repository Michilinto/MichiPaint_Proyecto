using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class ElipseFigura : Figura2D
    {
        private readonly List<PointF> puntos = new List<PointF>();

        public override string Tipo => "elipse";
        public override IReadOnlyList<PointF> PuntosBase => puntos;
        public AlgoritmoCirculoTipo Algoritmo { get; set; } = AlgoritmoCirculoTipo.PuntoMedio;

        public ElipseFigura()
        {
        }

        public ElipseFigura(
            RectangleF rectangulo,
            EstiloFigura estilo,
            AlgoritmoCirculoTipo algoritmo)
        {
            puntos.Add(new PointF(rectangulo.Left, rectangulo.Top));
            puntos.Add(new PointF(rectangulo.Right, rectangulo.Bottom));

            Estilo = estilo.Clonar();
            Algoritmo = algoritmo;
        }

        public void EstablecerPuntos(IEnumerable<PointF> nuevosPuntos)
        {
            puntos.Clear();
            puntos.AddRange(nuevosPuntos);
        }

        public override void Rasterizar(ContextoRaster contexto)
        {
            if (puntos.Count < 2)
            {
                return;
            }

            var contornoBase = CrearContornoBase();
            var contorno = contornoBase
                .Select(Transformacion.Aplicar)
                .Select(Point.Round)
                .ToList();

            if (Estilo.TieneRelleno)
            {
                new RellenoScanline().Rellenar(contexto.Buffer, contorno, Estilo.ColorRelleno);
            }

            contexto.Linea = new LineaBresenham();

            for (int i = 1; i < contorno.Count; i++)
            {
                contexto.DibujarSegmento(
                    contorno[i - 1],
                    contorno[i],
                    Estilo.ColorLinea,
                    Estilo.Grosor);
            }

            if (contorno.Count > 1)
            {
                contexto.DibujarSegmento(
                    contorno[contorno.Count - 1],
                    contorno[0],
                    Estilo.ColorLinea,
                    Estilo.Grosor);
            }
        }

        public override RectangleF ObtenerLimites()
        {
            if (puntos.Count < 2)
            {
                return RectangleF.Empty;
            }

            var esquinas = new[]
            {
                puntos[0],
                new PointF(puntos[1].X, puntos[0].Y),
                puntos[1],
                new PointF(puntos[0].X, puntos[1].Y)
            }
            .Select(Transformacion.Aplicar)
            .ToList();

            return RectangleF.FromLTRB(
                esquinas.Min(punto => punto.X),
                esquinas.Min(punto => punto.Y),
                esquinas.Max(punto => punto.X),
                esquinas.Max(punto => punto.Y));
        }

        private List<PointF> CrearContornoBase()
        {
            float izquierda = Math.Min(puntos[0].X, puntos[1].X);
            float derecha = Math.Max(puntos[0].X, puntos[1].X);
            float arriba = Math.Min(puntos[0].Y, puntos[1].Y);
            float abajo = Math.Max(puntos[0].Y, puntos[1].Y);
            float radioX = (derecha - izquierda) / 2f;
            float radioY = (abajo - arriba) / 2f;

            var centro = new PointF(izquierda + radioX, arriba + radioY);
            var contorno = new List<PointF>();

            if (Math.Abs(radioX - radioY) < 1.1f)
            {
                var circulo = new FabricaAlgoritmos()
                    .CrearCirculo(Algoritmo)
                    .Calcular(Point.Round(centro), (int)Math.Round(radioX));

                contorno.AddRange(circulo.Select(punto => new PointF(punto.X, punto.Y)));
            }
            else
            {
                for (int i = 0; i < 180; i++)
                {
                    double angulo = 2 * Math.PI * i / 180;
                    contorno.Add(new PointF(
                        centro.X + radioX * (float)Math.Cos(angulo),
                        centro.Y + radioY * (float)Math.Sin(angulo)));
                }
            }

            return contorno;
        }
    }
}
