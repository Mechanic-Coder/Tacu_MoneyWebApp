using AccountingDB.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountingDB
{
    public class AccountingContext : DbContext
    {
        public AccountingContext() { }
        public AccountingContext(DbContextOptions<AccountingContext> options) : base(options) { }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountUser> BankUsers { get; set; }
        public virtual DbSet<Meta> Metas { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<TransactionMeta> TransactionMetas { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Merchant> Merchants{ get; set; }
        public virtual DbSet<TransactionMerchant> TransactionMerchants { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Location> Locations { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountUser>().HasKey(x => new { x.UserId, x.AccountId });
            modelBuilder.Entity<Meta>().HasIndex(x => x.Content).IsUnique();
            modelBuilder.Entity<Location>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Merchant>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => x.ShortName).IsUnique();
            modelBuilder.Entity<State>().HasIndex(x => x.FullName).IsUnique();
            modelBuilder.Entity<TransactionMerchant>().HasIndex(x => new { x.LocationId, x.MerchantId, x.StateId }).IsUnique();

            //put the meaning of each enum in the colum properties under "extended properties- MS_Description
            modelBuilder.Entity<TransactionMeta>().Property(x => x.MetaType)
                .HasComment(
                $"{string.Join(", ", Enum.GetValues(typeof(MetaType)).Cast<MetaType>().Select(v => $"{v.ToString()}: {((long)v)}").ToList())}");

            //modelBuilder.Entity<BankAccount>().HasMany(x => x.BankUsers).WithOne(x => x.BankAccount).HasForeignKey(x => x.BankId);
            //modelBuilder.HasDefaultSchema("public");
            //base.OnModelCreating(modelBuilder);
        }
    }
}
