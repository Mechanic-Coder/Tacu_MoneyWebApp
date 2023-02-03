using AccountingDB;
using AccountingDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace TacuMoney.Controllers
{
    public class AccountController : Controller
    {
        private readonly AccountingContext _adb;
        public AccountController(AccountingContext adb)
        {
            _adb = adb;
        }
        public IActionResult Index()
        {
            //_adb.Accounts.Add(new Account { Name = "Curtis LOC" });
            //_adb.SaveChanges();

            var t = _adb.Accounts.Find(1);
            return View();
        }
    }
}
