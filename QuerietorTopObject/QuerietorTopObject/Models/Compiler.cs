using Microsoft.CSharp;
using Newtonsoft.Json;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;

namespace QuerietorTopObject.Models
{
    public class Compiler
    {
        public static CompilerResults CompilarCodigo(string codigoFuente)
        {
            CSharpCodeProvider proveedor = new CSharpCodeProvider();
            CompilerParameters parametros = new CompilerParameters();

            string directorioBase = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dlls");
            string[] archivosDll = Directory.GetFiles(directorioBase, "*.dll");

            foreach (string dllPath in archivosDll)
            {
                parametros.ReferencedAssemblies.Add(dllPath);
                Assembly.LoadFrom(dllPath);
            }

            parametros.ReferencedAssemblies.Add("System.Data.dll");
            parametros.ReferencedAssemblies.Add("System.Xml.dll");
            parametros.ReferencedAssemblies.Add("System.dll");
            parametros.ReferencedAssemblies.Add("System.Security.dll");
            parametros.ReferencedAssemblies.Add("System.ComponentModel.dll");
            parametros.GenerateExecutable = false;
            parametros.GenerateInMemory = true;

            return proveedor.CompileAssemblyFromSource(parametros, codigoFuente);
        }

        public static object CrearInstanciaDinamica(Assembly assembly, string typeName)
        {
            Type tipo = assembly.GetType(typeName);
            return Activator.CreateInstance(tipo);
        }

        public static string FormatearSalida(object salida)
        {
           // string jsonString = JsonConvert.SerializeObject(salida);
            //jsonString = jsonString.Replace(",", ",\n").Replace("[{", "[\n{").Replace("}}", "}\n}").Replace(":{", ":\n{\n").Replace("{\"", "{\n\"").Replace("\"}", "\"\n}").Replace("}]", "}\n]");
            //return  $"<pre> <code class=\"language-json\">{HttpUtility.HtmlEncode(jsonString)}</code></pre>";

            return $"<pre><code class=\"language-json\">{JsonConvert.SerializeObject(salida, Formatting.Indented)}</code></pre>";
        }
    }
}