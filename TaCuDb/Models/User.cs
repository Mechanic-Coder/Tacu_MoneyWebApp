using System.ComponentModel.DataAnnotations;

namespace TaCuDb.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public ICollection<BankUser> BankUsers { get; set; }
    }
}