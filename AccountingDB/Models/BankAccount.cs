﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AccountingDB.Models
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string? Name { get; set; }
        
        public ICollection<AccountUser>? AccountUser { get; set; }
        public ICollection<Transaction>? Transactions { get; set; }
    }
}