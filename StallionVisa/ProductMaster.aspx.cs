using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DOM;
using BAL;


namespace StallionVisa
{
    public partial class ProductMaster : System.Web.UI.Page
    {
        #region Global Variables

      
        ProductMasterBAL productBAL = new ProductMasterBAL();
        ProductMasterDOM productmasterDom = new ProductMasterDOM();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                
                ShowproductDetails();
                tdsubitemradio.Visible = false;
                tdsubitem.Visible = false;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
           
            Saveproductmaster();
            ShowproductDetails();
            Clear();
            
            //ShowCompanyDetails();
           // Clear();
        }
        private void ShowMessage(string message)
        {
            ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", message), true);
        }
        private void Saveproductmaster()
        {
            int id;
            productmasterDom.ItemDescription = txtitemdescription.Text.TrimStart();
            productmasterDom.ItemId =Convert.ToInt32( rbtnitem.SelectedItem.Value);
            if (!String.IsNullOrEmpty(rbtnsubitem.SelectedValue))
            {
                productmasterDom.SubItemId = Convert.ToInt32(rbtnsubitem.SelectedItem.Value);
            }
           
            

            productmasterDom.Createdby = "Admin";
          //  productmasterDom.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
           id= productBAL.Createproductmaster(productmasterDom);
           if (id>0)
           {
                ShowMessage("Record Saved Successfully");
           }
           else
           {
               ShowMessage("This Product Name Is Already Exist");
           }
            
        }
        protected void grdproduct_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);
            ViewState["CompanyId"] = id;
            if (e.CommandName == "Eddit")
            {
                List<ProductMasterDOM> lst = new List<ProductMasterDOM>();
                lst = productBAL.ReadProductDetails(id,0,0);
                txtitemdescription.Text = lst[0].ItemDescription;
                if (lst[0].ItemId==1)
                {
                    rbtnitem.SelectedValue = "1";
                }
                else if (lst[0].ItemId==2)
                {
                    rbtnitem.SelectedValue = "2";
                }
                else
                {
                    rbtnitem.SelectedValue = "3";    
                }
                if (lst[0].SubItemId==4)
                {
                    rbtnsubitem.SelectedValue = "4";
                    tdsubitem.Visible = true;
                    tdsubitemradio.Visible = true;
                }
                else if (lst[0].SubItemId == 5)
                {
                    rbtnsubitem.SelectedValue = "5";
                    tdsubitem.Visible = true;
                    tdsubitemradio.Visible = true;
                }
              
                btnUpdate.Visible = true;
                btnSave.Visible = false;
            }
            else if (e.CommandName == "Delet")
            {

                productBAL.DeleteProductDetails(id, "Admin");
                ShowproductDetails();
                Clear();
            }

            //ShowCompanyDetails();
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateCompanyMaster();
          
            Clear();
            ShowproductDetails();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        private void UpdateCompanyMaster()
        {
            
            ProductMasterDOM product = new ProductMasterDOM();
            product.ProductId = Convert.ToInt32(ViewState["CompanyId"]);
            product.ItemDescription = txtitemdescription.Text.Trim();
            product.ItemId = Convert.ToInt32(rbtnitem.SelectedItem.Value);
            product.Modifiedby = "Admin";
            if (!String.IsNullOrEmpty(rbtnsubitem.SelectedValue))
            {
                productmasterDom.SubItemId = Convert.ToInt32(rbtnsubitem.SelectedItem.Value);
            }
           
            // companyMasterDom.CreatedDate = Convert.ToDateTime(DateTime.Now.ToString("d"));
            productBAL.UpdateProductMaster(product);
           
               // ShowMessage("Record Updated Successfully");
           
        }
        private void ShowproductDetails()
        {
            List<ProductMasterDOM> lstproductMasterDom = new List<ProductMasterDOM>();
            lstproductMasterDom = productBAL.ReadProductDetails(0,0,0);
            grdproduct.DataSource = lstproductMasterDom;
            grdproduct.DataBind();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
            btnUpdate.Visible = false;
            btnSave.Visible = true;
        }
         private void Clear()
        {
            txtitemdescription.Text = string.Empty;
            rbtnitem.ClearSelection();
            rbtnitem.ClearSelection();
            rbtnsubitem.ClearSelection();
            tdsubitem.Visible = false;
            tdsubitemradio.Visible = false;
        }
        //private void ShowMessage(string message)
        //{
        //    ScriptManager.RegisterStartupScript(Page, this.GetType(), "Success", String.Format("alert('{0}');", message), true);
        //}
        //#endregion          

         protected void grdproduct_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdproduct.PageIndex = e.NewPageIndex;
            ShowproductDetails();
            
        }

         protected void rbtnitem_SelectedIndexChanged(object sender, EventArgs e)
         {
             if (rbtnitem.SelectedValue == "1")
             {
                 tdsubitem.Visible = true;
                 tdsubitemradio.Visible = true;
             }
             else 
             {
                 tdsubitem.Visible = false;
                 rbtnsubitem.ClearSelection();
                 tdsubitemradio.Visible = false;
             }
         }

        

         
    }

}