﻿using System;
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
        private static average_performance_broadband[] allItems;
        private static BBandPassRate[] allBBandPRItems;
        private static AuthorityPop_SyncSpeed[] allAuthoritySpeedItems;
        private static AuthorityEmployment_Speed[] allEmploymentSpeedItems;
        private static SpearmansRank[] allSRItems;
        
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

            //string path = "C:\\test.csv";

            

            

            grdMyGrid.DataSource = myTable;
            grdMyGrid.DataBind();

            MakeChart();
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

        //CONVERT TO DATATABLE

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

        static DataTable ConvertToDatatable(average_performance_broadband[] list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("CountyID");
            dt.Columns.Add("PercentPassed");
            dt.Columns.Add("AverageSpeed");

            foreach (var item in list)
            {
                var row = dt.NewRow();
                row["CountyID"] = item.county_id_fk;
                row["PercentPassed"] = item.Average_Pass_of_5_GCSES;
                row["AverageSpeed"] = item.Average_Speed_mbps;

                dt.Rows.Add(row);
            }
            return dt;
        }//convertToDT

        static DataTable ConvertToDatatable(AuthorityPop_SyncSpeed[] list)
        {
            DataTable datatable = new DataTable();

            datatable.Columns.Add("Authority");
            datatable.Columns.Add("PopSize");
            datatable.Columns.Add("SyncSpeed");
            foreach (var item in list)
            {
                var row = datatable.NewRow();

                row["Authority"] = item.Authority;
                row["PopSize"] = item.PopSize;
                row["SyncSpeed"] = item.AverageSync;

                datatable.Rows.Add(row);
            }

            return datatable;
        }//convertToDT

        static DataTable ConvertToDatatable(AuthorityEmployment_Speed[] list)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Authority");
            dt.Columns.Add("EmploymentRate");
            dt.Columns.Add("SyncSpeed");

            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["Authority"] = item.Authority;
                row["EmploymentRate"] = item.EmploymentRate;
                row["SyncSpeed"] = item.SyncSpeed;

                dt.Rows.Add(row);
            }

            return dt;
        }

        static DataTable ConvertToDatatable(SpearmansRank[] list)
        {
            DataTable dt = new DataTable();

        public  void MakeChart()
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

        public void AveragePerformanceInFiveGCSEs_Average_BBand_ByCounty()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allItems = proxy.getAverages();
                var serilizer = new System.Web.Script.Serialization.JavaScriptSerializer();

            }

            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allItems);

            StringBuilder str = new StringBuilder();

            str.Append(@"<script type = 'text/javascript'>
                            google.load('visualization', '1', {packages: ['corechart']});

                        function drawVisualization(){
                        var data = google.visualization.arrayToDataTable([
                        ['CountyID', 'PercentPassed', 'AverageSpeed'],");

            int count = dt.Rows.Count - 1;

            for (int i = 0; i <= count; i++)
            {
                if (count == i)
                {
                    str.Append("["
                        + dt.Rows[i]["CountyID"].ToString()
                        + ","
                        + dt.Rows[i]["PercentPassed"].ToString()
                        + ","
                        + dt.Rows[i]["AverageSpeed"].ToString()
                        + "]]);");
                }//if
                else
                {
                    str.Append("["
                        + dt.Rows[i]["CountyID"].ToString()
                        + ","
                         + dt.Rows[i]["PercentPassed"].ToString()
                        + ","
                        + dt.Rows[i]["AverageSpeed"].ToString()
                        + "],");
                }//else
            }//for

            str.Append("var options = {vAxes: {0: {title: 'Percent', format: ''},1: {title: 'Average Speed(mbps)', format: '##'}}, hAxis: {title: 'County', format: ''}, seriesType: 'bars', series:{0:{type: 'line', targetAxisIndex: 0 }, 1: {type: 'bars', targetAxisIndex: 1}}, }; ");

            str.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div_1')); chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");

            Literal1.Text = str.ToString() + "</script>";
        }//AvgPerformance_Bband_ByCounty

        public void AuthorityPopulation_SyncSpeed()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allAuthoritySpeedItems = proxy.getUptakeByAuthority();
                var serilizer = new System.Web.Script.Serialization.JavaScriptSerializer();
            }

            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allAuthoritySpeedItems);

            StringBuilder str = new StringBuilder();

            str.Append(@"<script type = 'text/javascript'>
                            google.load('visualization', '1', {packages: ['corechart']});

                        function drawVisualization(){
                        var data = google.visualization.arrayToDataTable([
                        ['Authority', 'PopSize', 'SyncSpeed'],");

            int count = dt.Rows.Count - 1;

            for (int i = 0; i <= count; i++)
            {
                if (count == i)
                {
                    str.Append("['"
                        + dt.Rows[i]["Authority"].ToString()
                        + "',"
                        + dt.Rows[i]["PopSize"].ToString()
                        + ","
                        + dt.Rows[i]["SyncSpeed"].ToString()
                        + "]]);");
                }//if
                else
                {
                    str.Append("['"
                        + dt.Rows[i]["Authority"].ToString()
                        + "',"
                         + dt.Rows[i]["PopSize"].ToString()
                        + ","
                        + dt.Rows[i]["SyncSpeed"].ToString()
                        + "],");
                }//else
            }//for

            str.Append("var options = {vAxes: {0: {title: 'Population Size', format: ''},1: {title: 'Sync Speed mbits', format: '##'}}, hAxis: {title: 'Authority', format: ''}, seriesType: 'bars', series:{0:{type: 'line', targetAxisIndex: 0 }, 1: {type: 'bars', targetAxisIndex: 1}}, }; ");

            str.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div_2')); chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");

            Literal2.Text = str.ToString() + "</script>";
        }//AvgPop_SyncSpeed

        public void AuthorityEmployment_SyncSpeed()//AuthorityEmployment_SyncSpeed
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allEmploymentSpeedItems = proxy.GetAuthorityEmployment_Speed();
                var serilizer = new System.Web.Script.Serialization.JavaScriptSerializer();
        }

            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allEmploymentSpeedItems);

            StringBuilder str = new StringBuilder();

            str.Append(@"<script type = 'text/javascript'>
                            google.load('visualization', '1', {packages: ['corechart']});

                        function drawVisualization(){
                        var data = google.visualization.arrayToDataTable([
                        ['Authority', 'EmploymentRate', 'SyncSpeed'],");

            int count = dt.Rows.Count - 1;

            for (int i = 0; i <= count; i++)
            {
                if (count == i)
                {
                    str.Append("['"
                        + dt.Rows[i]["Authority"].ToString()
                        + "',"
                        + dt.Rows[i]["EmploymentRate"].ToString()
                        + ","
                        + dt.Rows[i]["SyncSpeed"].ToString()
                        + "]]);");
                }//if
                else
                {
                    str.Append("['"
                        + dt.Rows[i]["Authority"].ToString()
                        + "',"
                         + dt.Rows[i]["EmploymentRate"].ToString()
                        + ","
                        + dt.Rows[i]["SyncSpeed"].ToString()
                        + "],");
                }//else
            }//for

            str.Append("var options = {vAxes: {0: {title: 'Employment Rate', format: '##'},1: {title: 'Sync Speed mbits', format: '##'}}, hAxis: {title: 'Authority', format: ''}, seriesType: 'bars', series:{0:{type: 'bars', targetAxisIndex: 0 }, 1: {type: 'line', targetAxisIndex: 1}}, }; ");

            str.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div_3')); chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");

            Literal3.Text = str.ToString() + "</script>";
        }

        public static string Serialize(object o)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(o);
        }

        //GET CORRELATION METHODS

        static decimal GetApbCorrelation()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                apbCorrelation = proxy.GetCountyAveragePerformance_BroadbandCorrelation();
            }
            return apbCorrelation;
        }

        static decimal GetBbprCorrelation()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                bbprCorrelation = proxy.GetSchoolPR_BroadbandCorrelation();
            }
            return bbprCorrelation;
        }

        static decimal GetApssCorrelation()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                apssCorrelation = proxy.GetAuthorityPop_SyncSpeedCorrelation();
            }

            return apssCorrelation;
        }

        static decimal GetAesCorrelation()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                aesCorrelation = proxy.GetAuthoritySpeed_EmploymentCorrelation();
            }

            return aesCorrelation;
        }

        //BIND DATATABLE TO GRIDVIEW ITEMS

        private void BindGVData()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                average_performance_broadband[] allItems = proxy.getAverages();

                Grid1D.DataSource = allItems;
                Grid1D.DataBind();

            }
        }

        private void BindSpearmansToGridview()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                SpearmansRank[] allItems = proxy.CreateSpearmansRankTable();

                Grid1D.DataSource = allItems;
                Grid1D.DataBind();
            }
        }

        private void BindCorrelationToLabels()
        {
            APBLabel.Text = GetApbCorrelation().ToString();
            BbprLabel.Text = GetBbprCorrelation().ToString();
            ApssLabel.Text = GetApssCorrelation().ToString();
            AesLabel.Text = GetAesCorrelation().ToString();
        }

        //METHODS ASSOCIATED WITH FILE UPLOADS
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
            List<string> filepaths = new List<string>();
            List<DataTable> allTables = new List<DataTable>();
            DataTable mergedTables = new DataTable();
            int rowCount = grdFiles.Rows.Count;
            
            for (int i = 0; i < rowCount; i++)
            {
                string y = grdFiles.Rows[i].Cells[0].Text;
                filepaths.Add(Server.MapPath("~/Uploads/") + y);
            }

            foreach(string path in filepaths)
            {
                DataTable dt = new DataTable();
                dt = GetDataTableFromCsv(path, true);
                allTables.Add(dt);
            }

            foreach(DataTable datatable in allTables)
            {
                mergedTables.Merge(datatable);
            }
            csvUploadResults.DataSource = mergedTables;
            csvUploadResults.DataBind();
        }
    }
}