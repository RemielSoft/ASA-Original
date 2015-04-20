using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace StallionVisa
{
    public partial class WebForm1 : System.Web.UI.Page
    {


        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;
        SqlCommand cmd1 = null;
        DataTable dtBill = new DataTable();
        DataTable dtBillGenerate = new DataTable();


        protected void Page_Load(object sender, EventArgs e)
        {
            //ReportDocument report = new ReportDocument();
            
            //report.Load(Server.MapPath("Test.rpt"));
            
            

            //CrystalReportViewer1.ReportSource = report;
            int billId = 9; //Convert.ToInt32(Request.QueryString["billId"]);

            DataSet ds = new DataSet();
            con.Open();

            //Arvind
            cmd = new SqlCommand("select * from Bill where bill_id='" + billId + "' ", con);
            //SqlDataAdapter adp = new SqlDataAdapter(cmd);
            SqlDataReader dr = cmd.ExecuteReader();
            dtBill.Load(dr);
            dr.Close();
            //adp.Fill(ds, "Bill");
            //cmd.Dispose();

            cmd1 = new SqlCommand("select * from Bill_Detail where bill_id='" + billId + "' ", con);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            //SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            dtBillGenerate.Load(dr1);
            dr1.Close();
            //adp1.Fill(ds, "Bill_Detail");



            ReportDocument report = new ReportDocument();
            //report.Load(Server.MapPath("~/StallionVisaRpt.rpt"));
            //report.Load(Server.MapPath(@"reports\TrialBalancedup.rpt")); 
            report.Load(Server.MapPath("test.rpt"));
            //report.SetDataSource(ds.Tables["Bill"]);
            report.SetDataSource(dtBill);
            //report.Subreports["StallionVisaSubReport"].SetDataSource(dtBillGenerate);

            CrystalReportViewer1.ReportSource = report;
            // CrystalReportViewer1.DataBind();

            con.Close();
        }
    }
}