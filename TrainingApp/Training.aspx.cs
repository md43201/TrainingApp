using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainingApp.Business;
using TrainingApp.Data.Mock;
using TrainingApp.Data.SqlServer;
using TrainingApp.Shared.DataModels;
using TrainingApp.Shared.Interfaces;
using TrainingApp.StructureMap;

namespace TrainingApp
{
    public partial class Training : BasePageWithIoC
    {
        private ISalesBusiness _salesBusiness = null;

        [SetterProperty]
        public ISalesBusiness SalesBusiness
        {
            get { return _salesBusiness; }
            set
            {
                _salesBusiness = value;
                this.ProductDetails.SalesBusiness = _salesBusiness;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //this.welcomeMessage.Text = DateTime.Now.ToString("HH:mm:ss.fff");

                LoadProductCategories();
            }
        }

        protected void LoadProductCategories()
        {
            //var salesData = new SalesData();
            //var sqlCategories = salesData.GetProductCategories();
            //var category = sqlCategories.First();

            //var sqlSubcategories = salesData.GetProductSubCategories(category.CategoryId);
            //var subCategory = sqlSubcategories.First();

            //var sqlProducts = salesData.GetProducts(subCategory.SubCategoryId);

            var categories = new List<ProductCategory>
            {
                new ProductCategory(0, "Select Category")
            };

            categories.AddRange(GetProductCategories());

            this.ddlCategories.DataSource = categories;
            this.ddlCategories.DataValueField = "CategoryId";
            this.ddlCategories.DataTextField = "CategoryName";
            this.ddlCategories.DataBind();
        }

        //protected void btnUpdateText_Click(object sender, EventArgs e)
        //{
        //    this.lblDataEntry.Text = this.txtDataEntry.Text;
        //}

        private IEnumerable<ProductCategory> GetProductCategories()
        {
            return SalesBusiness.GetProductCategories();
        }

        private IEnumerable<ProductSubCategory> GetProductSubCategories(int categoryId)
        {
            return SalesBusiness.GetProductSubCategories(categoryId);
        }

        private IEnumerable<Product> GetProducts(int subCategoryId)
        {
            return SalesBusiness.GetProducts(subCategoryId);
        }

        protected void ddlCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var subCategories = new List<ProductSubCategory>
            {
                new ProductSubCategory(0, 0, "Select Subcategory")
            };

            if (int.TryParse(ddlCategories.SelectedValue, out int productCategory))
            {
                subCategories.AddRange(GetProductSubCategories(productCategory));
            }

            ddlSubCategories.DataSource = subCategories;
            ddlSubCategories.DataValueField = "SubCategoryId";
            ddlSubCategories.DataTextField = "SubCategoryName";
            ddlSubCategories.DataBind();
            ddlSubCategories.Visible = true;
        }

        protected void ddlSubCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            var products = new List<Product>();

            if (int.TryParse(ddlSubCategories.SelectedValue, out int subCategory))
            {
                products.AddRange(GetProducts(subCategory));
            }

            gvProducts.DataSource = products;
            gvProducts.DataBind();
            gvProducts.Visible = true;
        }

        protected void gvProducts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (!int.TryParse(e.CommandArgument.ToString(), out int rowNumber))
            {
                return;
            }

            if (!int.TryParse(gvProducts.DataKeys[rowNumber].Value.ToString(), out int productId))
            {
                return;
            }

            this.ProductDetails.Initialize(productId);
            this.productDetailsContainer.Visible = true;
        }
    }
}