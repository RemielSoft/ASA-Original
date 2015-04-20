using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using DOM;

namespace BAL
{
    public class ProductMasterBAL
    {
        ProductMasterDAL productDAL =new ProductMasterDAL();
        public int Createproductmaster(ProductMasterDOM productDOM)
        {
            int id = 0;
          
               return id= productDAL.CreateProductMaster(productDOM);
           
           
        }
        public List<ProductMasterDOM> ReadProductDetails(int? id, int? itemId, int? subitemId)
        {
            List<ProductMasterDOM> lstProductMasterDom = new List<ProductMasterDOM>();

            lstProductMasterDom = productDAL.ReadProductDetails(id, itemId, subitemId);

            return lstProductMasterDom;
        }
        public void DeleteProductDetails(int id, string modifiedBy)
        {


            productDAL.DeleteProductDetails(id, modifiedBy);
           

        }
        public void UpdateProductMaster(ProductMasterDOM prodom)
        {
            productDAL.UpdateProductMaster(prodom);
           
        }
    }
}
