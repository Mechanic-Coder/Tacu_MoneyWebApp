using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacuDataAccess;
using TacuDataAccess.Models;


namespace TacuMoney.Controllers
{
    public class AuxiliaryController : Controller
    {
        private readonly TacuMoneyContext _db;
        public AuxiliaryController(TacuMoneyContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        //only for creating tables
        public IActionResult MigrateToTable()
        {
            var AddTo = _db.KeyWords.Select(x => new Category { Name = x.Restruant, KeyWord = "Restruant", Hidden = false }).ToList();

            AddTo.AddRange(_db.KeyWords.Where(x => x.Restruant != null).Select(x => new Category { Name = x.Restruant, KeyWord = "Restruant", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.Gas != null).Select(x => new Category { Name = x.Gas, KeyWord = "Gas", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.StudentLoans != null).Select(x => new Category { Name = x.StudentLoans, KeyWord = "StudentLoans", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.Automotive != null).Select(x => new Category { Name = x.Automotive, KeyWord = "Automotive", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.Amazon != null).Select(x => new Category { Name = x.Amazon, KeyWord = "Amazon", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.Grocerys != null).Select(x => new Category { Name = x.Grocerys, KeyWord = "Grocerys", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.UtilitiesRent != null).Select(x => new Category { Name = x.UtilitiesRent, KeyWord = "UtilitiesRent", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.WithDrawls != null).Select(x => new Category { Name = x.WithDrawls, KeyWord = "WithDrawls", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.Merchandise != null).Select(x => new Category { Name = x.Merchandise, KeyWord = "Merchandise", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.BadHabit != null).Select(x => new Category { Name = x.BadHabit, KeyWord = "BadHabit", Hidden = false }).ToList());
            AddTo.AddRange(_db.KeyWords.Where(x => x.Fun != null).Select(x => new Category { Name = x.Fun, KeyWord = "Fun", Hidden = false }).ToList());

            _db.Categorys.AddRange(AddTo);
            _db.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> ParseCuccLoc()
        {
            var table = _db.Cucclocs;
            foreach (var row in table)
            {
                row.AmountNum = row.Amount.MakeDouble();
            }
            _db.SaveChanges();
            return View("index");
        }


        [HttpGet]
        public async Task<IActionResult> ParseCSV()
        {
            var readcsv = System.IO.File.ReadAllText("C:/Users/curti/OneDrive/Documents/TaCu_Money/Interfaces/Tacu_MoneyWebApp/TacuMoney/TestFiles/TaHuntington.txt");
            string[] csvfilerecord = readcsv.Split('\n');
            var lis = new List<TaHuntington>();
            foreach (var row in csvfilerecord.Skip(1))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    var cells = row.Split(',');
                    lis.Add(new TaHuntington
                    {
                        Date = DateTime.Parse(cells[1]),
                        Reference_Number = cells[2],
                        Description = cells[3],
                        Memo = cells[4],
                        Amount = Decimal.Parse(cells[5]),

                    });


                }
            }

            _db.TaHuntington.AddRange(lis);

            _db.SaveChanges();

            return View("index");
        }
    }
}
