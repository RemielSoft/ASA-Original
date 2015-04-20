using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOM
{
    [Serializable]
    public class Bill:Base
    {
        public int BillId { get; set; }

        public DateTime BillDate { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public string CompanyName { get; set; }

        public int CompanyId { get; set; }

        public string CompanyAddress { get; set; }

        public string Corporate { get; set; }

        public string ClientName { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal ServiceCharge { get; set; }

        public decimal ServiceTax { get; set; }
        
        public decimal GrandTotal { get; set; }

        public string EntryType { get; set; }

        public string EntryDetail { get; set; }

        public decimal Debit { get; set; }

        public decimal Credit { get; set; }

        public DateTime PaymentDate { get; set; }

        public decimal Freight { get; set; }

        public decimal TCS { get; set; }

        public decimal E_cess { get; set; }

        public decimal SHE_cess { get; set; }

        public string Vehicle { get; set; }

        public decimal CstVATValue { get; set; }


        public int CstVatid { get; set; }
        public int CstValue { get; set; }
        public int VatValue { get; set; }
        public decimal TotalWithTax { get; set; }

        public int TaxId { get; set; }

        public BillDetail billdetail { get; set; }
        public string  QuantityMeasurement { get; set; }
        public int isDeleted { get; set; }
        

       
    }
}
