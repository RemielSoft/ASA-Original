using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DOM;
using DAL;

namespace BAL
{
    public class BillDetailBAL
    {
        BillDetailDAL obDal = new BillDetailDAL();
        public void AddItems(List<BillDetail> lstBillDetail, int id)
        {
            obDal.AddItems(lstBillDetail,id);
        }

        public int SaveBill(Bill bill)
        {
           return obDal.SaveBill(bill);
        }

        public List<BillDetail> ReadItems()
        {
            return obDal.ReadItems();
        }
        public List<Bill> ReadSexvicestax(int?id)
        {
            return obDal.ReadServiceTax(id);
        }


        public List<BillDetail> ReadBillDetailByBillId(int billId)
        {
            return obDal.ReadBillDetailByBillId(billId);
        }
        public void DeleteItemByBillId(int id)
        {
            obDal.DeleteItemByBillId(id);
        }
    }
}
