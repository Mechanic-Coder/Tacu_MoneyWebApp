using AccountingDB;
using AccountingDB.Models;
using AccountingDB.Services;
using Microsoft.Data.SqlClient;
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
    
    //[MainMenu("Migrate from old DB")]
    internal class MigrateFromOldDb //: IMenuOption
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
            //await HuntingTonMigration();
        }

        private async Task HuntingTonMigration()
        {
            var hunt = _tdb.TaHuntington.ToList();
            var metaHandler = new MetaHandling(_adb);
            var transList = new List<Transaction>();
            foreach ( var t in hunt)
            {
                var tran = new Transaction
                {
                    
                    TransactionDate = t.Date,
                    Amount = t.Amount == null ? 0 : t.Amount,
                    ReferenceNumber = t.Reference_Number,
                    AccountId = 2,
                  
                };

                tran.TransactionMetas = new List<TransactionMeta>();

                var desId = metaHandler.GetMetaId(t.Description.Replace("\"", "").Trim());
                tran.TransactionMetas.Add(new TransactionMeta
                {
                    MetaId = desId,
                    MetaType = MetaType.Description
                });

                var memoId = metaHandler.GetMetaId(t.Memo.Replace("\"", "").Trim());
                tran.TransactionMetas.Add(new TransactionMeta
                {
                    MetaId = memoId,
                    MetaType = MetaType.Memo
                });


                transList.Add(tran);
            }
            
            _adb.Transactions.AddRange(transList);
            _adb.SaveChanges();
        }

        public async Task LOCCreditCardMigration()
        {
            var locCC = _tdb.Cucclocs.ToList();
            //var look = _adb.GetMetaDictionary();
            var merchantHandler = new MerchantHandling(_adb);
            var metaHandler = new MetaHandling(_adb);
            var transList = new List<Transaction>();
            foreach (var c in locCC)
            {
                var tran = new Transaction
                {
                    PostingDate = c.PostingDate,
                    TransactionDate = c.TransDate,
                    Amount = c.AmountNum == null ? 0 : (decimal)c.AmountNum,
                    ReferenceNumber = c.ReferenceNumber,
                    AccountId = 3,
                    TransactionMerchantId = merchantHandler.GetTransactionMerchantId(c.MerchantName, c.MerchantCity, c.MerchantState)
                };

                tran.TransactionMetas = new List<TransactionMeta>();
                
                var desId = metaHandler.GetMetaId(c.Description);
                tran.TransactionMetas.Add(new TransactionMeta {
                    MetaId = desId,
                    MetaType = MetaType.Description
                });
                
                var cataId = metaHandler.GetMetaId(c.Category);
                tran.TransactionMetas.Add(new TransactionMeta
                {
                    MetaId = cataId,
                    MetaType = MetaType.Category
                });
                

                transList.Add(tran);
            }

            _adb.Transactions.AddRange(transList);
            _adb.SaveChanges();

        }



        //complete
        public async Task LOCBankMigration() {
            var locAccount = _tdb.Culocs.ToList();
            //var look = _adb.Metas.ToDictionary(x => x.Content, x => x.Id, StringComparer.OrdinalIgnoreCase);
            var metaHandler = new MetaHandling(_adb);
            var transList = new List<Transaction>();
            foreach (var loc in locAccount)
            {
                if (loc.CrDr != "DR" && loc.CrDr != "CR")
                {
                    throw new Exception("not valid action");
                }


                var amount = loc.CrDr == "DR" ? -1 * loc.Amount : loc.Amount;
                var tran = new Transaction
                {
                    PostingDate = loc.PostedDate,
                    Amount = Convert.ToDecimal(amount ?? 0d),
                    ReferenceNumber = loc.SerialNumber,
                    AccountId = 1
                };

                var metaId = metaHandler.GetMetaId( loc.Description);
                
                tran.TransactionMetas = new List<TransactionMeta>{new TransactionMeta
                    {
                        MetaId = metaId,
                        MetaType = MetaType.Description
                    }
                };

                transList.Add(tran);
            }


            _adb.Transactions.AddRange(transList);
            _adb.SaveChanges();
        }
    }
}
