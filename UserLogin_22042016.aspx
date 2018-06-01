<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserLogin_22042016.aspx.cs" Inherits="UserLogin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Amravati University</title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <!-- Bootstrap core CSS -->
    <link href="bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- Bootstrap theme -->
    <link href="bootstrap-3.3.6-dist/css/bootstrap-theme.min.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="bootstrap-3.3.6-dist/css/mycss.css" rel="stylesheet" type="text/css" />
    <link href="bootstrap-3.3.6-dist/css/header.css" rel="stylesheet" type="text/css" />

    <script src="bootstrap-3.3.6-dist/js/jquery-1.11.3.min.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">  
    </script>

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            $("#btnHideShowUsrNm").click(function () {
                if ($("#txtLoginId").attr("type") == "password") {
                    $('#btnHideShowUsrNm').text("Hide");
                    $("#txtLoginId").attr("type", "text");
                }
                else {
                    $('#btnHideShowUsrNm').text("Show");
                    $("#txtLoginId").attr("type", "password");
                }
            });
        });
    </script>
    <style type="text/css">
        span:hover
        {
            cursor: pointer;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server" class="form-horizontal">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="page-header">
            <h3 class="header-title text-center">Sant Gadge Baba Amravati University</h3>
        </div>
        <div class="navbar navbar-default" role="navigation">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".menu2">
                        <span class="sr-only">Toggle navigation</span> <span class="icon-bar"></span><span
                            class="icon-bar"></span><span class="icon-bar"></span>
                    </button>
                </div>
                <div class="navbar-collapse collapse menu2">
                    <ul class="nav navbar-nav navbar-right">
                    </ul>
                </div>
                <!--/.nav-collapse -->
            </div>
        </div>
        <div class="wrapper form-signin">
            <div class="container">
                <div class="card card-container">
                    <!-- <img class="profile-img-card" src="//lh3.googleusercontent.com/-6V8xOA6M7BA/AAAAAAAAAAI/AAAAAAAAAAA/rzlHcD0KYwo/photo.jpg?sz=120" alt="" /> -->
                    <img id="profile-img" class="profile-img-card" src="bootstrap-3.3.6-dist/images/logo.png" />
                    <asp:TextBox ID="txtLoginId" class="inputEmail form-control" placeholder="Enter User Name"
                        runat="server" required="" MaxLength="20" AutoCompleteType="Disabled"></asp:TextBox>


                    <div class="row">
                        <p id="Paragraph" align="right">
                            <%--<asp:LinkButton ID="btnHideShowUsrNm" CssClass="btn btn-link" runat="server" Text="Hide And Show User Name" />--%>
                            <span id="btnHideShowUsrNm">Hide</span>
                            <%-- <span id="SpanShow">[Show]</span>--%>
                        </p>
                    </div>
                    <asp:TextBox ID="txtLoginPwd" class="inputPassword form-control" placeholder="Enter Password"
                        TextMode="Password" runat="server" required="" MaxLength="12"></asp:TextBox>
                    <%-- <asp:RegularExpressionValidator ID="Regex2" runat="server" ControlToValidate="txtLoginPwd"
                        ValidationExpression="^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{8,}$"
                        ErrorMessage="Invalid User Id Or Password"
                        ForeColor="Red" />--%>
                    <%--<button class="btn btn-lg btn-primary btn-block btn-signin" id="Login" type="submit" onclick="return Login_onclick()">Login</button>--%>
                    <asp:Button ID="btnLogin" class="btn btn-lg btn-primary btn-block btn-signin" runat="server"
                        Text="Login" OnClick="btnLogin_Click" />
                    <span></span>
                    <!-- /form -->
                </div>
                <!-- /card-container -->
            </div>
            <!-- /container -->
        </div>
    </form>
</body>

<script src="bootstrap-3.3.6-dist/js/jquery.min.js" type="text/javascript"></script>
<script src="bootstrap-3.3.6-dist/js/jquery-1.10.2.js" type="text/javascript"></script>
<script src="bootstrap-3.3.6-dist/js/bootstrap.min.js" type="text/javascript"></script>
<script src="bootstrap-3.3.6-dist/js/script.js" type="text/javascript"></script>
</html>
