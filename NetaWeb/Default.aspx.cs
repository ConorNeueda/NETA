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
        //Chart Array variables
        public average_performance_broadband[] allAPBItems;
        public BBandPassRate[] allBBPassRates;
        public AuthorityPop_SyncSpeed[] allAuthoritySpeedItems;
        public AuthorityEmployment_Speed[] allEmploymentSpeedItems;

        //Correlation instance variables
        private static decimal apbCorrelation;
        private static decimal bbprCorrelation;
        private static decimal apssCorrelation;
        private static decimal aesCorrelation;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Code associated with the csvUpload method. 
            //When the page is refreshed, the user's uploaded files will be displayed on screen.
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

            //Call methods to display graphs
            MakeChart();

            /*
            AveragePerformanceInFiveGCSEs_Average_BBand_ByCounty();
            AuthorityPopulation_SyncSpeed();
            AuthorityEmployment_SyncSpeed();
            BindCorrelationToLabels();
            */
        }

        //CREATE MERGED DATA TABLE FROM UPLOADED CSV FILES
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

        //METHODS FOR CONVERTING ARRAYS TO DATA TABLES
        //USED IN ORDER TO POPULATE GRAPHS
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
        }//convertToDT(BBandPassRate)

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
        }//convertToDT(Avg_performance_bband)

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
        }//convertToDT(AuthorityPop_Speed)

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
        }//convertToDT(AuthorityEmploymentSpeed)

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
        }//ConvertToDT(SpearmansRank)

        //METHODS FOR CHART POPULATION

        //Make chart from BBandPassRate items
        public  void MakeChart()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allBBPassRates = proxy.MyView();
            }

            //Create and initialise datatable
            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allBBPassRates);

            //Initialise StringBuilder to hold JavaScript text
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

            //Assign stringbuilder variable to <asp:literal>
            chartLit.Text = str.ToString() + "</script>";

        }

        public void AveragePerformanceInFiveGCSEs_Average_BBand_ByCounty()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allAPBItems = proxy.getAverages();
                var serilizer = new System.Web.Script.Serialization.JavaScriptSerializer();

            }

            //Create and initialise datatable
            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allAPBItems);

            //Initialise StringBuilder to hold JavaScript text
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

            //Assign stringbuilder variable to <asp:literal>
            //lt.Text = str.ToString() + "</script>";
        }//AvgPerformance_Bband_ByCounty

        public void AuthorityPopulation_SyncSpeed()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allAuthoritySpeedItems = proxy.getUptakeByAuthority();
                var serilizer = new System.Web.Script.Serialization.JavaScriptSerializer();
            }

            //Create and initialise datatable
            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allAuthoritySpeedItems);

            //Initialise StringBuilder to hold JavaScript text
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

            //Assign stringbuilder variable to <asp:literal>
            authPopSpeed.Text = str.ToString() + "</script>";
        }//AvgPop_SyncSpeed

        public void AuthorityEmployment_SyncSpeed()//AuthorityEmployment_SyncSpeed
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allEmploymentSpeedItems = proxy.GetAuthorityEmployment_Speed();
                var serilizer = new System.Web.Script.Serialization.JavaScriptSerializer();
        }
            //Create and initialise datatable
            DataTable dt = new DataTable();
            dt = ConvertToDatatable(allEmploymentSpeedItems);

            //Initialise StringBuilder to hold JavaScript text
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

            //Assign stringbuilder variable to <asp:literal>
            authEmpSyncSpeed.Text = str.ToString() + "</script>";
        }

        public static string Serialize(object o)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(o);
        }

        //GET CORRELATION METHODS
        //EACH IS ASSOCIATED WITH A PARTICULAR GRAPH
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

        //METHOD TO ASSIGN EACH CORRELATION LABEL TO A SPEARMAN'S RANK TEST RESULT
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

        }

        protected void btnMerge_OnClick(object sender, EventArgs e)
        {
            List<string> filepaths = new List<string>();
            List<DataTable> allTables = new List<DataTable>();
            DataTable mergedTables = new DataTable();
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