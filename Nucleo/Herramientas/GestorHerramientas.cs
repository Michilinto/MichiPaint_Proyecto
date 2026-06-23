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
        private enum TransformacionActiva { Ninguna, Trasladar, Escalar, Rotar }
        private PointF inicio, ultimo;
        private bool presionado;
        private Figura2D previa;
        private readonly List<PointF> puntosTemporales = new List<PointF>();
        private readonly List<PointF> lazo = new List<PointF>();
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
        public PointF? PuntoInicialPoligono => puntosTemporales.Count>0?(PointF?)puntosTemporales[0]:null;
        public IReadOnlyList<PointF> PuntosCurvaEnConstruccion =>
            HerramientaActual == HerramientaTipo.Curva ? puntosTemporales.AsReadOnly() : new List<PointF>().AsReadOnly();
        public bool ConstruyendoCurva => HerramientaActual == HerramientaTipo.Curva && puntosTemporales.Count > 0 && !editandoFiguraReciente;
        public IReadOnlyList<Figura2D> Seleccion => seleccion.AsReadOnly();
        public bool EditandoFiguraReciente=>editandoFiguraReciente;
        public float ToleranciaInteraccion { get; set; } = 9f;
        private readonly List<Figura2D> seleccion = new List<Figura2D>();
        public event EventHandler CambioVisual;
        public event Action<PointF> TextoSolicitado;
        public event Action<int> SeleccionActualizada;

        public GestorHerramientas(DocumentoDibujo documento, HistorialComandos historial, RasterizadorDocumento rasterizador)
        { Documento = documento; Historial = historial; Rasterizador = rasterizador; }

        public void Iniciar(PointF p)
        {
            inicio = ultimo = p; presionado = true; previa = null;
            if(editandoFiguraReciente&&seleccion.Count>0)
            {
                if(EsInteraccionConSeleccion(p)){IniciarSeleccion(p);return;}
                seleccion.Clear();editandoFiguraReciente=false;NotificarSeleccion();Cambio();
            }
            if (HerramientaActual == HerramientaTipo.Seleccion) { IniciarSeleccion(p); return; }
            if (HerramientaActual == HerramientaTipo.SeleccionLibre)
            {
                if(EsInteraccionConSeleccion(p)){IniciarSeleccion(p);return;}
                lazo.Clear();lazo.Add(p);Cambio();return;
            }
            if (HerramientaActual == HerramientaTipo.Relleno) { AplicarRelleno(Point.Round(p)); presionado = false; return; }
            if (HerramientaActual == HerramientaTipo.Texto) { presionado = false; var texto=Documento.Figuras.Reverse().OfType<TextoFigura>().FirstOrDefault(x=>x.Contiene(p)); seleccion.Clear(); if(texto!=null)seleccion.Add(texto); TextoSolicitado?.Invoke(p); return; }
            if (HerramientaActual == HerramientaTipo.Poligono && FormaActual == FormaPersonalizada.Poligono)
            {
                if(puntosTemporales.Count>=3&&LineaFigura.Distancia(puntosTemporales[0],p)<=ToleranciaInteraccion)
                {
                    presionado=false;ConfirmarTemporal();return;
                }
                if(puntosTemporales.Count==0||LineaFigura.Distancia(puntosTemporales[puntosTemporales.Count-1],p)>2f)puntosTemporales.Add(p);
                previa=puntosTemporales.Count>1?new PoligonoFigura(puntosTemporales,EstiloActual){Cerrado=false}:null;presionado=false;Cambio();return;
            }
            if (HerramientaActual == HerramientaTipo.Curva)
            {
                if (puntosTemporales.Count == 0 || LineaFigura.Distancia(puntosTemporales[puntosTemporales.Count - 1], p) > 2f)
                    puntosTemporales.Add(p);

                previa = puntosTemporales.Count > 1
                    ? new BezierFigura(puntosTemporales, EstiloActual)
                    : null;
                presionado = false;

                // Dos puntos forman el grado 1, tres el grado 2 y cuatro
                // el grado 3. El cuarto punto confirma la curva cúbica.
                if (puntosTemporales.Count == 4) ConfirmarTemporal();
                else Cambio();
                return;
            }
            if (HerramientaActual == HerramientaTipo.Lapiz || HerramientaActual == HerramientaTipo.Pincel || HerramientaActual == HerramientaTipo.Borrador)
            {
                var e = EstiloActual.Clonar();
                previa = new TrazoFigura(new[] { p }, e, HerramientaActual == HerramientaTipo.Borrador, HerramientaActual == HerramientaTipo.Pincel); Cambio();
            }
        }

        public void Mover(PointF p)
        {
            if (!presionado && ConstruyendoCurva)
            {
                var puntosVista = new List<PointF>(puntosTemporales);
                if (LineaFigura.Distancia(puntosVista[puntosVista.Count - 1], p) > .5f)
                    puntosVista.Add(p);
                previa = puntosVista.Count > 1 ? new BezierFigura(puntosVista, EstiloActual) : null;
                Cambio();
                return;
            }
            if(!presionado&&HerramientaActual==HerramientaTipo.Poligono&&FormaActual==FormaPersonalizada.Poligono&&puntosTemporales.Count>0)
            {
                var puntosVista=new List<PointF>(puntosTemporales);if(LineaFigura.Distancia(puntosVista[puntosVista.Count-1],p)>.5f)puntosVista.Add(p);
                previa=puntosVista.Count>1?new PoligonoFigura(puntosVista,EstiloActual){Cerrado=false}:null;Cambio();return;
            }
            if (!presionado) return;
            if (transformacion != TransformacionActiva.Ninguna) { TransformarSeleccion(p); ultimo = p; Cambio(); return; }
            if (HerramientaActual == HerramientaTipo.SeleccionLibre) { if (LineaFigura.Distancia(p, ultimo) > 2) lazo.Add(p); ultimo = p; Cambio(); return; }
            var trazo = previa as TrazoFigura; if (trazo != null) { if (LineaFigura.Distancia(p, ultimo) > 1) trazo.AgregarPunto(p); ultimo = p; Cambio(); return; }
            previa = CrearFiguraArrastre(inicio, p); ultimo = p; Cambio();
        }

        public void Terminar(PointF p)
        {
            if (!presionado) return; presionado = false;
            if (transformacion != TransformacionActiva.Ninguna) { FinalizarTransformacion(); return; }
            if (HerramientaActual == HerramientaTipo.SeleccionLibre) { SeleccionarConLazo(); lazo.Clear(); Cambio(); return; }
            if (previa == null) previa = CrearFiguraArrastre(inicio, p);
            if (previa != null) { var creada=previa;Historial.Ejecutar(new ComandoAgregar(Documento, creada));previa=null;if(DebeEditarAlCrear(creada))SeleccionarFiguraReciente(creada); }
            Cambio();
        }

        public void DobleClic(PointF p)
        {
            if (HerramientaActual == HerramientaTipo.Poligono && FormaActual == FormaPersonalizada.Poligono && puntosTemporales.Count >= 3) ConfirmarTemporal();
            else if (HerramientaActual == HerramientaTipo.Texto)
            {
                var t = Documento.Figuras.Reverse().OfType<TextoFigura>().FirstOrDefault(x => x.Contiene(p)); if (t != null) { SeleccionarSolo(t); TextoSolicitado?.Invoke(p); }
            }
        }

        public void Cancelar() { previa = null; puntosTemporales.Clear(); lazo.Clear(); presionado = false; transformacion = TransformacionActiva.Ninguna;editandoFiguraReciente=false;seleccion.Clear();NotificarSeleccion();Cambio(); }

        public void AgregarTexto(PointF p, string texto)
        {
            if (string.IsNullOrWhiteSpace(texto)) return;
            var existente = seleccion.OfType<TextoFigura>().FirstOrDefault(x => x.Contiene(p));
            if (existente != null) { existente.Texto = texto; Documento.Notificar(); }
            else{var figura=new TextoFigura(p,texto,FuenteTexto,EstiloActual.ColorLinea);Historial.Ejecutar(new ComandoAgregar(Documento,figura));SeleccionarFiguraReciente(figura);}
        }

        public void EliminarSeleccion()
        {
            if (seleccion.Count == 0) return; Historial.Ejecutar(new ComandoEliminar(Documento, seleccion)); seleccion.Clear(); NotificarSeleccion(); Cambio();
        }

        public void LimpiarSeleccion() { seleccion.Clear(); NotificarSeleccion(); Cambio(); }

        public RectangleF LimitesSeleccion()
        {
            if (seleccion.Count == 0) return RectangleF.Empty; var r = seleccion[0].ObtenerLimites();
            foreach (var f in seleccion.Skip(1)) r = RectangleF.Union(r, f.ObtenerLimites()); return r;
        }

        public PointF PuntoTiradorRotacion()
        {
            var marco=MarcoSeleccion();if(marco.Count<4)return PointF.Empty;
            PointF medio=new PointF((marco[0].X+marco[1].X)/2f,(marco[0].Y+marco[1].Y)/2f);
            float dx=marco[1].X-marco[0].X,dy=marco[1].Y-marco[0].Y,largo=(float)Math.Sqrt(dx*dx+dy*dy);
            if(largo<.001f){dx=1;dy=0;largo=1;}
            PointF candidato=new PointF(medio.X+dy/largo*18f,medio.Y-dx/largo*18f);
            return candidato.Y>=0?candidato:new PointF(medio.X-dy/largo*18f,medio.Y+dx/largo*18f);
        }

        public PointF PuntoTiradorEscala(){var marco=MarcoSeleccion();return marco.Count>=3?marco[2]:PointF.Empty;}

        public IReadOnlyList<PointF> MarcoSeleccion()
        {
            if(seleccion.Count==1)return seleccion[0].ObtenerMarcoSeleccion();
            RectangleF r=LimitesSeleccion();if(r.IsEmpty)return new List<PointF>();
            return new[]{new PointF(r.Left,r.Top),new PointF(r.Right,r.Top),new PointF(r.Right,r.Bottom),new PointF(r.Left,r.Bottom)};
        }

        private PointF CentroSeleccion(){var marco=MarcoSeleccion();if(marco.Count==0)return PointF.Empty;return new PointF(marco.Average(p=>p.X),marco.Average(p=>p.Y));}

        public void AplicarEstiloASeleccion()
        {
            if(seleccion.Count>0) Historial.Ejecutar(new ComandoEstilo(Documento, seleccion, EstiloActual)); Cambio();
        }

        public bool TrasladarSeleccion(float desplazamientoX, float desplazamientoY)
        {
            if (seleccion.Count == 0 || (Math.Abs(desplazamientoX) < .001f && Math.Abs(desplazamientoY) < .001f)) return false;
            return AplicarTransformacionNumerica((figura, centro) => figura.Trasladar(desplazamientoX, desplazamientoY));
        }

        public bool RotarSeleccion(float grados)
        {
            if (seleccion.Count == 0 || Math.Abs(grados) < .001f) return false;
            return AplicarTransformacionNumerica((figura, centro) => figura.Rotar(grados, centro));
        }

        public bool EscalarSeleccion(float escalaX, float escalaY)
        {
            if (seleccion.Count == 0 || escalaX <= 0 || escalaY <= 0) return false;
            if (Math.Abs(escalaX - 1) < .001f && Math.Abs(escalaY - 1) < .001f) return false;
            return AplicarTransformacionNumerica((figura, centro) => figura.Escalar(escalaX, escalaY, centro));
        }

        private bool AplicarTransformacionNumerica(Action<Figura2D, PointF> operacion)
        {
            var figuras = seleccion.ToList();
            var antes = figuras.Select(x => x.Transformacion.AArreglo()).ToList();
            PointF centro = CentroSeleccion();
            foreach (var figura in figuras) operacion(figura, centro);
            var despues = figuras.Select(x => x.Transformacion.AArreglo()).ToList();
            Documento.Notificar();
            Historial.RegistrarEjecutado(new ComandoTransformar(Documento, figuras, antes, despues));
            Cambio();
            return true;
        }

        private Figura2D CrearFiguraArrastre(PointF a, PointF b)
        {
            var r = Normalizar(a, b); if (r.Width < 1 && r.Height < 1) return null;
            if (HerramientaActual == HerramientaTipo.Linea) return new LineaFigura(a, b, EstiloActual, AlgoritmoLinea);
            if (HerramientaActual == HerramientaTipo.Rectangulo) return new RectanguloFigura(r, EstiloActual, false);
            if (HerramientaActual == HerramientaTipo.RectanguloRedondeado) return new RectanguloRedondeadoFigura(r, EstiloActual);
            if (HerramientaActual == HerramientaTipo.Elipse) return new ElipseFigura(r, EstiloActual, AlgoritmoCirculo);
            if (HerramientaActual == HerramientaTipo.Poligono && FormaActual != FormaPersonalizada.Poligono) return new FabricaFigurasPersonalizadas().Crear(FormaActual, r, EstiloActual);
            return null;
        }

        private void ConfirmarTemporal()
        {
            if(HerramientaActual==HerramientaTipo.Poligono&&FormaActual==FormaPersonalizada.Poligono)
            {
                if(puntosTemporales.Count>=3){var figura=new PoligonoFigura(puntosTemporales,EstiloActual){Cerrado=true};Historial.Ejecutar(new ComandoAgregar(Documento,figura));SeleccionarFiguraReciente(figura);}
            }
            else if (previa != null){var figura=previa;Historial.Ejecutar(new ComandoAgregar(Documento,figura));if(DebeEditarAlCrear(figura))SeleccionarFiguraReciente(figura);}
            previa = null; puntosTemporales.Clear(); Cambio();
        }

        private void AplicarRelleno(Point semilla)
        {
            var buffer = Rasterizador.RenderizarBuffer(Documento); var tramos = new RellenoFloodFill().RellenarTramos(buffer, semilla, EstiloActual.ColorRelleno);
            if (tramos.Count > 0) Historial.Ejecutar(new ComandoAgregar(Documento, new RellenoRasterFigura(tramos, EstiloActual.ColorRelleno))); Cambio();
        }

        private void IniciarSeleccion(PointF p)
        {
            RectangleF r = LimitesSeleccion(); transformacion = TransformacionActiva.Ninguna;
            if (seleccion.Count > 0 && Cerca(p, PuntoTiradorEscala(), ToleranciaInteraccion)) transformacion = TransformacionActiva.Escalar;
            else if (seleccion.Count > 0 && Cerca(p, PuntoTiradorRotacion(), ToleranciaInteraccion)) transformacion = TransformacionActiva.Rotar;
            else if(seleccion.Count>1&&r.Contains(p))transformacion=TransformacionActiva.Trasladar;
            else
            {
                var encontrada = Documento.Figuras.Reverse().FirstOrDefault(x => !(x is RellenoRasterFigura) && x.Contiene(p));
                if (encontrada == null) { seleccion.Clear(); NotificarSeleccion(); presionado = false; Cambio(); return; }
                if (!seleccion.Contains(encontrada)) SeleccionarSolo(encontrada); transformacion = TransformacionActiva.Trasladar;
            }
            centroTransformacion = CentroSeleccion();
            matricesAntes = seleccion.Select(x => x.Transformacion.AArreglo()).ToList(); Cambio();
        }

        private bool EsInteraccionConSeleccion(PointF p)
        {
            if(seleccion.Count==0)return false;
            if(Cerca(p,PuntoTiradorEscala(),ToleranciaInteraccion)||Cerca(p,PuntoTiradorRotacion(),ToleranciaInteraccion))return true;
            if(seleccion.Any(figura=>figura.Contiene(p)))return true;
            return seleccion.Count>1&&LimitesSeleccion().Contains(p);
        }

        private void TransformarSeleccion(PointF p)
        {
            if (seleccion.Count == 0 || transformacion == TransformacionActiva.Ninguna) return; PointF centro = centroTransformacion;
            if (transformacion == TransformacionActiva.Trasladar) foreach (var f in seleccion) f.Trasladar(p.X - ultimo.X, p.Y - ultimo.Y);
            else if (transformacion == TransformacionActiva.Escalar)
            {
                float distanciaAnterior=LineaFigura.Distancia(ultimo,centro),distanciaNueva=LineaFigura.Distancia(p,centro);
                if(distanciaAnterior<.5f)return;
                var marco=MarcoSeleccion();float ancho=marco.Count>=4?LineaFigura.Distancia(marco[0],marco[1]):1f,alto=marco.Count>=4?LineaFigura.Distancia(marco[1],marco[2]):1f;
                float minimo=Math.Max(4f/Math.Max(ancho,.1f),4f/Math.Max(alto,.1f));
                float factor=Math.Max(minimo,Math.Min(20f,distanciaNueva/distanciaAnterior));
                foreach (var f in seleccion) f.Escalar(factor, factor, centro);
            }
            else
            {
                double a0 = Math.Atan2(ultimo.Y - centro.Y, ultimo.X - centro.X), a1 = Math.Atan2(p.Y - centro.Y, p.X - centro.X); float grados = (float)((a1 - a0) * 180 / Math.PI); foreach (var f in seleccion) f.Rotar(grados, centro);
            }
            Documento.Notificar();
        }

        private void FinalizarTransformacion()
        {
            if (seleccion.Count > 0 && matricesAntes != null)
            {
                var despues = seleccion.Select(x => x.Transformacion.AArreglo()).ToList();
                if (!Iguales(matricesAntes, despues)) Historial.RegistrarEjecutado(new ComandoTransformar(Documento, seleccion, matricesAntes, despues));
            }
            transformacion = TransformacionActiva.Ninguna; matricesAntes = null; Cambio();
        }

        private void SeleccionarConLazo()
        {
            seleccion.Clear(); if (lazo.Count < 3) { NotificarSeleccion(); return; }
            foreach (var f in Documento.Figuras.Where(x => !(x is RellenoRasterFigura)))
            {
                if (FiguraCoincideConLazo(f)) seleccion.Add(f);
            }
            NotificarSeleccion();
        }

        private bool FiguraCoincideConLazo(Figura2D figura)
        {
            var marco=figura.ObtenerMarcoSeleccion();
            if(marco.Count>0)
            {
                PointF centro=new PointF(marco.Average(p=>p.X),marco.Average(p=>p.Y));
                if(PuntoEnLazo(centro)||marco.Any(PuntoEnLazo))return true;
                for(int i=0;i<marco.Count;i++)
                    for(int j=0;j<lazo.Count;j++)
                        if(SegmentosIntersectan(marco[i],marco[(i+1)%marco.Count],lazo[j],lazo[(j+1)%lazo.Count]))return true;
            }
            if(figura.PuntosTransformados.Any(PuntoEnLazo))return true;
            return lazo.Any(p=>figura.Contiene(p,2));
        }

        private static bool SegmentosIntersectan(PointF a,PointF b,PointF c,PointF d)
        {
            float o1=Orientacion(a,b,c),o2=Orientacion(a,b,d),o3=Orientacion(c,d,a),o4=Orientacion(c,d,b);
            return ((o1>0&&o2<0)||(o1<0&&o2>0))&&((o3>0&&o4<0)||(o3<0&&o4>0));
        }
        private static float Orientacion(PointF a,PointF b,PointF c){return (b.X-a.X)*(c.Y-a.Y)-(b.Y-a.Y)*(c.X-a.X);}

        private bool PuntoEnLazo(PointF p)
        {
            bool dentro = false; for (int i = 0, j = lazo.Count - 1; i < lazo.Count; j = i++) if (((lazo[i].Y > p.Y) != (lazo[j].Y > p.Y)) && p.X < (lazo[j].X-lazo[i].X)*(p.Y-lazo[i].Y)/(lazo[j].Y-lazo[i].Y)+lazo[i].X) dentro = !dentro; return dentro;
        }

        private void SeleccionarSolo(Figura2D f) { seleccion.Clear(); if (f != null) seleccion.Add(f); NotificarSeleccion(); }
        private void SeleccionarFiguraReciente(Figura2D figura){seleccion.Clear();seleccion.Add(figura);editandoFiguraReciente=true;NotificarSeleccion();}
        private static bool DebeEditarAlCrear(Figura2D figura){return figura is LineaFigura||figura is RectanguloFigura||figura is ElipseFigura||figura is PoligonoFigura||figura is BezierFigura||figura is TextoFigura;}
        private void NotificarSeleccion()=>SeleccionActualizada?.Invoke(seleccion.Count);
        private void Cambio() => CambioVisual?.Invoke(this, EventArgs.Empty);
        private static RectangleF Normalizar(PointF a, PointF b) => RectangleF.FromLTRB(Math.Min(a.X,b.X), Math.Min(a.Y,b.Y), Math.Max(a.X,b.X), Math.Max(a.Y,b.Y));
        private static bool Cerca(PointF a, PointF b, float t) => LineaFigura.Distancia(a,b) <= t;
        private static bool Iguales(List<double[]> a, List<double[]> b) { if (a.Count != b.Count) return false; for (int i=0;i<a.Count;i++) for(int j=0;j<a[i].Length;j++) if(Math.Abs(a[i][j]-b[i][j])>.0001)return false; return true; }
    }
}
