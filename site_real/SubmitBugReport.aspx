<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SubmitBugReport.aspx.cs" Inherits="site_real.SubmitBugReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <asp:Label runat="server" Text="קותרת:" ></asp:Label>
    <asp:TextBox runat="server" ID="ReportTitle"></asp:TextBox>
    <asp:Label runat="server" Text="גוף:" ></asp:Label>
    <asp:TextBox runat="server" id="Body" ></asp:TextBox>
    <asp:Button runat="server" ID="Submit" Text="שלח" OnClick="SubmitReport" />
</asp:Content>
