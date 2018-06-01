<%@ Page Language="C#"  AutoEventWireup="true"
    CodeFile="DecodePwd.aspx.cs" Inherits="DecodePwd" Title="Amravati University" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Amravati University</title>
</head>
<body>
    <form id="form1" runat="server">
    <table align="center">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Enter Encoded Password :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="txtDecodePwd" runat="server" Width="250px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <table align="center">
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="submit" OnClick="btnSubmit_Click" />
            </td>
        </tr>
    </table>
    <div align="center">
        <asp:Label ID="lblEncode" runat="server" ForeColor="Red"></asp:Label>
    </div>
    </form>
</body>
</html>
