using Newtonsoft.Json;
using QuerietorTopObject.Models;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace QuerietorTopObject
{
    public partial class querietor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
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
                hdnCodeMirrorEditorValue.Value = codigoInicial;
            }
        }

        protected void btnPrueba_Click(object sender, EventArgs e)
        {
            try
            {
                string codigoFuente = hdnCodeMirrorEditorValue.Value;
                CompilerResults resultados = Compiler.CompilarCodigo(codigoFuente);

                if (resultados.Errors.HasErrors == false)
                {
                    object instancia = Compiler.CrearInstanciaDinamica(resultados.CompiledAssembly, "EjecucionDinamica");

                    MethodInfo metodo = instancia.GetType().GetMethod("Ejecutar");
                    object salida = metodo.Invoke(instancia, null);

                    dvSalida.InnerHtml = Compiler.FormatearSalida(salida);
                }
                else
                {
                    string errores = string.Join(Environment.NewLine, resultados.Errors.Cast<CompilerError>().Select(error => error.ErrorText));
                    dvSalida.InnerHtml = Compiler.FormatearSalida(errores);
                }
            }
            catch (Exception ex)
            {
                dvSalida.InnerHtml = Compiler.FormatearSalida(ex);
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "highlightSyntax", "highlightSyntax();", true);
        }
    }
}