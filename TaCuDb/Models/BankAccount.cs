using System.ComponentModel.DataAnnotations;

namespace TaCuDb.Models
{
    public class BankAccount
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        
        public ICollection<BankUser> BankUsers { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}