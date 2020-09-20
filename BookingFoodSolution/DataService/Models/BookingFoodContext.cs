using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DataService.Models
{
    public partial class BookingFoodContext : DbContext
    {
        public BookingFoodContext()
        {
        }

        public BookingFoodContext(DbContextOptions<BookingFoodContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Account { get; set; }
        public virtual DbSet<AccountNotification> AccountNotification { get; set; }
        public virtual DbSet<Banner> Banner { get; set; }
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Notification> Notification { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<OrderDetail> OrderDetail { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<ProductImage> ProductImage { get; set; }
        public virtual DbSet<Store> Store { get; set; }
        public virtual DbSet<Transaction> Transaction { get; set; }
        public virtual DbSet<UserFeedBack> UserFeedBack { get; set; }
        public virtual DbSet<Wallet> Wallet { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"server=54.169.64.6;database=BookingFood;user=sa;pwd=zaQ@1234;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.DeviceToken)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.Gmail)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.UrlImage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AccountNotification>(entity =>
            {
                entity.HasKey(e => new { e.AccountId, e.NotificationId });

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountNotification)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountNotification_Account");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.AccountNotification)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AccountNotification_Notification");
            });

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.CollectionDetail).HasMaxLength(300);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(300);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.UrlImage)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Banner)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Banner_Store");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Content).HasMaxLength(100);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.Property(e => e.UrlImage)
                    .HasMaxLength(500)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Note).HasMaxLength(50);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Order)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Order_Account");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ProductId });

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetail)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetail_Product");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.Payment)
                    .HasForeignKey<Payment>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payment_Transaction");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.DefaultUrlImage)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.ExpirationDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Product_Category");

                entity.HasOne(d => d.Store)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.StoreId)
                    .HasConstraintName("FK_Product_Store");
            });

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.Property(e => e.UrlImage)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImage)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_ProductImage_Product");
            });

            modelBuilder.Entity<Store>(entity =>
            {
                entity.Property(e => e.AccountId).HasColumnName("AccountID");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.IdAddress).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.UrlImageDefault)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Store)
                    .HasForeignKey(d => d.AccountId)
                    .HasConstraintName("FK_Store_Account");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_Transaction_Order");

                entity.HasOne(d => d.Wallet)
                    .WithMany(p => p.Transaction)
                    .HasForeignKey(d => d.WalletId)
                    .HasConstraintName("FK_Transaction_Wallet");
            });

            modelBuilder.Entity<UserFeedBack>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.OrderId).ValueGeneratedNever();

                entity.Property(e => e.Created).HasColumnType("datetime");

                entity.Property(e => e.FeedBack).HasMaxLength(100);

                entity.HasOne(d => d.Order)
                    .WithOne(p => p.UserFeedBack)
                    .HasForeignKey<UserFeedBack>(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserFeedBack_Order");
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Wallet)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Wallet_Account");
            });
        }
    }
}
