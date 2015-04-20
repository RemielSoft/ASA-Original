using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using DOM;
using System.Data.Common;

namespace DAL
{
   public class CompanyMasterDal
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;

        #region RadioButton

        public List<CompanyMasterDom> BindRadioButton(int? id)
        {
            List<CompanyMasterDom> lst = new List<CompanyMasterDom>();
            cmd = new SqlCommand("ProcReadMetaDataChargesType", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            if (id != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@in_Id", id));
            }
           
            
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
                {
                    CompanyMasterDom companyMasterDom = new CompanyMasterDom();
                    companyMasterDom.chargesId = Convert.ToInt32(dr["ID"]);
                    companyMasterDom.ChargesName = dr["Name"].ToString();
                    companyMasterDom.Chargesvalue = Convert.ToDecimal(dr["Value"]);
                    lst.Add(companyMasterDom);
                    //con.Close();
                }
            return lst;
        }

       #endregion
                  
        public int CreateCompanyMaster(CompanyMasterDom companyMasterDom)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procCreateCompanyMaster";
            cmd.Connection = con;
            con.Open();
            int id = 0;

            cmd.Parameters.Add(new SqlParameter("@in_Company_Name", companyMasterDom.CompanyName));
            cmd.Parameters.Add(new SqlParameter("@in_Company_Address", companyMasterDom.CompanyAddress));
            cmd.Parameters.Add(new SqlParameter("@in_Charges", companyMasterDom.chargesId));
            cmd.Parameters.Add(new SqlParameter("@in_CompanyPhoneNo", companyMasterDom.Phone));
            cmd.Parameters.Add(new SqlParameter("@in_ECC", companyMasterDom.Information.EccNumber));           
            cmd.Parameters.Add(new SqlParameter("@in_TIN",companyMasterDom.Information.TanNumber));            
            cmd.Parameters.Add(new SqlParameter("@in_Created_By", companyMasterDom.CreatedBy));
            cmd.Parameters.Add(new SqlParameter("@in_Created_Date",companyMasterDom.CreatedDate));
            cmd.Parameters.Add(new SqlParameter("@out_Company_Id", DbType.Int32));
            cmd.Parameters["@out_Company_Id"].Direction = ParameterDirection.Output;
            
            cmd.ExecuteNonQuery();
            id = Convert.ToInt32(cmd.Parameters["@out_Company_Id"].Value);
            cmd.Dispose();
            con.Close();
            
            return id;
        }

        public List<CompanyMasterDom> ReadCompanyDetails(int? Id)
        {
            cmd = new SqlCommand("procReadCompanyDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            if (Id != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@in_CompanyId", Id));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@in_CompanyId", null));
            }
           
            SqlDataReader dr = cmd.ExecuteReader();
           
            List<CompanyMasterDom> lstCompanyMasterDom = new List<CompanyMasterDom>();
            while (dr.Read())
            {
                CompanyMasterDom companyMasterDom = new CompanyMasterDom();
                companyMasterDom.Information = new ContactInfo();
                companyMasterDom.CompanyId = Convert.ToInt32(dr["CompanyId"]);
                companyMasterDom.CompanyName =Convert.ToString( dr["CompanyName"]);
                companyMasterDom.CompanyAddress =Convert.ToString( dr["CompanyAddress"]); 
                companyMasterDom.chargesId = Convert.ToInt32(dr["Charges"]);                
                if (companyMasterDom.chargesId == 1)
                {
                    companyMasterDom.ChargesName = "CST";
                }
                else
                {
                    companyMasterDom.ChargesName = "VAT";
                }               
                companyMasterDom.Phone = dr["Company_Phone_no"].ToString();
                companyMasterDom.Information.EccNumber = Convert.ToString(dr["ECC"]);                
                companyMasterDom.Information.TanNumber =Convert.ToString(dr["TIN"]);
                
                lstCompanyMasterDom.Add(companyMasterDom);
                
            }
            dr.Close();           
            dr.Dispose();
            con.Close();

            return lstCompanyMasterDom;
        }

        public CompanyMasterDom ReadCompanyDetailsById(int id)
        {
            cmd = new SqlCommand("procReadCompanyDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_CompanyId", id));
            SqlDataReader dr = cmd.ExecuteReader();
            CompanyMasterDom companyMasterDom = new CompanyMasterDom();
            while (dr.Read())
            {
                companyMasterDom.CompanyId = Convert.ToInt32(dr["CompanyId"]);
                companyMasterDom.CompanyName = dr["CompanyName"].ToString();
                companyMasterDom.CompanyAddress = dr["CompanyAddress"].ToString();
                //companyMasterDom.Description = dr["Deascription"].ToString();
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return companyMasterDom;
        }
        
       public int UpdateCompanyMaster(CompanyMasterDom companyMasterDom)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procUpdateCompanyDetails";
            cmd.Connection = con;
            con.Open();
            int id = 0;

            cmd.Parameters.Add(new SqlParameter("@in_Company_Id",companyMasterDom.CompanyId));
            cmd.Parameters.Add(new SqlParameter("@in_Company_Name", companyMasterDom.CompanyName));
            cmd.Parameters.Add(new SqlParameter("@in_Company_Address", companyMasterDom.CompanyAddress));
            cmd.Parameters.Add(new SqlParameter("@in_Charges", companyMasterDom.chargesId));
            cmd.Parameters.Add(new SqlParameter("@in_CompanyPhoneNo", companyMasterDom.Phone));           
            cmd.Parameters.Add(new SqlParameter("@in_ECC", companyMasterDom.Information.EccNumber));            
            cmd.Parameters.Add(new SqlParameter("@in_TIN", companyMasterDom.Information.TanNumber));           
            cmd.Parameters.Add(new SqlParameter("@in_Modified_By", companyMasterDom.ModifiedBy));
           // cmd.Parameters.Add(new SqlParameter("@in_Created_Date", companyMasterDom.CreatedDate));
            cmd.Parameters.Add(new SqlParameter("@out_Company_Id", DbType.Int32));
            cmd.Parameters["@out_Company_Id"].Direction = ParameterDirection.Output;

            id = Convert.ToInt32(cmd.Parameters["@out_Company_Id"].Value);
            cmd.ExecuteNonQuery();          
            cmd.Dispose();
            con.Close();
            return id;
        }
        public void DeleteCompanyDetails(int id,string modifiedBy)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procDeleteCompanyDetails";
            cmd.Connection = con;
            con.Open();            

            cmd.Parameters.Add(new SqlParameter("@in_Company_Id",id));           
            cmd.Parameters.Add(new SqlParameter("@in_Modified_By", modifiedBy));
            
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
            
        }
    }
}
