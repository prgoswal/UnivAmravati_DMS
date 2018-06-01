<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptShowRdlc.aspx.cs" Inherits="RptShow" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div>
            <table style="width: 100%; height: 100%">
                <tr>
                    <td>
                        <asp:Button runat="server" ID="btnBack" Text="Back To Home" OnClick="btnBack_Click" />
                    </td>
                </tr>
                <tr>
                    <td>

                        <rsweb:ReportViewer ID="RptTrPageDetails" runat="server"></rsweb:ReportViewer>

                     <%--   <rsweb:ReportViewer ID="RptTrPageDetails" runat="server" ShowParameterPrompts="False"
                            DocumentMapWidth="120%" LinkActiveHoverColor="ActiveBorder"
                            ShowDocumentMapButton="False" Width="100%" Height="600px" DocumentMapCollapsed="True" Font-Names="Verdana"
                            Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
                            <%-- <LocalReport ReportPath="Reports\RptShowTrDetails.rdlc">
                            </LocalReport>
                        </rsweb:ReportViewer>--%>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
