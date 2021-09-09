using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacuDataAccess.Models;

namespace TacuMoney.Models
{
    public class CatagoryLists
    {
        public IEnumerable<Culoc> Restruant { get; set; }
        public IQueryable<Culoc> Gas { get; set; }
        public IQueryable<Culoc> StudentLoans { get; set; }
        public IQueryable<Culoc> Automotive { get; set; }
        public IQueryable<Culoc> Amazon { get; set; }
        public IQueryable<Culoc> Grocerys { get; set; }
        public IQueryable<Culoc> UtilitiesRent { get; set; }
        public IQueryable<Culoc> WithDrawls { get; set; }
        public IQueryable<Culoc> Merchandise { get; set; }
        public IQueryable<Culoc> BadHabit { get; set; }
        public IQueryable<Culoc> Fun { get; set; }

        public IQueryable<Culoc> Other { get; set; }
    }
}
