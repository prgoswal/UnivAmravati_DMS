<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true"
    CodeFile="ImgViewer.aspx.cs" Inherits="ImageViewer" %>

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

        .viewport {
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
                url: "ImgViewer.aspx/Next",
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
                jAlert('Please Enter Register No.', 'Error', function () { $("#txtRegNo").focus() })
                return false;
            }
            else {
                $('#<%=txtRegNo.ClientID%>').css('border-color', '');
            }
            <%--   if ($('#<%=txtSRNo.ClientID%>').val() == '') {
                $('#<%=txtSRNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Student Roll No.', 'Error', function() { $("#txtSRNo").focus() })
                return false;
            }--%>
            <%--    else {
                $('#<%=txtSRNo.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtStudentName.ClientID%>').val() == '') {
                $('#<%=txtStudentName.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Student Name', 'Error', function() { $("#txtStudentName").focus() })
                return false;
            }--%>
          <%--  else {
                $('#<%=txtStudentName.ClientID%>').css('border-color', '');
            }--%>
        };
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

    <%--<asp:Label ID="lblFileType" ForeColor="Transparent" Text='<%#Eval("FileType") %>'></asp:Label>--%>
    <div>
        <div class="col-lg-12">
            <div class="form-group">
                <asp:Label runat="server" ID="lblRegNo" Style="font-weight: 600;" class="col-sm-2 col-md-2 col-lg-2"
                    Text="   Enter Register No.">
                </asp:Label>
                <div class="col-sm-3 col-md-2 col-lg-2">
                    <asp:TextBox ID="txtRegNo" CssClass="form-control input-sm" placeholder="Enter Register No."
                        runat="server" MaxLength="8"></asp:TextBox>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                        TargetControlID="txtRegNo" ValidChars="0123456789">
                    </cc1:FilteredTextBoxExtender>
                </div>
                <div class="col-sm-2 col-md-2 col-lg-2">
                    <asp:LinkButton ID="btnSearchImg" CssClass="btn btn-sm btn-primary" runat="server"
                        Text="" OnClick="btnSearchImg_Click" OnClientClick="return ValidateRegister();"><i class="fa fa-search"> </i>&nbsp;Search Image</asp:LinkButton>
                </div>
                <div class="col-sm-2 col-md-6 col-lg-6">
                    <asp:Label runat="server" ID="lblDetails" Style="font-family: Times New Roman" ForeColor="#B8061D" Font-Size="16px"></asp:Label>
                    <br></br>
                    <%--   <asp:Label runat="server" ID="lblDetails" Style="font-family: Times New Roman"  ForeColor="#3074ad" Font-Size="16px"></asp:Label>--%>
                    <asp:Label runat="server" ID="lblCount" Style="font-family: Times New Roman" ForeColor="#3074ad"
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
                        <div class="col-md-12 col-lg-12">
                            <%--  <section>--%>
                            <asp:Panel ID="pnlTrType" runat="server" Visible="false">
                                <asp:Label CssClass="pull-left" ID="lblType" Style="margin-left: 14px; margin-top: 2px; color: #003F8E; font-weight: 600;"
                                    Text="" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;
                              
                            </asp:Panel>
                            <div class="parent panzoom" style="text-align: center; height: 550px; width: 900px; position: static; border: 2px solid #50618C;"
                                id="">
                              
                             <%--       <asp:DataList ID="dlImages" runat="server" OnSelectedIndexChanged="dlImages_SelectedIndexChanged">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td>
                                                       
                                                        <img id="image" src='<%# Eval("FilePath")%>' alt="" class="img-responsive" style="position: relative; top: 0px; left: 0px; width: 950px; height: 545px;" />
                                                    
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:DataList>--%>
<%--                                <img ID="imgfile" runat="server"  Height="500" Width="800" />--%>
                                <asp:Label ID="lblpath" runat="server" ></asp:Label>
                                 <asp:Image ID="imgfile" runat="server" CssClass="panzoom" Height="500" Width="800" />
                                    <asp:HiddenField ID="Filetype" runat="server" />
                                    <asp:HiddenField ID="RegNoCF" runat="server" />
                                    <asp:HiddenField ID="FilePathImg" runat="server" />
                              
                               

                                     
                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1 col-sm-01">
                            <%-- <asp:UpdatePanel runat="server" ID="updpnlPN">
                                <ContentTemplate>--%>
                            <div class="buttons" style="margin-left: -11px;">
                                <button class="zoom-in" style="display: none">
                                    Zoom In</button>
                                <asp:LinkButton ID="btnprev" CssClass="btn btn-primary btn-change" runat="server"
                                    Visible="false" OnClick="btnprev_Click"><i class="fa fa-angle-double-left"></i>&nbsp;<</asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton1" CssClass="btn btn-primary btn-change" runat="server"
                                    OnClick="btnprev_Click"><i class="fa fa- fa-angle-double-left"></i>&nbsp;<</asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" CssClass="btn btn-primary btn-change" runat="server" Enabled="false"
                                    OnClick="btnnext_Click">>&nbsp;<i class="fa fa- fa-angle-double-right"></i></asp:LinkButton>

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
                                <div style="margin-left: 35%;" class="col-lg-7">
                                    <div class="box">

                                        <asp:Label runat="server" Style="text-align: center; font-family: Times New Roman; color: #061A5C; font-weight: 600"
                                            Text="Foil Entry" ID="lblFoilEntry" Font-Size="Large"></asp:Label>
                                         <asp:Button ID="btnnext" runat="server" Text="next" OnClick="btnnext_Click" />
                                        <asp:Panel ID="pnlfoilentry" runat="server" Visible="false">
                                            <div class="table table-responsive">
                                                <table width="100%">
                                               
                                                    <tr>
                                                        <td><span style="font-weight: bold">Tabulation Page No.<span style="color: red; font-weight: bold;">*</span></span>


                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpageno" CssClass="form-control input-sm" placeholder ="FoilPageNo"  Width="90px" runat="server" MaxLength="5" ValidationGroup="s"></asp:TextBox>


                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtpageno" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="reqpageno" runat="server" ControlToValidate="txtpageno" ErrorMessage="Please Enter PageNo." ValidationGroup="s"></asp:RequiredFieldValidator>

                                                        </td>
                                                        <td></td>

                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold">Roll No.From<span style="color: red; font-weight: bold;">*</span></span></td>

                                                        <td>
                                                            <asp:TextBox ID="txtfrom" CssClass="form-control input-sm"  placeholder ="FoilFrom" runat="server" Width="90px" ValidationGroup="s">

                                                            </asp:TextBox>

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtfrom" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfrom" ErrorMessage="Please Enter Roll No.From" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td><span style="font-weight: bold">Roll No.To<span style="color: red; font-weight: bold;">*</span></span></td>
                                                        <td>

                                                            <asp:TextBox ID="txtto" CssClass="form-control input-sm" placeholder ="FoilTo" runat="server" Width="90px" ValidationGroup="s">

                                                            </asp:TextBox>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>

                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers"
                                                                TargetControlID="txtto" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtto" ErrorMessage="Please Enter Roll No.To" ValidationGroup="s"></asp:RequiredFieldValidator>

                                                        </td>
                                                        <td></td>
                                                    </tr>




                                                </table>
                                                <table style="margin-top: 10px;">
                                                    <tr>
                                                        <td></td>
                                                        <td>
                                                            <asp:Button runat="server" CssClass="btn btn-primary" ID="btnSave" Text="Save" ValidationGroup="s" OnClick="btnSave_Click" style="margin-left:37px;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <%-- <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-primary" Text="Add" ValidationGroup="s"
                                                                OnClick="btnAdd_Click" OnClientClick="return ValidateRegister();"></asp:LinkButton>--%>


                                                            &nbsp&nbsp     
                                                            <asp:LinkButton ID="btnError" CssClass=" btn btn-primary" runat="server"
                                                                OnClick="btnError_Click" OnClientClick="return confirm ('Do You Really Want To Add Page Error Details ?')">&nbsp;Error&nbsp;</asp:LinkButton>

                                                            &nbsp&nbsp   
                                                            <asp:LinkButton ID="btnClear" CssClass="btn btn-primary " runat="server"
                                                                OnClick="btnClear_Click1">&nbsp;Clear&nbsp;</asp:LinkButton>



                                                        </td>
                                                        <td></td>
                                                        <td></td>

                                                    </tr>

                                                </table>
                                                <%--                        <table>
                                                    <tr>
                                                        <td>
                                                            <asp:LinkButton ID="btnnext" CssClass="btn btn-primary btn-change" runat="server"
                                                                Enabled="false" OnClick="btnnext_Click">Next&nbsp;<i class="fa fa-angle-double-right"></i></asp:LinkButton>

                                                        </td>
                                                    </tr>

                                                </table>--%>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel runat="server" ID="pnlCf" Visible="false">
                                            <table width="100%">
                                                <tr>
                                                    <td><span style="font-weight: bold">Tabulation Page No.<span style="color: red; font-weight: bold;">*</span></span>


                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCFpageno" placeholder ="CFoilPageNo" CssClass="form-control input-sm" Width="90px" runat="server" MaxLength="5"  ValidationGroup="g"></asp:TextBox>


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtCFpageno" ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCFpageno" ErrorMessage="Please Enter PageNo." ValidationGroup="g"></asp:RequiredFieldValidator>

                                                    </td>
                                                    <td></td>

                                                </tr>
                                                <tr>
                                                    <td><span style="font-weight: bold">Roll No.From<span style="color: red; font-weight: bold;">*</span></span></td>

                                                    <td>

                                                        <asp:TextBox ID="txtRollNoFrom"   placeholder ="CFoilFrom" CssClass="form-control input-sm" MaxLength="8" ValidationGroup="g"
                                                            runat="server" Width="90px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtRollNoFrom">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRollNoFrom" ErrorMessage="Please Enter Roll No.From" ValidationGroup="g"></asp:RequiredFieldValidator>

                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td><span style="font-weight: bold">Roll No.To<span style="color: red; font-weight: bold;">*</span></span></td>
                                                    <td>


                                                        <asp:TextBox ID="txtRollNoTo" placeholder ="CFoilTo" CssClass="form-control input-sm " ValidationGroup="g" Style="text-transform: uppercase"
                                                            runat="server" Width="90px"></asp:TextBox>
                                                        
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>

                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers"
                                                            TargetControlID="txtRollNoTo">
                                                        </cc1:FilteredTextBoxExtender>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtRollNoTo" ErrorMessage="Please Enter Roll No.To" ValidationGroup="g"></asp:RequiredFieldValidator>

                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>
                                                        <asp:LinkButton ID="LbtnSaveCF" runat="server" CssClass="btn btn-primary" Text="Save"
                                                            OnClick="LbtnSaveCF_Click" OnClientClick="return ValidateCFForm();" ValidationGroup="g"></asp:LinkButton>
                                                    </td>
                                                </tr>



                                            </table>
                       
                                        </asp:Panel>
                                                             <table>
                                                <tr>
                                                    <td>
                                                    <img src="images/mouse_gcvrza.png" height="10px" width="10px" />
                                                        <asp:Label ID="lblnote" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Please Scroll Mouse On Image for Zoom-In & Zoom-Out"></asp:Label>

                                                       

                                                        <asp:Label ID="lblnote1" runat="server" Font-Size="X-Small" ForeColor="Red" Text="Asterisk(*) for compulsary field!"></asp:Label>



                                                    </td>


                                                </tr>
                                            </table>
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
                    <asp:Label runat="server" ID="lblTotalPages" Text="" ForeColor="Transparent"></asp:Label>
                    <asp:Label runat="server" ID="lblEntryPage" Text="" ForeColor="Transparent"></asp:Label>
                </div>
            </div>
        </div>
    </asp:Panel>
    <%--</div>
</div>--%>

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

    <%--<script type="text/javascript" src="http://code.jquery.com/jquery-1.9.1.min.js"></script>--%>
    <script type="text/javascript" src="bootstrap-3.3.6-dist/js/jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="bootstrap-3.3.6-dist/js/jquery.slides.min.js"></script>
    <script type="text/javascript" src="bootstrap-3.3.6-dist/js/jquery-1.11.2.min.js"></script>
    <%--<script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.2/jquery.min.js"></script>--%>

</asp:Content>
