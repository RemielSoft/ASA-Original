using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BAL;
using DOM;
//using System.Windows.Forms;

namespace StallionVisa
{
    public partial class _Default : System.Web.UI.Page
    {
        #region Global Variables

        Decimal amount = 0;
        Decimal quantity = 0;
        Decimal rate = 0;

        Decimal grandTotal = 0;
        Decimal serviceTax = 0;
        Decimal serviceCharge = 0;

        List<BillDetail> lstBillDetail = new List<BillDetail>();
        List<Bill> lstBil = new List<Bill>();
        BillDetailBAL obBal = new BillDetailBAL();
        ViewBillBAL obViewBillBL = new ViewBillBAL();

        Bill bill = null;

        decimal cst;
        decimal vat;
        decimal tcs;
        decimal freight;
        decimal ECESS;
        decimal Shecess;

        #endregion

        #region Protected Method

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                otherText.Visible = false;
                Session["tempdata"] = null;
                tdsubitem.Visible = false;
             //   lblPK.Visible = false;
                rbtnsubitem.ClearSelection();
                tdsubitemradio.Visible = false;
                BindDropDown();
                int billid = Convert.ToInt32(Request.QueryString["billId"]);
                ddlCmpnName.Enabled = true;
                txtCorporate.Enabled = true;
                txtName.Enabled = true;
                if (billid != 0)
                {
                    ddlCmpnName.Enabled = false;
                    txtCorporate.Enabled = false;
                    txtName.Enabled = false;

                    ViewState["billId"] = billid;
                    ViewBillByBillId(billid);
                    ViewBillDetailByBillId(billid);
                }
                txtRate.Attributes.Add("OnBlur", "return fnValidrate()");

            }
            //btnPrint.Visible = false;

        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            BillDetail billDetail = new BillDetail();
            int id;
            id = Convert.ToInt32(ddlitem.SelectedItem.Value);
            // billDetail.ItemDescription = txtItemDesc.Text.Trim();
            if (id <= 0)
            {
                billDetail.ItemDescription = txtOther.Text;

            }
            else
            {
                billDetail.ItemDescription = ddlitem.SelectedItem.Text;
            }
            billDetail.Quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
            billDetail.Rate = Convert.ToDecimal(txtRate.Text.Trim());
            billDetail.Amount = CalculateAmount();

            billDetail.measrment = ViewState["measrument"].ToString();
            billDetail.BoxMeasrument = ViewState["boxmeasurment"].ToString();

            billDetail.PiecesMeasrument = ViewState["Piecesmeasurment"].ToString();

            if (!string.IsNullOrEmpty(txtbox.Text))
            {
                billDetail.Box = Convert.ToInt32(txtbox.Text);
            }
            if (!string.IsNullOrEmpty(txtpieces.Text))
            {
                billDetail.Pieces = Convert.ToInt32(txtpieces.Text);
            }
            if (ViewState["itemname"] == null)
            {
                billDetail.ItemName = "Other";
            }
            else
            {
                billDetail.ItemName = ViewState["itemname"].ToString();
            }

            billDetail.CreatedBy = "Admin";
            billDetail.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("d"));

            List<BillDetail> lstData = (List<BillDetail>)Session["tempdata"];
            if (lstData != null)
            {
                lstData.Add(billDetail);
                Session["tempdata"] = lstData;
            }
            else
            {
                lstBillDetail.Add(billDetail);
                Session["tempdata"] = lstBillDetail;
            }

            BindGrid();

            ClearItems();

            txtserTax.Text = string.Empty;
            txtGTotal.Text = string.Empty;

            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            ProductMasterBAL productMasterBAL = new ProductMasterBAL();
            if (id <= 0)
            {
                ViewState["productitem"] = 0;
            }
            else
            {
                lstproductMasterDom = productMasterBAL.ReadProductDetails(id, 0, 0);

                ViewState["productitem"] = lstproductMasterDom[0].ItemId;
            }

            Readservicetax();
            CalculateGrandtotal();


        }



        protected void btn_Save_Click(object sender, EventArgs e)
        {
            CalculateGrandtotal();
            SaveData();
            ClearAllData();
            Response.Redirect("GenerateBill.aspx");


        }



        protected void btnPrint_Click(object sender, EventArgs e)
        {

            int billId = SaveData();
            // btnPrint.Attributes.Add("onclick", 
            if (billId != 0)
            {
                Response.Redirect("/ReportSSRS/ReportBill.aspx?billId=" + billId);
            }
            else
            {
                Response.Redirect("/ReportSSRS/ReportBill.aspx?billId=" + Convert.ToInt32(ViewState["billId"]));
                ViewState["billId"] = null;
            }
        }

        protected void txtRate_TextChanged(object sender, EventArgs e)
        {
            
            txtAmount.Text = CalculateAmount().ToString();
            if (string.IsNullOrEmpty(txtQuantity.Text))
            {
                txtQuantity.Focus();
            }

        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            txtAmount.Text = CalculateAmount().ToString();
            if (string.IsNullOrEmpty(txtRate.Text))
            {
                txtRate.Focus();
            }

        }



        protected void ddlCmpnName_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<CompanyMasterDom> companyMasterDom = new List<CompanyMasterDom>();

            CompanyMasterBAL companyMasterBAL = new BAL.CompanyMasterBAL();
            int id = Convert.ToInt32(ddlCmpnName.SelectedValue);
            if (id > 0)
            {
                int vatandcstid;
                companyMasterDom = companyMasterBAL.ReadCompanyDetails(id);
                txtAddress.Text = companyMasterDom[0].CompanyAddress.ToString();
                vatandcstid = companyMasterDom[0].chargesId;
                ViewState["vatandcstid"] = vatandcstid;
                if (vatandcstid == 1)
                {
                    lblcst.Visible = true;
                    lblvat.Visible = false;
                }
                else
                {
                    lblcst.Visible = false;
                    lblvat.Visible = true;
                }

                //companyMasterDom = new List<CompanyMasterDom>();
                //companyMasterDom = companyMasterBAL.BindRadioButton(vatandcstid);
                //txtvat.Text = (companyMasterDom[0].Chargesvalue).ToString();


            }
            else if (id == -1)
            {
                txtAddress.Text = null;
            }


        }
        public void Readservicetax()
        {
            int Itemid = 0;
            int taxid = 0;

            Itemid = Convert.ToInt32(ViewState["productitem"]);
            BillDetailBAL Billob = new BillDetailBAL();
            Bill BillDom = new Bill();
            List<Bill> lstbill = new List<Bill>();
            if (Itemid == 3)
            {
                taxid = 1;

            }

            else
            {
                taxid = 2;
            }

            lstbill = Billob.ReadSexvicestax(taxid);
            txtserTax.Text = lstbill[0].ServiceTax.ToString();
            txtecess.Text = lstbill[0].E_cess.ToString();
            txtshecess.Text = lstbill[0].SHE_cess.ToString();
            txtTCS.Text = lstbill[0].TCS.ToString();
            if (Itemid == 0)
            {
                txtserTax.Text = "0";
                txtecess.Text = "0";
                txtshecess.Text = "0";
                txtTCS.Text = "0";

            }


            List<CompanyMasterDom> companyMasterDom = new List<CompanyMasterDom>();
            CompanyMasterBAL companyMasterBAL = new CompanyMasterBAL();
            int id = Convert.ToInt32(ViewState["vatandcstid"]);
            companyMasterDom = companyMasterBAL.BindRadioButton(id);
            txtvat.Text = (companyMasterDom[0].Chargesvalue).ToString();




        }

        protected void gvItemDesc_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvItemDesc.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        #endregion

        #region private Method

        private decimal CalculateAmount()
        {
            if (!string.IsNullOrEmpty(txtQuantity.Text.Trim()) && !string.IsNullOrEmpty(txtRate.Text.Trim()))
            {

                
                quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
                
                rate = Convert.ToDecimal(txtRate.Text.Trim());
                amount = quantity * rate;


            }
            return amount;
        }

        private void Calculate_TotalAmt()
        {
            if (Session["tempdata"] != null)
            {
                List<BillDetail> lstData = (List<BillDetail>)Session["tempdata"];
                txtTotal.Text = (lstData.Sum(Items => Items.Amount)).ToString();
            }
        }

        private decimal CalculateServiceTax()
        {
            try
            {
                Decimal totalAmount = 0;
                if (!string.IsNullOrEmpty(txtserTax.Text.Trim()))
                {
                    serviceTax = Convert.ToDecimal(txtserTax.Text.Trim());
                }
                //if (!String.IsNullOrEmpty(txtserCharge.Text))
                //{
                //    serviceCharge = Convert.ToDecimal(txtserCharge.Text.Trim());
                //}
                if (!String.IsNullOrEmpty(txtTotal.Text))
                {
                    totalAmount = Convert.ToDecimal(txtTotal.Text);
                }

                grandTotal = totalAmount + (totalAmount * serviceTax) / 100 + serviceCharge;
                txtGTotal.Text = grandTotal.ToString();

                return grandTotal;
            }
            catch (Exception exp)
            {
                return 0;
            }

        }
        private decimal CalculateGrandtotal()
        {

            try
            {
                Decimal totalAmount = 0;
                if (!string.IsNullOrEmpty(txtserTax.Text.Trim()))
                {
                    serviceTax = Convert.ToDecimal(txtserTax.Text.Trim());
                }

                if (!String.IsNullOrEmpty(txtTotal.Text))
                {
                    totalAmount = Math.Round(Convert.ToDecimal(txtTotal.Text), 0);
                }
                //if (!String.IsNullOrEmpty(txtcst.Text))
                //{
                //    cst = Convert.ToDecimal(txtcst.Text);
                //}
                if (!String.IsNullOrEmpty(txtvat.Text))
                {
                    vat = Convert.ToDecimal(txtvat.Text);
                }
                if (!String.IsNullOrEmpty(txtTCS.Text))
                {
                    tcs = Convert.ToDecimal(txtTCS.Text);
                }
                if (!String.IsNullOrEmpty(txtFreight.Text))
                {
                    freight = Convert.ToDecimal(txtFreight.Text);
                }
                if (!String.IsNullOrEmpty(txtecess.Text))
                {
                    ECESS = Convert.ToDecimal(txtecess.Text);
                }
                if (!String.IsNullOrEmpty(txtshecess.Text))
                {
                    Shecess = Convert.ToDecimal(txtshecess.Text);
                }
                int ProductId;
                ProductId = Convert.ToInt32(ViewState["productitem"]);



               
                decimal servicecharge;
                decimal vat1;
                decimal totaltcs;
                decimal ECESS1;
                decimal SHECESS;
                servicecharge = Math.Round(totalAmount * serviceTax / 100, 0, MidpointRounding.AwayFromZero);
                ECESS1 = Math.Round(servicecharge * ECESS / 100, 0, MidpointRounding.AwayFromZero);
                SHECESS = Math.Round(servicecharge * Shecess / 100, 0, MidpointRounding.AwayFromZero);
                vat1 = Math.Round(totalAmount + servicecharge + ECESS1 + SHECESS, 0, MidpointRounding.AwayFromZero);
                txttotaltax.Text = Math.Round(vat1, 0).ToString();

                totaltcs = Math.Round(vat1 + (vat1 * vat / 100), 0, MidpointRounding.AwayFromZero);
                grandTotal = totaltcs + (totaltcs * tcs / 100) + freight;
                txtGTotal.Text = Math.Round(grandTotal, 0, MidpointRounding.AwayFromZero).ToString();
                return grandTotal;
              


            }
            catch (Exception exp)
            {
                return 0;
            }

        }

        private void BindGrid()
        {
            if (Session["tempdata"] != null)
            {
                List<BillDetail> lstData = (List<BillDetail>)Session["tempdata"];
                gvItemDesc.DataSource = lstData;
                gvItemDesc.DataBind();

                Calculate_TotalAmt();
            }

        }

        private void ClearItems()
        {
            //txtItemDesc.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            txtbox.Text = "";
            txtpieces.Text = "";
            tdsubitem.Visible = false;
            rbtnsubitem.ClearSelection();
            tdsubitemradio.Visible = false;
            rbtnitem.ClearSelection();
            //txtItemDesc.Focus();
            txtOther.Text = "";
        }

        private void ClearAllData()
        {

            ddlCmpnName.SelectedIndex = 0;
            ddlCmpnName.Enabled = true;
            txtAddress.Text = string.Empty;
            txtCorporate.Text = string.Empty;
            txtName.Text = string.Empty;
            txtserTax.Text = string.Empty;
            // txtserCharge.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txtGTotal.Text = string.Empty;
            //ddlitem.ClearSelection();
            //  txtItemDesc.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtAmount.Text = string.Empty;
            gvItemDesc.DataSource = null;
            gvItemDesc.DataBind();
            Session["tempdata"] = null;
            txtTCS.Text = string.Empty;
            txtshecess.Text = string.Empty;
            txtshecess.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtvat.Text = string.Empty;
            txtFreight.Text = string.Empty;
            txttotaltax.Text = string.Empty;
            txtecess.Text = string.Empty;
            ViewState["vatandcstid"] = null;
            tdsubitem.Visible = false;
            rbtnsubitem.ClearSelection();
            tdsubitemradio.Visible = false;
            ViewState["itemname"] = null;
        }

        private int SaveData()
        {

            int id = 0;
            Bill bill = new Bill();
            bill.BillDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            bill.CompanyId = Convert.ToInt32(ddlCmpnName.SelectedValue);
            bill.CompanyAddress = txtAddress.Text.Trim();
            bill.Corporate = txtCorporate.Text.Trim();
            bill.ClientName = txtName.Text.Trim();
            bill.CstVatid = Convert.ToInt32(ViewState["vatandcstid"]);

            if (!string.IsNullOrEmpty(txtVehicleNo.Text))
            {
                bill.Vehicle = txtVehicleNo.Text;
            }
            else
            {
                bill.Vehicle = "0";
            }
            if (!string.IsNullOrEmpty(txtecess.Text))
            {
                bill.E_cess = Convert.ToDecimal(txtecess.Text.Trim());
            }

            if (!string.IsNullOrEmpty(txtshecess.Text))
            {
                bill.SHE_cess = Convert.ToDecimal(txtshecess.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtvat.Text))
            {
                bill.CstVATValue = Convert.ToDecimal(txtvat.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtFreight.Text))
            {
                bill.Freight = Convert.ToDecimal(txtFreight.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtTCS.Text))
            {

                bill.TCS = Convert.ToDecimal(txtTCS.Text.Trim());
            }

            if (!string.IsNullOrEmpty(txtTotal.Text))
            {
                bill.TotalAmount = Convert.ToDecimal(txtTotal.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtserTax.Text))
            {
                bill.ServiceTax = Convert.ToDecimal(txtserTax.Text.Trim());
            }

            if (!string.IsNullOrEmpty(txttotaltax.Text))
            {
                bill.TotalWithTax = Convert.ToDecimal(txttotaltax.Text.Trim());
            }
            if (!string.IsNullOrEmpty(txtGTotal.Text))
            {
                bill.GrandTotal = Convert.ToDecimal(txtGTotal.Text.Trim());
            }
            else
            {
                if (!string.IsNullOrEmpty(txtTotal.Text))
                {
                    bill.GrandTotal = Convert.ToDecimal(txtTotal.Text.Trim());
                }
            }
            bill.CreatedBy = "Admin";
            bill.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("d"));

            List<BillDetail> lstData = new List<BillDetail>();

            lstData = (List<BillDetail>)Session["tempdata"];
            if (lstData != null)
            {
                if (ViewState["billId"] != null)
                {
                    bill.BillId = Convert.ToInt32(ViewState["billId"]);
                    obViewBillBL.UpdateBillById(bill);
                    obBal.AddItems(lstData, Convert.ToInt32(ViewState["billId"]));
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", "Bill is Updated Successfully"), true);
                    //Response.Redirect("GenerateBill.aspx");



                }
                else
                {

                    id = obBal.SaveBill(bill);
                    Session["billId"] = id;
                    obBal.AddItems(lstData, id);
                    //MessageBox.Show("Your Generated Bill id is " + id.ToString());
                    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", "Your Generated Bill id is " + id.ToString()), true);

                }
                ClearAllData();
            }
            return id;

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
            ListItem item = new ListItem("--Select--", "-1");
            ddlCmpnName.Items.Insert(0, item);
        }
        private void BindDropDownItemDescription(int? id, int? itemid, int? subitem)
        {
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            ProductMasterBAL productMasterBAL = new ProductMasterBAL();
            // ddlitem.DataTextField = "ItemDescription" ;

            ddlitem.DataTextField = "AllItem";
            ddlitem.DataValueField = "ProductId";
            lstproductMasterDom = productMasterBAL.ReadProductDetails(0, itemid, subitem);
            ddlitem.DataSource = lstproductMasterDom;
            ddlitem.DataBind();
            ListItem item = new ListItem("--Select--", "-1");
            ddlitem.Items.Insert(0, item);

        }


        private void ViewBillByBillId(int billId)
        {

            bill = new Bill();
            bill = obViewBillBL.ReadBillById(billId);
            if (bill != null)
            {


               ddlCmpnName.SelectedValue = bill.CompanyId.ToString();
               // ddlCmpnName.Text = bill.CompanyName;
                txtAddress.Text = bill.CompanyAddress;
                txtCorporate.Text = bill.Corporate;
                txtName.Text = bill.ClientName;
                txtTotal.Text = bill.TotalAmount.ToString();
                txtserTax.Text = bill.ServiceTax.ToString();
                txtGTotal.Text = bill.GrandTotal.ToString();
                txttotaltax.Text = bill.TotalWithTax.ToString();
                txtvat.Text = bill.CstVATValue.ToString();
                txtVehicleNo.Text = bill.Vehicle;
                txtecess.Text = bill.E_cess.ToString();
                txtshecess.Text = bill.SHE_cess.ToString();
                txtTCS.Text = bill.TCS.ToString();
                txttotaltax.Text = bill.TotalWithTax.ToString();
                txtFreight.Text = bill.Freight.ToString();

                ViewState["vatandcstid"] = bill.CstVatid;
                if (Convert.ToInt32(ViewState["vatandcstid"]) == 1)
                {
                    lblcst.Visible = true;
                    lblvat.Visible = false;
                }
                else
                {
                    lblcst.Visible = false;
                    lblvat.Visible = true;
                }

            }
        }

        private void ViewBillDetailByBillId(int billId)
        {
            //int billid = Convert.ToInt32(Request.QueryString["billId"]);
            lstBillDetail = obBal.ReadBillDetailByBillId(billId);
            Session["tempdata"] = lstBillDetail;

            if (lstBillDetail != null)
            {
                gvItemDesc.DataSource = lstBillDetail;
                gvItemDesc.DataBind();
            }
        }

        #endregion

        protected void ddlitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int ProductId;
            ProductId = Convert.ToInt32(ddlitem.SelectedItem.Value);

            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            ProductMasterBAL productMasterBAL = new ProductMasterBAL();
            lstproductMasterDom = productMasterBAL.ReadProductDetails(ProductId, 0, 0);
            ViewState["itemname"] = lstproductMasterDom[0].ItemName;
            if (lstproductMasterDom[0].ItemId == 1)
            {
               // lblPK.Visible = false;
                lblkg.Visible = true;
                lblquantity.Visible = false;
                ViewState["measrument"] = lblkg.Text;
                ViewState["boxmeasurment"] = "Box";
                ViewState["Piecesmeasurment"] = lblquantity.Text;

                tdpieces.Visible = true;
                tdtcs.Visible = false;
            }
            else if (lstproductMasterDom[0].ItemId == 2)
            {
                //lblPK.Visible = false;
                lblquantity.Visible = true;
                lblkg.Visible = false;
                tdtcs.Visible = false;
                tdpieces.Visible = false;
                ViewState["measrument"] = lblquantity.Text;
                ViewState["boxmeasurment"] = "Box";
                ViewState["Piecesmeasurment"] = lblquantity.Text;


            }




            else if (lstproductMasterDom[0].ItemId == 2 && lstproductMasterDom[0].ItemId == 1)
            {
                ViewState["boxmeasurment"] = "Box";
                ViewState["Piecesmeasurment"] = lblquantity.Text;

            }
            else
            {
                //lblPK.Visible = false;
                lblkg.Visible = true;
                lblquantity.Visible = false;
                tdtcs.Visible = true;
                ViewState["measrument"] = lblkg.Text;
                ViewState["boxmeasurment"] = "Box";
                ViewState["Piecesmeasurment"] = lblquantity.Text;
                tdpieces.Visible = false;
            }

        }

        protected void txtFreight_TextChanged(object sender, EventArgs e)
        {
            CalculateGrandtotal();

        }

        protected void rbtnitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int itemId;
            int subitemid = 0;
            itemId = Convert.ToInt32(rbtnitem.SelectedItem.Value);
            if (rbtnitem.SelectedValue == "1")
            {
                tdsubitem.Visible = true;
                tdsubitemradio.Visible = true;
                otherText.Visible = false;
                ddlHide.Visible = true;
               // lblPK.Visible = false;
                chkkg.Checked = false;
                chkkg.Visible = false;
                txtOther.Text = "";
                //  subitemid =Convert.ToInt32( rbtnsubitem.SelectedItem.Value);

            }
            else if (rbtnitem.SelectedValue == "6")
            {
                ViewState["measrument"] = null;
                ddlHide.Visible = false;
                otherText.Visible = true;
               // lblPK.Visible = true;
                chkkg.Checked = false;
                tdtcs.Visible = false;
                tdpieces.Visible = false;
                txtpieces.Text = "";
                lblquantity.Visible = true;
                lblkg.Visible = false;
                chkkg.Visible = true;
                ViewState["measrument"] = lblquantity.Text;
                ViewState["boxmeasurment"] = "Box";
                ViewState["Piecesmeasurment"] = "0";
                tdsubitemradio.Visible = false;
                tdsubitem.Visible = false;
                rbtnsubitem.ClearSelection();
               
            }
            else if (rbtnitem.SelectedValue == "3")
            {
                lblquantity.Visible = false;
                lblkg.Visible = true;
                tdsubitemradio.Visible = false;
                rbtnsubitem.ClearSelection();
                tdsubitem.Visible = false;
             //   lblPK.Visible = false;
                tdpieces.Visible = false;
                txtpieces.Text = "";
                chkkg.Checked = false;
                chkkg.Visible = false;
                ddlHide.Visible = true;
                txtOther.Text = "";
                otherText.Visible = false;
            }
            else
            {
                tdsubitem.Visible = false;
                tdsubitemradio.Visible = false;
                rbtnsubitem.ClearSelection();
                subitemid = 0;
                otherText.Visible = false;
                txtOther.Text = "";
                ddlHide.Visible = true;
               // lblPK.Visible = false;
                lblkg.Visible = true;
                tdpieces.Visible = false;
                txtpieces.Text = "";
                chkkg.Checked = false;
                chkkg.Visible = false;
                
            }

            BindDropDownItemDescription(0, itemId, subitemid);


        }

        protected void rbtnsubitem_SelectedIndexChanged(object sender, EventArgs e)
        {
            int subitemid;
            int itemid;
            itemid = Convert.ToInt32(rbtnitem.SelectedValue);

            subitemid = Convert.ToInt32(rbtnsubitem.SelectedItem.Value);

            BindDropDownItemDescription(0, itemid, subitemid);
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("GenerateBill.aspx");
            txtpieces.Text = "";
            txtbox.Text = "";
            txtTCS.Text = string.Empty;
            txtshecess.Text = string.Empty;
            txtshecess.Text = string.Empty;
            txtVehicleNo.Text = string.Empty;
            txtvat.Text = string.Empty;
            txtFreight.Text = string.Empty;
            txttotaltax.Text = string.Empty;
            txtecess.Text = string.Empty;
            ClearItems();
            // txtCpnName.Text = string.Empty;
            ddlCmpnName.SelectedIndex = 0;
            // ddlitem.SelectedIndex = 0;
            txtAddress.Text = string.Empty;
            txtCorporate.Text = string.Empty;
            txtName.Text = string.Empty;
            txtserTax.Text = string.Empty;
            // txtserCharge.Text = string.Empty;
            txtTotal.Text = string.Empty;
            txtGTotal.Text = string.Empty;
            Session["tempdata"] = null;
            gvItemDesc.DataSource = null;
            gvItemDesc.DataBind();
            ddlitem.ClearSelection();
            rbtnitem.ClearSelection();
            tdsubitem.Visible = false;
            rbtnsubitem.ClearSelection();
            tdsubitemradio.Visible = false;
        }

        protected void btnRemove_Click1(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow item in gvItemDesc.Rows)
                {
                    List<BillDetail> lstData = (List<BillDetail>)Session["tempdata"];
                    System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)item.FindControl("chkRemove");
                    if (chk.Checked == true)
                    {
                        int index = item.RowIndex;
                        //HiddenField hf = (HiddenField)item.FindControl("HiddenField1");
                        lstData.RemoveAt(index);
                        Session["tempdata"] = lstData;
                        BindGrid();
                    }
                    else if (item.RowIndex == gvItemDesc.Rows.Count - 1 && chk.Checked != true)
                    {
                        ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", "Select Any Record to Remove "), true);

                    }
                    if (gvItemDesc.Rows.Count == 0)
                    {
                        ClearAllData();
                    }

                    Calculate_TotalAmt();
                    CalculateGrandtotal();
                    //txtserTax.Text = string.Empty;
                    // txtserCharge.Text = string.Empty;
                    // txtGTotal.Text = string.Empty;

                }


            }
            catch (Exception exp)
            {
                System.Windows.Forms.MessageBox.Show("At a time only one item can be removed");
                ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", "At a time only one item can be removed "), true);
            }



        }

        protected void chkkg_CheckedChanged(object sender, EventArgs e)
        {
            if (chkkg.Checked)
            {
                lblkg.Visible = true;
                lblquantity.Visible = false;
                ViewState["measrument"] = lblkg.Text;
                //"Pieces/Kg"

            }
            else
            {
                lblquantity.Visible = true;
                lblkg.Visible = false;
                ViewState["measrument"] = lblquantity.Text;

            }

        }



    }
}
