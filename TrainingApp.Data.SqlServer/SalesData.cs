using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;
using TrainingApp.Shared.Enums;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp.Data.SqlServer
{
    public class SalesData : DataBase, ISalesData
    {
        #region Private Classes

        private class OrderDataLine
        {
            public int OrderId { get; set; }
            public DateTime OrderDate { get; set; }
            public string CustomerAccountNumber { get; set; }
            public string CustomerLastName { get; set; }
            public string CustomerFirstName { get; set; }
            public string CustomerEmailAddress { get; set; }
            public string CustomerPhoneNumber { get; set; }
            public string CustomerPhoneType { get; set; }
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int OrderQty { get; set; }
            public decimal ListPrice { get; set; }
            public decimal LineTotal { get; set; }
        }

        #endregion

        #region ISalesData Implementation

        public IEnumerable<SalesOrder> GetOrdersForProduct(int productId)
        {
            List<OrderDataLine> orderDataLines = new List<OrderDataLine>();
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
                    int indexAccountNumber = ordersReader.GetOrdinal("AccountNumber");
                    int indexCustomerNameLast = ordersReader.GetOrdinal("CustomerLastName");
                    int indexCustomerNameFirst = ordersReader.GetOrdinal("CustomerFirstName");
                    int indexCustomerEmailAddress = ordersReader.GetOrdinal("CustomerEmailAddress");
                    int indexCustomerPhoneNumber = ordersReader.GetOrdinal("CustomerPhone");
                    int indexCustomerPhoneType = ordersReader.GetOrdinal("PhoneType");
                    int indexProductId = ordersReader.GetOrdinal("ProductID");
                    int indexProductName = ordersReader.GetOrdinal("ProductName");
                    int indexOrderQty = ordersReader.GetOrdinal("OrderQty");
                    int indexListPrice = ordersReader.GetOrdinal("ListPrice");
                    int indexLineTotal = ordersReader.GetOrdinal("LineTotal");

                    while (ordersReader.Read())
                    {
                        OrderDataLine line = new OrderDataLine
                        {
                            OrderId = ordersReader.GetInt32(indexOrderId),
                            OrderDate = ordersReader.GetDateTime(indexOrderDate),
                            CustomerAccountNumber = ordersReader.GetString(indexAccountNumber),
                            CustomerLastName = ordersReader.GetString(indexCustomerNameLast),
                            CustomerFirstName = ordersReader.GetString(indexCustomerNameFirst),
                            CustomerEmailAddress = ordersReader.GetString(indexCustomerEmailAddress),
                            CustomerPhoneNumber = ordersReader.GetString(indexCustomerPhoneNumber),
                            CustomerPhoneType = ordersReader.GetString(indexCustomerPhoneType),
                            ProductId = ordersReader.GetInt32(indexProductId),
                            ProductName = ordersReader.GetString(indexProductName),
                            OrderQty = ordersReader.GetInt16(indexOrderQty),
                            ListPrice = ordersReader.GetDecimal(indexListPrice),
                            LineTotal = ordersReader.GetDecimal(indexLineTotal)
                        };

                        orderDataLines.Add(line);
                    }
                }


                var distinctOrders = orderDataLines
                                        .GroupBy(odl => new
                                        {
                                            OrderId = odl.OrderId,
                                            OrderDate = odl.OrderDate,
                                            CustomerAccountNumber = odl.CustomerAccountNumber,
                                            CustomerLastName = odl.CustomerLastName,
                                            CustomerFirstName = odl.CustomerFirstName,
                                            CustomerEmailAddress = odl.CustomerEmailAddress
                                        })
                                        .Select(odl => odl.First());

                foreach(var order in distinctOrders)
                {
                    Person customer = new Person(order.CustomerAccountNumber, order.CustomerLastName, order.CustomerFirstName, order.CustomerEmailAddress);
                    AddCustomerPhoneNumbers(orderDataLines, order.OrderId, customer);

                    SalesOrder salesOrder = new SalesOrder(order.OrderId, 
                                                            order.OrderDate, 
                                                            customer, 
                                                            GetSalesOrderDetailLines(distinctOrders, order.OrderId));

                    orders.Add(salesOrder);
                }
            }

            return orders;
        }

        #endregion

        #region Private Methods

        private IEnumerable<SalesOrderDetailLine> GetSalesOrderDetailLines(IEnumerable<OrderDataLine> orderDataLines, int orderId)
        {
            var orders = new List<SalesOrderDetailLine>();

            foreach (var order in orderDataLines.Where(odl => odl.OrderId == orderId))
            {
                orders.Add(new SalesOrderDetailLine(order.ProductId, order.ProductName, order.OrderQty, order.ListPrice, order.LineTotal));
            }

            return orders.ToArray();
        }

        private void AddCustomerPhoneNumbers(IEnumerable<OrderDataLine> orderDataLines, int orderId, Person customer)
        {
            var customerPhoneNumbers = new List<PhoneNumber>();

            foreach (var order in orderDataLines
                        .Where(odl => odl.OrderId == orderId)
                        .GroupBy(odl => new { 
                                                OrderId = odl.OrderId,
                                                PhoneNumber = odl.CustomerPhoneNumber, 
                                                PhoneType = odl.CustomerPhoneType 
                                            })
                        .Select(odl => odl.First()))
            {
                customer.AddPhoneNumber(new PhoneNumber(order.CustomerPhoneNumber, 
                                                        GetPhoneNumberType(order.CustomerPhoneType)));
            }
        }

        private PhoneNumberType GetPhoneNumberType(string phoneNumberType)
        {
            PhoneNumberType numberType = PhoneNumberType.Unknown;

            switch(phoneNumberType)
            {
                case "Cell":
                    numberType = PhoneNumberType.Cell;
                    break;

                case "Home":
                    numberType = PhoneNumberType.Home;
                    break;

                case "Work":
                    numberType = PhoneNumberType.Work;
                    break;
            }

            return numberType;
        }

        #endregion
    }
}
