<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="RptExamWisePunchedRec.aspx.cs" Inherits="RptExamWisePunchedRec" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        .jumbotron {
            margin-top: 10px;
            box-shadow: 0px 5px 20px -4px rgb(51, 116, 183);
            background-color: white;
            padding-bottom: 1em;
            padding-top:12px;
        }
    </style>
    <script type="text/javascript">
        function printGrid() {

            <%--      var prtGrid = document.getElementById('<%=GrdMis.ClientID %>');
            prtGrid.border = 0;
            var prtwin = window.open('', 'PrintGridViewData', 'left=100,top=100,width=1000,height=1000,tollbar=0,scrollbars=1,status=0,resizable=1');
            prtwin.document.write(prtGrid.outerHTML);
            prtwin.document.close();
            prtwin.focus();
            prtwin.print();
            prtwin.close();--%>



            var gridData = document.getElementById('<%= GrdMis.ClientID %>');
            var windowUrl = '';
            //set print document name for gridview
            var uniqueName = new Date();
            var windowName = 'Print_' + uniqueName.getTime();

            var prtWindow = window.open(windowUrl, windowName,
            'left=100,top=100,right=100,bottom=100,width=700,height=500');
            prtWindow.document.write('<html ><head style="text-align:center;"><title> For Check !</title> <style type ="text/css"> h1{} tr {} @page {size: 21cm 29.7cm;margin: 10mm 10mm 10mm 10mm;} div.titlepage {page: Summer-2013;} div.chapter, div.appendix {page-break-after: always;} @media print {th {display: table-header-group,inline;color:black;}} td{border:1px solid black}</style>   <table style="width:100%;background-color:#526a9c;color:white;border:solid 2px;"> <tr>  <td style="width:5%">Rec Id</td><td style="width:20%">Faculty Name</td><td style="width:50%">Exam Name</td><td style="width:5%">TR Type Desc</td><td style="width:5%">Register No.</td><td style="width:5%">Total Pages</td><td style="width:5%">Punched Pages</td><td style="width:5%">Diff.</td></tr></table></head>');
            prtWindow.document.write('<body style="background:none !important">');
            prtWindow.document.write(gridData.outerHTML);
            prtWindow.document.write('</body></html>');
            prtWindow.document.close();
            prtWindow.focus();
            prtWindow.print();
            prtWindow.close();

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" style="min-height: 500px" runat="Server" onload="javascript:AddTHEAD('<%= GrdMis.ClientID %>');">
    <div class="form-heading">Exam Wise Punched Records</div>
    <div>
        <table>
            <tr >
                <td rowspan="2"></td>
            </tr>
            <tr></tr>
        </table>
    </div>
    <div class="container">
        <div>
        </div>
        <div class="row">
            <div class="form-group">
                <div class="col-md-2">
                    Exam Year
                    <asp:DropDownList CssClass=" form-control" ID="ddlExamYear" runat="server" required=""></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    Exam Session
                    <asp:DropDownList CssClass="form-control " ID="ddlExamSession" runat="server" required=""></asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <br />
                    <asp:Button ID="btnShow" CssClass="btn btn-primary" Text="Show" OnClick="btnShow_Click" runat="server" />
                    <asp:Button ID="btnClear" CssClass="btn btn-primary" Text="Clear" ValidationGroup="s" OnClick="btnClear_Click" runat="server" />
                </div>
                <div class="col-md-4">
                    <br />
                    <div class="alert-danger form-control" visible="false" id="AlertWarning" runat="server">
                        <span class="glyphicon glyphicon-exclamation-sign"></span>
                        <strong>Please! </strong>Select Exam Year & Session.
                    </div>
                    <div class="alert-info form-control" visible="false" id="AlertRec" runat="server">
                        <span class="glyphicon glyphicon-warning-sign"></span>
                        No Record Found.
                    </div>

                </div>
               
            </div>
        </div>
    </div>
    
    <div class="" id="divGrid" runat="server">
        <div class="jumbotron">
            <div class="form-group">
                <div class="text-right">
                    <div style="">
                        <table class="pull-right">
                            <tr>
                                <td>
                                    <%--  <asp:Button ID="btnprint1" runat="server" Text="Print Grid" CssClass="btn btn-primary" OnClick="btnprint1_Click"  />--%>
                                    <asp:LinkButton ID="btnprint1" runat="server" CssClass="btn btn-primary" OnClick="btnprint1_Click"><span class="glyphicon glyphicon-print"></span>&nbsp;Print Grid</asp:LinkButton>
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkexcel" runat="server" CssClass="btn btn-primary" OnClick="lnkexcel_Click"><span class="glyphicon glyphicon-save"></span>&nbsp;Import To Grid</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="form-group" style="">
                <div class="">
                    <asp:Panel ID="pnl" runat="server" Style="overflow: auto; height: 300px;">
                        <asp:GridView align="center" ID="GrdMis"
                            runat="server" ShowHeader="true"
                            OnRowDataBound="GrdMis_RowDataBound"
                            Width="100%" CellPadding="4"
                            ForeColor="Black" GridLines="Vertical" BackColor="#f1efef"
                            Font-Size="11px"
                            ShowFooter="false" AutoGenerateColumns="false">
                            <RowStyle BorderStyle="Solid" BorderWidth="1px" />
                            <HeaderStyle BorderWidth="1px" BackColor="#c1c1c1" ForeColor="Black" Font-Bold="true" BorderColor="#808080" />
                            <Columns>
                                <asp:BoundField DataField="RecID" ItemStyle-Width="5%" HeaderText="Sr No." />
                                <asp:BoundField DataField="FacultyName" ItemStyle-Width="20%" HeaderText="Faculty Name" />
                                <asp:BoundField DataField="ExamName" ItemStyle-Width="50%" HeaderText="Exam Name" />
                                <asp:BoundField DataField="TRTypeDesc" ItemStyle-Width="5%" HeaderText="TR Type" />
                                <asp:BoundField DataField="RegisterNo" ItemStyle-Width="5%" HeaderText="Register No." />
                                <asp:BoundField DataField="TotalPages" ItemStyle-Width="5%" HeaderText="Total Pages" />
                                <asp:BoundField DataField="PunchedPages" ItemStyle-Width="5%" HeaderText="Punched Pages" />
                                <asp:BoundField DataField="Diff" ItemStyle-Width="5%" HeaderText="Diff." />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                    <div class="" style="margin-top: 5px">
                        <div class="">
                            <div class="form-group">
                                <div class="col-md-2">
                                    Register No.
                            <asp:Label ID="lblRegNo" Text="" runat="server" />
                                </div>
                                <div class="col-md-2">
                                    Total Pages
                            <asp:Label ID="lblTotalPage" Text="" runat="server" />
                                </div>
                                <div class="col-md-2">
                                    Punched Pages
                            <asp:Label ID="lblPunchedPage" Text="" runat="server" />
                                </div>
                                <div class="col-md-2">
                                    Diff
                            <asp:Label ID="lblDiff" Text="" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </div>

        </div>
    </div>

</asp:Content>

