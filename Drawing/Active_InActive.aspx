<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Active_InActive.aspx.vb" Inherits="Drawing.Active_InActive" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="container">
        <div class ="row">
            <div class="col-6">
                <div class="card">
                    <div class="card-header">
                        <b>Active Drawing</b>
                        <div class="form-group row float-right">
                            DWG No.
                            <div class="col-auto">
                                <asp:TextBox ID="TxtSearchActive" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <asp:Button ID="BtnActiveSearch" runat="server" Text="ค้นหา" class="btn btn-outline-primary"/>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="GrdActive" runat="server" CellPadding="1" ForeColor="#333333" GridLines="None" CssClass="col-12 text-center" Font-Size="Small" AllowPaging="True" PageSize="25">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Select" Text="In-Active" ControlStyle-CssClass="btn btn-danger" ControlStyle-Font-Size="X-Small"/>
                            </Columns>
                            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Left" />
                            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                            <SortedAscendingCellStyle BackColor="#FDF5AC" />
                            <SortedAscendingHeaderStyle BackColor="#4D0000" />
                            <SortedDescendingCellStyle BackColor="#FCF6C0" />
                            <SortedDescendingHeaderStyle BackColor="#820000" />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="col-6">
                <div class="card">
                    <div class="card-header">
                        <b>In-Active Drawing</b>
                        <div class="form-group row float-right">
                            DWG No.
                            <div class="col-auto">
                                <asp:TextBox ID="TxtSearchInActive" runat="server" class="form-control"></asp:TextBox>
                            </div>
                            <asp:Button ID="BtnInActiveSearch" runat="server" Text="ค้นหา" class="btn btn-outline-primary"/>
                        </div>
                    </div>
                    <div class="card-body">
                        <asp:GridView ID="GrdInActive" runat="server" CellPadding="1" ForeColor="#333333" GridLines="None" CssClass="col-12 text-center" Font-Size="Small" PageSize="25" AllowPaging="True">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:ButtonField ButtonType="Button" CommandName="Select" Text="Active" ControlStyle-CssClass="btn btn-success" ControlStyle-Font-Size="X-Small">
<ControlStyle CssClass="btn btn-success"></ControlStyle>
                                </asp:ButtonField>
                            </Columns>
                            <EditRowStyle BackColor="#7C6F57" />
                            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Left" />
                            <RowStyle BackColor="#E3EAEB" />
                            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F8FAFA" />
                            <SortedAscendingHeaderStyle BackColor="#246B61" />
                            <SortedDescendingCellStyle BackColor="#D4DFE1" />
                            <SortedDescendingHeaderStyle BackColor="#15524A" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <%--<asp:TextBox ID="TxtTest" runat="server" TextMode="MultiLine"></asp:TextBox>--%>
</asp:Content>
