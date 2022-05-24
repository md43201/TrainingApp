using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;

namespace TrainingApp.Shared.Interfaces
{
    public interface ISalesInformation
    {
        IEnumerable<SalesOrder> GetSalesOrders(int productId);
    }
}
