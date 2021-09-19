using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public class TabModel
    {
        public string Controller { get; set; }
        public string Action { get; set; }

        public string CurrentPage { get; set; }
    }
}
