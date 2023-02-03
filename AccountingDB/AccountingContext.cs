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
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountUser>().HasKey(x => new { x.UserId, x.AccountId });
            modelBuilder.Entity<Meta>().HasIndex(x => x.Content).IsUnique();

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
