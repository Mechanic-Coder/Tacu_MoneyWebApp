using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaCuDb.Models;

namespace TaCuDb
{
    public class TacuContext : DbContext
    {
        public TacuContext(DbContextOptions<TacuContext> options) : base(options) { }

        public virtual DbSet<Transaction> Transactions { get; set; }
        //public virtual DbSet<Seal> Seals { get; set; }
        //public virtual DbSet<SealProfile> SealProfiles { get; set; }
        //public virtual DbSet<Vendor> Vendors { get; set; }
        //public virtual DbSet<Image> Images { get; set; }
        //public virtual DbSet<ScrapePrep> ScrapePreps { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    //modelBuilder.HasDefaultSchema("public");
        //    //base.OnModelCreating(modelBuilder);
        //}
    }
}
