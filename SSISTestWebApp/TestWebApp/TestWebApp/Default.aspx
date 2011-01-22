<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="TestWebApp._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <h2>
       Package Type: 
        <asp:TextBox ID="PkType" runat="server"></asp:TextBox>
        Package Folder:
         <asp:TextBox ID="PKLoc" runat="server"></asp:TextBox>
           Package Name:
         <asp:TextBox ID="PKName" runat="server"></asp:TextBox>
      </h2>
      <h2> <asp:Button ID="RunButton" runat="server" Text="Execute" 
            onclick="RunButton_Click" Font-Bold="True"  /></h2>    
        
   
   
</asp:Content>
