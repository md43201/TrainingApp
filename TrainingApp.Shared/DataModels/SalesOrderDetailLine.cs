using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Shared.DataModels
{
    public class SalesOrderDetailLine
    {
        public SalesOrderDetailLine(int productId,
                                    string productName,
                                    int quantity,
                                    decimal productPrice,
                                    decimal lineItemAmount)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            ProductPrice = productPrice;
            LineItemAmount = lineItemAmount;
        }

        public int ProductId { get; }
        public string ProductName { get; }
        public int Quantity { get; }
        public decimal ProductPrice { get; }
        public decimal LineItemAmount { get; }
    }
}
