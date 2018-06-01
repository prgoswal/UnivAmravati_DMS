<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="bootstrap-3.3.6-dist/css/jquery.alerts.css" rel="stylesheet" />
    <script src="bootstrap-3.3.6-dist/js/jquery.min.js"></script>
    <script src="bootstrap-3.3.6-dist/js/bootstrap.min.js"></script>
    <script src="bootstrap-3.3.6-dist/js/jquery-1.7.1.js"></script>
    <script src="bootstrap-3.3.6-dist/js/jquery.ui.draggable.js"></script>
    <script src="bootstrap-3.3.6-dist/js/jquery.alerts.js"></script>

    <script type="text/ecmascript">

        function ValidateChangePwd() {

            if ($('#<%=txtOldPwd.ClientID%>').val() == '') {
                $('#<%=txtOldPwd.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Old Password', 'Error', function () { $("#txtOldPwd").focus() })
                return false;
            }
            else {
                $('#<%=txtOldPwd.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtNewPwd.ClientID%>').val() == '') {
                $('#<%=txtNewPwd.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter New Password', 'Error', function () { $("#txtNewPwd").focus() })
                return false;
            }
            else {
                $('#<%=txtNewPwd.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtConfirmPwd.ClientID%>').val() == '') {
                $('#<%=txtConfirmPwd.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Confirm Password', 'Error', function () { $("#txtConfirmPwd").focus() })
                return false;
            }
            else {
                $('#<%=txtConfirmPwd.ClientID%>').css('border-color', '');
            }


            if ($('#<%=txtConfirmPwd.ClientID%>').val() != $('#<%=txtNewPwd.ClientID%>').val()) {
                $('#<%=txtNewPwd.ClientID%>').css('border-color', 'Red');
                jAlert('New Password And Confirm Password Are MisMatch', 'Error', function () {
                    $("#txtNewPwd").focus()
                })
                return false;
            }
            else {
                $('#<%=txtNewPwd.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtNewPwd.ClientID%>').val == $('#<%=txtOldPwd.ClientID%>').val()) {
                $('#<%=txtNewPwd.ClientID%>').css('border-color', 'Red');
                jAlert('Old Password And New Password Should Not Be Same', 'Error', function () { $("#txtNewPwd").focus() })
                return false;
            }
            else {
                $('#<%=txtNewPwd.ClientID %>').css('border-color', '');
            }

        }
    </script>
    <asp:UpdatePanel runat="server" ID="updatepanelExamMaster">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-2 col-lg-3">
                </div>

                <div class="col-md-8 col-lg-6">
                    <div class="change-password">
                        <h3 class="text-center">Change Password</h3>
                        <hr class="changecolor" />
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                Old Password &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-4 col-lg-4">
                                <asp:TextBox ID="txtOldPwd" CssClass="form-control" runat="server" TabIndex="1"
                                    PlaceHolder="Enter Old Password" TextMode="Password" MaxLength="12"></asp:TextBox>
                            </div>
                            <div class="col-md-4 col-lg-4">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtOldPwd"
                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                    ErrorMessage="Minimum 8 characters" ForeColor="Red" />
                            </div>
                            <div class="col-md-4 col-lg-4">
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                New Password &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-4 col-lg-4">
                                <asp:TextBox ID="txtNewPwd" CssClass="form-control"
                                    PlaceHolder="Enter New Password" runat="server" TabIndex="2"
                                    TextMode="Password" MaxLength="12"></asp:TextBox>
                            </div>
                            <div class="col-md-4 col-lg-4">
                                <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txtNewPwd"
                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                    ErrorMessage="Minimum 8 characters" ForeColor="Red" />
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                Confirm Password &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-4 col-lg-4">
                                <asp:TextBox ID="txtConfirmPwd" CssClass="form-control"
                                    PlaceHolder="Enter Confirm Password" runat="server" TabIndex="3"
                                    TextMode="Password" MaxLength="12"></asp:TextBox>
                            </div>
                            <div class="col-md-4 col-lg-4">
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtConfirmPwd"
                                    ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                                    ErrorMessage="Minimum 8 characters" ForeColor="Red" />
                            </div>
                        </div>
                        <asp:Label runat="server" ID="lblmsg" ForeColor="Red"></asp:Label>
                        <div class="form-group">
                            <div class="col-md-4 col-lg-4">
                                <label><span style="color: Red; font-size: 15px;">*&nbsp;</span>Fields Are Mandetory.</label>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="center-block">
                                    <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server"
                                        TabIndex="4" OnClick="linkSave_Click"
                                        OnClientClick="return ValidateChangePwd();"><i class="fa fa-save"></i>&nbsp;Change</asp:LinkButton>
                                    <asp:LinkButton ID="linkCancel" class="btn btn-primary" runat="server"
                                        TabIndex="5" OnClick="linkCancel_Click"><i class="fa fa-close"></i> &nbsp;Clear</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <hr class="changecolor" />
                        <p class="text-center" style="font-weight: 600;">
                            <span style="color: red; font-size: 20px;">*&nbsp;</span> Password atleast 1 Alphabet,
            1 Number and 1 Special Character
                        </p>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="col-md-2 col-lg-3">
    </div>

</asp:Content>
