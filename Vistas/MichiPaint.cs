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
        private DocumentoDibujo documento;
        private HistorialComandos historial;
        private RasterizadorDocumento rasterizador;
        private GestorHerramientas gestor;
        private readonly GestorProyecto gestorProyecto = new GestorProyecto();
        private readonly ExportadorImagen exportador = new ExportadorImagen();
        private readonly Dictionary<HerramientaTipo, Button> botones = new Dictionary<HerramientaTipo, Button>();
        private string rutaProyecto;
        private Font fuenteTexto = new Font("Microsoft Sans Serif", 12);

        public MichiPaint()
        {
            InitializeComponent();
            ConfigurarInterfaz();
            NuevoDocumento(1200, 800);
        }

        private void ConfigurarInterfaz()
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
            botones.Add(HerramientaTipo.Zoom, btnZoom); botones.Add(HerramientaTipo.Elipse, btnElipse);
            ConfigurarBoton(btnBorrador,HerramientaTipo.Borrador,"1.png","Borrador"); 
            ConfigurarBoton(btnLinea,HerramientaTipo.Linea,"2.png","Línea"); 
            ConfigurarBoton(btnCurva,HerramientaTipo.Curva,"3.png","Curva Bézier"); 
            ConfigurarBoton(btnPincel,HerramientaTipo.Pincel,"4.png","Pincel");
            ConfigurarBoton(btnRelleno,HerramientaTipo.Relleno,"5.png","Balde de relleno");
            ConfigurarBoton(btnLapiz,HerramientaTipo.Lapiz,"6.png","Lápiz");
            ConfigurarBoton(btnTexto,HerramientaTipo.Texto,"7.png","Texto"); 
            ConfigurarBoton(btnPoligono,HerramientaTipo.Poligono,"8.png","Polígono y figuras");
            ConfigurarBoton(btnRectangulo,HerramientaTipo.Rectangulo,"9.png","Rectángulo"); 
            ConfigurarBoton(btnSeleccionLibre,HerramientaTipo.SeleccionLibre,"10.png","Selección libre"); 
            ConfigurarBoton(btnSeleccion,HerramientaTipo.Seleccion,"11.png","Seleccionar"); 
            ConfigurarBoton(btnRectanguloRedondeado,HerramientaTipo.RectanguloRedondeado,"12.png","Rectángulo redondeado");
            ConfigurarBoton(btnZoom,HerramientaTipo.Zoom,"13.png","Zoom"); 
            ConfigurarBoton(btnElipse,HerramientaTipo.Elipse,"15.png","Círculo o elipse");
            btnLimpiar.BackColor=Crema;btnLimpiar.ForeColor=Marron;btnLimpiar.FlatStyle=FlatStyle.Flat;
            btnLimpiar.FlatAppearance.BorderColor=Marron;btnLimpiar.FlatAppearance.MouseOverBackColor=Amarillo;
            btnLimpiar.Image=CargarIcono("14.png",38);btnLimpiar.ImageAlign=ContentAlignment.MiddleCenter;
            ayuda.SetToolTip(btnLimpiar,"Limpiar lienzo");btnLimpiar.Click+=(s,e)=>Limpiar();
            btnPoligono.MouseUp+=(s,e)=>{if(e.Button==MouseButtons.Right)MostrarGaleria(btnPoligono);};
            btnFuente.BackColor=Amarillo;
            btnFuente.ForeColor=Marron;
            btnFuente.FlatAppearance.BorderColor=Marron;
            btnFuente.Click+=(s,e)=>ElegirFuente();
            foreach(Control c in panelOpciones.Controls)c.ForeColor=Marron;
            lblColoresActuales.ForeColor=Marron;
            CrearMuestrasColor();
            ConfigurarMenusEIconos();
            ConfigurarTransformacionesYColor();
            grosor.ValueChanged+=(s,e)=>ActualizarEstilo(); 
            usarRelleno.CheckedChanged+=(s,e)=>ActualizarEstilo();
            algoritmoLinea.SelectedIndexChanged+=(s,e)=>{if(gestor!=null)gestor.AlgoritmoLinea=(AlgoritmoLineaTipo)algoritmoLinea.SelectedIndex;};
            algoritmoCirculo.SelectedIndexChanged+=(s,e)=>{if(gestor!=null)gestor.AlgoritmoCirculo=(AlgoritmoCirculoTipo)algoritmoCirculo.SelectedIndex;};
            zoom.SelectedIndexChanged+=(s,e)=>EstablecerZoom(new[]{25,50,100,200,400}[zoom.SelectedIndex]);
            lienzoControl.CoordenadaCambio+=p=>estadoCoordenadas.Text=$"X: {(int)p.X}  Y: {(int)p.Y}";
            FormClosing+=AlCerrar;
            KeyDown+=TeclaPresionada;
        }

        private void ConfigurarMenusEIconos()
        {
            itemNuevo.Image=CargarIcono("nuevo_archivo_pixel.png",20);
            itemAbrir.Image=CargarIcono("16.png",20);
            itemGuardar.Image=CargarIcono("guardar_pixel.png",20);
            itemGuardarComo.Image=CargarIcono("guardar_como_pixel.png",20);
            itemExportar.Image=CargarIcono("guardar_exportar_pixel.png",20);
            itemLimpiar.Image=CargarIcono("14.png",20);
            itemNuevo.Click+=(s,e)=>Nuevo();
            itemAbrir.Click+=(s,e)=>Abrir();
            itemGuardar.Click+=(s,e)=>Guardar();
            itemGuardarComo.Click+=(s,e)=>GuardarComo();
            itemExportar.Click+=(s,e)=>Exportar();
            itemSalir.Click+=(s,e)=>Close();
            itemDeshacer.Click+=(s,e)=>historial?.Deshacer();
            itemRehacer.Click+=(s,e)=>historial?.Rehacer();
            itemEliminar.Click+=(s,e)=>gestor?.EliminarSeleccion();
            itemLimpiar.Click+=(s,e)=>Limpiar();
            itemZoom25.Click+=(s,e)=>EstablecerZoom(25);
            itemZoom50.Click+=(s,e)=>EstablecerZoom(50);
            itemZoom100.Click+=(s,e)=>EstablecerZoom(100);
            itemZoom200.Click+=(s,e)=>EstablecerZoom(200);
            itemZoom400.Click+=(s,e)=>EstablecerZoom(400);
            itemControles.Click+=(s,e)=>MostrarAyuda();
            itemAcerca.Click+=(s,e)=>MostrarAcerca();
            ConfigurarRapido(rapidoGuardar,"guardar_pixel.png","Guardar",(s,e)=>Guardar());
            ConfigurarRapido(rapidoExportar,"guardar_exportar_pixel.png","Exportar PNG",(s,e)=>Exportar());
            ConfigurarRapido(rapidoDeshacer,"deshacer_pixel.png","↶",(s,e)=>historial?.Deshacer());
            ConfigurarRapido(rapidoRehacer,"rehacer_pixel.png","↷",(s,e)=>historial?.Rehacer());
        }

        private void ConfigurarRapido(ToolStripMenuItem item,string archivo,string alternativa,EventHandler accion)
        {
            var imagen=CargarIcono(archivo,20);if(imagen!=null){item.Image=imagen;item.DisplayStyle=ToolStripItemDisplayStyle.Image;}else{item.Text=alternativa;item.Font=new Font(Font.FontFamily,14,FontStyle.Bold);item.DisplayStyle=ToolStripItemDisplayStyle.Text;}item.Click+=accion;
        }

        private void ConfigurarBoton(Button boton,HerramientaTipo tipo,string archivo,string texto)
        {
            boton.BackColor=Crema;boton.ForeColor=Marron;boton.FlatStyle=FlatStyle.Flat;boton.FlatAppearance.BorderColor=Marron;boton.FlatAppearance.MouseOverBackColor=Amarillo;boton.Image=CargarIcono(archivo,38);boton.ImageAlign=ContentAlignment.MiddleCenter;boton.Padding=Padding.Empty;ayuda.SetToolTip(boton,texto);boton.Click+=(s,e)=>SeleccionarHerramienta(tipo,boton);
        }

        private void CrearMuestrasColor()
        {
            Color[] colores={Marron,Amarillo,Rosa,Crema,Lienzo,Color.Black,Color.Gray,Color.White,Color.Red,Color.Orange,Color.Yellow,Color.Green,Color.Cyan,Color.Blue,Color.Violet,Color.Magenta};
            flujoPaleta.AutoScroll=true;foreach(var color in colores)AgregarMuestraColor(color);
            ayuda.SetToolTip(muestraLinea,"Color de linea actual");ayuda.SetToolTip(muestraRelleno,"Color de relleno actual");
            muestraLinea.Cursor=Cursors.Hand;muestraRelleno.Cursor=Cursors.Hand;muestraLinea.Click+=(s,e)=>ElegirColorPersonalizado(MouseButtons.Left);muestraRelleno.Click+=(s,e)=>ElegirColorPersonalizado(MouseButtons.Right);
        }

        private void AgregarMuestraColor(Color color)
        {
            if(flujoPaleta.Controls.OfType<Button>().Any(x=>x.Tag is int&&(int)x.Tag==color.ToArgb()))return;
            var boton=new Button{BackColor=color,Size=new Size(25,25),Margin=new Padding(1),FlatStyle=FlatStyle.Flat,Tag=color.ToArgb()};boton.FlatAppearance.BorderColor=Marron;boton.MouseDown+=(s,e)=>AsignarColor(((Button)s).BackColor,e.Button);flujoPaleta.Controls.Add(boton);
        }

        private void ConfigurarTransformacionesYColor()
        {
            btnColorPersonalizado.BackColor=Amarillo;btnColorPersonalizado.ForeColor=Marron;btnColorPersonalizado.FlatAppearance.BorderColor=Marron;btnColorPersonalizado.MouseDown+=(s,e)=>ElegirColorPersonalizado(e.Button);ayuda.SetToolTip(btnColorPersonalizado,"Clic izquierdo: color de linea. Clic derecho: color de relleno.");
            panelTransformaciones.ForeColor=Marron;foreach(Control control in panelTransformaciones.Controls)control.ForeColor=Marron;
            foreach(var boton in new[]{btnAplicarTraslacion,btnAplicarRotacion,btnAplicarEscala}){boton.BackColor=Amarillo;boton.FlatAppearance.BorderColor=Marron;boton.FlatAppearance.MouseOverBackColor=Rosa;}
            btnAplicarTraslacion.Click+=(s,e)=>AplicarTraslacion();btnAplicarRotacion.Click+=(s,e)=>AplicarRotacion();btnAplicarEscala.Click+=(s,e)=>AplicarEscala();
        }

        private void ElegirColorPersonalizado(MouseButtons destino)
        {
            Color inicial=destino==MouseButtons.Right?gestor.EstiloActual.ColorRelleno:gestor.EstiloActual.ColorLinea;
            using(var dialogo=new ColorDialog{Color=inicial,FullOpen=true,AnyColor=true})if(dialogo.ShowDialog(this)==DialogResult.OK){AgregarMuestraColor(dialogo.Color);AsignarColor(dialogo.Color,destino==MouseButtons.Right?MouseButtons.Right:MouseButtons.Left);}
        }

        private void AplicarTraslacion()
        {
            if(gestor.Seleccion.Count==0){MostrarSeleccionRequerida();return;}gestor.TrasladarSeleccion((float)traslacionX.Value,(float)traslacionY.Value);
        }

        private void AplicarRotacion()
        {
            if(gestor.Seleccion.Count==0){MostrarSeleccionRequerida();return;}gestor.RotarSeleccion((float)rotacionGrados.Value);
        }

        private void AplicarEscala()
        {
            if(gestor.Seleccion.Count==0){MostrarSeleccionRequerida();return;}gestor.EscalarSeleccion((float)escalaX.Value/100f,(float)escalaY.Value/100f);
        }

        private void MostrarSeleccionRequerida()
        {
            MessageBox.Show(this,"Primero selecciona una figura o un grupo en el lienzo.","Transformaciones",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void NuevoDocumento(int ancho,int alto)
        {
            documento=new DocumentoDibujo(ancho,alto){ColorFondo=Lienzo};historial=new HistorialComandos();rasterizador=new RasterizadorDocumento();gestor=new GestorHerramientas(documento,historial,rasterizador){FuenteTexto=fuenteTexto};
            gestor.TextoSolicitado+=SolicitarTexto;gestor.SeleccionActualizada+=cantidad=>estadoHerramienta.Text=cantidad==0?"Sin seleccion":cantidad==1?"1 figura seleccionada":$"{cantidad} figuras seleccionadas";documento.Cambio+=(s,e)=>ActualizarTitulo();historial.Cambio+=(s,e)=>ActualizarHistorial();lienzoControl.Configurar(documento,gestor);rutaProyecto=null;ActualizarEstilo();SeleccionarHerramienta(HerramientaTipo.Lapiz,btnLapiz);historial.Reiniciar();documento.Modificado=false;ActualizarTitulo();estadoTamano.Text=$"{ancho} × {alto}";
        }

        private void SeleccionarHerramienta(HerramientaTipo tipo,Button boton)
        {
            if(gestor==null)return;gestor.Cancelar();gestor.HerramientaActual=tipo;foreach(var b in botones.Values){b.BackColor=Crema;b.FlatAppearance.BorderSize=2;}boton.BackColor=Rosa;boton.FlatAppearance.BorderSize=3;estadoHerramienta.Text=ayuda.GetToolTip(boton);
            if(tipo==HerramientaTipo.Zoom)zoom.SelectedIndex=zoom.SelectedIndex==zoom.Items.Count-1?2:zoom.SelectedIndex+1;
        }

        private void MostrarGaleria(Control control)
        {
            var cm=new ContextMenuStrip{BackColor=Crema,ForeColor=Marron};foreach(FormaPersonalizada forma in Enum.GetValues(typeof(FormaPersonalizada))){var item=new ToolStripMenuItem(forma==FormaPersonalizada.Poligono?"Polígono libre":forma.ToString());item.Click+=(s,e)=>{gestor.FormaActual=forma;SeleccionarHerramienta(HerramientaTipo.Poligono,btnPoligono);estadoHerramienta.Text=item.Text;};cm.Items.Add(item);}cm.Show(control,new Point(control.Width,0));
        }

        private void ActualizarEstilo(){if(gestor==null)return;gestor.EstiloActual.Grosor=(int)grosor.Value;gestor.EstiloActual.TieneRelleno=usarRelleno.Checked;gestor.AplicarEstiloASeleccion();}
        private void AsignarColor(Color color,MouseButtons boton){if(gestor==null)return;bool aplicadoPorActivacion=false;if(boton==MouseButtons.Right){gestor.EstiloActual.ColorRelleno=color;muestraRelleno.BackColor=color;if(!usarRelleno.Checked){usarRelleno.Checked=true;aplicadoPorActivacion=true;}estadoHerramienta.Text=$"Relleno: #{color.R:X2}{color.G:X2}{color.B:X2}";}else{gestor.EstiloActual.ColorLinea=color;muestraLinea.BackColor=color;estadoHerramienta.Text=$"Linea: #{color.R:X2}{color.G:X2}{color.B:X2}";}if(!aplicadoPorActivacion)gestor.AplicarEstiloASeleccion();}
        private void ElegirFuente(){using(var d=new FontDialog{Font=fuenteTexto})if(d.ShowDialog(this)==DialogResult.OK){fuenteTexto?.Dispose();fuenteTexto=(Font)d.Font.Clone();gestor.FuenteTexto=fuenteTexto;}}
        private void SolicitarTexto(PointF p){var existente=gestor.Seleccion.OfType<TextoFigura>().FirstOrDefault(x=>x.Contiene(p));string texto=DialogoTexto.Mostrar(this,existente?.Texto??"");if(texto!=null)gestor.AgregarTexto(p,texto);}

        private void Nuevo(){
            if(!ConfirmarCambios())return;
            using(var d=new DialogoLienzo())if(d.ShowDialog(this)==DialogResult.OK)NuevoDocumento(d.Ancho,d.Alto);
        }
        private void Abrir(){if(!ConfirmarCambios())return;using(var d=new OpenFileDialog{Filter="Proyecto MichiPaint (*.mpaint)|*.mpaint",Title="Abrir proyecto"})if(d.ShowDialog(this)==DialogResult.OK)try{var nuevo=gestorProyecto.Abrir(d.FileName);NuevoDocumento(nuevo.Ancho,nuevo.Alto);documento.ColorFondo=nuevo.ColorFondo;documento.Reemplazar(nuevo.Figuras);documento.Modificado=false;rutaProyecto=d.FileName;ActualizarTitulo();lienzoControl.ActualizarImagen();}catch(Exception ex){MessageBox.Show(this,ex.Message,"No se pudo abrir",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
        private bool Guardar(){if(string.IsNullOrEmpty(rutaProyecto))return GuardarComo();try{gestorProyecto.Guardar(rutaProyecto,documento);ActualizarTitulo();return true;}catch(Exception ex){MessageBox.Show(this,ex.Message,"No se pudo guardar",MessageBoxButtons.OK,MessageBoxIcon.Error);return false;}}
        private bool GuardarComo(){using(var d=new SaveFileDialog{Filter="Proyecto MichiPaint (*.mpaint)|*.mpaint",DefaultExt="mpaint",AddExtension=true,Title="Guardar proyecto como"})if(d.ShowDialog(this)==DialogResult.OK){rutaProyecto=d.FileName;return Guardar();}return false;}
        private void Exportar(){using(var d=new SaveFileDialog{Filter="Imagen PNG (*.png)|*.png",DefaultExt="png",AddExtension=true,Title="Exportar imagen"})if(d.ShowDialog(this)==DialogResult.OK)try{exportador.ExportarPng(d.FileName,documento,rasterizador);}catch(Exception ex){MessageBox.Show(this,ex.Message,"No se pudo exportar",MessageBoxButtons.OK,MessageBoxIcon.Error);}}
        private void Limpiar(){if(documento.Figuras.Count==0)return;if(MessageBox.Show(this,"¿Limpiar todo el lienzo? Esta acción se puede deshacer.","Limpiar",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes){gestor.LimpiarSeleccion();historial.Ejecutar(new ComandoLimpiar(documento));}}
        private bool ConfirmarCambios(){if(documento==null||!documento.Modificado)return true;var r=MessageBox.Show(this,"Hay cambios sin guardar. ¿Deseas guardarlos?","MichiPaint",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Warning);if(r==DialogResult.Cancel)return false;return r!=DialogResult.Yes||Guardar();}
        private void AlCerrar(object sender,FormClosingEventArgs e){if(!ConfirmarCambios())e.Cancel=true;}
        private void TeclaPresionada(object sender,KeyEventArgs e){if(e.KeyCode==Keys.Escape)gestor?.Cancelar();else if(e.KeyCode==Keys.Delete)gestor?.EliminarSeleccion();else if(e.KeyCode==Keys.Enter&&gestor?.HerramientaActual==HerramientaTipo.Poligono)gestor.DobleClic(Point.Empty);}
        private void EstablecerZoom(int porcentaje){int indice=Array.IndexOf(new[]{25,50,100,200,400},porcentaje);if(indice>=0&&zoom.SelectedIndex!=indice){zoom.SelectedIndex=indice;return;}lienzoControl?.EstablecerZoom(porcentaje/100f);estadoHerramienta.Text=$"Zoom {porcentaje} %";}
        private void ActualizarTitulo(){Text=$"{(documento!=null&&documento.Modificado?"*":"")}{(string.IsNullOrEmpty(rutaProyecto)?"Sin título":Path.GetFileName(rutaProyecto))} - MichiPaint";}
        private void ActualizarHistorial(){bool d=historial?.PuedeDeshacer==true,r=historial?.PuedeRehacer==true;itemDeshacer.Enabled=rapidoDeshacer.Enabled=d;itemRehacer.Enabled=rapidoRehacer.Enabled=r;}
        private void MostrarAyuda(){MessageBox.Show(this,"Dibuja arrastrando sobre el lienzo.\n\nPolígono: clic por vértice y doble clic o Enter para cerrar.\nBézier: marca cuatro puntos.\nSelección: arrastra para mover; usa los tiradores para escalar y rotar.\nClic izquierdo en un color: contorno. Clic derecho: relleno.","Controles de MichiPaint",MessageBoxButtons.OK,MessageBoxIcon.Information);}
        private void MostrarAcerca(){MessageBox.Show(this,"MichiPaint\nProyecto de Computación Gráfica\n\nRasterización, transformaciones y rellenos implementados con algoritmos propios.","Acerca de",MessageBoxButtons.OK,MessageBoxIcon.Information);}

        private Image CargarIcono(string nombre,int tamano)
        {
            if(string.IsNullOrEmpty(nombre))return null;string ruta=Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"Recursos",nombre);if(!File.Exists(ruta))return null;using(var original=Image.FromFile(ruta)){var bmp=new Bitmap(tamano,tamano);using(var g=Graphics.FromImage(bmp)){g.InterpolationMode=InterpolationMode.NearestNeighbor;g.PixelOffsetMode=PixelOffsetMode.Half;g.DrawImage(original,new Rectangle(0,0,tamano,tamano));}return bmp;}
        }
    }

    internal sealed class DialogoLienzo : Form
    {
        private readonly NumericUpDown ancho,alto;public int Ancho=>(int)ancho.Value;public int Alto=>(int)alto.Value;
        public DialogoLienzo(){Text="Nuevo lienzo";FormBorderStyle=FormBorderStyle.FixedDialog;StartPosition=FormStartPosition.CenterParent;ClientSize=new Size(250,145);MaximizeBox=MinimizeBox=false;Controls.Add(new Label{Text="Ancho:",Location=new Point(18,20),AutoSize=true});Controls.Add(new Label{Text="Alto:",Location=new Point(18,55),AutoSize=true});ancho=new NumericUpDown{Minimum=100,Maximum=4000,Value=1200,Location=new Point(90,17),Width=125};alto=new NumericUpDown{Minimum=100,Maximum=4000,Value=800,Location=new Point(90,52),Width=125};Controls.Add(ancho);Controls.Add(alto);var ok=new Button{Text="Crear",DialogResult=DialogResult.OK,Location=new Point(55,100),Width=70};var cancelar=new Button{Text="Cancelar",DialogResult=DialogResult.Cancel,Location=new Point(135,100),Width=70};Controls.Add(ok);Controls.Add(cancelar);AcceptButton=ok;CancelButton=cancelar;}
    }
    internal sealed class DialogoTexto : Form
    {
        private readonly TextBox caja;
        private DialogoTexto(string texto){Text="Texto";StartPosition=FormStartPosition.CenterParent;FormBorderStyle=FormBorderStyle.FixedDialog;ClientSize=new Size(380,175);MaximizeBox=MinimizeBox=false;caja=new TextBox{Multiline=true,Text=texto??"",Location=new Point(12,12),Size=new Size(356,115),ScrollBars=ScrollBars.Vertical};var ok=new Button{Text="Aceptar",DialogResult=DialogResult.OK,Location=new Point(210,137),Width=75};var cancelar=new Button{Text="Cancelar",DialogResult=DialogResult.Cancel,Location=new Point(293,137),Width=75};Controls.Add(caja);Controls.Add(ok);Controls.Add(cancelar);AcceptButton=ok;CancelButton=cancelar;}
        public static string Mostrar(IWin32Window padre,string texto){using(var d=new DialogoTexto(texto))return d.ShowDialog(padre)==DialogResult.OK?d.caja.Text:null;}
    }
}
