<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="querietor.aspx.cs" Inherits="QuerietorTopObject.querietor" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h3 class="display-4">Object Query Live Commander</h3>
        <p class="lead">Tome como plantilla el ejemplo dado.</p>
    </div>

    <div class="container text-center p-2 m-2" style="background-color: #dcdced;">
        <div class="col-12" >
            <h3>Consola de edición</h3>
            <asp:HiddenField ID="hdnCodeMirrorEditorValue" runat="server" ClientIDMode="Static" />
            <div id="codeEditor" clientidmode="Static" style="text-align: left;" runat="server"/>
        </div>

        <div class="container text-center p-2 m-2">
            <asp:LinkButton ID="btnPrueba" OnClick="btnPrueba_Click" CssClass="btn btn-primary" runat="server">Correr<i class="fa fa-solid fa-play"></i></asp:LinkButton>
        </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <asp:Panel ID="panel" ClientIDMode="Static" runat="server">

                <div class="container text-center p-2 m-2" style="background-color: #dcdced;">
                    <h3>Salida</h3>
                    <div class="p-0 m-0" style="position: relative; width: 100%; text-align: left;">
                        <div id="dvSalida" style="text-align: left; background-color: #fffef4; " clientidmode="Static" runat="server"></div>
                        <button onclick="copiarAlPortapapeles()" style="position: absolute; top: 5px; right: 5px;"><i class="fa-sharp fa-solid fa-copy"></i></button>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script>

        function initializeCodeMirror() {
            var editor = CodeMirror(document.getElementById("codeEditor"), {
                lineNumbers: true,
                mode: "text/x-csharp",
                height: "auto",
                viewportMargin: Infinity,
                theme: "dracula"
            });
            editor.setValue(document.getElementById('hdnCodeMirrorEditorValue').value);

            window.codeMirrorEditor = editor;

            editor.on("change", function () {
                document.getElementById('hdnCodeMirrorEditorValue').value = editor.getValue();
            });
        }

        document.addEventListener("DOMContentLoaded", function () {
            initializeCodeMirror();
        });

        function highlightSyntax() {
            document.querySelectorAll('pre code').forEach((block) => {
                hljs.highlightBlock(block);
            });
        }

        function copiarAlPortapapeles() {
            var contenidoDiv = document.querySelector('#dvSalida').innerText;
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
    </script>
</asp:Content>
