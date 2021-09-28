using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TacuDataAccess;
using TacuDataAccess.Models;
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

        //public IActionResult Category(string filterBy, bool hideHidden, bool reverse, string table, string category = "Restaurants")
        public IActionResult Category(CategoryModel input)
        {
            IQueryable<Category> filtersCategory;
            if (input.HideHidden)
            {
                filtersCategory = _db.Categorys.Where(x => x.Hidden != true);
            } else
            {
                filtersCategory = _db.Categorys;

            }
            if(input.Category == null)
            {
                input.Category = "Restruant";
            }
            IQueryable<string> dbCategory;
            if (input.Category == "theRest")
            {
                dbCategory = filtersCategory.Where(x => x.Hidden == false).Select(x => x.Name);
            } else
            {
                dbCategory = filtersCategory.Where(x => x.KeyWord == input.Category).Select(x => x.Name);
            }


            IQueryable<GenericRecordModel> results;

            switch (input.Table)
            {
                case "CuLoc":
                    Expression<Func<Culoc, bool>> culoc;
                    if (input.Category == "theRest")
                    {
                        culoc = x => dbCategory.All(c => x.Description.IndexOf(c) == -1);
                    } else
                    {
                        culoc = x => dbCategory.Any(c => x.Description.IndexOf(c) >= 0);
                    }
                    results = _db.Culocs.Where(culoc).MakeGenericRecord(dbCategory);
                    break;

                case "CuccLoc":
                    Expression<Func<Cuccloc, bool>> cuccloc;
                    if (input.Category == "theRest")
                    {
                        cuccloc = x => dbCategory.All(c => x.MerchantName.IndexOf(c) == -1);
                    }
                    else
                    {
                        cuccloc = x => dbCategory.Any(c => x.MerchantName.IndexOf(c) >= 0);
                    }
                    results = _db.Cucclocs.Where(cuccloc).MakeGenericRecord(dbCategory);
                    break;

                case "TaHuntington":
                    Expression<Func<TaHuntington, bool>> taHuntington;
                    if (input.Category == "theRest")
                    {
                        taHuntington = x => dbCategory.All(c => x.Description.IndexOf(c) == -1);
                    }
                    else
                    {
                        taHuntington = x => dbCategory.Any(c => x.Description.IndexOf(c) >= 0);
                    }
                    results = _db.TaHuntington.Where(taHuntington).MakeGenericRecord(dbCategory);
                    break;

                default:
                    if (input.Category == "theRest")
                    {
                        culoc = x => dbCategory.All(c => x.Description.IndexOf(c) == -1);
                    }
                    else
                    {
                        culoc = x => dbCategory.Any(c => x.Description.IndexOf(c) >= 0);
                    }
                    results = _db.Culocs.Where(culoc).MakeGenericRecord(dbCategory);
                    break;

            }
            switch (input.FilterBy)
            {
                case "PostedDate":
                    results = input.Reverse ? results.OrderBy(x => x.PostedDate): results.OrderByDescending(x => x.PostedDate);
                    break;
                case "Category":
                    results = input.Reverse ? results.OrderBy(x => x.Category.OrderBy(c => c).FirstOrDefault()): results.OrderByDescending(x => x.Category.OrderBy(c => c).LastOrDefault());
                    break;
                case "Description":
                    results = input.Reverse ? results.OrderBy(x => x.Description): results.OrderByDescending(x => x.Description);
                    break;
                case "Amount":
                    results = input.Reverse ? results.OrderBy(x => x.Amount) : results.OrderByDescending(x => x.Amount);
                    break;
            }

            if(input.StartDate != null)
            {
                results = results.Where(x => x.PostedDate >= input.StartDate);
            }
            if (input.EndDate != null)
            {
                input.EndDate = input.EndDate?.AddHours(23.9);
                results = results.Where(x => x.PostedDate <= input.EndDate);
            }

            input.Records = results;
            input.Reverse = !input.Reverse;
            input.Action = "Category";
            input.Controller = "Query";
            //var model = new CategoryModel
            //{
            //    Records = results,
            //    Category = category,
            //    FilterBy = filterBy,
            //    Reverse = !reverse,
            //    Table = table,
            //    Action = "Category",
            //    Controller = "Query"
                
            //};

            return View(input);
        }

        public IActionResult GroupedCategory(string filterBy, bool reverse, string table, string category = "Restruant")
        {
            var dbCategory = _db.Categorys.Where(x => x.KeyWord == category).Select(x => x.Name);

            IQueryable<GenericRecordModel> results;
            switch (table)
            {
                case "CuLoc":
                    results = _db.Culocs.Where(x => dbCategory.Any(c => x.Description.IndexOf(c) >= 0)).MakeGroupedGenericRecord(dbCategory);
                    break;

                case "CuccLoc":
                    results = _db.Cucclocs.Where(x => dbCategory.Any(c => x.MerchantName.IndexOf(c) >= 0)).MakeGroupedGenericRecord(dbCategory);
                    break;

                case "TaHuntington":
                    results = _db.TaHuntington.Where(x => dbCategory.Any(c => x.Description.IndexOf(c) >= 0)).MakeGroupedGenericRecord(dbCategory);
                    break;

                default:
                    results = _db.Culocs.Where(x => dbCategory.Any(c => x.Description.IndexOf(c) >= 0)).MakeGroupedGenericRecord(dbCategory);
                    break;

            }
            //var results = _db.Culocs.Where(x => dbCategory.Any(c => x.Description.IndexOf(c) >= 0)).Select(x =>
            //   new GenericRecordModel
            //   {
            //       PostedDate = x.PostedDate,
            //       Description = x.Description,
            //       Amount = x.Amount,
            //       FirstCategory = dbCategory.FirstOrDefault(c => x.Description.IndexOf(c) >= 0)
            //   });

            var groupResults = results.AsEnumerable().GroupBy(x => x.FirstCategory);
            switch (filterBy)
            {
                case "Category":
                    groupResults = reverse ? groupResults.OrderBy(x => x.Key) : groupResults.OrderByDescending(x => x.Key);
                    break;
                case "Amount":
                    groupResults = reverse ? groupResults.OrderBy(x => x.Sum(y => y.Amount)) : groupResults.OrderByDescending(x => x.Sum(y => y.Amount));
                    break;
            }
            
            var model = new GroupedCategoryModel
            {
                Records = groupResults,
                Category = category,
                FilterBy = filterBy,
                Reverse = !reverse,
                Table = table,
                Action = "GroupedCategory",
                Controller = "Query",
            };

            return View(model);
        }

        public IActionResult SpecificName(string name, string table)
        {
            //var results = _db.Culocs.Where(x =>  x.Description.IndexOf(name) >= 0).Select(x =>
            //   new GenericRecordModel
            //   {
            //       PostedDate = x.PostedDate,
            //       Description = x.Description,
            //       Amount = x.Amount,
            //   });
            IQueryable<GenericRecordModel> results;
            switch (table)
            {
                case "CuLoc":
                    results = _db.Culocs.Where(x => x.Description.IndexOf(name) >= 0).MakeGenericRecord();
                    break;

                case "CuccLoc":
                    results = _db.Cucclocs.Where(x => x.MerchantName.IndexOf(name) >= 0).MakeGenericRecord();
                    break;

                case "TaHuntington":
                    results = _db.TaHuntington.Where(x => x.Description.IndexOf(name) >= 0).MakeGenericRecord();
                    break;

                default:
                    results = _db.Culocs.Where(x => x.Description.IndexOf(name) >= 0).MakeGenericRecord();
                    break;

            }

            var Model = new SpecificNameModel
            {
                Records = results,
                Name = name,
                Table = table
            };
            return View(Model);
        }
    }
}
