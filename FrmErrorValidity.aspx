<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="FrmErrorValidity.aspx.cs" Inherits="FrmErrorValidity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form-heading">Report Error Validity</div>
     <div class="container">
        <div class="row">
            <div class="form-group" >
                <div class="col-md-2">  
                       Select  Exam Year
                     <asp:DropDownList ID="ddlExamYear" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamYear_SelectedIndexChanged">
                            </asp:DropDownList>
                </div>
                   <div class="col-md-2">  
 Select Session
   <asp:DropDownList ID="ddlSession" CssClass="form-control input-sm" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" >
                            </asp:DropDownList>
                       </div>                  
                                
            <div class="col-md-2">  
                Select Register No.
               <asp:DropDownList ID="ddlregno"
                    CssClass="form-control input-sm" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                       </div>
                   <div class="col-md-2">
                       <br />

                        <asp:LinkButton ID="linkShow" class="btn btn-primary" runat="server" OnClientClick="return ValidateRegister();"
                                TabIndex="12" OnClick="linkShow_Click"><i class="fa fa-save" ></i>&nbsp;Show</asp:LinkButton>
                            <asp:LinkButton ID="linkCancel" class="btn btn-primary" runat="server" TabIndex="13"
                                OnClick="linkCancel_Click"><i class="fa fa-close"></i>
                        &nbsp;Clear</asp:LinkButton>
                   </div>
                <div class="col-md-4"><br />
                    <asp:Label ID="lblmsg" runat="server" CssClass="text-danger"></asp:Label>
                </div>
                </div>
             <asp:GridView ID="grddata" class="table table-bordered" runat="server" AutoGenerateColumns="false" Width="50%"  >
                            <Columns>
                                <asp:BoundField DataField="SrNo" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="Center" HeaderText="Sr. No." />
                                <asp:BoundField DataField="RegNo" ItemStyle-Width="20%" ItemStyle-HorizontalAlign="Center" HeaderText="Register No." />
                                  <asp:BoundField DataField="PageNo" ItemStyle-Width="20%"  ItemStyle-HorizontalAlign="Center" HeaderText="Page No." />
                       
                                </Columns>
            </asp:GridView>
            </div>
<%--         <//div>--%>




</asp:Content>

