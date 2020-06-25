<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdminControlsMap.aspx.cs" Inherits="site_real.AdminControlsMap" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="module" src="/js/MapWCAdmin.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <admin-map id="map">

    </admin-map>
</asp:Content>
