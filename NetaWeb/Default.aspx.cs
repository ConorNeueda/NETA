using System;
using System.Web.UI;
using NetaWeb.NetaServices;
using System.Data;
using System.Text;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Web;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace NetaWeb

{
    public partial class _Default : Page
    {
        public BBandPassRate[] allItems;
        public TopBroadbandSpeed[] topSpeeds;

        protected void Page_Load(object sender, EventArgs e)
        {
            DataTable myTable = new DataTable();
            if (!IsPostBack)
            {
                string[] filePaths = Directory.GetFiles(Server.MapPath("~/Uploads/"));
                List<ListItem> files = new List<ListItem>();
                foreach (string filePath in filePaths)
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                    myTable = GetDataTableFromCsv(filePath, true);
                    grdFiles.DataSource = files;
                    grdFiles.DataBind();
                }
            }
            csvUploadResults.DataSource = myTable;
            csvUploadResults.DataBind();

            MakeChart(0, "");
        }

        static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = Path.GetDirectoryName(path);
            string filename = Path.GetFileName(path);

            string sql = @"SELECT * FROM [" + filename + "]";

            using (OleDbConnection connection = new OleDbConnection(
                             @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
              ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable datatable = new DataTable();
                datatable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(datatable);
                return datatable;
            }
        }

        private void PopulateMap()
        {
            StringBuilder populateMap = new StringBuilder();

            populateMap.Append(@"<script type = 'text/javascript'>
                    google.maps.event.addDomListener(window, 'load', init);

                                    var map;
                                    function init() {

                                        var locations = [
                                         ['Title A', 3.180967, 101.715546, 1],
                                         ['Title B', 3.200848, 101.616669, 2],
                                         ['Title C', 3.147372, 101.597443, 3],
                                         ['Title D', 3.19125, 101.710052, 4]
                                        ];");

            populateMap.Append(@"  var mapOptions = {
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
                                            styles: [{ 'featureType': 'water', 'elementType': 'geometry', 'stylers': [{ 'color': '#e9e9e9' }, { 'lightness': 17 }] }, { 'featureType': 'landscape', 'elementType': 'geometry', 'stylers': [{ 'color': '#f5f5f5' }, { 'lightness': 20 }] }, { 'featureType': 'road.highway', 'elementType': 'geometry.fill', 'stylers': [{ 'color': '#ffffff' }, { 'lightness': 17 }] }, { 'featureType': 'road.highway', 'elementType': 'geometry.stroke', 'stylers': [{ 'color': '#ffffff' }, { 'lightness': 29 }, { 'weight': 0.2 }] }, { 'featureType': 'road.arterial', 'elementType': 'geometry', 'stylers': [{ 'color': '#ffffff' }, { 'lightness': 18 }] }, { 'featureType': 'road.local', 'elementType': 'geometry', 'stylers': [{ 'color': '#ffffff' }, { 'lightness': 16 }] }, { 'featureType': 'poi', 'elementType': 'geometry', 'stylers': [{ 'color': '#f5f5f5' }, { 'lightness': 21 }] }, { 'featureType': 'poi.park', 'elementType': 'geometry', 'stylers': [{ 'color': '#dedede' }, { 'lightness': 21 }] }, { 'elementType': 'labels.text.stroke', 'stylers': [{ 'visibility': 'on' }, { 'color': '#ffffff' }, { 'lightness': 16 }] }, { 'elementType': 'labels.text.fill', 'stylers': [{ 'saturation': 36 }, { 'color': '#333333' }, { 'lightness': 40 }] }, { 'elementType': 'labels.icon', 'stylers': [{ 'visibility': 'off' }] }, { 'featureType': 'transit', 'elementType': 'geometry', 'stylers': [{ 'color': '#f2f2f2' }, { 'lightness': 19 }] }, { 'featureType': 'administrative', 'elementType': 'geometry.fill', 'stylers': [{ 'color': '#fefefe' }, { 'lightness': 20 }] }, { 'featureType': 'administrative', 'elementType': 'geometry.stroke', 'stylers': [{ 'color': '#fefefe' }, { 'lightness': 17 }, { 'weight': 1.2 }] }],
                                        };");

            populateMap.Append(@"    var mapElement = document.getElementById('NI');
                                        var map = new google.maps.Map(mapElement, mapOptions);
                                        var marker, i;

                                        for (i = 0; i < locations.length; i++) {
                                            marker = new google.maps.Marker({
                                                position: new google.maps.LatLng(locations[i][1], locations[i][2]),
                                                map: map
                                            });
                                        }
                                    }");

            mapLit.Text = populateMap.ToString() + "</script>";


        }

        static DataTable ConvertToDatatable(BBandPassRate[] list)
        {
            DataTable datatable = new DataTable();

            datatable.Columns.Add("SchoolName");
            datatable.Columns.Add("PercentPassed");
            datatable.Columns.Add("AverageSpeed");
            foreach (var item in list)
            {
                var row = datatable.NewRow();

                row["SchoolName"] = item.SchoolName;
                row["PercentPassed"] = item.PassRate;
                row["AverageSpeed"] = item.AverageSpeed;


                datatable.Rows.Add(row);
            }

            return datatable;
        }

        public void MakeChart(int xAxis, string yAxis)
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allItems = proxy.MyView();
            }

            DataTable dt = new DataTable();

            dt = ConvertToDatatable(allItems);


            StringBuilder str = new StringBuilder();

            str.Append(@"<script type = 'text/javascript'>
                      google.load('visualization', '1', { packages: ['corechart']});
            
                      function drawVisualization() {
                      var data = google.visualization.arrayToDataTable([
                       ['SchoolName','PercentPassed','AverageSpeed'],");

            int count = dt.Rows.Count - 1;

            for (int i = 0; i <= count; i++)
            {
                if (count == i)
                {
                    str.Append("['"
                        + dt.Rows[i]["SchoolName"].ToString()
                        + "',"
                        + dt.Rows[i]["PercentPassed"].ToString()
                        + ","
                        + dt.Rows[i]["AverageSpeed"].ToString()
                        + "]]);");
                }
                else
                {
                    str.Append("['"
                        + dt.Rows[i]["SchoolName"].ToString()
                        + "',"
                         + dt.Rows[i]["PercentPassed"].ToString()
                        + ","
                        + dt.Rows[i]["AverageSpeed"].ToString()
                        + "],");
                }
            }


            str.Append("var options = {vAxes: {0: { format: ''},1: { format: '##'}},hAxis: { title: 'Week', format: 'm/d/y'},seriesType: 'bars',series:{ 0:{ type: 'bars', targetAxisIndex: 0 }, 1: { type: 'line', targetAxisIndex: 1}}, };");

            str.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  
                         chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");

            lt.Text = str.ToString() + "</script>";

        }

        protected void UploadFile(object sender, EventArgs e)
        {
            DataTable myTable = new DataTable();

            string fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            if (FileUpload1.FileName != null && FileUpload1.PostedFile != null)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Uploads/") + fileName);
                string fullPath = Server.MapPath("~/Uploads/") + fileName;

                myTable = GetDataTableFromCsv(fullPath, true);

                csvUploadResults.DataSource = myTable;
                csvUploadResults.DataBind();

                Response.Redirect(Request.Url.AbsoluteUri);
            }


        }

        protected void DownloadFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
        }

        protected void DeleteFile(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            File.Delete(filePath);
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void ViewFile(object sender, EventArgs e)
        {
            string filepath = (sender as LinkButton).CommandArgument;
            DataTable viewFileDataTable = new DataTable();

            viewFileDataTable = GetDataTableFromCsv(filepath, true);

            csvUploadResults.DataSource = viewFileDataTable;
            csvUploadResults.DataBind();
        }
         
        protected void btnPlot_OnCLick(object sender, EventArgs e)
        {

        }

        protected void btnMerge_OnClick(object sender, EventArgs e)
        {
            int x = grdFiles.Rows.Count;
            string y = grdFiles.Rows[0].Cells[0].Text;
        }
    }
}