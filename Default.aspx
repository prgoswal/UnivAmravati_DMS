<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GridView Images Example</title>

    <script type="text/javascript" src="libs/jquery/jquery.js"></script>

    <script type="text/javascript" src="libs/jquery/jquery-ui.js"></script>

    <script type="text/javascript" src="libs/jquery.fs.zoetrope.min.js"></script>

    <script type="text/javascript" src="libs/toe.min.js"></script>

    <script type="text/javascript" src="libs/jquery.mousewheel.min.js"></script>

    <script type="text/javascript" src="src/imgViewer.js"></script>

    <script type="text/javascript">
        function LoadDiv(url) {
            var img = new Image();
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            var imgLoader = document.getElementById("imgLoader");

            imgLoader.style.display = "block";
            img.onload = function() {
                imgFull.src = img.src;
                imgFull.style.display = "block";
                imgLoader.style.display = "none";
            };
            img.src = url;
            var width = document.body.clientWidth;
            if (document.body.clientHeight > document.body.scrollHeight) {
                bcgDiv.style.height = document.body.clientHeight + "px";
            }
            else {
                bcgDiv.style.height = document.body.scrollHeight + "px";
            }

            imgDiv.style.left = "2px";
            imgDiv.style.top = "5px";
            bcgDiv.style.width = "100%";

            bcgDiv.style.display = "block";
            imgDiv.style.display = "block";
            return false;
        }
        function HideDiv() {
            var bcgDiv = document.getElementById("divBackground");
            var imgDiv = document.getElementById("divImage");
            var imgFull = document.getElementById("imgFull");
            if (bcgDiv != null) {
                bcgDiv.style.display = "none";
                imgDiv.style.display = "none";
                imgFull.style.display = "none";
            }
        }
    </script>

    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            height: 100%;
            overflow-y: auto;
        }
        .modal
        {
            display: none;
            position: absolute;
            top: 0px;
            left: 0px;
            background-color: black;
            z-index: 100;
            opacity: 0.8;
            filter: alpha(opacity=60);
            -moz-opacity: 0.8;
            min-height: 100%;
        }
        #divImage
        {
            display: none;
            z-index: 1000;
            position: fixed;
            top: 0;
            left: 0;
            background-color: White;
            height: 650px;
            width: 100%;
            padding: 3px;
            border: solid 1px black;
        }
        * html #divImage
        {
            position: absolute;
        }
    </style>
    <!--[if lte IE 6]>
   <style type="text/css">
   /*<![CDATA[*/ 
html {overflow-x:auto; overflow-y:hidden;}
   /*]]>*/
   </style>
<![endif]-->
</head>
<body>

    <script type="text/javascript">
        ; (function($) {
        $("#divImage").imgViewer({
                onClick: function(e, self) {
                    var pos = self.cursorToImg(e.pageX, e.pageY);
                    $("#position").html(e.pageX + " " + e.pageY + " " + pos.x + " " + pos.y);

                }
            });
        })(jQuery);
    </script>

    <form id="form1" runat="server">
    <div>
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Font-Names="Arial">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="ID" />
                <asp:BoundField DataField="FileName" HeaderText="Image Name" />
                <asp:TemplateField HeaderText="Preview Image">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl='<%# Eval("FileName")%>'
                            Width="100px" Height="100px" Style="cursor: pointer" OnClientClick="return LoadDiv(this.src);" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div id="divBackground" class="modal">
    </div>
    <div id="divImage" class="info">
        <table style="height: 100%; width: 100%">
            <tr>
                <td valign="middle" align="center">
                    <img id="imgLoader" alt="" src="images/loader.gif" />
                    <img id="imgFull" runat="server" alt="" src="" style="display: none; height:610px;
                        width: 990px" />
                </td>
            </tr>
            <tr>
                <td align="center" valign="bottom">
                    <input id="btnClose" type="button" value="close" onclick="HideDiv()" />
                  
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
