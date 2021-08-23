<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Upload.aspx.vb" Inherits="Drawing.Upload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container">
        <div class="row">
            <div class="col-4">
                <div class="form-row">
                    <div class="form-group col-9">
                        <asp:Label ID="LblDrawing" runat="server" Text="Drawing No."></asp:Label>
                        <asp:TextBox ID="TxtDwgNo" runat="server" class="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group col-3">
                        <asp:Label ID="LblRev" runat="server" Text="Rev."></asp:Label>
                        <asp:TextBox ID="TxtRev" runat="server" class="form-control"></asp:TextBox>
                    </div>
                </div>

                <div class="form-group">
                    <asp:Label ID="LblPartNo" runat="server" Text="Part No."></asp:Label>
                    <div class="input-group">
                        <asp:TextBox ID="TxtDash" runat="server" class="form-control col-3" placeholder="No.XX"></asp:TextBox>
                        <asp:TextBox ID="TxtPartNo" runat="server" class="form-control col-9"></asp:TextBox>
                        <div class="input-group-append">
                            <asp:Button ID="BtnAdd" runat="server" Text="Add" class="btn btn-primary"/>
                        </div>
                    </div>
                </div>
                <div class="form-group">
                    <div class="card">
                        <div class="card-body">
                            <asp:GridView ID="GrdPartNo" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Horizontal" CssClass="col-12 text-center">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="ลบ" ControlStyle-CssClass="btn btn-danger btn-sm"/>
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
                    </div>
                </div>
            </div>

            <div class="col-4">
                <div class="form-group">
                    <asp:Label ID="LblPartName" runat="server" Text="Part Name"></asp:Label>
                    <asp:TextBox ID="TxtPartName" runat="server" class="form-control col-12"></asp:TextBox>
                </div>
                <div class="form-group">
                    <asp:Label ID="LblProcess" runat="server" Text="Process"></asp:Label>
                    <asp:DropDownList ID="DrpProcess" runat="server" class="form-control col-12"></asp:DropDownList>
                </div>
                <div class="form-group">
                    <div class="card">
                        <div class="card-body">
                            <asp:GridView ID="GrdProcess" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="1" GridLines="Horizontal" CssClass="col-12 text-center">
                                <AlternatingRowStyle BackColor="#F7F7F7" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Button" CommandName="Delete" Text="ลบ" ControlStyle-CssClass="btn btn-danger btn-sm"/>
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
                    </div>
                </div>
            </div>
            <div class="col-4">
                
                <div class="form-group">
                    <asp:Label ID="LblUploadPdf" runat="server" Text="Pdf file Upload"></asp:Label>
                    <div class="input-group">
                            <asp:FileUpload ID="FileUploadPdf" runat="server" class="form-control" />
                        <div class="input-group-append">
                            <asp:Button ID="BtnView" runat="server" Text="View" class="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="mt-3">
            <asp:Button ID="BtnUpload" runat="server" Text="Upload file || อัพโหลดไฟล์" class="btn btn-success" />
            <asp:Button ID="BtnClear" runat="server" Text="Clear || ล้างรายการ" class="btn btn-danger" />
        </div>
    </div>

    <%--<asp:TextBox ID="TxtTest" runat="server" TextMode="MultiLine"></asp:TextBox>--%>
</asp:Content>