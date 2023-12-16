<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="querietor.aspx.cs" Inherits="QuerietorTier.querietor" Async="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3 class="display-4">Ingrese su query</h3>
        <p class="lead">coloque su código siguiendo manteniendo la plantilla ejemplo propuesta.</p>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server"  ChildrenAsTriggers="false" UpdateMode="Conditional">
        <ContentTemplate>

            <div class="container body-content text-center" style="background-color: #dcdced;">

                <div class="form-column">
                    <h4>Subrutinas en c#,( respecte la sintaxis!) :</h4>

                    <div class="form-group col m-2 col-12" style="text-align: left;">
                        <label class="col-form-label col-3 text-right" for="tbIdentificadorDeuda">Consulta:</label>
                        <asp:TextBox ID="tbQueryCsharp"  ClientIDMode="Static"
                            EnableViewState="true" TextMode="MultiLine"
                            Rows="25" CssClass="form-control col-12" Text="" runat="server" 
                            placeholder="su código aquí!" />
                    </div>
                </div>

                <div class="form-group text-center">
                    <asp:Button ID="btnEjecutar" CssClass="btn btn-sm btn-primary"
                        Text="Ejecutar" OnClick="btnEjecutar_Click" runat="server"/>
                     <asp:Button ID="Button1" CssClass="btn btn-sm btn-primary"
                        Text="Colorear" OnClientClick="ejecutarDespuesDeClick();" runat="server"/>
                </div>
            </div>

            <div class="form-group text-center">
                <label>
                    <h3>Salida:</h3>
                </label>
                <div style="position: relative; width: 100%; background: #fffef4; text-align: left; border: 1px solid black;">
                    <button onclick="copiarAlPortapapeles()" style="position: absolute; top: 5px; right: 5px;"><i class="fa-sharp fa-solid fa-copy"></i></button>
                    <div id="dvContenido" class="code">
                        <asp:Literal  ID="tbRespuesta" runat="server" Mode="PassThrough" />
                    </div>
                </div>
            </div>

            <style>
                .highlight {
                    padding: 1em;
                    background-color: #f4f4f4;
                    border-radius: 5px;
                    overflow: auto;
                }
            </style>

            <script>
              
                function initializeCodeMirror() {
                    var editor = CodeMirror.fromTextArea(document.getElementById("tbQueryCsharp"), {
                        lineNumbers: true,
                        mode: "text/x-csharp",
                        height: "auto",
                        viewportMargin: Infinity
                    });

                    window.codeMirrorEditor = editor;
                }

                function ejecutarDespuesDeClick() {
                    initializeCodeMirror();
                }

                function EndRequestHandler(sender, args) {
                    initializeCodeMirror();
                }
            
                function highlightSyntax() {
                    document.querySelectorAll('pre code').forEach((block) => {
                        hljs.highlightBlock(block);
                    });
                }

                function handleKeyPress(event) {
                    var charCode = event.charCode;
                    if ((charCode < 48 || charCode > 57) && charCode !== 44) {
                        return false;
                    }
                }

                function copiarAlPortapapeles()
                {
                    var contenidoDiv = document.querySelector('#dvContenido').innerText;

                    var areaTextoTemporal = document.createElement('textarea');
                    areaTextoTemporal.value = contenidoDiv;
                    areaTextoTemporal.style.position = 'fixed';
                    areaTextoTemporal.style.opacity = 0;
                    document.body.appendChild(areaTextoTemporal);
                    areaTextoTemporal.select();
                    document.execCommand('copy');
                    document.body.removeChild(areaTextoTemporal);
                    alert('Contenido copiado al portapapeles');
                }

                document.addEventListener("DOMContentLoaded", function () {
                    initializeCodeMirror();
                });


            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
