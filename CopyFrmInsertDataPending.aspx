<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeFile="CopyFrmInsertDataPending.aspx.cs" Inherits="CopyFrmInsertDataPending" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">

        .tbl-head{
            background-color:#337ab7;
            color:#fff;
        }
        .tbl-head a{
            color:#fff;
        }
        /*.tbl-head a:hover{
            color:black;
        }*/
        /*.tbl-head:hover{
            background-color:#fff;
            color:#337ab7;
        }*/

        .imgt {
            height: 100px;
            width: 100px;
        }

        section {
            text-align: center; /*margin: 50px 0;*/
        }

        .panzoom-parent {
            border: 2px solid #333;
        }

            .panzoom-parent .panzoom {
                border: 2px dashed #666;
            }

        .buttons {
            margin: 112px 0 0;
        }

        .btn-change {
            margin-bottom: 10px;
        }

        .m-0 {
            margin: 0;
        }

        .viewport {
            position: absolute;
            overflow: hidden;
            top: 100px;
            left: 17px;
            width: 120%;
            height: 545px;
        }

        #image {
            height: 545px;
            width: 1000px;
        }
        /*#image
        {
            -webkit-tap-highlight-color: transparent;
            transform: translate(322.385px, -5.05769px) scale(2.1, 2.1);
        }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" src="libs/jquery/jquery.js"></script>

    <script type="text/javascript" src="libs/jquery/jquery-ui.js"></script>

    <script type="text/javascript" src="libs/jquery.fs.zoetrope.min.js"></script>

    <script type="text/javascript" src="libs/toe.min.js"></script>

    <script type="text/javascript" src="libs/jquery.mousewheel.min.js"></script>

    <script type="text/javascript" src="src/imgViewer.js"></script>


    <script type="text/javascript">
        function Prev() {

            $.ajax({
                type: "POST",
                url: "ImgViewer.aspx/Prev",
                data: '{str: "' + $("#<%=Session["RegNo"]%>")[0].value + '"}',
                contentType: "application/json; c harset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d);

                },
                failure: function (response) {

                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }

        function Next() {

            $.ajax({
                type: "POST",
                url: "ImgViewer.aspx/Next",
                data: '{str: "' + $("#<%=Session["RegNo"]%>")[0].value + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    alert(response.d);
                },
                failure: function (response) {

                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }
    </script>

    <script type="text/javascript">

        function ValidateCFForm() {

            if ($('#<%=txtRollNoFrom.ClientID%>').val() == '') {
                $('#<%=txtRollNoFrom.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter RollNoFrom', 'Error', function () { $("#txtRollNoFrom").focus() })
                return false;
            }
            else {
                $('#<%=txtRollNoFrom.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtRollNoTo.ClientID%>').val() == '') {
                $('#<%=txtRollNoTo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter RollNo To', 'Error', function () { $("#txtRollNoTo").focus() })
                return false;
            }
            else {
                $('#<%=txtRollNoTo.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtRollNoTo.ClientID%>').val() < ('#<%=txtRollNoFrom.ClientID%>').val()) {
                $('#<%=txtRollNoTo.ClientID%>').css('border-color', 'Red');
                jAlert('Roll No From Must Be Less Than of Roll No To', 'Error', function () { $("#txtRollNoTo").focus() })
                return false;
            }
            else {
                $('#<%=txtRollNoTo.ClientID%>').css('border-color', '');
            }
        };
    </script>

    <div class="col-lg-12">
        <div class="form-group">
            <asp:Label runat="server" ID="lblRegNo" Style="font-weight: 600; margin-top: 4px;"
                class="col-sm-1 col-md-1 col-lg-1" Text="RegNo.">
            </asp:Label>
            <div class="col-sm-1 col-md-1 col-lg-1">
                <asp:TextBox ID="txtRegNo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                    TargetControlID="txtRegNo">
                </cc1:FilteredTextBoxExtender>
            </div>
            <div class="col-sm-2 col-md-6 col-lg-6">
                <asp:Label runat="server" ID="lblPageNo" Style="font-weight: 600;" class="col-sm-2 col-md-2 col-lg-2"
                    Text="Page No."></asp:Label>
                <div class="col-sm-3 col-md-2 col-lg-2">
                    <asp:TextBox ID="txtPageNo" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                        TargetControlID="txtPageNo">
                    </cc1:FilteredTextBoxExtender>

                </div>
                <div class="col-sm-2 col-md-2 col-lg-2">
                    <asp:LinkButton ID="btnSearchImg" CssClass="btn btn-sm btn-primary" runat="server"
                        Text="" OnClick="btnSearchImg_Click" OnClientClick="return ValidateRegister();"><i class="fa fa-show"> </i>&nbsp;Show</asp:LinkButton>
                </div>
                <div class="col-sm-5 col-md-5 col-lg-5">
                    <asp:Label runat="server" ID="lblDetails" Style="font-family: Times New Roman; margin-top: 4px;"
                        CssClass="text-danger badge" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

    <asp:Panel runat="server" ID="pnlImageViewer" Visible="false">
        <div class="">
            <asp:Panel ID="pnlTrType" runat="server" Visible="true">
                <div class="col-lg-10 col-md-10">
                    <div class="row">
                        <div class="col-md-4 col-lg-4 text-center ">
                            <asp:Label ID="lblFoil" Style="margin-left: 14px; color: Blue; text-align: center; margin-top: 2px; color: #003F8E; font-weight: 600;"
                                Text="" runat="server"></asp:Label>
                        </div>
                        <div class="col-md-4 col-lg-4 text-center">
                            <asp:Label ID="lblCF" Style="margin-left: 14px; color: Blue; text-align: center; margin-top: 2px; color: #003F8E; font-weight: 600;"
                                Text=""
                                runat="server"></asp:Label>
                        </div>
                    </div>
            </asp:Panel>
            <div class="col-lg-12">
                <div class="col-md-9 col-lg-7">
                    <div class="row">
                        <div class="col-md-10 col-lg-11">
                            <%--  <section>--%>
                            <div class="parent" style="text-align: center; height: 553px; width: 120%; position: static; border: 2px solid #50618C;"
                                id="">
                                <div class="panzoom" style="height: 550px; width: 114%">
                                    <div class="col-md-5 col-lg-6" style="margin-left: -15px;">
                                        <asp:DataList ID="dlImages" runat="server">
                                            <ItemTemplate>
                                                <table border="2px" style="border-color: Green" width="100%">
                                                    <tr>
                                                        <td>
                                                            <img id="image" src='<%# Eval("FilePath")%>' alt="" class="img-responsive" style="position: relative; top: 0px; left: 0px; width: 100%; height: 545px;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <asp:HiddenField ID="FilePathImg" runat="server" />
                                    <asp:HiddenField ID="RegNoFoil" runat="server" />
                                    <div class="col-md-5 col-lg-5">
                                        <asp:DataList ID="dlImagesCF" runat="server">
                                            <ItemTemplate>
                                                <table width="107%" style="margin-left: -29px; border: Solid 2px Red">
                                                    <tr>
                                                        <td>
                                                            <img id="imageCF" src='<%# Eval("FilePath")%>' alt="" class="img-responsive" style="top: 0px; left: 0px; width: 100%; height: 545px;" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                    <asp:HiddenField ID="Filetype" runat="server" />
                                    <asp:HiddenField ID="RegNoCF" runat="server" />
                                    <asp:HiddenField ID="FilePathImgCF" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-lg-5">

                    <div class="row">
                        <div class="col-lg-9 col-md-9" style="margin-left: 80px;">
                            <div class="box">
                                <asp:Label runat="server" Style="text-align: center; font-family: Times New Roman; color: Blue; font-weight: 600"
                                    Text="Foil Entry" ID="lblFoilEntry"></asp:Label>
                                <asp:UpdatePanel runat="server" ID="pnlFoil">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="pnlcounter" Visible="true">
                                            <div class="table table-responsive m-0">
                                                <table class="table table-bordered">
                                                    <thead class="">
                                                        <tr>
                                                            <th class="col-lg-1" style="text-align: center">Sr No.
                                                            </th>
                                                            <th class="col-lg-5" style="text-align: center">Roll No.
                                                            </th>
                                                            <th class="col-lg-5" style="text-align: center"></th>
                                                            <th></th>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSr1" Text="1" runat="server"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtSRNo" Text="" CssClass="form-control input-sm" MaxLength="8"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <cc1:FilteredTextBoxExtender ID="fNumber" runat="server" FilterType="Numbers" TargetControlID="txtSRNo">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <td>
                                                                <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add"
                                                                    OnClick="btnAdd_Click" OnClientClick="return ValidateRegister();"></asp:LinkButton>
                                                                <%-- <asp:TextBox ID="txtStudentName" Text="" CssClass="form-control input-sm " Style="text-transform: uppercase"
                                                                    runat="server"></asp:TextBox>--%>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td>
                                                                <asp:CheckBox ID="ChkAll" runat ="server" Text="  Select All" OnCheckedChanged="ChkAll_CheckedChanged" />
                                                            </td>
                                                        </tr>--%>
                                                    </thead>
                                                    <tbody>
                                                        <asp:GridView ID="grdEntry" AllowSorting="true" OnSorting="grdEntry_Sorting" CssClass="table text-center table-hover m0" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdEntry_RowDeleting">
                                                            <HeaderStyle CssClass="tbl-head" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderStyle-Height="1px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Height="1px" />
                                                                    <ItemStyle Width="15%" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Roll No." HeaderStyle-CssClass="text-center" DataField="RollNo"  SortExpression="RollNo" />
                                                                
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" Style="text-align: center"
                                                                            CommandName="Delete" ForeColor="Red" OnClientClick="return confirm ('Do You Really Want To Delete ?')">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField ItemStyle-Width="40px">
                                                                    <HeaderTemplate>
                                                                        <div class="text-center">
                                                                            <asp:CheckBox ID="ChkSelectAll" runat="server" onclick="CheckAll(this);" />
                                                                        </div>
                                                                    </HeaderTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="ChkGridRow" runat="server"></asp:CheckBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                        <div class="form-group m-0" style="margin-left:0; margin-right:0">
                                                                <asp:LinkButton ID="LinkDeleteAll" runat="server" Text="Delete Selected Records" ForeColor="Red" OnClick="LinkDeleteAll_Click"></asp:LinkButton>
                                                            <div class="pull-right">
                                                                <asp:LinkButton CssClass="btn btn-default" ID="btnUp" OnClick="btnUp_Click" runat="server"><i class="glyphicon glyphicon-arrow-up"></i> Up</asp:LinkButton>
                                                                <asp:LinkButton CssClass="btn btn-default" ID="btnDown" OnClick="btnDown_Click" runat="server" ><i class="glyphicon glyphicon-arrow-down"></i> Down</asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </tbody>
                                                    <tr>
                                                        <td>
                                                            
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                
                                <asp:Panel runat="server" ID="pnlCf" Visible="true">
                                    <div class="box">
                                        <asp:Label runat="server" Style="text-align: center; font-family: Times New Roman; font-weight: 600"
                                            Text="Counter Foil Entry" ID="lblCFEntry"></asp:Label>
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th class="col-lg-5">Roll No. From</th>
                                                    <th class="col-lg-5">Roll No. To</th>
                                                </tr>
                                            </thead>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtRollNoFrom" Text="" CssClass="form-control input-sm" MaxLength="8"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtRollNoFrom">
                                                </cc1:FilteredTextBoxExtender>
                                                <td>
                                                    <asp:TextBox ID="txtRollNoTo" Text="" CssClass="form-control input-sm " Style="text-transform: uppercase"
                                                        runat="server"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                        TargetControlID="txtRollNoTo">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                </asp:Panel>
                                <asp:Panel runat="server" ID="Panel1" Visible="true">
                                    <div class="box">
                                        <table class="table table-bordered">
                                            <thead>
                                                <tr>
                                                    <th class="col-lg-4 text-center ">
                                                        <div style="margin-bottom: 5%;">
                                                            <asp:CheckBox ID="chkverified" AutoPostBack="true" runat="server" />&nbsp&nbsp  Data Verify 
                                                        </div>
                                                    </th>
                                                    <th class="col-lg-4 text-center">
                                                        <asp:Button runat="server" Style="margin-left: 15px" ID="btnUpdate" Text="Update" CssClass="btn btn-primary" OnClick="btnUpdate_Click" OnClientClick="Confirm();" />
                                                    </th>
                                                    <th class="col-lg-4 text-center ">
                                                        <asp:Button runat="server" ID="btnClear" Text="Clear" CssClass="btn btn-primary" OnClick="btnClear_Click" />
                                                    </th>
                                                </tr>
                                            </thead>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </div>
            </div>
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
            </div>
    </asp:Panel>

    <script type="text/javascript">
        (function ($) {
            $("#image").imgViewer({
                onClick: function (e, self) {
                    var pos = self.cursorToImg(e.pageX, e.pageY);
                    $("#position").html(e.pageX + " " + e.pageY + " " + pos.x + " " + pos.y);
                    $(this).rotate(90);
                }
            });

        })(jQuery);

        (function ($) {
            $("#imageCF").imgViewer({
                onClick: function (e, self) {
                    var pos = self.cursorToImg(e.pageX, e.pageY);
                    $("#position").html(e.pageX + " " + e.pageY + " " + pos.x + " " + pos.y);
                    $(this).rotate(90);
                }
            });

        })(jQuery);

    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            $('#divSection').bind('wheel mousewheel', function (e) {
                var delta;

                if (e.originalEvent.wheelDelta !== undefined)
                    delta = e.originalEvent.wheelDelta;
                else
                    delta = e.originalEvent.deltaY * -1;

                if (delta > 0) {
                    $("#divSection").css("width", "+=10");
                }
                else {
                    $("#divSection").css("width", "-=10");
                }
            });
        });

    </script>

    <script type="text/javascript" src="bootstrap-3.3.6-dist/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="bootstrap-3.3.6-dist/js/jquery.slides.min.js"></script>
    <script type="text/javascript" src="bootstrap-3.3.6-dist/js/jquery-1.11.2.min.js"></script>
    
    <script type="text/javascript" language="javascript">
        function CheckAll(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=grdEntry.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            if ($('#<%=chkverified.ClientID%>').is(':checked')) {
                if (confirm("Do You Want To Verify Data?")) {
                    $('#<% =hdnval.ClientID %>').val('1');
                         }
                         else {
                             $('#<% =hdnval.ClientID %>').val('0');
                         }
                     }
                     else {
                         $('#<% =hdnval.ClientID %>').val('null');
            }
        }
    </script>

    <asp:HiddenField ID="hdnval" runat="server" />
</asp:Content>

