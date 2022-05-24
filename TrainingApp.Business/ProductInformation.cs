using StructureMap.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp.Business
{
    public class ProductInformation : IProductInformation
    {
        private IProductData _salesData = null;

        public ProductInformation(IProductData salesData)
        {
            _salesData = salesData;
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return _salesData.GetProductCategories().ToArray();
        }

        public IEnumerable<ProductSubCategory> GetProductSubCategories(int categoryId, string nameStartsWith = null)
        {
            var subCategories = _salesData.GetProductSubCategories(categoryId);

            if (!string.IsNullOrEmpty(nameStartsWith))
            {
                subCategories = subCategories.Where(sc => sc.SubCategoryName.StartsWith(nameStartsWith));
            }

            return subCategories.ToArray();
        }

        public IEnumerable<Product> GetProducts(int subCategoryId, decimal? lowPrice = null, decimal? highPrice = null)
        {
            var products = _salesData.GetProducts(subCategoryId);

            if (lowPrice.HasValue)
            {
                products = products.Where(pr => pr.Price >= lowPrice.Value).ToList();
            }

            if (highPrice.HasValue)
            {
                products = products.Where(pr => pr.Price <= highPrice.Value).ToList();
            }

            return products.ToArray();
        }

        public Product GetProduct(int productId)
        {
            return _salesData.GetProduct(productId); 
        }
    }
}
