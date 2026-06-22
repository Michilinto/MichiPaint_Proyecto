# MichiPaint

Aplicación de dibujo desarrollada en C# con Windows Forms y .NET Framework 4.7.2 para la asignatura de Computación Gráfica.

## Arquitectura

- **Vistas:** formulario principal, lienzo y controles retro. Convierte eventos del mouse a coordenadas del documento.
- **Núcleo:** documento, figuras, herramientas, transformaciones, historial, buffer de píxeles y algoritmos.
- **Archivos:** persistencia editable `.mpaint` en JSON y exportación PNG.
- **Recursos:** iconos pixel art utilizados por la interfaz.

La interfaz depende del núcleo y de archivos; archivos depende del núcleo. El núcleo no conoce formularios ni diálogos.

## Programación Orientada a Objetos

- `Figura2D` abstrae geometría, estilo, transformación, rasterización y selección.
- Las figuras concretas sobrescriben su comportamiento mediante herencia y polimorfismo.
- `IAlgoritmoLinea`, `IAlgoritmoCirculo` e `IAlgoritmoRelleno` aplican el patrón Strategy.
- `IHerramienta` separa la interacción del mouse del formulario.
- `IComando` permite deshacer y rehacer sin acoplar el historial a cada figura.
- `DocumentoDibujo` encapsula su colección y expone operaciones controladas.

## Algoritmos gráficos

- Líneas: Bresenham, DDA y punto medio.
- Círculos: punto medio, coordenadas polares y ecuación directa.
- Curvas: Bézier mediante polinomios de Bernstein.
- Relleno: Flood Fill de cuatro vecinos y Scanline con regla par-impar.
- Recorte: Cohen–Sutherland para segmentos y Sutherland–Hodgman para polígonos.
- Transformaciones: matrices homogéneas 3×3 para traslación, escala y rotación.
- Rasterización: buffer ARGB con control directo de píxeles y recorte seguro.

La lógica matemática fue reimplementada localmente a partir de lo aprendido en clase. MichiPaint no referencia ni carga ensamblados, clases o archivos de los proyectos `SHAPES_2D` y `Algoritmos_P2`.

Cada algoritmo tiene su propio archivo y clase que implementa un contrato: `LineaBresenham`, `LineaDDA`, `LineaPuntoMedio`, `CirculoPuntoMedio`, `CirculoPolar`, `CirculoEcuacionDirecta`, `CurvaBezier`, `RellenoFloodFill`, `RellenoScanline`, `RecorteCohenSutherland` y `RecorteSutherlandHodgman`.

Las figuras también son clases independientes derivadas de `Figura2D`. Las figuras personalizadas (`CorazonFigura`, `EstrellaFigura`, `FlechaFigura`, `CruzFigura`, `RomboFigura` y `TrapecioFigura`) heredan de `PoligonoFigura` y conservan su tipo concreto al guardar y abrir un proyecto.

## Controles

- Arrastrar para lápiz, pincel, borrador, líneas y figuras.
- Polígono: clic por vértice; doble clic o `Enter` para cerrar; `Esc` cancela.
- Bézier: cuatro clics de control.
- Selección: arrastrar la figura para trasladar, tirador inferior para escalar y superior para rotar.
- Selección libre: rodear completamente las figuras que formarán el grupo temporal.
- Clic izquierdo en la paleta: color de línea; clic derecho: color de relleno.
- Clic derecho sobre la herramienta de polígono: galería de figuras personalizadas.
- `Ctrl+N`, `Ctrl+O`, `Ctrl+S`, `Ctrl+Shift+S`, `Ctrl+E`, `Ctrl+Z`, `Ctrl+Y` y `Delete`.

## Archivos

- `.mpaint`: dimensiones, fondo, figuras, estilos, texto, algoritmos y matrices de transformación.
- `.png`: imagen final opaca con el tamaño exacto del lienzo.

## Compilación

Abrir `Paint_Bolaños_Flores_Venegas.csproj` en Visual Studio o ejecutar:

```powershell
dotnet msbuild Paint_Bolaños_Flores_Venegas.csproj -t:Rebuild -p:Configuration=Release
```
