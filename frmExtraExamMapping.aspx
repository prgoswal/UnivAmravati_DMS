<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master" CodeFile="frmExtraExamMapping.aspx.cs" Inherits="frmExtraExamMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        <%--function CloseMPR() {
            $('#<%=missingPunchedRecordModal.ClientID%>').hide();
        }
        function CloseEPR() {
            $('#<%=extraPunchedRecordModal.ClientID%>').hide();
        }--%>
        function LoadAllScript() {
            <%--function CloseMPR() {
                $('#<%=missingPunchedRecordModal.ClientID%>').hide();
            }
            function CloseEPR() {
                $('#<%=extraPunchedRecordModal.ClientID%>').hide();
            }--%>

            function LoadActive() {
                $('.loading').addClass(' active');
            }
        }
    </script>
    <%--<script type="text/javascript">
        function PrintMPRPanel() {
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("<%=pnlMissingPunchedRecordPopUp.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
                $(document).ready(function () { $('#sum').hide(); $('.hidden-print').show(); });
            }, 500);
            return false;
        }

        function PrintEPRPanel() {
            debugger;
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("<%=pnlExtraPunchedRecord.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
                $(document).ready(function () { $('#sum').hide(); $('.hidden-print').show(); });
            }, 500);
            return false;
        }
    </script>--%>
    <script type="text/javascript">
        function ValidateRegister() {

            if ($('#<%=ddlExamYear.ClientID%>').val() == 0) {
                $('#<%=ddlExamYear.ClientID%>').css('border-color', 'Red');
                jAlert('Select Exam Year.', 'Error', function () { $("#ddlExamYear").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamYear.ClientID%>').css('border-color', '');
            }

            if ($('#<%=ddlSession.ClientID%>').val() == 0) {
                $('#<%=ddlSession.ClientID%>').css('border-color', 'Red');
                jAlert('Select Session.', 'Error', function () { $("#ddlSession").focus() })
                return false;
            }
            else {
                $('#<%=ddlSession.ClientID%>').css('border-color', '');
            }

            <%--if ($('#<%=ddlExamFaculty.ClientID%>').val() == 0) {
                $('#<%=ddlExamFaculty.ClientID%>').css('border-color', 'Red');
                jAlert('Select Exam Faculty.', 'Error', function () { $("#ddlExamFaculty").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamFaculty.ClientID%>').css('border-color', '');
            }--%>
        }
    </script>
    <style type="text/css">
        .marginLeft {
            margin-left: 20px;
        }

        .width5 {
            width: 5%;
        }

        .width10 {
            width: 10%;
        }

        .width15 {
            width: 15%;
        }

        .width20 {
            width: 20%;
        }

        .width50 {
            width: 40%;
        }

        .height28widht54 {
            height: 28px;
            width: 54px;
        }

        .fontsize13px {
            font-size: 13px;
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
        <ProgressTemplate>
            <div class="loading active">
                <img src="loader.gif" alt="" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
        <ContentTemplate>
            <script type="text/javascript">
                Sys.Application.add_load(LoadAllScript)
            </script>
            <h3 class="header-title">
                <asp:Label ID="lblHeadeing" Text="Extra Exam Mapping" runat="server"></asp:Label></h3>
            <div class="box">
                <asp:Panel runat="server" ID="pnlRegistrmasteUpdate">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="col-sm-6">
                                    <span class="pull-right">Exam Year<asp:Label runat="server" ID="lblExamYear" ForeColor="Red" Text="*"></asp:Label></span>

                                </label>
                                <div class="col-sm-6">
                                    <asp:DropDownList ID="ddlExamYear" CssClass="form-control input-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="col-sm-4">
                                    Exam Session
                                    <asp:Label runat="server" ID="lblSession" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="ddlSession" CssClass="form-control input-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <asp:Button ID="btnShow" Text="Show" CssClass="btn btn-primary btn-xs" Style="height: 28px" OnClick="btnShow_Click" OnClientClick="return ValidateRegister();" runat="server" />
                                <asp:Button ID="btnClearAll" Text="Clear" CssClass="btn btn-danger btn-xs" Style="height: 28px" Enabled="false" OnClick="btnClearAll_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                    <%--<div id="divMapped" class="row" visible="false" runat="server">
                        <div class="col-sm-11">
                            <div class="text-center" style="border: ridge; height: 40px;">
                                <h4>Roll Number Mapping Status</h4>
                            </div>
                        </div>
                    </div>--%>
                    <div class="row">
                        <div class="col-sm-11">
                            <asp:GridView ID="grdExtraExamMapping" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdExtraExamMapping_RowCommand" OnRowDataBound="grdExtraExamMapping_RowDataBound" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="fontsize13px text-center width5" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Faculty CD" HeaderStyle-CssClass="fontsize13px text-center width20">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacultyCD" Text='<%#Eval ("FacultyCD") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblFacultyName" Text='<%#Eval("FacultyName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Exam Name" HeaderStyle-CssClass="fontsize13px text-center width50">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCtrl" Text='<%#Eval ("Ctrl") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblExamName" Text='<%#Eval("ExamName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Register No." HeaderStyle-CssClass="fontsize13px text-center width15" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblRegisterNo" Text='<%#Eval("RegisterNo") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Insert Extra Exam" HeaderStyle-CssClass="fontsize13px text-center width10" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnInsertExtraExam" CommandName="InsertExtraExam" CommandArgument='<%#Container.DataItemIndex %>' Text="Insert" CssClass="btn btn-success btn-xs" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-12 text-right" style="margin-top: 7px">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </div>
                    </div>

                    <%--<style>
                        .divOveFlow {
                            max-height: 370px;
                            overflow-y: scroll;
                        }

                        .p-r17 {
                            padding-right: 17px;
                        }
                    </style>--%>

                    <%-------------------------------------------Missing Punched Record Model Popup----------------------------------------------------%>

                    <%--<div class="modal fade in" runat="server" id="missingPunchedRecordModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
                        <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
                            <div class="modal-content" style="border-radius: 20px 20px; margin-top: 70px">
                                <asp:Panel ID="pnlMissingPunchedRecordPopUp" runat="server" Width="100%" Height="100%" Visible="true">
                                    <div class="modal-header c" style="background-color: #50618c; color: #FFFFFF; border-radius: 5px;">
                                        <h4 class="modal-title" style="text-align: center">
                                            <asp:Label ID="lblMissingPunchedRecord" Text="Missing Punched Record" runat="server"></asp:Label></h4>
                                    </div>
                                    <div class="modal-body">
                                        <style>
                                            .modal {
                                                display: none;
                                                overflow: hidden;
                                                position: fixed;
                                                top: 0;
                                                right: 0;
                                                bottom: 0;
                                                left: 0;
                                                z-index: 1050;
                                                -webkit-overflow-scrolling: touch;
                                                outline: 0;
                                            }

                                            .fade.in {
                                                opacity: 1;
                                            }

                                            .fade {
                                                opacity: 0;
                                                -webkit-transition: opacity .15s linear;
                                                -o-transition: opacity .15s linear;
                                                transition: opacity .15s linear;
                                            }

                                            .modal-dialog {
                                                width: 600px;
                                                margin: 30px auto;
                                            }

                                            .modal-content {
                                                -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                                box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                            }

                                            .modal-sm {
                                                width: 300px;
                                            }

                                            .modal-body {
                                                position: relative;
                                                padding: 4px 5px !important;
                                            }

                                                .modal-body table {
                                                    font-size: 12px;
                                                }

                                            .table-bordered1 {
                                                border: 1px solid #918f8f;
                                            }

                                                .table-bordered1 > thead > tr > th, .table-bordered1 > tbody > tr > th, .table-bordered1 > tfoot > tr > th, .table-bordered1 > thead > tr > td, .table-bordered1 > tbody > tr > td, .table-bordered1 > tfoot > tr > td {
                                                    border: 1px solid #918f8f;
                                                }

                                                .table-bordered1 > thead > tr > th, .table-bordered1 > thead > tr > td {
                                                    border-bottom-width: 2px;
                                                }

                                            .first_tr_hide tbody tr:first-child {
                                                display: none;
                                            }

                                            .hide_my_pdosi + tr {
                                                display: none;
                                            }

                                            .first_tr_hide tr td:first-child {
                                                display: none;
                                            }

                                            .calign {
                                                text-align: center;
                                            }

                                            .ralign {
                                                text-align: right;
                                            }

                                            .marginTop {
                                                margin-top: 5px;
                                            }

                                            .width10 {
                                                width: 10%;
                                            }

                                            .width65 {
                                                width: 65%;
                                            }

                                            .width50 {
                                                width: 50%;
                                            }

                                            .width15 {
                                                width: 15%;
                                                text-align: right;
                                            }
                                        </style>
                                        <table class="table-bordered1" style="width: 100%">
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Year : </b>
                                                    <asp:Label ID="lblMPRExamYear" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Session : </b>
                                                    <asp:Label ID="lblMPRExamSession" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Faculty : </b>
                                                    <asp:Label ID="lblMPRExamFaculty" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    <b>University Exam : </b>
                                                    <asp:Label ID="lblMPRUniversityExam" runat="server" />
                                                </td>
                                                <td colspan="4" style="text-align: left">
                                                    <b>DMS Exam : </b>
                                                    <asp:Label ID="lblMPRDMSExam" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <div class="divOveFlow">
                                                        <asp:GridView ID="grdMissingPunchedRecord" AutoGenerateColumns="false" Width="100%" CssClass="table-bordered1 first_tr_hide marginTop" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <tr>
                                                                            <th class="calign" style="width: 10%">
                                                                                <asp:Label ID="lblSrNo" Text="Sr. No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 10%">
                                                                                <asp:Label ID="lbRollNo" Text="Roll No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 30%">
                                                                                <asp:Label ID="lblStudentName" Text="Student Name" runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 10%">
                                                                                <asp:Label ID="lblCollegeCode" Text="College Code" runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 10%">
                                                                                <asp:Label ID="lblCenterCode" Text="Center Code" runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 15%">
                                                                                <asp:Label ID="lblRegisterNo" Text="Register No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 15%">
                                                                                <asp:Label ID="lblPageNo" Text="Page No." runat="server"></asp:Label></th>
                                                                        </tr>
                                                                        <tr class="hide_my_pdosi">
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRollNo" Text='<%#Eval("RollNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStudentName" Text='<%#Eval("StudentName") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCollegeCode" Text='<%#Eval("CollegeCode") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCenterCode" Text='<%#Eval("CentreCode") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRegisterNo" Text='<%#Eval("RegisterNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApprxPageNo" Text='<%#Eval("ApprxPageNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <div class="modal-footer">
                                    <button type="button" id="btnPrintMPR" class="font-awesome-font btn btn-info" onclick="PrintMPRPanel();" data-dismiss="modal">Print</button>
                                    <button type="button" id="btnCancelMPR" class="btn btn-danger" value="Cancel" onclick="CloseMPR();" data-dismiss="modal">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </div>--%>

                    <%-------------------------------------------Extra Punched Record Model Popup----------------------------------------------------%>

                    <%--<div class="modal fade in" runat="server" id="extraPunchedRecordModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
                        <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
                            <div class="modal-content" style="border-radius: 20px 20px; margin-top: 70px">
                                <asp:Panel ID="pnlExtraPunchedRecord" runat="server" Width="100%" Height="100%" Visible="true">
                                    <div class="modal-header c" style="background-color: #50618c; color: #FFFFFF; border-radius: 5px;">
                                        <h4 class="modal-title" style="text-align: center">
                                            <asp:Label ID="lblExtraPunchedRecord" Text="Extra Punched Record" runat="server"></asp:Label></h4>
                                    </div>
                                    <div class="modal-body">
                                        <style>
                                            .modal {
                                                display: none;
                                                overflow: hidden;
                                                position: fixed;
                                                top: 0;
                                                right: 0;
                                                bottom: 0;
                                                left: 0;
                                                z-index: 1050;
                                                -webkit-overflow-scrolling: touch;
                                                outline: 0;
                                            }

                                            .fade.in {
                                                opacity: 1;
                                            }

                                            .fade {
                                                opacity: 0;
                                                -webkit-transition: opacity .15s linear;
                                                -o-transition: opacity .15s linear;
                                                transition: opacity .15s linear;
                                            }

                                            .modal-dialog {
                                                width: 600px;
                                                margin: 30px auto;
                                            }

                                            .modal-content {
                                                -webkit-box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                                box-shadow: 0 5px 15px rgba(0,0,0,.5);
                                            }

                                            .modal-sm {
                                                width: 300px;
                                            }

                                            .modal-body {
                                                position: relative;
                                                padding: 4px 5px !important;
                                            }

                                                .modal-body table {
                                                    font-size: 12px;
                                                }

                                            .table-bordered1 {
                                                border: 1px solid #918f8f; /*918f8f*/
                                            }

                                                .table-bordered1 > thead > tr > th, .table-bordered1 > tbody > tr > th, .table-bordered1 > tfoot > tr > th, .table-bordered1 > thead > tr > td, .table-bordered1 > tbody > tr > td, .table-bordered1 > tfoot > tr > td {
                                                    border: 1px solid #918f8f;
                                                }

                                                .table-bordered1 > thead > tr > th, .table-bordered1 > thead > tr > td {
                                                    border-bottom-width: 2px;
                                                }

                                            .first_tr_hide tbody tr:first-child {
                                                display: none;
                                            }

                                            .hide_my_pdosi + tr {
                                                display: none;
                                            }

                                            .first_tr_hide tr td:first-child {
                                                display: none;
                                            }

                                            .calign {
                                                text-align: center;
                                            }

                                            .ralign {
                                                text-align: right;
                                            }

                                            .marginTop {
                                                margin-top: 5px;
                                            }

                                            .width10 {
                                                width: 10%;
                                            }

                                            .width65 {
                                                width: 65%;
                                            }

                                            .width50 {
                                                width: 50%;
                                            }

                                            .width15 {
                                                width: 15%;
                                                text-align: right;
                                            }
                                        </style>

                                        <table class="table-bordered1" style="width: 100%">
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Year : </b>
                                                    <asp:Label ID="lblEPRExamYear" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Session : </b>
                                                    <asp:Label ID="lblEPRExamSession" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Faculty : </b>
                                                    <asp:Label ID="lblEPRExamFaculty" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    <b>University Exam : </b>
                                                    <asp:Label ID="lblEPRUniversityExam" runat="server" />
                                                </td>
                                                <td colspan="4" style="text-align: left">
                                                    <b>DMS Exam : </b>
                                                    <asp:Label ID="lblEPRDMSExam" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <div class="divOveFlow">
                                                        <asp:GridView ID="grdExtraPunchedRecord" AutoGenerateColumns="false" Width="100%" CssClass="table-bordered1 first_tr_hide marginTop" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <tr>
                                                                            <th class="calign" style="width: 10%">
                                                                                <asp:Label ID="lblSrNo" Text="Sr. No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 30%">
                                                                                <asp:Label ID="lblRegisterNo" Text="Register No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 30%">
                                                                                <asp:Label ID="lblPageNo" Text="Page No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 30%">
                                                                                <asp:Label ID="lbRollNo" Text="Roll No." runat="server"></asp:Label></th>
                                                                        </tr>
                                                                        <tr class="hide_my_pdosi">
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRegisterNo" Text='<%#Eval("RegisterNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPageNo" Text='<%#Eval("PageNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRollNo" Text='<%#Eval("RollNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <div class="modal-footer">
                                    <button type="button" id="btnPrintEPR" class="font-awesome-font btn btn-info" onclick="PrintEPRPanel();" data-dismiss="modal">Print</button>
                                    <button type="button" id="btnCancelEPR" class="btn btn-danger" value="Cancel" onclick="CloseEPR();" data-dismiss="modal">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </div>--%>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>