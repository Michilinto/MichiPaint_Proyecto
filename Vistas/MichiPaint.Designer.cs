using System.Drawing;
using System.Windows.Forms;

namespace Paint_Bolaños_Flores_Venegas.Vistas
{
    partial class MichiPaint
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menu;
        private ToolStripMenuItem menuArchivo;
        private ToolStripMenuItem menuEditar;
        private ToolStripMenuItem menuVer;
        private ToolStripMenuItem menuAyuda;
        private ToolStripMenuItem itemNuevo;
        private ToolStripMenuItem itemAbrir;
        private ToolStripMenuItem itemGuardar;
        private ToolStripMenuItem itemGuardarComo;
        private ToolStripMenuItem itemExportar;
        private ToolStripMenuItem itemSalir;
        private ToolStripMenuItem itemDeshacer;
        private ToolStripMenuItem itemRehacer;
        private ToolStripMenuItem itemEliminar;
        private ToolStripMenuItem itemLimpiar;
        private ToolStripMenuItem itemZoom25;
        private ToolStripMenuItem itemZoom50;
        private ToolStripMenuItem itemZoom100;
        private ToolStripMenuItem itemZoom200;
        private ToolStripMenuItem itemZoom400;
        private ToolStripMenuItem itemControles;
        private ToolStripMenuItem itemAcerca;
        private ToolStripMenuItem rapidoGuardar;
        private ToolStripMenuItem rapidoExportar;
        private ToolStripMenuItem rapidoDeshacer;
        private ToolStripMenuItem rapidoRehacer;
        private ToolStripSeparator separadorArchivo1;
        private ToolStripSeparator separadorArchivo2;
        private ToolStripSeparator separadorEditar;
        private ToolStripSeparator separadorAccesos;
        private StatusStrip barraEstado;
        private ToolStripStatusLabel estadoHerramienta;
        private ToolStripStatusLabel estadoCoordenadas;
        private ToolStripStatusLabel estadoTamano;
        private Panel panelHerramientas;
        private Panel panelLienzo;
        private Panel panelPaleta;
        private Panel panelTransformaciones;
        private Panel muestraLinea;
        private Panel muestraRelleno;
        private TableLayoutPanel grillaHerramientas;
        private FlowLayoutPanel panelOpciones;
        private FlowLayoutPanel flujoPaleta;
        private LienzoControl lienzoControl;
        private Button btnBorrador;
        private Button btnLinea;
        private Button btnCurva;
        private Button btnPincel;
        private Button btnRelleno;
        private Button btnLapiz;
        private Button btnTexto;
        private Button btnPoligono;
        private Button btnRectangulo;
        private Button btnSeleccionLibre;
        private Button btnSeleccion;
        private Button btnRectanguloRedondeado;
        private Button btnZoom;
        private Button btnLimpiar;
        private Button btnElipse;
        private Button btnFuente;
        private Button btnColorPersonalizado;
        private Button btnAplicarTraslacion;
        private Button btnAplicarRotacion;
        private Button btnAplicarEscala;
        private Label lblGrosor;
        private Label lblFigura;
        private Label lblAlgoritmoLinea;
        private Label lblAlgoritmoCirculo;
        private Label lblTituloTransformaciones;
        private Label lblTraslacionX;
        private Label lblTraslacionY;
        private Label lblRotacion;
        private Label lblEscalaX;
        private Label lblEscalaY;
        private Label lblColoresActuales;
        private Panel panelGrosor;
        private Panel barraGrosor;
        private Panel rellenoGrosor;
        private Panel perillaGrosor;
        private Label lblValorGrosor;
        private Label lblMinMaxGrosor;
        private NumericUpDown traslacionX;
        private NumericUpDown traslacionY;
        private NumericUpDown rotacionGrados;
        private NumericUpDown escalaX;
        private NumericUpDown escalaY;
        private CheckBox usarRelleno;
        private ComboBox algoritmoLinea;
        private ComboBox algoritmoCirculo;
        private ComboBox selectorFigura;
        private ComboBox grosorFigura;
        private ComboBox zoom;
        private ToolTip ayuda;
        // Nuevos controles decorativos
        private Panel panelCabecera;
        private Label lblTituloApp;
        private Label lblSubtituloApp;
        private Label lblSeccionHerramientas;
        private Label lblSeccionOpciones;
        private Label lblSeccionTransformaciones;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.menu = new System.Windows.Forms.MenuStrip();
            this.menuArchivo = new System.Windows.Forms.ToolStripMenuItem();
            this.itemNuevo = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAbrir = new System.Windows.Forms.ToolStripMenuItem();
            this.separadorArchivo1 = new System.Windows.Forms.ToolStripSeparator();
            this.itemGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemGuardarComo = new System.Windows.Forms.ToolStripMenuItem();
            this.itemExportar = new System.Windows.Forms.ToolStripMenuItem();
            this.separadorArchivo2 = new System.Windows.Forms.ToolStripSeparator();
            this.itemSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.menuEditar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemDeshacer = new System.Windows.Forms.ToolStripMenuItem();
            this.itemRehacer = new System.Windows.Forms.ToolStripMenuItem();
            this.separadorEditar = new System.Windows.Forms.ToolStripSeparator();
            this.itemEliminar = new System.Windows.Forms.ToolStripMenuItem();
            this.itemLimpiar = new System.Windows.Forms.ToolStripMenuItem();
            this.menuVer = new System.Windows.Forms.ToolStripMenuItem();
            this.itemZoom25 = new System.Windows.Forms.ToolStripMenuItem();
            this.itemZoom50 = new System.Windows.Forms.ToolStripMenuItem();
            this.itemZoom100 = new System.Windows.Forms.ToolStripMenuItem();
            this.itemZoom200 = new System.Windows.Forms.ToolStripMenuItem();
            this.itemZoom400 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuAyuda = new System.Windows.Forms.ToolStripMenuItem();
            this.itemControles = new System.Windows.Forms.ToolStripMenuItem();
            this.itemAcerca = new System.Windows.Forms.ToolStripMenuItem();
            this.separadorAccesos = new System.Windows.Forms.ToolStripSeparator();
            this.rapidoGuardar = new System.Windows.Forms.ToolStripMenuItem();
            this.rapidoExportar = new System.Windows.Forms.ToolStripMenuItem();
            this.rapidoDeshacer = new System.Windows.Forms.ToolStripMenuItem();
            this.rapidoRehacer = new System.Windows.Forms.ToolStripMenuItem();
            this.barraEstado = new System.Windows.Forms.StatusStrip();
            this.estadoHerramienta = new System.Windows.Forms.ToolStripStatusLabel();
            this.estadoCoordenadas = new System.Windows.Forms.ToolStripStatusLabel();
            this.estadoTamano = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelHerramientas = new System.Windows.Forms.Panel();
            this.panelOpciones = new System.Windows.Forms.FlowLayoutPanel();
            this.lblGrosor = new System.Windows.Forms.Label();
            this.panelGrosor = new System.Windows.Forms.Panel();
            this.barraGrosor = new System.Windows.Forms.Panel();
            this.rellenoGrosor = new System.Windows.Forms.Panel();
            this.perillaGrosor = new System.Windows.Forms.Panel();
            this.lblValorGrosor = new System.Windows.Forms.Label();
            this.lblMinMaxGrosor = new System.Windows.Forms.Label();
            this.grosorFigura = new System.Windows.Forms.ComboBox();
            this.usarRelleno = new System.Windows.Forms.CheckBox();
            this.lblFigura = new System.Windows.Forms.Label();
            this.selectorFigura = new System.Windows.Forms.ComboBox();
            this.btnFuente = new System.Windows.Forms.Button();
            this.grillaHerramientas = new System.Windows.Forms.TableLayoutPanel();
            this.btnBorrador = new System.Windows.Forms.Button();
            this.btnLinea = new System.Windows.Forms.Button();
            this.btnCurva = new System.Windows.Forms.Button();
            this.btnPincel = new System.Windows.Forms.Button();
            this.btnRelleno = new System.Windows.Forms.Button();
            this.btnLapiz = new System.Windows.Forms.Button();
            this.btnTexto = new System.Windows.Forms.Button();
            this.btnPoligono = new System.Windows.Forms.Button();
            this.btnRectangulo = new System.Windows.Forms.Button();
            this.btnSeleccionLibre = new System.Windows.Forms.Button();
            this.btnSeleccion = new System.Windows.Forms.Button();
            this.btnRectanguloRedondeado = new System.Windows.Forms.Button();
            this.btnZoom = new System.Windows.Forms.Button();
            this.btnLimpiar = new System.Windows.Forms.Button();
            this.btnElipse = new System.Windows.Forms.Button();
            this.btnColorPersonalizado = new System.Windows.Forms.Button();
            this.panelPaleta = new System.Windows.Forms.Panel();
            this.muestraLinea = new System.Windows.Forms.Panel();
            this.muestraRelleno = new System.Windows.Forms.Panel();
            this.lblColoresActuales = new System.Windows.Forms.Label();
            this.flujoPaleta = new System.Windows.Forms.FlowLayoutPanel();
            this.zoom = new System.Windows.Forms.ComboBox();
            this.panelLienzo = new System.Windows.Forms.Panel();
            this.lienzoControl = new Paint_Bolaños_Flores_Venegas.Vistas.LienzoControl();
            this.panelTransformaciones = new System.Windows.Forms.Panel();
            this.lblTituloTransformaciones = new System.Windows.Forms.Label();
            this.lblAlgoritmoLinea = new System.Windows.Forms.Label();
            this.algoritmoLinea = new System.Windows.Forms.ComboBox();
            this.lblAlgoritmoCirculo = new System.Windows.Forms.Label();
            this.algoritmoCirculo = new System.Windows.Forms.ComboBox();
            this.lblTraslacionX = new System.Windows.Forms.Label();
            this.traslacionX = new System.Windows.Forms.NumericUpDown();
            this.lblTraslacionY = new System.Windows.Forms.Label();
            this.traslacionY = new System.Windows.Forms.NumericUpDown();
            this.btnAplicarTraslacion = new System.Windows.Forms.Button();
            this.lblRotacion = new System.Windows.Forms.Label();
            this.rotacionGrados = new System.Windows.Forms.NumericUpDown();
            this.btnAplicarRotacion = new System.Windows.Forms.Button();
            this.lblEscalaX = new System.Windows.Forms.Label();
            this.escalaX = new System.Windows.Forms.NumericUpDown();
            this.lblEscalaY = new System.Windows.Forms.Label();
            this.escalaY = new System.Windows.Forms.NumericUpDown();
            this.btnAplicarEscala = new System.Windows.Forms.Button();
            this.ayuda = new System.Windows.Forms.ToolTip(this.components);
            this.panelCabecera = new System.Windows.Forms.Panel();
            this.lblTituloApp = new System.Windows.Forms.Label();
            this.lblSubtituloApp = new System.Windows.Forms.Label();
            this.lblSeccionHerramientas = new System.Windows.Forms.Label();
            this.lblSeccionOpciones = new System.Windows.Forms.Label();
            this.lblSeccionTransformaciones = new System.Windows.Forms.Label();
            this.menu.SuspendLayout();
            this.barraEstado.SuspendLayout();
            this.panelHerramientas.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            this.grillaHerramientas.SuspendLayout();
            this.panelGrosor.SuspendLayout();
            this.barraGrosor.SuspendLayout();
            this.panelPaleta.SuspendLayout();
            this.panelLienzo.SuspendLayout();
            this.panelTransformaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traslacionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.traslacionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotacionGrados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.escalaX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.escalaY)).BeginInit();
            this.panelCabecera.SuspendLayout();
            this.SuspendLayout();
            //
            // menu
            //
            this.menu.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.menu.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.menu.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.menu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuArchivo,
            this.menuEditar,
            this.menuVer,
            this.menuAyuda,
            this.separadorAccesos,
            this.rapidoGuardar,
            this.rapidoExportar,
            this.rapidoDeshacer,
            this.rapidoRehacer});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(1475, 32);
            this.menu.TabIndex = 4;
            //
            // menuArchivo
            //
            this.menuArchivo.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemNuevo,
            this.itemAbrir,
            this.separadorArchivo1,
            this.itemGuardar,
            this.itemGuardarComo,
            this.itemExportar,
            this.separadorArchivo2,
            this.itemSalir});
            this.menuArchivo.Name = "menuArchivo";
            this.menuArchivo.Size = new System.Drawing.Size(73, 28);
            this.menuArchivo.Text = "Archivo";
            //
            // itemNuevo
            //
            this.itemNuevo.Name = "itemNuevo";
            this.itemNuevo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.itemNuevo.Size = new System.Drawing.Size(301, 26);
            this.itemNuevo.Text = "Nuevo";
            //
            // itemAbrir
            //
            this.itemAbrir.Name = "itemAbrir";
            this.itemAbrir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.itemAbrir.Size = new System.Drawing.Size(301, 26);
            this.itemAbrir.Text = "Abrir...";
            //
            // separadorArchivo1
            //
            this.separadorArchivo1.Name = "separadorArchivo1";
            this.separadorArchivo1.Size = new System.Drawing.Size(298, 6);
            //
            // itemGuardar
            //
            this.itemGuardar.Name = "itemGuardar";
            this.itemGuardar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.itemGuardar.Size = new System.Drawing.Size(301, 26);
            this.itemGuardar.Text = "Guardar";
            //
            // itemGuardarComo
            //
            this.itemGuardarComo.Name = "itemGuardarComo";
            this.itemGuardarComo.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
            | System.Windows.Forms.Keys.S)));
            this.itemGuardarComo.Size = new System.Drawing.Size(301, 26);
            this.itemGuardarComo.Text = "Guardar como...";
            //
            // itemExportar
            //
            this.itemExportar.Name = "itemExportar";
            this.itemExportar.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.itemExportar.Size = new System.Drawing.Size(301, 26);
            this.itemExportar.Text = "Exportar PNG...";
            //
            // separadorArchivo2
            //
            this.separadorArchivo2.Name = "separadorArchivo2";
            this.separadorArchivo2.Size = new System.Drawing.Size(298, 6);
            //
            // itemSalir
            //
            this.itemSalir.Name = "itemSalir";
            this.itemSalir.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.itemSalir.Size = new System.Drawing.Size(301, 26);
            this.itemSalir.Text = "Salir";
            //
            // menuEditar
            //
            this.menuEditar.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemDeshacer,
            this.itemRehacer,
            this.separadorEditar,
            this.itemEliminar,
            this.itemLimpiar});
            this.menuEditar.Name = "menuEditar";
            this.menuEditar.Size = new System.Drawing.Size(62, 28);
            this.menuEditar.Text = "Editar";
            //
            // itemDeshacer
            //
            this.itemDeshacer.Name = "itemDeshacer";
            this.itemDeshacer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.itemDeshacer.Size = new System.Drawing.Size(250, 26);
            this.itemDeshacer.Text = "Deshacer";
            //
            // itemRehacer
            //
            this.itemRehacer.Name = "itemRehacer";
            this.itemRehacer.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.itemRehacer.Size = new System.Drawing.Size(250, 26);
            this.itemRehacer.Text = "Rehacer";
            //
            // separadorEditar
            //
            this.separadorEditar.Name = "separadorEditar";
            this.separadorEditar.Size = new System.Drawing.Size(247, 6);
            //
            // itemEliminar
            //
            this.itemEliminar.Name = "itemEliminar";
            this.itemEliminar.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.itemEliminar.Size = new System.Drawing.Size(250, 26);
            this.itemEliminar.Text = "Eliminar seleccion";
            //
            // itemLimpiar
            //
            this.itemLimpiar.Name = "itemLimpiar";
            this.itemLimpiar.Size = new System.Drawing.Size(250, 26);
            this.itemLimpiar.Text = "Limpiar lienzo";
            //
            // menuVer
            //
            this.menuVer.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemZoom25,
            this.itemZoom50,
            this.itemZoom100,
            this.itemZoom200,
            this.itemZoom400});
            this.menuVer.Name = "menuVer";
            this.menuVer.Size = new System.Drawing.Size(44, 28);
            this.menuVer.Text = "Ver";
            //
            // itemZoom25
            //
            this.itemZoom25.Name = "itemZoom25";
            this.itemZoom25.Size = new System.Drawing.Size(176, 26);
            this.itemZoom25.Text = "Zoom 25 %";
            //
            // itemZoom50
            //
            this.itemZoom50.Name = "itemZoom50";
            this.itemZoom50.Size = new System.Drawing.Size(176, 26);
            this.itemZoom50.Text = "Zoom 50 %";
            //
            // itemZoom100
            //
            this.itemZoom100.Name = "itemZoom100";
            this.itemZoom100.Size = new System.Drawing.Size(176, 26);
            this.itemZoom100.Text = "Zoom 100 %";
            //
            // itemZoom200
            //
            this.itemZoom200.Name = "itemZoom200";
            this.itemZoom200.Size = new System.Drawing.Size(176, 26);
            this.itemZoom200.Text = "Zoom 200 %";
            //
            // itemZoom400
            //
            this.itemZoom400.Name = "itemZoom400";
            this.itemZoom400.Size = new System.Drawing.Size(176, 26);
            this.itemZoom400.Text = "Zoom 400 %";
            //
            // menuAyuda
            //
            this.menuAyuda.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemControles,
            this.itemAcerca});
            this.menuAyuda.Name = "menuAyuda";
            this.menuAyuda.Size = new System.Drawing.Size(65, 28);
            this.menuAyuda.Text = "Ayuda";
            //
            // itemControles
            //
            this.itemControles.Name = "itemControles";
            this.itemControles.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.itemControles.Size = new System.Drawing.Size(230, 26);
            this.itemControles.Text = "Controles";
            //
            // itemAcerca
            //
            this.itemAcerca.Name = "itemAcerca";
            this.itemAcerca.Size = new System.Drawing.Size(230, 26);
            this.itemAcerca.Text = "Acerca de MichiPaint";
            //
            // separadorAccesos
            //
            this.separadorAccesos.Name = "separadorAccesos";
            this.separadorAccesos.Size = new System.Drawing.Size(6, 28);
            //
            // rapidoGuardar
            //
            this.rapidoGuardar.Name = "rapidoGuardar";
            this.rapidoGuardar.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.rapidoGuardar.Size = new System.Drawing.Size(18, 28);
            this.rapidoGuardar.ToolTipText = "Guardar";
            //
            // rapidoExportar
            //
            this.rapidoExportar.Name = "rapidoExportar";
            this.rapidoExportar.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.rapidoExportar.Size = new System.Drawing.Size(18, 28);
            this.rapidoExportar.ToolTipText = "Exportar PNG";
            //
            // rapidoDeshacer
            //
            this.rapidoDeshacer.Name = "rapidoDeshacer";
            this.rapidoDeshacer.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.rapidoDeshacer.Size = new System.Drawing.Size(18, 28);
            this.rapidoDeshacer.ToolTipText = "Deshacer";
            //
            // rapidoRehacer
            //
            this.rapidoRehacer.Name = "rapidoRehacer";
            this.rapidoRehacer.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.rapidoRehacer.Size = new System.Drawing.Size(18, 28);
            this.rapidoRehacer.ToolTipText = "Rehacer";
            //
            // barraEstado — colores originales + fuente Consolas
            //
            this.barraEstado.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.barraEstado.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.barraEstado.Font = new System.Drawing.Font("Consolas", 8.5F);
            this.barraEstado.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.barraEstado.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.estadoHerramienta,
            this.estadoCoordenadas,
            this.estadoTamano});
            this.barraEstado.Location = new System.Drawing.Point(0, 734);
            this.barraEstado.Name = "barraEstado";
            this.barraEstado.Size = new System.Drawing.Size(1180, 26);
            this.barraEstado.TabIndex = 3;
            //
            // estadoHerramienta
            //
            this.estadoHerramienta.Name = "estadoHerramienta";
            this.estadoHerramienta.Size = new System.Drawing.Size(1021, 20);
            this.estadoHerramienta.Spring = true;
            this.estadoHerramienta.Text = "Lapiz";
            this.estadoHerramienta.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // estadoCoordenadas
            //
            this.estadoCoordenadas.Name = "estadoCoordenadas";
            this.estadoCoordenadas.Size = new System.Drawing.Size(64, 20);
            this.estadoCoordenadas.Text = "X: 0  Y: 0";
            //
            // estadoTamano
            //
            this.estadoTamano.Name = "estadoTamano";
            this.estadoTamano.Size = new System.Drawing.Size(80, 20);
            this.estadoTamano.Text = "1200 x 800";
            //
            // panelHerramientas
            //
            this.panelHerramientas.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.panelHerramientas.Controls.Add(this.panelOpciones);
            this.panelHerramientas.Controls.Add(this.grillaHerramientas);
            this.panelHerramientas.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelHerramientas.Location = new System.Drawing.Point(0, 32);
            this.panelHerramientas.Name = "panelHerramientas";
            this.panelHerramientas.Padding = new System.Windows.Forms.Padding(4);
            this.panelHerramientas.Size = new System.Drawing.Size(114, 650);
            this.panelHerramientas.TabIndex = 1;
            //
            // lblSeccionHerramientas — banda titulo seccion
            //
            this.lblSeccionHerramientas.BackColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblSeccionHerramientas.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccionHerramientas.Font = new System.Drawing.Font("Consolas", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSeccionHerramientas.ForeColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.lblSeccionHerramientas.Name = "lblSeccionHerramientas";
            this.lblSeccionHerramientas.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.lblSeccionHerramientas.Size = new System.Drawing.Size(106, 22);
            this.lblSeccionHerramientas.TabIndex = 3;
            this.lblSeccionHerramientas.Text = "[ HERRAMIENTAS ]";
            this.lblSeccionHerramientas.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblSeccionOpciones — banda titulo seccion opciones
            //
            this.lblSeccionOpciones.BackColor = System.Drawing.Color.FromArgb(191, 174, 145);
            this.lblSeccionOpciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccionOpciones.Font = new System.Drawing.Font("Consolas", 7F, System.Drawing.FontStyle.Bold);
            this.lblSeccionOpciones.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblSeccionOpciones.Name = "lblSeccionOpciones";
            this.lblSeccionOpciones.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.lblSeccionOpciones.Size = new System.Drawing.Size(106, 20);
            this.lblSeccionOpciones.TabIndex = 4;
            this.lblSeccionOpciones.Text = "[ OPCIONES ]";
            this.lblSeccionOpciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // panelOpciones
            //
            this.panelOpciones.AutoScroll = true;
            this.panelOpciones.Controls.Add(this.lblGrosor);
            this.panelOpciones.Controls.Add(this.panelGrosor);
            this.panelOpciones.Controls.Add(this.grosorFigura);
            this.panelOpciones.Controls.Add(this.usarRelleno);
            this.panelOpciones.Controls.Add(this.lblFigura);
            this.panelOpciones.Controls.Add(this.selectorFigura);
            this.panelOpciones.Controls.Add(this.lblAlgoritmoLinea);
            this.panelOpciones.Controls.Add(this.algoritmoLinea);
            this.panelOpciones.Controls.Add(this.lblAlgoritmoCirculo);
            this.panelOpciones.Controls.Add(this.algoritmoCirculo);
            this.panelOpciones.Controls.Add(this.btnFuente);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelOpciones.Location = new System.Drawing.Point(4, 368);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(2);
            this.panelOpciones.Size = new System.Drawing.Size(106, 278);
            this.panelOpciones.TabIndex = 0;
            this.panelOpciones.WrapContents = false;
            //
            // lblGrosor
            //
            this.lblGrosor.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblGrosor.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblGrosor.Location = new System.Drawing.Point(7, 4);
            this.lblGrosor.Name = "lblGrosor";
            this.lblGrosor.Size = new System.Drawing.Size(94, 18);
            this.lblGrosor.TabIndex = 0;
            this.lblGrosor.Text = "Grosor: 2";
            //
            // panelGrosor
            //
            this.panelGrosor.BackColor = System.Drawing.Color.FromArgb(255, 253, 245);
            this.panelGrosor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelGrosor.Controls.Add(this.lblValorGrosor);
            this.panelGrosor.Controls.Add(this.lblMinMaxGrosor);
            this.panelGrosor.Controls.Add(this.barraGrosor);
            this.panelGrosor.Controls.Add(this.perillaGrosor);
            this.panelGrosor.Location = new System.Drawing.Point(7, 24);
            this.panelGrosor.Name = "panelGrosor";
            this.panelGrosor.Size = new System.Drawing.Size(94, 60);
            this.panelGrosor.TabIndex = 1;
            //
            // lblValorGrosor
            //
            this.lblValorGrosor.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold);
            this.lblValorGrosor.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblValorGrosor.Location = new System.Drawing.Point(7, 4);
            this.lblValorGrosor.Name = "lblValorGrosor";
            this.lblValorGrosor.Size = new System.Drawing.Size(78, 16);
            this.lblValorGrosor.TabIndex = 0;
            this.lblValorGrosor.Text = "2 px";
            this.lblValorGrosor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblMinMaxGrosor
            //
            this.lblMinMaxGrosor.Font = new System.Drawing.Font("Consolas", 6F);
            this.lblMinMaxGrosor.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblMinMaxGrosor.Location = new System.Drawing.Point(7, 44);
            this.lblMinMaxGrosor.Name = "lblMinMaxGrosor";
            this.lblMinMaxGrosor.Size = new System.Drawing.Size(78, 12);
            this.lblMinMaxGrosor.TabIndex = 3;
            this.lblMinMaxGrosor.Text = "1        100";
            this.lblMinMaxGrosor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // barraGrosor
            //
            this.barraGrosor.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.barraGrosor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.barraGrosor.Controls.Add(this.rellenoGrosor);
            this.barraGrosor.Location = new System.Drawing.Point(10, 27);
            this.barraGrosor.Name = "barraGrosor";
            this.barraGrosor.Size = new System.Drawing.Size(72, 8);
            this.barraGrosor.TabIndex = 1;
            //
            // rellenoGrosor
            //
            this.rellenoGrosor.BackColor = System.Drawing.Color.FromArgb(228, 169, 168);
            this.rellenoGrosor.Location = new System.Drawing.Point(0, 0);
            this.rellenoGrosor.Name = "rellenoGrosor";
            this.rellenoGrosor.Size = new System.Drawing.Size(1, 6);
            this.rellenoGrosor.TabIndex = 0;
            //
            // perillaGrosor
            //
            this.perillaGrosor.BackColor = System.Drawing.Color.FromArgb(241, 216, 132);
            this.perillaGrosor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.perillaGrosor.Location = new System.Drawing.Point(5, 22);
            this.perillaGrosor.Name = "perillaGrosor";
            this.perillaGrosor.Size = new System.Drawing.Size(12, 18);
            this.perillaGrosor.TabIndex = 2;
            //
            // grosorFigura
            //
            this.grosorFigura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.grosorFigura.Font = new System.Drawing.Font("Consolas", 8F);
            this.grosorFigura.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.grosorFigura.FormattingEnabled = true;
            this.grosorFigura.Items.AddRange(new object[] {
            "1 px",
            "3 px",
            "5 px",
            "8 px"});
            this.grosorFigura.Location = new System.Drawing.Point(7, 24);
            this.grosorFigura.Name = "grosorFigura";
            this.grosorFigura.Size = new System.Drawing.Size(94, 22);
            this.grosorFigura.TabIndex = 2;
            this.grosorFigura.Visible = false;
            //
            // usarRelleno
            //
            this.usarRelleno.Font = new System.Drawing.Font("Consolas", 8F);
            this.usarRelleno.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.usarRelleno.Location = new System.Drawing.Point(7, 74);
            this.usarRelleno.Name = "usarRelleno";
            this.usarRelleno.Size = new System.Drawing.Size(94, 22);
            this.usarRelleno.TabIndex = 2;
            this.usarRelleno.Text = "Relleno";
            //
            // lblFigura
            //
            this.lblFigura.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblFigura.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblFigura.Location = new System.Drawing.Point(7, 99);
            this.lblFigura.Name = "lblFigura";
            this.lblFigura.Size = new System.Drawing.Size(94, 18);
            this.lblFigura.TabIndex = 3;
            this.lblFigura.Text = "Figura";
            //
            // selectorFigura
            //
            this.selectorFigura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.selectorFigura.Font = new System.Drawing.Font("Consolas", 8F);
            this.selectorFigura.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.selectorFigura.FormattingEnabled = true;
            this.selectorFigura.Items.AddRange(new object[] {
            "Polígono libre",
            "Corazón",
            "Estrella",
            "Flecha",
            "Cruz",
            "Rombo",
            "Trapecio"});
            this.selectorFigura.Location = new System.Drawing.Point(7, 120);
            this.selectorFigura.Name = "selectorFigura";
            this.selectorFigura.Size = new System.Drawing.Size(94, 22);
            this.selectorFigura.TabIndex = 4;
            //
            // lblAlgoritmoLinea
            //
            this.lblAlgoritmoLinea.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblAlgoritmoLinea.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblAlgoritmoLinea.Location = new System.Drawing.Point(5, 78);
            this.lblAlgoritmoLinea.Name = "lblAlgoritmoLinea";
            this.lblAlgoritmoLinea.Size = new System.Drawing.Size(94, 18);
            this.lblAlgoritmoLinea.TabIndex = 3;
            this.lblAlgoritmoLinea.Text = "Alg. línea";
            this.lblAlgoritmoLinea.Visible = false;
            //
            // algoritmoLinea
            //
            this.algoritmoLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algoritmoLinea.Font = new System.Drawing.Font("Consolas", 8F);
            this.algoritmoLinea.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.algoritmoLinea.Items.AddRange(new object[] {
            "Bresenham",
            "DDA",
            "Punto medio"});
            this.algoritmoLinea.Location = new System.Drawing.Point(5, 99);
            this.algoritmoLinea.Name = "algoritmoLinea";
            this.algoritmoLinea.Size = new System.Drawing.Size(94, 22);
            this.algoritmoLinea.TabIndex = 4;
            this.algoritmoLinea.Visible = false;
            //
            // lblAlgoritmoCirculo
            //
            this.lblAlgoritmoCirculo.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblAlgoritmoCirculo.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblAlgoritmoCirculo.Location = new System.Drawing.Point(5, 128);
            this.lblAlgoritmoCirculo.Name = "lblAlgoritmoCirculo";
            this.lblAlgoritmoCirculo.Size = new System.Drawing.Size(94, 18);
            this.lblAlgoritmoCirculo.TabIndex = 5;
            this.lblAlgoritmoCirculo.Text = "Alg. elipse";
            this.lblAlgoritmoCirculo.Visible = false;
            //
            // algoritmoCirculo
            //
            this.algoritmoCirculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algoritmoCirculo.Font = new System.Drawing.Font("Consolas", 8F);
            this.algoritmoCirculo.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.algoritmoCirculo.Items.AddRange(new object[] {
            "Punto medio",
            "Polar",
            "Ecuación"});
            this.algoritmoCirculo.Location = new System.Drawing.Point(5, 149);
            this.algoritmoCirculo.Name = "algoritmoCirculo";
            this.algoritmoCirculo.Size = new System.Drawing.Size(94, 22);
            this.algoritmoCirculo.TabIndex = 6;
            this.algoritmoCirculo.Visible = false;
            //
            // btnFuente
            //
            this.btnFuente.BackColor = System.Drawing.Color.FromArgb(241, 216, 132);
            this.btnFuente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFuente.Font = new System.Drawing.Font("Consolas", 8F, System.Drawing.FontStyle.Bold);
            this.btnFuente.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.btnFuente.Location = new System.Drawing.Point(7, 152);
            this.btnFuente.Name = "btnFuente";
            this.btnFuente.Size = new System.Drawing.Size(94, 26);
            this.btnFuente.TabIndex = 5;
            this.btnFuente.Text = "Fuente...";
            //
            // grillaHerramientas
            //
            this.grillaHerramientas.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.grillaHerramientas.ColumnCount = 2;
            this.grillaHerramientas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.grillaHerramientas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 53F));
            this.grillaHerramientas.Controls.Add(this.btnLapiz, 0, 0);
            this.grillaHerramientas.Controls.Add(this.btnBorrador, 1, 0);
            this.grillaHerramientas.Controls.Add(this.btnPincel, 0, 1);
            this.grillaHerramientas.Controls.Add(this.btnRelleno, 1, 1);
            this.grillaHerramientas.Controls.Add(this.btnLinea, 0, 2);
            this.grillaHerramientas.Controls.Add(this.btnCurva, 1, 2);
            this.grillaHerramientas.Controls.Add(this.btnRectangulo, 0, 3);
            this.grillaHerramientas.Controls.Add(this.btnRectanguloRedondeado, 1, 3);
            this.grillaHerramientas.Controls.Add(this.btnElipse, 0, 4);
            this.grillaHerramientas.Controls.Add(this.btnPoligono, 1, 4);
            this.grillaHerramientas.Controls.Add(this.btnSeleccion, 0, 5);
            this.grillaHerramientas.Controls.Add(this.btnSeleccionLibre, 1, 5);
            this.grillaHerramientas.Controls.Add(this.btnTexto, 0, 6);
            this.grillaHerramientas.Controls.Add(this.btnLimpiar, 1, 6);
            this.grillaHerramientas.Dock = System.Windows.Forms.DockStyle.Top;
            this.grillaHerramientas.Location = new System.Drawing.Point(4, 4);
            this.grillaHerramientas.Name = "grillaHerramientas";
            this.grillaHerramientas.RowCount = 7;
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.grillaHerramientas.Size = new System.Drawing.Size(106, 364);
            this.grillaHerramientas.TabIndex = 1;
            //
            // botones de herramientas
            //
            this.btnBorrador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBorrador.Margin = new System.Windows.Forms.Padding(2);
            this.btnBorrador.Name = "btnBorrador";
            this.btnBorrador.Size = new System.Drawing.Size(49, 48);
            this.btnBorrador.TabIndex = 0;
            this.btnBorrador.TabStop = false;
            //
            this.btnLinea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLinea.Margin = new System.Windows.Forms.Padding(2);
            this.btnLinea.Name = "btnLinea";
            this.btnLinea.Size = new System.Drawing.Size(49, 48);
            this.btnLinea.TabIndex = 1;
            this.btnLinea.TabStop = false;
            //
            this.btnCurva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCurva.Margin = new System.Windows.Forms.Padding(2);
            this.btnCurva.Name = "btnCurva";
            this.btnCurva.Size = new System.Drawing.Size(49, 48);
            this.btnCurva.TabIndex = 2;
            this.btnCurva.TabStop = false;
            //
            this.btnPincel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPincel.Margin = new System.Windows.Forms.Padding(2);
            this.btnPincel.Name = "btnPincel";
            this.btnPincel.Size = new System.Drawing.Size(49, 48);
            this.btnPincel.TabIndex = 3;
            this.btnPincel.TabStop = false;
            //
            this.btnRelleno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRelleno.Margin = new System.Windows.Forms.Padding(2);
            this.btnRelleno.Name = "btnRelleno";
            this.btnRelleno.Size = new System.Drawing.Size(49, 48);
            this.btnRelleno.TabIndex = 4;
            this.btnRelleno.TabStop = false;
            //
            this.btnLapiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLapiz.Margin = new System.Windows.Forms.Padding(2);
            this.btnLapiz.Name = "btnLapiz";
            this.btnLapiz.Size = new System.Drawing.Size(49, 48);
            this.btnLapiz.TabIndex = 5;
            this.btnLapiz.TabStop = false;
            //
            this.btnTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTexto.Margin = new System.Windows.Forms.Padding(2);
            this.btnTexto.Name = "btnTexto";
            this.btnTexto.Size = new System.Drawing.Size(49, 48);
            this.btnTexto.TabIndex = 6;
            this.btnTexto.TabStop = false;
            //
            this.btnPoligono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPoligono.Margin = new System.Windows.Forms.Padding(2);
            this.btnPoligono.Name = "btnPoligono";
            this.btnPoligono.Size = new System.Drawing.Size(49, 48);
            this.btnPoligono.TabIndex = 7;
            this.btnPoligono.TabStop = false;
            //
            this.btnRectangulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRectangulo.Margin = new System.Windows.Forms.Padding(2);
            this.btnRectangulo.Name = "btnRectangulo";
            this.btnRectangulo.Size = new System.Drawing.Size(49, 48);
            this.btnRectangulo.TabIndex = 8;
            this.btnRectangulo.TabStop = false;
            //
            this.btnSeleccionLibre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSeleccionLibre.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccionLibre.Name = "btnSeleccionLibre";
            this.btnSeleccionLibre.Size = new System.Drawing.Size(49, 48);
            this.btnSeleccionLibre.TabIndex = 9;
            this.btnSeleccionLibre.TabStop = false;
            //
            this.btnSeleccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSeleccion.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccion.Name = "btnSeleccion";
            this.btnSeleccion.Size = new System.Drawing.Size(49, 48);
            this.btnSeleccion.TabIndex = 10;
            this.btnSeleccion.TabStop = false;
            //
            this.btnRectanguloRedondeado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRectanguloRedondeado.Margin = new System.Windows.Forms.Padding(2);
            this.btnRectanguloRedondeado.Name = "btnRectanguloRedondeado";
            this.btnRectanguloRedondeado.Size = new System.Drawing.Size(49, 48);
            this.btnRectanguloRedondeado.TabIndex = 11;
            this.btnRectanguloRedondeado.TabStop = false;
            //
            this.btnZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnZoom.Margin = new System.Windows.Forms.Padding(2);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(49, 48);
            this.btnZoom.TabIndex = 12;
            this.btnZoom.TabStop = false;
            //
            this.btnLimpiar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(49, 48);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.TabStop = false;
            //
            this.btnElipse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnElipse.Margin = new System.Windows.Forms.Padding(2);
            this.btnElipse.Name = "btnElipse";
            this.btnElipse.Size = new System.Drawing.Size(49, 48);
            this.btnElipse.TabIndex = 14;
            this.btnElipse.TabStop = false;
            //
            // btnColorPersonalizado
            //
            this.btnColorPersonalizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorPersonalizado.BackColor = System.Drawing.Color.FromArgb(241, 216, 132);
            this.btnColorPersonalizado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorPersonalizado.Font = new System.Drawing.Font("Consolas", 7.5F, System.Drawing.FontStyle.Bold);
            this.btnColorPersonalizado.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.btnColorPersonalizado.Location = new System.Drawing.Point(820, 9);
            this.btnColorPersonalizado.Name = "btnColorPersonalizado";
            this.btnColorPersonalizado.Size = new System.Drawing.Size(118, 28);
            this.btnColorPersonalizado.TabIndex = 3;
            this.btnColorPersonalizado.Text = "+ Mas colores";
            //
            // panelPaleta
            //
            this.panelPaleta.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.panelPaleta.Controls.Add(this.muestraLinea);
            this.panelPaleta.Controls.Add(this.muestraRelleno);
            this.panelPaleta.Controls.Add(this.lblColoresActuales);
            this.panelPaleta.Controls.Add(this.flujoPaleta);
            this.panelPaleta.Controls.Add(this.btnColorPersonalizado);
            this.panelPaleta.Controls.Add(this.zoom);
            this.panelPaleta.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPaleta.Location = new System.Drawing.Point(0, 682);
            this.panelPaleta.Name = "panelPaleta";
            this.panelPaleta.Size = new System.Drawing.Size(1180, 50);
            this.panelPaleta.TabIndex = 2;
            //
            // muestraLinea
            //
            this.muestraLinea.BackColor = System.Drawing.Color.Black;
            this.muestraLinea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.muestraLinea.Location = new System.Drawing.Point(8, 7);
            this.muestraLinea.Name = "muestraLinea";
            this.muestraLinea.Size = new System.Drawing.Size(26, 26);
            this.muestraLinea.TabIndex = 0;
            //
            // muestraRelleno
            //
            this.muestraRelleno.BackColor = System.Drawing.Color.FromArgb(241, 216, 132);
            this.muestraRelleno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.muestraRelleno.Location = new System.Drawing.Point(22, 19);
            this.muestraRelleno.Name = "muestraRelleno";
            this.muestraRelleno.Size = new System.Drawing.Size(26, 26);
            this.muestraRelleno.TabIndex = 1;
            //
            // lblColoresActuales
            //
            this.lblColoresActuales.Font = new System.Drawing.Font("Consolas", 7F);
            this.lblColoresActuales.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblColoresActuales.Location = new System.Drawing.Point(54, 6);
            this.lblColoresActuales.Name = "lblColoresActuales";
            this.lblColoresActuales.Size = new System.Drawing.Size(110, 38);
            this.lblColoresActuales.TabIndex = 2;
            this.lblColoresActuales.Text = "Linea (frente)\r\nRelleno (fondo)";
            //
            // flujoPaleta
            //
            this.flujoPaleta.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.flujoPaleta.Location = new System.Drawing.Point(168, 6);
            this.flujoPaleta.Name = "flujoPaleta";
            this.flujoPaleta.Size = new System.Drawing.Size(645, 38);
            this.flujoPaleta.TabIndex = 2;
            this.flujoPaleta.WrapContents = false;
            //
            // zoom
            //
            this.zoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoom.Font = new System.Drawing.Font("Consolas", 8F);
            this.zoom.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.zoom.Items.AddRange(new object[] {
            "25 %",
            "50 %",
            "100 %",
            "200 %",
            "400 %"});
            this.zoom.Location = new System.Drawing.Point(950, 12);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(80, 22);
            this.zoom.TabIndex = 4;
            //
            // panelLienzo
            //
            this.panelLienzo.AutoScroll = true;
            this.panelLienzo.BackColor = System.Drawing.Color.FromArgb(190, 174, 145);
            this.panelLienzo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLienzo.Controls.Add(this.lienzoControl);
            this.panelLienzo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLienzo.Location = new System.Drawing.Point(114, 32);
            this.panelLienzo.Name = "panelLienzo";
            this.panelLienzo.Padding = new System.Windows.Forms.Padding(10);
            this.panelLienzo.Size = new System.Drawing.Size(1066, 650);
            this.panelLienzo.TabIndex = 0;
            //
            // lienzoControl
            //
            this.lienzoControl.BackColor = System.Drawing.Color.FromArgb(255, 253, 245);
            this.lienzoControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.lienzoControl.Location = new System.Drawing.Point(10, 10);
            this.lienzoControl.Name = "lienzoControl";
            this.lienzoControl.Size = new System.Drawing.Size(1200, 800);
            this.lienzoControl.TabIndex = 0;
            //
            // panelTransformaciones — panel derecho (antes faltaba en Controls.Add)
            //
            this.panelTransformaciones.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.panelTransformaciones.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.panelTransformaciones.Controls.Add(this.lblTituloTransformaciones);
            this.panelTransformaciones.Controls.Add(this.lblSeccionTransformaciones);
            this.panelTransformaciones.Controls.Add(this.lblTraslacionX);
            this.panelTransformaciones.Controls.Add(this.traslacionX);
            this.panelTransformaciones.Controls.Add(this.lblTraslacionY);
            this.panelTransformaciones.Controls.Add(this.traslacionY);
            this.panelTransformaciones.Controls.Add(this.btnAplicarTraslacion);
            this.panelTransformaciones.Controls.Add(this.lblRotacion);
            this.panelTransformaciones.Controls.Add(this.rotacionGrados);
            this.panelTransformaciones.Controls.Add(this.btnAplicarRotacion);
            this.panelTransformaciones.Controls.Add(this.lblEscalaX);
            this.panelTransformaciones.Controls.Add(this.escalaX);
            this.panelTransformaciones.Controls.Add(this.lblEscalaY);
            this.panelTransformaciones.Controls.Add(this.escalaY);
            this.panelTransformaciones.Controls.Add(this.btnAplicarEscala);
            this.panelTransformaciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelTransformaciones.Location = new System.Drawing.Point(1194, 32);
            this.panelTransformaciones.Name = "panelTransformaciones";
            this.panelTransformaciones.Size = new System.Drawing.Size(281, 805);
            this.panelTransformaciones.TabIndex = 1;
            //
            // lblSeccionTransformaciones
            //
            this.lblSeccionTransformaciones.BackColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblSeccionTransformaciones.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSeccionTransformaciones.Font = new System.Drawing.Font("Consolas", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSeccionTransformaciones.ForeColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.lblSeccionTransformaciones.Name = "lblSeccionTransformaciones";
            this.lblSeccionTransformaciones.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.lblSeccionTransformaciones.Size = new System.Drawing.Size(281, 22);
            this.lblSeccionTransformaciones.TabIndex = 16;
            this.lblSeccionTransformaciones.Text = "[ TRANSFORMACIONES ]";
            this.lblSeccionTransformaciones.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblTituloTransformaciones
            //
            this.lblTituloTransformaciones.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.lblTituloTransformaciones.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblTituloTransformaciones.Location = new System.Drawing.Point(10, 36);
            this.lblTituloTransformaciones.Name = "lblTituloTransformaciones";
            this.lblTituloTransformaciones.Size = new System.Drawing.Size(200, 22);
            this.lblTituloTransformaciones.TabIndex = 0;
            this.lblTituloTransformaciones.Text = "Traslacion";
            //
            // lblTraslacionX
            //
            this.lblTraslacionX.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblTraslacionX.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblTraslacionX.Location = new System.Drawing.Point(10, 62);
            this.lblTraslacionX.Name = "lblTraslacionX";
            this.lblTraslacionX.Size = new System.Drawing.Size(62, 22);
            this.lblTraslacionX.TabIndex = 1;
            this.lblTraslacionX.Text = "Mover X:";
            //
            // traslacionX
            //
            this.traslacionX.Font = new System.Drawing.Font("Consolas", 9F);
            this.traslacionX.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.traslacionX.Location = new System.Drawing.Point(80, 60);
            this.traslacionX.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            this.traslacionX.Minimum = new decimal(new int[] { 4000, 0, 0, -2147483648 });
            this.traslacionX.Name = "traslacionX";
            this.traslacionX.Size = new System.Drawing.Size(85, 22);
            this.traslacionX.TabIndex = 2;
            //
            // lblTraslacionY
            //
            this.lblTraslacionY.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblTraslacionY.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblTraslacionY.Location = new System.Drawing.Point(10, 90);
            this.lblTraslacionY.Name = "lblTraslacionY";
            this.lblTraslacionY.Size = new System.Drawing.Size(62, 22);
            this.lblTraslacionY.TabIndex = 3;
            this.lblTraslacionY.Text = "Mover Y:";
            //
            // traslacionY
            //
            this.traslacionY.Font = new System.Drawing.Font("Consolas", 9F);
            this.traslacionY.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.traslacionY.Location = new System.Drawing.Point(80, 88);
            this.traslacionY.Maximum = new decimal(new int[] { 4000, 0, 0, 0 });
            this.traslacionY.Minimum = new decimal(new int[] { 4000, 0, 0, -2147483648 });
            this.traslacionY.Name = "traslacionY";
            this.traslacionY.Size = new System.Drawing.Size(85, 22);
            this.traslacionY.TabIndex = 4;
            //
            // btnAplicarTraslacion
            //
            this.btnAplicarTraslacion.BackColor = System.Drawing.Color.FromArgb(241, 216, 132);
            this.btnAplicarTraslacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarTraslacion.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnAplicarTraslacion.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.btnAplicarTraslacion.Location = new System.Drawing.Point(10, 118);
            this.btnAplicarTraslacion.Name = "btnAplicarTraslacion";
            this.btnAplicarTraslacion.Size = new System.Drawing.Size(155, 30);
            this.btnAplicarTraslacion.TabIndex = 5;
            this.btnAplicarTraslacion.Text = ">> Trasladar";
            //
            // lblRotacion
            //
            this.lblRotacion.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.lblRotacion.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblRotacion.Location = new System.Drawing.Point(10, 166);
            this.lblRotacion.Name = "lblRotacion";
            this.lblRotacion.Size = new System.Drawing.Size(150, 22);
            this.lblRotacion.TabIndex = 6;
            this.lblRotacion.Text = "Rotacion";
            //
            // rotacionGrados
            //
            this.rotacionGrados.Font = new System.Drawing.Font("Consolas", 9F);
            this.rotacionGrados.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.rotacionGrados.Location = new System.Drawing.Point(10, 194);
            this.rotacionGrados.Maximum = new decimal(new int[] { 360, 0, 0, 0 });
            this.rotacionGrados.Minimum = new decimal(new int[] { 360, 0, 0, -2147483648 });
            this.rotacionGrados.Name = "rotacionGrados";
            this.rotacionGrados.Size = new System.Drawing.Size(85, 22);
            this.rotacionGrados.TabIndex = 7;
            this.rotacionGrados.Value = new decimal(new int[] { 15, 0, 0, 0 });
            //
            // btnAplicarRotacion
            //
            this.btnAplicarRotacion.BackColor = System.Drawing.Color.FromArgb(241, 216, 132);
            this.btnAplicarRotacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarRotacion.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnAplicarRotacion.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.btnAplicarRotacion.Location = new System.Drawing.Point(10, 224);
            this.btnAplicarRotacion.Name = "btnAplicarRotacion";
            this.btnAplicarRotacion.Size = new System.Drawing.Size(155, 30);
            this.btnAplicarRotacion.TabIndex = 8;
            this.btnAplicarRotacion.Text = ">> Rotar";
            //
            // lblEscalaX
            //
            this.lblEscalaX.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Bold);
            this.lblEscalaX.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblEscalaX.Location = new System.Drawing.Point(10, 272);
            this.lblEscalaX.Name = "lblEscalaX";
            this.lblEscalaX.Size = new System.Drawing.Size(150, 22);
            this.lblEscalaX.TabIndex = 9;
            this.lblEscalaX.Text = "Escala";
            //
            // escalaX
            //
            this.escalaX.Font = new System.Drawing.Font("Consolas", 9F);
            this.escalaX.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.escalaX.Location = new System.Drawing.Point(10, 298);
            this.escalaX.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.escalaX.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.escalaX.Name = "escalaX";
            this.escalaX.Size = new System.Drawing.Size(85, 22);
            this.escalaX.TabIndex = 10;
            this.escalaX.Value = new decimal(new int[] { 100, 0, 0, 0 });
            //
            // lblEscalaY
            //
            this.lblEscalaY.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblEscalaY.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.lblEscalaY.Location = new System.Drawing.Point(10, 298);
            this.lblEscalaY.Name = "lblEscalaY";
            this.lblEscalaY.Size = new System.Drawing.Size(55, 22);
            this.lblEscalaY.TabIndex = 11;
            this.lblEscalaY.Text = "Esc. X:";
            //
            // escalaY
            //
            this.escalaY.Font = new System.Drawing.Font("Consolas", 9F);
            this.escalaY.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.escalaY.Location = new System.Drawing.Point(10, 326);
            this.escalaY.Maximum = new decimal(new int[] { 500, 0, 0, 0 });
            this.escalaY.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.escalaY.Name = "escalaY";
            this.escalaY.Size = new System.Drawing.Size(85, 22);
            this.escalaY.TabIndex = 12;
            this.escalaY.Value = new decimal(new int[] { 100, 0, 0, 0 });
            //
            // btnAplicarEscala
            //
            this.btnAplicarEscala.BackColor = System.Drawing.Color.FromArgb(241, 216, 132);
            this.btnAplicarEscala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarEscala.Font = new System.Drawing.Font("Consolas", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnAplicarEscala.ForeColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.btnAplicarEscala.Location = new System.Drawing.Point(10, 356);
            this.btnAplicarEscala.Name = "btnAplicarEscala";
            this.btnAplicarEscala.Size = new System.Drawing.Size(155, 30);
            this.btnAplicarEscala.TabIndex = 13;
            this.btnAplicarEscala.Text = ">> Escalar";
            //
            // panelCabecera — cabecera decorativa con titulo
            //
            this.panelCabecera.BackColor = System.Drawing.Color.FromArgb(131, 94, 72);
            this.panelCabecera.Controls.Add(this.lblSubtituloApp);
            this.panelCabecera.Controls.Add(this.lblTituloApp);
            this.panelCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCabecera.Location = new System.Drawing.Point(0, 32);
            this.panelCabecera.Name = "panelCabecera";
            this.panelCabecera.Size = new System.Drawing.Size(1475, 68);
            this.panelCabecera.TabIndex = 6;
            //
            // lblTituloApp
            //
            this.lblTituloApp.AutoSize = true;
            this.lblTituloApp.Font = new System.Drawing.Font("Consolas", 22F, System.Drawing.FontStyle.Bold);
            this.lblTituloApp.ForeColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.lblTituloApp.Location = new System.Drawing.Point(14, 5);
            this.lblTituloApp.Name = "lblTituloApp";
            this.lblTituloApp.Size = new System.Drawing.Size(260, 36);
            this.lblTituloApp.TabIndex = 0;
            this.lblTituloApp.Text = "MichiPaint";
            //
            // lblSubtituloApp
            //
            this.lblSubtituloApp.AutoSize = true;
            this.lblSubtituloApp.Font = new System.Drawing.Font("Consolas", 8F);
            this.lblSubtituloApp.ForeColor = System.Drawing.Color.FromArgb(241, 216, 132);
            this.lblSubtituloApp.Location = new System.Drawing.Point(18, 46);
            this.lblSubtituloApp.Name = "lblSubtituloApp";
            this.lblSubtituloApp.Size = new System.Drawing.Size(300, 14);
            this.lblSubtituloApp.TabIndex = 1;
            this.lblSubtituloApp.Text = "Editor de dibujo  ·  Computación Gráfica";
            //
            // MichiPaint
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(242, 226, 186);
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.panelLienzo);
            this.Controls.Add(this.panelHerramientas);
            this.Controls.Add(this.panelPaleta);
            this.Controls.Add(this.barraEstado);
            this.Controls.Add(this.menu);
            this.Font = new System.Drawing.Font("Consolas", 9F);
            this.KeyPreview = true;
            this.MainMenuStrip = this.menu;
            this.MinimumSize = new System.Drawing.Size(900, 620);
            this.Name = "MichiPaint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sin titulo - MichiPaint";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.barraEstado.ResumeLayout(false);
            this.barraEstado.PerformLayout();
            this.panelHerramientas.ResumeLayout(false);
            this.panelOpciones.ResumeLayout(false);
            this.grillaHerramientas.ResumeLayout(false);
            this.panelGrosor.ResumeLayout(false);
            this.barraGrosor.ResumeLayout(false);
            this.panelPaleta.ResumeLayout(false);
            this.panelLienzo.ResumeLayout(false);
            this.panelTransformaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.traslacionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.traslacionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotacionGrados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.escalaX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.escalaY)).EndInit();
            this.panelCabecera.ResumeLayout(false);
            this.panelCabecera.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
