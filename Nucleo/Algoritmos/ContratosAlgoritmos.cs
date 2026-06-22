using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public interface IAlgoritmoLinea
    {
        string Nombre { get; }
        IEnumerable<Point> Calcular(Point inicio, Point fin);
    }

    public interface IAlgoritmoCirculo
    {
        string Nombre { get; }
        IEnumerable<Point> Calcular(Point centro, int radio);
    }

    public interface IAlgoritmoCurva
    {
        string Nombre { get; }
        List<Point> Calcular(IReadOnlyList<PointF> puntosControl, int muestras = 120);
    }

    public interface IAlgoritmoRelleno
    {
        string Nombre { get; }
        IEnumerable<Point> Rellenar(BufferPixeles buffer, Point semilla, Color color);
    }

    public interface IAlgoritmoRellenoPoligono
    {
        string Nombre { get; }
        void Rellenar(BufferPixeles buffer, IReadOnlyList<Point> vertices, Color color);
    }

    public interface IAlgoritmoRecorteLinea
    {
        string Nombre { get; }
        bool Recortar(Rectangle ventana, ref PointF inicio, ref PointF fin);
    }

    public interface IAlgoritmoRecortePoligono
    {
        string Nombre { get; }
        List<Point> Recortar(IEnumerable<Point> poligono, Rectangle ventana);
    }
}
