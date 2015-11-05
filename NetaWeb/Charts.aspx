<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Charts.aspx.cs" Inherits="NetaWeb.Charts" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <title>Neta</title>
    <link rel="stylesheet" type="text/css" href="Content/style.css">

</head>
<body>

    <form id="form1" runat="server">

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
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
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

    <div id ="chart_div" runat="server" style="width:500px;height:400px"></div>
     <asp:Literal ID="bbprScripts" runat="server"></asp:Literal> 
     <asp:Label ID="CorrelationLabel1" runat="Server" Text="Spearmans Rank Correlation: "></asp:Label>
     <asp:TextBox ID="BbprLabel" runat="server"></asp:TextBox>
     <asp:Label ID="CorrelationDescription1" runat="server" 
            Text ="A very weak, negative correlation score.
                  This tells us that each variable has little to no effect on the other.
                   As one variable changes, the other changes in the opposite direction very minimally."
         ></asp:Label>

     <div id="chart_div_1" runat="server" style="width:500px;height:400px"></div>
     <asp:Literal ID="apbLiteral" runat="server"></asp:Literal> 
     <asp:Label ID="CorrelationLabel2" runat="Server" Text="Spearmans Rank Correlation: "></asp:Label>
     <asp:TextBox ID="APBLabel" runat="server"></asp:TextBox>
     <asp:Label ID="CorrelationDescription2" runat="server"
            Text ="Weak, negative correlation score. As one variable changes, the other changes in the opposite direction."
      ></asp:Label>

     <div id="chart_div_2" runat="server" style="width:500px;height:400px"></div>
     <asp:Literal ID="ApssLiteral" runat="server"></asp:Literal>
     <asp:Label ID="CorrelationLabel3" runat="Server" Text="Spearmans Rank Correlation: "></asp:Label>
     <asp:TextBox ID="ApssLabel" runat="server"></asp:TextBox>
     <asp:Label ID="CorrelationDescription3" runat="server"
            Text ="A moderately positive correlation score. As one variable changes, the other changes in the same direction."
      ></asp:Label>

    <div id="chart_div_3" runat="server" style="width:500px;height:400px"></div>
     <asp:Literal ID="AesLiteral" runat="server"></asp:Literal>
     <asp:Label ID="CorrelationLabel4" runat="Server" Text="Spearmans Rank Correlation: "></asp:Label>
     <asp:TextBox ID="AesLabel" runat="server"></asp:TextBox>
     <asp:Label ID="CorrelationDescription4" runat="server"
            Text ="Weak, negative correlation score. As one variable changes, the other changes in the opposite direction."
      ></asp:Label>
    </form>
</body>
</html>

