<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Join.aspx.cs" Inherits="site_real.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link type="text/css" href="css/join.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1>הצטרף לאתר שלנו</h1>
    <br />
    <asp:Label Text="שם משתמש:" AssociatedControlID="u_name" runat="server" />
    <asp:TextBox runat="server" id="u_name"  />
    <br />
    <asp:Label Text="סיסמא:" AssociatedControlID="u_password" runat="server" />
    <asp:TextBox runat="server" id="u_password"  TextMode="Password"/>
    <br />
    <asp:Label Text="אישור סיסמא:" AssociatedControlID="c_password" runat="server" />
    <asp:TextBox runat="server" id="c_password" TextMode="Password"  />
    <br />
    <asp:Label Text="אני אדמין" AssociatedControlID="admin_check" runat="server" />
    <asp:CheckBox AutoPostBack="true" id="admin_check" runat="server" OnCheckedChanged="Unnamed_CheckedChanged" />
    <asp:TextBox name="k" Visible="false" id="admin_code" runat="server"/>
    <br />
    <asp:Button Text="אשר והירשם" runat="server" OnClick="join" />
    <br />
    <asp:Label id="message" runat="server" />

</asp:Content>
