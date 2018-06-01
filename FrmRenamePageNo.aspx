<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeFile="FrmRenamePageNo.aspx.cs" Inherits="FrmRenamePageNo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .shad {
            box-shadow: 0px 2px 2px 0px rgba(132, 108, 108, 0.67);
        }

        #image {
            height: 545px;
            width: 1000px;
        }

        .lbl {
            font-weight: bold;
        }

        .p0 {
            padding: 0;
        }

        .mbox {
            display: -webkit-box;
            background-color: white;
            padding: 9px;
            border-radius: 7px;
            box-shadow: 2px 2px 5px 0px rgba(51, 51, 51, 0.62);
        }

        @media(min-width: 800px) {
            img {
                width: 100%;
                height: 546px;
            }
        }

        @media (min-width: 600px) {
            img {
                width: 100%;
                height: 546px;
            }
        }

        .wrap {
            overflow: hidden;
            border: 1px solid;
        }
    </style>
    <script type="text/javascript" src="js/wheelzoom.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <div class="container-fluid">
        <div class="form-group">
            <asp:Label CssClass="col-md-1 lbl" runat="server" ID="lblRegNo" Text="RegNo." />
            <div class="col-md-1">
                <asp:TextBox ID="txtRegNo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtRegNo"></cc1:FilteredTextBoxExtender>
            </div>
            <div class="col-md-6">
                <asp:Label class="col-md-2 lbl" runat="server" ID="lblPageNo" Text="Page No."></asp:Label>
                <div class="col-md-2">
                    <asp:TextBox ID="txtPageNo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtPageNo"></cc1:FilteredTextBoxExtender>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnSearchImg" CssClass="btn btn-sm btn-primary" runat="server" Text="" OnClick="btnSearchImg_Click" OnClientClick="return ValidateRegister();"><i class="fa fa-show"> </i>&nbsp;Show</asp:LinkButton>
                </div>
                <div class="col-md-5">
                    <asp:Label runat="server" ID="lblDetails" CssClass="text-danger badge" Text=""></asp:Label>
                </div>
            </div>
            <div class="col-md-4" runat="server" id="divRenamePage" visible="false">
                <div class="mbox">
                    <label class="col-md-5">Rename Page No.</label>
                    <div class="col-md-3">
                        <asp:TextBox CssClass="form-control" MaxLength="6" ID="txtRenamePage" runat="server" />
                        <cc1:FilteredTextBoxExtender ID="FilteredtxtRenamePage" runat="server" FilterType="Numbers" TargetControlID="txtRenamePage"></cc1:FilteredTextBoxExtender>
                    </div>
                </div>
            </div>
        </div>

        <asp:Panel class="mbox" runat="server" ID="pnlImageViewer" Visible="false">
            <div class="col-md-4 p0 wrap">
                <%--<label class="col-md-12 shad" >Foil Entry</label>--%>
                <asp:Label  class="col-md-12 shad" ID="lblFoilEntry" runat="server" />
                <div>
                    <asp:Image class="zoom2" ID="image" runat="server" />
                    <script type="text/javascript">
                        wheelzoom(document.querySelector('img.zoom2'), { zoom: 1.05 });
                    </script>
                </div>
            </div>

            <div class="col-md-4 p0 wrap">
                <%--<label class="col-md-12 shad">Counter Foil Image</label>--%>
                <asp:Label  class="col-md-12 shad" ID="lblCF" runat="server" />
                <div>
                    <asp:Image class="zoom" ID="imageCF" runat="server" /><%--Width="730px" Height="460px"--%>
                    <script type="text/javascript">
                        wheelzoom(document.querySelector('img.zoom'), { zoom: 1.05 });
                    </script>
                </div>
            </div>

            <div class="col-md-4">
                <div class="">
                    <label class="text-center">Foil Entry</label>
                    <asp:GridView ID="grdEntry" CssClass="table text-center table-hover m0" runat="server" AutoGenerateColumns="False">
                        <HeaderStyle CssClass="bg-primary form-control-static" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderStyle-Height="1px">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                </ItemTemplate>
                                <HeaderStyle Height="1px" />
                                <ItemStyle Width="15%" />
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Roll No." HeaderStyle-CssClass="text-center" DataField="RollNo" />
                        </Columns>
                    </asp:GridView>

                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th class="col-lg-5">Roll No. From</th>
                                <th class="col-lg-5">Roll No. To</th>
                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtRollNoFrom" Text="" CssClass="form-control input-sm" MaxLength="8" ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtRollNoTo" CssClass="form-control input-sm " Style="text-transform: uppercase" ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>

                    <div class="box">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th class="col-lg-4 text-center">
                                        <asp:Button runat="server" Style="margin-left: 15px" ID="btnUpdate" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" OnClientClick="return Confirm();" />
                                    </th>
                                    <th class="col-lg-4 text-center ">
                                        <asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                    </th>
                                </tr>
                            </thead>
                        </table>
                    </div>

                </div>
            </div>
            <div class="col-md-3">
                <div class="row">                    
                    <asp:Label ID="lblmsg" runat="server" CssClass="text-danger" Text=""></asp:Label>
                    <asp:Label ID="lblmsgCF" runat="server" CssClass="text-danger" Text=""></asp:Label>
                </div>
                <div class="row">
                    <asp:Label runat="server" ID="lblInd" Visible="false" Text="" CssClass="text-danger"></asp:Label>
                    <br />
                    <asp:Label runat="server" ID="lblLotNo" Visible="false" Text="" CssClass="text-danger"></asp:Label>
                    <asp:Label runat="server" ID="lblTotalPages" Text="" ForeColor="Transparent"></asp:Label>
                </div>
            </div>
        </asp:Panel>
        <div class="form-group"></div>

    </div>
    <script type="text/javascript" language="javascript">
        function CheckAll(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdEntry.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    
        function Confirm() {
            var r = confirm("Do You Want To Update!");
            if (r == true) {
                debugger
                return false;
            } else {
                return true;
            }
        }
    </script>

    <asp:HiddenField ID="hdnval" runat="server" />
</asp:Content>

