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
    public sealed class PuntoDto { public float X { get; set; } public float Y { get; set; } }
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
            if (string.IsNullOrWhiteSpace(ruta)) throw new ArgumentException("Ruta no válida.");
            var dto = new ProyectoDto { Ancho = documento.Ancho, Alto = documento.Alto, ColorFondo = documento.ColorFondo.ToArgb(), Figuras = documento.Figuras.Select(CrearDto).ToList() };
            var serializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue, RecursionLimit = 100 };
            File.WriteAllText(ruta, serializer.Serialize(dto), new UTF8Encoding(false)); documento.Modificado = false;
        }

        public DocumentoDibujo Abrir(string ruta)
        {
            var serializer = new JavaScriptSerializer { MaxJsonLength = int.MaxValue, RecursionLimit = 100 };
            ProyectoDto dto;
            try { dto = serializer.Deserialize<ProyectoDto>(File.ReadAllText(ruta, Encoding.UTF8)); }
            catch (Exception ex) { throw new InvalidDataException("El archivo no contiene un proyecto MichiPaint válido.", ex); }
            if (dto == null || dto.Version != 1 || dto.Ancho < 100 || dto.Ancho > 4000 || dto.Alto < 100 || dto.Alto > 4000) throw new InvalidDataException("Versión o dimensiones de proyecto no compatibles.");
            var doc = new DocumentoDibujo(dto.Ancho, dto.Alto) { ColorFondo = Color.FromArgb(dto.ColorFondo) };
            foreach (var figura in dto.Figuras ?? new List<FiguraDto>()) doc.Agregar(CrearFigura(figura));
            doc.Modificado = false; return doc;
        }

        private static FiguraDto CrearDto(Figura2D f)
        {
            var d = new FiguraDto
            {
                Tipo = f.Tipo, Id = f.Id.ToString(), Puntos = f.PuntosBase.Select(p => new PuntoDto { X = p.X, Y = p.Y }).ToList(),
                ColorLinea = f.Estilo.ColorLinea.ToArgb(), ColorRelleno = f.Estilo.ColorRelleno.ToArgb(), Grosor = f.Estilo.Grosor,
                TieneRelleno = f.Estilo.TieneRelleno, Matriz = f.Transformacion.AArreglo()
            };
            var linea = f as LineaFigura; if (linea != null) d.AlgoritmoLinea = (int)linea.Algoritmo;
            var elipse = f as ElipseFigura; if (elipse != null) d.AlgoritmoCirculo = (int)elipse.Algoritmo;
            var poligono = f as PoligonoFigura; if (poligono != null) d.Forma = (int)poligono.Forma;
            var rect = f as RectanguloFigura; if (rect != null) d.Redondeado = rect.Redondeado;
            var trazo = f as TrazoFigura; if (trazo != null) { d.EsBorrador = trazo.EsBorrador; d.EsPincel = trazo.EsPincel; }
            var texto = f as TextoFigura; if (texto != null) { d.Texto=texto.Texto; d.Fuente=texto.Fuente; d.TamanoFuente=texto.TamanoFuente; d.Negrita=texto.Negrita; d.Cursiva=texto.Cursiva; }
            return d;
        }

        private static Figura2D CrearFigura(FiguraDto d)
        {
            if (d == null || d.Puntos == null) throw new InvalidDataException("Figura incompleta.");
            var p = d.Puntos.Select(x => new PointF(x.X, x.Y)).ToList(); Figura2D f;
            if (d.Tipo == "linea") { var x = new LineaFigura(); x.EstablecerPuntos(p); x.Algoritmo=(AlgoritmoLineaTipo)d.AlgoritmoLinea; f=x; }
            else if (d.Tipo == "trazo" || d.Tipo == "borrador") { var x=new TrazoFigura(); x.EstablecerPuntos(p); x.EsBorrador=d.EsBorrador || d.Tipo=="borrador"; x.EsPincel=d.EsPincel; f=x; }
            else if (d.Tipo == "rectangulo" || d.Tipo == "rectangulo_redondeado") { RectanguloFigura x=d.Redondeado||d.Tipo=="rectangulo_redondeado"?(RectanguloFigura)new RectanguloRedondeadoFigura():new RectanguloFigura(); x.EstablecerPuntos(p); f=x; }
            else if (d.Tipo == "elipse") { var x=new ElipseFigura(); x.EstablecerPuntos(p); x.Algoritmo=(AlgoritmoCirculoTipo)d.AlgoritmoCirculo; f=x; }
            else if (d.Tipo == "bezier") { var x=new BezierFigura(); x.EstablecerPuntos(p); f=x; }
            else if (d.Tipo == "texto") { var x=new TextoFigura(); x.EstablecerPuntos(p); x.Texto=d.Texto??""; x.Fuente=d.Fuente??"Microsoft Sans Serif"; x.TamanoFuente=d.TamanoFuente<=0?12:d.TamanoFuente; x.Negrita=d.Negrita; x.Cursiva=d.Cursiva; f=x; }
            else if (d.Tipo == "relleno_raster") { var x=new RellenoRasterFigura(); x.EstablecerPuntos(p); f=x; }
            else { var x=new FabricaFigurasPersonalizadas().CrearVacia((FormaPersonalizada)d.Forma); x.EstablecerPuntos(p); f=x; }
            Guid id; if (Guid.TryParse(d.Id, out id)) f.Id=id;
            f.Estilo = new EstiloFigura { ColorLinea=Color.FromArgb(d.ColorLinea), ColorRelleno=Color.FromArgb(d.ColorRelleno), Grosor=Math.Max(1,d.Grosor), TieneRelleno=d.TieneRelleno };
            f.Transformacion = MatrizTransformacion.DesdeArreglo(d.Matriz); return f;
        }
    }

    public sealed class ExportadorImagen
    {
        public void ExportarPng(string ruta, DocumentoDibujo documento, RasterizadorDocumento rasterizador)
        {
            using (var bmp = rasterizador.Renderizar(documento)) bmp.Save(ruta, System.Drawing.Imaging.ImageFormat.Png);
        }
    }
}
