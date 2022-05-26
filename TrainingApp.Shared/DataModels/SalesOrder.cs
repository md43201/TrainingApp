using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Shared.DataModels
{
    public class SalesOrder
    {
        public SalesOrder(int orderId,
                            DateTime orderDate,
                            Person customer,
                            IEnumerable<SalesOrderDetailLine> salesDetailLines)
        {
            OrderId = orderId;
            OrderDate = orderDate;
            Customer = customer;
            SalesDetailLines = salesDetailLines;
        }

        public int OrderId { get; }
        public DateTime OrderDate { get; }
        public Person Customer { get; }
        public IEnumerable<SalesOrderDetailLine> SalesDetailLines { get; }
    }
}
