<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="UserLockingUnlocking.aspx.cs" Inherits="UserLockingUnlocking" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <div class="table-responsive">
                    <table align="center">
                        <tr>
                            <asp:GridView ID="grdUserUnlocking" runat="server" class="table table-bordered" AutoGenerateColumns="False" Width="100%" OnRowUpdating="grdUserUnlocking_RowUpdating" DataKeyNames="UserId" OnRowDataBound="grdUserUnlocking_RowDataBound" OnSelectedIndexChanging="grdUserUnlocking_SelectedIndexChanging">
                                <Columns>
                                    <asp:BoundField ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" HeaderText="Register No." DataField="UserID" />
                                    <asp:BoundField HeaderText="User Name" DataField="UserName" />
                                    <asp:BoundField HeaderText="User Login ID" DataField="UserLoginID" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkUpdte" CommandName="Update"
                                                Text="Lock"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label runat="server" ID="lblMsg" ForeColor="Red"></asp:Label></td>
                        </tr>
                    </table>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

