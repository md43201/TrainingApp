using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TrainingApp.Business;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp
{
    public partial class Products : System.Web.UI.UserControl
    {
        public IProductInformation ProductInformation { get; set; } 
        public ISalesInformation SalesInformation { get; set; } 

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //public void Initialize(ISalesBusiness trainingAppBusiness, int productId)
        //{
        //    SalesBusiness = trainingAppBusiness;

        //    var product = SalesBusiness.GetProduct(productId);

        //    if (product is null)
        //    {
        //        throw new InvalidOperationException("Invalid parameter passed - no Product found");
        //    }

        //    this.productName.Text = product.ProductName;
        //    this.productColor.Text = product.Color;
        //    this.productPrice.Text = product.Price.ToString("C");
        //}

        public void Initialize(int productId)
        {
            var product = ProductInformation.GetProduct(productId);

            if (product is null)
            {
                throw new InvalidOperationException("Invalid parameter passed - no Product found");
            }

            this.productName.Text = product.ProductName;
            this.productColor.Text = product.Color;
            this.productPrice.Text = product.Price.ToString("C");

            GetSalesInformation(productId);
        }

        private void GetSalesInformation(int productId)
        {
            var sales = SalesInformation.GetSalesOrders(productId);
        }
    }
}