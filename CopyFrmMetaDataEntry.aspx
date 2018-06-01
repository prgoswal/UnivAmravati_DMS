<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeFile="CopyFrmMetaDataEntry.aspx.cs" Inherits="CopyFrmMetaDataEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <div class="form-group">
            <div class="col-md-2">
                <span>Enter Register No.</span>
                <div class="form-group"></div>
            </div>
            <div class="col-md-2">
                <asp:TextBox ID="txtRegNo" CssClass="form-control" placeholder="Enter Register No." runat="server"></asp:TextBox>
                <div class="form-group"></div>
            </div>
            <div class="col-md-2">
                <asp:LinkButton ID="btnSearchImg" CssClass="btn btn-sm btn-primary" runat="server" OnClick="btnSearchImg_Click"><i class="fa fa-search"> </i>&nbsp;Search Image</asp:LinkButton>
                <div class="form-group"></div>
            </div>
            <div class="col-md-5">
                <asp:Label class="text-danger" runat="server" ID="lblError"></asp:Label>
                <asp:Label class="text-danger" runat="server" ID="lblCount" ></asp:Label>
            </div>
        </div>

        <div class="form-group">
            <div class="col-md-4">
                <asp:Image ID="imgFoil" runat="server" />
            </div>
            <div class="col-md-4">

            </div>
            <div class="col-md-4">

            </div>
        </div>
    </div>
</asp:Content>

