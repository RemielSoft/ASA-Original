using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DOM;

namespace BAL
{
   public class CompanyMasterBAL
    {
       CompanyMasterDal companyMasterDal = new CompanyMasterDal();
       
       #region Bind RadioButton


       public List<CompanyMasterDom> BindRadioButton(int? id)
       {
         List<CompanyMasterDom> lst = new List<CompanyMasterDom>();
         try
         {
             lst = companyMasterDal.BindRadioButton(id);
         }
         catch (Exception ex)
         { 
         
         }
         return lst;
       }

       #endregion

       public int CreateCompanyMaster(CompanyMasterDom companyMasterDom)
       {
           int id = 0;
           return id = companyMasterDal.CreateCompanyMaster(companyMasterDom);               
           
       }
       public List<CompanyMasterDom> ReadCompanyDetails(int? Id)
       {
           List<CompanyMasterDom> lstCompanyMasterDom = new List<CompanyMasterDom>();
           try
           {
               lstCompanyMasterDom = companyMasterDal.ReadCompanyDetails(Id);
           }
           catch(Exception ex)
           {

           }
           return lstCompanyMasterDom;
       }

       //public CompanyMasterDom ReadCompanyDetailsById(int id)
       //{
       //    CompanyMasterDom companyMasterDom = null;
       //    try
       //    {
       //        companyMasterDom = companyMasterDal.ReadCompanyDetailsById(id);
       //    }
       //    catch (Exception ex)
       //    {

       //    }
       //   return companyMasterDom;
       //}

       public int UpdateCompanyMaster(CompanyMasterDom companyMasterDom)
       {
           int id = 0;
           try
           {
               id = companyMasterDal.UpdateCompanyMaster(companyMasterDom);
           }
           catch (Exception ex)
           {

           }
           return id;
       }
       public void DeleteCompanyDetails(int id, string modifiedBy)
       {
           
           try
           {
                companyMasterDal.DeleteCompanyDetails(id,modifiedBy);
           }
           catch (Exception ex)
           {

           }
          
       }
    }
}
