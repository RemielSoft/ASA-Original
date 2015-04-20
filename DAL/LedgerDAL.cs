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
    public class LedgerDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;
        public void CreateLedger(Ledger_Detail ledger)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procCreateLedger";
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_bill_id", ledger.billId));
            cmd.Parameters.Add(new SqlParameter("@in_entry_type", ledger.EntryType));
            cmd.Parameters.Add(new SqlParameter("@in_entry_detail", ledger.EntryDetail));
            cmd.Parameters.Add(new SqlParameter("@in_debit", ledger.Debit));

           // cmd.Parameters.Add(new SqlParameter("@in_created_by", ledger.CreatedBy));
            cmd.Parameters.Add(new SqlParameter("@in_created_date",DateTime.Now));

            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();

        }

        public List<Ledger_Detail> ReadLedger(int id)
        {
            cmd = new SqlCommand("procReadLedger",con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@in_bill_id", id));
            SqlDataReader dr = cmd.ExecuteReader();
            List<Ledger_Detail> lstLedger = new List<Ledger_Detail>();
            while (dr.Read())
            {
                Ledger_Detail ob = new Ledger_Detail();
                ob.billId = Convert.ToInt32(dr["bill_id"]);
                ob.CreatedDate = Convert.ToDateTime(dr["created_date"]);
                ob.EntryType = dr["entry_type"].ToString();
                ob.EntryDetail = dr["entry_detail"].ToString();
                ob.Debit = Convert.ToDecimal(dr["debit"]);
               // ob.Credit = Convert.ToDecimal(dr["credit"]);
                lstLedger.Add(ob);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstLedger;

        }
    }
}
