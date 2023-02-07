using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDB.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "varchar(100)")]
        public string? Name { get; set; }
        public ICollection<TransactionMerchant>? TransactionMerchants { get; set; }
    }
}