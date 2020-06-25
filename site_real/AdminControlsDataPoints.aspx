<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AdminControlsDataPoints.aspx.cs" Inherits="site_real.AdminControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
    <div dir="ltr">
        <div runat="server" id="datapairs">
        </div>
        <br />
        <asp:Label runat="server" AssociatedControlID="key" Text="key:"></asp:Label>
        <asp:TextBox runat="server" ID="key"></asp:TextBox>
        <asp:Label runat="server" AssociatedControlID="Value" Text="value:"></asp:Label>
        <asp:TextBox runat="server" ID="Value"></asp:TextBox>

    </div>

</asp:Content>
