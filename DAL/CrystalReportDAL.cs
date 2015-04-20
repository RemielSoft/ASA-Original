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
    public class CrystalReportDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;

        public List<CrystalReportDOM> ReadCrystalReport(int billId)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "procReadCrystalReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@in_Bill_Id", billId));
            SqlDataReader sdr = cmd.ExecuteReader();
            List<CrystalReportDOM> lstCrystalReportDOM = new List<CrystalReportDOM>();
            while (sdr.Read())
            {
                CrystalReportDOM crystalReportDOM = new CrystalReportDOM();
                crystalReportDOM.BillId = Convert.ToInt32(sdr["bill_id"]);
                crystalReportDOM.BillDate = Convert.ToDateTime(sdr["bill_date"]);
                crystalReportDOM.CompanyName = sdr["company_name"].ToString();
                crystalReportDOM.CompanyAddress = sdr["company_address"].ToString();
                crystalReportDOM.Corporate = sdr["corporate"].ToString();
                crystalReportDOM.ClientName = sdr["client_name"].ToString();
                crystalReportDOM.TotalAmount = Convert.ToDecimal(sdr["total_amt"]);
                crystalReportDOM.ServiceCharge = Convert.ToDecimal(sdr["service_charge"]);
                crystalReportDOM.ServiceTax = Convert.ToDecimal(sdr["service_tax"]);
                crystalReportDOM.GrandTotal = Convert.ToDecimal(sdr["grand_total"]);
                crystalReportDOM.ItemDescription = sdr["item_description"].ToString();
                crystalReportDOM.Quantity = Convert.ToDecimal(sdr["quantity"]);
                crystalReportDOM.Rate = Convert.ToDecimal(sdr["rate"]);
                crystalReportDOM.Amount = Convert.ToDecimal(sdr["amount"]);
                lstCrystalReportDOM.Add(crystalReportDOM);
            }
            con.Close();
            return lstCrystalReportDOM;
        }

        public List<BillDetail> ReadCrystalReportDS(int billId)
        {
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "procReadCrystalReport";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@in_Bill_Id", billId));
            SqlDataReader dr = cmd.ExecuteReader();

            List<BillDetail> lstBillDetail = new List<BillDetail>();

            while (dr.Read())
            {
                BillDetail billDetail = new BillDetail();
                billDetail.Id =Convert.ToInt32(dr["id"]);
                billDetail.BillId=Convert.ToInt32(dr["bill_id"]);
                billDetail.ItemDescription = dr["item_description"].ToString();
                billDetail.Quantity =Convert.ToDecimal(dr["quantity"]);
                billDetail.Rate = Convert.ToDecimal(dr["rate"]);
                billDetail.Amount = Convert.ToDecimal(dr["amount"]);
                billDetail.CreatedBy = dr["created_by"].ToString();
                billDetail.CreatedDate =Convert.ToDateTime(dr["created_date"]);
                billDetail.ModifiedBy = dr["modified_by"].ToString();
                //billDetail.ModifiedDate = Convert.ToDateTime(dr["modified_date"]);

                lstBillDetail.Add(billDetail);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstBillDetail;  //procReadBillById
        }

        public List<Bill> ReadBiLLDetailByBillId(int billId)
        {
            Bill bill = new Bill();
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            cmd.CommandText = "procReadBillById";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@in_Bill_Id", billId));
            SqlDataReader dr = cmd.ExecuteReader();
            List<Bill> lstBill = new List<Bill>();
            while (dr.Read())
            {
                bill.BillId = Convert.ToInt32(dr["bill_id"]);
                bill.ClientName = dr["client_name"].ToString();
                bill.CompanyName = dr["CompanyName"].ToString();
                bill.CompanyAddress = dr["company_address"].ToString();
                bill.TotalAmount =Convert.ToDecimal(dr["total_amt"]);
                bill.ServiceCharge = Convert.ToDecimal(dr["service_charge"]);
                bill.ServiceTax = Convert.ToDecimal(dr["service_tax"]);
                bill.GrandTotal = Convert.ToDecimal(dr["grand_total"]);
                bill.Corporate = dr["corporate"].ToString();
                bill.CreatedBy = dr["created_by"].ToString();
                bill.BillDate = Convert.ToDateTime(dr["created_date"]);
                bill.ModifiedBy = dr["modified_by"].ToString();
                //bill.ModifiedDate = Convert.ToDateTime(dr["modified_date"]);
                lstBill.Add(bill);
            }

            return lstBill;
        }
    }
}
