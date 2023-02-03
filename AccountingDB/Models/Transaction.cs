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
        /// postive means debit 
        /// negative means credit
        /// </summary>
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
        
        public ICollection<TransactionMeta>? TransactionMetas { get; set; }
        
        public int TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        public TransactionType? TransactionType { get; set; }

        public string? ReferenceNumber { get; set; }

        public int BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public Account? Account { get; set; }


    }

}