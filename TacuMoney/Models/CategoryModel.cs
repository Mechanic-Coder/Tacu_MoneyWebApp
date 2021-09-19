using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public class CategoryModel
    {
        public IQueryable<GenericRecordModel> Records { get; set; }
        public string Category { get; set; }
        public string FilterBy { get; set; }
        
        public bool Reverse { get; set; }
    }

}
