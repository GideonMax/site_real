<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="site_real.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript" src="/js/countryData.js"></script>
    <script type="text/javascript" src="/js/geocoding.js"></script>
    <script type="text/javascript" src="/js/test.js"></script>
     <link type="text/css" href="css/default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1>Welcome to World Viewer Beta</h1>
    <db-div TextName="main"></db-div>
    <br />
</asp:Content>
