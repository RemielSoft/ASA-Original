using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DOM
{
    [Serializable]
   public class ProductMasterDOM
    {
        public int ProductId { get; set; }
        public string ItemDescription { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string  Createdby { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Modifiedby { get; set; }
        public DateTime Modifieddate { get; set; }
        public int SubItemId { get; set; }
        public string  SubitemName { get; set; }
        public string AllItem { get; set; }
    }
}
