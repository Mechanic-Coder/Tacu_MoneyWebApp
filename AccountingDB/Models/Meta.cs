using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDB.Models
{
    public class Meta
    {
        [Key]
        public int Id { get; set; }


        [Column(TypeName = "varchar(100)")]
        public string? Content { get; set; }

    }
}