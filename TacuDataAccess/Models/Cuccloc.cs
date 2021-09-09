using System;
using System.Collections.Generic;

#nullable disable

namespace TacuDataAccess.Models
{
    public partial class Cuccloc
    {
        public int Id { get; set; }
        public string OriginatingAccountNumber { get; set; }
        public DateTime? PostingDate { get; set; }
        public DateTime? TransDate { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string MerchantName { get; set; }
        public string MerchantCity { get; set; }
        public string MerchantState { get; set; }
        public string Description { get; set; }
        public string TransactionType { get; set; }
        public string Amount { get; set; }
        public string ReferenceNumber { get; set; }
        public string DivertedToAccountLast4 { get; set; }
        public string Column14 { get; set; }
    }
}
