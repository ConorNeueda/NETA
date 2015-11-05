<%@ Page Title="Neta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="NetaWeb._Default" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <html>

    <head>
        <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
        <script type="text/javascript" src="https://www.google.com/jsapi"></script>
        <link rel="stylesheet" type="text/css" href="Content/style.css">
        <link rel="stylesheet" type="text/css" href="Content/text.css">
        <title>Neta</title>
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

        <asp:Literal ID="lt" runat="server"></asp:Literal>

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

        <div class="centerWritingHeading">
            <h4>Hi!</h4>
        </div>
        <div class="centerAbout">
            <p>Neta is an intelligence tool that focuses on the effect of IT Infrastructure on... </p>
        </div>

    </body>

    </html>
    </asp:Content>