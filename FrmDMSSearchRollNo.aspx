<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeFile="FrmDMSSearchRollNo.aspx.cs" Inherits="FrmDMSSearchRollNo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style type="text/css">
        .jumbotron {
            margin-top: 10px;
            margin-bottom: 15px;
            box-shadow: 0px 5px 20px -4px rgb(51, 116, 183);
            background-color: white;
            padding: 1em 2em;
        }

        .imgt {
            height: 100px;
            width: 100px;
        }

        section {
            text-align: center; /*margin: 50px 0;*/
        }

        .panzoom-parent {
            border: 2px solid #333;
        }

            .panzoom-parent .panzoom {
                border: 2px dashed #666;
            }

        .buttons {
            margin: 112px 0 0;
        }

        .btn-change {
            margin-bottom: 10px;
        }

        .viewport {
            position: absolute;
            overflow: hidden;
            top: 100px;
            left: 17px;
            width: 120%;
            height: 545px;
        }
    </style>
    <script src="js1/jquery.js" type="text/javascript"></script>
    <script src="js1/bootstrap.min.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ShowPopup() {

            //$('#mask').show();
            <%--$('#<%=pnlpopup.ClientID %>').show();--%>
            $('#divImgModal').modal();
        }
        function HidePopup() {

            //$('#mask').hide();
            <%--$('#<%=pnlpopup.ClientID %>').hide();--%>
            $('#divImgModal').toggle();
        }

    </script>
    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
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
                    Student Roll No.
                <asp:TextBox ID="txtrollno" runat="server" placeholder="Student Roll No." CssClass="form-control"></asp:TextBox>
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
                        <strong>Please! </strong>Select Exam Year, Session & Roll No.
                    </div>
                    <div class="alert-danger form-control" visible="false" id="AlertRecordNotF" runat="server">
                        <span class="glyphicon glyphicon-exclamation-sign"></span>
                        <strong>This! </strong>Record Not Found.
                    </div>
                </div>

            </div>
        </div>
    </div>


    <asp:Panel ID="pnlstudentDetail" runat="server" Visible="false" BackColor="White" Width="75%" Style="z-index: 111;min-height: 55px; max-height: 150px; background-color: #f1efef; margin:auto">
    <div class="jumbotron">        
        <asp:GridView TabIndex="9" align="center" ID="GridStudentDetail" DataKeyNames="RollNo,PartARegNo,PartAImagePath,PartBRegNo,PartBImagePath"
            runat="server" HeaderStyle-BackColor="#295582" HeaderStyle-Font-Bold="true" HeaderStyle-HorizontalAlign="Center"
            HeaderStyle-ForeColor="White" Width="99%" CellPadding="4" RowStyle-BorderStyle="Solid"
            ForeColor="Black" GridLines="Vertical" BackColor="#f1efef" BorderColor="Gray"
            BorderStyle="Solid" BorderWidth="2px" OnRowCommand="GridStudentDetail_RowCommand1"
            ShowFooter="false" AutoGenerateColumns="false">
            <RowStyle BorderStyle="Solid" />
            <Columns>
                <asp:BoundField HeaderText="Roll No." DataField="RollNo" ItemStyle-Width="8%" />
                <asp:TemplateField HeaderText="Exam Name" ItemStyle-Width="55%">
                    <ItemTemplate>
                        <asp:Label ID="lblExamName" Text='<%#Eval("ExamName") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="ExamName" ItemStyle-Width="55%" />--%>

                <asp:TemplateField HeaderText="Action" ItemStyle-Width="6%">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandArgument='<%#Eval("FilePath") %>'
                            CommandName="ShowPopup">View</asp:LinkButton>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="0px" HeaderText="PartARegNo" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblPartARegNo" Text='<%#Eval("PartARegNo") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="0px" HeaderText="PartAImagePath" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblPartAImagePath" Text='<%#Eval("PartAImagePath") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="0px" HeaderText="PartBRegNo" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblPartBRegNo" Text='<%#Eval("PartBRegNo") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ItemStyle-Width="0px" HeaderText="PartBImagePath" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblPartBImagePath" Text='<%#Eval("PartBImagePath") %>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="PartARegNo" ItemStyle-Width="0px" HeaderText="PartARegNo" Visible="false" />--%>
                <%--<asp:BoundField DataField="PartAImagePath" ItemStyle-Width="0px" HeaderText="PartAImagePath" Visible="false" />--%>
                <%--<asp:BoundField DataField="PartBRegNo" ItemStyle-Width="0px" HeaderText="PartBRegNo" Visible="false" />--%>
                <%--<asp:BoundField DataField="PartBImagePath" ItemStyle-Width="0px" HeaderText="PartBImagePath" Visible="false" />--%>
            </Columns>
            <HeaderStyle BackColor="#50618C" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
        </asp:GridView>
    </div>
    </asp:Panel>

    <div class="modal fade" id="divImgModal" data-backdrop="static">
        <div class="modal-dialog" style="margin: auto; margin-top: 1%; width: 80%">
            <div class="modal-content">
                <div class="modal-header">
                    <button class="close">&times;</button>
                    <div class="modal-title">
                        <asp:Label ID="lblExamName" runat="server" />
                    </div>
                </div>
                <div class="modal-body" style="padding-top:0;padding-bottom:0">
                    <div id="divImage" runat="server">
                        <%--  <section>--%>

                        <div id="divSection" class="panzoom" style="align-content: center">

                            <div class="jumbotron">
                                <div class="form-group">
                                    <table class="col-md-12">
                                        <thead>
                                            <tr>
                                                <th style="text-align: center">Foil Image <asp:Label ID="lblFoilReg" runat="server" /> </th>
                                                <th class="text-center">Counter Foil Image <asp:Label ID="lblCFReg" runat="server" /> </th>
                                            </tr>
                                        </thead>
                                        <tr>
                                            <td style="border: 1px solid black;">
                                                
                                                <asp:Image ID="ImageFoil" class='zoom' runat="server" Height="500px" Width="100%" Style="background-size: 581px 500px;"  />
                                                <%--<img id="image" src='<%# Eval("FilePath")%>' alt="" class="img-responsive" style="position: relative; top: 0px; left: 0px; width: 100%; height: 545px;" />--%>
                                                <script type="text/javascript" src="js/wheelzoom.js"></script>
                                                <script type="text/javascript">
                                                    wheelzoom(document.querySelector('img.zoom'));
                                                </script>
                                            </td>
                                            <td style="border: 1px solid black;">
                                                <script type="text/javascript" src="js/wheelzoom.js"></script>
                                                <asp:Image ID="ImageCF" class='zoom22' runat="server"  Height="500px" Width="100%" Style="background-size: 581px 500px;"  />
                                                <%--<img id="imageCF" src='<%# Eval("FilePath")%>' alt="" class="img-responsive" style="top: 0px; left: 0px; width: 100%; height: 545px;" />--%>


                                                <script type="text/javascript">
                                                    wheelzoom(document.querySelector('img.zoom22'));
                                                </script>

                                            </td>
                                        </tr>

                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <%--<script type="text/javascript">
        $(document).ready(function () {
            $('#divSection').bind('wheel mousewheel', function (e) {
                var delta;

                if (e.originalEvent.wheelDelta !== undefined)
                    delta = e.originalEvent.wheelDelta;
                else
                    delta = e.originalEvent.deltaY * -1;

                if (delta > 0) {
                    $("#divSection").css("width", "+=10");
                }
                else {
                    $("#divSection").css("width", "-=10");
                }
            });
        });

    </script>--%>
    <script type="text/javascript">
        (function ($) {
            $("#ImageFoil").imgViewer({
                onClick: function (e, self) {
                    var pos = self.cursorToImg(e.pageX, e.pageY);
                    $("#position").html(e.pageX + " " + e.pageY + " " + pos.x + " " + pos.y);
                    $(this).rotate(90);
                }
            });

            $("#ImageCF").imgViewer({
                onClick: function (e, self) {
                    var pos = self.cursorToImg(e.pageX, e.pageY);
                    $("#position").html(e.pageX + " " + e.pageY + " " + pos.x + " " + pos.y);
                    $(this).rotate(90);
                }
            });

        })(jQuery);

        //(function ($) {


        //})(jQuery);

    </script>
</asp:Content>

