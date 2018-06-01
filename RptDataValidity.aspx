<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="RptDataValidity.aspx.cs" Inherits="RptDataValidity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/Custom.css" rel="stylesheet" />
    <style>
        #loader {
            background: rgba(47, 40, 40, 0.8);
            display: none;
            position: fixed;
            top: 0;
            bottom: 0;
            right: 0;
            left: 0;
        }
        #PrintDiv {
            max-height: 310px;
            overflow-y: scroll;
        }
        
    </style>
    <%--<script>
        
        var ddlText,ddlValue,ddl;
        function ItemArray() {debugger
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=cbRegNo.ClientID%>");
            for (var i = 0; i < ddl.rows.length; i++) {                
                ddlText[ddlText.length] = ddl.rows[i].cells[0].lastChild.innerHTML;
                ddlValue[ddlValue.length] = ddl.rows[i].cells[0].lastChild.innerHTML;
            }
        }
        window.onload = ItemArray;
        
        function Search(event) {
            for (var i = 0; i < ddlText.length; i++) {
                if (eval == ddlText[i]) {
                    AddItem(ddlText);
                }
            }
        }
        function AddItem(val) {
            var ele = document.createElement("input");
            ddl.rows[i]
        }
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server" onload="javascript:AddTHEAD('<%= gvFoilCF.ClientID %>');">
    <div class="form-horizontal">
        <div class="form-heading">Data Validity Report</div>
        <div class="form-group">
            <div class="col-md-2 col-small">
                Select  Exam Year
                 <asp:DropDownList ID="ddlExamYear" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlExamYear_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-2 col-small">
                Select Session
               <asp:DropDownList ID="ddlSession" CssClass="form-control input-sm" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlSession_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                Select Register No.
                <%--<input type="text" id="btnRegToggle" onkeyup="Search(this.value)" onclick="Opendrop();" class="form-control input-sm"   />--%>
                <button id="btnRegToggle" onclick="Opendrop();" class="form-control input-sm" type="button">Select Register No<i class="caret pull-right" style="margin-top: 8px"></i></button>
                <div id="divdrop" class="collapse drop">
                    <div>
                        <asp:CheckBox Checked="false" ID="cbSelectAll" AutoPostBack="true" Text="Select All" OnCheckedChanged="cbSelectAll_CheckedChanged" runat="server" />
                        <asp:CheckBoxList onclick="Closedrop(this);" CssClass="cb" ID="cbRegNo" runat="server"></asp:CheckBoxList>
                        <div class="form-control lblReg hidden">
                            <asp:Label ID="lblRegCount" runat="server" />
                        </div>
                    </div>
                </div>
                <asp:Label CssClass="lblRegOut" ID="lblRegOutSide" runat="server" />
            </div>
            <div class="col-md-3">
                Select Validity Report
                <asp:DropDownList CssClass="form-control input-sm" ID="ddlValidityReport" AutoPostBack="true" OnSelectedIndexChanged="ddlValidityReport_SelectedIndexChanged" runat="server">
                    <asp:ListItem Value="0" Text="-- Select Report --" Selected="True" />
                    <asp:ListItem Text="Foil & CF Roll No. Mismatch" />
                    <asp:ListItem Text="Duplicate Page No. In Foil (Within Reg. No.)" />
                    <asp:ListItem Text="Duplicate Roll No. In Foil (Within Reg. No.)" />
                    <asp:ListItem Text="Missing Page No. In Foil (Within Reg. No.)" />
                    <asp:ListItem Text="Wrong Roll No. Entry" />
                </asp:DropDownList>
            </div>
            <div class="col-md-4">
                <br />
                <asp:Button ID="btnShow" CssClass="btn btn-primary" Text="Show" OnClick="btnShow_Click" OnClientClick="ShowLoad();" runat="server" />
                <asp:Button ID="btnClear" CssClass="btn btn-danger" Text="Clear" ValidationGroup="s" OnClick="btnClear_Click" runat="server" />
                <asp:Label ID="lblMsg" CssClass="text-danger" runat="server" />
            </div>
        </div>

        <div class="divGrid" id="divGrid" visible="false" runat="server">
            <div class="jumbotron">
                <div class="form-group">
                    <div class="pull-right">
                        <asp:LinkButton ID="btnprint" OnClick="btnprint_Click" runat="server" CssClass="btn btn-primary"><span class="glyphicon glyphicon-print"></span>&nbsp;Print Grid</asp:LinkButton>
                        <asp:LinkButton ID="lnkexcel" OnClientClick="GenerateExcel();" OnClick="lnkexcel_Click" runat="server" CssClass="btn btn-primary"><span class="glyphicon glyphicon-save"></span>&nbsp;Export To Excel</asp:LinkButton>
                    </div>
                    <div class="text-center text-uppercase" style="margin-bottom:20px;">
                        <h4><asp:Label CssClass="hideOnPrint" ID="grdHeader" runat="server" /></h4>
                    </div>
                </div>

                <div class="form-group">
                    <div class="col-md-12">
                            <table class="text-center table-bordered w100" id="tblGvFoilCF" runat="server" visible="false" >
                                <tr>
                                    <td class="col-md-2 p0">Register No.</td>
                                    <td class="col-md-1 p0">Page No.</td>
                                    <td class="col-md-7 p0">File Path</td>
                                    <td class="col-md-2 p0 noExl">Action</td>
                                </tr>
                            </table> <%-- Header For Duplicate PageNo --%>
                            
                            <table class="text-center table-bordered w100" id="tblGvFoilCFMismatch" runat="server" visible="false" >
                                <tr>
                                    <td class="col-md-4 p0">Sr. No.</td>
                                    <td class="col-md-4 p0">Register No.</td>
                                    <td class="col-md-4 p0">Page No.</td>
                                </tr>
                            </table> <%-- Header For FoilCF --%>
                            <table class="text-center table-bordered w100" id="tblGvMissingPage" runat="server" visible="false" >
                                <tr>
                                    <td class="col-md-4 p0">Sr. No.</td>
                                    <td class="col-md-4 p0">Register No.</td>
                                    <td class="col-md-4 p0">Missing Page No.</td>
                                </tr>
                            </table> <%-- Header For Missing Page --%>
                            <table class="text-center table-bordered w100" id="tblGvWrongRollEntry" runat="server" visible="false" >
                                <tr>
                                    <td class="col-md-2 p0">Register No.</td>
                                    <td class="col-md-1 p0">Roll No.</td>
                                    <td class="col-md-7 p0">File Path</td>
                                    <td class="col-md-2 p0 noExl">Action</td>
                                </tr>
                            </table> <%-- Wrong Roll No Entry --%>

                        <div id="PrintDiv">
                            
                            <asp:GridView class="table2excel table table-responsive text-center table-hover" 
                                HeaderStyle-CssClass="text-center" data-tableName="Test Table 1" runat="server"
                                ID="gvFoilCF" OnRowCommand="gvFoilCF_RowCommand" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Register" ItemStyle-CssClass="col-md-2" DataField="RegisterNo" />
                                    <asp:BoundField HeaderText="Page No" ItemStyle-CssClass="col-md-1" DataField="PageNo" />
                                    <asp:BoundField HeaderText="File Path" ItemStyle-CssClass="col-md-7" DataField="FilePath" />
                                    <asp:TemplateField ItemStyle-CssClass="col-md-2 noExl" HeaderStyle-CssClass="noExl">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="ShowData" ID="btnEdit" runat="server">Edit &nbsp<i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:GridView class="table2excel table table-responsive text-center table-condensed table-hover" ID="gvFoilCFMismatch" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Sr. No." ItemStyle-CssClass="col-md-4" DataField="Sr. No." />
                                    <asp:BoundField HeaderText="Register No." ItemStyle-CssClass="col-md-4" DataField="Register No." />
                                    <asp:BoundField HeaderText="Page No." ItemStyle-CssClass="col-md-4" DataField="Page No." />
                                </Columns>
                            </asp:GridView>

                            <asp:GridView class="table2excel table table-responsive text-center table-condensed table-hover" ID="gvMissingPage" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Sr. No." ItemStyle-CssClass="col-md-4" DataField="Sr. No." />
                                    <asp:BoundField HeaderText="Register No." ItemStyle-CssClass="col-md-4" DataField="Register No." />
                                    <asp:BoundField HeaderText="Missing Page No." ItemStyle-CssClass="col-md-4" DataField="Missing Page No." />
                                </Columns>
                            </asp:GridView>

                            <asp:GridView class="table2excel table table-responsive text-center table-condensed table-hover" OnRowCommand="gvFoilCF_RowCommand" ID="gvWrongRollNo" runat="server" AutoGenerateColumns="False">
                                <Columns>
                                    <asp:BoundField HeaderText="Register No." ItemStyle-CssClass="col-md-2" DataField="RegisterNo" />
                                    <asp:BoundField HeaderText="Roll No." ItemStyle-CssClass="col-md-1" DataField="RollNo" />
                                    <asp:BoundField HeaderText="File Path" ItemStyle-CssClass="col-md-7" DataField="FilePath" />
                                    <asp:TemplateField ItemStyle-CssClass="col-md-2 noExl"  HeaderStyle-CssClass="noExl">
                                        <ItemTemplate>
                                            <asp:LinkButton CommandName="ShowData" ID="btnEdit" runat="server">Edit &nbsp<i class="glyphicon glyphicon-pencil"></i></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                           <style type="text/css">
                                @media print
                                {
                                    th
                                    {
                                        color: black;
                                        background-color: white;
                                    }
                                    THEAD
                                    {
                                        display: table-header-group;
                                        border: solid 2px black;
                                    }
                                    .w100{
                                        display:none;
                                    }
                                    .hideOnPrint{
                                        display:none;
                                    }
                                }
                            </style>
                        </div>
                        <div class="badge">
                            <label>Total Record - </label>
                            <asp:Label ID="lblTotalRecord" runat="server" />
                        </div>
                    </div>
                </div>                
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalOnEdit" data-backdrop="static">
        <div class="container" style="margin-top: 40px;">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="text-center">
                            <h4 class="m0">Update Wrong Entry</h4>
                        </div>
                        <hr />
                        <div class="col-md-8" style="border-right: 1px solid #ddd;">
                            Image Path - 
                            <asp:Label ID="lblImgPath" runat="server" />
                            <div>
                                <asp:Image ID="imgFoil" class="zoom"  runat="server" style="border:1px solid" /><%--Width="730px" Height="460px"--%>
                                <script src="js/wheelzoom.js"></script>
                                <script>
                                    wheelzoom(document.querySelector('img.zoom'), { zoom: 1.05 });
                                </script>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div style="min-height: 140px;">                               
                                <div class="form-group">
                                    <label class="col-md-6">Register No.</label>
                                    <div class="col-md-6">
                                        <asp:TextBox onKeyUp="numericFilter(this)" CssClass="form-control" ReadOnly="true" placeholder="Enter Register No." ID="txtRegNo" runat="server" />
                                    </div>
                                </div>
                                <div id="divDuplicatePage" runat="server" visible="false">
                                    <div class="form-group">
                                        <label class="col-md-6">Current Page No.</label>
                                        <div class="col-md-6">
                                            <asp:TextBox onKeyUp="numericFilter(this)" CssClass="form-control" ReadOnly="true" placeholder="Enter Foil Page No." ID="txtFoilPageFrom" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-6">Update Page No.</label>
                                        <div class="col-md-6">
                                            <asp:TextBox onKeyUp="numericFilter(this)" CssClass="form-control" placeholder="Enter Foil Page No." ID="txtFoilPage" runat="server" />
                                        </div>
                                    </div>
                                </div>
                                <div id="divDuplicateRoll" runat="server" visible="false">
                                    <div class="form-group">
                                        <label class="col-md-6">Current Roll No.</label>
                                        <div class="col-md-6">
                                            <asp:TextBox onKeyUp="numericFilter(this)" CssClass="form-control" ReadOnly="true" placeholder="Enter Foil Page No." ID="txtRollFrom" runat="server" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-6">Update Roll No.</label>
                                        <div class="col-md-6">
                                            <asp:TextBox onKeyUp="numericFilter(this)" CssClass="form-control" placeholder="Enter Foil Page No." ID="txtRollTo" runat="server" />
                                        </div>
                                    </div>
                                 </div>
                            </div>
                            <div>
                                <asp:LinkButton CssClass="btn btn-primary" ID="btnUpdatePage" OnClick="btnUpdatePage_Click" runat="server"><i class="fa fa-download"></i> Update</asp:LinkButton>
                                <button type="button" data-dismiss="modal" class="btn btn-danger" ><i class="fa fa-backward"></i> Exit</button>
                            </div>
                            <div id="divRollGv" runat="server" visible="false">
                            <hr />
                            <label>Current Entered Roll No In Foil.</label>
                            <div class="mh-250">
                                <asp:GridView CssClass="table table-hover table-responsive" ID="gvRollOfPage" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sr. No.">
                                            <ItemTemplate><%#Container.DataItemIndex + 1 %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RollNo" HeaderText="Roll No" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="auto-set Fixed-Center" id="loader">
        <div class="auto-set">
            <div id="fountainG">
                <div id="fountainG_1" class="fountainG"></div>
                <div id="fountainG_2" class="fountainG"></div>
                <div id="fountainG_3" class="fountainG"></div>
                <div id="fountainG_4" class="fountainG"></div>
                <div id="fountainG_5" class="fountainG"></div>
                <div id="fountainG_6" class="fountainG"></div>
                <div id="fountainG_7" class="fountainG"></div>
                <div id="fountainG_8" class="fountainG"></div>
            </div>
        </div>
    </div>
   
    <script src="js1/jquery.table2excelWithHead.js"></script>
    <script src="js1/bootstrap.min.js"></script>
    <script type="text/javascript">
        function numericFilter(txb) {
            txb.value = txb.value.replace(/[^\0-9]/ig, "");
        }

        function AddTHEAD() {
            var index = '<%=ddlValidityReport.SelectedIndex%>';
            var Origionaltable;
            if (index == 1) {
                Origionaltable = document.getElementById('<%=gvFoilCFMismatch.ClientID%>');
            }
            else if (index == 2) {
                Origionaltable = document.getElementById('<%=gvFoilCF.ClientID%>');
            }
            else if (index == 3 || index == 5) {
                Origionaltable = document.getElementById('<%=gvWrongRollNo.ClientID%>');
            }
            else if (index == 4) {
                Origionaltable = document.getElementById('<%=gvMissingPage.ClientID%>');
            }
            
            table = Origionaltable;
            if (table != null) {
                var head = document.createElement("THEAD");
                
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
                span.innerText += '<%=ddlValidityReport.SelectedItem.Text%>' ;
                th.appendChild(span);
                tr2.appendChild(th);
                head.appendChild(tr2);
                //////////////////////////

                head.appendChild(table.rows[0]);
                table.insertBefore(head, table.childNodes[0]);
               
            }
           // return table;
        }
        var a = 1;
        function PrintDiv() {
            AddTHEAD();
            var divContents = document.getElementById('<%=divGrid.ClientID%>').innerHTML;
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title></title>');
            printWindow.document.write('</head><body>');
            printWindow.document.write('<style>.col-md-2{width:20%} .col-md-7{width:78%} .col-md-1{width:10%}  .col-md-6{width:60%} a{display:none;} .text-center{text-align: center;} .noExl{display:none;} table{width:100%;border: none;} table tr td{border:1px solid black;} table tr th{border:1px solid black;} </style>')
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
        
        function CreateHead(table) {
            var head = document.createElement("THEAD");
            var tr1 = document.createElement("tr");
            var th1 = document.createElement("th");
            var h1 = document.createElement("h1");
            var h4 = document.createElement("h4");
            h1.innerText = "Sant Gadge Baba Amravati University";
            h4.innerText = '<%=ddlValidityReport.SelectedItem.Text%>';
            th1.appendChild(h1);
            th1.appendChild(h4);
            tr1.appendChild(th1);
            head.appendChild(tr1);
            table[0].insertBefore(head, table[0].childNodes[0]);
            
        }

        var EYear = '<%=ddlExamYear.SelectedItem.Text%>'
        var ESession = '<%=ddlSession.SelectedItem.Text%>';
        var ValidityRpt = '<%=ddlValidityReport.SelectedIndex %>';
        function GenerateExcel() { //// Generate Excel File From Grid.
            var FileName = "";
            var headerText = 
            CreateHead($(".table2excel"));
            if (ValidityRpt == 1) {
                FileName = "Foil & CF Mismatch";
            }
            if (ValidityRpt == 2) {
                FileName = "Duplicate Page No";
            }
            if (ValidityRpt == 3) {
                FileName = "Duplicate Roll No";
            }
            if (ValidityRpt == 4) {
                FileName = "Missing Page No"
            }
            FileName += " Of " + EYear + "-" + ESession; //+ "-" + ValidityRpt.replace(".","");

            $(".table2excel").table2excel({
                exclude: ".noExl",
                name: "Excel Document Name",
                filename: FileName,
                fileext: ".xls",
                exclude_img: true,
                exclude_links: true,
                exclude_inputs: true
            });
        }

        function Closedrop(e) { //
            if (e != 1) {
                document.getElementById('divdrop').classList.add('in');
            }
            /////
            var chkBox = document.getElementById('<%= cbRegNo.ClientID %>');
            var options = chkBox.getElementsByTagName('input');
            var listOfSpans = chkBox.getElementsByTagName('label');
            var btnRegToggle = document.getElementById('btnRegToggle');
            var lblRegCount = document.getElementById('<%=lblRegCount.ClientID%>');
            var hiddenValue = document.getElementById('<%=Register.ClientID %>');
            var regSelected = '';
            var count = 0;
            /////
            for (var i = 0; i < options.length; i++) {
                if (options[i].checked) {
                    var values = listOfSpans[i].innerHTML;
                    if (count > 0) {
                        regSelected += ", ";
                    }
                    if (count >= 0) {
                        $('.lblReg').removeClass('hidden');
                    }
                    regSelected += values;
                    count = count + 1;
                }
            }
            regSelected.lastIndexOf(',')
            btnRegToggle.innerText = 'Total Selected Reg. - ' + count;
            lblRegCount.innerHTML = regSelected;
            document.getElementById('<%=lblRegOutSide.ClientID%>').innerText = regSelected;
            document.getElementById('<%=Register.ClientID %>').value = regSelected;
        }

        function Opendrop() { //// Open Checbox DropDown.
            var chkBox = document.getElementById('<%= cbRegNo.ClientID %>');
            if (chkBox != null) {
                document.getElementById('divdrop').classList.add('in');
            }
            else {
                alert('Please Select Exam Year & Session!');
            }
        }

        function modalOnEdit() {
            $("#modalOnEdit").modal();
            Closedrop(1);
        }
        function ShowLoad() {
            document.getElementById("loader").style.display = "flex";
            $(".Fixed-Center").show();
            var a  ="";
        };

        var openDrop = false;
        document.onmouseup = function (e) { /// Close Checkbox DropDown On Outside Click.
            var cb = document.getElementById('divdrop');
            if (openDrop == true) {
                openDrop = false;
                return false;
            }
            if (cb.classList.item(2) == 'in') {
                cb.hidden = true;
                cb.classList.remove('in');
            }
        }
        document.getElementById('divdrop').onmouseup = function () {
            openDrop = true;
        }


        
    </script>
    <asp:HiddenField ID="Register" runat="server" />
</asp:Content>