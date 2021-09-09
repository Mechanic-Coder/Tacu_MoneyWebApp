using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public class TestModel
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Decimal Amount { get; set; }
        public string CR_DR { get; set; }
    }
}
