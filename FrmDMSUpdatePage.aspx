<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmDMSUpdatePage.aspx.cs" Inherits="FrmDMSUpdatePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .zoom {
            border: 1px solid #ddd;
            width: 790px;
            height: 400px;
            margin: auto;
        }

        .form-group {
            /*margin-bottom:5px;*/
        }

        .table {
            margin-bottom: 5px;
        }

        .maxheight {
            max-height: 300px;
            overflow: auto;
        }
    </style>
    <script src="bootstrap-3.3.6-dist/js/jquery-1.9.0.js"></script>
    <script>
        function numericFilter(txb) {
            txb.value = txb.value.replace(/[^\0-9]/ig, "");
        }
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-1">Register No.</label>
            <div class="col-md-2">                
                <asp:TextBox MaxLength="10" CssClass="form-control" ID="txtRegNo" onKeyUp="numericFilter(this)" placeholder="Enter Register No." runat="server" />
            </div>
            <div class="col-md-3">
                <asp:RadioButtonList OnSelectedIndexChanged="RblRegType_SelectedIndexChanged" AutoPostBack="true" ID="RblRegType" RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem style="padding: 0px 15px;" Selected="True" Text="Out Of Range" />
                    <asp:ListItem style="padding: 0px 15px;" Text="With In Range" />
                </asp:RadioButtonList>
            </div>
            <div class="col-md-2">
                <asp:LinkButton CssClass="btn btn-default" ID="btnShow" OnClick="btnShow_Click" runat="server"><i class="fa fa-expand"></i> Show</asp:LinkButton>
                <asp:LinkButton CssClass="btn btn-danger" ID="btnClear" OnClick="btnClear_Click" runat="server"> <i class="fa fa-trash"></i> Clear</asp:LinkButton>
            </div>
            <div class="col-md-4">
                <asp:Label CssClass="text-danger" ID="lblMsg" runat="server" />
            </div>
        </div>
        <div class="form-group" id="divHideRegi" runat="server" visible="false">
            <div class="col-md-8">
                <asp:GridView CssClass="table table-bordered maxheight" ID="gvPages" AutoGenerateColumns="false" OnRowCommand="gvPages_RowCommand" runat="server">
                    <Columns>
                        <asp:BoundField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" DataField="RecID" HeaderText="Sr. No." />
                        <asp:BoundField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" DataField="FoilPageNo" HeaderText="Page No." />
                        <asp:BoundField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" DataField="FoilFilePath" HeaderText="Foil File Path" />
                        <asp:TemplateField ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center" HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton CommandName="ShowImg" ID="linkEditPage" runat="server"> Edit</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="form-group" id="divEditEntry" visible="false" runat="server">
            <div class="col-md-8">
                <div class="panel panel-primary">
                    <div class="panel-heading">Foil Image</div>
                    <div class="">
                        <%--panel-body--%>
                        <asp:Image ID="imgFilePath" class="zoom img-responsive" runat="server" />
                        <script src="js/wheelzoom.js"></script>
                        <script>
                            wheelzoom(document.querySelector('img.zoom'), { zoom: 2.05 });
                        </script>
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="panel-group">
                    <div class="panel panel-primary" id="pnlFoil" runat="server" visible="false">
                        <div class="panel-heading">Foil Page</div>
                        <div class="panel-body">
                            <label class="col-md-6">Current Page No.</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtFoilCur" onKeyUp="numericFilter(this)" CssClass="form-control" placeholder="Enter Foil Page No." runat="server" />
                            </div>
                            <div class="form-group"></div>
                            <label class="col-md-6">Change Page No.</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtFoilUpdate" onKeyUp="numericFilter(this)" CssClass="form-control" placeholder="Enter Foil Page No." runat="server" />
                            </div>
                        </div>
                    </div>

                    <div class="panel panel-primary" id="pnlCF" runat="server" visible="false">
                        <div class="panel-heading">CF Page</div>
                        <div class="panel-body">

                            <label class="col-md-6">Current Page No.</label>
                            <div class="col-md-6 ">
                                <asp:TextBox ID="txtCFCur" onKeyUp="numericFilter(this)" CssClass="form-control" placeholder="Enter CF Page No." runat="server" />
                            </div>
                            <div class="form-group"></div>
                            <label class="col-md-6">Change Page No.</label>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtCFUPdate" onKeyUp="numericFilter(this)" CssClass="form-control" placeholder="Enter CF Page No." runat="server" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-md-12">
                    <asp:LinkButton CssClass="btn btn-primary" ID="btnUpdate" OnClick="btnUpdate_Click" runat="server"> <i class="fa fa-random"></i> Update</asp:LinkButton>
                </div>

                <%--<h5><u>Foil Page</u></h5>
                    <label class="col-md-6">Current Page No.</label> 
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFoilCur" CssClass="form-control" runat="server" />
                    </div>

                    <label class="col-md-6">Change Page No.</label> 
                    <div class="col-md-6">
                        <asp:TextBox ID="txtFoilChange" CssClass="form-control" runat="server" />
                    </div>--%>
            </div>
        </div>
    </div>
</asp:Content>

