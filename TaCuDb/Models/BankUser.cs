using System.ComponentModel.DataAnnotations.Schema;

namespace TaCuDb.Models
{
    public class BankUser
    {
        public int BankId { get; set; }
        public int UserId { get; set; }
        
        [ForeignKey("BankId")]
        public BankAccount BankAccount { get; set; }
        
        [ForeignKey("UserId")]
        public User User { get; set; }
    }
}