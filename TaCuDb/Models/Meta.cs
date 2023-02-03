using System.ComponentModel.DataAnnotations;

namespace TaCuDb.Models
{
    public class Meta
    {
        [Key]
        public int Id { get; set; }
        public string? Content { get; set; }

    }
}