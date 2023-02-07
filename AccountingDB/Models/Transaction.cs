using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDB.Models
{
    public class Transaction
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime? PostingDate { get; set; }
        
        [Column(TypeName = "date")]
        public DateTime? TransactionDate { get; set; }

        /// <summary>
        /// postive  means credit/deposit/refund
        /// negative means debit/withdrawal/payment
        /// </summary>
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        
        public ICollection<TransactionMeta>? TransactionMetas { get; set; }
        
        public string? ReferenceNumber { get; set; }

        public int AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
        
        public int? TransactionMerchantId { get; set; }
        
        [ForeignKey("TransactionMerchantId")]
        public TransactionMerchant? TransactionMerchant { get; set; }


    }

}