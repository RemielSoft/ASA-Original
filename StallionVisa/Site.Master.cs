using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using DOM;
using BAL;

namespace StallionVisa
{
    public partial class SiteMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (String.Equals(LoggedInUser.Role.ToUpper(), "ADMIN"))
                {
                    NavigationMenuAdmin.Visible = true;
                }
                else
                {
                    NavigationMenuAdmin.Visible = false;
                }
            }
        }
        protected void imgSignOut_Click(object sender, ImageClickEventArgs e)
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            Response.Redirect("~/Login.aspx");
        }

        public RegistrationDOM LoggedInUser
        {
            get
            {
                if (HttpContext.Current.Session["USER_SESSION"] == null ||
                     CurrentUserName.ToUpper() != ((RegistrationDOM)HttpContext.Current.Session["USER_SESSION"]).LoginId.ToUpper()
                    )
                {
                    RegistrationDOM user = new RegistrationDOM();
                    user = GetUserByUserID(CurrentUserName);

                    HttpContext.Current.Session["USER_SESSION"] = user;
                }
                return (RegistrationDOM)HttpContext.Current.Session["USER_SESSION"];
            }
        }

        private String CurrentUserName
        {
            get
            {
                String currentEmployee = Page.User.Identity.Name;
                if (currentEmployee.IndexOf('\\') > -1)
                {
                    currentEmployee = currentEmployee.Substring(currentEmployee.IndexOf('\\') + 1, currentEmployee.Length - currentEmployee.IndexOf('\\') - 1);
                }
                return currentEmployee;
            }

        }

        private RegistrationDOM GetUserByUserID(string userId)
        {
            RegistrationBAL registrationBL = new RegistrationBAL();

            RegistrationDOM user = registrationBL.ReadUserByLoginID(userId);
            return user;
        }
    }
}
