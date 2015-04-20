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
    public partial class UserRegistration : System.Web.UI.Page
    {
        #region Global Variables
        RegistrationBAL registrationBAL = new RegistrationBAL();
        RegistrationDOM registrationDOM = new RegistrationDOM();

        #endregion

        #region Protected Method
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ShowRegistrationDetails();
                
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveLoginRegistration();
          //  Clear();
            ShowRegistrationDetails();
        }
        protected void grdRegistration_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            ViewState["RegistrationId"] = id;
            if (e.CommandName == "Eddit")
            {
                registrationDOM = registrationBAL.ReadRegistrationDetailsById(id);
                txtLoginName.Text = registrationDOM.LoginName;
                txtLoginID.Text = registrationDOM.LoginId;
                txtpassword.Text = registrationDOM.Password;
              //  txtConfirmPassword.Text = registrationDOM.ConfirmPassword;
                txtConfirmPassword.Text = registrationDOM.Password;
                ddlRole.SelectedValue= registrationDOM.Role;  
               
                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
            else if (e.CommandName == "Delet")
            {
                registrationBAL.DeleteRegistrationDetails(id,"Admin");
            }
            ShowRegistrationDetails();
          
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
           
            UpdateRegistrationDetails();
            Clear();
            ShowRegistrationDetails();
        }
        #endregion

        #region Private Method

        private int SaveLoginRegistration()
        {
            int id = 0;
            registrationDOM.LoginName = txtLoginName.Text.Trim();
            registrationDOM.LoginId = txtLoginID.Text.Trim();
            registrationDOM.Password = txtpassword.Text.Trim();
            registrationDOM.ConfirmPassword = txtConfirmPassword.Text.Trim();
            registrationDOM.Role = ddlRole.SelectedValue;
            registrationDOM.CreatedBy = "Admin";
            registrationDOM.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            id = registrationBAL.CreateLoginRegistration(registrationDOM);
            if (id > 0)
            {
                ShowMessage("Record Saved Successfully");
                Clear();
            }
            else
            {
                ShowMessage("This User Is Alrady Exist");
            }
            return id;
        }
        private void UpdateRegistrationDetails()
        {
            int id = 0;
            registrationDOM.RegistrationId = Convert.ToInt32(ViewState["RegistrationId"]);
            registrationDOM.LoginName = txtLoginName.Text.Trim();
            registrationDOM.LoginId = txtLoginID.Text.Trim();
            registrationDOM.Password = txtpassword.Text.Trim();
            registrationDOM.ConfirmPassword = txtConfirmPassword.Text.Trim();
            registrationDOM.Role = ddlRole.SelectedValue;

            registrationDOM.ModifiedBy = "Admin";
            
            id = registrationBAL.UpdateRegistrationDetails(registrationDOM);
            if (id > 0)
            {
                ShowMessage("Record Updated Successfully");               
            }
              
            else
            {
                ShowMessage("Record Not Updated");
            }
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            
        }

        private void ShowRegistrationDetails()
        {
            List<RegistrationDOM> lstregistrationDOM = new List<RegistrationDOM>();
            lstregistrationDOM = registrationBAL.ReadRegistrationDetails();
            grdRegistration.DataSource = lstregistrationDOM;
            grdRegistration.DataBind();
        }
        private void Clear()
        {
            txtLoginName.Text= string.Empty;
            txtLoginID.Text = string.Empty;
            txtpassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
            ddlRole.SelectedValue = "User";
        }
        private void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", message), true);
        }
        #endregion          

        

        
              
    }
}