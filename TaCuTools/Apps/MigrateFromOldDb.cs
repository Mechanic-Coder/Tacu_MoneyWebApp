using AccountingDB;
using AccountingDB.Models;
using Shane32.ConsoleDI;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TacuDataAccess;

namespace TaCuTools.Apps
{
    [MainMenu("Migrate from old DB")]
    internal class MigrateFromOldDb : IMenuOption
    {
        private readonly AccountingContext _adb;
        private readonly TacuMoneyContext _tdb;

        public MigrateFromOldDb(AccountingContext adb, TacuMoneyContext tdb)
        {
            _adb = adb;
            _tdb = tdb;
        }


        public async Task RunAsync()
        {
            //var description = locAccount.Select(x => x.Description.ToLower()).ToHashSet().Select(x => new Meta { Content = x});
            //_adb.Metas.AddRange(description);
            //_adb.SaveChanges();

            var locAccount = _tdb.Culocs.ToList();
            var look = _adb.Metas.ToDictionary(x => x.Content, x => x.Id);

            foreach(var loc in locAccount)
            {
                if(loc.CrDr != "DR" && loc.CrDr != "CR")
                {
                    throw new Exception("not valid action");
                }


                var amount = loc.CrDr == "DR" ? -1 * loc.Amount : loc.Amount;
                var tran = new Transaction
                {
                    PostingDate = loc.PostedDate,
                    Amount = Convert.ToDecimal(amount ?? 0d),
                    ReferenceNumber = loc.SerialNumber
                };

                if(look.TryGetValue(loc.Description, out var metaId))
                {
                    tran.TransactionMetas.Add(new TransactionMeta
                    {
                        MetaId = metaId,
                        MetaType = MetaType.Description
                    });
                }
            }



            //var t = _adb.Accounts.FirstOrDefault();   


        }
    }
}
