<%@ Page Title="Neta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NetaWeb._Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

          <div style="width: 600px;height:auto; overflow: auto;overflow-y: hidden;">
              <div id="chart_div"></div>
           </div>

<html>
<head>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <link rel="stylesheet" type="text/css" href="Content/style.css">
    <link href="Content/searchBar.css" rel="stylesheet" type="text/css">
    <title>Neta</title>
</head>

<body>
 
   

    <script type="text/javascript">

    jQuery(document).ready(function() {
    jQuery('.tabs .tab-links a').on('click', function(e)  {
    var currentAttrValue = jQuery(this).attr('href');
 
    // Show/Hide Tabs
    jQuery('.tabs ' + currentAttrValue).show().siblings().hide();
 
    // Change/remove current tab to active
    jQuery(this).parent('li').addClass('active').siblings().removeClass('active');
 
    e.preventDefault();
    });
    });

    $(document).ready(function(){
    $("#accordian h3").click(function(){
    //slide up all the link lists
    $("#accordian ul ul").slideUp();
    //slide down the link list below the h3 clicked - only if its closed
    if(!$(this).next().is(":visible"))
    {
    $(this).next().slideDown();
    }
    })
    })

    </script>

    
    <asp:Literal ID="lt" runat="server" ></asp:Literal>
    <!-- Header -->
    <div class="container">
    <header>
    <div class=frame>
    <span class="helper"></span><img src="images/NetaLogo.png" width="80" height="84"/>
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
    <a href="#tab1" style="text-decoration:none">MAP VIEW</a></li>
    <li><a href="#tab2" style="text-decoration:none">CHART VIEW</a></li>
    </ul>
 
    <div class="tab-content">
    
    <div id="tab1" class="tab active">

    <!-- Map Content -->
    <div>

    <div class="center">    
    <script src=	'https://maps.googleapis.com/maps/api/js?key=AIzaSyDCyhQSMdTS9kOTc-Rlg-hHwTmQNCsCp8Y&sensor=false&extension=.js'></script> 
 
    <script> 
    google.maps.event.addDomListener(window, 'load', init);
    var map;
    function init() {
    var mapOptions = {
    center: new google.maps.LatLng(54.619798,-6.834413),
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
    draggable : true,
    overviewMapControl: true,
    overviewMapControlOptions: {
    opened: false,
    },
    mapTypeId: google.maps.MapTypeId.ROADMAP,
    styles: [{"featureType":"water","elementType":"geometry","stylers":[{"color":"#e9e9e9"},{"lightness":17}]},{"featureType":"landscape","elementType":"geometry","stylers":[{"color":"#f5f5f5"},{"lightness":20}]},{"featureType":"road.highway","elementType":"geometry.fill","stylers":[{"color":"#ffffff"},{"lightness":17}]},{"featureType":"road.highway","elementType":"geometry.stroke","stylers":[{"color":"#ffffff"},{"lightness":29},{"weight":0.2}]},{"featureType":"road.arterial","elementType":"geometry","stylers":[{"color":"#ffffff"},{"lightness":18}]},{"featureType":"road.local","elementType":"geometry","stylers":[{"color":"#ffffff"},{"lightness":16}]},{"featureType":"poi","elementType":"geometry","stylers":[{"color":"#f5f5f5"},{"lightness":21}]},{"featureType":"poi.park","elementType":"geometry","stylers":[{"color":"#dedede"},{"lightness":21}]},{"elementType":"labels.text.stroke","stylers":[{"visibility":"on"},{"color":"#ffffff"},{"lightness":16}]},{"elementType":"labels.text.fill","stylers":[{"saturation":36},{"color":"#333333"},{"lightness":40}]},{"elementType":"labels.icon","stylers":[{"visibility":"off"}]},{"featureType":"transit","elementType":"geometry","stylers":[{"color":"#f2f2f2"},{"lightness":19}]},{"featureType":"administrative","elementType":"geometry.fill","stylers":[{"color":"#fefefe"},{"lightness":20}]},{"featureType":"administrative","elementType":"geometry.stroke","stylers":[{"color":"#fefefe"},{"lightness":17},{"weight":1.2}]}],
    }
    var mapElement = document.getElementById('NI');
    var map = new google.maps.Map(mapElement, mapOptions);
    var locations = [

    ];
    for (i = 0; i < locations.length; i++) {
    if (locations[i][1] =='undefined'){ description ='';} else { description = locations[i][1];}
    if (locations[i][2] =='undefined'){ telephone ='';} else { telephone = locations[i][2];}
    if (locations[i][3] =='undefined'){ email ='';} else { email = locations[i][3];}
    if (locations[i][4] =='undefined'){ web ='';} else { web = locations[i][4];}
    if (locations[i][7] =='undefined'){ markericon ='';} else { markericon = locations[i][7];}
    marker = new google.maps.Marker({
    icon: markericon,
    position: new google.maps.LatLng(locations[i][5], locations[i][6]),
    map: map,
    title: locations[i][0],
    desc: description,
    tel: telephone,
    email: email,
    web: web
    });
    link = '';     }

    }
    </script>
        

    <style>
    #NI {
    height:400px;
    width:600px;
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
    </div><!-- End Tab 1 Content -->
 
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

<asp:GridView ID="Grid1D" runat="server" Font-Names = "Arial"
    Font-Size = "11pt" AlternatingRowStyle-BackColor = "#C2D69B" 
    HeaderStyle-BackColor = "green" Caption = "1-Dimensional Array" OnSelectedIndexChanged="Grid1D_SelectedIndexChanged">
</asp:GridView>

<asp:ObjectDataSource ID="ObjectDataSource1" runat="server"></asp:ObjectDataSource>
</div>



    
</asp:Content>
