using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using DOM;
using System.Linq;
using BAL;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System;
using System.Collections.Generic;


namespace StallionVisa
{
    public partial class LedgerDetailRpt : System.Web.UI.Page
    {
        ViewBillBAL viewBillBL = new ViewBillBAL();
        CrystalReportBAL crBL = new CrystalReportBAL();
        List<Bill> lstBillDetail = new List<Bill>();
        List<Bill> lstCmpDetail = new List<Bill>();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Convert.ToInt32(Request.QueryString["companyId"]) != 0)
            {
                BindRpt();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Company Id Doesn't Exist");
            }
        }

        private void BindRpt()
        {
            lstBillDetail = viewBillBL.ReadLedgerByCompanyID(Convert.ToInt32(Request.QueryString["companyId"]));
            
            if (lstBillDetail.Count>0)
            {
                Bill ob = new Bill();
                decimal totalCredit = lstBillDetail.Sum(Items => Items.Credit);
                decimal totalDebit = (lstBillDetail.Sum(Items => Items.Debit));
                if ((totalCredit - totalDebit) < 0)
                {
                    ob.TotalAmount = -(totalCredit - totalDebit); 
                }
                else
                {
                    ob.GrandTotal = totalCredit - totalDebit;
                }
               
               foreach (Bill item in lstBillDetail)
               {
                   if ((!string.IsNullOrEmpty(item.CompanyName)) && (!string.IsNullOrEmpty(item.CompanyAddress)))
                   {
                       ob.CompanyName = item.CompanyName;
                       ob.CompanyAddress = item.CompanyAddress;
                       break;
                   }
               }
               lstCmpDetail.Add(ob);
               

               ReportDocument report1 = new ReportDocument();

               report1.Load(Server.MapPath("LedgerRpt.rpt"));

               report1.SetDataSource(lstCmpDetail);
               report1.Subreports["LedgerSubReport"].SetDataSource(lstBillDetail);

               CrystalReportViewer1.ReportSource = report1;

            }

        }

    }
}