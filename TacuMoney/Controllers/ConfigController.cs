using AccountingDB;
using AccountingDB.Models;
using AccountingDB.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly AccountingContext _adb;
        public ConfigController(TacuMoneyContext db, AccountingContext adb)
        {
            _db = db;
            _adb = adb;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UploadCSV(IFormFile file, AccountEnum account)
        {

            var metaHandler = new MetaHandling(_adb);
            var merchantHandler = new MerchantHandling(_adb);
            ( Func<StreamReader, Dictionary<string, int>, Transaction> rowReader, Func<StreamReader, Dictionary<string, int>> headReader) reader = ( null,  null);
            switch (account)
            {
                case AccountEnum.CurtisLoc:
                    reader =  (ReadLoc, headReaderQoutes);
                    break;
                case AccountEnum.CurtisLocCC:
                    reader = (ReadLocCC, headReader);
                    break;
                //case AccountEnum.TammyHuntington:
                //    //ReadHunt(file);
                //    break;
                //default:
                //    break;
            };
            if (reader.rowReader == null || reader.headReader == null) return Redirect("index");

            var transList = new List<Transaction>();

            using (var sreader = new StreamReader(file.OpenReadStream()))
            {
                var h = reader.headReader(sreader);
                while (!sreader.EndOfStream)                          //get all the content in rows 
                {
                    transList.Add(reader.rowReader(sreader, h));

                }
            }

            _adb.Transactions.AddRange(transList);
            _adb.SaveChanges();
            
            return Redirect("index");
            Dictionary<string, int> headReaderQoutes(StreamReader sr)
            {
                return sr.ReadLine().Split(',').Select((x, i) => new { Item = x, Index = i }).ToDictionary(x => x.Item.Replace("\"", "").Trim(), x => x.Index, StringComparer.OrdinalIgnoreCase);     //Title
            }

            Dictionary<string, int> headReader(StreamReader sr)
            {
                var g = sr.ReadLine().Split(',').Select((x, i) => new { Item = x, Index = i }).ToDictionary(x => x.Item.Trim(), x => x.Index, StringComparer.OrdinalIgnoreCase);     //Title
                sr.ReadLine(); // skip empty line
                return g;
            }

            Transaction ReadLoc(StreamReader sr, Dictionary<string, int> h)
            {
                var r = sr.ReadLine().Split("\",\"");

                string gV(string name)
                {
                    var val = r[h[name]].Replace("\"", "").ToString();
                    return val;
                }

                //var metaHandler = new MetaHandling(_adb);
                //var transList = new List<Transaction>();
                //using (var sreader = new StreamReader(file.OpenReadStream()))
                //{
                //    var h = sreader.ReadLine().Split(',').Select((x, i) => new { Item = x, Index = i }).ToDictionary(x => x.Item.Replace("\"", "").Trim(), x => x.Index, StringComparer.OrdinalIgnoreCase);     //Title
                //    while (!sreader.EndOfStream)                          //get all the content in rows 
                //    {

                //        var r = sreader.ReadLine().Split("\",\"");

                //        string gV(string name)
                //        {
                //            var val = r[h[name]].Replace("\"", "").ToString();
                //            return val;
                //        }
                var t = new Transaction
                {
                    PostingDate = DateTime.Parse(gV("Posting Date")),
                    TransactionDate = DateTime.Parse(gV("Effective Date")),
                    Amount = decimal.Parse(gV("Amount")),
                    ReferenceNumber = gV("Reference Number"),
                    AccountId = 1

                };
                t.TransactionMetas = new List<TransactionMeta>
                    {
                        new TransactionMeta
                        {
                            MetaId = metaHandler.GetMetaId(gV("Description")),
                            MetaType = MetaType.Description
                        },
                        new TransactionMeta
                        {
                            MetaId = metaHandler.GetMetaId(gV("Extended Description")),
                            MetaType = MetaType.ExtDescription
                        },
                        new TransactionMeta
                        {
                            MetaId = metaHandler.GetMetaId(gV("Transaction Category")),
                            MetaType = MetaType.Category
                        },
                        new TransactionMeta
                        {
                            MetaId = metaHandler.GetMetaId(gV("Type")),
                            MetaType = MetaType.Type
                        }
                    };

                var checkNum = gV("Check Number");
                if (!string.IsNullOrEmpty(checkNum))
                {
                    t.TransactionMetas.Add(
                        new TransactionMeta
                        {
                            MetaId = metaHandler.GetMetaId(checkNum),
                            MetaType = MetaType.CheckNumber
                        }
                        );
                }
                return t;
                //        transList.Add(t);
                //    }

                //}
                //_adb.Transactions.AddRangeAsync(transList);
                //_adb.SaveChanges();
            }
            Transaction ReadLocCC(StreamReader sr, Dictionary<string, int> h)
            {

                var r = sr.ReadLine().Split("\",\"");

                string gV(string name)
                {
                    var val = r[h[name]].Replace("\"", "").ToString();
                    return val;
                }

                var t = new Transaction();
                t.PostingDate = DateTime.Parse(gV("Posting Date"));
                t.TransactionDate = DateTime.Parse(gV("Trans Date"));
                t.ReferenceNumber = gV("Reference Number");
                t.AccountId = 3;
                t.TransactionMerchantId = merchantHandler.GetTransactionMerchantId(gV("Merchant Name"), gV("Merchant City"), gV("Merchant State"));

                var amountString = gV("Amount").Replace("$", "");
                if (amountString.Contains("("))
                {
                    t.Amount = decimal.Parse(amountString.Replace("(", "").Replace(")", ""));
                }else
                {
                    t.Amount = -1 * decimal.Parse(amountString);
                }

                t.TransactionMetas = new List<TransactionMeta>
                {
                    new TransactionMeta
                    {
                        MetaId = metaHandler.GetMetaId(gV("Category")),
                        MetaType = MetaType.Description
                    },
                    new TransactionMeta
                    {
                        MetaId = metaHandler.GetMetaId(gV("Description")),
                        MetaType = MetaType.ExtDescription
                    }
                };

                return t;
            }
        }
        
        


        //this will be obsolete
        [HttpPost]
        public async Task<IActionResult> EditCategory(EditCategoryInputModel input)
        {
            var oldCategorys = _db.Categorys.OrderBy(x => x.Name).Where(x => x.KeyWord == input.Category);
            _db.Categorys.RemoveRange(oldCategorys);

            var newCategorys = input.Names.Where(x => x != null).Select(x => new Category { KeyWord = input.Category, Name = x.Name, Hidden = x.Hidden });

            _db.Categorys.AddRange(newCategorys);

            _db.SaveChanges();

            return RedirectToAction("EditCategory", new {category = input.Category });
        }
        [HttpPost]
        public string DeleteCategoryName(int id)
        {
            if(id != 0) {
                var row = _db.Categorys.Single(x => x.Id == id);
                _db.Categorys.Remove(row);
                _db.SaveChanges();
            }
            return "Success";
        }

        [HttpPost]
        public string HideCategoryName(int id, bool hide)
        {
            if (id != 0)
            {
                var row = _db.Categorys.Single(x => x.Id == id);
                row.Hidden = hide;
                _db.SaveChanges();
            }
            return "Success";
        }

        [HttpPost]
        public string EditCategoryName(string name, int? id, string category)
        {
            Category item;
            if(id == null)
            {
                item = new Category();
                item.Hidden = false;
                item.KeyWord = category;
                _db.Categorys.Add(item);
            } else
            {
                item = _db.Categorys.Single(x => x.Id == id);
            }

            item.Name = name;
            _db.SaveChanges();

            return item.Id.ToString();
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


        public IActionResult UploadCSVOBS(IFormFile file, AccountEnum account)
        {
            if (file == null) return Redirect("index");
            if (file.FileName.EndsWith(".csv") || file.FileName.EndsWith(".CSV"))
            {
                using (var sreader = new StreamReader(file.OpenReadStream()))
                {
                    string[] headers = sreader.ReadLine().Split(',');     //Title
                    while (!sreader.EndOfStream)                          //get all the content in rows 
                    {
                        switch (account)
                        {
                            case AccountEnum.CurtisLoc:
                                AddToCuLoc(sreader);
                                break;
                            case AccountEnum.CurtisLocCC:
                                AddToCuLocCC(sreader);
                                break;
                            case AccountEnum.TammyHuntington:
                                break;
                            default:
                                break;
                        }

                        if (account == AccountEnum.CurtisLoc)
                        {
                            AddToCuLoc(sreader);
                        }
                    }

                    _db.SaveChanges();
                }
            }

            void AddToCuLocCC(StreamReader reader)
            {
                var record = new Cuccloc();
                string[] rows = reader.ReadLine().Split(',');
                record.OriginatingAccountNumber = rows[0].ToString();
                record.PostingDate = DateTime.Parse(rows[1].ToString());
                record.TransDate = DateTime.Parse(rows[2].ToString());
                record.Type = rows[3].ToString();
                record.Category = rows[4].ToString();
                record.MerchantName = rows[5].ToString();
                record.MerchantCity = rows[6].ToString();
                record.MerchantState = rows[7].ToString();
                record.Description = rows[8].ToString();
                record.TransactionType = rows[9].ToString();
                var amount = rows[10].ToString().Replace("\"", "");
                record.Amount = amount;
                //record.AmountNum = rows[10].ToString().MakeDouble();
                record.ReferenceNumber = rows[11].ToString();

                _db.Cucclocs.Add(record);
            }

            void AddToCuLoc(StreamReader reader)
            {
                var record = new Culoc();
                string[] rows = reader.ReadLine().Split(',');
                record.PostedDate = DateTime.Parse(rows[1].ToString());
                record.Description = rows[3].ToString();
                record.Amount = Double.Parse(rows[4].ToString());
                record.CrDr = rows[5].ToString();
                _db.Culocs.Add(record);
            }

            return Redirect("index");

        }
    }
}
