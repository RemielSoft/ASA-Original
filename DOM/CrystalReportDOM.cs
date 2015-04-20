using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOM
{
    [Serializable]
    public class CrystalReportDOM:Base
    {
        public int BillId { get; set; }

        public DateTime BillDate { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }


        public string CompanyName { get; set; }

        public string CompanyAddress { get; set; }

        public string Corporate { get; set; }

        public string ClientName { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal ServiceCharge { get; set; }

        public decimal ServiceTax { get; set; }

        public decimal GrandTotal { get; set; }

        public string ItemDescription { get; set; }
        public decimal Quantity { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
    }
}
