using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDB.Models
{
    public class AccountUser
    {
        public int AccountId { get; set; }
        public int UserId { get; set; }
        
        [ForeignKey("AccountId")]
        public Account? Account { get; set; }
        
        [ForeignKey("UserId")]
        public User? User { get; set; }
    }
}