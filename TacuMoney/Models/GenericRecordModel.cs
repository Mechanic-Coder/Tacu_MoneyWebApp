using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TacuDataAccess.Models;

namespace TacuMoney.Models
{
    public class GenericRecordModel
    {
        public DateTime? PostedDate { get; set; }
        public string Description { get; set; }
        public double? Amount { get; set; }
        public List<string> Category { get; set; }
        public string FirstCategory { get; set; }

        public GenericRecordModel()
        {}
        public GenericRecordModel(Culoc record, List<string> stuff)
        {
            PostedDate = record.PostedDate;
            Description = record.Description;
            Amount = record.Amount;
            Category = stuff;
        }

        public GenericRecordModel(Cuccloc record)
        { }
        public GenericRecordModel(TaHuntington record)
        { }
    }
}
