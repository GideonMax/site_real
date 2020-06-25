<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="site_real.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="module"  src="/js/DBTextWC.js"></script>
    <!--<link type="text/css" href="css/default.css" rel="stylesheet" />-->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1>ברוך הבא לWorld Viewer</h1>
    <br />
    <a href="https://drive.google.com/file/d/1JyRfGyKg0bYNYcM17faH9OdLmIOKLkUA/view?usp=sharing">מצגת</a>
    <db-div TextName="main"></db-div>
    <br />
    <img id="home" src="https://media.giphy.com/media/l1KVcrdl7rJpFnY2s/giphy.gif">
    <br />
</asp:Content>
