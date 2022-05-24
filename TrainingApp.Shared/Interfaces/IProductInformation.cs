using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;

namespace TrainingApp.Shared.Interfaces
{
    public interface IProductInformation
    {
        IEnumerable<ProductCategory> GetProductCategories();

        IEnumerable<ProductSubCategory> GetProductSubCategories(int categoryId, string nameStartsWith = null);

        IEnumerable<Product> GetProducts(int subCategoryId, decimal? lowPrice = null, decimal? highPrice = null);

        Product GetProduct(int productId);
    }
}
