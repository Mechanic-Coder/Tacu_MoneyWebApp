using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public class GroupedCategoryModel : CategoryModel
    {
        public new IEnumerable<IGrouping<string,GenericRecordModel>> Records { get; set; }
    }
}
