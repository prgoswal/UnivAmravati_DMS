<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="RptLotAllotment.aspx.cs" Inherits="RptLotAllotment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/style.css" rel="stylesheet" type="text/css" />
    <link href="css/jquery-ui.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script src="bootstrap-3.3.6-dist/js/jquery-1.10.2.js" type="text/javascript"></script>

    <script src="bootstrap-3.3.6-dist/js/jquery-ui.js" type="text/javascript"></script>

    <script type="text/javascript">
//        $(function() {
//            $("#txtDateFrom").datepicker();
//        });
        $(function() {
        $('#<%=txtDateFrom.ClientID %>').change(function() {
        $('#<%=txtDateTo.ClientID %>').val($('#<%=txtDateFrom.ClientID %>').val());
            });
        });

//        function OneTextToOther() {

//            var first = document.getElementById('<%= txtDateFrom.ClientID %>').value;
//            document.getElementById('<%= txtDateTo.ClientID %>').value = first;
//        }
        function ValidateRegister() {

            if ($('#<%=txtDateFrom.ClientID%>').val() == 0) {
                $('#<%=txtDateFrom.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Date From', 'Error', function() { $("#txtDateFrom").focus() })
                return false;
            }
            else {
                $('#<%=txtDateFrom.ClientID%>').css('border-color', '');
            }

            if ($('#<%=txtDateTo.ClientID%>').val() == 0)
            {
                $('#<%=txtDateTo.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter Date To', 'Error', function() { $("#txtDateTo").focus() })
                return false;
            }
            else
            {
                $('#<%=txtDateTo.ClientID%>').css('border-color', '');
            }
        }
    </script>

  <%--   <asp:UpdatePanel runat="server" ID="Upnl">
        <ContentTemplate>--%>
    <div class="form-heading">Entry And Validity Reports</div>
    <div style="text-align: center">
        <asp:RadioButton ID="rbtnAll" Text="All" runat="server" Checked="True" GroupName="chk"
            AutoPostBack="True" OnCheckedChanged="rbtnAll_CheckedChanged" />&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbtnDateWise" Text="Date Wise" runat="server" GroupName="chk"
            AutoPostBack="True" OnCheckedChanged="rbtnDateWise_CheckedChanged" />&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbtnUserWise" Text="User Wise" runat="server" GroupName="chk"
            AutoPostBack="True" OnCheckedChanged="rbtnUserWise_CheckedChanged" />&nbsp;&nbsp;&nbsp;
        <asp:RadioButton ID="rbtnUserWithDate" Text="User Date Wise" runat="server" GroupName="chk"
            AutoPostBack="True" OnCheckedChanged="rbtnUserWithDate_CheckedChanged" />
    </div>
    <br />
    <asp:Panel runat="server" ID="pnlDateWise" Visible="true">
        <div style="text-align: center">
            <asp:Label runat="server" ID="lblDateFrom" Text="Date From"></asp:Label>
            <asp:TextBox runat="server" ID="txtDateFrom" 
                TabIndex="1"></asp:TextBox>
            <asp:Image ID="Image1" runat="server" onkeyup="OneTextToOther();" ImageUrl="~/images/calendar.png" Width="25px"
                Height="20px" />
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDateFrom"
                Format="dd/MM/yyyy" PopupButtonID="Image1">
            </cc1:CalendarExtender>
            <asp:Label runat="server" ID="lblDateTo" Text="Date To"></asp:Label>
            <asp:TextBox runat="server" ID="txtDateTo" TabIndex="2"></asp:TextBox>
            <asp:Image ID="Image2" runat="server" ImageUrl="~/images/calendar.png" Width="25px"
                Height="20px" />
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDateTo"
                Format="dd/MM/yyyy" PopupButtonID="Image2">
            </cc1:CalendarExtender>
            <asp:Label runat="server" ID="lblUser" Text="User"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlUser" 
                OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" TabIndex="3">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="pnluserWise" Visible="true">
        <div style="text-align: center">
            <asp:Label runat="server" ID="lblUserWise" Text="User"></asp:Label>
            <asp:DropDownList runat="server" ID="ddlUserWise">
            </asp:DropDownList>
        </div>
    </asp:Panel>
    <br />
   <%-- <asp:UpdatePanel runat="server" ID="updpnl">
        <ContentTemplate>--%>
            <div style="text-align: center">
                <asp:LinkButton ID="btnShow" class="btn btn-primary text-center" runat="server" OnClick="btnShow_Click"
                    OnClientClick="return ValidateRegister();" TabIndex="4">
                                    &nbsp;Show</asp:LinkButton>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="btnPrint" class="btn btn-primary text-center" runat="server"
                    OnClick="btnPrint_Click" OnClientClick="return ValidateRegister();" 
                    TabIndex="5">
                                    &nbsp;Print</asp:LinkButton>
            </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <div style="text-align: center">
        <asp:Label runat="server" ID="lblmsg" Style="color: Red; font-size: 18px; font-weight: 500"></asp:Label>
    </div>
    <br />
    <asp:Panel runat="server" ID="pnlTableHdng">
        <div style="width: 81%; margin-left: 136px; text-align: center; background-color: #50618C;
            border: 1px solid #084B8A; color: #ffffff; font-weight: bold;">
            <table rules="all" width="100%">
                <tr>
                   
                    <td style="width: 37px;">
                        Sr No.
                    </td>
                    <td style="width: 204px; color: White">
                        User Name
                    </td>
                    <td style="width: 100px; color: White">
                        Register No.
                    </td>
                    <td style="width: 148px; color: White">
                        Entry Date
                    </td>
                    <td style="width: 62px; color: White">
                        No. of Records
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div style="overflow: auto; text-align: center; margin-left: 136px; width: 81%; height: 300px;">
        <asp:GridView align="center" ID="grdDetails" runat="server" ShowHeader="false" HeaderStyle-BackColor="#61A6F8"
            HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="100%" CellPadding="4"
            RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="White"
            BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="True" AutoGenerateColumns="true">
            <Columns>
                <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Height="1px">
                    <ItemTemplate>
                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div style="text-align: right">
        <asp:Label runat="server" ID="lblCount" Style="color: Red; margin-right: 186px; font-size: 18px;
            font-weight: 500"></asp:Label>
    </div>
   <%--  </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
