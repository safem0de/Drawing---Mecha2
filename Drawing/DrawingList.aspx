<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="DrawingList.aspx.vb" Inherits="Drawing.DrawingList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="form-row">
        <div class="form-group col-md-6">
            <label>ค้นหา (Search)</label>
            <div class="input-group-append">
                <asp:TextBox ID="TxtSearch" runat="server" class="form-control" placeholder="ค้นหาจาก DWG No. หรือ Part No."></asp:TextBox>&nbsp;
                <asp:Button ID="BtnSearch" runat="server" Text="ค้นหา" class="btn btn-primary"/>&nbsp;
                <asp:Button ID="BtnClear" runat="server" Text="ล้างรายการ" class="btn btn-danger"/>
            </div>
        </div>
    </div>

    <div class="card">
        <h5 class="card-header">รายชื่อ Drawing ทั้งหมด</h5>
        <div class="card-body">
            <asp:GridView ID="GrdDrawingList" runat="server" AllowPaging="True" CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="col-12 text-center" PageSize="20">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Left" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
