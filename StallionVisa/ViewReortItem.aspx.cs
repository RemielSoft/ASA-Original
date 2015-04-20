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
    public partial class ViewReortItem : System.Web.UI.Page
    {
        decimal Total_Quantity = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                txtFromDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                txtToDate.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                BindDropDown();
                btnExport.Visible = false;

            }

        }
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

        protected void btnViewBill_Click(object sender, EventArgs e)
        {
            if (rbtnitem.SelectedIndex == -1 || rbtnitem.SelectedValue == "1"||rbtnitem.SelectedValue=="4")
            {
                gridViewBill.Columns[5].Visible = true;
                gridViewBill.Columns[6].Visible = true;
            }
            else if (rbtnitem.SelectedValue == "2")
            {
                gridViewBill.Columns[5].Visible = true;
                gridViewBill.Columns[6].Visible = false;

            }
            else
            {
                gridViewBill.Columns[5].Visible = false;
                gridViewBill.Columns[6].Visible = true;
            }
            ViewBillBAL viewbal = new ViewBillBAL();
            int companyid = 0;
            int billid = 0;
            string itemname = null;
            if (!string.IsNullOrEmpty(rbtnitem.SelectedValue))
            {
                itemname = rbtnitem.SelectedItem.Text;

            }
            else
            {
                itemname = null;
            }


            companyid = Convert.ToInt32(ddlCmpnName.SelectedItem.Value);

            DateTime Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", null);
            DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", null);

            billid = Convert.ToInt32(null);
            showreports();

            if (gridViewBill.Rows.Count == 0)
            {
                btnExport.Visible = false;
            }
            else
            {
                btnExport.Visible = true;
            }








        }
        protected void gridViewBill_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            gridViewBill.PageIndex = e.NewPageIndex;

            showreports();


        }
        public void showreports()
        {
            ViewBillBAL viewbal = new ViewBillBAL();
            int companyid;

            int billid;
            string itemname;
            string subitem;
            if (!string.IsNullOrEmpty(rbtnitem.SelectedValue))
            {
                itemname = rbtnitem.SelectedItem.Text;

            }
            else
            {
                itemname = null;
            }
            companyid = Convert.ToInt32(ddlCmpnName.SelectedItem.Value);




            DateTime Fromdate = DateTime.ParseExact(txtFromDate.Text, "dd-MM-yyyy", null);
            DateTime Todate = DateTime.ParseExact(txtToDate.Text, "dd-MM-yyyy", null);
            subitem = null;
            billid = (0);
            gridViewBill.DataSource = viewbal.ShowBillReportsDetailsBal(Fromdate, Todate, billid, companyid, itemname, subitem);
            gridViewBill.DataBind();


        }

        protected void gridViewBill_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblQuantity = (Label)e.Row.FindControl("lblAmount");
                Total_Quantity += Convert.ToDecimal(lblQuantity.Text);
                Label lblPieces = (Label)e.Row.FindControl("lblpe");

                if (lblPieces.Text == "0")
                {
                    lblPieces.Text = "-";
                }
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //e.Row.Cells[0].ColumnSpan = 6;
                //e.Row.Cells.RemoveAt(6);
                //e.Row.Cells.RemoveAt(5);
                //e.Row.Cells.RemoveAt(4);
                //e.Row.Cells.RemoveAt(3);
                //e.Row.Cells.RemoveAt(2);
                //e.Row.Cells.RemoveAt(1);
                //e.Row.Cells.RemoveAt(0);
                e.Row.Cells[6].Text = "Total(INR)";
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Font.Bold = true;

                Label totalquantity = (Label)e.Row.FindControl("lblTotalQuantiy");
                totalquantity.Text = Total_Quantity.ToString();
            }
        }



        protected void gridViewBill_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            if (e.CommandName == "lnkDetails")
            {
                ViewBillBAL viewbal = new ViewBillBAL();
                List<Bill> lstbill = new List<Bill>();

                int billid = Convert.ToInt32(e.CommandArgument);
                Bill BILL = new Bill();
                lstbill = viewbal.ShowBillDetailsBal(DateTime.MinValue, DateTime.MinValue, -1, billid);
                // lstbill.Add(BILL);
                gvBillreports.DataSource = lstbill;
                gvBillreports.DataBind();
                Modalpopup.Show();
            }

        }

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
            showreports();

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