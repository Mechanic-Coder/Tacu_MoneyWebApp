using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TacuDataAccess.Models
{
    public class TaHuntington
    {
        public int Id  {get; set;}
        public DateTime Date { get; set; }
        public string Reference_Number { get; set; }
        public string Description { get; set; }
        public string Memo { get; set; }
        public decimal Amount { get; set; }
        public string Category_Name { get; set; }

    }
}
