using System.ComponentModel.DataAnnotations.Schema;

namespace TaCuDb.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateOnly PostingDate { get; set; }
        public DateOnly TransactionDate { get; set; }
        
        /// <summary>
        /// postive means debit 
        /// negative means credit
        /// </summary>
        public decimal Amount { get; set; }
        
        public ICollection<TransactionMeta> TransactionMetas { get; set; }

        //public int? MemoId { get; set; }
        //public int? MemoId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? ReferenceNumber { get; set; }

        public int BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public BankAccount BankAccount { get; set; }


    }

    public enum TransactionType
    {
        POS,
        Check,
        BillPay,
        Card,
        ACH,
        ATM,
        Fees



    }
}