<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="NetaWeb.Upload" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <script type="text/javascript" src="Scripts/jquery-1.10.2.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>
    <link rel="stylesheet" type="text/css" href="Content/style.css">
    <link rel="stylesheet" type="text/css" href="Content/text.css">
    <title>Neta</title>
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
                    <head>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
        <script type="text/javascript" src="https://www.google.com/jsapi"></script>
        <title>Neta</title>
    </head>
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

    <div id="csv_upload_div" style="border:1px solid black;margin:10px;padding:10px;">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
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

              <asp:Literal ID="chartLit" runat="server"></asp:Literal>
          <div style="width: auto;height:auto; overflow: auto;overflow-y: hidden;">
              <div id="myChart"></div>
          </div>
    </form>
</body>
</html>