using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DOM;

namespace BAL
{
   public class ViewBillBAL
    {
       ViewBillDAL billDal = new ViewBillDAL();
       public List<Bill> ShowBillDetailsBal(DateTime? fromDate, DateTime? toDate, Int32? companyId, int? billid)
       {
           return billDal.ShowBillDetails(fromDate, toDate, companyId, billid);
       }
       public List<Bill> ShowBillReportsDetailsBal( DateTime fromDate, DateTime toDate,int? billdeatailid,Int32? companyId ,string itemname,string subitem)
       {
           return billDal.ShowBillReportsDetails(fromDate, toDate, billdeatailid, companyId, itemname, subitem);
       }
       public List<BillDetail> BilldetailByitemname( string itemname)
       {
           return billDal.ReadBilldetailByitemname(itemname);
       }

       public Bill ReadBillById(int id) 
       {
           return billDal.ReadBillById(id);
       }

       public void CreateLedger(Bill bill)
       {
           billDal.CreateLedger(bill);
       }
       public List<Bill> ReadLedgerByCompanyID(int companyID)
        {
            return billDal.ReadLedgerByCompanyID(companyID);
        }

       public void UpdateBillById(Bill bill)
       {
           billDal.UpdateBillById(bill);
       }
    }
}
