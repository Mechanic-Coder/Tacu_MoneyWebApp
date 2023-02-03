using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TacuDataAccess.Models;
#nullable disable

namespace TacuDataAccess
{
    public partial class TacuMoneyContext : DbContext
    {
        public TacuMoneyContext()
        {
        }

        public TacuMoneyContext(DbContextOptions<TacuMoneyContext> options) : base(options)
        {
        }

        public virtual DbSet<Cuccloc> Cucclocs { get; set; }
        public virtual DbSet<Culoc> Culocs { get; set; }
        public virtual DbSet<KeyWord> KeyWords { get; set; }
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<TaHuntington> TaHuntington { get; set; }


//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
////#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=DESKTOP-UE09IBH;Database=TacuMoney;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            //modelBuilder.Entity<Category>(entity =>
            //{
            //    entity.ToTable("Categorys");
            //    entity.Property(e => e.Name).HasColumnType("text");
            //    entity.Property(e => e.KeyWord).HasColumnType("text");
            //});


            modelBuilder.Entity<Cuccloc>(entity =>
            {
                entity.ToTable("CUCCLoc");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("text");

                entity.Property(e => e.Category).HasMaxLength(50);

                entity.Property(e => e.Column14)
                    .HasMaxLength(1)
                    .HasColumnName("column14");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.DivertedToAccountLast4)
                    .HasMaxLength(50)
                    .HasColumnName("Diverted_To_Account_Last4");

                entity.Property(e => e.MerchantCity)
                    .HasMaxLength(50)
                    .HasColumnName("Merchant_City");

                entity.Property(e => e.MerchantName)
                    .HasMaxLength(50)
                    .HasColumnName("Merchant_Name");

                entity.Property(e => e.MerchantState)
                    .HasMaxLength(50)
                    .HasColumnName("Merchant_State");

                entity.Property(e => e.OriginatingAccountNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Originating_Account_Number");

                entity.Property(e => e.PostingDate).HasColumnName("Posting_Date");

                entity.Property(e => e.ReferenceNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Reference_Number");

                entity.Property(e => e.TransDate).HasColumnName("Trans_Date");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(50)
                    .HasColumnName("Transaction_Type");

                entity.Property(e => e.Type).HasMaxLength(50);
            });

            modelBuilder.Entity<Culoc>(entity =>
            {
                entity.ToTable("CULoc");

                entity.Property(e => e.CrDr)
                    .HasMaxLength(50)
                    .HasColumnName("CR_DR");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.PostedDate).HasColumnName("Posted_Date");

                entity.Property(e => e.SerialNumber)
                    .HasMaxLength(50)
                    .HasColumnName("Serial_Number");
            });



            modelBuilder.Entity<KeyWord>(entity =>
            {
                entity.ToTable("keyWords");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .HasColumnName("ID");

                entity.Property(e => e.Amazon).HasMaxLength(50);

                entity.Property(e => e.Automotive).HasMaxLength(50);

                entity.Property(e => e.BadHabit)
                    .HasMaxLength(50)
                    .HasColumnName("Bad_habit");

                entity.Property(e => e.BadHabits)
                    .HasMaxLength(50)
                    .HasColumnName("Bad_habits");

                entity.Property(e => e.Fun)
                    .HasMaxLength(50)
                    .HasColumnName("fun");

                entity.Property(e => e.Gas).HasMaxLength(50);

                entity.Property(e => e.Grocerys)
                    .HasMaxLength(50)
                    .HasColumnName("grocerys");

                entity.Property(e => e.Merchandise).HasMaxLength(50);

                entity.Property(e => e.Restruant)
                    .HasMaxLength(50)
                    .HasColumnName("restruant");

                entity.Property(e => e.StudentLoans)
                    .HasMaxLength(50)
                    .HasColumnName("student_Loans");

                entity.Property(e => e.UtilitiesRent)
                    .HasMaxLength(50)
                    .HasColumnName("Utilities_Rent");

                entity.Property(e => e.WithDrawls)
                    .HasMaxLength(50)
                    .HasColumnName("withDrawls");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
