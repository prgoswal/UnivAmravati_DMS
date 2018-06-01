<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="TrRegisterReport.aspx.cs" Inherits="TrRegisterReport" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="box">
        <div class="row">
            <div class="col-md-2 col-lg-2">
            </div>
            <div class="col-md-6 col-lg-6">
                <div class="form-group">
                    <label class="col-md-4 col-lg-4">
                        Select TR Type</label>
                    <div class="col-md-5 col-lg-6">
                        <asp:DropDownList ID="ddlTrType" class="form-control input-sm" runat="server">
                            <asp:ListItem Value="0" Text="All Register"></asp:ListItem>
                            <asp:ListItem Value="1" Text="Active Register"></asp:ListItem>
                            <asp:ListItem Value="2" Text="InActive Register"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-2 col-lg-2">
                        <asp:LinkButton ID="linkShowRpt" class="btn btn-sm btn-primary" runat="server" OnClick="linkShowRpt_Click"><i class="fa fa-search"></i>&nbsp;Show Report</asp:LinkButton>
                    </div>
                </div>
            </div>
            <div class="col-md-4 col-lg-4">
                <asp:Label ID="lblmsg" runat="server" Style="color: Red" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>
