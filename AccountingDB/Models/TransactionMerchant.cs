using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDB.Models
{
    public class TransactionMerchant
    {
        [Key]
        public int Id { get; set; }
        
        public int? LocationId { get; set; }
        [ForeignKey("LocationId")]
        public Location? Location { get; set; }
        
        public int? MerchantId { get; set; }
        [ForeignKey("MerchantId")]
        public Merchant? Merchant { get; set; }

        public int? StateId { get; set; }

        [ForeignKey("StateId")]
        public State? State { get; set; }

        public ICollection<Transaction>? Transactions { get; set; }
    }
}