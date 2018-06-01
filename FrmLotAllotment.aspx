<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="FrmLotAllotment.aspx.cs" Inherits="FrmLotAllotment" Title="Amravati University" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>--%>
    <script src="bootstrap-3.3.6-dist/js/jquery-1.8.3.min.js"></script>
    <script type="text/javascript">
        $(function() {
        $('[id*=chkselect]').on('change', function() {
                var value = 0;
                $('[id*=chkselect]:checked').each(function() {
                    var row = $(this).closest('tr');
                    value = value + parseInt(row.find('[id*=TotalPages]').html());
                });
                $('[id*=lblTotal]').html("Total Pages :- " +value);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="bootstrap-3.3.6-dist/js/jquery.min.js" type="text/javascript"></script>
    
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-12 col-md-12 col-sm-12 text-center">
                <div class="h3 text-primary">
                    <b>Register No. Allotment For Users</b></div>
                <asp:UpdatePanel ID="Updp" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4">
                                <div class="row form-group">
                                    <label class="col-lg-4 col-md-4 col-sm-6 control-label text-left">
                                        User Name
                                    </label>
                                    <div class="col-lg-7 col-md-7 col-sm-6">
                                        <asp:DropDownList runat="server" ID="ddlUserName" AutoPostBack="true" class="form-control"
                                            TabIndex="1">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-4 col-md-4 col-sm-4">
                                <div class="row form-group">
                                    <label class="col-lg-5 col-md-5 col-sm-6 control-label">
                                        Punching Type
                                    </label>
                                    <div class="col-lg-7 col-md-7 col-sm-6">
                                        <asp:DropDownList runat="server" ID="ddlAllotmentNo" class="form-control" TabIndex="2">
                                            <asp:ListItem Value="0">.....Select.....</asp:ListItem>
                                            <asp:ListItem Value="1">First Time</asp:ListItem>
                                            <asp:ListItem Value="2">Second Time</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-lg-2 col-md-2 col-sm-2">
                            </div>
                        </div>
                        <br />
                        <div class="form-group" style="display: block; margin: -24px 13px;">
                            <div style="width: 100%; background-color:rgba(22, 94, 154, 0.85); border: 1px solid #084B8A; color: #ffffff;
                                font-weight: bold;">
                                <table rules="all" width="100%">
                                    <tr>
                                        <%--<td style="width: 61px; color: White;">
                                ALL
                                <asp:CheckBox ID="chkboxSelectAll" Style="text-align: center; height: 13px" runat="server"
                                    onclick="CheckAllEmp(this);" AutoPostBack="true" />
                            </td>--%>
                                        <td style="width: 37px;">
                                            Sr No.
                                        </td>
                                        <td style="width: 70px; color: White">
                                            Foil RegNo
                                        </td>
                                         <td style="width: 40px; color: White">
                                            Foil Pages
                                        </td>
                                        <td style="width: 78px; color: White">
                                            CF RegNo
                                        </td>
                                        <td style="width: 40px; color: White">
                                            CF Pages
                                        </td>
                                        <td style="width: 353px; color: White">
                                            Exam Name
                                        </td>
                                        <td style="width: 48px; color: White">
                                            Check
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div align="center" style="overflow: auto; width: 101%; height: 303px;">
                                <asp:GridView align="center" ID="grdEntry" runat="server" ShowHeader="false" HeaderStyle-BackColor="#61A6F8"
                                    HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="100%" CellPadding="4"
                                    RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                    BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="True" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Height="1px" ItemStyle-Height="30px"
                                            ItemStyle-Width="41px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Foil RegNo" ItemStyle-Width="78px"  ItemStyle-Height="30px"
                                            DataField="RegisterNo" />
                                             <asp:TemplateField HeaderText="Foil Pages" HeaderStyle-Height="1px" ItemStyle-Height="30px"
                                            ItemStyle-Width="49px">
                                            <ItemTemplate>
                                                <asp:Label ID="TotalPages" Text='<%# Eval("TotalPages") %>' runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="CF RegNo" ItemStyle-Width="88px" ItemStyle-Height="30px"
                                            DataField="RegNoCF" />
                                        <asp:BoundField HeaderText="CF Pages" ItemStyle-Width="49px" ItemStyle-Height="30px"
                                            DataField="TotalPages2" />                                 
                                        <asp:BoundField HeaderText="Exam Name" ItemStyle-Width="396px" ItemStyle-Height="30px"
                                            DataField="ExamShortName" />
                                        <asp:TemplateField ItemStyle-CssClass="text-center" ItemStyle-Height="30px" ItemStyle-Width="48px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkselect" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                              </asp:GridView>    
                            </div>
                            <asp:Label ID="lblTotal" runat="server" Text=""  ForeColor="red" style="font-size:18px"></asp:Label>
                            <br />
                            <div class="row">
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                    <div class="row">
                                        <div class="col-lg-6 col-md-6 col-sm-6">
                                            <%--<input type="submit" class="btn btn-block btn-success" value="Save" />--%>
                                            <asp:Button runat="server" class="btn btn-block btn-success" Text="Save" ID="btnSave"
                                                OnClick="btnSave_Click" TabIndex="3" />
                                        </div>
                                        <div class="col-lg-6 col-md-6 col-sm-6">
                                            <%--<input type="submit" class="btn btn-block btn-primary" value="Home" />--%>
                                            <asp:Button runat="server" class="btn btn-block btn-success" Text="Clear" ID="btnClear"
                                                OnClick="btnClear_Click" TabIndex="4" />
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-4 col-md-4 col-sm-4">
                                </div>
                                <div class="col-lg-12 col-md-12 col-sm-12">
                                    <asp:Label runat="server" ID="lblmsg" Text="" Style="color: Red; font-family: Times New Roman;
                                        font-weight: 500; font-size: 18px">
                                    </asp:Label>
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
</asp:Content>
