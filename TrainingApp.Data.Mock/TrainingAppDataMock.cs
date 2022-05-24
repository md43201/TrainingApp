using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp.Data.Mock
{
    public class TrainingAppDataMock : IProductData
    {
        private const int ProductCategoryKeyBikes = 1;
        private const int ProductCategoryKeyMoPeds = 2;
        private const int ProductCategoryKeySkateBoards = 3;

        private const int ProductSubCategoryKeyRacer = 1;
        private const int ProductSubCategoryKeyMountain = 2;
        private const int ProductSubCategoryKeyRoad = 3;
        private const int ProductSubCategoryKeyChild = 4;
        private const int ProductSubCategoryKeyAdult = 5;
        private const int ProductSubCategoryKeyShortboard = 6;
        private const int ProductSubCategoryKeyLongboard = 7;

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            return new List<ProductCategory>
            {
                new ProductCategory(ProductCategoryKeyBikes, "Bikes"),
                new ProductCategory(ProductCategoryKeyMoPeds, "MoPeds"),
                new ProductCategory(ProductCategoryKeySkateBoards, "SkateBoards")
            };
        }

        public IEnumerable<ProductSubCategory> GetProductSubCategories(int categoryId)
        {
            var productSubCategories = new List<ProductSubCategory>
            {
                new ProductSubCategory(ProductSubCategoryKeyRacer, ProductCategoryKeyBikes, "Racer"),
                new ProductSubCategory(ProductSubCategoryKeyMountain, ProductCategoryKeyBikes, "Mountain"),
                new ProductSubCategory(ProductSubCategoryKeyRoad, ProductCategoryKeyBikes, "Road"),
                new ProductSubCategory(ProductSubCategoryKeyChild, ProductCategoryKeyMoPeds, "Child"),
                new ProductSubCategory(ProductSubCategoryKeyAdult, ProductCategoryKeyMoPeds, "Adult"),
                new ProductSubCategory(ProductSubCategoryKeyShortboard, ProductCategoryKeySkateBoards, "Shortboard"),
                new ProductSubCategory(ProductSubCategoryKeyLongboard, ProductCategoryKeySkateBoards, "Longboard")
            };

            return productSubCategories
                        .Where(x => categoryId == 0 
                                    || x.CategoryId == categoryId)
                        .ToArray();
        }

        public IEnumerable<Product> GetProducts(int subCategoryId)
        {
            return GetProducts()
                    .Where(pr => subCategoryId == 0 
                                    || pr.SubCategoryId == subCategoryId)
                    .ToArray();
        }

        public Product GetProduct(int productId)
        {
            return GetProducts()
                    .FirstOrDefault(x => x.ProductId == productId); 
        }


        private IEnumerable<Product> GetProducts()
        {
            var products = new List<Product>
            {
                new Product(1, ProductSubCategoryKeyRacer, "BX101W", "White", 599.95m),
                new Product(2, ProductSubCategoryKeyRacer, "BX101B", "Black", 599.95m),
                new Product(3, ProductSubCategoryKeyRacer, "BX101R", "Red", 649.95m),
                new Product(4, ProductSubCategoryKeyMountain, "MB401MB", "Black", 419.95m),
                new Product(5, ProductSubCategoryKeyMountain, "MB401MW", "White", 419.95m),
                new Product(6, ProductSubCategoryKeyMountain, "MB401WB", "Black", 519.95m),
                new Product(7, ProductSubCategoryKeyMountain, "MB401WW", "White", 519.95m),
                new Product(8, ProductSubCategoryKeyRoad, "RB512CW", "White", 319.95m),
                new Product(9, ProductSubCategoryKeyRoad, "RB512AW", "White", 419.95m),
                new Product(10, ProductSubCategoryKeyShortboard, "Tykester", "Black", 89.95m),
                new Product(11, ProductSubCategoryKeyShortboard, "BabyFreak", "Red", 99.95m),
                new Product(12, ProductSubCategoryKeyLongboard, "LongBeach", "White", 249.95m),
                new Product(13, ProductSubCategoryKeyLongboard, "Monster", "Flame", 349.95m),
            };

            return products
                    .ToArray();
        }

        public IEnumerable<SalesOrder> GetOrdersForProduct(int productId)
        {
            throw new NotImplementedException();
        }
    }
}
