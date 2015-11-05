<%@ Page Title="Neta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NetaWeb._Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

         
<br />
<br />
<div id="csv_upload_div" style="border:1px solid black;margin:10px;padding:10px;">
    <div>
        <br />
        <br />
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="UploadFile" />
        <hr />
    </div>
        <br />
        <br />
    <div>
        <br />
         <asp:UpdatePanel ID="UpdatePanel1" runat="server">
             <ContentTemplate>
<br />
                 <asp:GridView ID="grdFiles" runat="server" AutoGenerateColumns="false" EmptyDataText="No files uploaded">
                     <Columns>
                         <asp:BoundField DataField="Text" HeaderText="File Name" />
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkDownload" runat="server" CommandArgument='<%# Eval("Value") %>' OnClick="DownloadFile" Text="Download"></asp:LinkButton>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Eval("Value") %>' OnClick="DeleteFile" Text="Delete" />
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField>
                             <ItemTemplate>
                                 <asp:LinkButton ID="lnkView" runat="server" CommandArgument='<%# Eval("Value") %>' OnClick="ViewFile" Text="View" />
                             </ItemTemplate>
                         </asp:TemplateField>
                     </Columns>
                 </asp:GridView>
<br />
<br />
<br />
                 <asp:Button ID="btnMerge" runat="server" OnClick="btnMerge_OnClick" Text="Merge Data" />
<br />
<br />
                 <asp:GridView ID="csvUploadResults" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                     <AlternatingRowStyle BackColor="White" />
                     <EditRowStyle BackColor="#2461BF" />
                     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                     <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                     <RowStyle BackColor="#EFF3FB" />
                     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                     <SortedAscendingCellStyle BackColor="#F5F7FB" />
                     <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                     <SortedDescendingCellStyle BackColor="#E9EBEF" />
                     <SortedDescendingHeaderStyle BackColor="#4870BE" />
                 </asp:GridView>
<br />
<br />
<br />
             </ContentTemplate>
         </asp:UpdatePanel>
        <br />
         <strong>Create Combo Chart from data:
         <br />
         </strong>
        <br />
        Select X Axis column (enter column number)
        <br />
        <asp:TextBox runat="server" ID ="txtXAxis" Width="16px"></asp:TextBox>
        <br />
        Select Y Axis 1 column (enter column number)
        <br />
        <asp:TextBox runat="server" ID ="txtYAxis1" Width="16px"></asp:TextBox>
        <br />
        Select Y Axis 2 column (enter column number)
        <br />
        <asp:TextBox runat="server" ID ="txtYAxis2" Width="16px"></asp:TextBox>
        <br />
        <asp:Button runat="server" ID="btnPlot" Text="plot" OnClick="btnPlot_OnCLick" />
        <br />
        <br />
    </div>
</div>
<br />
<br />
    <html>
    <head>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="https://www.google.com/jsapi"></script>
        <link rel="stylesheet" type="text/css" href="Content/style.css">
        <link href="Content/searchBar.css" rel="stylesheet" type="text/css">
        <title>Neta</title>
    </head>

    <body>
          <asp:Literal ID="chartLit" runat="server"></asp:Literal>
          <div style="width: auto;height:auto; overflow: auto;overflow-y: hidden;">
              <div id="myChart"></div>
          </div>


 
        <script type="text/javascript">

            jQuery(document).ready(function () {
                jQuery('.tabs .tab-links a').on('click', function (e) {
                    var currentAttrValue = jQuery(this).attr('href');

                    // Show/Hide Tabs
                    jQuery('.tabs ' + currentAttrValue).show().siblings().hide();

                    // Change/remove current tab to active
                    jQuery(this).parent('li').addClass('active').siblings().removeClass('active');

                    e.preventDefault();
                });
            });

            $(document).ready(function () {
                $("#accordian h3").click(function () {
                    //slide up all the link lists
                    $("#accordian ul ul").slideUp();
                    //slide down the link list below the h3 clicked - only if its closed
                    if (!$(this).next().is(":visible")) {
                        $(this).next().slideDown();
                    }
                })
            })

        </script>
        
        <asp:Literal ID="authPopSpeed" runat="server"></asp:Literal>
        <asp:Literal ID="authEmpSyncSpeed" runat="server"></asp:Literal>
        
        <!-- Header -->
        <div class="container">
            <header>
                <div class="frame">
                    <span class="helper"></span>
                    <img src="images/NetaLogo.png" width="80" height="84" />
                </div>
            </header>
        </div>
        <div class="sidebar1">
            <div id="accordian">
                <ul>
                    <li>
                        <h3><span class="icon-dashboard"></span>Neta</h3>
                        <ul>
                            <li><a href="#">About</a></li>
                        </ul>
                    </li>
                    <li>
                        <h3><span class="icon-calendar"></span>Education</h3>
                        <ul>
                            <li><a href="#">GCSE Results</a></li>
                        </ul>
                    </li>
                    <li>
                        <h3><span class="icon-heart"></span>Population</h3>
                        <ul>
                            <li><a href="#">Population by Local Authority</a></li>
                        </ul>
                    </li>
                </ul>
            </div>
        </div>

        <div id="rightmenu">
            <h4>BROADBAND SPEED</h4>
        </div>
        <!-- Tabs -->
        <div class="centerTabs">
            <div class="tabs">
                <ul class="tab-links">
                    <li class="active">
                        <a href="#tab1" style="text-decoration: none">MAP VIEW</a></li>
                    <li><a href="#tab2" style="text-decoration: none">CHART VIEW</a></li>
                </ul>

                <div class="tab-content">

                    <div id="tab1" class="tab active">

                        <!-- Map Content -->
                        <div>

                            <div class="center">
                                <script src='https://maps.googleapis.com/maps/api/js?key=AIzaSyDCyhQSMdTS9kOTc-Rlg-hHwTmQNCsCp8Y&sensor=false&extension=.js'></script>

                                <asp:Literal ID="mapLit" runat="server"></asp:Literal>
                                <!-- Map Content 
                                <script>
                                    google.maps.event.addDomListener(window, 'load', init);

                                    var map;
                                    function init() {

                                        var locations = [
                                         ['Title A', 3.180967, 101.715546, 1],
                                         ['Title B', 3.200848, 101.616669, 2],
                                         ['Title C', 3.147372, 101.597443, 3],
                                         ['Title D', 3.19125, 101.710052, 4]
                                        ];
                                        var mapOptions = {
                                            center: new google.maps.LatLng(54.619798, -6.834413),
                                            zoom: 8,
                                            zoomControl: true,
                                            zoomControlOptions: {
                                                style: google.maps.ZoomControlStyle.DEFAULT,
                                            },
                                            disableDoubleClickZoom: true,
                                            mapTypeControl: true,
                                            mapTypeControlOptions: {
                                                style: google.maps.MapTypeControlStyle.HORIZONTAL_BAR,
                                            },
                                            scaleControl: true,
                                            scrollwheel: true,
                                            panControl: true,
                                            streetViewControl: true,
                                            draggable: true,
                                            overviewMapControl: true,
                                            overviewMapControlOptions: {
                                                opened: false,
                                            },
                                            mapTypeId: google.maps.MapTypeId.ROADMAP,
                                            styles: [{ "featureType": "water", "elementType": "geometry", "stylers": [{ "color": "#e9e9e9" }, { "lightness": 17 }] }, { "featureType": "landscape", "elementType": "geometry", "stylers": [{ "color": "#f5f5f5" }, { "lightness": 20 }] }, { "featureType": "road.highway", "elementType": "geometry.fill", "stylers": [{ "color": "#ffffff" }, { "lightness": 17 }] }, { "featureType": "road.highway", "elementType": "geometry.stroke", "stylers": [{ "color": "#ffffff" }, { "lightness": 29 }, { "weight": 0.2 }] }, { "featureType": "road.arterial", "elementType": "geometry", "stylers": [{ "color": "#ffffff" }, { "lightness": 18 }] }, { "featureType": "road.local", "elementType": "geometry", "stylers": [{ "color": "#ffffff" }, { "lightness": 16 }] }, { "featureType": "poi", "elementType": "geometry", "stylers": [{ "color": "#f5f5f5" }, { "lightness": 21 }] }, { "featureType": "poi.park", "elementType": "geometry", "stylers": [{ "color": "#dedede" }, { "lightness": 21 }] }, { "elementType": "labels.text.stroke", "stylers": [{ "visibility": "on" }, { "color": "#ffffff" }, { "lightness": 16 }] }, { "elementType": "labels.text.fill", "stylers": [{ "saturation": 36 }, { "color": "#333333" }, { "lightness": 40 }] }, { "elementType": "labels.icon", "stylers": [{ "visibility": "off" }] }, { "featureType": "transit", "elementType": "geometry", "stylers": [{ "color": "#f2f2f2" }, { "lightness": 19 }] }, { "featureType": "administrative", "elementType": "geometry.fill", "stylers": [{ "color": "#fefefe" }, { "lightness": 20 }] }, { "featureType": "administrative", "elementType": "geometry.stroke", "stylers": [{ "color": "#fefefe" }, { "lightness": 17 }, { "weight": 1.2 }] }],
                                        }


                                        var mapElement = document.getElementById('NI');
                                        var map = new google.maps.Map(mapElement, mapOptions);
                                        var marker, i;

                                        for (i = 0; i < locations.length; i++) {
                                            marker = new google.maps.Marker({
                                                position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                                                map: map
                                            });
                                        }
                                    }
                                </script>
                                    -->

                                <style>
                                    #NI {
                                        height: 400px;
                                        width: 600px;
                                    }

                                    .gm-style-iw * {
                                        display: block;
                                        width: 100%;
                                    }

                                    .gm-style-iw h4, .gm-style-iw p {
                                        margin: 0;
                                        padding: 0;
                                    }

                                    .gm-style-iw a {
                                        color: #4272db;
                                    }

                                    #chart_div {
                                        width: 1757px;
                                    }
                                </style>

                                <div id='NI'></div>
                            </div>

                        </div>
                        <!-- End Map Content -->
                    </div>
                    <!-- End Tab 1 Content -->

                    <div id="tab2" class="tab">
                        <p>Tab #2 content goes here!</p>
                        <p>Hello</p>
                    </div>
                </div>
            </div>
        </div>
    </body>
    </html>
<div class="jumbotron">
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
</div>
</asp:Content>
