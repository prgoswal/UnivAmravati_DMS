<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RptShow.aspx.cs" Inherits="RptShow" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<%--<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap-3.3.6-dist/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div style="text-align:center">
            <table style="width: 81%;height: auto;margin: auto;margin-top: 3%;">
                <tr>
                    <td>
                        <asp:Button runat="server" OnClientClick="history.back()" ID="btnBack" style="text-align:center"  Text="Back To Home" OnClick="btnBack_Click" />
                    </td>
                </tr>
                <tr>
                    <td>
                     <%--  <rsweb:ReportViewer ID="ReportViewer1" ShowPrintButton="true" ShowParameterPrompts="false" runat="server" Width="100%"></rsweb:ReportViewer>--%>
                        <rsweb:ReportViewer ID="ReportViewer1" ShowPrintButton="true" ShowParameterPrompts="false" runat="server" Width="100%"></rsweb:ReportViewer>
                     <%--   <rsweb:ReportViewer ID="ReportViewer1"  style="text-align:center" runat="server" ShowParameterPrompts="false"
                            DocumentMapWidth="120%" LinkActiveHoverColor="ActiveBorder" ShowPromptAreaButton="true"
                            ShowDocumentMapButton="false" Width="990px" Height="500px" ProcessingMode="Remote"
                            ShowPrintButton="true" DocumentMapCollapsed="True">
                        </rsweb:ReportViewer>--%>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
