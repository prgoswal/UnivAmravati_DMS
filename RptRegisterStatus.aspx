<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="RptRegisterStatus.aspx.cs" Inherits="RptRegisterStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .table2excel {
            max-height: 500px;
        }

        .jumbotron {
            margin-top: 10px;
            box-shadow: 0px 5px 20px -4px rgb(51, 116, 183);
            background-color: white;
            padding-bottom: 1em;
            padding-top: 12px;
        }

        .col-small {
            width: 10.666667%;
        }

        .drop {
            width: 194px;
            overflow: auto;
            background-color: white;
            padding: 5px;
            position: absolute;
            height: auto;
            /*max-height: 200px;*/
            border: 1px solid cornflowerblue;
            border-radius: 3px;
            margin-top: 2px;
            z-index: 999;
        }

        .drop > table {
            min-width: 165px;
        }

        .cb {
            height: 150px;
            display: block;
            overflow: auto;
        }

        .zoom {
            border: 1px solid #ddd;
            width: 540px;
            height: 448px;
        }

        #PrintDiv {
            max-height: 310px;
            overflow: auto;
        }

        .w100 {
            width: 98.7%;
        }
        thead{
            display:none;
        }
        .table2excel th{
            display:none;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form-heading">Alloted Lot Status Report</div>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-md-2 col-small">Select Exam Year</label>
            <div class="col-md-2 col-small">
                 <asp:DropDownList ID="ddlExamYear" OnSelectedIndexChanged="ddlExamYear_SelectedIndexChanged" CssClass="form-control input-sm" runat="server" AutoPostBack="true"></asp:DropDownList>
            </div>

            <label class="col-md-2 col-small">Select Session</label>
            <div class="col-md-2 col-small">                
               <asp:DropDownList ID="ddlSession" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged" CssClass="form-control input-sm" AutoPostBack="true" runat="server"></asp:DropDownList>
            </div>

            <div class="col-md-2 ">
                <asp:RadioButtonList ID="rbFilterStatus"  RepeatDirection="Horizontal" runat="server">
                    <asp:ListItem style="padding: 0 10px;" Selected="True" Text="Running/Pending" />
                    <asp:ListItem style="padding: 0 10px;" Text="All" />
                </asp:RadioButtonList>
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnShow" CssClass="btn btn-primary" Text="Show" OnClick="btnShow_Click" runat="server" />
                <asp:Button ID="btnClear" CssClass="btn btn-danger" Text="Clear" ValidationGroup="s" OnClick="btnClear_Click" runat="server" />
                <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
            </div>
        </div>

        <div class="form-group">
            
        </div>
        <div class="divGrid" id="divGrid" visible="false" runat="server">
            <div class="jumbotron">
                <div class="form-group">
                    <div class="pull-right">
                        <asp:LinkButton ID="btnprint" OnClick="btnprint_Click" runat="server" CssClass="btn btn-primary"><span class="glyphicon glyphicon-print"></span>&nbsp;Print Grid</asp:LinkButton>
                        <asp:LinkButton ID="lnkexcel" OnClientClick="GenerateExcel();" OnClick="lnkexcel_Click" runat="server" CssClass="btn btn-primary"><span class="glyphicon glyphicon-save"></span>&nbsp;Import To Grid</asp:LinkButton>
                        <%--<button class="btn btn-block" type="button" onclick="GenerateExcel();"><span class="glyphicon glyphicon-save"></span>&nbsp;Import To Grid</button>--%>
                    </div>
                    <div class="text-center text-uppercase" style="margin-bottom:20px;">
                        <h4><asp:Label ID="grdHeader" runat="server" /></h4>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-12">
                        <table class="text-center table-bordered w100">
                            <tr>
                                <td class="col-md-3">Faculty</td>
                                <td class="col-md-4">Exam Name</td>
                                <td class="col-md-1">Register No.</td>
                                <td class="col-md-1">Total Page</td>                                
                                <td class="col-md-1">Punch Page</td>
                                <td class="col-md-1">User Name</td>
                                <td class="col-md-1">Status</td>
                            </tr>
                        </table>

                        <div id="PrintDiv">
                           <asp:GridView CssClass="table2excel table table-responsive text-center" AutoGenerateColumns="false" OnDataBound="gvAllotmentStatus_DataBound" ID="gvAllotmentStatus" runat="server">                                
                                <Columns>
                                    <%--<asp:BoundField DataField="FacultyCD" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="Ctrl" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />--%>
                                    <asp:BoundField ItemStyle-CssClass="col-md-3 " DataField="Faculty" HeaderText="Faculty" />
                                    <asp:BoundField ItemStyle-CssClass="col-md-4" DataField="ExamName" HeaderText="Exam Name" />
                                    <asp:BoundField ItemStyle-CssClass="col-md-1" DataField="RegNo" HeaderText="Register No." />
                                    <asp:BoundField ItemStyle-CssClass="col-md-1" DataField="TotalPage" HeaderText="Total Page" />
                                    <asp:BoundField ItemStyle-CssClass="col-md-1" DataField="PunchPage" HeaderText="Punch Page" />
                                    <asp:BoundField ItemStyle-CssClass="col-md-1" DataField="UserName" HeaderText="User Name" />
                                    <asp:BoundField ItemStyle-CssClass="col-md-1" DataField="EntryStatus" HeaderText="Status" />
                                </Columns>
                                <FooterStyle BackColor="#CCCCCC" />
                                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="#CCCCCC" />
                            </asp:GridView>
                           <style type="text/css">
                                @media print
                                {
                                    th
                                    {
                                        color: black;
                                        background-color: white;
                                    }
                                    THEAD {
                                        display: table-header-group;
                                    }                                  
                                    .w100 {
                                        display:none;
                                    }
                                    @page{
                                        size:landscape;
                                    }
                                }
                            </style>
                           <style type="text/css">        
                                THEAD
                                {
                                    display: table-header-group;
                                    border: solid 1px black;
                                }
                            </style>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="js1/jquery.table2excel.js"></script>
    <script>
        function PrintDiv() {
            var table = document.getElementById('<%=gvAllotmentStatus.ClientID%>');
            var original = table.innerHTML;
            AddTHEAD(table);
            var divContents = document.getElementById('<%=divGrid.ClientID%>').innerHTML;
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<style>table{font-size: 12px;} .col-md-2{width:20%} .col-md-7{width:78%} .col-md-1{width:10%}  .col-md-6{width:60%} a{display:none;} .text-center{text-align: center;} .noExl{display:none;} table{width:100%;} table tr td{border:1px solid black;} table tr th{border:1px solid black;} </style>')
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
            table.innerHTML = original;
        }
        function AddTHEAD(table) {
            if (table != null) {
                var head = document.createElement("THEAD");
                head.style.display = "table-header-group";

                /// For Header Logo With University Name tr1
                var tr1 = document.createElement("tr");
                var td1 = document.createElement("th");
                var td2 = document.createElement("th");
                var img = document.createElement("img");
                var h = document.createElement("h1");
                img.src = "bootstrap-3.3.6-dist/images/logo.png";
                td1.appendChild(img);
                td1.style.border = "none";
                td1.style.paddingBottom = "10px";
                td1.rowSpan = 2;
                h.style.margin = "auto";
                h.innerText = "Sant Gadge Baba Amravati University";
                td2.colSpan = 6;
                td2.style.border = "none";
                td2.appendChild(h);
                tr1.appendChild(td1);
                tr1.appendChild(td2);
                head.style.border = "none";
                head.appendChild(tr1);
                //////////////////////////
                /// For Header Report Name tr2
                var tr2 = document.createElement("tr");
                var th = document.createElement("th");
                var span = document.createElement("span");
                th.colSpan = 7;
                th.style = "center";
                th.style.border = "none";
                span.innerText = "For Exam Year - " + '<%=ddlExamYear.SelectedItem.Text%>' + " Session - " + '<%=ddlSession.SelectedItem.Text%>' + "\n";
                span.innerText += "Register Wise " + '<%=rbFilterStatus.SelectedItem.Text%>' + " Status Report.";
                th.appendChild(span);
                tr2.appendChild(th);
                head.appendChild(tr2);
                //////////////////////////

                head.appendChild(table.rows[0]);
                table.style.border = "none";
                table.insertBefore(head, table.childNodes[0]);               
            }
        }
        function GenerateExcel() { //// Generate Excel File From Grid.
     
            var Year = '<%=ddlExamYear.SelectedItem.Text%>';
            var Session = '<%=ddlSession.SelectedItem.Text%>';
            var type = '<%=rbFilterStatus.SelectedItem.Text%>';
            var XlName = Session + "-" + Year + "-" + type;
            $(".table2excel").table2excel({
                exclude: ".noExl",
                name: "Excel Document Name",
                filename: XlName,
                fileext: ".xls",
                exclude_img: true,
                exclude_links: true,
                exclude_inputs: true
            });
        }
    </script>
</asp:Content>

