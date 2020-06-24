<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="site_real.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="module"  src="/js/DBTextWC.js"></script>
    <link type="text/css" href="css/default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1>ברוך הבא לWorld Viewer</h1>
    <br />
    <db-div TextName="main"></db-div>
    <br />
</asp:Content>
