using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacuDataAccess.Models;

namespace TacuMoney.Models
{
    public class ManageCategorys
    {
        public string SelectedCategory { get; set; }
        public IQueryable<Category> Terms { get; set; }
        public IQueryable<string> Categorys { get; set; }
    }
}
