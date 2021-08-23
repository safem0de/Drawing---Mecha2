<%@ Page Title="Request Drawing" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Search.aspx.vb" Inherits="Drawing.Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="form-row">
        <div class="form-group col-md-3">
            <%--<label>Drawing No.</label>--%>
            <asp:Label ID="LblDrawing" runat="server" Text="Part No."></asp:Label>
            <asp:TextBox ID="TxtPartNo" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group col-md-3">
            <%--<label>Lot No.</label>--%>
            <asp:Label ID="LblLotNo" runat="server" Text="Lot No."></asp:Label>
            <asp:TextBox ID="TxtLotNo" runat="server" class="form-control"></asp:TextBox>
        </div>
        <div class="form-group col-md-3">
            <%--<label>Process</label>--%>
            <asp:Label ID="LblProcess" runat="server" Text="Process"></asp:Label>
            <asp:DropDownList ID="DrpProcess" runat="server" class="form-control"></asp:DropDownList>
        </div>
    </div>
    <asp:Button ID="Search" runat="server" Text="Request Drawing" class="btn btn-success"/>
    <asp:Button ID="Clear" runat="server" Text="Clear" class="btn btn-danger"/>
    
    <%--<asp:TextBox ID="TxtTest" runat="server" TextMode="MultiLine"></asp:TextBox>--%>
</asp:Content>
