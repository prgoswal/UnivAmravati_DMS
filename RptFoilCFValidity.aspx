<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="RptFoilCFValidity.aspx.cs" Inherits="RptFoilCFValidity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ValidateRegister() {

            if ($('#<%=ddlExamYear.ClientID%>').val() == 'Select Year') {
                $('#<%=ddlExamYear.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Year', 'Error', function () { $("#ddlExamYear").focus() })
                return false;
            }
            else {
                $('#<%=ddlExamYear.ClientID%>').css('border-color', '');
            }

            if ($('#<%=ddlSession.ClientID%>').val() == 0) {
                $('#<%=ddlSession.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Exam Session', 'Error', function () { $("#ddlSession").focus() })
                return false;
            }
            else {
                $('#<%=ddlSession.ClientID%>').css('border-color', '');
            }
        }</script>

    <asp:UpdatePanel runat="server" ID="updaepanel">
        <ContentTemplate>
            <div class="form-heading"><div class="text-center">Report Foil/CF Validity</div></div>
            <div class="container">
                <div class="row">
                    <div class="form-group">
                        <div class="col-md-2">
                            Exam Year
                     <asp:DropDownList ID="ddlExamYear" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamYear_SelectedIndexChanged">
                     </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Session
   <asp:DropDownList ID="ddlSession" CssClass="form-control input-sm" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged">
   </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <asp:CheckBox ID="chklist" runat="server" AutoPostBack="true" OnCheckedChanged="chklist_CheckedChanged" />
                            Show By Register No.
                        </div>
                        <div class="col-md-2">
                            Select Register No.
               <asp:DropDownList ID="ddlregno" Style="width: 75%" CssClass="form-control input-sm" runat="server">
               </asp:DropDownList>
                        </div>
                        <div class="col-md-4">
                            <br />
                            <asp:LinkButton ID="linkShowRpt" class="btn btn-primary" runat="server"
                                Text="" OnClientClick="return ValidateRegister();" OnClick="linkShowRpt_Click"> 
          <i class="fa fa-save"></i>&nbsp;Show </asp:LinkButton>
                            <asp:LinkButton ID="linkCncl" class="btn btn-primary" runat="server" Text="">
                     <i class="fa fa-save"></i>&nbsp;Clear</asp:LinkButton>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lblmsg" runat="server" CssClass="text-danger bg-danger"></asp:Label>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

