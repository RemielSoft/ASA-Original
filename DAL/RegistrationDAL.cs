using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DOM;
namespace DAL
{
    public class RegistrationDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;
        public int CreateLoginRegistration(RegistrationDOM registrationDOM)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procCreateLoginRegistration";
            cmd.Connection = con;
            con.Open();
            int id = 0;

            cmd.Parameters.Add(new SqlParameter("@in_Login_Name", registrationDOM.LoginName));
            cmd.Parameters.Add(new SqlParameter("@in_Login_Id", registrationDOM.LoginId));
            cmd.Parameters.Add(new SqlParameter("@in_Password", registrationDOM.Password));
            cmd.Parameters.Add(new SqlParameter("@in_Confirm_Password", registrationDOM.ConfirmPassword));
            cmd.Parameters.Add(new SqlParameter("@in_Role", registrationDOM.Role));

            cmd.Parameters.Add(new SqlParameter("@in_Created_By", registrationDOM.CreatedBy));
            cmd.Parameters.Add(new SqlParameter("@in_Created_Date", registrationDOM.CreatedDate));
            cmd.Parameters.Add(new SqlParameter("@out_Login_Id", DbType.Int32));
            cmd.Parameters["@out_Login_Id"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            id = Convert.ToInt32(cmd.Parameters["@out_Login_Id"].Value);
            cmd.Dispose();
            con.Close();
            return id;
        }
        public List<RegistrationDOM> ReadRegistrationDetails()
        {
            cmd = new SqlCommand("procReadRegistraionDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            List<RegistrationDOM> lstRegistrationDOM = new List<RegistrationDOM>();
            while (dr.Read())
            {
                RegistrationDOM registrationDOM = new RegistrationDOM();
                registrationDOM.RegistrationId = Convert.ToInt32(dr["Id"]);
                registrationDOM.LoginName = dr["Login_Name"].ToString();
                registrationDOM.LoginId = dr["Login_Id"].ToString();
                registrationDOM.Password = dr["Password"].ToString();
                registrationDOM.ConfirmPassword = dr["Confirm_Password"].ToString();
                registrationDOM.Role = dr["Role"].ToString();
                lstRegistrationDOM.Add(registrationDOM);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstRegistrationDOM;
        }
        public RegistrationDOM ReadRegistrationDetailsById(int id)
        {
            cmd = new SqlCommand("procReadRegistraionDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_Id", id));
            SqlDataReader dr = cmd.ExecuteReader();
            RegistrationDOM registrationDOM = new RegistrationDOM();
            while (dr.Read())
            {
                registrationDOM.RegistrationId = Convert.ToInt32(dr["Id"]);
                registrationDOM.LoginName = dr["Login_Name"].ToString();
                registrationDOM.LoginId = dr["Login_Id"].ToString();
                registrationDOM.Password = dr["Password"].ToString();
                registrationDOM.ConfirmPassword = dr["Confirm_Password"].ToString();
                registrationDOM.Role = dr["Role"].ToString();
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return registrationDOM;
        }
        public void DeleteRegistrationDetails(int id, string modifiedBy)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procDeleteRegistrationDetails";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new SqlParameter("@in_Id", id));
            cmd.Parameters.Add(new SqlParameter("@in_Modified_By", modifiedBy));


            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

        }
        public int UpdateRegistrationDetails(RegistrationDOM registrationDOM)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procUpdateRegistrationDetails";
            cmd.Connection = con;
            con.Open();
            int id = 0;

            cmd.Parameters.Add(new SqlParameter("@in_Id", registrationDOM.RegistrationId));
            cmd.Parameters.Add(new SqlParameter("@in_Login_Name", registrationDOM.LoginName));
            cmd.Parameters.Add(new SqlParameter("@in_Login_Id", registrationDOM.LoginId));
            cmd.Parameters.Add(new SqlParameter("@in_Password", registrationDOM.Password));
            cmd.Parameters.Add(new SqlParameter("@in_Confirm_Password", registrationDOM.ConfirmPassword));
            cmd.Parameters.Add(new SqlParameter("@in_Role", registrationDOM.Role));

            cmd.Parameters.Add(new SqlParameter("@in_Modified_By", registrationDOM.ModifiedBy));
            // cmd.Parameters.Add(new SqlParameter("@in_Created_Date", companyMasterDom.CreatedDate));
            cmd.Parameters.Add(new SqlParameter("@out_Login_Id", DbType.Int32));
            cmd.Parameters["@out_Login_Id"].Direction = ParameterDirection.Output;

            cmd.ExecuteNonQuery();

            id = Convert.ToInt32(cmd.Parameters["@out_Login_Id"].Value);
            cmd.Dispose();
            con.Close();
            return id;
        }

        

        public int ValidateUser(String loginName, String password)
        {
            int userId=0;

            
            cmd = new SqlCommand("procValidateUser", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_user_login_name", loginName));
            cmd.Parameters.Add(new SqlParameter("@in_password", password));
            cmd.Parameters.Add(new SqlParameter("@out_userId", DbType.Int32));
            cmd.Parameters["@out_userId"].Direction = ParameterDirection.Output;
            cmd.ExecuteNonQuery();

            userId = Convert.ToInt32(cmd.Parameters["@out_userId"].Value);
            cmd.Dispose();
            con.Close();

            return userId;
        }

        public RegistrationDOM ReadUserByLoginID(String loginID)
        {
           
            cmd = new SqlCommand("ProcReadUserByLoginId", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("in_Login_Id", loginID));
            SqlDataReader dr = cmd.ExecuteReader();
            RegistrationDOM registrationDOM = new RegistrationDOM();
            while (dr.Read())
            {
                registrationDOM.RegistrationId = Convert.ToInt32(dr["Id"]);
                registrationDOM.LoginName = dr["Login_Name"].ToString();
                registrationDOM.LoginId = dr["Login_Id"].ToString();
                registrationDOM.Password = dr["Password"].ToString();
                registrationDOM.Role = dr["Role"].ToString();
            }
            dr.Close();
            dr.Dispose();
            con.Close();

            return registrationDOM;
        }
    }
}
