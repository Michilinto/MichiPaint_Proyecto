using System.Collections.Generic;
using System.Drawing;

namespace Paint_Bolaños_Flores_Venegas.Nucleo
{
    public sealed class RellenoFloodFill : IAlgoritmoRelleno
    {
        public string Nombre=>"Flood Fill";
        public IEnumerable<Point> Rellenar(BufferPixeles buffer,Point semilla,Color reemplazo)
        {
            foreach(var tramo in RellenarTramos(buffer,semilla,reemplazo))for(int x=tramo.Inicio;x<=tramo.Fin;x++)yield return new Point(x,tramo.Y);
        }

        public List<TramoRelleno> RellenarTramos(BufferPixeles buffer,Point semilla,Color reemplazo)
        {
            var tramos=new List<TramoRelleno>();if(!buffer.Dentro(semilla.X,semilla.Y))return tramos;int objetivo=buffer.ObtenerPixel(semilla.X,semilla.Y).ToArgb();if(objetivo==reemplazo.ToArgb())return tramos;
            var semillas=new Stack<Point>();semillas.Push(semilla);
            while(semillas.Count>0){var s=semillas.Pop();if(!buffer.Dentro(s.X,s.Y)||buffer.ObtenerPixel(s.X,s.Y).ToArgb()!=objetivo)continue;int izquierda=s.X,derecha=s.X;while(izquierda>0&&buffer.ObtenerPixel(izquierda-1,s.Y).ToArgb()==objetivo)izquierda--;while(derecha<buffer.Ancho-1&&buffer.ObtenerPixel(derecha+1,s.Y).ToArgb()==objetivo)derecha++;for(int x=izquierda;x<=derecha;x++)buffer.PonerPixel(x,s.Y,reemplazo);tramos.Add(new TramoRelleno(s.Y,izquierda,derecha));BuscarSemillas(buffer,objetivo,s.Y-1,izquierda,derecha,semillas);BuscarSemillas(buffer,objetivo,s.Y+1,izquierda,derecha,semillas);}
            return tramos;
        }

        private static void BuscarSemillas(BufferPixeles buffer,int objetivo,int y,int izquierda,int derecha,Stack<Point> semillas)
        {
            if(y<0||y>=buffer.Alto)return;bool enRegion=false;for(int x=izquierda;x<=derecha;x++){bool coincide=buffer.ObtenerPixel(x,y).ToArgb()==objetivo;if(coincide&&!enRegion){semillas.Push(new Point(x,y));enRegion=true;}else if(!coincide)enRegion=false;}
        }
    }

    public sealed class TramoRelleno
    {
        public int Y{get;}public int Inicio{get;}public int Fin{get;}
        public TramoRelleno(int y,int inicio,int fin){Y=y;Inicio=inicio;Fin=fin;}
    }
}
