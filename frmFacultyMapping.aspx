<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master" CodeFile="frmFacultyMapping.aspx.cs" Inherits="frmFacultyMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function LoadAllScript() {
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
        }
    </script>
    <style type="text/css">
        .marginLeft {
            margin-left: 20px;
        }

        .width5 {
            width: 5%;
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
            var gv = document.getElementById("<%=grdDMSExamFaculty.ClientID%>");
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
                <asp:Label ID="lblHeadeing" Text="Faculty Mapping" runat="server"></asp:Label></h3>
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
                    <div id="divUnMapped" class="row" visible="false" runat="server">
                        <div class="col-sm-11">
                            <div class="text-center" style="border: ridge; height: 40px;">
                                <h4>Un - Mapped Faculties</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <asp:GridView ID="grdUniversityExamFaculty" AutoGenerateColumns="false" Width="100%" runat="server">
                                <Columns>
                                    <asp:TemplateField ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkUniExamFaculty" CommandArgument='<%#Container.DataItemIndex %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="University Exam Faculty" HeaderStyle-CssClass="fontsize13px text-center" ItemStyle-CssClass="marginLeft">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSoftDataFacultyCD" Text='<%#Eval("SoftDataFacultyCD") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblSoftDataFacultyName" Text='<%#Eval("SoftDataFacultyName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-5">
                            <asp:GridView ID="grdDMSExamFaculty" AutoGenerateColumns="false" Width="100%" runat="server">
                                <Columns>
                                    <asp:TemplateField ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkDMSExamFaculty" CommandArgument='<%#Container.DataItemIndex %>' AutoPostBack="true" onclick="CheckBoxCheck(this)" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DMS Exam Faculty" HeaderStyle-CssClass="fontsize13px text-center" ItemStyle-CssClass="marginLeft">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacultyCD" Text='<%#Eval("FacultyCD") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblFacultyName" Text='<%#Eval("FacultyName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-1">
                            <asp:Button ID="btnAdd" Text="Add" CssClass="btn btn-primary" Visible="false" OnClick="btnAdd_Click" runat="server" />
                        </div>
                    </div>
                    <br />
                    <div id="divMapped" class="row" visible="false" runat="server">
                        <div class="col-sm-11">
                            <div class="text-center" style="border: ridge; height: 40px;">
                                <h4>Mapped Faculties</h4>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-11">
                            <asp:GridView ID="grdFinalUniAndDMSFaculty" AutoGenerateColumns="false" Width="100%" OnRowCommand="grdFinalUniAndDMSFaculty_RowCommand" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr. No." HeaderStyle-CssClass="fontsize13px text-center width5" ItemStyle-CssClass="text-center">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSrNo" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="University Exam Faculty" HeaderStyle-CssClass="fontsize13px text-center width45">
                                        <ItemTemplate>
                                            <asp:Label Text='<%#Eval ("ExamYear") %>' Visible="false" runat="server" />
                                            <asp:Label Text='<%#Eval ("ExamSession") %>' Visible="false" runat="server" />
                                            <asp:Label Text='<%#Eval ("ExamSessionID") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblUniExamFaculty" Text='<%# Eval("UnivFacultyName") %>' runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DMS Exam Faculty" HeaderStyle-CssClass="fontsize13px text-center width45">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFacultyCD" Text='<%#Eval("FacultyCD") %>' Visible="false" runat="server" />
                                            <asp:Label ID="lblFacultyName" Text='<%#Eval("DMSFacultyName") %>' runat="server" />
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
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
