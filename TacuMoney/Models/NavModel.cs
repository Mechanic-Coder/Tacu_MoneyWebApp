using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TacuMoney.Models
{
    public interface NavModel : BasicNavModel
    {
        bool Reverse { get; set; }
        string FilterBy { get; set; }
    }
    public interface BasicNavModel
    {
        string Action { get; set; }
        string Controller { get; set; }
        string Table { get; set; }
        string Category { get; set; }
        


    }
}
