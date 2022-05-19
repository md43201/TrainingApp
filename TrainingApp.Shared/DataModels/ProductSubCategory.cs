using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingApp.Shared.DataModels
{
    public class ProductSubCategory
    {
        public ProductSubCategory(int subCategoryId,
                                    int categoryId,
                                    string subCategoryName)
        {
            SubCategoryId = subCategoryId;
            CategoryId = categoryId;
            SubCategoryName = subCategoryName;
        }

        public int SubCategoryId { get; }

        public int CategoryId { get; }

        public string SubCategoryName { get; }
    }
}
