<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Update.aspx.cs" Inherits="Update" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Button runat="server" ID="btnupdate" Text="Update" OnClick="btnupdate_Click" />
            <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label>
        </div>
    </form>
</body>
</html>
