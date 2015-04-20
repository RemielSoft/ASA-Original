using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOM
{
    [Serializable]
    public class Ledger_Detail:Base
    {
        public int billId { get; set; }
        public string EntryType { get; set; }
        public string EntryDetail { get; set; }
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
    }
}
