using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public class GenericRecordModel
    {
        public DateTime? PostedDate { get; set; }
        public string Description { get; set; }
        public double? Amount { get; set; }
        public List<string> Category { get; set; }
    }
}
