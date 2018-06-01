<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="RptDailyWorkReport.aspx.cs" Inherits="RptDailyWorkReport" Title="UnivAmravati" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-heading">Daily Work Reports</div>
    <div style="text-align: center">
        <asp:RadioButton ID="rbtnRegAllotment" Text="Register Allotment" runat="server" Checked="True"
            GroupName="chk" />&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbtnDailyEntry" Text="Daily Entry Details" runat="server" GroupName="chk" />
        &nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbtnErrorValidity" Text="Error Validity" runat="server" Checked="True"
            GroupName="chk" />
    </div>
    <br />
    <div style="text-align: center">
        <asp:LinkButton ID="btnShow" class="btn btn-primary text-center" runat="server" OnClick="btnShow_Click">
                                   &nbsp;Show</asp:LinkButton></div>
</asp:Content>
