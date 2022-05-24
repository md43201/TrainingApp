using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;

namespace TrainingApp.Shared.Interfaces
{
    public interface IProductData
    {
        IEnumerable<ProductCategory> GetProductCategories();
        IEnumerable<ProductSubCategory> GetProductSubCategories(int categoryId);
        IEnumerable<Product> GetProducts(int subCategoryId);
        Product GetProduct(int productId);
    }
}
