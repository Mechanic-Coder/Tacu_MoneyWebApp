using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacuDataAccess.Models;
using TacuDataAccess;
using System.Diagnostics;
using TacuMoney.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;
using AgileObjects.ReadableExpressions;


namespace TacuMoney.Controllers
{
    public class MainController : Controller
    {
        private readonly TacuMoneyContext _db;

        public MainController(TacuMoneyContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var test = _db.Culocs.ToList();

            return View(test);
        }


        public IActionResult Raw()
        {
            var test = (DbSet<TestModel>)_db.Culocs.FromSqlRaw(@"select Distinct[TacuMoney].[dbo].[CULoc].Id, 
                                    [TacuMoney].[dbo].[CULoc].Description, 
                                    [TacuMoney].[dbo].[CULoc].Amount, 
                                    [TacuMoney].[dbo].[CULoc].CR_DR
                                        from [TacuMoney].[dbo].[CULoc]
                                        inner join[TacuMoney].[dbo].[keyWords] on[TacuMoney].[dbo].[CULoc].Description
                                        like '%' + [TacuMoney].[dbo].[keyWords].restruant + '%'
                                        Where [TacuMoney].[dbo].[CULoc].CR_DR Like 'DR'
                                        ORDER BY[TacuMoney].[dbo].[CULoc].Id ASC");
            //var test = _db.Culocs
            //.FromSqlRaw(@"SELECT TOP (1000) [Id]
            //                  ,[Posted_Date]
            //                  ,[Serial_Number]
            //                  ,[Description]
            //                  ,[Amount]
            //                  ,[CR_DR]
            //              FROM [TacuMoney].[dbo].[CULoc]");
            //var test = _db.Database.(@"select [keyWords].Amazon, Sum([CULoc].[Amount]) AS TOTAL
            //                from [TacuMoney].[dbo].[CULoc]
            //                inner join [TacuMoney].[dbo].[keyWords] on [TacuMoney].[dbo].[CULoc].Description 
            //                like '%' + [TacuMoney].[dbo].[keyWords].Amazon  + '%'
            //                GROUP BY [keyWords].Amazon ");

            return View(test);
        }
        private async Task<IEnumerable<Culoc>> GetErrors(string propertyToFilter, string value)
        {
            var error = Expression.Parameter(typeof(Culoc));
            var memberAccess = Expression.PropertyOrField(error, propertyToFilter);
            var exprRight = Expression.Constant(value);
            var equalExpr = Expression.Equal(memberAccess, exprRight);
            Expression<Func<Culoc, bool>> lambda = Expression.Lambda<Func<Culoc, bool>>(equalExpr, error);

            return await _db.Culocs.Where(lambda).ToListAsync();
        }

        private async Task<IEnumerable<Culoc>> GetContains(string propertyName, string propertyValue)
        {


            var parameterExp = Expression.Parameter(typeof(Culoc));

            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(propertyValue, typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            Expression<Func<Culoc, bool>> lambda = Expression.Lambda<Func<Culoc, bool>>(containsMethodExp, parameterExp);

            string readable = lambda.ToReadableString();

            return await _db.Culocs.Where(lambda).ToListAsync();
        }

        private async Task<IEnumerable<Culoc>> GetContainsList(string propertyName)
        {
            var numbers = new List<string>();
            numbers.Add("venmo");
            numbers.Add("shoe");

            var parameterExp = Expression.Parameter(typeof(Culoc));

            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(numbers, typeof(List<string>));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            Expression<Func<Culoc, bool>> lambda = Expression.Lambda<Func<Culoc, bool>>(containsMethodExp, parameterExp);
            return await _db.Culocs.Where(lambda).ToListAsync();
        }

        private async Task<IEnumerable<Culoc>> GetContainsKeyWord(string propertyName)
        {
            var parameterExp = Expression.Parameter(typeof(Culoc));

            var propertyExp = Expression.Property(parameterExp, propertyName);
            MethodInfo method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var someValue = Expression.Constant(_db.KeyWords.Select(x => x.Grocerys), typeof(string));
            var containsMethodExp = Expression.Call(propertyExp, method, someValue);

            Expression<Func<Culoc, bool>> lambda = Expression.Lambda<Func<Culoc, bool>>(containsMethodExp, parameterExp);
            return await _db.Culocs.Where(lambda).ToListAsync();
        }

        //public async Task<IActionResult> ItemizedExpensesAsync()
        //{
        //    /// learn about struct next
        //    /// //Add mechanic coder git hub 
        //    var model = new CatagoryLists();
        //    //var another = await GetErrors("CrDr", "DR");
        //    //var te = await GetContains("Description", "venmo");

        //    //var tez = await GetContainsList("Description");
        //    //var test = await GetContainsKeyWord("Description");

        //    //foreach (var a in another)
        //    //{
        //    //    var g = a;
        //    //}

        //    //var maxCulLoc = _db.Culocs;
        //    //var descList = new List<string> { "venmo", "Meijer", "krogers" }.AsQueryable();

        //    //var t = CULocs.Where(x => descList.Any(d => x.Description.IndexOf(d) >= 0)).ToList();

        //    //var test = maxCulLoc.Where(x => descList.Any(k => x.Description.Contains(k)));


        //    //Expression<Func<KeyWord, string>> Catagory = x => x.Restruant;
        //    //IQueryable<string> searchingFor = _db.KeyWords.Select(Catagory);
        //    //Expression<Func<Culoc, bool>> stuff = x => searchingFor.Any(k => x.Description.Contains(k));


        //    //IEnumerable<Culoc> rest = _db.Culocs.Where(stuff);

        //    //var result = _db.Culocs.Where(stuff);
        //    //model.Restruant = result;

        //    //var AlreadyUsed = model.Restruant.Select(x => x.Id).ToHashSet();

        //    var AlreadyUsed =  new List<int>();

        //    (model.BadHabit, AlreadyUsed) = GetCategory(x => x.BadHabit, AlreadyUsed);
        //    //var(new CatagoryLists(BadHabit = ), AlreadyUsed) = GetCategory(x => x.BadHabit, AlreadyUsed);
        //    (model.Restruant, AlreadyUsed) = GetCategory(x => x.Restruant, AlreadyUsed);

        //    (model.Gas, AlreadyUsed) = GetCategory(x => x.Gas, AlreadyUsed);
        //    //model.Gas = GetCategory(x => x.Gas, AlreadyUsed, out AlreadyUsed);



        //    (model.Grocerys, AlreadyUsed) = GetCategory(x => x.Grocerys, AlreadyUsed);
        //    //model.Grocerys = GetCategory(x => x.Grocerys, AlreadyUsed, out AlreadyUsed);



        //    (model.StudentLoans, AlreadyUsed) = GetCategory(x => x.StudentLoans, AlreadyUsed);
        //    //model.StudentLoans = GetCategory(x => x.StudentLoans, AlreadyUsed, out AlreadyUsed);


        //    (model.Automotive, AlreadyUsed) = GetCategory(x => x.Automotive, AlreadyUsed);
        //    //model.Automotive = GetCategory(x => x.Automotive, AlreadyUsed, out AlreadyUsed);


        //    (model.Amazon, AlreadyUsed) = GetCategory(x => x.Amazon, AlreadyUsed);
        //    //model.Amazon = GetCategory(x => x.Amazon, AlreadyUsed, out AlreadyUsed);


        //    (model.UtilitiesRent, AlreadyUsed) = GetCategory(x => x.UtilitiesRent, AlreadyUsed);
        //    //model.UtilitiesRent = GetCategory(x => x.UtilitiesRent, AlreadyUsed, out AlreadyUsed);


        //    (model.WithDrawls, AlreadyUsed) = GetCategory(x => x.WithDrawls, AlreadyUsed);
        //    //model.WithDrawls = GetCategory(x => x.WithDrawls, AlreadyUsed, out AlreadyUsed);


        //    (model.Merchandise, AlreadyUsed) = GetCategory(x => x.Merchandise, AlreadyUsed);
        //    //model.Merchandise = GetCategory(x => x.Merchandise, AlreadyUsed, out AlreadyUsed);


        //    //model.BadHabit = GetCategory(x => x.BadHabit, AlreadyUsed, out AlreadyUsed);


        //    (model.Fun, AlreadyUsed) = GetCategory(x => x.Fun, AlreadyUsed);
        //    //model.Fun = GetCategory(x => x.Fun, AlreadyUsed, out AlreadyUsed);


        //    model.Other = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id));

        //    return View("ItemizedExpenses", model);
        //}

        public async Task<IActionResult> ItemizedExpensesAsync()
        {
            /// learn about struct next
            /// //Add mechanic coder git hub 
            var model = new CatagoryLists();

            model.BadHabit = GetCategory("BadHabit");

            model.Restruant = GetCategory("Restruant");

            model.Gas = GetCategory("Gas");

            model.Grocerys = GetCategory("Grocerys");

            model.StudentLoans = GetCategory("StudentLoans");

            model.Automotive = GetCategory("Automotive");

            model.Amazon = GetCategory("Amazon");

            model.UtilitiesRent = GetCategory("UtilitiesRent");

            model.WithDrawls = GetCategory("WithDrawls");

            model.Merchandise = GetCategory("Merchandise");

            model.Fun = GetCategory("Fun");

            return View("ItemizedExpenses", model);
        }


        public IActionResult Withdrawls()
        {
            var model = GetCaegoryList("WithDrawls");
            return View(model);
        }
        //private (IQueryable<Culoc>, List<int>) GetCategory(Expression<Func<KeyWord, string>> Catagory)
        //{
        //    IQueryable<string> searchingFor = _db.KeyWords.Select(Catagory);
        //    var result = _db.Culocs.Where(x => x.CrDr == "DR" && !remove.Contains(x.Id) && searchingFor.Any(k => x.Description.IndexOf(k) >= 0)).Select(x => new Culoc { Amount = x.Amount, Id = x.Id });
        //    return (result, moreAlreadUsed);
        //}
        private IQueryable<Culoc> GetCaegoryList(string type)
        {
            var searchingFor = _db.Categorys.Where(x => x.KeyWord == type).Select(x => x.Name);
            var DontGet = _db.Categorys.Where(x => x.KeyWord != type).Select(x => x.Name);
            var result = _db.Culocs.Where(x => x.CrDr == "DR" && !DontGet.Any(k => x.Description.IndexOf(k) >= 0) && searchingFor.Any(k => x.Description.IndexOf(k) >= 0));
            return result;
        }

        private IQueryable<Culoc> GetCategory(string type)
        {
            var searchingFor = _db.Categorys.Where(x => x.KeyWord == type).Select(x => x.Name);
            var DontGet = _db.Categorys.Where(x => x.KeyWord != type && x.KeyWord != "WithDrawls").Select(x => x.Name);
            var result = _db.Culocs.Where(x => x.CrDr == "DR" && !DontGet.Any(k => x.Description.IndexOf(k) >= 0) && searchingFor.Any(k => x.Description.IndexOf(k) >= 0)).Select(x => new Culoc { Amount = x.Amount, Id = x.Id });
            return result;
        }

        private (IQueryable<Culoc>, List<int>) GetCategory(Expression<Func<KeyWord, string>> Catagory, List<int> nAlreadyUsed)
        {
            IQueryable<string> searchingFor = _db.KeyWords.Select(Catagory);
            var remove = nAlreadyUsed;
            var result = _db.Culocs.Where(x => x.CrDr == "DR" && !remove.Contains(x.Id) && searchingFor.Any(k => x.Description.IndexOf(k) >= 0)).Select(x => new Culoc {Amount = x.Amount, Id = x.Id });
            var moreAlreadUsed = nAlreadyUsed.Concat(result.Select(x => x.Id)).ToList();
            return (result, moreAlreadUsed);
        }

        private IQueryable<Culoc> GetCategory(Expression<Func<KeyWord, string>> Catagory, HashSet<int> nAlreadyUsed,  out HashSet<int> AlreadyUsed)
        {
            IQueryable<string> searchingFor = _db.KeyWords.Select(Catagory);
            var remove = nAlreadyUsed;
            var result = _db.Culocs.Where(x => x.CrDr == "DR" && !remove.Contains(x.Id) && searchingFor.Any(k => x.Description.IndexOf(k) >= 0));
            AlreadyUsed = nAlreadyUsed.Concat(result.Select(x => x.Id)).ToHashSet();
            return result;
        }

        private List<int> GetCategory(Expression<Func<KeyWord, string>> Catagory, List<int> nAlreadyUsed, out IQueryable<Culoc> result )
        {
            IQueryable<string> searchingFor = _db.KeyWords.Select(Catagory);
            var remove = new List<int>(nAlreadyUsed);
            result = _db.Culocs.Where(x => !remove.Contains(x.Id) && searchingFor.Any(k => x.Description.IndexOf(k) >= 0));
            nAlreadyUsed = nAlreadyUsed.Concat(result.Select(x => x.Id)).ToList();
            return nAlreadyUsed;
        }


        public async Task<IActionResult> PracticeCodeItemizedExpensesAsync()
        {
            //var another = await GetErrors("CrDr", "DR");
            //var te = await GetContains("Description", "venmo");

            //var tez = await GetContainsList("Description");
            //var test = await GetContainsKeyWord("Description");

            //foreach (var a in another)
            //{
            //    var g = a;
            //}

            var maxCulLoc = _db.Culocs;
            var descList = new List<string>{ "venmo", "Meijer", "krogers" }.AsQueryable();

            //var t = CULocs.Where(x => descList.Any(d => x.Description.IndexOf(d) >= 0)).ToList();

            var test = maxCulLoc.Where(x => descList.Any(k => x.Description.Contains(k)));

            Expression<Func<KeyWord, string>> arg = x => x.Restruant;

            IQueryable<string> resDesc = _db.KeyWords.Select(arg);

            //Func<KeyWord, Culoc, bool> firsStuff = k => x.Description.Contains(k);
            Expression<Func<Culoc, bool>> stuff = x => resDesc.Any(k => x.Description.Contains(k));

            IEnumerable<Culoc> rest = _db.Culocs.Where(stuff);
            //IEnumerable<Culoc> rest = _db.Culocs.Where(x => resDesc.Any(k => x.Description.Contains(k)));
            var AlreadyUsed = rest.Select(x => x.Id).ToHashSet();

            //var resDesc = _db.KeyWords.Select(x => x.Restruant);
            //var rest = maxCulLoc.Where(x => resDesc.Any(k => x.Description.Contains(k) ));
            //var AlreadyUsed = rest.Select(x => x.Id).ToHashSet();

            var gasDesc = _db.KeyWords.Select(x => x.Gas);
            var Gas = maxCulLoc.Where(x => !new HashSet<int>(AlreadyUsed).Contains(x.Id) && gasDesc.Any(k => x.Description.IndexOf(k) >= 0));
            var nAlreadyUsed = new HashSet<int>(AlreadyUsed.Concat(Gas.Select(x => x.Id)));

            var grocdesc = _db.KeyWords.Select(x => x.Grocerys);
            var Grocerys = maxCulLoc.Where(x => !new HashSet<int>(AlreadyUsed).Contains(x.Id) && grocdesc.Any(k => x.Description.IndexOf(k) >= 0));
            var nnAlreadyUsed = new HashSet<int>(nAlreadyUsed.Concat(Grocerys.Select(x => x.Id)));

            //var studentLoans = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.IndexOf(k.StudentLoans) >= 0));
            //AlreadyUsed = AlreadyUsed.Concat(studentLoans.Select(x => x.Id));

            //var Automotive = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.IndexOf(k.Automotive) >= 0));
            //AlreadyUsed = AlreadyUsed.Concat(Automotive.Select(x => x.Id));

            //var Amazon = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.IndexOf(k.Amazon) >= 0));
            //AlreadyUsed = AlreadyUsed.Concat(Amazon.Select(x => x.Id));


            ////var justMiejer = Grocerys.Select(x => x.Id);

            ////AlreadyUsed.Concat(justMiejer);

            //var UtilitiesRent = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.IndexOf(k.UtilitiesRent) >= 0));
            //AlreadyUsed = AlreadyUsed.Concat(UtilitiesRent.Select(x => x.Id));

            //var WithDrawls = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.IndexOf(k.WithDrawls) >= 0));
            //AlreadyUsed = AlreadyUsed.Concat(WithDrawls.Select(x => x.Id));

            //var Merchandise = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.IndexOf(k.Merchandise) >= 0));
            //AlreadyUsed = AlreadyUsed.Concat(Merchandise.Select(x => x.Id));

            //var BadHabit = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.IndexOf(k.BadHabit) >= 0));
            ////var tesBadHabit = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.Contains(k.BadHabit)));
            //AlreadyUsed = AlreadyUsed.Concat(BadHabit.Select(x => x.Id));

            //var Fun = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id) && _db.KeyWords.Any(k => x.Description.IndexOf(k.Fun) >= 0));
            //AlreadyUsed = AlreadyUsed.Concat(Fun.Select(x => x.Id));




            var model = new CatagoryLists
            {
                Restruant = rest,
                Gas = Gas,
                Grocerys = Grocerys,
                //StudentLoans = studentLoans,
                //Automotive = Automotive,
                //Amazon = Amazon,
                //UtilitiesRent = UtilitiesRent,
                //WithDrawls = WithDrawls,
                //Merchandise = Merchandise,
                //BadHabit = BadHabit,
                //Fun = Fun,
                Other = _db.Culocs.Where(x => !AlreadyUsed.Contains(x.Id))
            };
            return View("ItemizedExpenses", model);
        }
        //public IActionResult ItemizedExpenses()
        //{
        //    //var test = _db.Culocs.Where(x => x.Description.Contains())
        //    var allVals = _db.Culocs.Take(100);
        //    var theListOne = allVals;
        //    var restruants = from s in allVals
        //                     from sa in _db.KeyWords
        //                     where s.Description.Contains(sa.Restruant)
        //                     select s;
        //    var newAllVals = theListOne.Except(restruants.AsEnumerable());


        //    var gas = from s in newAllVals
        //              from sa in _db.KeyWords
        //                     where s.Description.Contains(sa.Gas)
        //                     select s;
        //    newAllVals = newAllVals.Except(gas.AsEnumerable());
        //    var studentLoans = from s in newAllVals
        //                       from sa in _db.KeyWords
        //                     where s.Description.Contains(sa.StudentLoans)
        //                     select s;
        //    newAllVals = newAllVals.Except(studentLoans.AsEnumerable());
        //    var autoMotive = from s in newAllVals
        //                     from sa in _db.KeyWords
        //                     where s.Description.Contains(sa.Automotive)
        //                     select s;
        //    newAllVals = newAllVals.Except(autoMotive.AsEnumerable());
        //    var amazon = from s in newAllVals
        //                 from sa in _db.KeyWords
        //                     where s.Description.Contains(sa.Amazon)
        //                     select s;
        //    newAllVals = newAllVals.Except(amazon.AsEnumerable());
        //    var grocerys = from s in newAllVals
        //                   from sa in _db.KeyWords
        //                     where s.Description.Contains(sa.Grocerys)
        //                     select s;
        //    newAllVals = newAllVals.Except(grocerys.AsEnumerable());
        //    var utilitiesRent = from s in newAllVals
        //                        from sa in _db.KeyWords
        //                   where s.Description.Contains(sa.UtilitiesRent)
        //                   select s;
        //    newAllVals = newAllVals.Except(utilitiesRent.AsEnumerable());
        //    var withdrawls = from s in newAllVals
        //                     from sa in _db.KeyWords
        //                   where s.Description.Contains(sa.WithDrawls)
        //                   select s;
        //    newAllVals = newAllVals.Except(withdrawls.AsEnumerable());
        //    var merchandise = from s in newAllVals
        //                      from sa in _db.KeyWords
        //                   where s.Description.Contains(sa.Merchandise)
        //                   select s;
        //    newAllVals = newAllVals.Except(merchandise.AsEnumerable());
        //    var badHabit = from s in newAllVals
        //                   from sa in _db.KeyWords
        //                   where s.Description.Contains(sa.BadHabit)
        //                   select s;
        //    newAllVals = newAllVals.Except(badHabit.AsEnumerable());
        //    var fun = from s in newAllVals
        //              from sa in _db.KeyWords
        //                   where s.Description.Contains(sa.Fun)
        //                   select s;
        //    newAllVals = newAllVals.Except(fun.AsEnumerable());
        //    //list of items never picked figure out how to do this on every sql 
        //    //var theList = newAllVals.ToList();
        //    //convert to list takes foreaver
        //    // try using two objects?
        //    // or go itterartive c# route
        //    var model = new CatagoryLists
        //    {
        //        Restruant = restruants.ToList(),
        //        Gas = gas.ToList(),
        //        StudentLoans = studentLoans.ToList(),
        //        Automotive = autoMotive.ToList(),
        //        Amazon = amazon.ToList(),
        //        Grocerys = grocerys.ToList(),
        //        UtilitiesRent = utilitiesRent.ToList(),
        //        WithDrawls = withdrawls.ToList(),
        //        Merchandise = merchandise.ToList(),
        //        BadHabit = badHabit.ToList(),
        //        Fun = fun.ToList()
        //    };
        //    //var x = _db.Culocs.Where(x => x.Description.Contains())
        //    //Debug.WriteLine(x);
        //    //                       where s.Description.Contains(sa.Restruant)
        //    return View("ItemizedExpenses", model);
        //}
    }
}


