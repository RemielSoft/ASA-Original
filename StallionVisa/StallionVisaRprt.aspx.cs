using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using DOM;
using BAL;
using System.Data.SqlClient;
using System.Configuration;

namespace StallionVisa
{
    public partial class StallionVisaRprt : System.Web.UI.Page
    {

        BillDetailBAL billDetailBL = new BillDetailBAL();
        CrystalReportBAL crystalReportBL = new CrystalReportBAL();
        List<BillDetail> lstBillDetail = new List<BillDetail>();
        List<Bill> lstBill = new List<Bill>();

        protected void Page_Load(object sender, EventArgs e)
        {
           
            if (Convert.ToInt32(Request.QueryString["billId"]) !=0)
            {
                BindRpt();
            
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Bill Id Doesn't Exist");
            }
            
        }

        private void BindRpt()
        {
           
            lstBillDetail = crystalReportBL.ReadCrystalReportDS(Convert.ToInt32(Request.QueryString["billId"]));
            lstBill = crystalReportBL.ReadBiLLDetailByBillId(Convert.ToInt32(Request.QueryString["billId"]));
            ReportDocument report1 = new ReportDocument();

            report1.Load(Server.MapPath("StallionVisaRpt.rpt"));

            report1.SetDataSource(lstBill);
            report1.Subreports["StallionVisaSubReport"].SetDataSource(lstBillDetail);

            CrystalReportViewer1.ReportSource = report1;
        }
    }
}