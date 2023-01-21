using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FurnitureShop.Models
{
    public partial class ModelContext : DbContext
    {
        public ModelContext()
        {
        }

        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<FurnAbout> FurnAbouts { get; set; }
        public virtual DbSet<FurnBank> FurnBanks { get; set; }
        public virtual DbSet<FurnCategory> FurnCategories { get; set; }
        public virtual DbSet<FurnContactu> FurnContactus { get; set; }
        public virtual DbSet<FurnFavourate> FurnFavourates { get; set; }
        public virtual DbSet<FurnHomepage> FurnHomepages { get; set; }
        public virtual DbSet<FurnLogin> FurnLogins { get; set; }
        public virtual DbSet<FurnOrder> FurnOrders { get; set; }
        public virtual DbSet<FurnPayment> FurnPayments { get; set; }
        public virtual DbSet<FurnProduct> FurnProducts { get; set; }
        public virtual DbSet<FurnProductOrder> FurnProductOrders { get; set; }
        public virtual DbSet<FurnRole> FurnRoles { get; set; }
        public virtual DbSet<FurnTestimonial> FurnTestimonials { get; set; }
        public virtual DbSet<FurnUserAccount> FurnUserAccounts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseOracle("USER ID=JOR17_User51;PASSWORD=Ghayath123;DATA SOURCE=94.56.229.181:3488/traindb");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("JOR17_USER51")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP");

            modelBuilder.Entity<FurnAbout>(entity =>
            {
                entity.ToTable("FURN_ABOUT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Paragraph1)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPH1");

                entity.Property(e => e.Paragraph2)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPH2");

                entity.Property(e => e.Paragraph3)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPH3");
            });

            modelBuilder.Entity<FurnBank>(entity =>
            {
                entity.ToTable("FURN_BANK");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.CardNumber)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CARD_NUMBER");

                entity.Property(e => e.Cvv)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CVV");
            });

            modelBuilder.Entity<FurnCategory>(entity =>
            {
                entity.ToTable("FURN_CATEGORY");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CATEGORY_NAME");

                entity.Property(e => e.Imagepath)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("IMAGEPATH");
            });

            modelBuilder.Entity<FurnContactu>(entity =>
            {
                entity.ToTable("FURN_CONTACTUS");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Message)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Namee)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NAMEE");
            });

            modelBuilder.Entity<FurnFavourate>(entity =>
            {
                entity.ToTable("FURN_FAVOURATE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.FurnFavourates)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FAV_FK_PROD");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FurnFavourates)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FAV_FK_USER");
            });

            modelBuilder.Entity<FurnHomepage>(entity =>
            {
                entity.ToTable("FURN_HOMEPAGE");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Address)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("ADDRESS");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.LogoPath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("LOGO_PATH");

                entity.Property(e => e.Paragraph)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("PARAGRAPH");

                entity.Property(e => e.Phone)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.Text1)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("TEXT1");
            });

            modelBuilder.Entity<FurnLogin>(entity =>
            {
                entity.ToTable("FURN_LOGIN");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Passwordd)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("PASSWORDD");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FurnLogins)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FURN_USER_ID3");
            });

            modelBuilder.Entity<FurnOrder>(entity =>
            {
                entity.ToTable("FURN_ORDER");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Datee)
                    .HasColumnType("DATE")
                    .HasColumnName("DATEE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FurnOrders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FURN_USER_ID");
            });

            modelBuilder.Entity<FurnPayment>(entity =>
            {
                entity.ToTable("FURN_PAYMENT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Amount)
                    .HasColumnType("NUMBER")
                    .HasColumnName("AMOUNT");

                entity.Property(e => e.Paydate)
                    .HasColumnType("DATE")
                    .HasColumnName("PAYDATE");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FurnPayments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FURN_USER_ID1");
            });

            modelBuilder.Entity<FurnProduct>(entity =>
            {
                entity.ToTable("FURN_PRODUCT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.CategoryId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("CATEGORY_ID");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Namee)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NAMEE");

                entity.Property(e => e.Price)
                    .HasColumnType("FLOAT")
                    .HasColumnName("PRICE");

                entity.Property(e => e.Valuee)
                    .HasColumnType("FLOAT")
                    .HasColumnName("VALUEE");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.FurnProducts)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK_FURN_CATEGORY_ID");
            });

            modelBuilder.Entity<FurnProductOrder>(entity =>
            {
                entity.ToTable("FURN_PRODUCT_ORDER");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.OrderId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ORDER_ID");

                entity.Property(e => e.ProductId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("PRODUCT_ID");

                entity.Property(e => e.Status)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.FurnProductOrders)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK1_FURN_ORDER_ID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.FurnProductOrders)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK1_FURN_PRODUCT_ID");
            });

            modelBuilder.Entity<FurnRole>(entity =>
            {
                entity.ToTable("FURN_ROLES");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Rolename)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("ROLENAME");
            });

            modelBuilder.Entity<FurnTestimonial>(entity =>
            {
                entity.ToTable("FURN_TESTIMONIAL");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Message)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("MESSAGE");

                entity.Property(e => e.Status)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("STATUS");

                entity.Property(e => e.UserId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("USER_ID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.FurnTestimonials)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FURN_USER_ID2");
            });

            modelBuilder.Entity<FurnUserAccount>(entity =>
            {
                entity.ToTable("FURN_USER_ACCOUNT");

                entity.Property(e => e.Id)
                    .HasColumnType("NUMBER")
                    .ValueGeneratedOnAdd()
                    .HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .HasColumnName("FULLNAME");

                entity.Property(e => e.ImagePath)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("IMAGE_PATH");

                entity.Property(e => e.Phone)
                    .HasMaxLength(35)
                    .IsUnicode(false)
                    .HasColumnName("PHONE");

                entity.Property(e => e.RoleId)
                    .HasColumnType("NUMBER")
                    .HasColumnName("ROLE_ID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.FurnUserAccounts)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_FURN_ROLE_ID");
            });

            modelBuilder.HasSequence("EMP_SEQ1").IncrementsBy(5);

            modelBuilder.HasSequence("EMP_SEQ2").IsCyclic();

            modelBuilder.HasSequence("EMP_SEQ3")
                .IncrementsBy(2)
                .IsCyclic();

            modelBuilder.HasSequence("EMPLOYEE_SEQ").IncrementsBy(2);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
