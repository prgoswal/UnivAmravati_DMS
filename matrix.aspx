<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MainMaster.master"
    CodeFile="matrix.aspx.cs" Inherits="_Default" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<%--<html xmlns="http://www.w3.org/1999/xhtml">--%>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .txtVertical
        {
            filter: progid:DXImageTransform.Microsoft.BasicImage(rotation=3); /* IE6,IE7 */
            ms-filter: "progid:DXImageTransform.Microsoft.BasicImage(rotation=3)"; /* IE8 */
            -moz-transform: rotate(-90deg); /* FF3.5+ */
            -o-transform: rotate(-90deg); /* Opera 10.5 */
            -webkit-transform: rotate(-90deg); /* Safari 3.1+, Chrome */
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div align="center">
        <%-- <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
        <h3 class="text-center">
            USER AUTHORITY FORM
        </h3>
    </div>
    <div>
        <table style="width: 100%">
            <tr>
                <td align="center" style="padding-bottom: 10px; color: Red; font-size: 14px">
                    WORKING MENUS
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%">
                        <tr>
                            <td style="min-height: 200px; color: Red; padding-right: 10px; font-size: 14px; padding-top: 14%;
                                width: 7%; vertical-align: text-bottom">
                                <asp:Label ID="lblUserProfile" CssClass="txtVertical" runat="server" Text="USER PROFILE"></asp:Label>
                            </td>
                            <td style="width: 93%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pl" runat="server" ScrollBars="Auto">
                                            <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound"
                                                OnRowCommand="GridView1_RowCommand" Height="274px" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="None" BorderWidth="1px" CellSpacing="5" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-HorizontalAlign="Center" HorizontalAlign="Center" CellPadding="4">
                                                <RowStyle HorizontalAlign="Center" ForeColor="#000066" />
                                                <FooterStyle BackColor="White" ForeColor="#000066" />
                                                <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                                                <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#006699" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <br /><br />
                                        <asp:Button ID="Btnselect" class="btn btn-primary" runat="server" Text="Save" OnClick="Btnselect_Click" />
                                        <asp:Label ID="lblvalue" runat="server" ForeColor="Red" Text=""></asp:Label>
                                        <br />
                                        
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div><br />
    <asp:Image ID="Image1" runat="server" BackColor="LightGreen" BorderStyle="Solid"
        BorderColor="Black" Height="18px" Width="26px" ImageUrl="~/images/sun.rays.small.png"
        BorderWidth="1px" />
    <asp:Label ID="Label3" runat="server" ForeColor="Red" Text=" - Green For Rights Allotted"></asp:Label>
    &nbsp;&nbsp;
    <asp:Image ID="Image2" runat="server" BackColor="#FFFF66" BorderStyle="Solid" BorderColor="Black"
        Height="18px" Width="26px" ImageUrl="~/images/sun.rays.small.png" BorderWidth="1px" />
    <asp:Label ID="Label4" runat="server" ForeColor="Red" Text=" - Yellow For Selected Rights Allot"></asp:Label>
</asp:Content>
