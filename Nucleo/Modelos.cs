using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public enum HerramientaTipo
    {
        Seleccion,
        SeleccionLibre,
        Lapiz,
        Pincel,
        Borrador,
        Linea,
        Curva,
        Relleno,
        Texto,
        Poligono,
        Rectangulo,
        RectanguloRedondeado,
        Elipse,
        Zoom
    }

    public enum AlgoritmoLineaTipo
    {
        Bresenham,
        DDA,
        PuntoMedio
    }

    public enum AlgoritmoCirculoTipo
    {
        PuntoMedio,
        Polar,
        EcuacionDirecta
    }

    public enum FormaPersonalizada
    {
        Poligono,
        Corazon,
        Estrella,
        Flecha,
        Cruz,
        Rombo,
        Trapecio
    }

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
            valores[0, 0] = 1;
            valores[1, 1] = 1;
            valores[2, 2] = 1;
        }

        public double[] AArreglo()
        {
            return new[]
            {
                valores[0, 0],
                valores[0, 1],
                valores[0, 2],
                valores[1, 0],
                valores[1, 1],
                valores[1, 2],
                valores[2, 0],
                valores[2, 1],
                valores[2, 2]
            };
        }

        public static MatrizTransformacion DesdeArreglo(double[] datos)
        {
            var matriz = new MatrizTransformacion();

            if (datos == null || datos.Length != 9)
            {
                return matriz;
            }

            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    matriz.valores[fila, columna] = datos[fila * 3 + columna];
                }
            }

            return matriz;
        }

        public PointF Aplicar(PointF punto)
        {
            double x = valores[0, 0] * punto.X + valores[0, 1] * punto.Y + valores[0, 2];
            double y = valores[1, 0] * punto.X + valores[1, 1] * punto.Y + valores[1, 2];

            return new PointF((float)x, (float)y);
        }

        public MatrizTransformacion Clonar()
        {
            return DesdeArreglo(AArreglo());
        }

        public void Trasladar(float desplazamientoX, float desplazamientoY)
        {
            PreMultiplicar(CrearTraslacion(desplazamientoX, desplazamientoY));
        }

        public void Escalar(float escalaX, float escalaY, PointF centro)
        {
            PreMultiplicar(CrearTraslacion(-centro.X, -centro.Y));
            PreMultiplicar(CrearEscala(escalaX, escalaY));
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
            double[,] resultado = new double[3, 3];

            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        resultado[fila, columna] += izquierda.valores[fila, k] * valores[k, columna];
                    }
                }
            }

            for (int fila = 0; fila < 3; fila++)
            {
                for (int columna = 0; columna < 3; columna++)
                {
                    valores[fila, columna] = resultado[fila, columna];
                }
            }
        }

        private static MatrizTransformacion CrearTraslacion(float desplazamientoX, float desplazamientoY)
        {
            var matriz = new MatrizTransformacion();
            matriz.valores[0, 2] = desplazamientoX;
            matriz.valores[1, 2] = desplazamientoY;

            return matriz;
        }

        private static MatrizTransformacion CrearEscala(float escalaX, float escalaY)
        {
            var matriz = new MatrizTransformacion();
            matriz.valores[0, 0] = escalaX;
            matriz.valores[1, 1] = escalaY;

            return matriz;
        }

        private static MatrizTransformacion CrearRotacion(float grados)
        {
            var matriz = new MatrizTransformacion();
            double angulo = grados * Math.PI / 180.0;
            double coseno = Math.Cos(angulo);
            double seno = Math.Sin(angulo);

            matriz.valores[0, 0] = coseno;
            matriz.valores[0, 1] = -seno;
            matriz.valores[1, 0] = seno;
            matriz.valores[1, 1] = coseno;

            return matriz;
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

        public IReadOnlyList<PointF> PuntosTransformados
        {
            get
            {
                return PuntosBase.Select(Transformacion.Aplicar).ToList();
            }
        }

        public virtual RectangleF ObtenerLimites()
        {
            var puntos = PuntosTransformados;

            if (puntos.Count == 0)
            {
                return RectangleF.Empty;
            }

            float minimoX = puntos.Min(punto => punto.X);
            float maximoX = puntos.Max(punto => punto.X);
            float minimoY = puntos.Min(punto => punto.Y);
            float maximoY = puntos.Max(punto => punto.Y);

            return RectangleF.FromLTRB(minimoX, minimoY, maximoX, maximoY);
        }

        public virtual IReadOnlyList<PointF> ObtenerMarcoSeleccion()
        {
            if (PuntosBase.Count == 0)
            {
                return new List<PointF>();
            }

            float minimoX = PuntosBase.Min(punto => punto.X);
            float maximoX = PuntosBase.Max(punto => punto.X);
            float minimoY = PuntosBase.Min(punto => punto.Y);
            float maximoY = PuntosBase.Max(punto => punto.Y);

            return new[]
            {
                Transformacion.Aplicar(new PointF(minimoX, minimoY)),
                Transformacion.Aplicar(new PointF(maximoX, minimoY)),
                Transformacion.Aplicar(new PointF(maximoX, maximoY)),
                Transformacion.Aplicar(new PointF(minimoX, maximoY))
            };
        }

        public virtual bool Contiene(PointF punto, float tolerancia = 5)
        {
            var limites = ObtenerLimites();
            limites.Inflate(tolerancia, tolerancia);

            return limites.Contains(punto);
        }

        public void Trasladar(float desplazamientoX, float desplazamientoY)
        {
            Transformacion.Trasladar(desplazamientoX, desplazamientoY);
        }

        public void Escalar(float escalaX, float escalaY, PointF centro)
        {
            Transformacion.Escalar(escalaX, escalaY, centro);
        }

        public void Rotar(float grados, PointF centro)
        {
            Transformacion.Rotar(grados, centro);
        }
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
            bool dimensionesInvalidas = ancho < 100 || ancho > 4000 || alto < 100 || alto > 4000;

            if (dimensionesInvalidas)
            {
                throw new ArgumentOutOfRangeException("El lienzo debe medir entre 100 y 4000 píxeles.");
            }

            Ancho = ancho;
            Alto = alto;

            Notificar(false);
        }

        public void Agregar(Figura2D figura)
        {
            if (figura == null)
            {
                return;
            }

            figuras.Add(figura);
            Notificar();
        }

        public void Insertar(int indice, Figura2D figura)
        {
            int indiceSeguro = Math.Max(0, Math.Min(indice, figuras.Count));
            figuras.Insert(indiceSeguro, figura);
            Notificar();
        }

        public bool Quitar(Figura2D figura)
        {
            bool eliminado = figuras.Remove(figura);

            if (eliminado)
            {
                Notificar();
            }

            return eliminado;
        }

        public void Limpiar()
        {
            figuras.Clear();
            Notificar();
        }

        public void Reemplazar(IEnumerable<Figura2D> nuevasFiguras)
        {
            figuras.Clear();
            figuras.AddRange(nuevasFiguras ?? Enumerable.Empty<Figura2D>());
            Notificar();
        }

        public void Notificar(bool modificado = true)
        {
            if (modificado)
            {
                Modificado = true;
            }

            Cambio?.Invoke(this, EventArgs.Empty);
        }
    }

    public interface IComando
    {
        string Nombre { get; }
        void Ejecutar();
        void Deshacer();
    }

    public sealed class HistorialComandos
    {
        private readonly Stack<IComando> deshacer = new Stack<IComando>();
        private readonly Stack<IComando> rehacer = new Stack<IComando>();

        public bool PuedeDeshacer => deshacer.Count > 0;
        public bool PuedeRehacer => rehacer.Count > 0;

        public event EventHandler Cambio;

        public void Ejecutar(IComando comando)
        {
            comando.Ejecutar();
            deshacer.Push(comando);
            rehacer.Clear();
            RecortarSiEsNecesario();
            Cambio?.Invoke(this, EventArgs.Empty);
        }

        public void RegistrarEjecutado(IComando comando)
        {
            deshacer.Push(comando);
            rehacer.Clear();
            RecortarSiEsNecesario();
            Cambio?.Invoke(this, EventArgs.Empty);
        }

        public void Deshacer()
        {
            if (!PuedeDeshacer)
            {
                return;
            }

            var comando = deshacer.Pop();
            comando.Deshacer();
            rehacer.Push(comando);
            Cambio?.Invoke(this, EventArgs.Empty);
        }

        public void Rehacer()
        {
            if (!PuedeRehacer)
            {
                return;
            }

            var comando = rehacer.Pop();
            comando.Ejecutar();
            deshacer.Push(comando);
            Cambio?.Invoke(this, EventArgs.Empty);
        }

        public void Reiniciar()
        {
            deshacer.Clear();
            rehacer.Clear();
            Cambio?.Invoke(this, EventArgs.Empty);
        }

        private void RecortarSiEsNecesario()
        {
            while (deshacer.Count > 100)
            {
                RecortarFondo();
            }
        }

        private void RecortarFondo()
        {
            var lista = deshacer.Reverse().Skip(1).ToList();
            deshacer.Clear();

            foreach (var comando in lista)
            {
                deshacer.Push(comando);
            }
        }
    }

    public sealed class ComandoAgregar : IComando
    {
        private readonly DocumentoDibujo documento;
        private readonly Figura2D figura;

        public string Nombre => "Agregar figura";

        public ComandoAgregar(DocumentoDibujo documento, Figura2D figura)
        {
            this.documento = documento;
            this.figura = figura;
        }

        public void Ejecutar()
        {
            documento.Agregar(figura);
        }

        public void Deshacer()
        {
            documento.Quitar(figura);
        }
    }

    public sealed class ComandoEliminar : IComando
    {
        private readonly DocumentoDibujo documento;
        private readonly List<Figura2D> figuras;
        private readonly List<int> indices;

        public string Nombre => "Eliminar";

        public ComandoEliminar(DocumentoDibujo documento, IEnumerable<Figura2D> figuras)
        {
            this.documento = documento;
            this.figuras = figuras.ToList();

            var actuales = documento.Figuras.ToList();
            indices = this.figuras.Select(figura => actuales.IndexOf(figura)).ToList();
        }

        public void Ejecutar()
        {
            foreach (var figura in figuras)
            {
                documento.Quitar(figura);
            }
        }

        public void Deshacer()
        {
            for (int i = 0; i < figuras.Count; i++)
            {
                documento.Insertar(indices[i], figuras[i]);
            }
        }
    }

    public sealed class ComandoLimpiar : IComando
    {
        private readonly DocumentoDibujo documento;
        private readonly List<Figura2D> anteriores;

        public string Nombre => "Limpiar lienzo";

        public ComandoLimpiar(DocumentoDibujo documento)
        {
            this.documento = documento;
            anteriores = documento.Figuras.ToList();
        }

        public void Ejecutar()
        {
            documento.Limpiar();
        }

        public void Deshacer()
        {
            documento.Reemplazar(anteriores);
        }
    }

    public sealed class ComandoTransformar : IComando
    {
        private readonly DocumentoDibujo documento;
        private readonly List<Figura2D> figuras;
        private readonly List<double[]> antes;
        private readonly List<double[]> despues;

        public string Nombre => "Transformar";

        public ComandoTransformar(
            DocumentoDibujo documento,
            IEnumerable<Figura2D> figuras,
            IEnumerable<double[]> antes,
            IEnumerable<double[]> despues)
        {
            this.documento = documento;
            this.figuras = figuras.ToList();
            this.antes = antes.Select(matriz => matriz.ToArray()).ToList();
            this.despues = despues.Select(matriz => matriz.ToArray()).ToList();
        }

        public void Ejecutar()
        {
            Aplicar(despues);
        }

        public void Deshacer()
        {
            Aplicar(antes);
        }

        private void Aplicar(List<double[]> valores)
        {
            for (int i = 0; i < figuras.Count; i++)
            {
                figuras[i].Transformacion = MatrizTransformacion.DesdeArreglo(valores[i]);
            }

            documento.Notificar();
        }
    }

    public sealed class ComandoEstilo : IComando
    {
        private readonly DocumentoDibujo documento;
        private readonly List<Figura2D> figuras;
        private readonly List<EstiloFigura> antes;
        private readonly List<EstiloFigura> despues;

        public string Nombre => "Cambiar estilo";

        public ComandoEstilo(
            DocumentoDibujo documento,
            IEnumerable<Figura2D> figuras,
            EstiloFigura nuevo)
        {
            this.documento = documento;
            this.figuras = figuras.ToList();
            antes = this.figuras.Select(figura => figura.Estilo.Clonar()).ToList();
            despues = this.figuras.Select(figura => nuevo.Clonar()).ToList();
        }

        public void Ejecutar()
        {
            Aplicar(despues);
        }

        public void Deshacer()
        {
            Aplicar(antes);
        }

        private void Aplicar(List<EstiloFigura> estilos)
        {
            for (int i = 0; i < figuras.Count; i++)
            {
                figuras[i].Estilo = estilos[i].Clonar();
            }

            documento.Notificar();
        }
    }
}
