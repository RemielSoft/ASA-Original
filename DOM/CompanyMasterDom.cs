using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOM
{
    [Serializable]
   public class CompanyMasterDom:Base
    {
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public int chargesId { get; set; }     
        public string ChargesName { get; set; }       
        public string Phone { get; set; }
        public decimal Chargesvalue { get; set; }
        public ContactInfo Information { get; set; }
    }
}
