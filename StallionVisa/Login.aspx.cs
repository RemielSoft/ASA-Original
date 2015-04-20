using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DOM;
using BAL;
using System.Web.Security;

namespace StallionVisa
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        

        protected void loginControl_Authenticate(object sender, AuthenticateEventArgs e)
        {
            RegistrationBAL registrationBL = new RegistrationBAL();
            RegistrationDOM user = registrationBL.ReadUserByLoginID(loginControl.UserName);
            if (ValidateUser())
            {
               
                //RegistrationBAL registrationBL = new RegistrationBAL();
                //RegistrationDOM user = registrationBL.ReadUserByLoginID(loginControl.UserName);
                if (user == null)
                {
                    loginControl.FailureText = "User not Found..";
                    return;
                }
                FormsAuthentication.RedirectFromLoginPage(loginControl.UserName, loginControl.RememberMeSet);
                Response.Redirect("ViewBill.aspx");

            }
            else
            {
                loginControl.FailureText = "User name or password incorrect. Please try again..";
            }

           
        }



        #region Private Sections

        private bool ValidateUser()
        {
            RegistrationBAL registrationBL = new RegistrationBAL();

            if (registrationBL.ValidateUser(loginControl.UserName, loginControl.Password) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion


    }
}