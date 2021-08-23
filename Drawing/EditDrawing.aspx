<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditDrawing.aspx.vb" Inherits="Drawing.EditDrawing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />

    <div class="form-row">
        <div class="form-group col-md-6">
            <label>ค้นหา (Search)</label>
            <div class="input-group-append">
                <asp:TextBox ID="TxtSearch" runat="server" class="form-control" placeholder="ค้นหาจาก DWG No. หรือ Part No."></asp:TextBox>&nbsp;
                <asp:Button ID="BtnSearch" runat="server" Text="ค้นหา" class="btn btn-primary"/>&nbsp;
            </div>
        </div>
    </div>

    <br />

    <div class="row">
        <div class="col-3">
            <label>หมายเลขแบบ (Drawing No./Rev)</label>
            <div class="input-group mb-3">
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="far fa-id-card"></i></span>
                </div>
                <asp:TextBox ID="TxtDrawing" runat="server" class="form-control col-9" ></asp:TextBox>
                <asp:TextBox ID="TxtRevision" runat="server" class="form-control col-3" ></asp:TextBox>
            </div>
        </div>

        <div class="col-3">
            <label>ชื่อ (PartName/Dash)</label>
            <div class="input-group mb-3">
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="far fa-id-card"></i></span>
                </div>
                <asp:TextBox ID="TxtPartName" runat="server" class="form-control"></asp:TextBox>
            </div>
        </div>

        <div class="col-3">
            <label>หมายเลขชิ้นงาน (PartNo/Dash)</label>
            <div class="input-group mb-3">
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="far fa-id-card"></i></span>
                </div>
                <asp:TextBox ID="TxtPartNo" runat="server" class="form-control col-9"></asp:TextBox>
                <asp:TextBox ID="TxtDash" runat="server" class="form-control col-3"></asp:TextBox>
            </div>
        </div>

        <div class="col-3">
            <label>กระบวนการ (Process)</label>
            <div class="input-group mb-3">
                <div class="input-group-prepend">
                    <span class="input-group-text"><i class="far fa-id-card"></i></span>
                </div>
                <asp:DropDownList ID="DrpProcess" runat="server" class="form-control"></asp:DropDownList>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-auto justify-content-end">
            <asp:Button ID="BtnSave" runat="server" Text="บันทึก" class="btn btn-success" />
            <asp:Button ID="BtnClear" runat="server" Text="ล้างรายการ" class="btn btn-danger" />
        </div>
    </div>
    <br />

    <asp:GridView ID="GrdDrawing" runat="server" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="2" CssClass="col-12 text-center" AllowPaging="True" AllowSorting="True" PageSize="20">
        <Columns>
            <asp:ButtonField ButtonType="Button" CommandName="Select" Text="แก้ไข" ControlStyle-CssClass="btn btn-outline-warning">
<ControlStyle CssClass="btn btn-outline-warning"></ControlStyle>
            </asp:ButtonField>
            <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="ลบ" ControlStyle-CssClass ="btn btn-outline-danger"/>
        </Columns>
        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
        <RowStyle BackColor="White" ForeColor="#003399" />
        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
        <SortedAscendingCellStyle BackColor="#EDF6F6" />
        <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
        <SortedDescendingCellStyle BackColor="#D6DFDF" />
        <SortedDescendingHeaderStyle BackColor="#002876" />
    </asp:GridView>

    <asp:TextBox ID="TxtTest" runat="server" TextMode="MultiLine"></asp:TextBox>
</asp:Content>
