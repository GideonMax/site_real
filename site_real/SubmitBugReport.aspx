<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SubmitBugReport.aspx.cs" Inherits="site_real.SubmitBugReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:TextBox runat="server" ID="ReportTitle"></asp:TextBox>
    <asp:TextBox runat="server" id="Body" ></asp:TextBox>
    <asp:Button runat="server" ID="Submit" Text="שלח" OnClick="SubmitReport" />
</asp:Content>
