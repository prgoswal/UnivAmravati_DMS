<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="Registermaster.aspx.cs" Inherits="Registermaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function CalculateNoOfPages() {
            var txtPageFrom = document.getElementById('<%= txtPageNoFrom.ClientID %>');
            var txtPageTo = document.getElementById('<%= txtPageNoTo.ClientID %>');
            var x = parseInt(txtPageFrom.value); var y = parseInt(txtPageTo.value)
            if (txtPageFrom.value == "" || txtPageTo.value == "") {
                document.getElementById('<%=lblTotalNoOfPages.ClientID %>').value = "0";
                return;
            }
            document.getElementById('<%=lblTotalNoOfPages.ClientID %>').value = (y - x) + 1;
        }

        function ValidateRegister() {

            if ($('#<%=ddlFaculty.ClientID%>').val() == 0) {
                $('#<%=ddlFaculty.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Faculty', 'Error', function() { $("#ddlFaculty").focus() })
                return false;
            }
            else {
                $('#<%=ddlFaculty.ClientID%>').css('border-color', '');
            }

            if ($('#<%=ddlExamname.ClientID%>').val() == 0) {
                $('#<%=ddlExamname.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Name', 'Error', function() { $("#ddlExamname").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamname.ClientID%>').css('border-color', '');
            }


            if ($('#<%=ddlExamYear.ClientID%>').val() == 'Select Year') {
                $('#<%=ddlExamYear.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Year', 'Error', function() { $("#ddlExamYear").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamYear.ClientID%>').css('border-color', '');
            }


            if ($('#<%=ddlSession.ClientID%>').val() == 0) {
                $('#<%=ddlSession.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Session', 'Error', function() { $("#ddlSession").focus() })
                return false;
            }
            else {
                $('#<%=ddlSession.ClientID%>').css('border-color', '');
            }

            if ($('#<%=ddlExamType.ClientID%>').val() == 0) {
                $('#<%=ddlExamType.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Type', 'Error', function() { $("#ddlExamType").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamType.ClientID%>').css('border-color', '');
            }

            if ($('#<%=ddlCopyType.ClientID%>').val() == 0) {
                $('#<%=ddlCopyType.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select TR Type', 'Error', function() { $("#ddlCopyType").focus() })
                return false;
            }
            else {
                $('#<%=ddlCopyType.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtRefBookNo.ClientID%>').val() == '') {
                $('#<%=txtRefBookNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter No. Of Books', 'Error', function() { $("#txtRefBookNo").focus() })
                return false;
            }
            else {
                $('#<%=txtRefBookNo.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtCurrentBookNo.ClientID%>').val() == '') {
                $('#<%=txtCurrentBookNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Current Book No.', 'Error', function() { $("#txtCurrentBookNo").focus() })
                return false;
            }
            else {
                $('#<%=txtCurrentBookNo.ClientID%>').css('border-color', '');
            }



            if ($('#<%=txtPageNoFrom.ClientID%>').val() == '') {
                $('#<%=txtPageNoFrom.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Page No. From', 'Error', function() { $("#txtPageNoFrom").focus() })
                return false;
            }
            else {
                $('#<%=txtPageNoFrom.ClientID%>').css('border-color', '');
            }


            if ($('#<%=txtPageNoTo.ClientID%>').val() == '') {
                $('#<%=txtPageNoTo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Page No. To', 'Error', function() { $("#txtPageNoTo").focus() })
                return false;
            }
            else {
                $('#<%=txtPageNoTo.ClientID%>').css('border-color', '');
            }
        }
        function ValidateCancelReason() {
            if ($('#<%=txtCnclReason.ClientID%>').val() == 0) {
                $('#<%=txtCnclReason.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Cancel Reason', 'Error', function() { $("#txtCnclReason").focus() })
                return false;
            }
            else {
                $('#<%=txtCnclReason.ClientID%>').css('border-color', '');
            }
        }



    </script>

    <h3 class="header-title">
        <asp:Label runat="server" ID="lblHeadeing"></asp:Label></h3>
    <div class="box">
        <div class="row">
            <div class="col-md-12 col-lg-10">
                <div class="form-group">
                    <%--<label class="col-sm-2 col-md-2 col-lg-4">Register No.</label>--%>
                    <asp:Label runat="server" ID="lblRegNo" Style="font-weight: 600;" class="col-sm-2 col-md-2 col-lg-2"
                        Text="Register No.">
                    </asp:Label>
                    <div class="col-sm-3 col-md-3 col-lg-3">
                        <asp:TextBox ID="txtRegisterNo" CssClass="form-control input-sm" placeholder="Enter Register No."
                            runat="server"></asp:TextBox>
                    </div>
                    <div class="col-sm-2 col-md-2 col-lg-2">
                        <asp:LinkButton ID="linkSearch" CssClass="btn btn-sm btn-primary" runat="server"
                            Text="" OnClick="linkSearch_Click"><i class="fa fa-search"> </i>&nbsp;Search</asp:LinkButton>
                    </div>
                    <div class="col-sm-2 col-md-5 col-lg-5">
                        <asp:Label runat="server" ID="lblSrchRegNo" ForeColor="Red"></asp:Label>
                    </div>
                </div>
            </div>
        </div>
        <asp:Panel runat="server" ID="pnlRegistrmasteUpdate">
            <asp:UpdatePanel runat="server" ID="updatepanel1">
                <ContentTemplate>
                    <div class="row">
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-4 col-md-5 col-lg-4">
                                    Faculty
                                    <asp:Label runat="server" ID="lblFaculty" ForeColor="Red" Text="*"></asp:Label>
                                </label>
                                <div class="col-sm-8 col-md-7 col-lg-8">
                                    <asp:DropDownList ID="ddlFaculty" CssClass="form-control input-sm" runat="server"
                                        OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="True"
                                        TabIndex="1">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-8 col-md-8 col-lg-8">
                            <div class="form-group">
                                <label class="col-sm-2 col-md-3 col-lg-2">
                                    Exam Name
                                    <asp:Label runat="server" ID="lblExamName" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-10 col-md-9 col-lg-10">
                                    <asp:DropDownList ID="ddlExamname" CssClass="form-control input-sm" runat="server"
                                        OnSelectedIndexChanged="ddlExamname_SelectedIndexChanged" AutoPostBack="True"
                                        TabIndex="2">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-6 col-md-5 col-lg-4">
                                    Exam Year
                                    <asp:Label runat="server" ID="lblExamYear" ForeColor="Red" Text="*"></asp:Label>
                                </label>
                                <div class="col-sm-6 col-md-7 col-lg-8">
                                    <asp:DropDownList ID="ddlExamYear" CssClass="form-control input-sm" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlExamYear_SelectedIndexChanged"
                                        TabIndex="3">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-6 col-md-6 col-lg-4">
                                    Session
                                    <asp:Label runat="server" ID="lblSession" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-6 col-md-6 col-lg-8">
                                    <asp:DropDownList ID="ddlSession" CssClass="form-control input-sm" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"
                                        TabIndex="4">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-2 col-md-6 col-lg-4">
                                    Exam Type
                                    <asp:Label runat="server" ID="Label1" ForeColor="Red" Text="*"></asp:Label>
                                </label>
                                <div class="col-sm-10 col-md-6 col-lg-8">
                                    <asp:DropDownList ID="ddlExamType" CssClass="form-control input-sm" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged"
                                        TabIndex="5">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-2 col-md-5 col-lg-4">
                                    TR Type
                                    <asp:Label runat="server" ID="lblCopyType" ForeColor="Red" Text="*"></asp:Label>
                                </label>
                                <div class="col-sm-2 col-md-7 col-lg-8">
                                    <asp:DropDownList ID="ddlCopyType" CssClass="form-control input-sm" runat="server"
                                        AutoPostBack="True" OnSelectedIndexChanged="ddlCopyType_SelectedIndexChanged"
                                        TabIndex="6">
                                        <asp:ListItem Text="Select TR Type" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Foil" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="Counter Foil" Value="2"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-6 col-md-6 col-lg-4">
                                    No. Of Books
                                    <asp:Label runat="server" ID="Label2" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-6 col-md-6 col-lg-8">
                                    <asp:TextBox ID="txtRefBookNo" CssClass="form-control input-sm" runat="server" MaxLength="10"
                                        TabIndex="7"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtRefBookNo"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-6 col-md-6 col-lg-4">
                                    Current Book No.
                                    <asp:Label runat="server" ID="Label3" ForeColor="Red" Text="*"></asp:Label></label>
                                <div class="col-sm-6 col-md-6 col-lg-8">
                                    <asp:TextBox ID="txtCurrentBookNo" CssClass="form-control input-sm" runat="server"
                                        MaxLength="10" TabIndex="8"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCurrentBookNo"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-2 col-md-5 col-lg-4">
                                    Page No. From
                                    <asp:Label runat="server" ID="lblPageNoFrom" ForeColor="Red" Text="*"></asp:Label>
                                </label>
                                <div class="col-sm-2 col-md-7 col-lg-8">
                                    <asp:TextBox ID="txtPageNoFrom" CssClass="form-control input-sm" runat="server" MaxLength="4"
                                        onblur="CalculateNoOfPages()" TabIndex="9"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPageNoFrom"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-6 col-md-6 col-lg-4">
                                    Page No. To
                                    <asp:Label runat="server" ID="lblPageNoTo" ForeColor="Red" Text="*"></asp:Label>
                                </label>
                                <div class="col-sm-6 col-md-6 col-lg-8">
                                    <asp:TextBox ID="txtPageNoTo" CssClass="form-control input-sm" runat="server" MaxLength="4"
                                        onblur="CalculateNoOfPages()" TabIndex="10"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPageNoTo"
                                        ValidChars="0123456789">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                            <div class="form-group">
                                <label class="col-sm-6 col-md-6 col-lg-4">
                                    No. Of Pages</label>
                                <div class="col-sm-6 col-md-6 col-lg-8">
                                    <asp:TextBox runat="server" ID="lblTotalNoOfPages" CssClass="form-control" Enabled="false"
                                        TabIndex="11" onblur="CalculateNoOfPages()"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-4 col-lg-4">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblCnclReason" CssClass="col-sm-2 col-md-5 col-lg-4"
                                    Style="font-weight: 600" Text=" Cancel Reason" Visible="false">
                                 <%--<span style="color:red">*</span>--%>
                                </asp:Label>
                                <%-- <asp:Label runat="server" ID="Label4" ForeColor="Red" Text="*"></asp:Label>--%>
                                <div class="col-sm-2 col-md-7 col-lg-8">
                                    <asp:TextBox runat="server" ID="txtCnclReason" CssClass="form-control input-sm" MaxLength="48"
                                        Visible="false" TabIndex="12"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                        </div>
                        <div class="col-sm-4 col-md-4 col-lg-4">
                        </div>
                    </div>
                    <asp:Panel ID="pnlAvailablePages" runat="server">
                        <div class="row">
                            <div class="col-md-3 col-lg-3">
                                <p class="pull-left">
                                    <span style="color: red; font-size: 20px;">*</span> Fields Are Mandetory.
                                </p>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="box">
                                    <p class="text-center" style="font-weight: 600; text-decoration: underline; margin-top: -10px;">
                                        Available Pages
                                    </p>
                                    <div class="row">
                                        <div class="col-md-5 col-lg-5">
                                            <div class="form-group">
                                                <label class="col-sm-6 col-md-5 col-lg-4">
                                                    From
                                                    <asp:Label runat="server" ID="Label4" ForeColor="Red" Text="*"></asp:Label>
                                                </label>
                                                <div class="col-sm-6 col-md-7 col-lg-8">
                                                    <asp:TextBox ID="txtPageNoFromAvail" CssClass="form-control input-sm" runat="server"
                                                        MaxLength="4" onblur="CalculateNoOfPages()" TabIndex="13"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtPageNoFromAvail"
                                                        ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4 col-md-5 col-lg-5">
                                            <div class="form-group">
                                                <label class="col-sm-6 col-md-4 col-lg-4">
                                                    To
                                                    <asp:Label runat="server" ID="Label5" ForeColor="Red" Text="*"></asp:Label>
                                                </label>
                                                <div class="col-sm-6 col-md-7 col-lg-8">
                                                    <asp:TextBox ID="txtPageNoToAvail" CssClass="form-control input-sm" runat="server"
                                                        MaxLength="4" onblur="CalculateNoOfPages()" TabIndex="14"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtPageNoToAvail"
                                                        ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-2 col-md-2 col-lg-2">
                                            <asp:LinkButton ID="linkAddAvailablePages" CssClass="btn btn-primary btn-sm" runat="server"
                                                OnClick="linkAddAvailablePages_Click" OnClientClick="return ValidateRegister();"
                                                TabIndex="15"><i class="fa fa-plus"></i>&nbsp;Add</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="table-responsive">
                                                <tr>
                                                    <asp:Panel ID="pnlAvlbPgs" runat="server">
                                                        <div style="width: 400px; font-weight: bold;">
                                                            <table style="border: 1px; border-style: double" rules="all">
                                                                <tr>
                                                                    <td style="width: 40px; font-family: Times New Roman; font-weight: bold; text-align: center">
                                                                        Sr.No.
                                                                    </td>
                                                                    <td style="width: 97px; font-family: Times New Roman; font-weight: bold; text-align: center">
                                                                        PageNo.From
                                                                    </td>
                                                                    <td style="width: 89px; font-family: Times New Roman; font-weight: bold; text-align: center">
                                                                        PageNo.To
                                                                    </td>
                                                                    <td style="width: 93px; font-family: Times New Roman; font-weight: bold; text-align: center">
                                                                        No.OfPages
                                                                    </td>
                                                                    <td style="width: 77px;font-family:Times New Roman;font-weight:bold;text-align:center">
                                                                    Action
                                                                </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                                    <div style="overflow: auto; height: 100px; width: 90.8%;">
                                                        <asp:GridView ID="grdAvailablePages" ShowHeader="false" runat="server" class="" AutoGenerateColumns="False"
                                                            Width="100%" Style="text-align: center; border: 1px; borderstyle: Double;" OnRowDeleting="grdAvailablePages_RowDeleting">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sr. No." ItemStyle-Width="40px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Page No. From" ItemStyle-Width="97px" DataField="PageNoFrom" />
                                                                <asp:BoundField HeaderText="Page No. To" ItemStyle-Width="89px" DataField="PageNoTo" />
                                                                <asp:BoundField HeaderText="No. Of Pages" ItemStyle-Width="93px" DataField="TotalPages" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lnkDelete" Text="Delete" Style="text-align: center"
                                                                            CommandName="Delete" ForeColor="Red" OnClientClick="return confirm ('Do You Really Want To Delete ?')">
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                </tr>
                                            </div>
                                            <div class="pull-right badge badge-info">
                                                <asp:Label runat="server" ID="lblTotalMsg" ForeColor="white" Text="Total Pages: "></asp:Label>
                                                <asp:Label runat="server" ID="lblTotal" ForeColor="white" Text="0"></asp:Label></div>
                                       
                                            <br />
                                              <asp:Label runat="server" ID="lblAvalPgsMsg" ForeColor="Red" Text=""></asp:Label>
                                             </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-3 col-lg-3">
                                <div class="pull-right">
                                    <asp:Label runat="server" ID="lblLastRegNo" ForeColor="Red" Style="color: #F44336;
                                        font-size: 17px;"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                    <asp:Label runat="server" ID="lblmsg" ForeColor="Red"></asp:Label>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="pull-right">
                                <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server" OnClick="linkSave_Click"
                                    Text="" OnClientClick="return ValidateRegister();" TabIndex="16"> <i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>
                                <asp:LinkButton ID="linkCncl" class="btn btn-primary" runat="server" Text="" TabIndex="17"
                                    Visible="false" OnClick="linkCncl_Click" OnClientClick="return ValidateCancelReason();"> <i class="fa fa-save"></i>&nbsp;Cancel</asp:LinkButton>
                                <asp:LinkButton ID="linkCancel" class="btn btn-primary" runat="server" OnClick="linkCancel_Click"
                                    TabIndex="18">
                                      <i class="fa fa-close"></i>&nbsp;Clear</asp:LinkButton>
                            </div>
                        </div>
                        <br />
                        <br />
                        <div class="container">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <tr>
                                            <asp:GridView ID="gridDetails" runat="server" class="table table-bordered" AutoGenerateColumns="False"
                                                Width="100%">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Register No." DataField="RegisterNo" />
                                                    <asp:BoundField HeaderText="Exam Name" DataField="ExamName" />
                                                    <asp:BoundField HeaderText="TR Type" DataField="TRPart" />
                                                    <asp:BoundField HeaderText="Page No." DataField="PageNo" />
                                                    <asp:BoundField HeaderText="Book No." DataField="BookNo" />
                                                </Columns>
                                            </asp:GridView>
                                        </tr>
                                        </tbody> </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
</asp:Content>
