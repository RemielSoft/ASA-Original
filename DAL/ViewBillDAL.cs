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
    public class ViewBillDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;


        public List<Bill> ShowBillDetails(DateTime? fromDate, DateTime? toDate, Int32? companyId, int? billid)
        {
            SqlCommand cmd = new SqlCommand("procReadBill ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr;
           
            
            
            if (fromDate == DateTime.MinValue)
            {
                cmd.Parameters.Add(new SqlParameter("@in_fromDate", DBNull.Value));
            }
            else { cmd.Parameters.Add(new SqlParameter("@in_fromDate", fromDate)); }
            if (toDate == DateTime.MinValue)
            {
                cmd.Parameters.Add(new SqlParameter("@in_toDate", DBNull.Value));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@in_toDate", toDate)); 
            }

            if (companyId == 0)
            {
                cmd.Parameters.Add(new SqlParameter("@in_Company_Id", DBNull.Value));
            }
            else { cmd.Parameters.Add(new SqlParameter("@in_Company_Id", companyId)); }
           // cmd.Parameters.Add(new SqlParameter("@in_toDate", toDate));
            if (billid != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@In_billId", billid));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@In_billId",DBNull.Value));
            }



            dr = cmd.ExecuteReader();
            List<Bill> lstBill = new List<Bill>();
            while (dr.Read())
            {
                decimal servicetaxvalue;
                decimal Ecess;
                decimal shecess;
                decimal cstvat;
                int CstVAtId;
                
                cstvat = Convert.ToDecimal(dr["TotalWithTax"]) * Convert.ToDecimal(dr["CST_VAT_VALUE"]) / 100;
                servicetaxvalue =Math.Round((Convert.ToDecimal(dr["total_amt"]) * Convert.ToDecimal(dr["service_tax"])) / 100,0);
                Ecess = servicetaxvalue * Convert.ToDecimal(dr["E_CESS"]) / 100;
                shecess = servicetaxvalue * Convert.ToDecimal(dr["SHE_CESS"]) / 100;
                Bill billDom = new Bill();
                billDom.isDeleted = Convert.ToInt32(dr["isdeleted"].ToString());
                billDom.BillId = Convert.ToInt32(dr["bill_id"].ToString());
                billDom.BillDate = Convert.ToDateTime(dr["bill_date"].ToString());
                billDom.CompanyName = dr["company_name"].ToString();
                billDom.CompanyAddress = dr["company_address"].ToString();
                billDom.Corporate = dr["corporate"].ToString();
                billDom.ClientName = dr["client_name"].ToString();
                billDom.TotalAmount = Convert.ToDecimal(dr["total_amt"].ToString());
                billDom.ServiceCharge = Convert.ToDecimal(dr["service_charge"].ToString());
                billDom.ServiceTax =(servicetaxvalue);
                billDom.GrandTotal =Math.Round( Convert.ToDecimal(dr["grand_total"].ToString()));
                billDom.CreatedBy = dr["created_by"].ToString();
                billDom.ModifiedBy = dr["modified_by"].ToString();
                billDom.TotalWithTax =Math.Round(Convert.ToDecimal(dr["TotalWithTax"]));
                billDom.E_cess = Math.Round(Ecess);
                billDom.SHE_cess = Math.Round(shecess);
                billDom.CstVATValue = Math.Round(cstvat);
                CstVAtId = Convert.ToInt16(dr["CST_VAT_ID"]);
                if (CstVAtId == 1)
                {
                    billDom.VatValue = 0;
                    billDom.CstValue =Convert.ToInt32(cstvat);
                }
                else 
                {
                    billDom.CstValue = 0;
                    billDom.VatValue = Convert.ToInt32(cstvat);
                }
                
                   
                lstBill.Add(billDom);
            }
            con.Close();
            return lstBill;
        }

        public Bill ReadBillById(int id) 
        {
            SqlCommand cmd = new SqlCommand("procReadBillById ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr;
            cmd.Parameters.Add(new SqlParameter("@in_bill_id", id));
            dr = cmd.ExecuteReader();

            Bill billDom = null; 
            while (dr.Read())
            {
               
               
                billDom = new Bill();
                billDom.BillId = Convert.ToInt32(dr["bill_id"].ToString());
                billDom.BillId = Convert.ToInt32(dr["bill_id"]);
                billDom.CompanyName = dr["CompanyName"].ToString();
                billDom.ClientName = dr["client_name"].ToString();
                billDom.CompanyAddress = dr["company_address"].ToString();
                billDom.Corporate = dr["corporate"].ToString();
                billDom.TotalAmount = Convert.ToDecimal(dr["total_amt"]);
                billDom.ServiceTax = Convert.ToDecimal(dr["service_tax"]);               
                billDom.GrandTotal = Math.Round(Convert.ToDecimal(dr["grand_total"].ToString()));
                billDom.CreatedBy = dr["created_by"].ToString();
                billDom.CreatedDate =Convert.ToDateTime( dr["created_date"]);
                //billDom.ModifiedBy = dr["modified_by"].ToString();
                //billDom.ModifiedDate = Convert.ToDateTime(dr["modified_date"]);
                billDom.SHE_cess = Convert.ToDecimal(dr["SHE_CESS"]);
                billDom.E_cess = Convert.ToDecimal(dr["E_CESS"]);
                billDom.CstVatid = Convert.ToInt32(dr["CST_VAT_ID"]);
                billDom.CstVATValue = Convert.ToDecimal(dr["CST_VAT_VALUE"]);
                billDom.Freight = Convert.ToDecimal(dr["Freight"]);
                billDom.TCS = Convert.ToDecimal(dr["TCS"]);
                billDom.TotalWithTax =Math.Round( Convert.ToDecimal(dr["TotalWithTax"]));
                billDom.Vehicle = (dr["Vehicle_no"].ToString());
                billDom.CstVATValue = Convert.ToDecimal(dr["CST_VAT_VALUE"]);
               // billDom.CstVatName = billDom.CstVATValue.ToString() + "  " + dr["cstvat"].ToString(); 
               
                
            }
            con.Close();
            return billDom;

        }
        public List<BillDetail> ReadBilldetailByitemname(string itemName)
        {
            List< BillDetail> lstbilldeatil = new List<BillDetail>();
           
            SqlCommand cmd = new SqlCommand("ProcReadbilldetailbyItemName ", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr;
            cmd.Parameters.Add(new SqlParameter("@In_itemName", itemName));
            dr = cmd.ExecuteReader();

            
            while (dr.Read())
            {
                BillDetail billdeatil = new BillDetail();
                billdeatil.ItemName = dr["item_description"].ToString();
                lstbilldeatil.Add(billdeatil);
            }
            con.Close();
            return lstbilldeatil;

        }

        public void UpdateBillById(Bill bill)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procUpdateItemByBillId";
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_bill_id", bill.BillId));
            cmd.Parameters.Add(new SqlParameter("@in_total_amt", bill.TotalAmount));
            cmd.Parameters.Add(new SqlParameter("@in_service_tax", bill.ServiceTax));
            cmd.Parameters.Add(new SqlParameter("@in_grand_total", bill.GrandTotal));
            cmd.Parameters.Add(new SqlParameter("@E_cess", bill.E_cess));
            cmd.Parameters.Add(new SqlParameter("@She_cess", bill.SHE_cess));
            cmd.Parameters.Add(new SqlParameter("@total_with_tax", bill.TotalWithTax));
            cmd.Parameters.Add(new SqlParameter("@vat", bill.CstVATValue));
            cmd.Parameters.Add(new SqlParameter("@freight", bill.Freight));
            cmd.Parameters.Add(new SqlParameter("@tcs", bill.TCS));
            cmd.Parameters.Add(new SqlParameter("@vechicleno", bill.Vehicle));

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }


        //Create Ledger
        public void CreateLedger(Bill bill)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procCreateLedger";
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_company_id", bill.CompanyId));
            cmd.Parameters.Add(new SqlParameter("@in_entry_type", bill.EntryType));
            cmd.Parameters.Add(new SqlParameter("@in_entry_detail", bill.EntryDetail));
            cmd.Parameters.Add(new SqlParameter("@in_debit", bill.Debit));
            cmd.Parameters.Add(new SqlParameter("@in_payment_date", DateTime.Now));

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }

        public List<Bill> ShowBillReportsDetails(DateTime fromDate, DateTime toDate, int? billdeatail, Int32? companyId, string itemname ,string subitem)
        {
           // SqlCommand cmd = new SqlCommand("select B.bill_id,B.bill_date,C.CompanyName,quantity,BD.item_description,BD.rate,BD.amount,BD.ItemName,BD.measurement from Bill B inner join	Bill_Detail BD on B.bill_id = BD.bill_id inner join	Company_Master C on B.company_id = C.CompanyId where (B.bill_date >= '" + fromDate + "')and(B.bill_date <= '" + toDate + "')and(BD.bill_id = '" + billdeatail + "')and(C.CompanyId = '" + companyId + "')and(bd.ItemName='" + itemname + "')and (bd.item_description in (" + subitem + ")) ", con);
            SqlCommand cmd = new SqlCommand("procRead_Bill_And_Bill_Detail_For_Report ", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.Add(new SqlParameter("@in_FromDate", fromDate));
            cmd.Parameters.Add(new SqlParameter("@in_ToDate", toDate));
            if (billdeatail != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@in_BillDetail_Id", billdeatail));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@in_BillDetail_Id", null));
            }

            cmd.Parameters.Add(new SqlParameter("@in_Company_Id", companyId));
            if (itemname != null)
            {
                cmd.Parameters.Add(new SqlParameter("@itemname", itemname));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@itemname", DBNull.Value));
            }

            con.Open();
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            List<Bill> lstBill = new List<Bill>();
            while (dr.Read())
            {
               

                Bill billDom = new Bill();
                billDom.billdetail = new BillDetail();
                billDom.BillId = Convert.ToInt16(dr["bill_id"].ToString());
                billDom.BillDate = Convert.ToDateTime(dr["bill_date"].ToString());
                billDom.CompanyName = dr["CompanyName"].ToString();
               // billDom.CompanyAddress = dr["CompanyAddress"].ToString();
                billDom.billdetail.ItemDescription = dr["item_description"].ToString();
                billDom.billdetail.ItemName = dr["ItemName"].ToString();
                billDom.billdetail.Quantity =Convert.ToDecimal(dr["quantity"]);
               // billDom.QuantityMeasurement = dr["quantity"].ToString() + dr["measurement"].ToString();
               // billDom.TotalAmount = Convert.ToDecimal(dr["Total_amt"].ToString());                
               // billDom.billdetail.Rate = Convert.ToDecimal(dr["rate"] .ToString());
                billDom.billdetail.Amount = Convert.ToDecimal(dr["amount"].ToString());
                billDom.billdetail.measrment = dr["rate"].ToString()+"/" + dr["measurement"].ToString();
                //billDom.billdetail.Pieces = Convert.ToInt32(dr["Pieces"]);

                if (dr["ItemName"].ToString() == "Die Casted Rotor")
                {
                    if (dr["measurement"].ToString() == "Piece")
                    {
                        billDom.billdetail.Pieces = Convert.ToInt32(dr["quantity"]);
                        billDom.QuantityMeasurement = "-";
                    }
                }
                else
                {
                    if (dr["measurement"].ToString() == "Pieces" || dr["measurement"].ToString() == "Pieces")
                    {
                        billDom.billdetail.Pieces = Convert.ToInt32(dr["quantity"]);
                        billDom.QuantityMeasurement = "-";


                    }

                    else if (dr["measurement"].ToString() == "Kg")
                    {
                        billDom.QuantityMeasurement = dr["quantity"].ToString();
                        billDom.billdetail.Pieces = Convert.ToInt32(dr["Pieces"]);

                    }
                    else
                    {
                        billDom.QuantityMeasurement = dr["quantity"].ToString();
                        billDom.billdetail.Pieces = Convert.ToInt32(dr["Pieces"]);
                    }
                }
              
                lstBill.Add(billDom);
            }
            con.Close();
            return lstBill;
        }

        public List<Bill> ReadLedgerByCompanyID(int companyID)
        {
            cmd = new SqlCommand("procReadLedgerByCopanyID", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_company_id", companyID));
            SqlDataReader dr = cmd.ExecuteReader();
            List<Bill> lstBill = new List<Bill>();
            while (dr.Read())
            {
                Bill ob = new Bill();
                if (!string.IsNullOrEmpty(dr["bill_id"].ToString()))
                {
                    ob.BillId = Convert.ToInt32(dr["bill_id"]);
                }
                if (!string.IsNullOrEmpty(dr["company_id"].ToString()))
                {
                    ob.CompanyId = Convert.ToInt32(dr["company_id"]);
                }
                
                if (!string.IsNullOrEmpty(dr["client_name"].ToString()))
                {
                    ob.ClientName = dr["client_name"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["CompanyName"].ToString()))
                {
                    ob.CompanyName = dr["CompanyName"].ToString();
                }
               
                if (!string.IsNullOrEmpty(dr["total_amt"].ToString()))
                {
                    ob.TotalAmount = Convert.ToDecimal(dr["total_amt"]);
                }
                if (!string.IsNullOrEmpty(dr["service_charge"].ToString()))
                {
                    ob.ServiceCharge = Convert.ToDecimal(dr["service_charge"]);
                }
                if (!string.IsNullOrEmpty(dr["service_tax"].ToString()))
                {
                    ob.ServiceTax = Convert.ToDecimal(dr["service_tax"]);
                }
                if (!string.IsNullOrEmpty(dr["grand_total"].ToString()))
                {
                    ob.GrandTotal = Convert.ToDecimal(dr["grand_total"]);
                }
                if (!string.IsNullOrEmpty(dr["company_address"].ToString()))
                {
                    ob.CompanyAddress = dr["company_address"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["corporate"].ToString()))
                {
                    ob.Corporate = dr["corporate"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["entry_type"].ToString()))
                {
                    ob.EntryType = dr["entry_type"].ToString();
                }
                else
                {
                    ob.EntryType ="Bill No. - "+ dr["bill_id"].ToString(); 
                }
                if (!string.IsNullOrEmpty(dr["entry_detail"].ToString()))
                {
                    ob.EntryDetail = dr["entry_detail"].ToString();
                }
                else
                {
                    ob.EntryDetail = dr["client_name"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["debit"].ToString()))
                {
                    ob.Debit = Convert.ToDecimal(dr["debit"]);
                }
                if (!string.IsNullOrEmpty(dr["grand_total"].ToString()))
                {
                    ob.Credit = Convert.ToDecimal(dr["grand_total"]);
                }
                if (!string.IsNullOrEmpty(dr["payment_date"].ToString()))
                {
                    ob.BillDate = Convert.ToDateTime(dr["payment_date"]);
                }
                
                if (!string.IsNullOrEmpty(dr["created_by"].ToString()))
                {
                    ob.CreatedBy = dr["created_by"].ToString();
                }
                if (!string.IsNullOrEmpty(dr["created_date"].ToString()))
                {
                    ob.BillDate = Convert.ToDateTime(dr["created_date"]);
                }
                if (!string.IsNullOrEmpty(dr["modified_by"].ToString()))
                {
                    ob.ModifiedBy = dr["modified_by"].ToString();
                }

                if (!string.IsNullOrEmpty(dr["modified_date"].ToString()))
                {
                    ob.ModifiedDate = Convert.ToDateTime(dr["modified_date"]);
                }
               
                
                
                lstBill.Add(ob);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstBill;

        }
    }
}
