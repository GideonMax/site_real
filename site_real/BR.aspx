<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="BR.aspx.cs" Inherits="site_real.BR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Body" runat="server">
	<h2>נושאים</h2>
	<ul id="ulSubjects" runat="server"></ul>
<h2>נושא חדש</h2>
	<asp:Label runat="server" Text="כותרת:" /><br />
	<asp:TextBox ID="txtTitle" runat="server" /><br />
	<asp:Label runat="server" Text="תוכן:" /><br />
	<asp:TextBox ID="txtContent" TextMode="MultiLine" runat="server" /><br />
	<asp:Button runat="server" Text="פתח נושא" OnClick="OpenSubject_Click" />
	<h2>נושא חדש</h2>
	<asp:Panel runat="server" ID="pnlUser">
		<asp:Label runat="server" Text="כותרת:" /><br />
		<asp:TextBox ID="TextBox1" runat="server" /><br />
		<asp:Label runat="server" Text="תוכן:" /><br />
		<asp:TextBox ID="TextBox2" TextMode="MultiLine" runat="server" /><br />
		<asp:Button runat="server" Text="פתח נושא" OnClick="OpenSubject_Click" />
	</asp:Panel>
	<asp:HyperLink ID="linkLoginToPost" runat="server"
		Text="עליך להתחבר כדי לפרסם בפורום" NavigateUrl="~/Login.aspx?ref=Forum.aspx" />
</asp:Content>