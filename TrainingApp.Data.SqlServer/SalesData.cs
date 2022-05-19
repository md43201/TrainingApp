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
    public class SalesData : ISalesData
    {
        private string _connectionString = "Data Source=DESKTOP-4CAE1CU;Integrated Security=True;Initial Catalog=AdventureWorks2019;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

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

        private SqlConnection GetOpenConnection()
        {
            var connecction = new SqlConnection(_connectionString);
            connecction.Open();
            return connecction; 
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

        private string GetStringField(SqlDataReader reader, int fieldIndex)
        {
            if (reader.IsDBNull(fieldIndex))
                return null;

            return reader.GetString(fieldIndex);
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

        public IEnumerable<SalesOrder> GetOrdersForProduct(int productId)
        {
            List<SalesOrder> orders = new List<SalesOrder>();
            List<SalesOrderDetailLine> orderDetailLines = new List<SalesOrderDetailLine>();

            using (var connection = GetOpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("productId", productId);

                    var ordersReader = command.ExecuteReader();

                    int indexOrderId = ordersReader.GetOrdinal("SalesOrderID");
                    int indexOrderDate = ordersReader.GetOrdinal("OrderDate");
                    int indexProductId = ordersReader.GetOrdinal("ProductID");
                    int indexProductName = ordersReader.GetOrdinal("ProductName");
                    int indexOrderQty = ordersReader.GetOrdinal("OrderQty");
                    int indexListPrice = ordersReader.GetOrdinal("ListPrice");
                    int indexLineTotal = ordersReader.GetOrdinal("LineTotal");

                    int currentOrderId = 0;

                    int orderId = 0;
                    DateTime orderDate = DateTime.MinValue;

                    while (ordersReader.Read())
                    {
                        orderId = ordersReader.GetInt32(indexOrderId);
                        orderDate = ordersReader.GetDateTime(indexOrderDate);
                        int dbProductId = ordersReader.GetInt32(indexProductId);
                        string productName = ordersReader.GetString(indexProductName);
                        int orderQty = ordersReader.GetInt32(indexOrderQty);
                        decimal listPrice = ordersReader.GetDecimal(indexListPrice);
                        decimal lineTotal = ordersReader.GetDecimal(indexLineTotal);

                        if (orderId != currentOrderId)
                        {
                            if (currentOrderId > 0)
                            {
                                orders.Add(new SalesOrder(orderId, orderDate, orderDetailLines));
                            }

                            currentOrderId = orderId;
                            orderDetailLines.Clear();
                        }

                        orderDetailLines.Add(new SalesOrderDetailLine(dbProductId, productName, orderQty, listPrice, lineTotal));
                    }

                    if (orderId > 0
                        && orderDetailLines.Any())
                    {
                        orders.Add(new SalesOrder(orderId, orderDate, orderDetailLines));
                    }
                }
            }

            return orders;
        }
    }
}
