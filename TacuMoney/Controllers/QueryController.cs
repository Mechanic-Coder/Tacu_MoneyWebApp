using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TacuDataAccess;
using TacuMoney.Models;

namespace TacuMoney.Controllers
{
    public class QueryController : Controller
    {
        private readonly TacuMoneyContext _db;
        public QueryController(TacuMoneyContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Category(string filterBy, bool reverse, string category = "Restruant")
        {
            var dbCategory = _db.Categorys.Where(x => x.KeyWord == category).Select(x => x.Name);

            var results = _db.Culocs.Where(x => dbCategory.Any(c => x.Description.IndexOf(c) >= 0)).Select(x =>
               new GenericRecordModel
               {
                   PostedDate = x.PostedDate,
                   Description = x.Description,
                   Amount = x.Amount,
                   Category = dbCategory.Where(c => x.Description.IndexOf(c) >= 0).ToList()
               });
            switch (filterBy)
            {
                case "PostedDate":
                    results = reverse ? results.OrderBy(x => x.PostedDate): results.OrderByDescending(x => x.PostedDate);
                    break;
                case "Category":
                    results = reverse ? results.OrderBy(x => x.Category.OrderBy(c => c).FirstOrDefault()): results.OrderByDescending(x => x.Category.OrderBy(c => c).LastOrDefault());
                    break;
                case "Description":
                    results = reverse ? results.OrderBy(x => x.Description): results.OrderByDescending(x => x.Description);
                    break;
                case "Amount":
                    results = reverse ? results.OrderBy(x => x.Amount) : results.OrderByDescending(x => x.Amount);
                    break;
            }

            var model = new CategoryModel
            {
                Records = results,
                Category = category,
                FilterBy = filterBy,
                Reverse = !reverse
            };

            return View(model);
        }

        public IActionResult SpecificName(string name)
        {
            var results = _db.Culocs.Where(x =>  x.Description.IndexOf(name) >= 0).Select(x =>
               new GenericRecordModel
               {
                   PostedDate = x.PostedDate,
                   Description = x.Description,
                   Amount = x.Amount,
               });

            var Model = new SpecificNameModel
            {
                Records = results,
                Name = name
            };
            return View(Model);
        }
    }
}
