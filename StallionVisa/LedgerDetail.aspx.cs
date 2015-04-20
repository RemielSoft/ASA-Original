using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DOM;
using System.Data;


namespace StallionVisa
{
    public partial class LedgerDetail : System.Web.UI.Page
    {
        #region Global Variables

        CompanyMasterBAL companyMasterBAL = new CompanyMasterBAL();
        ViewBillBAL viewBillBL = new ViewBillBAL();
        LedgerBAL ledgerBL = new LedgerBAL();

        List<CompanyMasterDom> lstCompanyMaster = new List<CompanyMasterDom>();
        List<Bill> lstLedgerDetail = new List<Bill>();

        Bill bill = new Bill();

        #endregion

        #region Protected Event Methods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompanyDDL();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            bill = new Bill();
            bill.CompanyId = Convert.ToInt32(ViewState["companyID"]);
            bill.EntryType = ddlEntryType.SelectedItem.ToString();
            bill.EntryDetail = txtEntryDetail.Text;
            bill.Debit = Convert.ToDecimal(txtDebit.Text);
            
            viewBillBL.CreateLedger(bill);

            BindGrid(Convert.ToInt32(ViewState["companyID"]));
            lblTotalBal.Visible = true;
            lblTotalBalance.Visible = true;

            if (TotalCredit(Convert.ToInt32(ViewState["companyID"])) > TotalDebit(Convert.ToInt32(ViewState["companyID"])))
            {
                BalanceCalculate();

            }
            else
            {
                lblAdvance.Text = Convert.ToDecimal(TotalDebit(Convert.ToInt32(ViewState["companyID"])) - TotalCredit(Convert.ToInt32(ViewState["companyID"]))).ToString();
                lblAdvance.Visible = true;
                lblAdvancePayment.Visible = true;
                lblTotalBalance.Text = "0";
            }
            ClearFields();
            btnPrint.Visible = false;
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        protected void ddlCmpnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = Convert.ToInt32(ddlCmpnName.SelectedValue);
            ViewState["companyID"] = companyID;
            if (companyID > 0)
            {
                lblTotalBal.Visible = true;
                lblTotalBalance.Visible = true;
                lblAdvance.Visible = true;
                lblAdvancePayment.Visible = true;
                btnPrint.Visible = false;
                BindGrid(companyID);
                

                if (TotalCredit(Convert.ToInt32(ViewState["companyID"])) > TotalDebit(Convert.ToInt32(ViewState["companyID"])))
                {
                    BalanceCalculate();
                    lblAdvance.Visible = false;
                    lblAdvancePayment.Visible = false;

                }
                else
                {
                    lblAdvance.Text = Convert.ToDecimal(TotalDebit(Convert.ToInt32(ViewState["companyID"])) - TotalCredit(Convert.ToInt32(ViewState["companyID"]))).ToString();
                    if (lblAdvance.Text=="0")
                    {
                         lblAdvance.Visible = false;
                    lblAdvancePayment.Visible = false;
                    }
                    else
                    {
                        lblAdvance.Visible = true;
                        lblAdvancePayment.Visible = true;
                        lblTotalBalance.Text = "0";
                    }
                   
                }
            }
            else
            {
                gridLedger.DataSource = null;
                gridLedger.DataBind();
                lblTotalBal.Visible = false;
                lblTotalBalance.Visible = false;
                lblAdvance.Visible = false;
                lblAdvancePayment.Visible = false;
                btnPrint.Visible = false;
            }
           

        }

        protected void gridLedger_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblBillDate = (Label)e.Row.FindControl("lblGridDate");
                Label lblPaymentDate = (Label)e.Row.FindControl("lblPaymentDate");
                Label lblGridEntryType = (Label)e.Row.FindControl("lblGridEntryType");
                Label lblBillDetail = (Label)e.Row.FindControl("lblBillId");

                bill = (Bill)e.Row.DataItem;
                //if (bill.CreatedDate == DateTime.MinValue)
                //{
                //    lblBillDate.Text = "";
                //}
                //else
                //{
                //    lblBillDate.Text = bill.BillDate.ToString("dd-MMM-yyyy");
                //}
                //if (bill.PaymentDate == DateTime.MinValue)
                //{
                //    lblPaymentDate.Text = "";
                //}
                //else
                //{
                //    lblPaymentDate.Text = bill.BillDate.ToString("dd-MMM-yyyy");
                //}
                //if (bill.CreatedDate != DateTime.MinValue && bill.PaymentDate != DateTime.MinValue)
                //{
                //    lblPaymentDate.Text = "";
                //    lblBillDate.Text = bill.CreatedDate.ToString("dd-MMM-yyyy");
                //}
                if (String.IsNullOrEmpty(lblGridEntryType.Text.Trim()))
                {
                    lblBillDetail.Text = String.Concat("Bill number is ", lblBillDetail.Text);
                }
                else
                {
                    lblBillDetail.Text = String.Empty;
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            int companyId = 0;
            companyId = Convert.ToInt32(ViewState["companyID"]);
            Response.Redirect("LedgerDetailRpt.aspx?companyId=" + companyId);
        }

        #endregion

        #region Private Methods

        private void BindCompanyDDL()
        {
            lstCompanyMaster = companyMasterBAL.ReadCompanyDetails(0);
            if (lstCompanyMaster.Count > 0)
            {
                ddlCmpnName.DataSource = lstCompanyMaster;
                ddlCmpnName.DataTextField = "CompanyName";
                ddlCmpnName.DataValueField = "CompanyId";
                ddlCmpnName.DataBind();
                ddlCmpnName.Items.Insert(0, new ListItem("--Select--", "0"));
                ddlCmpnName.SelectedValue = "0";
            }
        }

        private decimal TotalDebit(int companyID)
        {
            decimal totalDebit = 0;
            lstLedgerDetail = viewBillBL.ReadLedgerByCompanyID(companyID);
            totalDebit = (lstLedgerDetail.Sum(Items => Items.Debit));


            return totalDebit;
        }

        private decimal TotalCredit(int companyID)
        {
            decimal totalCredit = 0;
            lstLedgerDetail = viewBillBL.ReadLedgerByCompanyID(companyID);
            totalCredit = (lstLedgerDetail.Sum(Items => Items.Credit));

            return totalCredit;
        }

        private void BindGrid(int companyID)
        {
            if (companyID > 0)
            {
                lstLedgerDetail = viewBillBL.ReadLedgerByCompanyID(companyID);
                if (lstLedgerDetail.Count > 0)
                {
                    gridLedger.DataSource = lstLedgerDetail;
                    gridLedger.DataBind();
                }
                else
                {
                    gridLedger.DataSource = null;
                    gridLedger.DataBind();
                    lblAdvance.Visible = false;
                    lblAdvancePayment.Visible = false;
                    lblTotalBal.Visible = false;
                    lblTotalBalance.Visible = false;
                    btnPrint.Visible = false;
                }

            }

        }

        private void ClearFields()
        {
            ddlEntryType.SelectedIndex = 0;
            txtEntryDetail.Text = string.Empty;
            txtDebit.Text = string.Empty;
        }

        private void BalanceCalculate()
        {

            lblTotalBalance.Text = Convert.ToDecimal(TotalCredit(Convert.ToInt32(ViewState["companyID"])) - TotalDebit(Convert.ToInt32(ViewState["companyID"]))).ToString();
        }

        #endregion

        protected void gridLedger_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gridLedger.PageIndex = e.NewPageIndex;
            BindGrid(Convert.ToInt32(ViewState["companyID"]));
        }

    }
}