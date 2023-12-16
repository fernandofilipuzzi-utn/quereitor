# querietor

### Descripción
La idea principal es poder correr c# en vivo desde una consola web de una forma similar a como se puede correr sentencias sql. 

Aún hay problemas a resolver:
- Hay que agregar un control para que se pueda cargar las ddls con las clases que se quiera usar.
- Por ahora, hay una carpeta donde dejar las ddls, pero el problema es que hay que agregarlas en las referencia del proyecto y compilar el proyecto luego

###  Vista Web
<details open>
<summary>Implementación web</summary>

  <div align="center">
        <img style="width:80%;" src="QuerietorTopObject/QuerietorTopObject/docs/pantallazo_menu.jpg"/>
        <p>Figura 1. Vista del menú principal</p>
  </div>

Para correr el código es necesario respetar la siguiente clase :

```csharp
using EjemploClassLib;
using System.Data;

public class EjecucionDinamica
{
    public object Ejecutar()
    {
        //código 
        return respuesta;
    }
}
```
como se ve en el código es necesario devolver algo, aunque sea una cadena vacía, luego este objeto devuelto se verá en la salida.

La siguiente Figura ilustra su uso.

  <div align="center">
     <img style="width:80%;" src="QuerietorTopObject/QuerietorTopObject/docs/pantallazo_query_object.jpg"/>
     <p>Figura 2. Consola Web, ejemplo de uso </p>
  </div>

  En el ejemplo se instancia dos objetos y se conforma un array, la clase Persona es parte de un proyecto de clases incluido como una dll.
  
</details>
