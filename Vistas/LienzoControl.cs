using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using Paint_Bolaños_Flores_Venegas.Nucleo;

namespace Paint_Bolaños_Flores_Venegas.Vistas
{
    public sealed class LienzoControl : Control
    {
        private Bitmap imagen;
        private DocumentoDibujo documento;
        private GestorHerramientas gestor;
        private float zoom = 1f;

        public event Action<PointF> CoordenadaCambio;

        public LienzoControl()
        {
            DoubleBuffered = true;

            SetStyle(
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint,
                true);

            BackColor = Color.FromArgb(255, 253, 245);
            Cursor = Cursors.Cross;
        }

        public void Configurar(DocumentoDibujo doc, GestorHerramientas nuevoGestor)
        {
            if (documento != null)
            {
                documento.Cambio -= DocumentoCambio;
            }

            if (gestor != null)
            {
                gestor.CambioVisual -= GestorCambio;
            }

            documento = doc;
            gestor = nuevoGestor;

            documento.Cambio += DocumentoCambio;
            gestor.CambioVisual += GestorCambio;
            gestor.ToleranciaInteraccion = 9f / zoom;

            ActualizarTamano();
            ActualizarImagen();
        }

        public void EstablecerZoom(float valor)
        {
            zoom = Math.Max(.25f, Math.Min(4f, valor));

            if (gestor != null)
            {
                gestor.ToleranciaInteraccion = 9f / zoom;
            }

            ActualizarTamano();
            Invalidate();
        }

        public void ActualizarImagen()
        {
            if (documento == null || gestor == null)
            {
                return;
            }

            imagen?.Dispose();
            imagen = gestor.Rasterizador.Renderizar(documento, gestor.VistaPrevia);
            Invalidate();
        }

        private void DocumentoCambio(object sender, EventArgs e)
        {
            ActualizarImagen();
        }

        private void GestorCambio(object sender, EventArgs e)
        {
            ActualizarImagen();
        }

        private void ActualizarTamano()
        {
            if (documento == null)
            {
                return;
            }

            Size = new Size(
                Math.Max(1, (int)(documento.Ancho * zoom)),
                Math.Max(1, (int)(documento.Alto * zoom)));
        }

        private PointF ADocumento(Point punto)
        {
            return new PointF(punto.X / zoom, punto.Y / zoom);
        }

        private PointF APantalla(PointF punto)
        {
            return new PointF(punto.X * zoom, punto.Y * zoom);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (imagen == null)
            {
                return;
            }

            e.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
            e.Graphics.DrawImage(
                imagen,
                ClientRectangle,
                new Rectangle(0, 0, imagen.Width, imagen.Height),
                GraphicsUnit.Pixel);

            DibujarSeleccion(e.Graphics);
            DibujarLazo(e.Graphics);
            DibujarInicioPoligono(e.Graphics);
            DibujarPuntosCurva(e.Graphics);
        }

        private void DibujarSeleccion(Graphics g)
        {
            if (gestor == null || gestor.Seleccion.Count == 0)
            {
                return;
            }

            var marco = gestor.MarcoSeleccion();
            if (marco.Count < 4)
            {
                return;
            }

            var pantalla = marco.Select(APantalla).ToArray();

            using (var pen = new Pen(Color.FromArgb(131, 94, 72), 1) { DashStyle = DashStyle.Dash })
            {
                g.DrawPolygon(pen, pantalla);
            }

            DibujarTirador(g, APantalla(gestor.PuntoTiradorEscala()), Color.FromArgb(228, 169, 168));

            PointF rotacion = APantalla(gestor.PuntoTiradorRotacion());
            PointF anclaje = new PointF(
                (pantalla[0].X + pantalla[1].X) / 2f,
                (pantalla[0].Y + pantalla[1].Y) / 2f);

            using (var pen = new Pen(Color.FromArgb(131, 94, 72)))
            {
                g.DrawLine(pen, anclaje, rotacion);
            }

            DibujarTirador(g, rotacion, Color.FromArgb(241, 216, 132));
        }

        private static void DibujarTirador(Graphics g, PointF punto, Color color)
        {
            using (var brocha = new SolidBrush(color))
            using (var pen = new Pen(Color.FromArgb(131, 94, 72)))
            {
                var rectangulo = new RectangleF(punto.X - 4, punto.Y - 4, 8, 8);
                g.FillRectangle(brocha, rectangulo);
                g.DrawRectangle(pen, rectangulo.X, rectangulo.Y, rectangulo.Width, rectangulo.Height);
            }
        }

        private void DibujarLazo(Graphics g)
        {
            if (gestor == null || gestor.Lazo.Count < 2)
            {
                return;
            }

            using (var pen = new Pen(Color.FromArgb(131, 94, 72), 1) { DashStyle = DashStyle.Dot })
            {
                for (int i = 1; i < gestor.Lazo.Count; i++)
                {
                    g.DrawLine(pen, APantalla(gestor.Lazo[i - 1]), APantalla(gestor.Lazo[i]));
                }
            }
        }

        private void DibujarInicioPoligono(Graphics g)
        {
            bool puedeDibujar =
                gestor != null &&
                gestor.HerramientaActual == HerramientaTipo.Poligono &&
                gestor.FormaActual == FormaPersonalizada.Poligono &&
                gestor.PuntoInicialPoligono.HasValue;

            if (!puedeDibujar)
            {
                return;
            }

            PointF punto = APantalla(gestor.PuntoInicialPoligono.Value);

            using (var brocha = new SolidBrush(Color.FromArgb(241, 216, 132)))
            using (var pen = new Pen(Color.FromArgb(131, 94, 72), 2))
            {
                var rectangulo = new RectangleF(punto.X - 5, punto.Y - 5, 10, 10);
                g.FillEllipse(brocha, rectangulo);
                g.DrawEllipse(pen, rectangulo);
            }
        }

        private void DibujarPuntosCurva(Graphics g)
        {
            if (gestor == null || !gestor.ConstruyendoCurva)
            {
                return;
            }

            var puntos = gestor.PuntosCurvaEnConstruccion.Select(APantalla).ToArray();

            if (puntos.Length > 1)
            {
                using (var guia = new Pen(Color.FromArgb(131, 94, 72), 1) { DashStyle = DashStyle.Dot })
                {
                    g.DrawLines(guia, puntos);
                }
            }

            using (var fondo = new SolidBrush(Color.FromArgb(241, 216, 132)))
            using (var borde = new Pen(Color.FromArgb(131, 94, 72), 1))
            {
                for (int i = 0; i < puntos.Length; i++)
                {
                    var rectangulo = new RectangleF(puntos[i].X - 4, puntos[i].Y - 4, 8, 8);
                    g.FillRectangle(fondo, rectangulo);
                    g.DrawRectangle(borde, rectangulo.X, rectangulo.Y, rectangulo.Width, rectangulo.Height);
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left)
            {
                return;
            }

            Focus();
            gestor?.Iniciar(ADocumento(e.Location));
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            var punto = ADocumento(e.Location);
            CoordenadaCambio?.Invoke(punto);

            bool debeMover =
                e.Button == MouseButtons.Left ||
                (gestor?.HerramientaActual == HerramientaTipo.Poligono && gestor.EditandoFiguraReciente == false) ||
                gestor?.ConstruyendoCurva == true;

            if (debeMover)
            {
                gestor?.Mover(punto);
            }
            else
            {
                ActualizarCursor(punto);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button == MouseButtons.Left)
            {
                gestor?.Terminar(ADocumento(e.Location));
            }
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);

            if (e.Button == MouseButtons.Left)
            {
                gestor?.DobleClic(ADocumento(e.Location));
            }
        }

        private void ActualizarCursor(PointF punto)
        {
            bool puedeEditar =
                gestor != null &&
                (gestor.EditandoFiguraReciente ||
                 gestor.HerramientaActual == HerramientaTipo.Seleccion ||
                 gestor.HerramientaActual == HerramientaTipo.SeleccionLibre) &&
                gestor.Seleccion.Count > 0;

            if (!puedeEditar)
            {
                Cursor = Cursors.Cross;
                return;
            }

            RectangleF limites = gestor.LimitesSeleccion();
            PointF escala = gestor.PuntoTiradorEscala();
            PointF rotacion = gestor.PuntoTiradorRotacion();

            if (LineaFigura.Distancia(punto, rotacion) <= gestor.ToleranciaInteraccion)
            {
                Cursor = Cursors.Hand;
            }
            else if (LineaFigura.Distancia(punto, escala) <= gestor.ToleranciaInteraccion)
            {
                Cursor = Cursors.SizeNWSE;
            }
            else if (limites.Contains(punto))
            {
                Cursor = Cursors.SizeAll;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                imagen?.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
