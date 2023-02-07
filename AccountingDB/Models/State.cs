using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDB.Models
{
    public class State
    {
        [Key]
        public int Id { get; set; }
        
        [Column(TypeName = "varchar(100)")]
        public string? FullName { get; set; }
        
        [Column(TypeName = "varchar(10)")]
        public string? ShortName { get; set; }
        
        public ICollection<TransactionMerchant>? TransactionMerchants { get; set; }
    }
}