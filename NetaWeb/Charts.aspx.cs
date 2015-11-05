using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NetaWeb.NetaServices;
using System.Data;
using System.Web.Script.Serialization;
using System.Text;

namespace NetaWeb
{
    public partial class Charts : System.Web.UI.Page
    {
        public average_performance_broadband[] allItems;
        public BBandPassRate[] allBBandPRItems;
        public AuthorityPop_SyncSpeed[] allAuthoritySpeedItems;
        public AuthorityEmployment_Speed[] allEmploymentSpeedItems;
        public SpearmansRank[] allSRItems;

        private static decimal apbCorrelation;
        private static decimal bbprCorrelation;
        private static decimal apssCorrelation;
        private static decimal aesCorrelation;

        protected void Page_Load(object sender, EventArgs e)
        {
            PassRatesBroadband();
            AveragePerformanceInFiveGCSEs_Average_BBand_ByCounty();
            AuthorityEmployment_SyncSpeed();
            AuthorityPopulation_SyncSpeed();
            BindCorrelationToLabels();

        }

        public void PassRatesBroadband()
        {
            using (NetaServiceClient proxy = new NetaServiceClient())
            {
                allBBandPRItems = proxy.MyView();
                var serilizer = new System.Web.Script.Serialization.JavaScriptSerializer();
            }

            DataTable dt = new DataTable();

            dt = ConvertToDatatable(allBBandPRItems);


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


            str.Append("var options = {vAxes: {0: { title: 'Percent', format: ''},1: { title: 'Average Speed (mbps)', format: '##'}},hAxis: { title: 'School Name', format: ''},seriesType: 'bars',series:{ 0:{ type: 'bars', targetAxisIndex: 0}, 1: { type: 'line', targetAxisIndex: 1}}, };");

            str.Append(" var chart = new google.visualization.ComboChart(document.getElementById('chart_div'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");

            bbprScripts.Text = str.ToString() + "</script>";
        }//PassRatesBroadband

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

            apbLiteral.Text = str.ToString() + "</script>";
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

            ApssLiteral.Text = str.ToString() + "</script>";
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

            AesLiteral.Text = str.ToString() + "</script>";
        }

        //METHOD TO CREATE DATATABLE ITEMS FOR CHARTS

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

        //DATATABLE FOR SPEARMANS RANK CORRELATION VALUES

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
        }//concertToDT

        public static string Serialize(object o)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            return js.Serialize(o);
        }

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
            APBLabel.Text = GetApbCorrelation().ToString();
            BbprLabel.Text = GetBbprCorrelation().ToString();
            ApssLabel.Text = GetApssCorrelation().ToString();
            AesLabel.Text = GetAesCorrelation().ToString();
        }
    }
}