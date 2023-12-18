<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="QuerietorTopObject._index" Async="true" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h2>Querietor</h2>
        <p class="lead">etc...</p>
    </div>

    <div class="container row ">

        <div class="card col-lg-3 col-md-4 col-sm-6 m-2 p-3">
            <img src="img/consultas.jpg" class="card-img-top img-fluid" style="height: 200px; object-fit: cover;" />
            <div class="card-body">
                <div class="card-title">
                    <h5>Consola</h5>
                </div>
                <div class="card-text" style="max-height: 60px; overflow: hidden;">
                    <p>Consola para correr live object</p>
                </div>
            </div>
            <div class="text-center">
                <asp:HyperLink ID="btnConsultaDeuda" CssClass="btn btn-primary"
                    NavigateUrl="querietor.aspx" Text="Abrir consulta" runat="server" />
            </div>
        </div>

    </div>

</asp:Content>
