using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public class TabModel : BasicNavModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Table { get; set; }
        public string Category { get; set; }
    }
}
