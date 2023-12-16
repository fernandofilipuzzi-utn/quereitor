using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using WebGrease.Extensions;
using System.IO;
using System.Web.Services.Description;

namespace QuerietorTier
{
    public partial class querietor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                // Cargar el código inicial en el TextBox
                string codigoInicial = @"
using EjemploClassLib;
using System.Data;

public class EjecucionDinamica
{
    public object Ejecutar()
    {
        //instanciando un objeto
        Persona persona=new Persona {Dni=45656555, Nombre=""Catalina""};
        return persona;
    }
}";
                tbQueryCsharp.Text = codigoInicial;

            }
        }

        protected void btnEjecutar_Click(object sender, EventArgs e)
        {
            try
            {
                string codigoFuente = tbQueryCsharp.Text; // Asigna el contenido de tu caja de texto

                UpdatePanel1.Update();

                CompilerResults resultados = CompilarCodigo(codigoFuente);

                if (resultados.Errors.HasErrors)
                {
                    string errores = string.Join(Environment.NewLine, resultados.Errors.Cast<CompilerError>().Select(error => error.ErrorText));
                    tbRespuesta.Text = HttpUtility.HtmlEncode($"Errores de compilación: {errores}");
                    ((SiteMaster)this.Master).ShowMessage("Error", "Errores de compilación");
                }
                else
                {
                    object instancia = CrearInstanciaDinamica(resultados.CompiledAssembly, "EjecucionDinamica");

                    MethodInfo metodo = instancia.GetType().GetMethod("Ejecutar");
                    object respuesta = metodo.Invoke(instancia, null);

                    #region formateo json
                    string jsonString = JsonConvert.SerializeObject(respuesta);
                    jsonString = jsonString.Replace(",", ",\n").Replace("[{", "[\n{").Replace("}}", "}\n}").Replace(":{", ":\n{\n").Replace("{\"", "{\n\"").Replace("\"}", "\"\n}").Replace("}]", "}\n]");
                    var formattedHtml = $"<pre> <code class=\"language-json\">{HttpUtility.HtmlEncode(jsonString)}</code></pre>";

                    tbRespuesta.Text = formattedHtml;
                    tbRespuesta.Mode = LiteralMode.PassThrough;

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "highlightSyntax", "highlightSyntax();", true);
                    #endregion

                    ((SiteMaster)this.Master).ShowMessage("Mensaje", $"Enviado!");
                }
            }
            catch (Exception ex)
            {
                string message = $"Exception: {ex.Message}\n StackTrace: {ex.StackTrace}";
                if (ex.InnerException != null)
                    message += $"\nInnerException: {ex.InnerException.Message}\n StackTrace: {ex.InnerException.StackTrace}";

                tbRespuesta.Text = HttpUtility.HtmlEncode(message)
                                        .Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;")
                                        .Replace("\n", "<br/>");

                ((SiteMaster)this.Master).ShowMessage("Error", $"{message}");
            }
        }

        static CompilerResults CompilarCodigo(string codigoFuente)
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

        static object CrearInstanciaDinamica(Assembly assembly, string typeName)
        {
            Type tipo = assembly.GetType(typeName);
            return Activator.CreateInstance(tipo);
        }

        private string FormatJsonToHtml(string jsonString)
        {
            try
            {
                dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);

                var htmlTable = "<table border='1'><thead><tr>";

                foreach (var property in jsonObject[0].Properties())
                {
                    htmlTable += $"<th>{property.Name}</th>";
                }

                htmlTable += "</tr></thead><tbody>";

                foreach (var item in jsonObject)
                {
                    htmlTable += "<tr>";
                    foreach (var property in item.Properties())
                    {
                        htmlTable += $"<td>{property.Value}</td>";
                    }
                    htmlTable += "</tr>";
                }

                htmlTable += "</tbody></table>";

                return htmlTable;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al formatear el JSON a HTML: {ex.Message}| {ex.StackTrace}");
            }
        }

        protected void tbRespuesta_PreRender(object sender, EventArgs e)
        {
        }

        protected void tbQueryCsharp_Load(object sender, EventArgs e)
        {
            //  ScriptManager.RegisterStartupScript(this, GetType(), "initializeCodeMirror", "initializeCodeMirror();", true);
        }

        protected void tbQueryCsharp_Unload(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "initializeCodeMirror", "initializeCodeMirror();", true);
        }
    }
}