<%@ Page Title="CycleTime Calculator" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="CycletimeCalculator.aspx.vb" Inherits="Drawing.CycletimeCalculator" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <h4>คำนวนเวลาการทำงาน (Cycletime)</h4>

    <div class="card">
        <div class="card-header">
            ค่าคงที่ (Constant)
        </div>
        <div class="card-body">
            <asp:TextBox ID="TxtRev" runat="server"></asp:TextBox>
        </div>
    </div>
    <br />
    <div class="card">
        <div class="card-header">
            คำนวน (Calculate)
            <div class="input-group col-12 my-3">
                <div class="input-group-prepend">
                    <span class="input-group-text" id="">Spec : </span>
                    <asp:DropDownList ID="DrpSpec" runat="server" class="btn btn-outline-secondary bg-light text-secondary dropdown-toggle">
                        <asp:ListItem></asp:ListItem>
                        <asp:ListItem>ID</asp:ListItem>
                        <asp:ListItem>OD</asp:ListItem>
                        <asp:ListItem>Facing</asp:ListItem>
                    </asp:DropDownList>
                </div>
                <asp:TextBox ID="TxtLength" runat="server" class="form-control" placeholder="Cutting Length (mm)"></asp:TextBox>
                <asp:TextBox ID="TxtInsideDia" runat="server" class="form-control" placeholder="(Outer) Diameter (mm)"></asp:TextBox>
                <asp:TextBox ID="TxtOutsideDia" runat="server" class="form-control" placeholder="(Inner) Diameter (mm)"></asp:TextBox>
                <asp:TextBox ID="TxtFeed" runat="server" class="form-control" placeholder="Feed amount (mm/rev)"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="BtnAdd" runat="server" Text="เพิ่ม" class="btn btn-primary" />
                </div>
            </div>
        </div>
        <div class="card-body">

            <asp:GridView ID="GrdCalCyTime" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Horizontal" CssClass="col-12 text-center">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="ลบ" ControlStyle-CssClass="btn btn-danger"/>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                <SortedAscendingCellStyle BackColor="#F4F4FD" />
                <SortedAscendingHeaderStyle BackColor="#5A4C9D" />
                <SortedDescendingCellStyle BackColor="#D8D8F0" />
                <SortedDescendingHeaderStyle BackColor="#3E3277" />
            </asp:GridView>

        </div>
        <div class="card-footer text-muted">
            <asp:Label ID="LblSum" runat="server" Text="Total CycleTime : "></asp:Label>
        </div>
    </div>
</asp:Content>
                   