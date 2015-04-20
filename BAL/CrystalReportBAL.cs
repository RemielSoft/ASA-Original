using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DOM;
using DAL;

namespace BAL
{
   public class CrystalReportBAL
    {
       DataTable dt;
       CrystalReportDAL crystalReportDAL = new CrystalReportDAL();
       List<CrystalReportDOM> lstCrystalReportDOM = new List<CrystalReportDOM>();
       public List<CrystalReportDOM> ReadCrystalReport(int billId)
       {
           try
           {               
               lstCrystalReportDOM = crystalReportDAL.ReadCrystalReport(billId);               
           }
           catch(Exception ex)
           {
              
           }
           return lstCrystalReportDOM;
       }
       public List<BillDetail> ReadCrystalReportDS(int billId)
       {

           return crystalReportDAL.ReadCrystalReportDS(billId);
            
       }

       public List<Bill> ReadBiLLDetailByBillId(int billId)
       {
           return crystalReportDAL.ReadBiLLDetailByBillId(billId);
       }
    }
}
