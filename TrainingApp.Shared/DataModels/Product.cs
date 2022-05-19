using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Shared.DataModels
{
    public class Product
    {
        public Product(int productId,
                        int subCategoryId,
                        string productName,
                        string color,
                        decimal price)
        {
            ProductId = productId;
            SubCategoryId = subCategoryId;
            ProductName = productName;
            Color = color;
            Price = price;
        }

        public int ProductId { get; }
        public int SubCategoryId { get; }
        public string ProductName { get; }
        public string Color { get; }
        public decimal Price { get; }
    }
}
