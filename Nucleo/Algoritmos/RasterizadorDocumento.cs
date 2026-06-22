using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RasterizadorDocumento
    {
        public Bitmap Renderizar(DocumentoDibujo documento,Figura2D vistaPrevia=null)
        {
            var buffer=RenderizarBuffer(documento);var contexto=new ContextoRaster(buffer,documento.ColorFondo);vistaPrevia?.Rasterizar(contexto);return buffer.CrearBitmap();
        }

        public BufferPixeles RenderizarBuffer(DocumentoDibujo documento)
        {
            var buffer=new BufferPixeles(documento.Ancho,documento.Alto,documento.ColorFondo);var contexto=new ContextoRaster(buffer,documento.ColorFondo);foreach(var figura in documento.Figuras)figura.Rasterizar(contexto);return buffer;
        }
    }
}
