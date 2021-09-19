using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacuDataAccess;
using TacuDataAccess.Models;
using TacuMoney.Models;

namespace TacuMoney.Controllers
{
    public class ConfigController : Controller
    {
        private readonly TacuMoneyContext _db;
        public ConfigController(TacuMoneyContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(string category, List<string> name)
        {
            var oldCategorys = _db.Categorys.OrderBy(x => x.Name).Where(x => x.KeyWord == category);
            _db.Categorys.RemoveRange(oldCategorys);

            var newCategorys = name.Where(x => x != null).Select(x => new Category { KeyWord = category, Name = x });

            _db.Categorys.AddRange(newCategorys);


            _db.SaveChanges();

            return RedirectToAction("EditCategory", new {category = category });
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string newCategory)
        {
            var alreadyExists = _db.Categorys.Any(x => x.KeyWord == newCategory);

            if (!alreadyExists)
            {
                var cate = new Category { KeyWord = newCategory, Name = newCategory };
                _db.Categorys.AddRange(cate);
            }
            _db.SaveChanges();
            return RedirectToAction("index");
        }


        [HttpGet]
        public async Task<IActionResult> EditCategory(string category)
        {
            category = category == "" || category == null ? "Grocerys" : category;
            var model = new ManageCategorys
            {
                SelectedCategory = category,
                Terms = _db.Categorys.OrderBy(x => x.Name).Where(x => x.KeyWord == category),
                Categorys = _db.Categorys.GroupBy(x => x.KeyWord).Select(x => x.Key)
            };

            return View(model);
        }
    }
}
