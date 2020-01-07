<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="Join.aspx.cs" Inherits="site_real.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <h1>Join</h1>
    <br />
    <asp:Label Text="user name:" AssociatedControlID="u_name" runat="server" />
    <asp:TextBox runat="server" id="u_name"  />
    <br />
    <asp:Label Text="password:" AssociatedControlID="u_password" runat="server" />
    <asp:TextBox runat="server" id="u_password"  TextMode="Password"/>
    <br />
    <asp:Label Text="confirm password:" AssociatedControlID="c_password" runat="server" />
    <asp:TextBox runat="server" id="c_password" TextMode="Password"  />
    <br />
    <asp:Label Text="I am an admin" AssociatedControlID="admin_check" runat="server" />
    <asp:CheckBox AutoPostBack="true" id="admin_check" runat="server" OnCheckedChanged="Unnamed_CheckedChanged" />
    <asp:TextBox name="k" Visible="false" id="admin_code" runat="server"/>
    <br />
    <asp:Button Text="confirm and sign up" runat="server" OnClick="join" />
    <br />
    <asp:Label id="message" runat="server" />

</asp:Content>
