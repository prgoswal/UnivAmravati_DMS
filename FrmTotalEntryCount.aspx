<%@ Page Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="FrmTotalEntryCount.aspx.cs" Inherits="FrmTotalEntryCount" Title="Amravati University" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <div class="form-group" style="display: block;">
        <div style="width: 90%; background-color: #50618C; margin-left: 50px; border: 1px solid #084B8A; color: #ffffff;
            font-weight: bold;">
            <table rules="all" width="100%">
                <tr>
                    <td style="width: 51px;">
                        Sr No.
                    </td>
                    <td style="width: 302px; color: White">
                        User Name
                    </td>
                    <td style="width: 172px; color: White">
                        Regester No.
                    </td>
                    <td style="width: 154px; color: White">
                        Entry Date
                    </td>
                    <td style="width: 98px; color: White">
                        No. of Records
                    </td>
                </tr>
            </table>
        </div>
        <div align="center" style="overflow: auto; width: 90%; margin-left: 50px; height: 325px;">
            <asp:GridView align="center" ID="grdEntry" runat="server" ShowHeader="false" HeaderStyle-BackColor="#61A6F8"
                HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" Width="100%" CellPadding="4"
                RowStyle-BorderStyle="Solid" ForeColor="Black" GridLines="Vertical" BackColor="White"
                BorderColor="Gray" BorderStyle="Solid" BorderWidth="2px" ShowFooter="True" AutoGenerateColumns="False">
               <Columns>
                       <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Height="1px"
                    ItemStyle-Width="58px">
                    <ItemTemplate>
                        <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="350px" DataField="UserName" HeaderText="User Name" />
                        <asp:BoundField ItemStyle-Width="200px" DataField="RegNo" HeaderText="RegisterNo." />
                        <asp:BoundField ItemStyle-Width="177px" DataField="EntryDate" HeaderText="Entry Date" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="cnt" HeaderText="No. of Records" />
                    </Columns>
            </asp:GridView>
            <asp:Label runat="server" ID="lblmsg" style="color:Red;"></asp:Label>
        </div>
        <div style="text-align:right">
        <asp:Label runat="server" ID="lblCount" style="color:Red; margin-right: 186px; font-size:18px; font-weight:500"></asp:Label>
        </div>
    </div>
</asp:Content>
