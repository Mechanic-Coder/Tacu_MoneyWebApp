using System.ComponentModel.DataAnnotations.Schema;

namespace TaCuDb.Models
{
    public class TransactionMeta
    {
        public int TransactionId { get; set; }
        public int MetaId { get; set; }

        [ForeignKey("TransactionId")]
        public Transaction Transaction { get; set; }

        [ForeignKey("MetaId")]
        public Meta Meta { get; set; }

        public MetaType MetaType { get; set; }

    }


    public enum MetaType
    {
        Category,
        Tag,
        Note,
        Description,
        ExtDescription
    }
}