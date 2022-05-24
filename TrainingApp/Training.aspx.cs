using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using TrainingApp.Shared.DataModels;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp
{
    public partial class Training : BasePageWithIoC
    {
        private IProductInformation _productInformation = null;
        private ISalesInformation _salesInformation = null;

        [SetterProperty]
        public IProductInformation ProductInformation
        {
            get { return _productInformation; }
            set
            {
                _productInformation = value;
                this.ProductDetails.ProductInformation = _productInformation;
            }
        }

        [SetterProperty]
        public ISalesInformation SalesInformation
        {
            get { return _salesInformation; }
            set
            {
                _salesInformation = value;
                this.ProductDetails.SalesInformation = _salesInformation;
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
            return ProductInformation.GetProductCategories();
        }

        private IEnumerable<ProductSubCategory> GetProductSubCategories(int categoryId)
        {
            return ProductInformation.GetProductSubCategories(categoryId);
        }

        private IEnumerable<Product> GetProducts(int subCategoryId)
        {
            return ProductInformation.GetProducts(subCategoryId);
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