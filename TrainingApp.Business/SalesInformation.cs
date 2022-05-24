using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;
using TrainingApp.Shared.Interfaces;

namespace TrainingApp.Business
{
    public class SalesInformation : ISalesInformation
    {
        private ISalesData _salesData = null;

        public SalesInformation(ISalesData salesData)
        {
            _salesData = salesData;
        }

        public IEnumerable<SalesOrder> GetSalesOrders(int productId)
        {
            return _salesData.GetOrdersForProduct(productId);
        }
    }
}
