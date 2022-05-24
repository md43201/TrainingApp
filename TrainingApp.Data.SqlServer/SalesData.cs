using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp.Data.SqlServer
{
    public class SalesData : DataBase, ISalesData
    {
        public IEnumerable<SalesOrder> GetOrdersForProduct(int productId)
        {
            List<SalesOrder> orders = new List<SalesOrder>();
            List<SalesOrderDetailLine> orderDetailLines = new List<SalesOrderDetailLine>();

            using (var connection = GetOpenConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    command.CommandText = "dbo.GetProductSalesOrders";
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
                        int orderQty = ordersReader.GetInt16(indexOrderQty);
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
