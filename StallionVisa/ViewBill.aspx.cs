using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DOM;
using BAL;
using System.IO;
using System.Text;


namespace StallionVisa
{
    public partial class About : System.Web.UI.Page
    {
        ViewBillBAL billBal = new ViewBillBAL();
        Bill billDom = new Bill();
        RegistrationDOM registrationDOM = null;
        int billId;

        #region Protected Method
        protected void Page_Load(object sender, EventArgs e)
        {
            
            
           // ShowBill(billDom);    
            if (!IsPostBack)
            {
               
               
                
                //txtFromDate.Text = DateTime.Now.Date.ToShortDateString();
                //txtToDate.Text = DateTime.Now.Date.ToShortDateString();
                txtFromDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                txtToDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                BindDropDown();
                ShowBill(DateTime.Now.Date, DateTime.Now.Date,-1, 0);
                
                if (gridViewBill.Rows.Count == 0)
                {
                    btnExport.Visible = false;
                }
                else { btnExport.Visible = true; }
            }
        }

        protected void btnViewBill_Click(object sender, EventArgs e)
        {
            DateTime Fromdate;
            DateTime Todate;
            //billDom.FromDate = Convert.ToDateTime(txtFromDate.Text);
            //billDom.ToDate = Convert.ToDateTime(txtToDate.Text);
            //billDom.CompanyId =Convert.ToInt32(ddlCmpnName.SelectedValue);
            if (txtbillid.Text == string.Empty)
            {
                 billId = 0;
            }
            else
            {
                billId = Convert.ToInt32(txtbillid.Text);
            }
            //DateTime Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", null);
           // DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", null);
            string FDate = txtFromDate.Text;
            Fromdate = DateTime.ParseExact(FDate, "dd/MM/yyyy", null);
            string TDate = txtToDate.Text;
            Todate = DateTime.ParseExact(TDate, "dd/MM/yyyy", null);
          
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text))
            {
                Todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            if (!string.IsNullOrEmpty(txtFromDate.Text) && !string.IsNullOrEmpty(txtToDate.Text))
            {
                Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                Todate = DateTime.ParseExact(txtToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }

            ShowBill(Fromdate, Todate, Convert.ToInt32(ddlCmpnName.SelectedValue), billId);
            if (gridViewBill.Rows.Count == 0)
            {
                btnExport.Visible = false;
            }
            else { btnExport.Visible = true; }

        }

        protected void gridViewBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName=="print")
            {
                int billId = Convert.ToInt32(e.CommandArgument);
                Response.Redirect("/ReportSSRS/ReportBill.aspx?billId=" + billId);
            }

            if (e.CommandName == "editt")
            {
                int billId = Convert.ToInt32(e.CommandArgument);
                
                Response.Redirect("GenerateBill.aspx?billId=" + billId);
            }

        }

        protected void gridViewBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            
            gridViewBill.PageIndex = e.NewPageIndex;


            ShowBill(Convert.ToDateTime(txtFromDate.Text), Convert.ToDateTime(txtToDate.Text), Convert.ToInt32(ddlCmpnName.SelectedValue), billId);
           
        }

        protected void gridViewBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            registrationDOM = new RegistrationDOM();
            registrationDOM = (RegistrationDOM)HttpContext.Current.Session["USER_SESSION"];

            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                LinkButton lnkButton = (LinkButton)e.Row.FindControl("LinkButton1");
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {

                    //if (String.Equals(registrationDOM.Role.ToUpper(), "admin"))
                    //{
                        lnkButton.Visible = true;
                    //}
                   // else
                    //{
                        //lnkButton.Visible = false;
                    //}
                }
            }
        }
        #endregion

        #region Private Method
        private void BindDropDown()
        {
            List<CompanyMasterDom> lstCompanyMasterDom = new List<CompanyMasterDom>();
            CompanyMasterBAL companyMasterBAL = new CompanyMasterBAL();
            ddlCmpnName.DataTextField = "CompanyName";
            ddlCmpnName.DataValueField = "CompanyId";
            lstCompanyMasterDom = companyMasterBAL.ReadCompanyDetails(null);
            ddlCmpnName.DataSource = lstCompanyMasterDom;
            ddlCmpnName.DataBind();
            ListItem item = new ListItem("All", "-1");
            ddlCmpnName.Items.Insert(0, item);
        }

        private void ShowBill(DateTime fromDate, DateTime toDate, Int32? companyId, int? bill_Id)
        {
            List<Bill> lst=new List<Bill>();
            lst = billBal.ShowBillDetailsBal(fromDate, toDate, companyId, bill_Id);
            gridViewBill.DataSource=lst ;
            gridViewBill.DataBind();
            if (Master.LoggedInUser.Role.ToUpper() != "ADMIN")
            {
                foreach (GridViewRow row in gridViewBill.Rows)
                {
                    LinkButton lnk = (LinkButton)row.FindControl("LinkButton1");
                    lnk.Visible = false;
                }
            }
            
            
            
        }

        #endregion

        protected void btnExport_Click(object sender, EventArgs e)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.AddHeader("content-disposition", "attachment;filename=BillReport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.xls";
            StringBuilder sb = new StringBuilder();
            StringWriter stringWriter = new StringWriter(sb);
            HtmlTextWriter htm = new HtmlTextWriter(stringWriter);
            gridViewBill.AllowPaging = false;
            if (txtbillid.Text == string.Empty)
            {
                billId = 0;
            }
            else
            {
                billId = Convert.ToInt32(txtbillid.Text);
            }
            DateTime Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", null);
            DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", null);

            ShowBill(Fromdate,Todate, Convert.ToInt32(ddlCmpnName.SelectedValue), billId);
            gridViewBill.Columns[12].Visible = false;
            gridViewBill.FooterRow.HorizontalAlign = HorizontalAlign.Right;
            // gvDispatch.HeaderRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;

            gridViewBill.RenderControl(htm);
            Response.Write(stringWriter);
            Response.End();
            btnExport.Visible = false;
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }


    }
}
