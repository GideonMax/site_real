<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="site_real.Login1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link type="text/css" href="css/login.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1>כניסה לאתר</h1>
    <asp:Label Text="שם משתמש:" runat="server" AssociatedControlID="u_name" />
    <asp:TextBox runat="server" ID="u_name" />
	<br />
    <asp:Label runat="server" Text="סיסמא:" AssociatedControlID="u_password" />
	<asp:TextBox runat="server" ID="u_password" TextMode="Password" />
	<br />
    <asp:Button runat="server" ID="login" Text="כניסה" OnClick="Login" />
	<br />
    <asp:Label ID="Message" runat="server" />
</asp:Content>
