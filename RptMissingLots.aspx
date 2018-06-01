<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="RptMissingLots.aspx.cs" Inherits="RptMissingLots" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form-heading">Missing Lots Report</div>
      <div class="container">
      
        <div class="row">
            <div class="form-group">
                <div class="col-md-3">
                    Select Exam Year
                    <asp:DropDownList CssClass=" form-control" ID="ddlExamYear" runat="server" required=""></asp:DropDownList>
                </div>
                <div class="col-md-3"><br />
                    <asp:Button Text="Show" ID="btnshow" OnClick="btnshow_Click" runat="server" CssClass="btn btn-primary" />
                </div>
                <div class="col-md-6">

                </div>
                </div>
            </div>
          </div>



</asp:Content>

