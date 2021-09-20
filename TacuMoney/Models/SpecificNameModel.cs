using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public class SpecificNameModel
    {
        public IQueryable<GenericRecordModel> Records { get; set; }
        public string Name { get; set; }
        public string Table { get; set; }
    }
}
