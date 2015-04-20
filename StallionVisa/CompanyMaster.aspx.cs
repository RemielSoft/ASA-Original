using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DOM;
using BAL;

namespace StallionVisa
{
    public partial class CompanyMaster : System.Web.UI.Page
    {
        #region Global Variables

        int ChargesId;

        CompanyMasterBAL companyMasterBAL = new CompanyMasterBAL();
        CompanyMasterDom companyMasterDom = new CompanyMasterDom();
       
        
        #endregion

        #region Protected Method
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtCpnName.Focus();
                ShowCompanyDetails();
                BindRadioButton();
                //rbtnvat.SelectedItem.Value = "CST";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveCompanymaster();
            ShowCompanyDetails();
            Clear();
           
            

        }

        protected void grdCompany_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            ViewState["CompanyId"]=id;
            if (e.CommandName == "Edit")
            {
                List<CompanyMasterDom> lst = companyMasterBAL.ReadCompanyDetails(id);
                txtCpnName.Text = lst[0].CompanyName;
                txtCmpnAddress.Text = lst[0].CompanyAddress;
                //rbtnvat.SelectedItem.Value = Convert.ToInt32(lst[0].chargesId).ToString();
                int id1 = Convert.ToInt32(lst[0].chargesId);
                if (id1 == 1)
                {
                    rbtnvat.SelectedValue = "1";

                }
                else 
                {
                    rbtnvat.SelectedValue = "2";
                }
                
                txtContact.Text = lst[0].Phone;
                txtEcc.Text = lst[0].Information.EccNumber;
                txtTan.Text = lst[0].Information.TanNumber;
                
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
            else if (e.CommandName == "Delete")
            {

                companyMasterBAL.DeleteCompanyDetails(id, "Admin");
                ShowCompanyDetails();
            }

          
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCompanyMaster();
            Clear();
            ShowCompanyDetails();
           
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
        #endregion

        #region Private Method

        private void SaveCompanymaster()
        {

             //companyMasterDom.Information = new ContactInfo();
            int id = 0;
                companyMasterBAL = new CompanyMasterBAL();
                CompanyMasterDom companyMasterDom = new CompanyMasterDom();
                companyMasterDom.Information = new ContactInfo();

                companyMasterDom.CompanyName = txtCpnName.Text.Trim();
                companyMasterDom.CompanyAddress = txtCmpnAddress.Text.Trim();
               
                    companyMasterDom.chargesId = Convert.ToInt32(rbtnvat.SelectedItem.Value);
               
                   
                    companyMasterDom.Phone = txtContact.Text.Trim();
                    companyMasterDom.Information.EccNumber = txtEcc.Text;
                    companyMasterDom.Information.TanNumber = txtTan.Text;
                    companyMasterDom.CreatedBy = "Admin";
                    companyMasterDom.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
                    id = companyMasterBAL.CreateCompanyMaster(companyMasterDom);
              
                
                if (id > 0)
                {
                    
                    ShowMessage("Record Saved Successfully");
                    
                }
                else
                {
                    ShowMessage("This Company Name Is Already Exist");
                }

                
            //return id;
        }
        private void UpdateCompanyMaster()
        {
            companyMasterDom.Information = new ContactInfo();
            int id = 0;
            companyMasterDom.CompanyId = Convert.ToInt32(ViewState["CompanyId"]);
            companyMasterDom.CompanyName = txtCpnName.Text.Trim();
            companyMasterDom.CompanyAddress = txtCmpnAddress.Text.Trim();
            companyMasterDom.chargesId = Convert.ToInt32(rbtnvat.SelectedItem.Value);            
            companyMasterDom.Phone = txtContact.Text.Trim();
            companyMasterDom.Information.EccNumber = txtEcc.Text;
            companyMasterDom.Information.TanNumber =txtTan.Text;
            companyMasterDom.ModifiedBy = "Admin";
           // companyMasterDom.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            id = companyMasterBAL.UpdateCompanyMaster(companyMasterDom);
            if (id > 0)
            {
                ShowMessage("Record Updated Successfully");
                ShowCompanyDetails();
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                Clear();
            }
            else
            {
                ShowMessage("Record Not Updated");
            }           
        }

        private void ShowCompanyDetails()
        {
            List<CompanyMasterDom> lstCompanyMasterDom = new List<CompanyMasterDom>();
       
            lstCompanyMasterDom = companyMasterBAL.ReadCompanyDetails(0);
            grdCompany.DataSource = lstCompanyMasterDom;
            grdCompany.DataBind();
           
                     
        }

        public void BindRadioButton()
        {
            companyMasterBAL = new CompanyMasterBAL();
            rbtnvat.DataSource = companyMasterBAL.BindRadioButton(null);
            rbtnvat.DataTextField = "ChargesName";
            rbtnvat.DataValueField = "chargesId";
            rbtnvat.DataBind();
        }
        private void Clear()
        {
            txtCpnName.Text = string.Empty;
            txtCmpnAddress.Text = string.Empty;
            txtContact.Text = string.Empty;
            txtEcc.Text = string.Empty;
            txtTan.Text = string.Empty;
            rbtnvat.ClearSelection();
          //  ShowCompanyDetails();
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            //txtDescription.Text = string.Empty;
        }
        private void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", message), true);
        }
        #endregion          

        protected void grdCompany_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdCompany.PageIndex = e.NewPageIndex;
            ShowCompanyDetails();
            
        }

        protected void rbtnvat_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void grdCompany_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdCompany.EditIndex = e.NewEditIndex;
            ShowCompanyDetails();
        }

        protected void grdCompany_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {

        }

        
    }
}