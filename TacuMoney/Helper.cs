using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacuDataAccess.Models;
using TacuMoney.Models;

namespace TacuMoney
{
    public static class Helper
    {
        static public decimal MakeDouble(this string money)
        {
            money = money.Replace("$", "");

            money = money.Replace("(", "-");
            money = money.Replace(")", "");
            
            return System.Convert.ToDecimal(money);
        }

        static public IQueryable<GenericRecordModel> MakeGenericRecord(this IQueryable<Culoc> loc,IQueryable<string> dbCategory) 
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.PostedDate,
                           Description = x.Description,
                           Amount = x.Amount,
                           Category = dbCategory.Where(c => x.Description.IndexOf(c) >= 0).ToList()
                       });
        }

        static public IQueryable<GenericRecordModel> MakeGenericRecord(this IQueryable<Cuccloc> loc, IQueryable<string> dbCategory)
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.PostingDate,
                           Description = x.Description,
                           Amount = (double)x.AmountNum,
                           Category = dbCategory.Where(c => x.MerchantName.IndexOf(c) >= 0).ToList()
                       });
        }


        static public IQueryable<GenericRecordModel> MakeGenericRecord(this IQueryable<TaHuntington> loc, IQueryable<string> dbCategory)
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.Date,
                           Description = x.Description,
                           Amount = (double)(-1 * x.Amount),
                           Category = dbCategory.Where(c => x.Description.IndexOf(c) >= 0).ToList()
                       });
        }

        static public IQueryable<GenericRecordModel> MakeGroupedGenericRecord(this IQueryable<TaHuntington> loc, IQueryable<string> dbCategory)
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.Date,
                           Description = x.Description,
                           Amount = (double)(-1 * x.Amount),
                           FirstCategory = dbCategory.FirstOrDefault(c => x.Description.IndexOf(c) >= 0)
                       });
        }


        static public IQueryable<GenericRecordModel> MakeGroupedGenericRecord(this IQueryable<Culoc> loc, IQueryable<string> dbCategory)
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.PostedDate,
                           Description = x.Description,
                           Amount = x.Amount,
                           FirstCategory = dbCategory.FirstOrDefault(c => x.Description.IndexOf(c) >= 0)
                       });
        }

        static public IQueryable<GenericRecordModel> MakeGroupedGenericRecord(this IQueryable<Cuccloc> loc, IQueryable<string> dbCategory)
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.PostingDate,
                           Description = x.Description,
                           Amount = (double)x.AmountNum,
                           FirstCategory = dbCategory.FirstOrDefault(c => x.MerchantName.IndexOf(c) >= 0)
                       });
        }


        static public IQueryable<GenericRecordModel> MakeGenericRecord(this IQueryable<Culoc> loc)
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.PostedDate,
                           Description = x.Description,
                           Amount = x.Amount
                       });
        }

        static public IQueryable<GenericRecordModel> MakeGenericRecord(this IQueryable<Cuccloc> loc)
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.PostingDate,
                           Description = x.Description,
                           Amount = (double)x.AmountNum
                           
                       });
        }


        static public IQueryable<GenericRecordModel> MakeGenericRecord(this IQueryable<TaHuntington> loc)
        {
            return loc.Select(x =>
                       new GenericRecordModel
                       {
                           PostedDate = x.Date,
                           Description = x.Description,
                           Amount = (double)(-1 * x.Amount)
                       });
        }

    }
}
