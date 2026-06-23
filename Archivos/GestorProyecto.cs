using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using Paint_Bolaños_Flores_Venegas.Nucleo;

namespace Paint_Bolaños_Flores_Venegas.Archivos
{
    public sealed class PuntoDto
    {
        public float X { get; set; }
        public float Y { get; set; }
    }

    public sealed class FiguraDto
    {
        public string Tipo { get; set; }
        public string Id { get; set; }
        public List<PuntoDto> Puntos { get; set; }
        public int ColorLinea { get; set; }
        public int ColorRelleno { get; set; }
        public int Grosor { get; set; }
        public bool TieneRelleno { get; set; }
        public double[] Matriz { get; set; }
        public int AlgoritmoLinea { get; set; }
        public int AlgoritmoCirculo { get; set; }
        public int Forma { get; set; }
        public bool Redondeado { get; set; }
        public bool EsBorrador { get; set; }
        public bool EsPincel { get; set; }
        public string Texto { get; set; }
        public string Fuente { get; set; }
        public float TamanoFuente { get; set; }
        public bool Negrita { get; set; }
        public bool Cursiva { get; set; }
    }

    public sealed class ProyectoDto
    {
        public int Version { get; set; } = 1;
        public int Ancho { get; set; }
        public int Alto { get; set; }
        public int ColorFondo { get; set; }
        public List<FiguraDto> Figuras { get; set; } = new List<FiguraDto>();
    }

    public sealed class GestorProyecto
    {
        public void Guardar(string ruta, DocumentoDibujo documento)
        {
            if (string.IsNullOrWhiteSpace(ruta))
            {
                throw new ArgumentException("Ruta no válida.");
            }

            var dto = new ProyectoDto
            {
                Ancho = documento.Ancho,
                Alto = documento.Alto,
                ColorFondo = documento.ColorFondo.ToArgb(),
                Figuras = documento.Figuras.Select(CrearDto).ToList()
            };

            var serializador = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue,
                RecursionLimit = 100
            };

            File.WriteAllText(ruta, serializador.Serialize(dto), new UTF8Encoding(false));
            documento.Modificado = false;
        }

        public DocumentoDibujo Abrir(string ruta)
        {
            var serializador = new JavaScriptSerializer
            {
                MaxJsonLength = int.MaxValue,
                RecursionLimit = 100
            };

            ProyectoDto dto;

            try
            {
                dto = serializador.Deserialize<ProyectoDto>(File.ReadAllText(ruta, Encoding.UTF8));
            }
            catch (Exception ex)
            {
                throw new InvalidDataException("El archivo no contiene un proyecto MichiPaint válido.", ex);
            }

            ValidarProyecto(dto);

            var documento = new DocumentoDibujo(dto.Ancho, dto.Alto)
            {
                ColorFondo = Color.FromArgb(dto.ColorFondo)
            };

            foreach (var figura in dto.Figuras ?? new List<FiguraDto>())
            {
                documento.Agregar(CrearFigura(figura));
            }

            documento.Modificado = false;
            return documento;
        }

        private static void ValidarProyecto(ProyectoDto dto)
        {
            bool proyectoInvalido =
                dto == null ||
                dto.Version != 1 ||
                dto.Ancho < 100 ||
                dto.Ancho > 4000 ||
                dto.Alto < 100 ||
                dto.Alto > 4000;

            if (proyectoInvalido)
            {
                throw new InvalidDataException("Versión o dimensiones de proyecto no compatibles.");
            }
        }

        private static FiguraDto CrearDto(Figura2D figura)
        {
            var dto = new FiguraDto
            {
                Tipo = figura.Tipo,
                Id = figura.Id.ToString(),
                Puntos = figura.PuntosBase.Select(p => new PuntoDto { X = p.X, Y = p.Y }).ToList(),
                ColorLinea = figura.Estilo.ColorLinea.ToArgb(),
                ColorRelleno = figura.Estilo.ColorRelleno.ToArgb(),
                Grosor = figura.Estilo.Grosor,
                TieneRelleno = figura.Estilo.TieneRelleno,
                Matriz = figura.Transformacion.AArreglo()
            };

            CompletarDatosEspecificos(figura, dto);
            return dto;
        }

        private static void CompletarDatosEspecificos(Figura2D figura, FiguraDto dto)
        {
            var linea = figura as LineaFigura;
            if (linea != null)
            {
                dto.AlgoritmoLinea = (int)linea.Algoritmo;
            }

            var elipse = figura as ElipseFigura;
            if (elipse != null)
            {
                dto.AlgoritmoCirculo = (int)elipse.Algoritmo;
            }

            var poligono = figura as PoligonoFigura;
            if (poligono != null)
            {
                dto.Forma = (int)poligono.Forma;
            }

            var rectangulo = figura as RectanguloFigura;
            if (rectangulo != null)
            {
                dto.Redondeado = rectangulo.Redondeado;
            }

            var trazo = figura as TrazoFigura;
            if (trazo != null)
            {
                dto.EsBorrador = trazo.EsBorrador;
                dto.EsPincel = trazo.EsPincel;
            }

            var texto = figura as TextoFigura;
            if (texto != null)
            {
                dto.Texto = texto.Texto;
                dto.Fuente = texto.Fuente;
                dto.TamanoFuente = texto.TamanoFuente;
                dto.Negrita = texto.Negrita;
                dto.Cursiva = texto.Cursiva;
            }
        }

        private static Figura2D CrearFigura(FiguraDto dto)
        {
            if (dto == null || dto.Puntos == null)
            {
                throw new InvalidDataException("Figura incompleta.");
            }

            var puntos = dto.Puntos.Select(p => new PointF(p.X, p.Y)).ToList();
            Figura2D figura = CrearFiguraPorTipo(dto, puntos);

            Guid id;
            if (Guid.TryParse(dto.Id, out id))
            {
                figura.Id = id;
            }

            figura.Estilo = new EstiloFigura
            {
                ColorLinea = Color.FromArgb(dto.ColorLinea),
                ColorRelleno = Color.FromArgb(dto.ColorRelleno),
                Grosor = Math.Max(1, dto.Grosor),
                TieneRelleno = dto.TieneRelleno
            };

            figura.Transformacion = MatrizTransformacion.DesdeArreglo(dto.Matriz);
            return figura;
        }

        private static Figura2D CrearFiguraPorTipo(FiguraDto dto, IEnumerable<PointF> puntos)
        {
            switch (dto.Tipo)
            {
                case "linea":
                    return CrearLinea(dto, puntos);

                case "trazo":
                case "borrador":
                    return CrearTrazo(dto, puntos);

                case "rectangulo":
                case "rectangulo_redondeado":
                    return CrearRectangulo(dto, puntos);

                case "elipse":
                    return CrearElipse(dto, puntos);

                case "bezier":
                    return CrearBezier(puntos);

                case "texto":
                    return CrearTexto(dto, puntos);

                case "relleno_raster":
                    return CrearRellenoRaster(puntos);

                default:
                    return CrearPersonalizada(dto, puntos);
            }
        }

        private static Figura2D CrearLinea(FiguraDto dto, IEnumerable<PointF> puntos)
        {
            var linea = new LineaFigura
            {
                Algoritmo = (AlgoritmoLineaTipo)dto.AlgoritmoLinea
            };

            linea.EstablecerPuntos(puntos);
            return linea;
        }

        private static Figura2D CrearTrazo(FiguraDto dto, IEnumerable<PointF> puntos)
        {
            var trazo = new TrazoFigura
            {
                EsBorrador = dto.EsBorrador || dto.Tipo == "borrador",
                EsPincel = dto.EsPincel
            };

            trazo.EstablecerPuntos(puntos);
            return trazo;
        }

        private static Figura2D CrearRectangulo(FiguraDto dto, IEnumerable<PointF> puntos)
        {
            RectanguloFigura rectangulo =
                dto.Redondeado || dto.Tipo == "rectangulo_redondeado"
                    ? (RectanguloFigura)new RectanguloRedondeadoFigura()
                    : new RectanguloFigura();

            rectangulo.EstablecerPuntos(puntos);
            return rectangulo;
        }

        private static Figura2D CrearElipse(FiguraDto dto, IEnumerable<PointF> puntos)
        {
            var elipse = new ElipseFigura
            {
                Algoritmo = (AlgoritmoCirculoTipo)dto.AlgoritmoCirculo
            };

            elipse.EstablecerPuntos(puntos);
            return elipse;
        }

        private static Figura2D CrearBezier(IEnumerable<PointF> puntos)
        {
            var bezier = new BezierFigura();
            bezier.EstablecerPuntos(puntos);
            return bezier;
        }

        private static Figura2D CrearTexto(FiguraDto dto, IEnumerable<PointF> puntos)
        {
            var texto = new TextoFigura
            {
                Texto = dto.Texto ?? string.Empty,
                Fuente = dto.Fuente ?? "Microsoft Sans Serif",
                TamanoFuente = dto.TamanoFuente <= 0 ? 12 : dto.TamanoFuente,
                Negrita = dto.Negrita,
                Cursiva = dto.Cursiva
            };

            texto.EstablecerPuntos(puntos);
            return texto;
        }

        private static Figura2D CrearRellenoRaster(IEnumerable<PointF> puntos)
        {
            var relleno = new RellenoRasterFigura();
            relleno.EstablecerPuntos(puntos);
            return relleno;
        }

        private static Figura2D CrearPersonalizada(FiguraDto dto, IEnumerable<PointF> puntos)
        {
            var figura = new FabricaFigurasPersonalizadas().CrearVacia((FormaPersonalizada)dto.Forma);
            figura.EstablecerPuntos(puntos);
            return figura;
        }
    }

    public sealed class ExportadorImagen
    {
        public void ExportarPng(
            string ruta,
            DocumentoDibujo documento,
            RasterizadorDocumento rasterizador)
        {
            using (var bitmap = rasterizador.Renderizar(documento))
            {
                bitmap.Save(ruta, System.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}
