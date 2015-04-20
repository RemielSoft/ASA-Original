using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DOM;

namespace BAL
{
   public class RegistrationBAL
    {
       RegistrationDAL registrationDAL = new RegistrationDAL();

       public int CreateLoginRegistration(RegistrationDOM registrationDOM)
        {
            int id = 0;
            try
            {
                id = registrationDAL.CreateLoginRegistration(registrationDOM);
            }
            catch (Exception Ex)
            {

            }
            return id;
        }
       public List<RegistrationDOM> ReadRegistrationDetails()
        {
            List<RegistrationDOM> lstRegistrationDOM = new List<RegistrationDOM>();
            try
            {
                lstRegistrationDOM = registrationDAL.ReadRegistrationDetails();
            }
            catch (Exception ex)
            {

            }
            return lstRegistrationDOM;
        }
       public RegistrationDOM ReadRegistrationDetailsById(int id)
        {
            RegistrationDOM registrationDOM = null;
            try
            {
                registrationDOM = registrationDAL.ReadRegistrationDetailsById(id);
            }
            catch (Exception ex)
            {

            }
            return registrationDOM;
        }
       public void DeleteRegistrationDetails(int id, string modifiedBy)
       {

           try
           {
               registrationDAL.DeleteRegistrationDetails(id, modifiedBy);
           }
           catch (Exception ex)
           {

           }

       }
       public int UpdateRegistrationDetails(RegistrationDOM registrationDOM)
       {
           int id = 0;
           try
           {
               id = registrationDAL.UpdateRegistrationDetails(registrationDOM);
           }
           catch (Exception ex)
           {

           }
           return id;
       }

       public int ValidateUser(String loginName, String password)
       {
           Int32 userId = 0;

           try
           {
               userId = registrationDAL.ValidateUser(loginName, password);
           }
           catch (Exception exp)
           {
              
           }

           return userId;
       }

       public RegistrationDOM ReadUserByLoginID(String loginId)
       {
           RegistrationDOM registration = null;

           try
           {
               registration = registrationDAL.ReadUserByLoginID(loginId);
           }
           catch (Exception exp)
           {
               return null;
           }

           return registration;
       }


    }
}
