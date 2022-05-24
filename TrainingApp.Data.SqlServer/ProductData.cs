using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp.Data.SqlServer
{
    public class ProductData : DataBase, IProductData
    {
        public Product GetProduct(int productId)
        {
            Product product = null;

            using (var connection = GetOpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = $"SELECT * FROM Production.Product WHERE ProductID = {productId}";
                    var productsReader = command.ExecuteReader();

                    int indexId = productsReader.GetOrdinal("ProductID");
                    int indexSubcategoryId = productsReader.GetOrdinal("ProductSubcategoryID");
                    int indexName = productsReader.GetOrdinal("Name");
                    int indexColor = productsReader.GetOrdinal("Color");
                    int indexPrice = productsReader.GetOrdinal("ListPrice");

                    if (productsReader.Read())
                    {
                        int id = productsReader.GetInt32(indexId);
                        int owningSubCategoryId = productsReader.GetInt32(indexSubcategoryId);
                        string name = GetStringField(productsReader, indexName);
                        string color = GetStringField(productsReader, indexColor);
                        decimal price = productsReader.GetDecimal(indexPrice);

                        product = new Product(id, owningSubCategoryId, name, color, price);
                    }
                }
            }

            return product;
        }

        public IEnumerable<ProductCategory> GetProductCategories()
        {
            List<ProductCategory> categories = new List<ProductCategory>();

            using (var connection = GetOpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = "SELECT * FROM Production.ProductCategory";
                    var categoriesReader = command.ExecuteReader();

                    int indexId = categoriesReader.GetOrdinal("ProductCategoryID");
                    int indexName = categoriesReader.GetOrdinal("Name");

                    while (categoriesReader.Read())
                    {
                        int id = categoriesReader.GetInt32(indexId);
                        string name = categoriesReader.GetString(indexName);

                        categories.Add(new ProductCategory(id, name));
                    }
                }
            }

            return categories.ToArray();
        }

        public IEnumerable<Product> GetProducts(int subCategoryId)
        {
            List<Product> products = new List<Product>();

            using (var connection = GetOpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = $"SELECT * FROM Production.Product WHERE ProductSubcategoryID = {subCategoryId}";
                    var productsReader = command.ExecuteReader();

                    int indexId = productsReader.GetOrdinal("ProductID");
                    int indexSubcategoryId = productsReader.GetOrdinal("ProductSubcategoryID");
                    int indexName = productsReader.GetOrdinal("Name");
                    int indexColor = productsReader.GetOrdinal("Color");
                    int indexPrice = productsReader.GetOrdinal("ListPrice");

                    while (productsReader.Read())
                    {
                        int id = productsReader.GetInt32(indexId);
                        int owningSubCategoryId = productsReader.GetInt32(indexSubcategoryId);
                        string name = GetStringField(productsReader, indexName);
                        string color = GetStringField(productsReader, indexColor);
                        decimal price = productsReader.GetDecimal(indexPrice);

                        products.Add(new Product(id, owningSubCategoryId, name, color, price));
                    }
                }
            }

            return products
                    .ToArray();
        }

        public IEnumerable<ProductSubCategory> GetProductSubCategories(int categoryId)
        {
            List<ProductSubCategory> subCategories = new List<ProductSubCategory>();

            using (var connection = GetOpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.CommandText = $"SELECT * FROM Production.ProductSubCategory WHERE ProductCategoryID = {categoryId}";
                    var subCategoriesReader = command.ExecuteReader();

                    int indexId = subCategoriesReader.GetOrdinal("ProductSubcategoryID");
                    int indexCategoryId = subCategoriesReader.GetOrdinal("ProductCategoryID");
                    int indexName = subCategoriesReader.GetOrdinal("Name");

                    while (subCategoriesReader.Read())
                    {
                        int id = subCategoriesReader.GetInt32(indexId);
                        int owningCategoryId = subCategoriesReader.GetInt32(indexCategoryId);
                        string name = subCategoriesReader.GetString(indexName);

                        subCategories.Add(new ProductSubCategory(id, owningCategoryId, name));
                    }
                }
            }

            return subCategories
                        .ToArray();
        }
    }
}
