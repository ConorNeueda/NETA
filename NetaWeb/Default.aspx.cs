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
using System.Web.Script.Serialization;
using System.Linq;

namespace NetaWeb

{
    public partial class _Default : Page
    {
        public average_performance_broadband[] allAPBItems;
        public BBandPassRate[] allBBPassRates;
        public AuthorityPop_SyncSpeed[] allAuthoritySpeedItems;
        public AuthorityEmployment_Speed[] allEmploymentSpeedItems;
        public DataTable mergedTables = new DataTable();

        private static decimal apbCorrelation;
        private static decimal bbprCorrelation;
        private static decimal apssCorrelation;
        private static decimal aesCorrelation;

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

           // MakeChart();

            /*
            AveragePerformanceInFiveGCSEs_Average_BBand_ByCounty();
            AuthorityPopulation_SyncSpeed();
            AuthorityEmployment_SyncSpeed();
            BindCorrelationToLabels();
            */
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

            dt.Columns.Add("CorrelationID");
            dt.Columns.Add("CorrelationName");
            dt.Columns.Add("SpearmansRho");

            foreach (var item in list)
            {
                var row = dt.NewRow();

                row["CorrelationID"] = item.CorrelationID;
                row["CorrelationName"] = item.CorrelationName;
                row["SpearmansRho"] = item.SpearmansRho;

                dt.Rows.Add(row);
            }

            return dt;
        }

        public  void MakeChart()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allBBPassRates = proxy.MyView();
            }

            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allBBPassRates);
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

            str.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('myChart'));  
                         chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");

            chartLit.Text = str.ToString() + "</script>";

        }

        public void AveragePerformanceInFiveGCSEs_Average_BBand_ByCounty()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allAPBItems = proxy.getAverages();
                var serilizer = new System.Web.Script.Serialization.JavaScriptSerializer();

            }

            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allAPBItems);

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

            //lt.Text = str.ToString() + "</script>";
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

            authPopSpeed.Text = str.ToString() + "</script>";
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

            authEmpSyncSpeed.Text = str.ToString() + "</script>";
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

        private void BindCorrelationToLabels()
        {
            //APBLabel.Text = GetApbCorrelation().ToString();
            //BbprLabel.Text = GetBbprCorrelation().ToString();
            //ApssLabel.Text = GetApssCorrelation().ToString();
            //AesLabel.Text = GetAesCorrelation().ToString();
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
            int xAxis = Int32.Parse(txtXAxis.Text);
            int yAxis1 = Int32.Parse(txtYAxis1.Text);
            int yAxis2 = Int32.Parse(txtYAxis2.Text);


            MakeComboChart(xAxis, yAxis1, yAxis2);
        }

        private void MakeComboChart(int xAxis, int yAxis1, int yAxis2)
        {

            List<string> filepaths = new List<string>();
            List<DataTable> allTables = new List<DataTable>();

            int rowCount = grdFiles.Rows.Count;

            for (int i = 0; i < rowCount; i++)
            {
                string filename = grdFiles.Rows[i].Cells[0].Text;
                filepaths.Add(Server.MapPath("~/Uploads/") + filename);
            }

            foreach (string path in filepaths)
            {
                DataTable dt = new DataTable();
                dt = GetDataTableFromCsv(path, true);
                allTables.Add(dt);
            }

            mergedTables = MergeData(allTables.ElementAt(0), allTables.ElementAt(1));

            StringBuilder str = new StringBuilder();

            str.Append(@"<script type = 'text/javascript'>
                      google.load('visualization', '1', { packages: ['corechart']});
            
                      function drawVisualization() {
                      var data = google.visualization.arrayToDataTable([
                       ['SchoolName','PercentPassed','AverageSpeed'],");

            int count = mergedTables.Rows.Count - 1;

            for (int i = 0; i <= count; i++)
            {
                if (count == i)
                {
                    str.Append("['"
                        + mergedTables.Rows[i]["SchoolName"].ToString()
                        + "',"
                        + mergedTables.Rows[i]["PassRate"].ToString()
                        + ","
                        + mergedTables.Rows[i]["AverageSpeed"].ToString()
                        + "]]);");
                }
                else
                {
                    str.Append("['"
                        + mergedTables.Rows[i]["SchoolName"].ToString()
                        + "',"
                         + mergedTables.Rows[i]["PassRate"].ToString()
                        + ","
                        + mergedTables.Rows[i]["AverageSpeed"].ToString()
                        + "],");
                }
            }


            str.Append("var options = {vAxes: {0: { format: ''},1: { format: '##'}},hAxis: { title: 'Week', format: 'm/d/y'},seriesType: 'bars',series:{ 0:{ type: 'bars', targetAxisIndex: 0 }, 1: { type: 'line', targetAxisIndex: 1}}, };");

            str.Append(@"var chart = new google.visualization.ComboChart(document.getElementById('myChart'));  
                         chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");

            chartLit.Text = str.ToString() + "</script>";
        }

        protected void btnMerge_OnClick(object sender, EventArgs e)
        {
            List<string> filepaths = new List<string>();
            List<DataTable> allTables = new List<DataTable>();
            
            int rowCount = grdFiles.Rows.Count;
            
            for (int i = 0; i < rowCount; i++)
            {
                string filename = grdFiles.Rows[i].Cells[0].Text;
                filepaths.Add(Server.MapPath("~/Uploads/") + filename);
            }

            foreach(string path in filepaths)
            {
                DataTable dt = new DataTable();
                dt = GetDataTableFromCsv(path, true);
                allTables.Add(dt);
            }

            mergedTables = MergeData(allTables.ElementAt(0), allTables.ElementAt(1));
            csvUploadResults.DataSource = mergedTables;
            csvUploadResults.DataBind();
        }

        public DataTable MergeData(DataTable dtFirst, DataTable dtSecond)
        {
            dtFirst.Columns.Add("LocalAuthority");
            dtFirst.Columns.Add("AverageSpeed");
            for (int i = 0; i < dtFirst.Rows.Count; i++)
            {
                dtFirst.Rows[i]["LocalAuthority"] = dtSecond.Rows[i]["LocalAuthority"];
                dtFirst.Rows[i]["AverageSpeed"] = dtSecond.Rows[i]["AverageSpeed"];
            }
            return dtFirst;
        }
    }
}