using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDB.Models
{
    public class TransactionMeta
    {
        [Key]
        public int Id { get; set; }
        public int TransactionId { get; set; }
        public int MetaId { get; set; }

        [ForeignKey("TransactionId")]
        public Transaction? Transaction { get; set; }

        [ForeignKey("MetaId")]
        public Meta? Meta { get; set; }
        public MetaType MetaType { get; set; }

    }


    public enum MetaType
    {
        Description = 1,
        Category = 2,
        Note = 3, 
        Tag = 4,
        ExtDescription = 5
    }
}