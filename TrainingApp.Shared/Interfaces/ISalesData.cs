using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainingApp.Shared.DataModels;

namespace TrainingApp.Shared.Interfaces
{
    public interface ISalesData
    {
        IEnumerable<SalesOrder> GetOrdersForProduct(int productId);
    }
}
