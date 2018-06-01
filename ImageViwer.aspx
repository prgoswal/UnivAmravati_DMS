<%@ Page Title="" Language="C#" MasterPageFile="~/SubMaster.master" AutoEventWireup="true" CodeFile="ImageViwer.aspx.cs" Inherits="UniteGalary2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type='text/javascript' src='unitegallery/js/jquery-11.0.min.js'></script>
    <script type='text/javascript' src='unitegallery/js/unitegallery.min.js'></script>
    <link rel='stylesheet' href='unitegallery/css/unite-gallery.css' type='text/css' />
    <script type='text/javascript' src='unitegallery/themes/compact/ug-theme-compact.js'></script>
    <div class="row">
        <div class="col-lg-10">
            <div class="form-group">
                <asp:Label runat="server" ID="lblRegNo" Style="font-weight: 600;" class="col-sm-2 col-md-3 col-lg-2"
                    Text="Enter Register No.">
                </asp:Label>
                <div class="col-sm-3 col-md-2 col-lg-2">
                    <asp:TextBox ID="txtRegNo" CssClass="form-control input-sm" placeholder="Enter Register No." runat="server"></asp:TextBox>
                </div>
                <div class="col-sm-2 col-md-2 col-lg-2">
                    <asp:LinkButton ID="btnSearchImg" CssClass="btn btn-sm btn-primary" runat="server"
                        Text="" OnClick="btnSearchImg_Click"><i class="fa fa-search"> </i>&nbsp;Search Image</asp:LinkButton>
                </div>
                <div class="col-sm-2 col-md-5 col-lg-5">
                    <asp:Label runat="server" ID="lblCount" Style="font-family: Times New Roman" ForeColor="Red"
                        Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="form-group">
                <asp:Label runat="server" ID="lblDetails" Style="font-family: Times New Roman" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </div>
    <br />
    <br />
    <asp:Panel runat="server" ID="pnlImageViewer" Visible="false">
        <div id="gallery" style="display: none;">
            <asp:Repeater runat="server" ID="dlImages">
                <ItemTemplate>
                    <img src='<%# Eval("FilePath")%>' data-image='<%# Eval("FilePath")%>' />
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </asp:Panel>
    <br />
    <br />
    <%--<input type="button" value="Next Image" onclick="api.nextItem()" />
    <input type="button" value="Prev Image" onclick="api.prevItem()" />
    <input type="button" value="Play" onclick="api.play()" />
    <input type="button" value="Stop" onclick="api.stop()" />
    <input type="button" value="Toggle Fullscreen" onclick="api.toggleFullscreen()" />
    <input type="button" value="Zoom In" onclick="api.zoomIn()" />
    <input type="button" value="Zoom Out" onclick="api.zoomOut()" />
    <input type="button" value="Reset Zoom" onclick="api.resetZoom()" />
    <input type="button" value="Resize" onclick="api.resize(800)" />
    <input type="button" value="Select Item" onclick="api.selectItem(4)" />
    <input type="button" value="Get Item" onclick="alert(api.getItem(4).title)" />--%>

    <script type="text/javascript">
        var api;
        jQuery(document).ready(function () {
            api = jQuery("#gallery").unitegallery();
            //those are the eventws you can use:
            api.on("item_change", function (num, data) {		//on item change, get item number and item data
                if (console)
                    console.log(data);
                //do something
            });

            api.on("resize", function () {				//on gallery resize
                //do something
            });

            api.on("enter_fullscreen", function () {	//on enter fullscreen
                //do something
            });

            api.on("exit_fullscreen", function () {	//on exit fulscreen
                //do something
            });

            api.on("play", function () {				//on start playing
                //do something
            });

            api.on("stop", function () {				//on stop playing
                //do something
            });

            api.on("pause", function () {				//on pause playing
                //do something
            });
            api.on("continue", function () {			//on continue playing
                //do something
            });
        });
    </script>
</asp:Content>

