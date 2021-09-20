using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public class CategoryModel : NavModel
    {
        public IQueryable<GenericRecordModel> Records { get; set; }
        public string Category { get; set; }
        public string FilterBy { get; set; }
        public string Table { get; set; }
        public bool Reverse { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string CurrentPage { get; set; }
    }

}
