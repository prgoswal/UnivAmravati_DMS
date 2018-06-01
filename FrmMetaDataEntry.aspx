<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeFile="FrmMetaDataEntry.aspx.cs" Inherits="FrmMetaDataEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
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
    </style>
    <script type="text/javascript">
        function ValidateRegister() {

            if ($('#<%=txtRegNo.ClientID%>').val() == '') {
                $('#<%=txtRegNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Register No.', 'Error', function () { $("#txtRegNo").focus() })
                return false;
            }
            else {
                $('#<%=txtRegNo.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtSRNo.ClientID%>').val() == '') {
                $('#<%=txtSRNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Student Roll No.', 'Error', function () { $("#txtSRNo").focus() })
                return false;
            }
            else {
                $('#<%=txtSRNo.ClientID%>').css('border-color', '');
            }

        };
    </script>
    <style type="text/css">
        .loader {
          border: 16px solid #f3f3f3;
          border-radius: 50%;
          border-top: 16px solid blue;
          border-bottom: 16px solid blue;
          width: 120px;
          height: 120px;
          -webkit-animation: spin 2s linear infinite;
          animation: spin 2s linear infinite;
        }

        @-webkit-keyframes spin {
          0% { -webkit-transform: rotate(0deg); }
          100% { -webkit-transform: rotate(360deg); }
        }

        @keyframes spin {
          0% { transform: rotate(0deg); }
          100% { transform: rotate(360deg); }
        }
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
                data: '{str: "' + $("#<%=txtRegNo.ClientID%>")[0].value + '"}',
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

        function Next() {

            $.ajax({
                type: "POST",
                url: "ImgViewer_New.aspx/Next",
                data: '{str: "' + $("#<%=txtRegNo.ClientID%>")[0].value + '"}',
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

        function ValidateRegister() {


            if ($('#<%=txtRegNo.ClientID%>').val() == '') {
                $('#<%=txtRegNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Register No.', 'Error', function () { $("#txtRegNo").focus() })
                return false;
            }
            else {
                $('#<%=txtRegNo.ClientID%>').css('border-color', '');
            }
        };
        function ValidateEntryForm() {


        };
    </script>

    <div class="row">
        <div class="col-lg-12">
            <div class="form-group">
                <asp:Label runat="server" ID="lblRegNo" Style="font-weight: 600; padding-left: 76px;" class="col-sm-2 col-md-2 col-lg-2"
                    Text="Enter Register No.">
                </asp:Label>
                <div class="col-sm-3 col-md-2 col-lg-2">
                    <asp:TextBox ID="txtRegNo" CssClass="form-control input-sm" placeholder="Enter Register No."
                        runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2 col-md-2 col-lg-2">
                    <asp:LinkButton ID="btnSearchImg" CssClass="btn btn-sm btn-primary" runat="server"
                        Text="" OnClick="btnSearchImg_Click"><i class="fa fa-search"> </i>&nbsp;Search Image</asp:LinkButton>
                </div>
                <div class="col-sm-4 col-md-6 col-lg-6">
                    <asp:Label runat="server" ID="lblDetails" Style="font-family: Times New Roman" ForeColor="Red"></asp:Label>
                    <asp:Label runat="server" ID="lblCount" Style="font-family: Times New Roman" ForeColor="Red"
                        Text=""></asp:Label>
                </div>
            </div>
        </div>

    </div>
    <asp:Panel runat="server" ID="pnlImageViewer" Visible="false">
        <div class="row">
            <div class="col-lg-12">
                <div class="col-md-8 col-lg-7">
                    <div class="row">
                        <div class="col-md-12 col-lg-12">

                            <div id="divSection">
                                <%--<asp:Panel ID="pnlimg" runat="server" class="parent" Style="text-align: center; border: 2px solid #50618C; height: 550px; width: 790px;">--%>
                                <div class="panzoom">
                                    <asp:Label ID="lblpath" runat="server" Font-Bold="true" ForeColor="Red" Style="font-family: 'Times New Roman'; margin-left: 340px;"></asp:Label>
                                    <asp:LinkButton ID="btnprev" CssClass="btn btn-primary btn-change" runat="server"
                                        OnClick="btnprev_Click"><i class="fa fa- fa-angle-double-left"></i>&nbsp;Prev</asp:LinkButton>
                                    <asp:LinkButton ID="btnnext" CssClass="btn btn-primary btn-change" runat="server"
                                        OnClick="btnnext_Click">Next&nbsp;<i class="fa fa- fa-angle-double-right"></i></asp:LinkButton>
                                    <asp:DataList ID="dlImages" runat="server" OnSelectedIndexChanged="dlImages_SelectedIndexChanged" Style="text-align: center; width: 980px; height: 510px;">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <img id="image" src='<%# Eval("FilePath")%>' alt="" class="img-responsive" style="text-align: center; border: 2px solid #50618C; width: 1040px; height: 510px;" />

                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>

                                    <asp:HiddenField ID="Filetype" runat="server" />
                                    <asp:HiddenField ID="RegNoCF" runat="server" />
                                    <asp:HiddenField ID="FilePathImg" runat="server" />
                                </div>
                                <%--</asp:Panel>--%>
                            </div>
                        </div>

                    </div>


                </div>
                <div class="col-md-1 col-lg-1">
                    <div class="buttons" style="margin-left: 160px;">
                        <button class="zoom-in" style="display: none">
                            Zoom In</button>
                    </div>

                </div>
                <div class="col-md-3 col-lg-5">
                    <asp:UpdatePanel runat="server" ID="pnlFoil">
                        <ContentTemplate>
                            <div class="row">
                                <div style="margin-left: 40%;" class="col-lg-7">
                                    <div class="box" style="margin-top: -85px;">

                                        <asp:Label runat="server" Style="text-align: center; font-family: Times New Roman; color: red; font-weight: 600; text-align: left;"
                                            ID="lblFoilEntry" Font-Size="25px"></asp:Label>
                                        <asp:Panel runat="server" ID="pnlcounter" Visible="true">
                                            <div class="table table-responsive">
                                                <table class="table table-bordered">
                                                    <thead class="">
                                                        <tr>
                                                            <th  style="text-align: center">Page No.
                                                            </th>
                                                            <th  style="text-align: center">Roll No.
                                                            </th>
                                                            <th  style="text-align: center"></th>
                                                       
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%--<asp:Label ID="lblSr1" Text="1" runat="server"></asp:Label>--%>
                                                                <asp:TextBox ID="txtpagenofoil" CssClass="form-control input-sm" runat="server" style="width:50px"></asp:TextBox>
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
                                                        
                                                        </tr>
                                                        <%--<tr>
                                                            <td>
                                                                <asp:CheckBox ID="ChkAll" runat ="server" Text="  Select All" OnCheckedChanged="ChkAll_CheckedChanged" />
                                                            </td>
                                                        </tr>--%>
                                                    </thead>
                                                    <tbody>
                                                        <asp:GridView ID="grdEntry" CssClass="table text-center" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdEntry_RowDeleting">
                                                            <HeaderStyle CssClass="bg-primary form-control-static" />
                                                            <Columns>
                                                                <%--<asp:TemplateField HeaderText="Select" ItemStyle-Width="10%" HeaderStyle-Height="1px">
                                                                <ItemTemplate>
                                                                        <asp:CheckBox id="ChkGridRow" runat="server" onclick="CheckAll(this);" />
                                                                </ItemTemplate>
                                                                </asp:TemplateField>--%>

                                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="text-center" ItemStyle-Width="15%" HeaderStyle-Height="1px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Height="1px" />
                                                                    <ItemStyle Width="15%" />
                                                                </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Roll No." HeaderStyle-CssClass="text-center"  HeaderStyle-Height="1px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblrollno" Text='<%#Eval("RollNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Height="1px" />
                                                                    <ItemStyle Width="15%" />
                                                                </asp:TemplateField>
                                                            <%--    <asp:BoundField HeaderText="Roll No." HeaderStyle-CssClass="text-center" DataField="RollNo" />--%>
                                                                <%--  <asp:BoundField HeaderText="Student Name" DataField="SName" />--%>
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

                                                                <%--    <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkUpdate" Text="Update" Style="text-align: center"
                                                                            CommandName="Update" ForeColor="Red">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>--%>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </tbody>
                                                  
                                                </table>
                                                <table>
                                                      <tr>
                                                        <%--<td>
                                                                <asp:Button runat="server" CssClass="btn btn-primary" ID="btnSave" Text="Save" OnClick="btnSave_Click"
                                                                    Visible="False" />
                                                            </td>--%>
                                                        <td>
                                                            <asp:LinkButton ID="LinkDeleteAll" runat="server" Text="Delete Selected Records" ForeColor="Red" OnClick="LinkDeleteAll_Click"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel ID="pnlcounterFoil" runat="server" Visible="false">
                                            <div class="table table-responsive">
                                                <table width="100%">

                                                    <tr>
                                                        <td><span style="font-weight: bold">Tabulation Page No.<span style="color: red; font-weight: bold;">*</span></span>


                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpageno" CssClass="form-control input-sm" placeholder="PageNo" Width="90px" runat="server" MaxLength="5" ValidationGroup="s"></asp:TextBox>


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtpageno" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="reqpageno" runat="server" CssClass="text-danger" ControlToValidate="txtpageno" ErrorMessage="Please Enter PageNo." ValidationGroup="s"></asp:RequiredFieldValidator>

                                                        </td>
                                                        <td></td>

                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold">Roll No.From<span style="color: red; font-weight: bold;">*</span></span></td>

                                                        <td>
                                                            <asp:TextBox ID="txtfrom" CssClass="form-control input-sm" placeholder="From" runat="server" Width="90px" ValidationGroup="s">

                                                            </asp:TextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtfrom" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" CssClass="text-danger" runat="server" ControlToValidate="txtfrom" ErrorMessage="Please Enter Roll No.From" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold">Roll No.To<span style="color: red; font-weight: bold;">*</span></span></td>
                                                        <td>

                                                            <asp:TextBox ID="txtto" CssClass="form-control input-sm" placeholder="To" runat="server" Width="90px" ValidationGroup="s">

                                                            </asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtto" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" CssClass="text-danger" runat="server" ControlToValidate="txtto" ErrorMessage="Please Enter Roll No.To" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                        </td>
                                                        <td></td>
                                                    </tr>                                                    
                                                </table>


                                            </div>
                                        </asp:Panel>
                                        <panel id="panelsave" runat="server" >                                            
                                            <table>
                                            <tr>
                                                        <td>
                                                            <asp:LinkButton ID="btnSave" CssClass="btn btn-sm btn-primary" runat="server" 
                                                                ValidationGroup="s" OnClick="btnSave_Click1" Style="margin-left: 101px;">Save Data</asp:LinkButton>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="btnClear" runat="server" Style="margin-left: 20px" CssClass="btn btn-primary " OnClick="btnClear_Click1">&nbsp;Clear&nbsp;</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                          </table>
                                        </panel>
                                        <table style="margin-top: 10px;">

                                            <tr>
                                                <td>&nbsp&nbsp     
                                                            <asp:LinkButton ID="btnError" CssClass=" btn btn-primary" runat="server"
                                                                OnClick="btnError_Click" OnClientClick="return confirm ('Do You Really Want To Add Page Error Details ?')">&nbsp;Error&nbsp;</asp:LinkButton>

                                                    &nbsp&nbsp     
                                                </td>
                                                <td></td>
                                                <td></td>

                                            </tr>

                                        </table>
                                        <table>
                                            <tr>
                                                <td>
                                                    <img src="images/mouse_gcvrza.png" height="10px" width="10px" />
                                                    <asp:Label ID="lblnote" runat="server" Font-Size="X-Small" Font-Bold="true" CssClass="text-warning" Text="(1) Please Scroll Mouse On Image for Zoom-In & Zoom-Out"></asp:Label>
                                                    <asp:Label ID="lblnote1" runat="server" Font-Bold="true" Font-Size="X-Small" CssClass="text-warning" Text="(2) Asterisk(*) for compulsary field!"></asp:Label>
                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" Font-Size="X-Small" CssClass="text-warning" Text="(3) In Case of Error Please fill RollNo Zero."></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-lg-12" style="margin-left: 40px;">
                                    <div class="form-group">
                                        <asp:Label CssClass="col-md-6 col-lg-4" ID="lblpages" Text="" runat="server"></asp:Label>
                                        <div class="col-md-8 col-lg-8">
                                            <asp:Label ID="lblCuurentPage" runat="server" Text="" Style="color: #003F8E; font-weight: 600;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group" style="margin-top: -15px;">

                                        <div class="col-md-6 col-lg-4">
                                            <asp:Label ID="lblpagentrycomp" runat="server" Text=""></asp:Label>

                                        </div>
                                        &nbsp&nbsp 
                                         <asp:Label ID="lblrollnocompleted" runat="server" Font-Bold="true" Text="LastRollNo."></asp:Label>

                                        <div class="col-md-12 col-lg-12" style="text-align: center">
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <asp:Label runat="server" ID="lblInd" Visible="false" Text="" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:PostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Label runat="server" ID="lblLotNo" Visible="false" Text="" ForeColor="Red"></asp:Label>
                    <asp:Label runat="server" ID="lblTotalPages" Text="" ForeColor="Transparent"></asp:Label>
                    <asp:Label runat="server" ID="lblEntryPage" Text="" ForeColor="Transparent"></asp:Label>
                </div>
            </div>
        </div>
    </asp:Panel>

    <div class="modal fade" id="modalOnEdit" data-backdrop="static">
        <div class="container" style="margin-top: 40px;">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="loader"></div>
                </div>
            </div>
        </div>
    </div>
   
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

    </script>
    <script src="js1/bootstrap.min.js" type="text/javascript"></script>
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
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.min.js"></script>--%>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>--%>
</asp:Content>