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
        private Label lblAlgoritmoLinea;
        private Label lblAlgoritmoCirculo;
        private Label lblTituloTransformaciones;
        private Label lblTraslacionX;
        private Label lblTraslacionY;
        private Label lblRotacion;
        private Label lblEscalaX;
        private Label lblEscalaY;
        private Label lblColoresActuales;
        private NumericUpDown grosor;
        private NumericUpDown traslacionX;
        private NumericUpDown traslacionY;
        private NumericUpDown rotacionGrados;
        private NumericUpDown escalaX;
        private NumericUpDown escalaY;
        private CheckBox usarRelleno;
        private ComboBox algoritmoLinea;
        private ComboBox algoritmoCirculo;
        private ComboBox zoom;
        private ToolTip ayuda;

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
            this.grosor = new System.Windows.Forms.NumericUpDown();
            this.usarRelleno = new System.Windows.Forms.CheckBox();
            this.lblAlgoritmoLinea = new System.Windows.Forms.Label();
            this.algoritmoLinea = new System.Windows.Forms.ComboBox();
            this.lblAlgoritmoCirculo = new System.Windows.Forms.Label();
            this.algoritmoCirculo = new System.Windows.Forms.ComboBox();
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
            this.menu.SuspendLayout();
            this.barraEstado.SuspendLayout();
            this.panelHerramientas.SuspendLayout();
            this.panelOpciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grosor)).BeginInit();
            this.grillaHerramientas.SuspendLayout();
            this.panelPaleta.SuspendLayout();
            this.panelLienzo.SuspendLayout();
            this.panelTransformaciones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.traslacionX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.traslacionY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotacionGrados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.escalaX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.escalaY)).BeginInit();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(226)))), ((int)(((byte)(186)))));
            this.menu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(94)))), ((int)(((byte)(72)))));
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
            this.menu.Size = new System.Drawing.Size(1475, 38);
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
            this.menuArchivo.Size = new System.Drawing.Size(73, 34);
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
            this.menuEditar.Size = new System.Drawing.Size(62, 34);
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
            this.menuVer.Size = new System.Drawing.Size(44, 34);
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
            this.menuAyuda.Size = new System.Drawing.Size(65, 34);
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
            this.separadorAccesos.Size = new System.Drawing.Size(6, 34);
            // 
            // rapidoGuardar
            // 
            this.rapidoGuardar.Name = "rapidoGuardar";
            this.rapidoGuardar.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.rapidoGuardar.Size = new System.Drawing.Size(18, 34);
            this.rapidoGuardar.ToolTipText = "Guardar";
            // 
            // rapidoExportar
            // 
            this.rapidoExportar.Name = "rapidoExportar";
            this.rapidoExportar.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.rapidoExportar.Size = new System.Drawing.Size(18, 34);
            this.rapidoExportar.ToolTipText = "Exportar PNG";
            // 
            // rapidoDeshacer
            // 
            this.rapidoDeshacer.Name = "rapidoDeshacer";
            this.rapidoDeshacer.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.rapidoDeshacer.Size = new System.Drawing.Size(18, 34);
            this.rapidoDeshacer.ToolTipText = "Deshacer";
            // 
            // rapidoRehacer
            // 
            this.rapidoRehacer.Name = "rapidoRehacer";
            this.rapidoRehacer.Padding = new System.Windows.Forms.Padding(7, 2, 7, 2);
            this.rapidoRehacer.Size = new System.Drawing.Size(18, 34);
            this.rapidoRehacer.ToolTipText = "Rehacer";
            // 
            // barraEstado
            // 
            this.barraEstado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(226)))), ((int)(((byte)(186)))));
            this.barraEstado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(131)))), ((int)(((byte)(94)))), ((int)(((byte)(72)))));
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
            this.panelHerramientas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(226)))), ((int)(((byte)(186)))));
            this.panelHerramientas.Controls.Add(this.panelOpciones);
            this.panelHerramientas.Controls.Add(this.grillaHerramientas);
            this.panelHerramientas.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelHerramientas.Location = new System.Drawing.Point(0, 48);
            this.panelHerramientas.Name = "panelHerramientas";
            this.panelHerramientas.Padding = new System.Windows.Forms.Padding(5);
            this.panelHerramientas.Size = new System.Drawing.Size(175, 805);
            this.panelHerramientas.TabIndex = 1;
            // 
            // panelOpciones
            // 
            this.panelOpciones.AutoScroll = true;
            this.panelOpciones.Controls.Add(this.lblGrosor);
            this.panelOpciones.Controls.Add(this.grosor);
            this.panelOpciones.Controls.Add(this.usarRelleno);
            this.panelOpciones.Controls.Add(this.lblAlgoritmoLinea);
            this.panelOpciones.Controls.Add(this.algoritmoLinea);
            this.panelOpciones.Controls.Add(this.lblAlgoritmoCirculo);
            this.panelOpciones.Controls.Add(this.algoritmoCirculo);
            this.panelOpciones.Controls.Add(this.btnFuente);
            this.panelOpciones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelOpciones.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelOpciones.Location = new System.Drawing.Point(5, 413);
            this.panelOpciones.Name = "panelOpciones";
            this.panelOpciones.Padding = new System.Windows.Forms.Padding(2);
            this.panelOpciones.Size = new System.Drawing.Size(165, 387);
            this.panelOpciones.TabIndex = 0;
            this.panelOpciones.WrapContents = false;
            // 
            // lblGrosor
            // 
            this.lblGrosor.Location = new System.Drawing.Point(5, 2);
            this.lblGrosor.Name = "lblGrosor";
            this.lblGrosor.Size = new System.Drawing.Size(95, 18);
            this.lblGrosor.TabIndex = 0;
            this.lblGrosor.Text = "Grosor";
            // 
            // grosor
            // 
            this.grosor.Location = new System.Drawing.Point(5, 23);
            this.grosor.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.grosor.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.grosor.Name = "grosor";
            this.grosor.Size = new System.Drawing.Size(88, 24);
            this.grosor.TabIndex = 1;
            this.grosor.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // usarRelleno
            // 
            this.usarRelleno.Location = new System.Drawing.Point(5, 53);
            this.usarRelleno.Name = "usarRelleno";
            this.usarRelleno.Size = new System.Drawing.Size(95, 22);
            this.usarRelleno.TabIndex = 2;
            this.usarRelleno.Text = "Con relleno";
            // 
            // lblAlgoritmoLinea
            // 
            this.lblAlgoritmoLinea.Location = new System.Drawing.Point(5, 78);
            this.lblAlgoritmoLinea.Name = "lblAlgoritmoLinea";
            this.lblAlgoritmoLinea.Size = new System.Drawing.Size(95, 18);
            this.lblAlgoritmoLinea.TabIndex = 3;
            this.lblAlgoritmoLinea.Text = "Alg. linea";
            // 
            // algoritmoLinea
            // 
            this.algoritmoLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algoritmoLinea.Items.AddRange(new object[] {
            "Bresenham",
            "DDA",
            "Punto medio"});
            this.algoritmoLinea.Location = new System.Drawing.Point(5, 99);
            this.algoritmoLinea.Name = "algoritmoLinea";
            this.algoritmoLinea.Size = new System.Drawing.Size(94, 26);
            this.algoritmoLinea.TabIndex = 4;
            // 
            // lblAlgoritmoCirculo
            // 
            this.lblAlgoritmoCirculo.Location = new System.Drawing.Point(5, 128);
            this.lblAlgoritmoCirculo.Name = "lblAlgoritmoCirculo";
            this.lblAlgoritmoCirculo.Size = new System.Drawing.Size(95, 18);
            this.lblAlgoritmoCirculo.TabIndex = 5;
            this.lblAlgoritmoCirculo.Text = "Alg. circulo";
            // 
            // algoritmoCirculo
            // 
            this.algoritmoCirculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.algoritmoCirculo.Items.AddRange(new object[] {
            "Punto medio",
            "Polar",
            "Ecuacion"});
            this.algoritmoCirculo.Location = new System.Drawing.Point(5, 149);
            this.algoritmoCirculo.Name = "algoritmoCirculo";
            this.algoritmoCirculo.Size = new System.Drawing.Size(94, 26);
            this.algoritmoCirculo.TabIndex = 6;
            // 
            // btnFuente
            // 
            this.btnFuente.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFuente.Location = new System.Drawing.Point(5, 181);
            this.btnFuente.Name = "btnFuente";
            this.btnFuente.Size = new System.Drawing.Size(94, 25);
            this.btnFuente.TabIndex = 7;
            this.btnFuente.Text = "Fuente...";
            // 
            // grillaHerramientas
            // 
            this.grillaHerramientas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(226)))), ((int)(((byte)(186)))));
            this.grillaHerramientas.ColumnCount = 2;
            this.grillaHerramientas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.grillaHerramientas.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.grillaHerramientas.Controls.Add(this.btnBorrador, 0, 0);
            this.grillaHerramientas.Controls.Add(this.btnLinea, 1, 0);
            this.grillaHerramientas.Controls.Add(this.btnCurva, 0, 1);
            this.grillaHerramientas.Controls.Add(this.btnPincel, 1, 1);
            this.grillaHerramientas.Controls.Add(this.btnRelleno, 0, 2);
            this.grillaHerramientas.Controls.Add(this.btnLapiz, 1, 2);
            this.grillaHerramientas.Controls.Add(this.btnTexto, 0, 3);
            this.grillaHerramientas.Controls.Add(this.btnPoligono, 1, 3);
            this.grillaHerramientas.Controls.Add(this.btnRectangulo, 0, 4);
            this.grillaHerramientas.Controls.Add(this.btnSeleccionLibre, 1, 4);
            this.grillaHerramientas.Controls.Add(this.btnSeleccion, 0, 5);
            this.grillaHerramientas.Controls.Add(this.btnRectanguloRedondeado, 1, 5);
            this.grillaHerramientas.Controls.Add(this.btnZoom, 0, 6);
            this.grillaHerramientas.Controls.Add(this.btnLimpiar, 1, 6);
            this.grillaHerramientas.Controls.Add(this.btnElipse, 0, 7);
            this.grillaHerramientas.Dock = System.Windows.Forms.DockStyle.Top;
            this.grillaHerramientas.Location = new System.Drawing.Point(5, 5);
            this.grillaHerramientas.Name = "grillaHerramientas";
            this.grillaHerramientas.RowCount = 8;
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.grillaHerramientas.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 51F));
            this.grillaHerramientas.Size = new System.Drawing.Size(165, 408);
            this.grillaHerramientas.TabIndex = 1;
            // 
            // btnBorrador
            // 
            this.btnBorrador.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnBorrador.Location = new System.Drawing.Point(2, 2);
            this.btnBorrador.Margin = new System.Windows.Forms.Padding(2);
            this.btnBorrador.Name = "btnBorrador";
            this.btnBorrador.Size = new System.Drawing.Size(78, 47);
            this.btnBorrador.TabIndex = 0;
            this.btnBorrador.TabStop = false;
            // 
            // btnLinea
            // 
            this.btnLinea.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLinea.Location = new System.Drawing.Point(84, 2);
            this.btnLinea.Margin = new System.Windows.Forms.Padding(2);
            this.btnLinea.Name = "btnLinea";
            this.btnLinea.Size = new System.Drawing.Size(79, 47);
            this.btnLinea.TabIndex = 1;
            this.btnLinea.TabStop = false;
            // 
            // btnCurva
            // 
            this.btnCurva.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnCurva.Location = new System.Drawing.Point(2, 53);
            this.btnCurva.Margin = new System.Windows.Forms.Padding(2);
            this.btnCurva.Name = "btnCurva";
            this.btnCurva.Size = new System.Drawing.Size(78, 47);
            this.btnCurva.TabIndex = 2;
            this.btnCurva.TabStop = false;
            // 
            // btnPincel
            // 
            this.btnPincel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPincel.Location = new System.Drawing.Point(84, 53);
            this.btnPincel.Margin = new System.Windows.Forms.Padding(2);
            this.btnPincel.Name = "btnPincel";
            this.btnPincel.Size = new System.Drawing.Size(79, 47);
            this.btnPincel.TabIndex = 3;
            this.btnPincel.TabStop = false;
            // 
            // btnRelleno
            // 
            this.btnRelleno.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRelleno.Location = new System.Drawing.Point(2, 104);
            this.btnRelleno.Margin = new System.Windows.Forms.Padding(2);
            this.btnRelleno.Name = "btnRelleno";
            this.btnRelleno.Size = new System.Drawing.Size(78, 47);
            this.btnRelleno.TabIndex = 4;
            this.btnRelleno.TabStop = false;
            // 
            // btnLapiz
            // 
            this.btnLapiz.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLapiz.Location = new System.Drawing.Point(84, 104);
            this.btnLapiz.Margin = new System.Windows.Forms.Padding(2);
            this.btnLapiz.Name = "btnLapiz";
            this.btnLapiz.Size = new System.Drawing.Size(79, 47);
            this.btnLapiz.TabIndex = 5;
            this.btnLapiz.TabStop = false;
            // 
            // btnTexto
            // 
            this.btnTexto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnTexto.Location = new System.Drawing.Point(2, 155);
            this.btnTexto.Margin = new System.Windows.Forms.Padding(2);
            this.btnTexto.Name = "btnTexto";
            this.btnTexto.Size = new System.Drawing.Size(78, 47);
            this.btnTexto.TabIndex = 6;
            this.btnTexto.TabStop = false;
            // 
            // btnPoligono
            // 
            this.btnPoligono.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnPoligono.Location = new System.Drawing.Point(84, 155);
            this.btnPoligono.Margin = new System.Windows.Forms.Padding(2);
            this.btnPoligono.Name = "btnPoligono";
            this.btnPoligono.Size = new System.Drawing.Size(79, 47);
            this.btnPoligono.TabIndex = 7;
            this.btnPoligono.TabStop = false;
            // 
            // btnRectangulo
            // 
            this.btnRectangulo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRectangulo.Location = new System.Drawing.Point(2, 206);
            this.btnRectangulo.Margin = new System.Windows.Forms.Padding(2);
            this.btnRectangulo.Name = "btnRectangulo";
            this.btnRectangulo.Size = new System.Drawing.Size(78, 47);
            this.btnRectangulo.TabIndex = 8;
            this.btnRectangulo.TabStop = false;
            // 
            // btnSeleccionLibre
            // 
            this.btnSeleccionLibre.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSeleccionLibre.Location = new System.Drawing.Point(84, 206);
            this.btnSeleccionLibre.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccionLibre.Name = "btnSeleccionLibre";
            this.btnSeleccionLibre.Size = new System.Drawing.Size(79, 47);
            this.btnSeleccionLibre.TabIndex = 9;
            this.btnSeleccionLibre.TabStop = false;
            // 
            // btnSeleccion
            // 
            this.btnSeleccion.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnSeleccion.Location = new System.Drawing.Point(2, 257);
            this.btnSeleccion.Margin = new System.Windows.Forms.Padding(2);
            this.btnSeleccion.Name = "btnSeleccion";
            this.btnSeleccion.Size = new System.Drawing.Size(78, 47);
            this.btnSeleccion.TabIndex = 10;
            this.btnSeleccion.TabStop = false;
            // 
            // btnRectanguloRedondeado
            // 
            this.btnRectanguloRedondeado.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRectanguloRedondeado.Location = new System.Drawing.Point(84, 257);
            this.btnRectanguloRedondeado.Margin = new System.Windows.Forms.Padding(2);
            this.btnRectanguloRedondeado.Name = "btnRectanguloRedondeado";
            this.btnRectanguloRedondeado.Size = new System.Drawing.Size(79, 47);
            this.btnRectanguloRedondeado.TabIndex = 11;
            this.btnRectanguloRedondeado.TabStop = false;
            // 
            // btnZoom
            // 
            this.btnZoom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnZoom.Location = new System.Drawing.Point(2, 308);
            this.btnZoom.Margin = new System.Windows.Forms.Padding(2);
            this.btnZoom.Name = "btnZoom";
            this.btnZoom.Size = new System.Drawing.Size(78, 47);
            this.btnZoom.TabIndex = 12;
            this.btnZoom.TabStop = false;
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLimpiar.Location = new System.Drawing.Point(84, 308);
            this.btnLimpiar.Margin = new System.Windows.Forms.Padding(2);
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(79, 47);
            this.btnLimpiar.TabIndex = 13;
            this.btnLimpiar.TabStop = false;
            // 
            // btnElipse
            // 
            this.btnElipse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnElipse.Location = new System.Drawing.Point(2, 359);
            this.btnElipse.Margin = new System.Windows.Forms.Padding(2);
            this.btnElipse.Name = "btnElipse";
            this.btnElipse.Size = new System.Drawing.Size(78, 47);
            this.btnElipse.TabIndex = 14;
            this.btnElipse.TabStop = false;
            // 
            // btnColorPersonalizado
            // 
            this.btnColorPersonalizado.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnColorPersonalizado.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnColorPersonalizado.Location = new System.Drawing.Point(880, 10);
            this.btnColorPersonalizado.Name = "btnColorPersonalizado";
            this.btnColorPersonalizado.Size = new System.Drawing.Size(110, 28);
            this.btnColorPersonalizado.TabIndex = 3;
            this.btnColorPersonalizado.Text = "Mas colores...";
            // 
            // panelPaleta
            // 
            this.panelPaleta.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(226)))), ((int)(((byte)(186)))));
            this.panelPaleta.Controls.Add(this.muestraLinea);
            this.panelPaleta.Controls.Add(this.muestraRelleno);
            this.panelPaleta.Controls.Add(this.lblColoresActuales);
            this.panelPaleta.Controls.Add(this.flujoPaleta);
            this.panelPaleta.Controls.Add(this.btnColorPersonalizado);
            this.panelPaleta.Controls.Add(this.zoom);
            this.panelPaleta.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelPaleta.Location = new System.Drawing.Point(0, 682);
            this.panelPaleta.Name = "panelPaleta";
            this.panelPaleta.Size = new System.Drawing.Size(1180, 52);
            this.panelPaleta.TabIndex = 2;
            // 
            // muestraLinea
            // 
            this.muestraLinea.BackColor = System.Drawing.Color.Black;
            this.muestraLinea.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.muestraLinea.Location = new System.Drawing.Point(8, 8);
            this.muestraLinea.Name = "muestraLinea";
            this.muestraLinea.Size = new System.Drawing.Size(25, 25);
            this.muestraLinea.TabIndex = 0;
            // 
            // muestraRelleno
            // 
            this.muestraRelleno.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(216)))), ((int)(((byte)(132)))));
            this.muestraRelleno.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.muestraRelleno.Location = new System.Drawing.Point(24, 20);
            this.muestraRelleno.Name = "muestraRelleno";
            this.muestraRelleno.Size = new System.Drawing.Size(25, 25);
            this.muestraRelleno.TabIndex = 1;
            // 
            // lblColoresActuales
            // 
            this.lblColoresActuales.Location = new System.Drawing.Point(54, 7);
            this.lblColoresActuales.Name = "lblColoresActuales";
            this.lblColoresActuales.Size = new System.Drawing.Size(105, 38);
            this.lblColoresActuales.TabIndex = 2;
            this.lblColoresActuales.Text = "Linea (frente)\r\nRelleno (fondo)";
            // 
            // flujoPaleta
            // 
            this.flujoPaleta.Location = new System.Drawing.Point(165, 7);
            this.flujoPaleta.Name = "flujoPaleta";
            this.flujoPaleta.Size = new System.Drawing.Size(610, 38);
            this.flujoPaleta.TabIndex = 2;
            this.flujoPaleta.WrapContents = false;
            // 
            // zoom
            // 
            this.zoom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.zoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.zoom.Items.AddRange(new object[] {
            "25 %",
            "50 %",
            "100 %",
            "200 %",
            "400 %"});
            this.zoom.Location = new System.Drawing.Point(1000, 10);
            this.zoom.Name = "zoom";
            this.zoom.Size = new System.Drawing.Size(80, 26);
            this.zoom.TabIndex = 4;
            // 
            // panelLienzo
            // 
            this.panelLienzo.AutoScroll = true;
            this.panelLienzo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(190)))), ((int)(((byte)(174)))), ((int)(((byte)(145)))));
            this.panelLienzo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelLienzo.Controls.Add(this.lienzoControl);
            this.panelLienzo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelLienzo.Location = new System.Drawing.Point(219, 48);
            this.panelLienzo.Name = "panelLienzo";
            this.panelLienzo.Padding = new System.Windows.Forms.Padding(10);
            this.panelLienzo.Size = new System.Drawing.Size(905, 805);
            this.panelLienzo.TabIndex = 0;
            // 
            // lienzoControl
            // 
            this.lienzoControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(253)))), ((int)(((byte)(245)))));
            this.lienzoControl.Cursor = System.Windows.Forms.Cursors.Cross;
            this.lienzoControl.Location = new System.Drawing.Point(10, 10);
            this.lienzoControl.Name = "lienzoControl";
            this.lienzoControl.Size = new System.Drawing.Size(1200, 800);
            this.lienzoControl.TabIndex = 0;
            // 
            // panelTransformaciones
            // 
            this.panelTransformaciones.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(226)))), ((int)(((byte)(186)))));
            this.panelTransformaciones.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelTransformaciones.Controls.Add(this.lblTituloTransformaciones);
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
            this.panelTransformaciones.Location = new System.Drawing.Point(1194, 48);
            this.panelTransformaciones.Name = "panelTransformaciones";
            this.panelTransformaciones.Size = new System.Drawing.Size(281, 805);
            this.panelTransformaciones.TabIndex = 1;
            // 
            // lblTituloTransformaciones
            // 
            this.lblTituloTransformaciones.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);
            this.lblTituloTransformaciones.Location = new System.Drawing.Point(10, 14);
            this.lblTituloTransformaciones.Name = "lblTituloTransformaciones";
            this.lblTituloTransformaciones.Size = new System.Drawing.Size(156, 24);
            this.lblTituloTransformaciones.TabIndex = 0;
            this.lblTituloTransformaciones.Text = "Transformaciones";
            // 
            // lblTraslacionX
            // 
            this.lblTraslacionX.Location = new System.Drawing.Point(10, 55);
            this.lblTraslacionX.Name = "lblTraslacionX";
            this.lblTraslacionX.Size = new System.Drawing.Size(62, 24);
            this.lblTraslacionX.TabIndex = 1;
            this.lblTraslacionX.Text = "Mover X:";
            // 
            // traslacionX
            // 
            this.traslacionX.Location = new System.Drawing.Point(78, 52);
            this.traslacionX.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.traslacionX.Minimum = new decimal(new int[] {
            4000,
            0,
            0,
            -2147483648});
            this.traslacionX.Name = "traslacionX";
            this.traslacionX.Size = new System.Drawing.Size(82, 24);
            this.traslacionX.TabIndex = 2;
            // 
            // lblTraslacionY
            // 
            this.lblTraslacionY.Location = new System.Drawing.Point(10, 87);
            this.lblTraslacionY.Name = "lblTraslacionY";
            this.lblTraslacionY.Size = new System.Drawing.Size(62, 24);
            this.lblTraslacionY.TabIndex = 3;
            this.lblTraslacionY.Text = "Mover Y:";
            // 
            // traslacionY
            // 
            this.traslacionY.Location = new System.Drawing.Point(78, 84);
            this.traslacionY.Maximum = new decimal(new int[] {
            4000,
            0,
            0,
            0});
            this.traslacionY.Minimum = new decimal(new int[] {
            4000,
            0,
            0,
            -2147483648});
            this.traslacionY.Name = "traslacionY";
            this.traslacionY.Size = new System.Drawing.Size(82, 24);
            this.traslacionY.TabIndex = 4;
            // 
            // btnAplicarTraslacion
            // 
            this.btnAplicarTraslacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarTraslacion.Location = new System.Drawing.Point(12, 120);
            this.btnAplicarTraslacion.Name = "btnAplicarTraslacion";
            this.btnAplicarTraslacion.Size = new System.Drawing.Size(148, 30);
            this.btnAplicarTraslacion.TabIndex = 5;
            this.btnAplicarTraslacion.Text = "Aplicar traslacion";
            // 
            // lblRotacion
            // 
            this.lblRotacion.Location = new System.Drawing.Point(10, 176);
            this.lblRotacion.Name = "lblRotacion";
            this.lblRotacion.Size = new System.Drawing.Size(62, 24);
            this.lblRotacion.TabIndex = 6;
            this.lblRotacion.Text = "Grados:";
            // 
            // rotacionGrados
            // 
            this.rotacionGrados.Location = new System.Drawing.Point(78, 173);
            this.rotacionGrados.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.rotacionGrados.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.rotacionGrados.Name = "rotacionGrados";
            this.rotacionGrados.Size = new System.Drawing.Size(82, 24);
            this.rotacionGrados.TabIndex = 7;
            this.rotacionGrados.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            // 
            // btnAplicarRotacion
            // 
            this.btnAplicarRotacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarRotacion.Location = new System.Drawing.Point(12, 208);
            this.btnAplicarRotacion.Name = "btnAplicarRotacion";
            this.btnAplicarRotacion.Size = new System.Drawing.Size(148, 30);
            this.btnAplicarRotacion.TabIndex = 8;
            this.btnAplicarRotacion.Text = "Aplicar rotacion";
            // 
            // lblEscalaX
            // 
            this.lblEscalaX.Location = new System.Drawing.Point(10, 270);
            this.lblEscalaX.Name = "lblEscalaX";
            this.lblEscalaX.Size = new System.Drawing.Size(62, 24);
            this.lblEscalaX.TabIndex = 9;
            this.lblEscalaX.Text = "Escala X:";
            // 
            // escalaX
            // 
            this.escalaX.Location = new System.Drawing.Point(78, 267);
            this.escalaX.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.escalaX.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.escalaX.Name = "escalaX";
            this.escalaX.Size = new System.Drawing.Size(82, 24);
            this.escalaX.TabIndex = 10;
            this.escalaX.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // lblEscalaY
            // 
            this.lblEscalaY.Location = new System.Drawing.Point(10, 302);
            this.lblEscalaY.Name = "lblEscalaY";
            this.lblEscalaY.Size = new System.Drawing.Size(62, 24);
            this.lblEscalaY.TabIndex = 11;
            this.lblEscalaY.Text = "Escala Y:";
            // 
            // escalaY
            // 
            this.escalaY.Location = new System.Drawing.Point(78, 299);
            this.escalaY.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.escalaY.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.escalaY.Name = "escalaY";
            this.escalaY.Size = new System.Drawing.Size(82, 24);
            this.escalaY.TabIndex = 12;
            this.escalaY.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // btnAplicarEscala
            // 
            this.btnAplicarEscala.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAplicarEscala.Location = new System.Drawing.Point(12, 336);
            this.btnAplicarEscala.Name = "btnAplicarEscala";
            this.btnAplicarEscala.Size = new System.Drawing.Size(148, 30);
            this.btnAplicarEscala.TabIndex = 13;
            this.btnAplicarEscala.Text = "Aplicar escala";
            // 
            // MichiPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(226)))), ((int)(((byte)(186)))));
            this.ClientSize = new System.Drawing.Size(1180, 760);
            this.Controls.Add(this.panelLienzo);
            this.Controls.Add(this.panelTransformaciones);
            this.Controls.Add(this.panelHerramientas);
            this.Controls.Add(this.panelPaleta);
            this.Controls.Add(this.barraEstado);
            this.Controls.Add(this.menu);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F);
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
            ((System.ComponentModel.ISupportInitialize)(this.grosor)).EndInit();
            this.grillaHerramientas.ResumeLayout(false);
            this.panelPaleta.ResumeLayout(false);
            this.panelLienzo.ResumeLayout(false);
            this.panelTransformaciones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.traslacionX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.traslacionY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rotacionGrados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.escalaX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.escalaY)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
