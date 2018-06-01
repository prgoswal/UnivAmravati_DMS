<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="MovePhotos.aspx.cs" Inherits="MovePhotos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="bootstrap-3.3.6-dist/css/jquery.alerts.css" rel="stylesheet" />
    <script src="bootstrap-3.3.6-dist/js/jquery-1.7.1.js"></script>
    <script src="bootstrap-3.3.6-dist/js/jquery.ui.draggable.js"></script>
    <script src="bootstrap-3.3.6-dist/js/jquery.alerts.js"></script>
    <%--<script src="//ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>--%>
    <script src="bootstrap-3.3.6-dist/js/jquery-2.1.1.min.js"></script>
    <style type="text/css">
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: #000;
            z-index: 99;
            opacity: 0.2;
            filter: alpha(opacity=20);
            -moz-opacity: 0.2;
            min-height: 100%;
            width: 100%;
        }

        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 150px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript">

        function ddlClick() {
            //    //$('#ddlMachineNm').change(function () {
            //    var SelectedText = $(this).find("option :selected").text();
            //    var SelectedValue = $(this).val();
            //    $('#country').text(SelectedText);
            //    $('#Value').text(SelectedValue);

            //};
            if ($("#lblmsg").val() != "")
                $(".Lablecalass").empty();
        };

        function OneTextToOther() {

            var first = document.getElementById('<%= txtPartFrom.ClientID %>').value;
            document.getElementById('<%= txtPartTo.ClientID %>').value = first;
        }

        function ValidateRegister() {

            if ($('#<%=ddlMachineNm.ClientID%>').val() == 0) {
                $('#<%=ddlMachineNm.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Machine Name.', 'Error', function () { $("#ddlMachineNm").focus() })
                return false;
            }
            else {
                $('#<%=ddlMachineNm.ClientID%>').css('border-color', '');
            }


            if ($('#<%=ddlPartType.ClientID%>').val() == 0) {
                $('#<%=ddlPartType.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select TR Type.', 'Error', function () { $("#ddlPartType").focus() })
                return false;
            }
            else {
                $('#<%=ddlPartType.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtPartFrom.ClientID%>').val() == 0) {
                $('#<%=txtPartFrom.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Register No. From.', 'Error', function () { $("#txtPartFrom").focus() })
                return false;
            }
            else {
                $('#<%=txtPartFrom.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtPartTo.ClientID%>').val() == 0) {
                $('#<%=txtPartTo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Register No. To.', 'Error', function () { $("#txtPartTo").focus() })
                return false;
            }
            else {
                $('#<%=txtPartTo.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtPartFrom.ClientID%>').val() > $('#<%=txtPartTo.ClientID%>').val()) {
                $('#<%=txtPartTo.ClientID%>').css('border-color', 'Red');
                jAlert('Register No. From Must Be Less Than Register No. To.', 'Error', function () { $("#txtPartTo").focus() })
                return false;
            }
            else {
                $('#<%=txtPartTo.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtPartTo.ClientID%>').val() - $('#<%=txtPartFrom.ClientID%>').val() + 1 > 25) {
                $('#<%=txtPartTo.ClientID%>').css('border-color', 'Red');
                jAlert('Register No. Difference Must Be Between Or Equal To 10.', 'Error', function () { $("#txtPartTo").focus() })
                return false;
            }
            else {
                $('#<%=txtPartTo.ClientID%>').css('border-color', '');
            }
        }

    </script>
    <script type="text/javascript">
        function GetCountryDetails() {
            // Get id of dropdownlist
            var parm = document.getElementById("ddlPartType");
            // Get Dropdownlist selected item text
            document.getElementById('lblPartA').innerHTML = parm.options[parm.selectedIndex].text;
            // Get Dropdownlist selected value item
            document.getElementById('lblPartB').innerHTML = parm.options[parm.selectedIndex].value;
        }
    </script>

    <div class="row">
        <asp:Panel runat="server" ID="pnlMovePhto">
            <h3 class="text-center">TR Images Move Utility
            </h3>
            <div class="col-lg-3 col-md-3">
            </div>
            <div class="col-lg-6 col-md-6">
                <div class="box">

                    <div class="form-group">
                        <label class="col-sm-4 col-md-4 col-lg-4">
                            <%--  Last Part A :---%>
                        </label>
                        <div class="col-sm-8 col-md-8 col-lg-8">
                            <asp:Label runat="server" ID="lblPartA" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-4 col-md-4 col-lg-4">
                            <%--Last Part B :- --%>
                        </label>
                        <div class="col-sm-8 col-md-8 col-lg-8">
                            <asp:Label runat="server" ID="lblPartB" ForeColor="Red"></asp:Label>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-4 col-md-4 col-lg-4">
                            Machine Name:<asp:Label runat="server" ID="Label1" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-8 col-md-8 col-lg-8">
                            <asp:DropDownList ID="ddlMachineNm" runat="server" CssClass="form-control input-sm"
                                AutoPostBack="false" onChange="javascript:ddlClick()">
                            </asp:DropDownList>
                        </div>
                    </div>


                    <div class="form-group">
                        <label class="col-sm-4 col-md-4 col-lg-4">
                            TR Type<asp:Label runat="server" ID="lblFaculty" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-8 col-md-8 col-lg-8">
                            <asp:DropDownList ID="ddlPartType" runat="server" CssClass="form-control input-sm" onchange="GetCountryDetails()">
                                <asp:ListItem Text="Select TR Type" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Foil" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Counter Foil" Value="2"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-sm-4 col-md-4 col-lg-4">
                            Exam Year<asp:Label runat="server" ID="Label2" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-sm-8 col-md-8 col-lg-8">
                            <asp:DropDownList ID="ddlExamYear" runat="server" CssClass="form-control input-sm" onchange="GetCountryDetails()">
                                <asp:ListItem Text="Select Exam Year" Value="0"></asp:ListItem>
                                <asp:ListItem Text="2015" Value="2015"></asp:ListItem>
                                <asp:ListItem Text="2014" Value="2014"></asp:ListItem>
                                <asp:ListItem Text="2013" Value="2013"></asp:ListItem>
                                <asp:ListItem Text="2012" Value="2012"></asp:ListItem>
                                <asp:ListItem Text="2011" Value="2011"></asp:ListItem>
                                <asp:ListItem Text="2010" Value="2010"></asp:ListItem>
                                <asp:ListItem Text="2009" Value="2009"></asp:ListItem>
                                <asp:ListItem Text="2008" Value="2008"></asp:ListItem>
                                <asp:ListItem Text="2007" Value="2007"></asp:ListItem>
                                <asp:ListItem Text="2006" Value="2006"></asp:ListItem>
                                <asp:ListItem Text="2005" Value="2005"></asp:ListItem>
                                <asp:ListItem Text="2004" Value="2004"></asp:ListItem>
                                <asp:ListItem Text="2003" Value="2003"></asp:ListItem>
                                <asp:ListItem Text="2002" Value="2002"></asp:ListItem>
                                <asp:ListItem Text="2001" Value="2001"></asp:ListItem>
                                <asp:ListItem Text="2000" Value="2000"></asp:ListItem>                                
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="form-group">
                        <label class="col-md-4 col-lg-4">
                            Register No.
                        <asp:Label runat="server" ID="lblPartNo" ForeColor="Red" Text="*"></asp:Label>
                        </label>
                        <div class="col-md-4 col-lg-4">
                            <asp:TextBox ID="txtPartFrom" runat="server" CssClass="form-control input-sm"
                                placeholder="Enter Register No. From" MaxLength="7" onkeyup="OneTextToOther();"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpresphone1" ValidationGroup="phone" Display="Dynamic"
                                ControlToValidate="txtPartFrom" runat="server" ErrorMessage="Enter Atleast 7 Numbers"
                                SetFocusOnError="True" ValidationExpression="^\d{6}$" ForeColor="Red"></asp:RegularExpressionValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPartFrom"
                                ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                        <div class="col-md-4 col-lg-4">
                            <asp:TextBox ID="txtPartTo" runat="server" CssClass="form-control input-sm" placeholder="Enter Register No. To" MaxLength="7"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="phone" Display="Dynamic"
                                ControlToValidate="txtPartTo" runat="server" ErrorMessage="Enter Atleast 7 Numbers"
                                SetFocusOnError="True" ValidationExpression="^\d{6}$" ForeColor="Red"></asp:RegularExpressionValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPartTo"
                                ValidChars="0123456789">
                            </cc1:FilteredTextBoxExtender>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="text-center">
                            <label class="col-md-2 col-lg-2"></label>
                            <asp:Label runat="server" ID="lblConfirm" ForeColor="Red"></asp:Label>
                            <br />
                            <%--<asp:LinkButton ID="linkMove" class="btn btn-primary" runat="server" Text="Move"
                                OnClick="linkMove_Click" OnClientClick="return ValidateRegister();">--%>
                            <asp:Button ID="linkMove" class="btn btn-primary" runat="server" Text="Move" data-loading-text="Loading..." autocomplete="off"
                                OnClick="linkMove_Click" OnClientClick="return ValidateRegister();" />
                            <%--<i class="fa fa-save"></i>&nbsp;  OnClientClick="return ValidateRegister();"--%>

                            <asp:LinkButton ID="linkCancel" class="btn btn-primary" runat="server" OnClick="linkCancel_Click">
                                    <i class="fa fa-close"></i>&nbsp;Cancel</asp:LinkButton>
                        </div>
                        <div class="text-center">
                            <asp:Label runat="server" ID="lblmsg" ForeColor="Red" CssClass="Lablecalass"></asp:Label>
                            <%--<asp:TextBox runat="server" class="form-control" ID="txtcount" OnTextChanged="txtcount_TextChanged" Enabled="False"></asp:TextBox>--%>
                            <%--<asp:TextBox runat="server" class="form-control" ID="txtcount" OnTextChanged="txtcount_TextChanged" Enabled="False" Style="width: 300px; border: none; background: #DDDDDD;"></asp:TextBox>--%>
                        </div>
                        <div class="loading" align="center">
                            Images Transferring. Please Wait....<br />
                            <br />
                            <img src="loader.gif" alt="" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-lg-3 col-md-3">
                <br />
                <asp:Label ID="country" runat="server"></asp:Label>
                <br />
                <asp:Label ID="Value" runat="server"></asp:Label>
            </div>
            <div class="col-lg-6 col-md-6">
            </div>
        </asp:Panel>
    </div>
    <div class="col-lg-3 col-md-3">
    </div>

    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>

</asp:Content>
