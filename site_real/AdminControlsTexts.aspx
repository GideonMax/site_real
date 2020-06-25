<%@ Page validateRequest="false" Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdminControlsTexts.aspx.cs" Inherits="site_real.AdminControlsTexts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div dir="ltr">
        <div runat="server" id="TextNames">
        </div>
        <br />
        <asp:Literal runat="server" ID="TextDisplay" ></asp:Literal>
        <br />
        <asp:Label runat="server" AssociatedControlID="name" Text="name:"></asp:Label>
        <asp:TextBox runat="server" ID="name"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="Text" Text="Text:"></asp:Label>
        <asp:TextBox ID="Text" runat="server" TextMode="MultiLine" Rows="15" Columns="40"></asp:TextBox>

    </div>
</asp:Content>
