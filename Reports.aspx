<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="Reports.aspx.cs" Inherits="Reports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link rel="stylesheet" href="/../code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">

    <%--<script src="//code.jquery.com/jquery-1.10.2.js"></script>--%>
    <script src="bootstrap-3.3.6-dist/js/jquery-1.10.2.js"></script>

    <%--<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>
    <script src="bootstrap-3.3.6-dist/js/jquery-ui.js"></script>
    <link href="bootstrap-3.3.6-dist/css/style.css" rel="stylesheet" />
    <link href="bootstrap-3.3.6-dist/css/jquery.alerts.css" rel="stylesheet" />
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
        $(function() {
            $('#<%=txtDateFrom.ClientID %>').change(function() {
                $('#<%=txtDateTo.ClientID %>').val($('#<%=txtDateFrom.ClientID %>').val());
            });
        });

        function ValidateRegister() {

            if ($('#<%=txtDateFrom.ClientID%>').val() == 0) {
                $('#<%=txtDateFrom.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Date From', 'Error', function() { $("#txtDateFrom").focus() })
                return false;
            }
            else {
                $('#<%=txtDateFrom.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtDateTo.ClientID%>').val() == 0) {
                $('#<%=txtDateTo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Date To', 'Error', function() { $("#txtDateTo").focus() })
                return false;
            }
            else {
                $('#<%=txtDateTo.ClientID%>').css('border-color', '');
            }

        }
    </script>
    <div class="form-heading"><div class="text-center">Register Report</div></div>
    <%--<h2 align="center">
        Scanned TR Register Report</h2>--%>
    <table align="center" style="margin-top: 33px">
        <tr>
            <td>
                Date From:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDateFrom"></asp:TextBox>
                <asp:Image ID="Image1" runat="server" onkeyup="OneTextToOther();" ImageUrl="~/images/calendar.png"
                    Width="25px" Height="20px" />
                <cc1:CalendarExtender runat="server" TargetControlID="txtDateFrom"  Format="dd/MM/yyyy"
                    PopupButtonID="Image1">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                Date To:
            </td>
            <td>
                <asp:TextBox runat="server" ID="txtDateTo"></asp:TextBox>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Width="25px"
                    Height="20px" />
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateTo"
                     Format="dd/MM/yyyy" PopupButtonID="Image2">
                </cc1:CalendarExtender>
            </td>
        </tr>
    </table>
    <br />
    <table align="center">
        <tr>
            <td>
            </td>
            <td>
                <asp:Button runat="server" ID="btnShowReport" Text="Show Report" OnClick="btnShowReport_Click"
                    data-loading-text="Loading..." autocomplete="off" OnClientClick="return ValidateRegister();" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div class="loading" align="center">
                    Report Is Being Generated
                    <br />
                    Please Wait....<br />
                    <br />
                    <img src="loader.gif" alt="" />
                </div>
            </td>
        </tr>
    </table>

    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function() {
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
        $('form').live("submit", function() {
            ShowProgress();
        });
    </script>

</asp:Content>
