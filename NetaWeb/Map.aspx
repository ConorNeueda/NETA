<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Map.aspx.cs" Inherits="NetaWeb.Map" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <link rel="stylesheet" type="text/css" href="Content/style.css">
    <link rel="stylesheet" type="text/css" href="Content/text.css">
    <title>Neta</title>
    <script src="http://maps.googleapis.com/maps/api/js?key=AIzaSyBlMXOk_OIONKlwNdtu8dW0LgM2fJvBL1k"></script>
</head>
<body>

    <script type="text/javascript">

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

    <!-- Header -->
    <header>
        <div class="frame">
            <div class="helper"></div>
            <img src="images/NetaLogo.png" width="80" height="84" />
        </div>
    </header>
    <!-- End Header -->

    <!-- Left Sidebar -->
    <div class="sidebar1">
        <div id="accordian">
            <ul>
                <li>
                    <h3>NETA</h3>
                    <ul>
                        <li><a href="Default.aspx">About Us</a></li>
                    </ul>
                </li>
                <li>
                    <h3>MAP VIEW</h3>
                    <ul>
                        <li><a href="Map.aspx">Map View</a></li>
                    </ul>
                </li>
                <li>
                    <h3>CHART VIEW</h3>
                    <ul>
                        <li><a href="Charts.aspx">Chart View</a></li>
                    </ul>
                </li>
                <li>
                    <h3>UPLOAD</h3>
                    <ul>
                        <li><a href="Upload.aspx">Upload Your Data</a></li>
                    </ul>
                </li>
            </ul>
        </div>
    </div>
    <!-- End Left Sidebar -->


    <!-- Map Content -->
    <script> 
    google.maps.event.addDomListener(window, 'load', init);
    var map;
    function init() {
        var mapOptions = {
            center: new google.maps.LatLng(54.634108,-6.238404),
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
['Co. Derry/Londonderry: 18.43mbs', '', 'undefined', 'undefined', 'undefined', 55.1325802, -6.66461019999997, 'https://mapbuildr.com/assets/img/markers/ellipse-black.png'],['Co. Tyrone: 15.40mbps', 'undefined', 'undefined', 'undefined', 'undefined', 54.5977149, -7.3099595999999565, 'https://mapbuildr.com/assets/img/markers/ellipse-black.png'],['Co. Fermanagh: 15.09mbps', 'undefined', 'undefined', 'undefined', 'undefined', 54.34382429999999, -7.631533600000012, 'https://mapbuildr.com/assets/img/markers/ellipse-black.png'],['Co. Armagh: 16.26mbps', 'undefined', 'undefined', 'undefined', 'undefined', 54.3502798, -6.652791999999977, 'https://mapbuildr.com/assets/img/markers/ellipse-black.png'],['Co. Antrim: 19.94mbps', 'undefined', 'undefined', 'undefined', 'undefined', 54.59728500000001, -5.930119999999988, 'https://mapbuildr.com/assets/img/markers/ellipse-black.png'],['Co. Down: 16.86mbps', 'undefined', 'undefined', 'undefined', 'undefined', 54.32875139999999, -5.715692200000035, 'https://mapbuildr.com/assets/img/markers/ellipse-black.png']
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
        height:500px;
        width:800px;
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
</style>

<div id='NI'></div>
    <!-- End Map Content -->

</body>
</html>