using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacuDataAccess.Models;

namespace TacuMoney.Models
{
    public class EditCategoryInputModel
    {
        public string Category { get; set; }
        public List<Category?> Names { get; set; }
    }
}
