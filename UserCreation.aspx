<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="UserCreation.aspx.cs" Inherits="UserCreation" Title="Amravati University" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="bootstrap-3.3.6-dist/css/jquery.alerts.css" rel="stylesheet" />

    <script src="bootstrap-3.3.6-dist/js/jquery.alerts.js"></script>

    <script type="text/javascript" language="javascript">
        function ValidateUserDetails() {

            if ($('#<%=ddlUserType.ClientID%>').val() == 0) {
                $('#<%=ddlUserType.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select User Type', 'Error', function() { $("#ddlUserType").focus() })
                return false;
            }
            else {
                $('#<%=ddlUserType.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtUserName.ClientID%>').val() == '') {
                $('#<%=txtUserName.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User Name', 'Error', function() { $("#txtUserName").focus() })
                return false;
            }
            else {
                $('#<%=txtUserName.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtUserState.ClientID%>').val() == '') {
                $('#<%=txtUserState.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User State', 'Error', function() { $("#txtUserState").focus() })
                return false;
            }
            else {
                $('#<%=txtUserState.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtUserCity.ClientID%>').val() == '') {
                $('#<%=txtUserCity.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User City', 'Error', function() { $("#txtUserCity").focus() })
                return false;
            }
            else {
                $('#<%=txtUserCity.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtEmailAddress.ClientID%>').val() == '') {
                $('#<%=txtEmailAddress.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Email Address', 'Error', function() { $("#txtEmailAddress").focus() })
                return false;
            }
            else {
                $('#<%=txtEmailAddress.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtContactNo.ClientID%>').val() == '') {
                $('#<%=txtContactNo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Contact Number', 'Error', function() { $("#txtContactNo").focus() })
                return false;
            }
            else {
                $('#<%=txtContactNo.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtAddress.ClientID%>').val() == '') {
                $('#<%=txtAddress.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User Address', 'Error', function() { $("#txtAddress").focus() })
                return false;
            }
            else {
                $('#<%=txtAddress.ClientID%>').css('border-color', '');
            }
            if ($('#<%=txtLoginId.ClientID%>').val() == '') {
                $('#<%=txtLoginId.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User Id', 'Error', function() { $("#txtLoginId").focus() })
                return false;
            }
            else {
                $('#<%=txtLoginId.ClientID%>').css('border-color', '');
            }
        }
    </script>
   <%-- <script type="text/javascript">
     var email = document.getElementById('txtEmailAddress');
            var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

            if (!filter.test(email.value)) {
                alert('Please provide a valid email address');
                email.focus;
                return false;
            }
    </script>--%>
    <div class="row">
        <div class="col-md-1 col-lg-2">
        </div>
        <div class="col-md-10 col-lg-8">
            <div class="user-creation">
                <h3 class="text-center">
                    User Creation</h3>
                <hr class="changecolor" />
                <div class="row">
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                User Type &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:DropDownList ID="ddlUserType" CssClass="form-control input-sm" runat="server"
                                    TabIndex="1">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                User Full Name &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtUserName" CssClass="form-control input-sm" runat="server" MaxLength="33"
                                    TabIndex="2"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtUserName"
                                    ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                Email Address &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtEmailAddress" CssClass="form-control input-sm" runat="server"
                                    MaxLength="33" TabIndex="3"></asp:TextBox>
                                <%-- <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtEmailAddress"
                                    ErrorMessage="Please Enter Valid Email Id" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ForeColor="Red">
                                </asp:RegularExpressionValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                Contact No. &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtContactNo" CssClass="form-control input-sm" runat="server" MaxLength="10"
                                    TabIndex="4"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtContactNo"
                                    ValidChars="0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                State &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtUserState" CssClass="form-control input-sm" runat="server" MaxLength="28"
                                    TabIndex="5"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtUserState"
                                    ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ. ">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                City &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtUserCity" CssClass="form-control input-sm" runat="server" MaxLength="28"
                                    TabIndex="6"></asp:TextBox>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtUserCity"
                                    ValidChars="abcdefghijklmnopqrstuvwxABCDEFGHIJKLMNOPQRSTUVWXYZ. ">
                                </cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                    </div>
                    
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                Address &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtAddress" CssClass="form-control input-sm" runat="server" TextMode="MultiLine"
                                    MaxLength="148" TabIndex="7"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6 col-lg-6">
                        <div class="form-group">
                            <label class="col-md-4 col-lg-4">
                                Login Id &nbsp;<span style="color: Red">*</span></label>
                            <div class="col-md-6 col-lg-6">
                                <asp:TextBox ID="txtLoginId" CssClass="form-control input-sm" runat="server" MaxLength="14"
                                    TabIndex="8"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtLoginId"
                                    ID="RegularExpressionValidator2" ValidationExpression="^[\s\S]{8,14}$" runat="server"
                                    ErrorMessage="Minimum 8 characters Allowed And Max 14" ForeColor="Red"></asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center">
                    <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server" OnClientClick="return ValidateUserDetails()"
                        OnClick="linkSave_Click" TabIndex="9"><i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>
                    <asp:LinkButton ID="linkClear" class="btn btn-primary" runat="server" TabIndex="10"
                        OnClick="linkClear_Click"><i class="fa fa-close"></i> &nbsp;Clear</asp:LinkButton>
                    <br />
                    <asp:Label runat="server" ID="lblmsg" ForeColor="Red" Text=""></asp:Label>
                </div>
                <%--<hr />--%>
                <hr class="changecolor" />
                <p class="text-center" style="font-weight: 600;">
                    <span style="color: Red; font-size: 15px; text-align: center;">*&nbsp;</span>Fields
                    Are Mandetory.</p>
            </div>
        </div>
    </div>
    <div class="col-md-1 col-lg-2">
    </div>
</asp:Content>
