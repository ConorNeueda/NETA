using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NetaWeb
{
    public partial class Upload : System.Web.UI.Page
    {

        public DataTable mergedTables = new DataTable();

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

            foreach (string path in filepaths)
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