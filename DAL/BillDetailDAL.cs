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
    //Check out Promp is required
    public class BillDetailDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;
        
        public void AddItems(List<BillDetail> lstBillDetail, int id)
        {
            foreach (BillDetail billDetail in lstBillDetail)
            {
               
                    cmd = new SqlCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "procAddBillDetail";
                    cmd.Connection = con;
                    con.Open();

                    cmd.Parameters.Add(new SqlParameter("@in_bill_id", id));
                    cmd.Parameters.Add(new SqlParameter("@in_item_description", billDetail.ItemDescription));
                    cmd.Parameters.Add(new SqlParameter("@in_quantity", billDetail.Quantity));
                    cmd.Parameters.Add(new SqlParameter("@in_rate", billDetail.Rate));
                    cmd.Parameters.Add(new SqlParameter("@in_amount", billDetail.Amount));
                    cmd.Parameters.Add(new SqlParameter("@in_created_by", billDetail.CreatedBy));
                    cmd.Parameters.Add(new SqlParameter("@in_created_date", billDetail.CreatedDate));
                    cmd.Parameters.Add(new SqlParameter("@in_box", billDetail.Box));
                    cmd.Parameters.Add(new SqlParameter("@in_piesce", billDetail.Pieces));
                    cmd.Parameters.Add(new SqlParameter("@in_itemname", billDetail.ItemName));
                    cmd.Parameters.Add(new SqlParameter("@in_measurment", billDetail.measrment));
                    cmd.Parameters.Add(new SqlParameter("@in_boxmeasurment", billDetail.BoxMeasrument));
                    cmd.Parameters.Add(new SqlParameter("@in_piescemeasurment", billDetail.PiecesMeasrument));

                    cmd.ExecuteNonQuery();
                
            cmd.Dispose();
            con.Close();
            }
        }

        public int SaveBill(Bill bill)
        {
                cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "procSaveBill";
                cmd.Connection = con;
                con.Open();
                int id = 0;
                
               cmd.Parameters.Add(new SqlParameter("@Frieght", bill.Freight));
               cmd.Parameters.Add(new SqlParameter("@Tcs", bill.TCS));
               cmd.Parameters.Add(new SqlParameter("@e_cess", bill.E_cess));
               cmd.Parameters.Add(new SqlParameter("@she_cess", bill.SHE_cess));
               cmd.Parameters.Add(new SqlParameter("@vechicle", bill.Vehicle));
               cmd.Parameters.Add(new SqlParameter("@cstvatvalue", bill.CstVATValue));
               cmd.Parameters.Add(new SqlParameter("@cstvatId", bill.CstVatid));
               cmd.Parameters.Add(new SqlParameter("@totaltax", bill.TotalWithTax)); 

                cmd.Parameters.Add(new SqlParameter("@in_date", bill.BillDate));
                cmd.Parameters.Add(new SqlParameter("@in_company_id", bill.CompanyId));
                cmd.Parameters.Add(new SqlParameter("@in_company_address", bill.CompanyAddress));
                cmd.Parameters.Add(new SqlParameter("@in_corporate", bill.Corporate));
                cmd.Parameters.Add(new SqlParameter("@in_name", bill.ClientName));
                cmd.Parameters.Add(new SqlParameter("@in_total_amt", bill.TotalAmount));
                cmd.Parameters.Add(new SqlParameter("@in_service_tax", bill.ServiceTax));
                cmd.Parameters.Add(new SqlParameter("@in_service_charge", bill.ServiceCharge));
                cmd.Parameters.Add(new SqlParameter("@in_grand_total", bill.GrandTotal));

                cmd.Parameters.Add(new SqlParameter("@in_created_by", bill.CreatedBy));
                cmd.Parameters.Add(new SqlParameter("@in_created_date", bill.CreatedDate));
                cmd.Parameters.Add(new SqlParameter("@out_bill_id", DbType.Int32));
                cmd.Parameters["@out_bill_id"].Direction = ParameterDirection.Output;

                cmd.ExecuteNonQuery();
                
            id = Convert.ToInt32(cmd.Parameters["@out_bill_id"].Value);
            cmd.Dispose();
            con.Close();
            return id;
           

        }

        public List<BillDetail> ReadItems()
        {
            cmd = new SqlCommand("procReadItems", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr= cmd.ExecuteReader();
            List<BillDetail> lst = new List<BillDetail>();
            while (dr.Read())
            {
                BillDetail ob = new BillDetail();
                ob.Id =Convert.ToInt32(dr["Id"]);
                ob.ItemDescription = dr["item_description"].ToString();
                ob.Quantity = Convert.ToDecimal(dr["quantity"]);
                ob.Rate = Convert.ToDecimal(dr["rate"]);
                ob.Amount = Convert.ToDecimal(dr["amount"]);
                lst.Add(ob);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lst;
        }

        public List<BillDetail> ReadBillDetailByBillId(int billId)
        {
            cmd = new SqlCommand("procReadBillDetailByBillId",con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr;
            cmd.Parameters.Add(new SqlParameter("@in_bill_id", billId));
            dr= cmd.ExecuteReader();
            List<BillDetail> lstBillDetail = new List<BillDetail>();
            while (dr.Read())
            {
                BillDetail ob = new BillDetail();
                ob.BillId = Convert.ToInt32(dr["bill_id"]);
                ob.Box = Convert.ToInt32(dr["Box"]);
                ob.Pieces = Convert.ToInt32(dr["Pieces"]);
                ob.ItemDescription = dr["item_description"].ToString();
                ob.Quantity = Convert.ToDecimal(dr["quantity"]);
                ob.Rate = Convert.ToDecimal(dr["rate"]);
                ob.Amount = Convert.ToDecimal(dr["amount"]);
                ob.ItemName = dr["ItemName"].ToString();
                ob.measrment = dr["measurement"].ToString();
                ob.BoxMeasrument = dr["BoxMeasurement"].ToString();
                ob.PiecesMeasrument = dr["PiecesMeasurement"].ToString();
                ob.CreatedBy = dr["created_by"].ToString();
                
                ob.CreatedDate = Convert.ToDateTime(dr["created_date"]);
                ob.ModifiedBy = dr["modified_by"].ToString();
                if (!string.IsNullOrEmpty(dr["modified_date"].ToString()))
                {
                    ob.ModifiedDate = Convert.ToDateTime(dr["modified_date"]);
                }
                lstBillDetail.Add(ob);
                
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstBillDetail;

        }
        public void DeleteItemByBillId(int id)
        {
            cmd = new SqlCommand("Delete from Bill_Detail Where Id='" + id + "'",con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();

        }
        public List<Bill> ReadServiceTax(int? taxid)
        {
            cmd = new SqlCommand("procReadServiceTaxMaster", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr;
            if (taxid != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@tax_id", taxid));
            }
            dr = cmd.ExecuteReader();
            List<Bill> lstBill = new List<Bill>();
            while (dr.Read())
            {
                Bill Billob = new Bill();
                Billob.TaxId = Convert.ToInt32(dr["Tax_id"]);
                Billob.ServiceTax =Convert.ToDecimal( dr["ServiceTax"]);
                Billob.SHE_cess = Convert.ToDecimal(dr["SHE_cess"]);
                Billob.E_cess = Convert.ToDecimal(dr["E_cess"]);
                Billob.TCS = Convert.ToDecimal(dr["TCS"]);


                lstBill.Add(Billob);

            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstBill;


        }

        
    }
}
