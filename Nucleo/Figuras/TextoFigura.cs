using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class TextoFigura : Figura2D
    {
        private readonly List<PointF> puntos = new List<PointF>();

        public override string Tipo => "texto";
        public override IReadOnlyList<PointF> PuntosBase => puntos;

        public string Texto { get; set; } = string.Empty;
        public string Fuente { get; set; } = "Microsoft Sans Serif";
        public float TamanoFuente { get; set; } = 12;
        public bool Negrita { get; set; }
        public bool Cursiva { get; set; }

        public TextoFigura()
        {
        }

        public TextoFigura(PointF punto, string texto, Font fuente, Color color)
        {
            puntos.Add(punto);
            Texto = texto;
            Fuente = fuente.FontFamily.Name;
            TamanoFuente = fuente.Size;
            Negrita = fuente.Bold;
            Cursiva = fuente.Italic;
            Estilo.ColorLinea = color;
        }

        public void EstablecerPuntos(IEnumerable<PointF> nuevosPuntos)
        {
            puntos.Clear();
            puntos.AddRange(nuevosPuntos);
        }

        public override void Rasterizar(ContextoRaster contexto)
        {
            if (puntos.Count == 0 || string.IsNullOrEmpty(Texto))
            {
                return;
            }

            FontStyle estiloFuente = CrearEstiloFuente();

            try
            {
                using (var fuente = new Font(Fuente, TamanoFuente, estiloFuente))
                {
                    contexto.Buffer.DibujarTextoTransformado(
                        Texto,
                        fuente,
                        Estilo.ColorLinea,
                        puntos[0],
                        Transformacion);
                }
            }
            catch
            {
                using (var fuente = new Font("Microsoft Sans Serif", TamanoFuente, estiloFuente))
                {
                    contexto.Buffer.DibujarTextoTransformado(
                        Texto,
                        fuente,
                        Estilo.ColorLinea,
                        puntos[0],
                        Transformacion);
                }
            }
        }

        public override IReadOnlyList<PointF> ObtenerMarcoSeleccion()
        {
            if (puntos.Count == 0)
            {
                return new List<PointF>();
            }

            PointF origen = puntos[0];
            float ancho = Math.Max(20, Texto.Length * TamanoFuente * .65f);
            float alto = TamanoFuente * 1.6f;

            return new[]
            {
                Transformacion.Aplicar(origen),
                Transformacion.Aplicar(new PointF(origen.X + ancho, origen.Y)),
                Transformacion.Aplicar(new PointF(origen.X + ancho, origen.Y + alto)),
                Transformacion.Aplicar(new PointF(origen.X, origen.Y + alto))
            };
        }

        public override RectangleF ObtenerLimites()
        {
            var marco = ObtenerMarcoSeleccion();

            if (marco.Count == 0)
            {
                return RectangleF.Empty;
            }

            return RectangleF.FromLTRB(
                marco.Min(punto => punto.X),
                marco.Min(punto => punto.Y),
                marco.Max(punto => punto.X),
                marco.Max(punto => punto.Y));
        }

        public override bool Contiene(PointF punto, float tolerancia = 5)
        {
            var marco = ObtenerMarcoSeleccion();

            if (marco.Count < 4)
            {
                return false;
            }

            bool dentro = false;

            for (int i = 0, j = marco.Count - 1; i < marco.Count; j = i++)
            {
                bool cruza =
                    (marco[i].Y > punto.Y) != (marco[j].Y > punto.Y) &&
                    punto.X <
                    (marco[j].X - marco[i].X) *
                    (punto.Y - marco[i].Y) /
                    (marco[j].Y - marco[i].Y) +
                    marco[i].X;

                if (cruza)
                {
                    dentro = !dentro;
                }
            }

            return dentro;
        }

        private FontStyle CrearEstiloFuente()
        {
            FontStyle estilo = FontStyle.Regular;

            if (Negrita)
            {
                estilo |= FontStyle.Bold;
            }

            if (Cursiva)
            {
                estilo |= FontStyle.Italic;
            }

            return estilo;
        }
    }
}
