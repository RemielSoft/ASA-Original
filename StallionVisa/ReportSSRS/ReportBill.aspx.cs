using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;

namespace StallionVisa.ReportSSRS
{
    public partial class ReportBill : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string BId = Request.QueryString["billId"];
                ReportParameter[] repParam = new ReportParameter[1];
                repParam[0] = new ReportParameter("in_bill_id", BId);                
                rptBill.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Remote;
                rptBill.ServerReport.ReportServerUrl = new Uri(ConfigurationManager.AppSettings["ReportServerPath"].ToString());
                rptBill.ServerReport.ReportPath = "/ASAReport/4Report";
                rptBill.ServerReport.SetParameters(repParam);
                rptBill.ServerReport.Refresh();
            }

        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("/ViewBill.aspx");
        }
    }
}