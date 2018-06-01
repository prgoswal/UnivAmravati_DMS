<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true"
    CodeFile="Copy of ImgViewer.aspx.cs" Inherits="ImageViewer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .imgt
        {
            height: 100px;
            width: 100px;
        }
        section
        {
            text-align: center; /*margin: 50px 0;*/
        }
        .panzoom-parent
        {
            border: 2px solid #333;

        }
        .panzoom-parent .panzoom
        {
            border: 2px dashed #666;
        }
        .buttons
        {
            margin: 112px 0 0;
        }
        .btn-change
        {
            margin-bottom: 10px;
        }
        .viewport
        {
            position: absolute;
            overflow: hidden;
            top: 100px;
            left: 17px;
            width: 100%;
            height: 545px;
        }
        /*#image
        {
            -webkit-tap-highlight-color: transparent;
            transform: translate(322.385px, -5.05769px) scale(2.1, 2.1);
        }*/</style>
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
                success: function(response) {
                    alert(response.d);

                },
                failure: function(response) {

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
                data: '{str: "' + $("#<%=txtRegNo.ClientID%>")[0].value + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function(response) {
                    alert(response.d);
                },
                failure: function(response) {

                    alert(response.d);
                }
            });
        }
        function OnSuccess(response) {
            alert(response.d);
        }
    </script>

    <script type="text/javascript">
        //        $(document).ready(function() {
        //            $('#image').click(function() {
        //                $('#image').addClass('imageHeight');
        //            });
        //        });
        //         $('#image').css({
        //                '-webkit-tap-highlight-color': 'transparent',
        //                'transform': 'translate(322.385px, -5.05769px) scale(2.1, 2.1)'
        //        $('#image').click(function() {
        //        $("img").css("transform", "translate(322.385px, -5.05769px) scale(2.1, 2.1)");
        //        });
        function ValidateRegister() {


            if ($('#<%=txtRegNo.ClientID%>').val() == '') {
                $('#<%=txtRegNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Register No.', 'Error', function() { $("#txtRegNo").focus() })
                return false;
            }
            else {
                $('#<%=txtRegNo.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtSRNo.ClientID%>').val() == '') {
                $('#<%=txtSRNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Student Roll No.', 'Error', function() { $("#txtSRNo").focus() })
                return false;
            }
            else {
                $('#<%=txtSRNo.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtStudentName.ClientID%>').val() == '') {
                $('#<%=txtStudentName.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Student Name', 'Error', function() { $("#txtStudentName").focus() })
                return false;
            }
            else {
                $('#<%=txtStudentName.ClientID%>').css('border-color', '');
            }
        };
        function ValidateCFForm() {

            if ($('#<%=txtRollNoFrom.ClientID%>').val() == '') {
                $('#<%=txtRollNoFrom.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter RollNoFrom', 'Error', function() { $("#txtRollNoFrom").focus() })
                return false;
            }
            else {
                $('#<%=txtRollNoFrom.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtRollNoTo.ClientID%>').val() == '') {
                $('#<%=txtRollNoTo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter RollNo To', 'Error', function() { $("#txtRollNoTo").focus() })
                return false;
            }
            else {
                $('#<%=txtRollNoTo.ClientID%>').css('border-color', '');
            }
        };
    </script>

    <%--<asp:Label ID="lblFileType" ForeColor="Transparent" Text='<%#Eval("FileType") %>'></asp:Label>--%>
    <div>
        <div class="col-lg-12">
            <div class="form-group">
                <asp:Label runat="server" ID="lblRegNo" Style="font-weight: 600;" class="col-sm-2 col-md-2 col-lg-2"
                    Text="   Enter Register No.">
                </asp:Label>
                <div class="col-sm-3 col-md-2 col-lg-2">
                    <asp:TextBox ID="txtRegNo" CssClass="form-control input-sm" placeholder="Enter Register No."
                        runat="server"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                        TargetControlID="txtRegNo">
                    </cc1:FilteredTextBoxExtender>
                </div>
                <div class="col-sm-2 col-md-2 col-lg-2">
                    <asp:LinkButton ID="btnSearchImg" CssClass="btn btn-sm btn-primary" runat="server"
                        Text="" OnClick="btnSearchImg_Click" OnClientClick="return ValidateRegister();"><i class="fa fa-search"> </i>&nbsp;Search Image</asp:LinkButton>
                </div>
                <div class="col-sm-2 col-md-6 col-lg-6">
                    <asp:Label runat="server" ID="lblDetails" Style="font-family: Times New Roman" ForeColor="Red"></asp:Label>
                    <asp:Label runat="server" ID="lblCount" Style="font-family: Times New Roman" ForeColor="Red"
                        Text=""></asp:Label>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="col-md-9 col-lg-9">
            </div>
            <div class="col-md-3 col-lg-3">
            </div>
        </div>
        <%--<asp:Image ID="image" ImageUrl='<%# Eval("Value")+"\\"+Eval("Text")%>' runat="server" />--%>
    </div>
    <asp:Panel runat="server" ID="pnlImageViewer" Visible="false">
        <div class="">
            <div class="col-lg-12">
                <div class="col-md-9 col-lg-7">
                    <div class="row">
                        <div class="col-md-10 col-lg-11">
                            <%--  <section>--%>
                            <asp:Panel ID="pnlTrType" runat="server" Visible="false">
                                <asp:Label CssClass="pull-left" ID="lblType" Style="margin-left: 14px; margin-top: 2px;
                                    color: #003F8E; font-weight: 600;" Text="" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                                <asp:RadioButton ID="radioFoil" runat="server" Text="Foil" OnCheckedChanged="radioFoil_CheckedChanged"
                                    GroupName="fcf" AutoPostBack="true" Checked="True" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radioCF" runat="server"
                                    Text="Counter Foil" OnCheckedChanged="radioCF_CheckedChanged" GroupName="fcf"
                                    AutoPostBack="true" />
                            </asp:Panel>
                            <div class="parent" style="text-align: center; height: 550px; width: 100%; position: static;
                                border: 2px solid #50618C;" id="">
                                <div class="panzoom" style="height: 550px; width: 100%">
                                    <asp:DataList ID="dlImages" runat="server">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <%--<asp:Label ID="lblFileType" ForeColor="Transparent" Text='<%#Eval("FileType") %>'></asp:Label>--%>
                                                        <img id="image" src='<%# Eval("FilePath")%>' alt="" class="img-responsive" style="position: relative;
                                                            top: 0px; left: 0px; width: 100%; height: 545px;" />
                                                        <%--<asp:Image ID="image" ImageUrl='<%# Eval("Value")+"\\"+Eval("Text")%>' runat="server" />--%>
                                                        <%--  </section>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>
                                    <asp:HiddenField ID="Filetype" runat="server" />
                                    <asp:HiddenField ID="RegNoCF" runat="server" />
                                    <asp:HiddenField ID="FilePathImg" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1 col-sm-01">
                            <%-- <asp:UpdatePanel runat="server" ID="updpnlPN">
                                <ContentTemplate>--%>
                            <div class="buttons" style="margin-left: -11px;">
                                <button class="zoom-in" style="display: none">
                                    Zoom In</button>
                                <asp:LinkButton ID="btnprev" CssClass="btn btn-primary btn-change" runat="server"
                                    Visible="false" OnClick="btnprev_Click"><i class="fa fa- fa-angle-double-left"></i>&nbsp;Prev</asp:LinkButton>
                                <asp:LinkButton ID="btnnext" CssClass="btn btn-primary btn-change" runat="server"
                                    Enabled="false" OnClick="btnnext_Click">Next&nbsp;<i class="fa fa- fa-angle-double-right"></i></asp:LinkButton>
                                <asp:LinkButton ID="btnError" CssClass="btn btn-primary btn-change" 
                                    runat="server" onclick="btnError_Click">&nbsp;Error&nbsp;</asp:LinkButton>
                                <asp:LinkButton ID="btnClear" CssClass="btn btn-primary btn-change" runat="server"
                                    OnClick="btnClear_Click1">&nbsp;Clear&nbsp;</asp:LinkButton>
                                <%--<asp:Button ID="ChangeCSS" Text="Height" CssClass="btn btn-primary btn-change" runat="server" />--%>
                                <%-- <asp:LinkButton ID="btnHome" CssClass="btn btn-primary btn-change" runat="server"
                                    OnClick="btnHome_Click">&nbsp;Home&nbsp;</asp:LinkButton>--%>
                                <%-- <Triggers>
                                        <asp:PostBackTrigger ControlID="btnprev" />
                                        <asp:PostBackTrigger ControlID="btnnext" />
                                    </Triggers>--%>
                            </div>
                            <%--  </ContentTemplate>
                            </asp:UpdatePanel>--%>
                        </div>
                    </div>
                </div>
                <div class="col-md-3 col-lg-5">
                    <asp:UpdatePanel runat="server" ID="pnlFoil">
                        <ContentTemplate>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="box">
                                        <asp:Label runat="server" Style="text-align: center; font-family: Times New Roman;
                                            color: Blue; font-weight: 600" Text="Foil Entry" ID="lblFoilEntry"></asp:Label>
                                        <asp:Panel runat="server" ID="pnlcounter" Visible="false">
                                            <div class="table table-responsive">
                                                <table class="table table-bordered">
                                                    <thead>
                                                        <tr>
                                                            <th class="col-lg-1">
                                                                Sr No.
                                                            </th>
                                                            <th class="col-lg-5">
                                                                Roll No.
                                                            </th>
                                                            <th class="col-lg-5">
                                                                Student Name
                                                            </th>
                                                            <th>
                                                            </th>
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
                                                                <asp:TextBox ID="txtStudentName" Text="" CssClass="form-control input-sm " Style="text-transform: uppercase"
                                                                    runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" OnClick="btnAdd_Click" OnClientClick="return ValidateRegister();"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </thead>
                                                    <tbody>
                                                        <asp:GridView ID="grdEntry" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdEntry_RowDeleting"
                                                            OnRowUpdating="grdEntry_RowUpdating">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="15%" HeaderStyle-Height="1px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Height="1px" />
                                                                    <ItemStyle Width="15%" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Roll No." DataField="StudentRollNo" />
                                                                <asp:BoundField HeaderText="Student Name" DataField="StudentName" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" Style="text-align: center"
                                                                            CommandName="Delete" ForeColor="Red" OnClientClick="return confirm ('Do You Really Want To Delete ?')">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField> 
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkUpdate" Text="Update" Style="text-align: center"
                                                                            CommandName="Update" ForeColor="Red">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </tbody>
                                                    <tr>
                                                        <td>
                                                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="pnlCf" Visible="false">
                                            <table class="table table-bordered">
                                                <thead>
                                                    <tr>
                                                        <th class="col-lg-5">
                                                            Roll No. From
                                                        </th>
                                                        <th class="col-lg-5">
                                                            Roll No. To
                                                        </th>
                                                        <th>
                                                        </th>
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
                                                    <td>
                                                        <asp:LinkButton ID="LbtnSaveCF" runat="server" Text="Save" OnClick="LbtnSaveCF_Click"
                                                            OnClientClick="return ValidateCFForm();"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                            <%-- <div class="form-group">
                                                <label class="col-lg-4">
                                                    Roll No. From</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="clearfix">
                                            </div>
                                            <div class="form-group">
                                                <label class="col-lg-4">
                                                    Roll No. To</label>
                                                <div class="col-lg-8">
                                                    <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                                                </div>
                                            </div>--%></asp:Panel>
                                    </div>
                                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red" Text=""></asp:Label>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 col-lg-12">
                                    <div class="form-group">
                                        <asp:Label CssClass="col-md-6 col-lg-4" ID="lblpages" Text="" runat="server"></asp:Label>
                                        <div class="col-md-8 col-lg-8">
                                            <asp:Label ID="lblCuurentPage" runat="server" Text="" Style="color: #003F8E; font-weight: 600;"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="form-group" style="margin-top: -15px;">
                                        <%--<label class="col-md-6 col-lg-4">Page Entry Completed :-</label> --%>
                                        <label class="col-md-6 col-lg-4">
                                            <asp:Label ID="lblpagentrycomp" runat="server" Text=""></asp:Label></label>
                                        <div class="col-md-12 col-lg-12" style="text-align: center">
                                            <asp:Label ID="lblEntryCompleted" runat="server" Text="" Style="color: #003F8E; font-weight: 600;"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:Label runat="server" ID="lblInd" Visible="false" Text="" ForeColor="Red"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <asp:Label runat="server" ID="lblLotNo" Visible="false" Text="" ForeColor="Red"></asp:Label>
                </div>
            </div>
        </div>
    </asp:Panel>
    <%--</div>
</div>--%>

    <script type="text/javascript">
        (function($) {
            $("#image").imgViewer({
                onClick: function(e, self) {
                    var pos = self.cursorToImg(e.pageX, e.pageY);
                    $("#position").html(e.pageX + " " + e.pageY + " " + pos.x + " " + pos.y);
                    $(this).rotate(90);
                }
            });

        })(jQuery);

    </script>

    <%--<script type="text/javascript">
        function () {
            var $section = $('#divSection').first();
            $section.find('.panzoom').panzoom({
                $zoomOut: $section.find(".zoom-in"),
                $zoomIn: $section.find(".zoom-out"),
                $zoomRange: $section.find(".zoom-range"),
                $reset: $section.find(".reset btn btn-primary"),
                $rotate:
                });
        };
    </script>--%>

    <script type="text/javascript">
        $(document).ready(function() {
            $('#divSection').bind('wheel mousewheel', function(e) {
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
    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.min.js"></script>--%>

    <script type="text/javascript" src="bootstrap-3.3.6-dist/js/jquery.slides.min.js"></script>

    <script type="text/javascript" src="bootstrap-3.3.6-dist/js/jquery-1.11.2.min.js"></script>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>--%>
</asp:Content>
