<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="UserLevelMaster.aspx.cs" Inherits="UserLevelMaster" Title="Univ Amravati" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        function ValidateUserDetails() {
            if ($('#<%=txtUserLavel.ClientID%>').val() == '') {
                $('#<%=txtUserLavel.ClientID%>').css('border-color', 'Red');
                jAlert('Please Enter User Level', 'Error', function() { $("#txtUserLavel").focus() })
                return false;
            }
            else {
                $('#<%=txtUserLavel.ClientID%>').css('border-color', '');
            }
            if ($('#<%=ddlIsActive.ClientID%>').val() == 0) {
                $('#<%=ddlIsActive.ClientID%>').css('border-color', 'Red');
                jAlert('Please Select Level Type', 'Error', function() { $("#ddlIsActive").focus() })
                return false;
            }
            else {
                $('#<%=ddlIsActive.ClientID%>').css('border-color', '');
            }

        }
    </script>

    <asp:UpdatePanel runat="server" ID="updpnl1">
        <ContentTemplate>
            <div class="row">
                <div class="col-md-1 col-lg-2">
                </div>
                <div class="col-md-10 col-lg-8">
                    <div class="user-creation">
                        <h3 class="text-center">
                            User Level Active/DeActive</h3>
                        <hr class="changecolor" />
                        <div class="row">
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4">
                                        Level &nbsp;<span style="color: Red">*</span></label>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:TextBox ID="txtUserLavel" style="text-transform:uppercase" CssClass="form-control input-sm" runat="server" OnClientClick="return ValidateUserDetails()"
                                            MaxLength="20" TabIndex="1"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtUserLavel"
                                            ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ ">
                                        </cc1:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 col-lg-6">
                                <div class="form-group">
                                    <label class="col-md-4 col-lg-4">
                                        Activ/DeActive &nbsp;<span style="color: Red">*</span></label>
                                    <div class="col-md-6 col-lg-6">
                                        <asp:DropDownList ID="ddlIsActive" CssClass="form-control input-sm" runat="server"
                                            TabIndex="2">
                                            <asp:ListItem Value="0" Text="--SELECT--"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="TRUE"></asp:ListItem>
                                            <asp:ListItem Value="2" Text="FALSE"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="text-center">
                                <asp:LinkButton ID="linkSave" class="btn btn-primary" runat="server" OnClientClick="return ValidateUserDetails()"
                                    OnClick="linkSave_Click" TabIndex="3"><i class="fa fa-save"></i>&nbsp;Save</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="LnkbtnUpdate" class="btn btn-primary" runat="server" TabIndex="4" OnClientClick="return ValidateUserDetails()"
                                    OnClick="LnkbtnUpdate_Click"><i class="fa fa-close"></i> &nbsp;Update</asp:LinkButton>&nbsp;&nbsp;
                                <asp:LinkButton ID="linkClear" class="btn btn-primary" runat="server" TabIndex="5"
                                    OnClick="linkClear_Click"><i class="fa fa-close"></i> &nbsp;Clear</asp:LinkButton>
                                <%--  <asp:TextBox ID="txtIsActive" runat="server" Width="20px"></asp:TextBox>--%>
                                <br />
                                <asp:Label runat="server" ID="lblmsg" style="font-size:25px; font-family:Times New Roman;font-weight:500" ForeColor="Red" Text=""></asp:Label>
                            </div>
                            <br />
                            <div>
                                <asp:GridView align="center" ID="grdDetails" runat="server" HeaderStyle-BackColor="#3073AC"
                                    HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="50%" CellPadding="4"
                                    RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Horizontal" BackColor="White"
                                    BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" ShowFooter="True"
                                    AutoGenerateColumns="False" OnRowCommand="grdDetails_RowCommand" OnRowUpdating="grdDetails_RowUpdating">
                                    <RowStyle BorderStyle="Solid"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Height="1px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Height="1px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ItemId" HeaderStyle-Height="1px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblItemId" Text='<%# Eval("ItemId") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Height="1px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="LevelDescription">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkLevel" runat="server" Text='<%#Eval("LevelDescription") %>'
                                                    CommandName="More">
                            <itemstyle width="235px" />
                                                </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Activ/Deactive" HeaderStyle-Height="1px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActive" Text='<%#Eval("IsActive") %>' runat="server" />
                                            </ItemTemplate>
                                            <HeaderStyle Height="1px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <%-- <asp:BoundField HeaderText="Activ/Deactive" ItemStyle-Width="89px" DataField="IsActive">
                                  <ItemStyle Width="89px"></ItemStyle>
                                </asp:BoundField>--%>
                                    </Columns>
                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                </asp:GridView>
                            </div>
                        </div>
                        <%--<hr />--%>
                        <%--<p class="text-center" style="font-weight: 600;">
                    <span style="color: Red; font-size: 15px; text-align: center;">*&nbsp;</span>Fields
                    Are Mandetory.</p>--%>
                        <hr class="changecolor" />
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
