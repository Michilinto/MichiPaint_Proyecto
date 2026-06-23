using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Paint_Bolaños_Flores_Venegas.Archivos;
using Paint_Bolaños_Flores_Venegas.Nucleo;

namespace Paint_Bolaños_Flores_Venegas.Vistas
{
    public sealed partial class MichiPaint : Form
    {
        private static readonly Color Marron = ColorTranslator.FromHtml("#835E48");
        private static readonly Color Amarillo = ColorTranslator.FromHtml("#F1D884");
        private static readonly Color Rosa = ColorTranslator.FromHtml("#E4A9A8");
        private static readonly Color Crema = ColorTranslator.FromHtml("#F2E2BA");
        private static readonly Color Lienzo = ColorTranslator.FromHtml("#FFFDF5");

        private readonly GestorProyecto gestorProyecto = new GestorProyecto();
        private readonly ExportadorImagen exportador = new ExportadorImagen();
        private readonly Dictionary<HerramientaTipo, Button> botones = new Dictionary<HerramientaTipo, Button>();

        private DocumentoDibujo documento;
        private HistorialComandos historial;
        private RasterizadorDocumento rasterizador;
        private GestorHerramientas gestor;
        private string rutaProyecto;
        private Font fuenteTexto = new Font("Microsoft Sans Serif", 12);
        private int grosorValor = 2;
        private bool arrastrandoGrosor;

        public MichiPaint()
        {
            InitializeComponent();
            ConfigurarInterfaz();
            NuevoDocumento(1200, 800);
        }

        private void ConfigurarInterfaz()
        {
            RegistrarBotones();
            ConfigurarBotonesHerramienta();
            ConfigurarBotonLimpiar();
            ConfigurarBotonFuente();
            ConfigurarPanelOpciones();
            ConfigurarBotonesTransformacion();
            CrearMuestrasColor();
            ConfigurarMenusEIconos();
            ConfigurarColorPersonalizado();
            ConfigurarEventos();
        }

        private void RegistrarBotones()
        {
            botones.Add(HerramientaTipo.Borrador, btnBorrador);
            botones.Add(HerramientaTipo.Linea, btnLinea);
            botones.Add(HerramientaTipo.Curva, btnCurva);
            botones.Add(HerramientaTipo.Pincel, btnPincel);
            botones.Add(HerramientaTipo.Relleno, btnRelleno);
            botones.Add(HerramientaTipo.Lapiz, btnLapiz);
            botones.Add(HerramientaTipo.Texto, btnTexto);
            botones.Add(HerramientaTipo.Poligono, btnPoligono);
            botones.Add(HerramientaTipo.Rectangulo, btnRectangulo);
            botones.Add(HerramientaTipo.SeleccionLibre, btnSeleccionLibre);
            botones.Add(HerramientaTipo.Seleccion, btnSeleccion);
            botones.Add(HerramientaTipo.RectanguloRedondeado, btnRectanguloRedondeado);
            botones.Add(HerramientaTipo.Elipse, btnElipse);
        }

        private void ConfigurarBotonesHerramienta()
        {
            ConfigurarBoton(btnBorrador, HerramientaTipo.Borrador, "1.png", "Borrador: grosor ajustable");
            ConfigurarBoton(btnLinea, HerramientaTipo.Linea, "2.png", "Línea");
            ConfigurarBoton(btnCurva, HerramientaTipo.Curva, "3.png", "Curva Bézier");
            ConfigurarBoton(btnPincel, HerramientaTipo.Pincel, "4.png", "Pincel: punta circular con grosor ajustable");
            ConfigurarBoton(btnRelleno, HerramientaTipo.Relleno, "5.png", "Balde de relleno");
            ConfigurarBoton(btnLapiz, HerramientaTipo.Lapiz, "6.png", "Lápiz: punta cuadrada y borde pixelado");
            ConfigurarBoton(btnTexto, HerramientaTipo.Texto, "7.png", "Texto");
            ConfigurarBoton(btnPoligono, HerramientaTipo.Poligono, "8.png", "Polígono y figuras");
            ConfigurarBoton(btnRectangulo, HerramientaTipo.Rectangulo, "9.png", "Rectángulo");
            ConfigurarBoton(btnSeleccionLibre, HerramientaTipo.SeleccionLibre, "10.png", "Selección libre");
            ConfigurarBoton(btnSeleccion, HerramientaTipo.Seleccion, "11.png", "Seleccionar");
            ConfigurarBoton(btnRectanguloRedondeado, HerramientaTipo.RectanguloRedondeado, "12.png", "Rectángulo redondeado");
            ConfigurarBoton(btnElipse, HerramientaTipo.Elipse, "15.png", "Círculo o elipse");
        }

        private void ConfigurarBotonLimpiar()
        {
            btnLimpiar.BackColor = Crema;
            btnLimpiar.ForeColor = Marron;
            btnLimpiar.FlatStyle = FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderColor = Marron;
            btnLimpiar.FlatAppearance.MouseOverBackColor = Amarillo;
            btnLimpiar.Image = CargarIcono("14.png", 38);
            btnLimpiar.ImageAlign = ContentAlignment.MiddleCenter;
            btnLimpiar.Click += (sender, e) => Limpiar();

            ayuda.SetToolTip(btnLimpiar, "Limpiar lienzo");
        }

        private void ConfigurarBotonFuente()
        {
            btnFuente.BackColor = Amarillo;
            btnFuente.ForeColor = Marron;
            btnFuente.FlatAppearance.BorderColor = Marron;
            btnFuente.FlatAppearance.BorderSize = 2;
            btnFuente.Click += (sender, e) => ElegirFuente();
        }

        private void ConfigurarPanelOpciones()
        {
            foreach (Control control in panelOpciones.Controls)
            {
                control.ForeColor = Marron;
            }

            lblGrosor.Font = new Font("Consolas", 8f, FontStyle.Bold);
            ConfigurarGrosorPersonalizado();

            usarRelleno.FlatStyle = FlatStyle.Flat;
            usarRelleno.FlatAppearance.BorderColor = Marron;
            usarRelleno.Font = new Font("Consolas", 8f);
            usarRelleno.AutoSize = false;
            usarRelleno.Width = 94;
            usarRelleno.Padding = new Padding(1, 1, 0, 1);
            usarRelleno.Text = "Relleno";

            ayuda.SetToolTip(usarRelleno, "Activa o desactiva el color de relleno en figuras cerradas.");

            lblFigura.Font = new Font("Consolas", 8f, FontStyle.Bold);
            selectorFigura.Font = new Font("Consolas", 8f);
            selectorFigura.ForeColor = Marron;

            grosorFigura.Font = new Font("Consolas", 8f);
            grosorFigura.ForeColor = Marron;

            lblAlgoritmoLinea.Font = new Font("Consolas", 8f, FontStyle.Bold);
            lblAlgoritmoCirculo.Font = new Font("Consolas", 8f, FontStyle.Bold);

            algoritmoLinea.Font = new Font("Consolas", 8f);
            algoritmoLinea.ForeColor = Marron;
            algoritmoLinea.BackColor = Lienzo;

            algoritmoCirculo.Font = new Font("Consolas", 8f);
            algoritmoCirculo.ForeColor = Marron;
            algoritmoCirculo.BackColor = Lienzo;
        }

        private void ConfigurarBotonesTransformacion()
        {
            ConfigurarBotonTransformacion(btnAplicarTraslacion, AplicarTraslacion);
            ConfigurarBotonTransformacion(btnAplicarRotacion, AplicarRotacion);
            ConfigurarBotonTransformacion(btnAplicarEscala, AplicarEscala);
        }

        private void ConfigurarBotonTransformacion(Button boton, Action accion)
        {
            boton.FlatAppearance.BorderColor = Marron;
            boton.FlatAppearance.BorderSize = 2;
            boton.FlatAppearance.MouseOverBackColor = Amarillo;
            boton.Click += (sender, e) => accion();
        }

        private void ConfigurarEventos()
        {
            grosorFigura.SelectedIndexChanged += (sender, e) => ActualizarEstilo();
            grosorFigura.SelectedIndex = 1;

            usarRelleno.CheckedChanged += (sender, e) => ActualizarEstilo();

            selectorFigura.SelectedIndexChanged += (sender, e) => SeleccionarFiguraVisible();
            selectorFigura.SelectedIndex = 0;

            algoritmoLinea.SelectedIndexChanged += (sender, e) => ActualizarAlgoritmos();
            algoritmoLinea.SelectedIndex = 0;

            algoritmoCirculo.SelectedIndexChanged += (sender, e) => ActualizarAlgoritmos();
            algoritmoCirculo.SelectedIndex = 0;

            zoom.SelectedIndexChanged += (sender, e) =>
            {
                int[] valores = { 25, 50, 100, 200, 400 };
                EstablecerZoom(valores[zoom.SelectedIndex]);
            };

            lienzoControl.CoordenadaCambio += punto =>
            {
                estadoCoordenadas.Text = $"X: {(int)punto.X}  Y: {(int)punto.Y}";
            };

            FormClosing += AlCerrar;
            KeyDown += TeclaPresionada;
        }

        private void ConfigurarMenusEIconos()
        {
            itemNuevo.Image = CargarIcono("nuevo_archivo_pixel.png", 20);
            itemAbrir.Image = CargarIcono("16.png", 20);
            itemGuardar.Image = CargarIcono("guardar_pixel.png", 20);
            itemGuardarComo.Image = CargarIcono("guardar_como_pixel.png", 20);
            itemExportar.Image = CargarIcono("guardar_exportar_pixel.png", 20);
            itemLimpiar.Image = CargarIcono("14.png", 20);

            itemNuevo.Click += (sender, e) => Nuevo();
            itemAbrir.Click += (sender, e) => Abrir();
            itemGuardar.Click += (sender, e) => Guardar();
            itemGuardarComo.Click += (sender, e) => GuardarComo();
            itemExportar.Click += (sender, e) => Exportar();
            itemSalir.Click += (sender, e) => Close();
            itemDeshacer.Click += (sender, e) => historial?.Deshacer();
            itemRehacer.Click += (sender, e) => historial?.Rehacer();
            itemEliminar.Click += (sender, e) => gestor?.EliminarSeleccion();
            itemLimpiar.Click += (sender, e) => Limpiar();
            itemZoom25.Click += (sender, e) => EstablecerZoom(25);
            itemZoom50.Click += (sender, e) => EstablecerZoom(50);
            itemZoom100.Click += (sender, e) => EstablecerZoom(100);
            itemZoom200.Click += (sender, e) => EstablecerZoom(200);
            itemZoom400.Click += (sender, e) => EstablecerZoom(400);
            itemControles.Click += (sender, e) => MostrarAyuda();
            itemAcerca.Click += (sender, e) => MostrarAcerca();

            ConfigurarRapido(rapidoGuardar, "guardar_pixel.png", "Guardar", (sender, e) => Guardar());
            ConfigurarRapido(rapidoExportar, "guardar_exportar_pixel.png", "Exportar PNG", (sender, e) => Exportar());
            ConfigurarRapido(rapidoDeshacer, "deshacer_pixel.png", "↶", (sender, e) => historial?.Deshacer());
            ConfigurarRapido(rapidoRehacer, "rehacer_pixel.png", "↷", (sender, e) => historial?.Rehacer());
        }

        private void ConfigurarRapido(
            ToolStripMenuItem item,
            string archivo,
            string alternativa,
            EventHandler accion)
        {
            var imagen = CargarIcono(archivo, 20);

            if (imagen != null)
            {
                item.Image = imagen;
                item.DisplayStyle = ToolStripItemDisplayStyle.Image;
            }
            else
            {
                item.Text = alternativa;
                item.Font = new Font(Font.FontFamily, 14, FontStyle.Bold);
                item.DisplayStyle = ToolStripItemDisplayStyle.Text;
            }

            item.Click += accion;
        }

        private void ConfigurarBoton(
            Button boton,
            HerramientaTipo tipo,
            string archivo,
            string texto)
        {
            boton.BackColor = Crema;
            boton.ForeColor = Marron;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderColor = Marron;
            boton.FlatAppearance.MouseOverBackColor = Amarillo;
            boton.Image = CargarIcono(archivo, 38);
            boton.ImageAlign = ContentAlignment.MiddleCenter;
            boton.Padding = Padding.Empty;
            boton.Click += (sender, e) => SeleccionarHerramienta(tipo, boton);

            ayuda.SetToolTip(boton, texto);
        }

        private void CrearMuestrasColor()
        {
            Color[] colores =
            {
                Marron,
                Amarillo,
                Rosa,
                Crema,
                Lienzo,
                Color.Black,
                Color.Gray,
                Color.White,
                Color.Red,
                Color.Orange,
                Color.Yellow,
                Color.Green,
                Color.Cyan,
                Color.Blue,
                Color.Violet,
                Color.Magenta
            };

            flujoPaleta.AutoScroll = true;

            foreach (var color in colores)
            {
                AgregarMuestraColor(color);
            }

            ayuda.SetToolTip(muestraLinea, "Color de línea actual");
            ayuda.SetToolTip(muestraRelleno, "Color de relleno actual");

            muestraLinea.Cursor = Cursors.Hand;
            muestraRelleno.Cursor = Cursors.Hand;
            muestraLinea.Click += (sender, e) => ElegirColorPersonalizado(MouseButtons.Left);
            muestraRelleno.Click += (sender, e) => ElegirColorPersonalizado(MouseButtons.Right);
        }

        private void AgregarMuestraColor(Color color)
        {
            bool yaExiste = flujoPaleta.Controls
                .OfType<Button>()
                .Any(boton => boton.Tag is int && (int)boton.Tag == color.ToArgb());

            if (yaExiste)
            {
                return;
            }

            var botonColor = new Button
            {
                BackColor = color,
                Size = new Size(25, 25),
                Margin = new Padding(1),
                FlatStyle = FlatStyle.Flat,
                Tag = color.ToArgb()
            };

            botonColor.FlatAppearance.BorderColor = Marron;
            botonColor.MouseDown += (sender, e) =>
            {
                AsignarColor(((Button)sender).BackColor, e.Button);
            };

            flujoPaleta.Controls.Add(botonColor);
        }

        private void ConfigurarColorPersonalizado()
        {
            btnColorPersonalizado.BackColor = Amarillo;
            btnColorPersonalizado.ForeColor = Marron;
            btnColorPersonalizado.FlatAppearance.BorderColor = Marron;
            btnColorPersonalizado.MouseDown += (sender, e) => ElegirColorPersonalizado(e.Button);

            ayuda.SetToolTip(
                btnColorPersonalizado,
                "Clic izquierdo: color de línea. Clic derecho: color de relleno.");
        }

        private void ConfigurarGrosorPersonalizado()
        {
            panelGrosor.Cursor = Cursors.Hand;
            barraGrosor.Cursor = Cursors.Hand;
            rellenoGrosor.Cursor = Cursors.Hand;
            perillaGrosor.Cursor = Cursors.Hand;

            panelGrosor.BackColor = Lienzo;
            barraGrosor.BackColor = Crema;
            rellenoGrosor.BackColor = Rosa;
            perillaGrosor.BackColor = Amarillo;

            ayuda.SetToolTip(panelGrosor, "Arrastra o haz clic para cambiar el grosor");
            ayuda.SetToolTip(perillaGrosor, "Grosor del lápiz, pincel o borrador");

            MouseEventHandler iniciar = (sender, e) =>
            {
                arrastrandoGrosor = true;
                ActualizarGrosorDesdeControl(sender, e);
            };

            MouseEventHandler mover = (sender, e) =>
            {
                if (arrastrandoGrosor)
                {
                    ActualizarGrosorDesdeControl(sender, e);
                }
            };

            MouseEventHandler terminar = (sender, e) =>
            {
                arrastrandoGrosor = false;
            };

            foreach (Control control in new Control[] { panelGrosor, barraGrosor, rellenoGrosor, perillaGrosor })
            {
                control.MouseDown += iniciar;
                control.MouseMove += mover;
                control.MouseUp += terminar;
                control.MouseLeave += (sender, e) =>
                {
                    if (Control.MouseButtons != MouseButtons.Left)
                    {
                        arrastrandoGrosor = false;
                    }
                };
            }

            ActualizarVistaGrosor();
        }

        private void ActualizarGrosorDesdeControl(object origen, MouseEventArgs e)
        {
            int posicionX;

            if (origen == barraGrosor || origen == rellenoGrosor)
            {
                posicionX = barraGrosor.Left + e.X;
            }
            else if (origen == perillaGrosor)
            {
                posicionX = perillaGrosor.Left + e.X;
            }
            else
            {
                posicionX = e.X;
            }

            int inicio = barraGrosor.Left;
            int ancho = Math.Max(1, barraGrosor.Width - 2);
            float proporcion = Math.Max(0, Math.Min(1, (posicionX - inicio) / (float)ancho));

            grosorValor = Math.Max(1, Math.Min(100, 1 + (int)Math.Round(proporcion * 99)));

            ActualizarVistaGrosor();
            ActualizarEstilo();
        }

        private void ActualizarVistaGrosor()
        {
            lblValorGrosor.Text = $"{grosorValor} px";
            lblGrosor.Text = "Grosor";

            int ancho = Math.Max(1, barraGrosor.Width - 2);
            float proporcion = (grosorValor - 1) / 99f;
            int relleno = Math.Max(1, (int)Math.Round(ancho * proporcion));

            rellenoGrosor.Width = relleno;

            int centro = barraGrosor.Left + (int)Math.Round(ancho * proporcion);
            perillaGrosor.Left = Math.Max(
                0,
                Math.Min(panelGrosor.Width - perillaGrosor.Width, centro - perillaGrosor.Width / 2));
        }

        private void SeleccionarFiguraVisible()
        {
            if (gestor == null || selectorFigura.SelectedIndex < 0)
            {
                return;
            }

            gestor.FormaActual = (FormaPersonalizada)selectorFigura.SelectedIndex;
            SeleccionarHerramienta(HerramientaTipo.Poligono, btnPoligono);
            estadoHerramienta.Text = selectorFigura.SelectedItem.ToString();
        }

        private void ElegirColorPersonalizado(MouseButtons destino)
        {
            Color inicial = destino == MouseButtons.Right
                ? gestor.EstiloActual.ColorRelleno
                : gestor.EstiloActual.ColorLinea;

            using (var dialogo = new ColorDialog { Color = inicial, FullOpen = true, AnyColor = true })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                AgregarMuestraColor(dialogo.Color);

                MouseButtons botonDestino = destino == MouseButtons.Right
                    ? MouseButtons.Right
                    : MouseButtons.Left;

                AsignarColor(dialogo.Color, botonDestino);
            }
        }

        private void AplicarTraslacion()
        {
            if (gestor.Seleccion.Count == 0)
            {
                MostrarSeleccionRequerida();
                return;
            }

            gestor.TrasladarSeleccion((float)traslacionX.Value, (float)traslacionY.Value);
        }

        private void AplicarRotacion()
        {
            if (gestor.Seleccion.Count == 0)
            {
                MostrarSeleccionRequerida();
                return;
            }

            gestor.RotarSeleccion((float)rotacionGrados.Value);
        }

        private void AplicarEscala()
        {
            if (gestor.Seleccion.Count == 0)
            {
                MostrarSeleccionRequerida();
                return;
            }

            gestor.EscalarSeleccion((float)escalaX.Value / 100f, (float)escalaY.Value / 100f);
        }

        private void MostrarSeleccionRequerida()
        {
            MessageBox.Show(
                this,
                "Primero selecciona una figura o un grupo en el lienzo.",
                "Transformaciones",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void NuevoDocumento(int ancho, int alto)
        {
            documento = new DocumentoDibujo(ancho, alto)
            {
                ColorFondo = Lienzo
            };

            historial = new HistorialComandos();
            rasterizador = new RasterizadorDocumento();
            gestor = new GestorHerramientas(documento, historial, rasterizador)
            {
                FuenteTexto = fuenteTexto
            };

            gestor.TextoSolicitado += SolicitarTexto;
            gestor.SeleccionActualizada += ActualizarEstadoSeleccion;

            documento.Cambio += (sender, e) => ActualizarTitulo();
            historial.Cambio += (sender, e) => ActualizarHistorial();

            lienzoControl.Configurar(documento, gestor);

            rutaProyecto = null;

            ActualizarAlgoritmos();
            ActualizarEstilo();
            SeleccionarHerramienta(HerramientaTipo.Lapiz, btnLapiz);
            historial.Reiniciar();

            documento.Modificado = false;
            ActualizarTitulo();
            estadoTamano.Text = $"{ancho} × {alto}";
        }

        private void ActualizarEstadoSeleccion(int cantidad)
        {
            if (cantidad == 0)
            {
                estadoHerramienta.Text = "Sin selección";
            }
            else if (cantidad == 1)
            {
                estadoHerramienta.Text = "1 figura seleccionada";
            }
            else
            {
                estadoHerramienta.Text = $"{cantidad} figuras seleccionadas";
            }
        }

        private void SeleccionarHerramienta(HerramientaTipo tipo, Button boton)
        {
            if (gestor == null)
            {
                return;
            }

            gestor.Cancelar();
            gestor.HerramientaActual = tipo;

            foreach (var botonHerramienta in botones.Values)
            {
                botonHerramienta.BackColor = Crema;
                botonHerramienta.FlatAppearance.BorderSize = 2;
            }

            boton.BackColor = Rosa;
            boton.FlatAppearance.BorderSize = 3;
            estadoHerramienta.Text = ayuda.GetToolTip(boton);

            ActualizarControlGrosor(tipo);

            if (tipo == HerramientaTipo.Zoom)
            {
                zoom.SelectedIndex = zoom.SelectedIndex == zoom.Items.Count - 1
                    ? 2
                    : zoom.SelectedIndex + 1;
            }
        }

        private static bool EsHerramientaTrazo(HerramientaTipo tipo)
        {
            return
                tipo == HerramientaTipo.Lapiz ||
                tipo == HerramientaTipo.Pincel ||
                tipo == HerramientaTipo.Borrador;
        }

        private static bool EsHerramientaFigura(HerramientaTipo tipo)
        {
            return
                tipo == HerramientaTipo.Linea ||
                tipo == HerramientaTipo.Curva ||
                tipo == HerramientaTipo.Rectangulo ||
                tipo == HerramientaTipo.RectanguloRedondeado ||
                tipo == HerramientaTipo.Elipse ||
                tipo == HerramientaTipo.Poligono;
        }

        private void ActualizarControlGrosor(HerramientaTipo tipo)
        {
            bool trazo = EsHerramientaTrazo(tipo);
            bool figura = EsHerramientaFigura(tipo);
            bool texto = tipo == HerramientaTipo.Texto;
            bool poligono = tipo == HerramientaTipo.Poligono;
            bool linea = tipo == HerramientaTipo.Linea;
            bool elipse = tipo == HerramientaTipo.Elipse;

            lblGrosor.Visible = trazo || figura;
            panelGrosor.Visible = trazo;
            panelGrosor.Enabled = trazo;

            grosorFigura.Visible = figura;
            grosorFigura.Enabled = figura;

            usarRelleno.Visible = figura;
            usarRelleno.Enabled = figura;

            lblFigura.Visible = poligono;
            selectorFigura.Visible = poligono;
            btnFuente.Visible = texto;

            lblAlgoritmoLinea.Visible = linea;
            algoritmoLinea.Visible = linea;
            algoritmoLinea.Enabled = linea;

            lblAlgoritmoCirculo.Visible = elipse;
            algoritmoCirculo.Visible = elipse;
            algoritmoCirculo.Enabled = elipse;

            lblGrosor.Text = trazo
                ? "Grosor"
                : figura
                    ? "Grosor figura"
                    : string.Empty;

            ActualizarVistaGrosor();
            ActualizarEstilo();
        }

        private void ActualizarAlgoritmos()
        {
            if (gestor == null)
            {
                return;
            }

            if (algoritmoLinea.SelectedIndex >= 0)
            {
                gestor.AlgoritmoLinea = (AlgoritmoLineaTipo)algoritmoLinea.SelectedIndex;
            }

            if (algoritmoCirculo.SelectedIndex >= 0)
            {
                gestor.AlgoritmoCirculo = (AlgoritmoCirculoTipo)algoritmoCirculo.SelectedIndex;
            }
        }

        private void MostrarGaleria(Control control)
        {
            var menuContextual = new ContextMenuStrip
            {
                BackColor = Crema,
                ForeColor = Marron
            };

            foreach (FormaPersonalizada forma in Enum.GetValues(typeof(FormaPersonalizada)))
            {
                var item = new ToolStripMenuItem(ObtenerNombreFigura(forma));

                item.Click += (sender, e) =>
                {
                    gestor.FormaActual = forma;
                    SeleccionarHerramienta(HerramientaTipo.Poligono, btnPoligono);
                    estadoHerramienta.Text = item.Text;
                };

                menuContextual.Items.Add(item);
            }

            menuContextual.Show(control, new Point(control.Width, 0));
        }

        private static string ObtenerNombreFigura(FormaPersonalizada forma)
        {
            return forma == FormaPersonalizada.Poligono
                ? "Polígono libre"
                : forma.ToString();
        }

        private void ActualizarEstilo()
        {
            if (gestor == null)
            {
                return;
            }

            if (EsHerramientaTrazo(gestor.HerramientaActual))
            {
                gestor.EstiloActual.Grosor = grosorValor;
            }
            else if (EsHerramientaFigura(gestor.HerramientaActual) && grosorFigura.SelectedIndex >= 0)
            {
                int[] grosoresFigura = { 1, 3, 5, 8 };
                gestor.EstiloActual.Grosor = grosoresFigura[grosorFigura.SelectedIndex];
            }

            gestor.EstiloActual.TieneRelleno = usarRelleno.Checked;
            gestor.AplicarEstiloASeleccion();
        }

        private void AsignarColor(Color color, MouseButtons boton)
        {
            if (gestor == null)
            {
                return;
            }

            bool aplicadoPorActivacion = false;

            if (boton == MouseButtons.Right)
            {
                gestor.EstiloActual.ColorRelleno = color;
                muestraRelleno.BackColor = color;

                if (!usarRelleno.Checked)
                {
                    usarRelleno.Checked = true;
                    aplicadoPorActivacion = true;
                }

                estadoHerramienta.Text = $"Relleno: #{color.R:X2}{color.G:X2}{color.B:X2}";
            }
            else
            {
                gestor.EstiloActual.ColorLinea = color;
                muestraLinea.BackColor = color;
                estadoHerramienta.Text = $"Línea: #{color.R:X2}{color.G:X2}{color.B:X2}";
            }

            if (!aplicadoPorActivacion)
            {
                gestor.AplicarEstiloASeleccion();
            }
        }

        private void ElegirFuente()
        {
            using (var dialogo = new FontDialog { Font = fuenteTexto })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                fuenteTexto?.Dispose();
                fuenteTexto = (Font)dialogo.Font.Clone();
                gestor.FuenteTexto = fuenteTexto;
            }
        }

        private void SolicitarTexto(PointF punto)
        {
            var existente = gestor.Seleccion
                .OfType<TextoFigura>()
                .FirstOrDefault(texto => texto.Contiene(punto));

            string textoIngresado = DialogoTexto.Mostrar(this, existente?.Texto ?? string.Empty);

            if (textoIngresado != null)
            {
                gestor.AgregarTexto(punto, textoIngresado);
            }
        }

        private void Nuevo()
        {
            if (!ConfirmarCambios())
            {
                return;
            }

            using (var dialogo = new DialogoLienzo())
            {
                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    NuevoDocumento(dialogo.Ancho, dialogo.Alto);
                }
            }
        }

        private void Abrir()
        {
            if (!ConfirmarCambios())
            {
                return;
            }

            using (var dialogo = new OpenFileDialog
            {
                Filter = "Proyecto MichiPaint (*.mpaint)|*.mpaint",
                Title = "Abrir proyecto"
            })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    var nuevo = gestorProyecto.Abrir(dialogo.FileName);

                    NuevoDocumento(nuevo.Ancho, nuevo.Alto);
                    documento.ColorFondo = nuevo.ColorFondo;
                    documento.Reemplazar(nuevo.Figuras);
                    documento.Modificado = false;
                    rutaProyecto = dialogo.FileName;

                    ActualizarTitulo();
                    lienzoControl.ActualizarImagen();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        this,
                        ex.Message,
                        "No se pudo abrir",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private bool Guardar()
        {
            if (string.IsNullOrEmpty(rutaProyecto))
            {
                return GuardarComo();
            }

            try
            {
                gestorProyecto.Guardar(rutaProyecto, documento);
                ActualizarTitulo();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    this,
                    ex.Message,
                    "No se pudo guardar",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                return false;
            }
        }

        private bool GuardarComo()
        {
            using (var dialogo = new SaveFileDialog
            {
                Filter = "Proyecto MichiPaint (*.mpaint)|*.mpaint",
                DefaultExt = "mpaint",
                AddExtension = true,
                Title = "Guardar proyecto como"
            })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return false;
                }

                rutaProyecto = dialogo.FileName;
                return Guardar();
            }
        }

        private void Exportar()
        {
            using (var dialogo = new SaveFileDialog
            {
                Filter = "Imagen PNG (*.png)|*.png",
                DefaultExt = "png",
                AddExtension = true,
                Title = "Exportar imagen"
            })
            {
                if (dialogo.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    exportador.ExportarPng(dialogo.FileName, documento, rasterizador);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        this,
                        ex.Message,
                        "No se pudo exportar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void Limpiar()
        {
            if (documento.Figuras.Count == 0)
            {
                return;
            }

            var respuesta = MessageBox.Show(
                this,
                "¿Limpiar todo el lienzo? Esta acción se puede deshacer.",
                "Limpiar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta != DialogResult.Yes)
            {
                return;
            }

            gestor.LimpiarSeleccion();
            historial.Ejecutar(new ComandoLimpiar(documento));
        }

        private bool ConfirmarCambios()
        {
            if (documento == null || !documento.Modificado)
            {
                return true;
            }

            var respuesta = MessageBox.Show(
                this,
                "Hay cambios sin guardar. ¿Deseas guardarlos?",
                "MichiPaint",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Warning);

            if (respuesta == DialogResult.Cancel)
            {
                return false;
            }

            return respuesta != DialogResult.Yes || Guardar();
        }

        private void AlCerrar(object sender, FormClosingEventArgs e)
        {
            if (!ConfirmarCambios())
            {
                e.Cancel = true;
            }
        }

        private void TeclaPresionada(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                gestor?.Cancelar();
            }
            else if (e.KeyCode == Keys.Delete)
            {
                gestor?.EliminarSeleccion();
            }
            else if (e.KeyCode == Keys.Enter && gestor?.HerramientaActual == HerramientaTipo.Poligono)
            {
                gestor.DobleClic(Point.Empty);
            }
        }

        private void EstablecerZoom(int porcentaje)
        {
            int[] valores = { 25, 50, 100, 200, 400 };
            int indice = Array.IndexOf(valores, porcentaje);

            if (indice >= 0 && zoom.SelectedIndex != indice)
            {
                zoom.SelectedIndex = indice;
                return;
            }

            lienzoControl?.EstablecerZoom(porcentaje / 100f);
            estadoHerramienta.Text = $"Zoom {porcentaje} %";
        }

        private void ActualizarTitulo()
        {
            string marcaCambios = documento != null && documento.Modificado ? "*" : string.Empty;
            string nombre = string.IsNullOrEmpty(rutaProyecto)
                ? "Sin título"
                : Path.GetFileName(rutaProyecto);

            Text = $"{marcaCambios}{nombre} - MichiPaint";
        }

        private void ActualizarHistorial()
        {
            bool puedeDeshacer = historial?.PuedeDeshacer == true;
            bool puedeRehacer = historial?.PuedeRehacer == true;

            itemDeshacer.Enabled = puedeDeshacer;
            rapidoDeshacer.Enabled = puedeDeshacer;
            itemRehacer.Enabled = puedeRehacer;
            rapidoRehacer.Enabled = puedeRehacer;
        }

        private void MostrarAyuda()
        {
            MessageBox.Show(
                this,
                "Dibuja arrastrando sobre el lienzo.\n\n" +
                "Polígono: primer clic para iniciar, mueve el cursor para previsualizar cada línea, " +
                "clic para fijar cada vértice y doble clic o Enter para cerrar.\n" +
                "Bézier: el primer clic fija el inicio; el segundo forma el grado 1, " +
                "el tercero el grado 2 y el cuarto crea y confirma el grado 3. " +
                "Mueve el cursor para previsualizar cada grado.\n" +
                "Selección: arrastra para mover; usa los tiradores para escalar y rotar.\n" +
                "Clic izquierdo en un color: contorno. Clic derecho: relleno.",
                "Controles de MichiPaint",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void MostrarAcerca()
        {
            MessageBox.Show(
                this,
                "MichiPaint\n" +
                "Proyecto de Computación Gráfica\n\n" +
                "Rasterización, transformaciones y rellenos implementados con algoritmos propios.",
                "Acerca de",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private Image CargarIcono(string nombre, int tamano)
        {
            if (string.IsNullOrEmpty(nombre))
            {
                return null;
            }

            string ruta = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Recursos", nombre);

            if (!File.Exists(ruta))
            {
                return null;
            }

            using (var original = Image.FromFile(ruta))
            {
                var bitmap = new Bitmap(tamano, tamano);

                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
                    graphics.PixelOffsetMode = PixelOffsetMode.Half;
                    graphics.DrawImage(original, new Rectangle(0, 0, tamano, tamano));
                }

                return bitmap;
            }
        }
    }

    internal sealed class DialogoLienzo : Form
    {
        private readonly NumericUpDown ancho;
        private readonly NumericUpDown alto;

        public int Ancho => (int)ancho.Value;
        public int Alto => (int)alto.Value;

        public DialogoLienzo()
        {
            Text = "Nuevo lienzo";
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(250, 145);
            MaximizeBox = false;
            MinimizeBox = false;

            Controls.Add(new Label
            {
                Text = "Ancho:",
                Location = new Point(18, 20),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Text = "Alto:",
                Location = new Point(18, 55),
                AutoSize = true
            });

            ancho = new NumericUpDown
            {
                Minimum = 100,
                Maximum = 4000,
                Value = 1200,
                Location = new Point(90, 17),
                Width = 125
            };

            alto = new NumericUpDown
            {
                Minimum = 100,
                Maximum = 4000,
                Value = 800,
                Location = new Point(90, 52),
                Width = 125
            };

            var aceptar = new Button
            {
                Text = "Crear",
                DialogResult = DialogResult.OK,
                Location = new Point(55, 100),
                Width = 70
            };

            var cancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(135, 100),
                Width = 70
            };

            Controls.Add(ancho);
            Controls.Add(alto);
            Controls.Add(aceptar);
            Controls.Add(cancelar);

            AcceptButton = aceptar;
            CancelButton = cancelar;
        }
    }

    internal sealed class DialogoTexto : Form
    {
        private readonly TextBox caja;

        private DialogoTexto(string texto)
        {
            Text = "Texto";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ClientSize = new Size(380, 175);
            MaximizeBox = false;
            MinimizeBox = false;

            caja = new TextBox
            {
                Multiline = true,
                Text = texto ?? string.Empty,
                Location = new Point(12, 12),
                Size = new Size(356, 115),
                ScrollBars = ScrollBars.Vertical
            };

            var aceptar = new Button
            {
                Text = "Aceptar",
                DialogResult = DialogResult.OK,
                Location = new Point(210, 137),
                Width = 75
            };

            var cancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(293, 137),
                Width = 75
            };

            Controls.Add(caja);
            Controls.Add(aceptar);
            Controls.Add(cancelar);

            AcceptButton = aceptar;
            CancelButton = cancelar;
        }

        public static string Mostrar(IWin32Window padre, string texto)
        {
            using (var dialogo = new DialogoTexto(texto))
            {
                return dialogo.ShowDialog(padre) == DialogResult.OK
                    ? dialogo.caja.Text
                    : null;
            }
        }
    }
}
