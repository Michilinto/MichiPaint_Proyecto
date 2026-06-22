using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public enum HerramientaTipo
    {
        Seleccion, SeleccionLibre, Lapiz, Pincel, Borrador, Linea, Curva,
        Relleno, Texto, Poligono, Rectangulo, RectanguloRedondeado, Elipse, Zoom
    }

    public enum AlgoritmoLineaTipo { Bresenham, DDA, PuntoMedio }
    public enum AlgoritmoCirculoTipo { PuntoMedio, Polar, EcuacionDirecta }
    public enum FormaPersonalizada { Poligono, Corazon, Estrella, Flecha, Cruz, Rombo, Trapecio }

    public sealed class EstiloFigura
    {
        public Color ColorLinea { get; set; } = Color.Black;
        public Color ColorRelleno { get; set; } = Color.FromArgb(255, 241, 216, 132);
        public int Grosor { get; set; } = 2;
        public bool TieneRelleno { get; set; }

        public EstiloFigura Clonar()
        {
            return new EstiloFigura
            {
                ColorLinea = ColorLinea,
                ColorRelleno = ColorRelleno,
                Grosor = Grosor,
                TieneRelleno = TieneRelleno
            };
        }
    }

    public sealed class MatrizTransformacion
    {
        private readonly double[,] valores = new double[3, 3];

        public MatrizTransformacion()
        {
            valores[0, 0] = valores[1, 1] = valores[2, 2] = 1;
        }

        public double[] AArreglo()
        {
            return new[] { valores[0, 0], valores[0, 1], valores[0, 2], valores[1, 0], valores[1, 1], valores[1, 2], valores[2, 0], valores[2, 1], valores[2, 2] };
        }

        public static MatrizTransformacion DesdeArreglo(double[] datos)
        {
            var m = new MatrizTransformacion();
            if (datos == null || datos.Length != 9) return m;
            for (int f = 0; f < 3; f++)
                for (int c = 0; c < 3; c++)
                    m.valores[f, c] = datos[f * 3 + c];
            return m;
        }

        public PointF Aplicar(PointF punto)
        {
            double x = valores[0, 0] * punto.X + valores[0, 1] * punto.Y + valores[0, 2];
            double y = valores[1, 0] * punto.X + valores[1, 1] * punto.Y + valores[1, 2];
            return new PointF((float)x, (float)y);
        }

        public MatrizTransformacion Clonar() => DesdeArreglo(AArreglo());

        public void Trasladar(float dx, float dy) => PreMultiplicar(CrearTraslacion(dx, dy));

        public void Escalar(float sx, float sy, PointF centro)
        {
            PreMultiplicar(CrearTraslacion(-centro.X, -centro.Y));
            PreMultiplicar(CrearEscala(sx, sy));
            PreMultiplicar(CrearTraslacion(centro.X, centro.Y));
        }

        public void Rotar(float grados, PointF centro)
        {
            PreMultiplicar(CrearTraslacion(-centro.X, -centro.Y));
            PreMultiplicar(CrearRotacion(grados));
            PreMultiplicar(CrearTraslacion(centro.X, centro.Y));
        }

        private void PreMultiplicar(MatrizTransformacion izquierda)
        {
            double[,] r = new double[3, 3];
            for (int f = 0; f < 3; f++)
                for (int c = 0; c < 3; c++)
                    for (int k = 0; k < 3; k++)
                        r[f, c] += izquierda.valores[f, k] * valores[k, c];
            for (int f = 0; f < 3; f++)
                for (int c = 0; c < 3; c++) valores[f, c] = r[f, c];
        }

        private static MatrizTransformacion CrearTraslacion(float dx, float dy)
        {
            var m = new MatrizTransformacion(); m.valores[0, 2] = dx; m.valores[1, 2] = dy; return m;
        }
        private static MatrizTransformacion CrearEscala(float sx, float sy)
        {
            var m = new MatrizTransformacion(); m.valores[0, 0] = sx; m.valores[1, 1] = sy; return m;
        }
        private static MatrizTransformacion CrearRotacion(float grados)
        {
            var m = new MatrizTransformacion();
            double a = grados * Math.PI / 180.0, cos = Math.Cos(a), sin = Math.Sin(a);
            m.valores[0, 0] = cos; m.valores[0, 1] = -sin; m.valores[1, 0] = sin; m.valores[1, 1] = cos;
            return m;
        }
    }

    public abstract class Figura2D
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public EstiloFigura Estilo { get; set; } = new EstiloFigura();
        public MatrizTransformacion Transformacion { get; set; } = new MatrizTransformacion();
        public abstract string Tipo { get; }
        public abstract IReadOnlyList<PointF> PuntosBase { get; }
        public abstract void Rasterizar(ContextoRaster contexto);

        public IReadOnlyList<PointF> PuntosTransformados => PuntosBase.Select(Transformacion.Aplicar).ToList();

        public virtual RectangleF ObtenerLimites()
        {
            var puntos = PuntosTransformados;
            if (puntos.Count == 0) return RectangleF.Empty;
            float minX = puntos.Min(p => p.X), maxX = puntos.Max(p => p.X);
            float minY = puntos.Min(p => p.Y), maxY = puntos.Max(p => p.Y);
            return RectangleF.FromLTRB(minX, minY, maxX, maxY);
        }

        public virtual IReadOnlyList<PointF> ObtenerMarcoSeleccion()
        {
            if (PuntosBase.Count == 0) return new List<PointF>();
            float minX = PuntosBase.Min(p => p.X), maxX = PuntosBase.Max(p => p.X);
            float minY = PuntosBase.Min(p => p.Y), maxY = PuntosBase.Max(p => p.Y);
            return new[]
            {
                Transformacion.Aplicar(new PointF(minX,minY)),
                Transformacion.Aplicar(new PointF(maxX,minY)),
                Transformacion.Aplicar(new PointF(maxX,maxY)),
                Transformacion.Aplicar(new PointF(minX,maxY))
            };
        }

        public virtual bool Contiene(PointF punto, float tolerancia = 5)
        {
            var r = ObtenerLimites(); r.Inflate(tolerancia, tolerancia); return r.Contains(punto);
        }

        public void Trasladar(float dx, float dy) => Transformacion.Trasladar(dx, dy);
        public void Escalar(float sx, float sy, PointF centro) => Transformacion.Escalar(sx, sy, centro);
        public void Rotar(float grados, PointF centro) => Transformacion.Rotar(grados, centro);
    }

    public sealed class DocumentoDibujo
    {
        private readonly List<Figura2D> figuras = new List<Figura2D>();
        public int Ancho { get; private set; }
        public int Alto { get; private set; }
        public Color ColorFondo { get; set; } = Color.FromArgb(255, 255, 253, 245);
        public bool Modificado { get; set; }
        public IReadOnlyList<Figura2D> Figuras => figuras.AsReadOnly();
        public event EventHandler Cambio;

        public DocumentoDibujo(int ancho = 1200, int alto = 800)
        {
            CambiarTamano(ancho, alto);
        }

        public void CambiarTamano(int ancho, int alto)
        {
            if (ancho < 100 || ancho > 4000 || alto < 100 || alto > 4000)
                throw new ArgumentOutOfRangeException("El lienzo debe medir entre 100 y 4000 píxeles.");
            Ancho = ancho; Alto = alto; Notificar(false);
        }

        public void Agregar(Figura2D figura) { if (figura == null) return; figuras.Add(figura); Notificar(); }
        public void Insertar(int indice, Figura2D figura) { figuras.Insert(Math.Max(0, Math.Min(indice, figuras.Count)), figura); Notificar(); }
        public bool Quitar(Figura2D figura) { bool r = figuras.Remove(figura); if (r) Notificar(); return r; }
        public void Limpiar() { figuras.Clear(); Notificar(); }
        public void Reemplazar(IEnumerable<Figura2D> nuevas) { figuras.Clear(); figuras.AddRange(nuevas ?? Enumerable.Empty<Figura2D>()); Notificar(); }
        public void Notificar(bool modificado = true) { if (modificado) Modificado = true; Cambio?.Invoke(this, EventArgs.Empty); }
    }

    public interface IComando { void Ejecutar(); void Deshacer(); string Nombre { get; } }

    public sealed class HistorialComandos
    {
        private readonly Stack<IComando> deshacer = new Stack<IComando>();
        private readonly Stack<IComando> rehacer = new Stack<IComando>();
        public bool PuedeDeshacer => deshacer.Count > 0;
        public bool PuedeRehacer => rehacer.Count > 0;
        public event EventHandler Cambio;

        public void Ejecutar(IComando comando)
        {
            comando.Ejecutar(); deshacer.Push(comando); rehacer.Clear();
            while (deshacer.Count > 100) RecortarFondo();
            Cambio?.Invoke(this, EventArgs.Empty);
        }

        public void RegistrarEjecutado(IComando comando)
        {
            deshacer.Push(comando); rehacer.Clear();
            while (deshacer.Count > 100) RecortarFondo();
            Cambio?.Invoke(this, EventArgs.Empty);
        }

        public void Deshacer()
        {
            if (!PuedeDeshacer) return; var c = deshacer.Pop(); c.Deshacer(); rehacer.Push(c); Cambio?.Invoke(this, EventArgs.Empty);
        }
        public void Rehacer()
        {
            if (!PuedeRehacer) return; var c = rehacer.Pop(); c.Ejecutar(); deshacer.Push(c); Cambio?.Invoke(this, EventArgs.Empty);
        }
        public void Reiniciar() { deshacer.Clear(); rehacer.Clear(); Cambio?.Invoke(this, EventArgs.Empty); }
        private void RecortarFondo()
        {
            var lista = deshacer.Reverse().Skip(1).ToList(); deshacer.Clear(); foreach (var c in lista) deshacer.Push(c);
        }
    }

    public sealed class ComandoAgregar : IComando
    {
        private readonly DocumentoDibujo doc; private readonly Figura2D figura;
        public string Nombre => "Agregar figura";
        public ComandoAgregar(DocumentoDibujo d, Figura2D f) { doc = d; figura = f; }
        public void Ejecutar() => doc.Agregar(figura);
        public void Deshacer() => doc.Quitar(figura);
    }

    public sealed class ComandoEliminar : IComando
    {
        private readonly DocumentoDibujo doc; private readonly List<Figura2D> figuras; private readonly List<int> indices;
        public string Nombre => "Eliminar";
        public ComandoEliminar(DocumentoDibujo d, IEnumerable<Figura2D> f) { doc = d; figuras = f.ToList(); var actuales = d.Figuras.ToList(); indices = figuras.Select(x => actuales.IndexOf(x)).ToList(); }
        public void Ejecutar() { foreach (var f in figuras) doc.Quitar(f); }
        public void Deshacer() { for (int i = 0; i < figuras.Count; i++) doc.Insertar(indices[i], figuras[i]); }
    }

    public sealed class ComandoLimpiar : IComando
    {
        private readonly DocumentoDibujo doc; private readonly List<Figura2D> anteriores;
        public string Nombre => "Limpiar lienzo";
        public ComandoLimpiar(DocumentoDibujo d) { doc = d; anteriores = d.Figuras.ToList(); }
        public void Ejecutar() => doc.Limpiar();
        public void Deshacer() => doc.Reemplazar(anteriores);
    }

    public sealed class ComandoTransformar : IComando
    {
        private readonly DocumentoDibujo doc; private readonly List<Figura2D> figuras;
        private readonly List<double[]> antes, despues;
        public string Nombre => "Transformar";
        public ComandoTransformar(DocumentoDibujo d, IEnumerable<Figura2D> f, IEnumerable<double[]> a, IEnumerable<double[]> p)
        { doc = d; figuras = f.ToList(); antes = a.Select(x => x.ToArray()).ToList(); despues = p.Select(x => x.ToArray()).ToList(); }
        public void Ejecutar() => Aplicar(despues);
        public void Deshacer() => Aplicar(antes);
        private void Aplicar(List<double[]> valores) { for (int i = 0; i < figuras.Count; i++) figuras[i].Transformacion = MatrizTransformacion.DesdeArreglo(valores[i]); doc.Notificar(); }
    }

    public sealed class ComandoEstilo : IComando
    {
        private readonly DocumentoDibujo doc; private readonly List<Figura2D> figuras;
        private readonly List<EstiloFigura> antes, despues;
        public string Nombre => "Cambiar estilo";
        public ComandoEstilo(DocumentoDibujo d, IEnumerable<Figura2D> f, EstiloFigura nuevo)
        { doc=d; figuras=f.ToList(); antes=figuras.Select(x=>x.Estilo.Clonar()).ToList(); despues=figuras.Select(x=>nuevo.Clonar()).ToList(); }
        public void Ejecutar()=>Aplicar(despues);
        public void Deshacer()=>Aplicar(antes);
        private void Aplicar(List<EstiloFigura> estilos){for(int i=0;i<figuras.Count;i++)figuras[i].Estilo=estilos[i].Clonar();doc.Notificar();}
    }
}
