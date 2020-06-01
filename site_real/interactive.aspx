<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="interactive.aspx.cs" Inherits="site_real.interactive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="/css/interactive.css" rel="stylesheet" />
    <script src="maps.googleapis.com/maps/api/js?key=AIzaSyBos7iEDLXgnELcwl5EYC2YNZo3iSejM1Y"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div class="buttons">
        <asp:PlaceHolder runat="server" ID="buttons" />
    </div>
    <br />
    <asp:Literal  ID="article" runat="server" />
</asp:Content>
