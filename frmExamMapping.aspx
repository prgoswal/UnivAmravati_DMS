<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master" CodeFile="frmExamMapping.aspx.cs" Inherits="frmExamMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function CloseUEM() {
            $('#<%=universityExamModal.ClientID%>').hide();
        }

        function CloseDMSEM() {
            $('#<%=dmsExamModal.ClientID%>').hide();
        }

        function CloseUnivRecordsEM() {
            $('#<%=universityRecordsModal.ClientID%>').hide();
        }

        function LoadAllScript() {

            function CloseUEM() {
                $('#<%=universityExamModal.ClientID%>').hide();
            }

            function CloseDMSEM() {
                $('#<%=dmsExamModal.ClientID%>').hide();
            }

            function CloseUnivRecordsEM() {
                $('#<%=universityRecordsModal.ClientID%>').hide();
            }

            function LoadActive() {
                $('.loading').addClass(' active');
            }
        }
    </script>
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

            if ($('#<%=ddlExamFaculty.ClientID%>').val() == 0) {
                $('#<%=ddlExamFaculty.ClientID%>').css('border-color', 'Red');
                jAlert('Select Exam Faculty.', 'Error', function () { $("#ddlExamFaculty").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamFaculty.ClientID%>').css('border-color', '');
            }
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

        .width45 {
            width: 45%;
        }

        .fontsize13px {
            font-size: 13px;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        function CheckBoxCheck(rb) {
            debugger;
            var gv = document.getElementById("<%=grdDMSUnMappedExam.ClientID%>");
            var chk = gv.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < chk.length; i++) {
                if (chk[i].type == "checkbox") {
                    if (chk[i].checked && chk[i] != rb) {
                        chk[i].checked = false;
                        break;
                    }
                }
            }
        }
    </script>
    <script type="text/javascript">
        function PrintUnivExamPanel() {
            debugger;
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("<%=pnlUniversityExam.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.focus();
                printWindow.print();
                printWindow.close();
                $(document).ready(function () { $('#sum').hide(); $('.hidden-print').show(); });
            }, 500);
            return false;
        }

        function PrintDMSExamPanel() {
            debugger;
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("<%=pnlDMSExam.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.focus();
                printWindow.print();
                printWindow.close();
                $(document).ready(function () { $('#sum').hide(); $('.hidden-print').show(); });
            }, 500);
            return false;
        }

        function PrintUnivRecordsExamPanel() {
            debugger;
            $(document).ready(function () {
                $('#sum').show();
                $('.hidden-print').hide();
            });
            var panel = document.getElementById("<%=pnlUniversityRecords.ClientID %>");
            var printWindow = window.open('', '', 'height=600,width=1200');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.focus();
                printWindow.print();
                printWindow.close();
                $(document).ready(function () { $('#sum').hide(); $('.hidden-print').show(); });
            }, 500);
            return false;
        }
    </script>
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
                <asp:Label ID="lblHeadeing" Text="Exam Mapping" runat="server"></asp:Label></h3>
            <div class="box">
                <asp:Panel runat="server" ID="pnlRegistrmasteUpdate">
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="col-sm-4">
                                    Exam Year
                                    <asp:Label runat="server" ID="lblExamYear" ForeColor="Red" Text="*"></asp:Label>
                                </label>
                                <div class="col-sm-5">
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
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="col-sm-4">
                                    Exam Faculty
                                    <asp:Label runat="server" ID="lblExamFaculty" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-5">
                                    <asp:DropDownList ID="ddlExamFaculty" CssClass="form-control input-sm" runat="server">
                                    </asp:DropDownList>
                                </div>
                                <asp:Button ID="btnShow" Text="Show" CssClass="btn btn-primary btn-xs" Style="height: 28px" OnClick="btnShow_Click" OnClientClick="return ValidateRegister();" runat="server" />
                                <asp:Button ID="btnClearAll" Text="Clear" CssClass="btn btn-danger btn-xs" Style="height: 28px" Enabled="false" OnClick="btnClearAll_Click" runat="server" />
                            </div>
                        </div>
                    </div>
                    <div id="divUnMapped" class="row" visible="false" runat="server">
                        <div class="col-sm-11">
                            <div style="">
                                <style>
                                    .tbtn-container {
                                        margin-top: 5px;
                                        margin-left: 0px;
                                        width: 100%;
                                        float: left;
                                        border: ridge;
                                        padding-left: 10px;
                                        padding-right: 10px;
                                    }

                                    .tbtn1 {
                                        margin-top: 9px;
                                        width: 120px;
                                        float: left;
                                    }

                                    .tbtn2 {
                                        width: calc(100% - 240px);
                                        float: left;
                                    }

                                    .tbtn3 {
                                        margin-top: 9px;
                                        width: 120px;
                                        float: left;
                                    }
                                </style>
                                <div class="tbtn-container">
                                    <asp:Button ID="btnShowUnivExam" Text="Show Univ. Exam" Enabled="false" CssClass="tbtn1 btn btn-info btn-xs text-left" OnClick="btnShowUnivExam_Click" runat="server" />
                                    <h4 class="text-center tbtn2">Un - Mapped Exam</h4>
                                    <asp:Button ID="btnShowDMSExam" Text="Show DMS Exam" Enabled="false" CssClass="tbtn3 btn btn-info btn-xs text-right" OnClick="btnShowDMSExam_Click" runat="server" />
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-1">
                            <asp:Button ID="btnAdd" Text="Add" CssClass="btn btn-primary" Visible="false" OnClick="btnAdd_Click" runat="server" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <table id="tblUniversityGridHeader" class="text-center table-bordered" style="border: 1px #808080; height: 20px;" visible="false" runat="server">
                                <tr>
                                    <td style="width: 6%">Sr.&nbsp;No.</td>
                                    <td style="width: 7%"></td>
                                    <td style="width: 66%">University&nbsp;Exam</td>
                                    <td style="width: 17.1%">Univ.&nbsp;Records</td>
                                </tr>
                            </table>
                            <div id="divUnivUnMappedExam" class="table-responsive" runat="server">
                                <tr>
                                    <asp:GridView ID="grdUniversityUnMappedExam" AutoGenerateColumns="false" ShowHeader="false" Width="100%" OnRowCommand="grdUniversityUnMappedExam_RowCommand" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="fontsize13px text-center width5" ItemStyle-CssClass="text-center width5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="fontsize13px text-center width5" ItemStyle-CssClass="text-center width5">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkUniExam" CommandArgument='<%#Container.DataItemIndex %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="University Exam" HeaderStyle-CssClass="fontsize13px text-center width45" ItemStyle-CssClass="marginLeft width45">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUnivExamName" Text='<%#Eval("ExamName") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Univ. Records" HeaderStyle-CssClass="fontsize13px text-center width10" ItemStyle-CssClass="text-right marginLeft width10">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkUnivRecords" CommandName="UniversityRecords" CommandArgument='<%#Container.DataItemIndex %>' Text='<%#Eval("RecordCount") %>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </tr>
                                </tbody>
                                    </table>
                            </div>
                        </div>
                        <div class="col-sm-5">
                            <table id="tblDMSGridHeader" class="text-center table-bordered" style="border: 1px #808080 thin; height: 20px;" visible="false" runat="server">
                                <tr>
                                    <td style="width: .1%">Sr.&nbsp;No.</td>
                                    <td style="width: 3%"></td>
                                    <td style="width: 29%">DMS&nbsp;Exam</td>
                                </tr>
                            </table>
                            <div id="divDMSUnMappedExam" class="table-responsive" runat="server">
                                <tr>
                                    <asp:GridView ID="grdDMSUnMappedExam" AutoGenerateColumns="false" ShowHeader="false" Width="100%" runat="server">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="fontsize13px text-center width5" ItemStyle-CssClass="text-center width5">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-CssClass="fontsize13px text-center width5" ItemStyle-CssClass="text-center width5">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkDMSExam" CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" onclick="CheckBoxCheck(this)" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DMS Exam" HeaderStyle-CssClass="fontsize13px text-center width45" ItemStyle-CssClass="marginLeft width45">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCtrl" Text='<%#Eval("Ctrl") %>' Visible="false" runat="server" />
                                                    <asp:Label ID="lblExamName" Text='<%#Eval("ExamName") %>' runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </tr>
                                </tbody>
                                    </table>
                            </div>
                        </div>
                    </div>
                    <br />
                    <div id="divMapped" class="row" visible="false" runat="server">
                        <div class="col-sm-11">
                            <div class="text-center" style="border: ridge; height: 40px;">
                                <h4>Mapped Exam</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-11">
                            <asp:GridView ID="grdFinalUniAndDMSMappedExam" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdFinalUniAndDMSMappedExam_RowCommand" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="fontsize13px text-center width5" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="University Exam" HeaderStyle-CssClass="fontsize13px text-center width45">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExamYear" Text='<%#Eval ("ExamYear") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblExamSesion" Text='<%#Eval ("ExamSession") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblExamSesionID" Text='<%#Eval ("ExamSessionID") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblRecordCount" Text='<%#Eval ("FacultyCD") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblUniExamFaculty" Text='<%# Eval("UnivExamName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DMS Exam" HeaderStyle-CssClass="fontsize13px text-center width45">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacultyCD" Text='<%#Eval("DMSExamCD") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblFacultyName" Text='<%#Eval("DMSExamName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" HeaderStyle-CssClass="fontsize13px text-center width5" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Button ID="btnDelete" CommandName="DeleteRow" CommandArgument='<%#Container.DataItemIndex %>' Text="Del" CssClass="btn btn-danger btn-xs" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-sm-11 text-right" style="margin-top: 7px">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </div>
                        <div class="col-sm-1">
                            <asp:Button ID="btnSave" Text="Save" CssClass="btn btn-primary" Enabled="false" OnClick="btnSave_Click" runat="server" />
                        </div>
                    </div>

                    <style>
                        .divOveFlow {
                            max-height: 370px;
                            overflow-y: scroll;
                        }

                        .p-r17 {
                            padding-right: 17px;
                        }
                    </style>

                    <%-------------------------------------------University Exam Model Popup----------------------------------------------------%>

                    <div class="modal fade in" runat="server" id="universityExamModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
                        <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
                            <div class="modal-content" style="border-radius: 20px 20px; margin-top: 70px">
                                <asp:Panel ID="pnlUniversityExam" runat="server" Width="100%" Height="100%" Visible="true">
                                    <div class="modal-header c" style="background-color: #50618c; color: #FFFFFF; border-radius: 5px;">
                                        <h4 class="modal-title" style="text-align: center">
                                            <asp:Label ID="lblExtraPunchedRecord" Text="Exam Mapping - University Exam" runat="server"></asp:Label></h4>
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

                                            .width20 {
                                                width: 20%;
                                            }

                                            .width15 {
                                                width: 15%;
                                            }
                                        </style>
                                        <table class="table-bordered1" style="width: 100%">
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Year : </b>
                                                    <asp:Label ID="lblUnivExamYear" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Session : </b>
                                                    <asp:Label ID="lblUnivExamSession" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Faculty : </b>
                                                    <asp:Label ID="lblUnivExamFaculty" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <div class="divOveFlow">
                                                        <asp:GridView ID="grdPrintUnivExam" AutoGenerateColumns="false" Width="100%" CssClass="table-bordered1 first_tr_hide marginTop" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <tr>
                                                                            <th class="calign" style="width: 15%">
                                                                                <asp:Label ID="lblSrNo" Text="Sr. No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 65%">
                                                                                <asp:Label ID="lblUnivExam" Text="University Exam" runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 20%">
                                                                                <asp:Label ID="lblUnivRecords" Text="University Records" runat="server"></asp:Label></th>
                                                                        </tr>
                                                                        <tr class="hide_my_pdosi">
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="calign width15" ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="University Exam" HeaderStyle-CssClass="calign width65" ItemStyle-CssClass="marginLeft">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUnivExamName" Text='<%#Eval("ExamName") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="University Records" HeaderStyle-CssClass="calign width20" ItemStyle-CssClass="text-right marginLeft">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUniversityRecords" Text='<%#Eval("RecordCount") %>' runat="server" />
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
                                    <button type="button" id="btnPrintUnivExam" class="font-awesome-font btn btn-info" onclick="PrintUnivExamPanel();" data-dismiss="modal">Print</button>
                                    <button type="button" id="btnCancelEPR" class="btn btn-danger" value="Cancel" onclick="CloseUEM();" data-dismiss="modal">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-------------------------------------------DMS Exam Model Popup----------------------------------------------------%>

                    <div class="modal fade in" runat="server" id="dmsExamModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
                        <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
                            <div class="modal-content" style="border-radius: 20px 20px; margin-top: 70px">
                                <asp:Panel ID="pnlDMSExam" runat="server" Width="100%" Height="100%" Visible="true">
                                    <div class="modal-header c" style="background-color: #50618c; color: #FFFFFF; border-radius: 5px;">
                                        <h4 class="modal-title" style="text-align: center">
                                            <asp:Label ID="lblDMSExamHeader" Text="Exam Mapping - DMS Exam" runat="server"></asp:Label></h4>
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

                                            .width85 {
                                                width: 85%;
                                            }

                                            .width50 {
                                                width: 50%;
                                            }

                                            .width20 {
                                                width: 20%;
                                            }

                                            .width15 {
                                                width: 15%;
                                            }
                                        </style>
                                        <table class="table-bordered1" style="width: 100%">
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Year : </b>
                                                    <asp:Label ID="lblDMSExamYear" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Session : </b>
                                                    <asp:Label ID="lblDMSExamSession" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Faculty : </b>
                                                    <asp:Label ID="lblDMSExamFaculty" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <div class="divOveFlow">
                                                        <asp:GridView ID="grdDMSExam" AutoGenerateColumns="false" Width="100%" CssClass="table-bordered1 first_tr_hide marginTop" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <tr>
                                                                            <th class="calign" style="width: 15%">
                                                                                <asp:Label ID="lblSrNo" Text="Sr. No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 85%">
                                                                                <asp:Label ID="lblUnivExam" Text="DMS Exam" runat="server"></asp:Label></th>
                                                                        </tr>
                                                                        <tr class="hide_my_pdosi">
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="calign width15" ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DMS Exam" HeaderStyle-CssClass="calign width85" ItemStyle-CssClass="marginLeft">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDMSExamName" Text='<%#Eval("ExamName") %>' runat="server" />
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
                                    <button type="button" id="btnPrintDMSExam" class="font-awesome-font btn btn-info" onclick="PrintDMSExamPanel();" data-dismiss="modal">Print</button>
                                    <button type="button" id="btnCancelDMS" class="btn btn-danger" value="Cancel" onclick="CloseDMSEM();" data-dismiss="modal">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </div>

                    <%-------------------------------------------University Records Model Popup----------------------------------------------------%>

                    <div class="modal fade in" runat="server" id="universityRecordsModal" tabindex="-1" role="dialog" aria-labelledby="msgModalLabel" style="background: rgba(0, 0, 0, 0.5);">
                        <div class="modal-dialog modal-sm vertical-align-center" style="width: 60%;" role="document">
                            <div class="modal-content" style="border-radius: 20px 20px; margin-top: 70px">
                                <asp:Panel ID="pnlUniversityRecords" runat="server" Width="100%" Height="100%" Visible="true">
                                    <div class="modal-header c" style="background-color: #50618c; color: #FFFFFF; border-radius: 5px;">
                                        <h4 class="modal-title" style="text-align: center">
                                            <asp:Label ID="lblUniversityRecords" Text="University Records" runat="server"></asp:Label></h4>
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

                                            .width85 {
                                                width: 85%;
                                            }

                                            .width50 {
                                                width: 50%;
                                            }

                                            .width20 {
                                                width: 20%;
                                            }

                                            .width15 {
                                                width: 15%;
                                            }
                                        </style>
                                        <table class="table-bordered1" style="width: 100%">
                                            <tr>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Year : </b>
                                                    <asp:Label ID="lblUniRecExamYear" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Session : </b>
                                                    <asp:Label ID="lblUniRecExamSession" runat="server" />
                                                </td>
                                                <td colspan="2" style="text-align: left">
                                                    <b>Exam Faculty : </b>
                                                    <asp:Label ID="lblUniRecExamFaculty" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6" style="text-align: left">
                                                    <b>Exam Name : </b>
                                                    <asp:Label ID="lblUniRecExamName" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="6">
                                                    <div class="divOveFlow">
                                                        <asp:GridView ID="grdUnivRecords" AutoGenerateColumns="false" Width="100%" CssClass="table-bordered1 first_tr_hide marginTop" runat="server">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <tr>
                                                                            <th class="calign" style="width: 10%">
                                                                                <asp:Label ID="lblSrNo" Text="Sr. No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 10%">
                                                                                <asp:Label ID="lblRollNo" Text="Roll No." runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 50%">
                                                                                <asp:Label ID="lblStudentName" Text="Student Name" runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 15%">
                                                                                <asp:Label ID="lblCollegeCode" Text="CollegeCode" runat="server"></asp:Label></th>
                                                                            <th class="calign" style="width: 15%">
                                                                                <asp:Label ID="lblCentreCode" Text="Centre Code" runat="server"></asp:Label></th>
                                                                        </tr>
                                                                        <tr class="hide_my_pdosi">
                                                                        </tr>
                                                                    </HeaderTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="calign width15" ItemStyle-CssClass="calign">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DMS Exam" HeaderStyle-CssClass="calign width85" ItemStyle-CssClass="marginLeft">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRollNo" Text='<%#Eval("RollNo") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DMS Exam" HeaderStyle-CssClass="calign width85" ItemStyle-CssClass="marginLeft">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStudentName" Text='<%#Eval("StudentName") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DMS Exam" HeaderStyle-CssClass="calign width85" ItemStyle-CssClass="marginLeft">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCollegeCode" Text='<%#Eval("CollegeCode") %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="DMS Exam" HeaderStyle-CssClass="calign width85" ItemStyle-CssClass="marginLeft">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCentreCode" Text='<%#Eval("CentreCode") %>' runat="server" />
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
                                    <button type="button" id="btnPrintUnivRecords" class="font-awesome-font btn btn-info" onclick="PrintUnivRecordsExamPanel();" data-dismiss="modal">Print</button>
                                    <button type="button" id="btnCancelUnivRecords" class="btn btn-danger" value="Cancel" onclick="CloseUnivRecordsEM();" data-dismiss="modal">Cancel</button>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
