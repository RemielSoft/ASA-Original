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
    
    public class ProductMasterDAL
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conStr"].ToString());
        SqlCommand cmd = null;
        public int CreateProductMaster(ProductMasterDOM productdom)
        {
            int id=0;
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procCreateProductMaster";
            cmd.Connection = con;
            con.Open();
            cmd.Parameters.Add(new SqlParameter("@ItemDescription", productdom.ItemDescription));
            cmd.Parameters.Add(new SqlParameter("@itemId", productdom.ItemId));
            cmd.Parameters.Add(new SqlParameter("@createdby", productdom.Createdby));
            cmd.Parameters.Add(new SqlParameter("@createddate", DateTime.Now));
            cmd.Parameters.Add(new SqlParameter("@sub_itemId", productdom.SubItemId));
            cmd.Parameters.Add(new SqlParameter("@out_productId", DbType.Int32));

            cmd.Parameters["@out_productId"].Direction = ParameterDirection.Output;
            

            cmd.ExecuteNonQuery();
            id = Convert.ToInt32(cmd.Parameters["@out_productId"].Value);
            cmd.Dispose();
            con.Close();
            return id;

        }
        public List<ProductMasterDOM> ReadProductDetails(int? id ,int? itemId,int? subitemId)
        {
            cmd = new SqlCommand("procReadProductMaster", con);

            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            if (id != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@in_Productid", id));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@in_Productid", null));

            }
            if (itemId != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@In_itemId", itemId));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@In_itemId", null));

            }
            
            if (subitemId != 0)
            {
                cmd.Parameters.Add(new SqlParameter("@In_subiemid", subitemId));
            }
            else
            {
                cmd.Parameters.Add(new SqlParameter("@In_subiemid", null));

            }

            

                SqlDataReader dr = cmd.ExecuteReader(); 
           
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            while (dr.Read())
            {
                ProductMasterDOM productMasterDom = new ProductMasterDOM();
                productMasterDom.ProductId = Convert.ToInt32(dr["Product_Id"]);
                productMasterDom.ItemDescription = dr["Item_Description"].ToString();
               // productMasterDom.SubitemName = "Stator Stamping";


               

                productMasterDom.ItemId = Convert.ToInt32(dr["Item_Id"]);
                productMasterDom.SubItemId = Convert.ToInt32(dr["Sub_ItemId"]);
                if (productMasterDom.ItemId == 1)
                {
                    productMasterDom.ItemName = "Electrical Stampings";
                }
                else if (productMasterDom.ItemId==2)
                {
                    productMasterDom.ItemName = "Die Casted Rotor";
                }
                if (productMasterDom.ItemId == 3)
                {
                    productMasterDom.ItemName = "Waste";
                }
                if (productMasterDom.SubItemId == 4)
                {
                    productMasterDom.SubitemName = "Stator Stamping";
                }
                if ((productMasterDom.SubItemId == 5))
                {
                    productMasterDom.SubitemName = "Rotor Stamping";
                }
                productMasterDom.AllItem = productMasterDom.SubitemName + productMasterDom.ItemDescription;
                lstproductMasterDom.Add(productMasterDom);
            }
            dr.Close();
            dr.Dispose();
            con.Close();
            return lstproductMasterDom;
        }
        public void DeleteProductDetails(int id, string modifiedBy)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procDeletePrductMaster";
            cmd.Connection = con;
            con.Open();

            cmd.Parameters.Add(new SqlParameter("@in_Product_Id", id));
            cmd.Parameters.Add(new SqlParameter("@in_Modified_By", modifiedBy));


            cmd.ExecuteNonQuery();

            cmd.Dispose();
            con.Close();

        }
        public void UpdateProductMaster(ProductMasterDOM product)
        {
            cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "procUpdateproductMaster";
            cmd.Connection = con;
            con.Open();


            cmd.Parameters.Add(new SqlParameter("@productId", product.ProductId));
            cmd.Parameters.Add(new SqlParameter("@ItemDescripation", product.ItemDescription));
            cmd.Parameters.Add(new SqlParameter("@itemid", product.ItemId));
            cmd.Parameters.Add(new SqlParameter("@subitemid", product.SubItemId));

            cmd.Parameters.Add(new SqlParameter("@modifiedby", product.Modifiedby));
            
          
           

            cmd.ExecuteNonQuery();

            
            cmd.Dispose();
            con.Close();
            
        }

    }
}
