<%@ Page Title="Production History" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="PrdHistory.aspx.vb" Inherits="Drawing.PrdHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h3>ประวัติการเรียกใช้ Drawing(Production's Log)</h3>
    <br />
    <asp:GridView ID="GrdHistory" runat="server" CssClass="col-sm-auto bg-light text-center" GridLines="None" AllowSorting="True">
        <HeaderStyle BackColor="#FFCCCC" />
    </asp:GridView>
</asp:Content>
