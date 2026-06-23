using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public interface IHerramienta
    {
        void Iniciar(PointF punto);
        void Mover(PointF punto);
        void Terminar(PointF punto);
        void Cancelar();
    }

    public sealed class GestorHerramientas : IHerramienta
    {
        private enum TransformacionActiva
        {
            Ninguna,
            Trasladar,
            Escalar,
            Rotar
        }

        private readonly List<PointF> puntosTemporales = new List<PointF>();
        private readonly List<PointF> lazo = new List<PointF>();
        private readonly List<Figura2D> seleccion = new List<Figura2D>();

        private PointF inicio;
        private PointF ultimo;
        private bool presionado;
        private Figura2D previa;
        private TransformacionActiva transformacion;
        private List<double[]> matricesAntes;
        private PointF centroTransformacion;
        private bool editandoFiguraReciente;

        public DocumentoDibujo Documento { get; }
        public HistorialComandos Historial { get; }
        public RasterizadorDocumento Rasterizador { get; }

        public HerramientaTipo HerramientaActual { get; set; } = HerramientaTipo.Lapiz;
        public FormaPersonalizada FormaActual { get; set; } = FormaPersonalizada.Poligono;
        public AlgoritmoLineaTipo AlgoritmoLinea { get; set; } = AlgoritmoLineaTipo.Bresenham;
        public AlgoritmoCirculoTipo AlgoritmoCirculo { get; set; } = AlgoritmoCirculoTipo.PuntoMedio;
        public EstiloFigura EstiloActual { get; } = new EstiloFigura();
        public Font FuenteTexto { get; set; } = new Font("Microsoft Sans Serif", 12);
        public Figura2D VistaPrevia => previa;
        public IReadOnlyList<PointF> Lazo => lazo.AsReadOnly();
        public IReadOnlyList<Figura2D> Seleccion => seleccion.AsReadOnly();
        public bool EditandoFiguraReciente => editandoFiguraReciente;
        public float ToleranciaInteraccion { get; set; } = 9f;

        public PointF? PuntoInicialPoligono
        {
            get
            {
                if (puntosTemporales.Count == 0)
                {
                    return null;
                }

                return puntosTemporales[0];
            }
        }

        public IReadOnlyList<PointF> PuntosCurvaEnConstruccion
        {
            get
            {
                if (HerramientaActual != HerramientaTipo.Curva)
                {
                    return new List<PointF>().AsReadOnly();
                }

                return puntosTemporales.AsReadOnly();
            }
        }

        public bool ConstruyendoCurva
        {
            get
            {
                return
                    HerramientaActual == HerramientaTipo.Curva &&
                    puntosTemporales.Count > 0 &&
                    !editandoFiguraReciente;
            }
        }

        public event EventHandler CambioVisual;
        public event Action<PointF> TextoSolicitado;
        public event Action<int> SeleccionActualizada;

        public GestorHerramientas(
            DocumentoDibujo documento,
            HistorialComandos historial,
            RasterizadorDocumento rasterizador)
        {
            Documento = documento;
            Historial = historial;
            Rasterizador = rasterizador;
        }

        public void Iniciar(PointF punto)
        {
            inicio = punto;
            ultimo = punto;
            presionado = true;
            previa = null;

            if (IntentarEditarFiguraReciente(punto))
            {
                return;
            }

            if (HerramientaActual == HerramientaTipo.Seleccion)
            {
                IniciarSeleccion(punto);
                return;
            }

            if (HerramientaActual == HerramientaTipo.SeleccionLibre)
            {
                IniciarSeleccionLibre(punto);
                return;
            }

            if (HerramientaActual == HerramientaTipo.Relleno)
            {
                AplicarRelleno(Point.Round(punto));
                presionado = false;
                return;
            }

            if (HerramientaActual == HerramientaTipo.Texto)
            {
                IniciarTexto(punto);
                return;
            }

            if (HerramientaActual == HerramientaTipo.Poligono && FormaActual == FormaPersonalizada.Poligono)
            {
                IniciarPoligonoLibre(punto);
                return;
            }

            if (HerramientaActual == HerramientaTipo.Curva)
            {
                IniciarCurva(punto);
                return;
            }

            if (EsHerramientaTrazoLibre())
            {
                IniciarTrazoLibre(punto);
            }
        }

        public void Mover(PointF punto)
        {
            if (!presionado && ConstruyendoCurva)
            {
                PrevisualizarCurva(punto);
                return;
            }

            if (DebePrevisualizarPoligonoLibre())
            {
                PrevisualizarPoligonoLibre(punto);
                return;
            }

            if (!presionado)
            {
                return;
            }

            if (transformacion != TransformacionActiva.Ninguna)
            {
                TransformarSeleccion(punto);
                ultimo = punto;
                Cambio();
                return;
            }

            if (HerramientaActual == HerramientaTipo.SeleccionLibre)
            {
                MoverLazo(punto);
                return;
            }

            var trazo = previa as TrazoFigura;
            if (trazo != null)
            {
                MoverTrazo(trazo, punto);
                return;
            }

            previa = CrearFiguraArrastre(inicio, punto);
            ultimo = punto;
            Cambio();
        }

        public void Terminar(PointF punto)
        {
            if (!presionado)
            {
                return;
            }

            presionado = false;

            if (transformacion != TransformacionActiva.Ninguna)
            {
                FinalizarTransformacion();
                return;
            }

            if (HerramientaActual == HerramientaTipo.SeleccionLibre)
            {
                SeleccionarConLazo();
                lazo.Clear();
                Cambio();
                return;
            }

            if (previa == null)
            {
                previa = CrearFiguraArrastre(inicio, punto);
            }

            if (previa != null)
            {
                ConfirmarFiguraPrevia();
            }

            Cambio();
        }

        public void DobleClic(PointF punto)
        {
            if (HerramientaActual == HerramientaTipo.Poligono &&
                FormaActual == FormaPersonalizada.Poligono &&
                puntosTemporales.Count >= 3)
            {
                ConfirmarTemporal();
                return;
            }

            if (HerramientaActual == HerramientaTipo.Texto)
            {
                EditarTextoExistente(punto);
            }
        }

        public void Cancelar()
        {
            previa = null;
            puntosTemporales.Clear();
            lazo.Clear();
            presionado = false;
            transformacion = TransformacionActiva.Ninguna;
            editandoFiguraReciente = false;
            seleccion.Clear();

            NotificarSeleccion();
            Cambio();
        }

        public void AgregarTexto(PointF punto, string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
            {
                return;
            }

            var existente = seleccion
                .OfType<TextoFigura>()
                .FirstOrDefault(figura => figura.Contiene(punto));

            if (existente != null)
            {
                existente.Texto = texto;
                Documento.Notificar();
                return;
            }

            var figuraTexto = new TextoFigura(punto, texto, FuenteTexto, EstiloActual.ColorLinea);
            Historial.Ejecutar(new ComandoAgregar(Documento, figuraTexto));
            SeleccionarFiguraReciente(figuraTexto);
        }

        public void EliminarSeleccion()
        {
            if (seleccion.Count == 0)
            {
                return;
            }

            Historial.Ejecutar(new ComandoEliminar(Documento, seleccion));
            seleccion.Clear();
            NotificarSeleccion();
            Cambio();
        }

        public void LimpiarSeleccion()
        {
            seleccion.Clear();
            NotificarSeleccion();
            Cambio();
        }

        public RectangleF LimitesSeleccion()
        {
            if (seleccion.Count == 0)
            {
                return RectangleF.Empty;
            }

            var limites = seleccion[0].ObtenerLimites();

            foreach (var figura in seleccion.Skip(1))
            {
                limites = RectangleF.Union(limites, figura.ObtenerLimites());
            }

            return limites;
        }

        public PointF PuntoTiradorRotacion()
        {
            var marco = MarcoSeleccion();

            if (marco.Count < 4)
            {
                return PointF.Empty;
            }

            PointF medio = new PointF(
                (marco[0].X + marco[1].X) / 2f,
                (marco[0].Y + marco[1].Y) / 2f);

            float direccionX = marco[1].X - marco[0].X;
            float direccionY = marco[1].Y - marco[0].Y;
            float largo = (float)Math.Sqrt(direccionX * direccionX + direccionY * direccionY);

            if (largo < .001f)
            {
                direccionX = 1;
                direccionY = 0;
                largo = 1;
            }

            PointF candidato = new PointF(
                medio.X + direccionY / largo * 18f,
                medio.Y - direccionX / largo * 18f);

            if (candidato.Y >= 0)
            {
                return candidato;
            }

            return new PointF(
                medio.X - direccionY / largo * 18f,
                medio.Y + direccionX / largo * 18f);
        }

        public PointF PuntoTiradorEscala()
        {
            var marco = MarcoSeleccion();

            if (marco.Count < 3)
            {
                return PointF.Empty;
            }

            return marco[2];
        }

        public IReadOnlyList<PointF> MarcoSeleccion()
        {
            if (seleccion.Count == 1)
            {
                return seleccion[0].ObtenerMarcoSeleccion();
            }

            RectangleF limites = LimitesSeleccion();

            if (limites.IsEmpty)
            {
                return new List<PointF>();
            }

            return new[]
            {
                new PointF(limites.Left, limites.Top),
                new PointF(limites.Right, limites.Top),
                new PointF(limites.Right, limites.Bottom),
                new PointF(limites.Left, limites.Bottom)
            };
        }

        public void AplicarEstiloASeleccion()
        {
            if (seleccion.Count > 0)
            {
                Historial.Ejecutar(new ComandoEstilo(Documento, seleccion, EstiloActual));
            }

            Cambio();
        }

        public bool TrasladarSeleccion(float desplazamientoX, float desplazamientoY)
        {
            bool sinMovimiento =
                Math.Abs(desplazamientoX) < .001f &&
                Math.Abs(desplazamientoY) < .001f;

            if (seleccion.Count == 0 || sinMovimiento)
            {
                return false;
            }

            return AplicarTransformacionNumerica(
                (figura, centro) => figura.Trasladar(desplazamientoX, desplazamientoY));
        }

        public bool RotarSeleccion(float grados)
        {
            if (seleccion.Count == 0 || Math.Abs(grados) < .001f)
            {
                return false;
            }

            return AplicarTransformacionNumerica(
                (figura, centro) => figura.Rotar(grados, centro));
        }

        public bool EscalarSeleccion(float escalaX, float escalaY)
        {
            bool escalaInvalida = escalaX <= 0 || escalaY <= 0;
            bool sinEscala = Math.Abs(escalaX - 1) < .001f && Math.Abs(escalaY - 1) < .001f;

            if (seleccion.Count == 0 || escalaInvalida || sinEscala)
            {
                return false;
            }

            return AplicarTransformacionNumerica(
                (figura, centro) => figura.Escalar(escalaX, escalaY, centro));
        }

        private bool IntentarEditarFiguraReciente(PointF punto)
        {
            if (!editandoFiguraReciente || seleccion.Count == 0)
            {
                return false;
            }

            if (EsInteraccionConSeleccion(punto))
            {
                IniciarSeleccion(punto);
                return true;
            }

            seleccion.Clear();
            editandoFiguraReciente = false;

            NotificarSeleccion();
            Cambio();
            return false;
        }

        private void IniciarSeleccionLibre(PointF punto)
        {
            if (EsInteraccionConSeleccion(punto))
            {
                IniciarSeleccion(punto);
                return;
            }

            lazo.Clear();
            lazo.Add(punto);
            Cambio();
        }

        private void IniciarTexto(PointF punto)
        {
            presionado = false;

            var texto = Documento.Figuras
                .Reverse()
                .OfType<TextoFigura>()
                .FirstOrDefault(figura => figura.Contiene(punto));

            seleccion.Clear();

            if (texto != null)
            {
                seleccion.Add(texto);
            }

            TextoSolicitado?.Invoke(punto);
        }

        private void IniciarPoligonoLibre(PointF punto)
        {
            if (puntosTemporales.Count >= 3 &&
                LineaFigura.Distancia(puntosTemporales[0], punto) <= ToleranciaInteraccion)
            {
                presionado = false;
                ConfirmarTemporal();
                return;
            }

            bool puntoNuevo =
                puntosTemporales.Count == 0 ||
                LineaFigura.Distancia(puntosTemporales[puntosTemporales.Count - 1], punto) > 2f;

            if (puntoNuevo)
            {
                puntosTemporales.Add(punto);
            }

            previa = puntosTemporales.Count > 1
                ? new PoligonoFigura(puntosTemporales, EstiloActual) { Cerrado = false }
                : null;

            presionado = false;
            Cambio();
        }

        private void IniciarCurva(PointF punto)
        {
            bool puntoNuevo =
                puntosTemporales.Count == 0 ||
                LineaFigura.Distancia(puntosTemporales[puntosTemporales.Count - 1], punto) > 2f;

            if (puntoNuevo)
            {
                puntosTemporales.Add(punto);
            }

            previa = puntosTemporales.Count > 1
                ? new BezierFigura(puntosTemporales, EstiloActual)
                : null;

            presionado = false;

            if (puntosTemporales.Count == 4)
            {
                ConfirmarTemporal();
            }
            else
            {
                Cambio();
            }
        }

        private void IniciarTrazoLibre(PointF punto)
        {
            var estilo = EstiloActual.Clonar();
            bool esBorrador = HerramientaActual == HerramientaTipo.Borrador;
            bool esPincel = HerramientaActual == HerramientaTipo.Pincel;

            previa = new TrazoFigura(new[] { punto }, estilo, esBorrador, esPincel);
            Cambio();
        }

        private void PrevisualizarCurva(PointF punto)
        {
            var puntosVista = new List<PointF>(puntosTemporales);

            if (LineaFigura.Distancia(puntosVista[puntosVista.Count - 1], punto) > .5f)
            {
                puntosVista.Add(punto);
            }

            previa = puntosVista.Count > 1
                ? new BezierFigura(puntosVista, EstiloActual)
                : null;

            Cambio();
        }

        private bool DebePrevisualizarPoligonoLibre()
        {
            return
                !presionado &&
                HerramientaActual == HerramientaTipo.Poligono &&
                FormaActual == FormaPersonalizada.Poligono &&
                puntosTemporales.Count > 0;
        }

        private void PrevisualizarPoligonoLibre(PointF punto)
        {
            var puntosVista = new List<PointF>(puntosTemporales);

            if (LineaFigura.Distancia(puntosVista[puntosVista.Count - 1], punto) > .5f)
            {
                puntosVista.Add(punto);
            }

            previa = puntosVista.Count > 1
                ? new PoligonoFigura(puntosVista, EstiloActual) { Cerrado = false }
                : null;

            Cambio();
        }

        private void MoverLazo(PointF punto)
        {
            if (LineaFigura.Distancia(punto, ultimo) > 2)
            {
                lazo.Add(punto);
            }

            ultimo = punto;
            Cambio();
        }

        private void MoverTrazo(TrazoFigura trazo, PointF punto)
        {
            if (LineaFigura.Distancia(punto, ultimo) > 1)
            {
                trazo.AgregarPunto(punto);
            }

            ultimo = punto;
            Cambio();
        }

        private void ConfirmarFiguraPrevia()
        {
            var creada = previa;
            Historial.Ejecutar(new ComandoAgregar(Documento, creada));
            previa = null;

            if (DebeEditarAlCrear(creada))
            {
                SeleccionarFiguraReciente(creada);
            }
        }

        private void EditarTextoExistente(PointF punto)
        {
            var texto = Documento.Figuras
                .Reverse()
                .OfType<TextoFigura>()
                .FirstOrDefault(figura => figura.Contiene(punto));

            if (texto == null)
            {
                return;
            }

            SeleccionarSolo(texto);
            TextoSolicitado?.Invoke(punto);
        }

        private PointF CentroSeleccion()
        {
            var marco = MarcoSeleccion();

            if (marco.Count == 0)
            {
                return PointF.Empty;
            }

            return new PointF(marco.Average(punto => punto.X), marco.Average(punto => punto.Y));
        }

        private bool AplicarTransformacionNumerica(Action<Figura2D, PointF> operacion)
        {
            var figuras = seleccion.ToList();
            var antes = figuras.Select(figura => figura.Transformacion.AArreglo()).ToList();
            PointF centro = CentroSeleccion();

            foreach (var figura in figuras)
            {
                operacion(figura, centro);
            }

            var despues = figuras.Select(figura => figura.Transformacion.AArreglo()).ToList();

            Documento.Notificar();
            Historial.RegistrarEjecutado(new ComandoTransformar(Documento, figuras, antes, despues));
            Cambio();

            return true;
        }

        private Figura2D CrearFiguraArrastre(PointF a, PointF b)
        {
            var rectangulo = Normalizar(a, b);

            if (rectangulo.Width < 1 && rectangulo.Height < 1)
            {
                return null;
            }

            if (HerramientaActual == HerramientaTipo.Linea)
            {
                return new LineaFigura(a, b, EstiloActual, AlgoritmoLinea);
            }

            if (HerramientaActual == HerramientaTipo.Rectangulo)
            {
                return new RectanguloFigura(rectangulo, EstiloActual, false);
            }

            if (HerramientaActual == HerramientaTipo.RectanguloRedondeado)
            {
                return new RectanguloRedondeadoFigura(rectangulo, EstiloActual);
            }

            if (HerramientaActual == HerramientaTipo.Elipse)
            {
                return new ElipseFigura(rectangulo, EstiloActual, AlgoritmoCirculo);
            }

            if (HerramientaActual == HerramientaTipo.Poligono && FormaActual != FormaPersonalizada.Poligono)
            {
                return new FabricaFigurasPersonalizadas().Crear(FormaActual, rectangulo, EstiloActual);
            }

            return null;
        }

        private void ConfirmarTemporal()
        {
            if (HerramientaActual == HerramientaTipo.Poligono &&
                FormaActual == FormaPersonalizada.Poligono)
            {
                ConfirmarPoligonoLibre();
            }
            else if (previa != null)
            {
                ConfirmarFiguraTemporal();
            }

            previa = null;
            puntosTemporales.Clear();
            Cambio();
        }

        private void ConfirmarPoligonoLibre()
        {
            if (puntosTemporales.Count < 3)
            {
                return;
            }

            var figura = new PoligonoFigura(puntosTemporales, EstiloActual)
            {
                Cerrado = true
            };

            Historial.Ejecutar(new ComandoAgregar(Documento, figura));
            SeleccionarFiguraReciente(figura);
        }

        private void ConfirmarFiguraTemporal()
        {
            var figura = previa;
            Historial.Ejecutar(new ComandoAgregar(Documento, figura));

            if (DebeEditarAlCrear(figura))
            {
                SeleccionarFiguraReciente(figura);
            }
        }

        private void AplicarRelleno(Point semilla)
        {
            var figuraEditable = BuscarFiguraCerradaEnPunto(semilla);

            if (figuraEditable != null)
            {
                AplicarRellenoAEstilo(figuraEditable);
                return;
            }

            var buffer = Rasterizador.RenderizarBuffer(Documento);
            var tramos = new RellenoFloodFill().RellenarTramos(buffer, semilla, EstiloActual.ColorRelleno);

            if (tramos.Count > 0)
            {
                var figura = new RellenoRasterFigura(tramos, EstiloActual.ColorRelleno);
                Historial.Ejecutar(new ComandoAgregar(Documento, figura));
            }

            Cambio();
        }

        private Figura2D BuscarFiguraCerradaEnPunto(Point punto)
        {
            PointF posicion = new PointF(punto.X, punto.Y);

            return Documento.Figuras
                .Reverse()
                .Where(figura => !(figura is RellenoRasterFigura))
                .Where(EsFiguraCerradaRellenable)
                .FirstOrDefault(figura => figura.Contiene(posicion));
        }

        private static bool EsFiguraCerradaRellenable(Figura2D figura)
        {
            var poligono = figura as PoligonoFigura;

            if (poligono != null)
            {
                return poligono.Cerrado;
            }

            return figura is ElipseFigura;
        }

        private void AplicarRellenoAEstilo(Figura2D figura)
        {
            var nuevoEstilo = figura.Estilo.Clonar();
            nuevoEstilo.ColorRelleno = EstiloActual.ColorRelleno;
            nuevoEstilo.TieneRelleno = true;

            Historial.Ejecutar(new ComandoEstilo(Documento, new[] { figura }, nuevoEstilo));
            SeleccionarFiguraReciente(figura);
            Cambio();
        }

        private void IniciarSeleccion(PointF punto)
        {
            RectangleF limites = LimitesSeleccion();
            transformacion = TransformacionActiva.Ninguna;

            if (seleccion.Count > 0 && Cerca(punto, PuntoTiradorEscala(), ToleranciaInteraccion))
            {
                transformacion = TransformacionActiva.Escalar;
            }
            else if (seleccion.Count > 0 && Cerca(punto, PuntoTiradorRotacion(), ToleranciaInteraccion))
            {
                transformacion = TransformacionActiva.Rotar;
            }
            else if (seleccion.Count > 1 && limites.Contains(punto))
            {
                transformacion = TransformacionActiva.Trasladar;
            }
            else
            {
                SeleccionarFiguraEnPunto(punto);
            }

            centroTransformacion = CentroSeleccion();
            matricesAntes = seleccion.Select(figura => figura.Transformacion.AArreglo()).ToList();
            Cambio();
        }

        private void SeleccionarFiguraEnPunto(PointF punto)
        {
            var encontrada = Documento.Figuras
                .Reverse()
                .FirstOrDefault(figura => !(figura is RellenoRasterFigura) && figura.Contiene(punto));

            if (encontrada == null)
            {
                seleccion.Clear();
                NotificarSeleccion();
                presionado = false;
                Cambio();
                return;
            }

            if (!seleccion.Contains(encontrada))
            {
                SeleccionarSolo(encontrada);
            }

            transformacion = TransformacionActiva.Trasladar;
        }

        private bool EsInteraccionConSeleccion(PointF punto)
        {
            if (seleccion.Count == 0)
            {
                return false;
            }

            bool sobreTirador =
                Cerca(punto, PuntoTiradorEscala(), ToleranciaInteraccion) ||
                Cerca(punto, PuntoTiradorRotacion(), ToleranciaInteraccion);

            if (sobreTirador)
            {
                return true;
            }

            if (seleccion.Any(figura => figura.Contiene(punto)))
            {
                return true;
            }

            return seleccion.Count > 1 && LimitesSeleccion().Contains(punto);
        }

        private void TransformarSeleccion(PointF punto)
        {
            if (seleccion.Count == 0 || transformacion == TransformacionActiva.Ninguna)
            {
                return;
            }

            if (transformacion == TransformacionActiva.Trasladar)
            {
                TrasladarDuranteArrastre(punto);
            }
            else if (transformacion == TransformacionActiva.Escalar)
            {
                EscalarDuranteArrastre(punto);
            }
            else
            {
                RotarDuranteArrastre(punto);
            }

            Documento.Notificar();
        }

        private void TrasladarDuranteArrastre(PointF punto)
        {
            float desplazamientoX = punto.X - ultimo.X;
            float desplazamientoY = punto.Y - ultimo.Y;

            foreach (var figura in seleccion)
            {
                figura.Trasladar(desplazamientoX, desplazamientoY);
            }
        }

        private void EscalarDuranteArrastre(PointF punto)
        {
            float distanciaAnterior = LineaFigura.Distancia(ultimo, centroTransformacion);
            float distanciaNueva = LineaFigura.Distancia(punto, centroTransformacion);

            if (distanciaAnterior < .5f)
            {
                return;
            }

            float minimo = CalcularEscalaMinima();
            float factor = Math.Max(minimo, Math.Min(20f, distanciaNueva / distanciaAnterior));

            foreach (var figura in seleccion)
            {
                figura.Escalar(factor, factor, centroTransformacion);
            }
        }

        private float CalcularEscalaMinima()
        {
            var marco = MarcoSeleccion();

            if (marco.Count < 4)
            {
                return 1f;
            }

            float ancho = LineaFigura.Distancia(marco[0], marco[1]);
            float alto = LineaFigura.Distancia(marco[1], marco[2]);

            return Math.Max(
                4f / Math.Max(ancho, .1f),
                4f / Math.Max(alto, .1f));
        }

        private void RotarDuranteArrastre(PointF punto)
        {
            double anguloAnterior = Math.Atan2(
                ultimo.Y - centroTransformacion.Y,
                ultimo.X - centroTransformacion.X);

            double anguloNuevo = Math.Atan2(
                punto.Y - centroTransformacion.Y,
                punto.X - centroTransformacion.X);

            float grados = (float)((anguloNuevo - anguloAnterior) * 180 / Math.PI);

            foreach (var figura in seleccion)
            {
                figura.Rotar(grados, centroTransformacion);
            }
        }

        private void FinalizarTransformacion()
        {
            if (seleccion.Count > 0 && matricesAntes != null)
            {
                var despues = seleccion
                    .Select(figura => figura.Transformacion.AArreglo())
                    .ToList();

                if (!Iguales(matricesAntes, despues))
                {
                    Historial.RegistrarEjecutado(
                        new ComandoTransformar(Documento, seleccion, matricesAntes, despues));
                }
            }

            transformacion = TransformacionActiva.Ninguna;
            matricesAntes = null;
            Cambio();
        }

        private void SeleccionarConLazo()
        {
            seleccion.Clear();

            if (lazo.Count < 3)
            {
                NotificarSeleccion();
                return;
            }

            foreach (var figura in Documento.Figuras.Where(figura => !(figura is RellenoRasterFigura)))
            {
                if (FiguraCoincideConLazo(figura))
                {
                    seleccion.Add(figura);
                }
            }

            NotificarSeleccion();
        }

        private bool FiguraCoincideConLazo(Figura2D figura)
        {
            var marco = figura.ObtenerMarcoSeleccion();

            if (marco.Count > 0 && MarcoCoincideConLazo(marco))
            {
                return true;
            }

            if (figura.PuntosTransformados.Any(PuntoEnLazo))
            {
                return true;
            }

            return lazo.Any(punto => figura.Contiene(punto, 2));
        }

        private bool MarcoCoincideConLazo(IReadOnlyList<PointF> marco)
        {
            PointF centro = new PointF(
                marco.Average(punto => punto.X),
                marco.Average(punto => punto.Y));

            if (PuntoEnLazo(centro) || marco.Any(PuntoEnLazo))
            {
                return true;
            }

            for (int i = 0; i < marco.Count; i++)
            {
                for (int j = 0; j < lazo.Count; j++)
                {
                    bool intersectan = SegmentosIntersectan(
                        marco[i],
                        marco[(i + 1) % marco.Count],
                        lazo[j],
                        lazo[(j + 1) % lazo.Count]);

                    if (intersectan)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private static bool SegmentosIntersectan(PointF a, PointF b, PointF c, PointF d)
        {
            float orientacion1 = Orientacion(a, b, c);
            float orientacion2 = Orientacion(a, b, d);
            float orientacion3 = Orientacion(c, d, a);
            float orientacion4 = Orientacion(c, d, b);

            bool cruzaPrimero =
                (orientacion1 > 0 && orientacion2 < 0) ||
                (orientacion1 < 0 && orientacion2 > 0);

            bool cruzaSegundo =
                (orientacion3 > 0 && orientacion4 < 0) ||
                (orientacion3 < 0 && orientacion4 > 0);

            return cruzaPrimero && cruzaSegundo;
        }

        private static float Orientacion(PointF a, PointF b, PointF c)
        {
            return (b.X - a.X) * (c.Y - a.Y) - (b.Y - a.Y) * (c.X - a.X);
        }

        private bool PuntoEnLazo(PointF punto)
        {
            bool dentro = false;

            for (int i = 0, j = lazo.Count - 1; i < lazo.Count; j = i++)
            {
                bool cruza =
                    (lazo[i].Y > punto.Y) != (lazo[j].Y > punto.Y) &&
                    punto.X <
                    (lazo[j].X - lazo[i].X) *
                    (punto.Y - lazo[i].Y) /
                    (lazo[j].Y - lazo[i].Y) +
                    lazo[i].X;

                if (cruza)
                {
                    dentro = !dentro;
                }
            }

            return dentro;
        }

        private void SeleccionarSolo(Figura2D figura)
        {
            seleccion.Clear();

            if (figura != null)
            {
                seleccion.Add(figura);
                AgregarRellenosRasterAsociados(figura);
            }

            NotificarSeleccion();
        }

        private void SeleccionarFiguraReciente(Figura2D figura)
        {
            seleccion.Clear();
            seleccion.Add(figura);
            AgregarRellenosRasterAsociados(figura);
            editandoFiguraReciente = true;
            NotificarSeleccion();
        }

        private void AgregarRellenosRasterAsociados(Figura2D figura)
        {
            if (figura == null || figura is RellenoRasterFigura)
            {
                return;
            }

            foreach (var relleno in Documento.Figuras.OfType<RellenoRasterFigura>())
            {
                if (!seleccion.Contains(relleno) && RellenoPerteneceAFigura(relleno, figura))
                {
                    seleccion.Add(relleno);
                }
            }
        }

        private static bool RellenoPerteneceAFigura(RellenoRasterFigura relleno, Figura2D figura)
        {
            RectangleF limites = relleno.ObtenerLimites();

            if (limites.IsEmpty)
            {
                return false;
            }

            PointF centro = new PointF(
                limites.Left + limites.Width / 2f,
                limites.Top + limites.Height / 2f);

            return figura.Contiene(centro, 2);
        }

        private bool EsHerramientaTrazoLibre()
        {
            return
                HerramientaActual == HerramientaTipo.Lapiz ||
                HerramientaActual == HerramientaTipo.Pincel ||
                HerramientaActual == HerramientaTipo.Borrador;
        }

        private static bool DebeEditarAlCrear(Figura2D figura)
        {
            return
                figura is LineaFigura ||
                figura is RectanguloFigura ||
                figura is ElipseFigura ||
                figura is PoligonoFigura ||
                figura is BezierFigura ||
                figura is TextoFigura;
        }

        private void NotificarSeleccion()
        {
            int figurasVisibles = seleccion.Count(figura => !(figura is RellenoRasterFigura));
            SeleccionActualizada?.Invoke(figurasVisibles);
        }

        private void Cambio()
        {
            CambioVisual?.Invoke(this, EventArgs.Empty);
        }

        private static RectangleF Normalizar(PointF a, PointF b)
        {
            return RectangleF.FromLTRB(
                Math.Min(a.X, b.X),
                Math.Min(a.Y, b.Y),
                Math.Max(a.X, b.X),
                Math.Max(a.Y, b.Y));
        }

        private static bool Cerca(PointF a, PointF b, float tolerancia)
        {
            return LineaFigura.Distancia(a, b) <= tolerancia;
        }

        private static bool Iguales(List<double[]> a, List<double[]> b)
        {
            if (a.Count != b.Count)
            {
                return false;
            }

            for (int i = 0; i < a.Count; i++)
            {
                for (int j = 0; j < a[i].Length; j++)
                {
                    if (Math.Abs(a[i][j] - b[i][j]) > .0001)
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
