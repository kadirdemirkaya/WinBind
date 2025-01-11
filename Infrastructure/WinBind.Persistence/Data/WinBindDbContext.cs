using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WinBind.Domain.Entities;
using WinBind.Domain.Entities.Identity;

namespace WinBind.Persistence.Data
{
    public class WinBindDbContext : IdentityDbContext<AppUser, AppRole, Guid, AppUserClaim, AppUserRole, AppUserLogin, AppRoleClaim, AppUserToken>
    {
        public WinBindDbContext()
        {

        }
        public WinBindDbContext(DbContextOptions<WinBindDbContext> options)
            : base(options)
        {
        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Auction> Auctions { get; set; }
        public DbSet<AuctionResult> AuctionResults { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=WinBidDb;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<AppUser>()
                .HasMany(p => p.Products)
                .WithOne(pi => pi.AppUser)
                .HasForeignKey(pi => pi.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Product>()
               .HasMany(p => p.ProductImages)
               .WithOne(pi => pi.Product)
               .HasForeignKey(pi => pi.ProductId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProductImage>()
                .HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.AppUser)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId);

            modelBuilder.Entity<AuctionResult>()
                .HasOne(ar => ar.WinningBidDetails)
                .WithMany()
                .HasForeignKey(ar => ar.WinningBidId);

            modelBuilder.Entity<Bid>()
                .HasOne(b => b.AppUser)
                .WithMany(u => u.Bids)
                .HasForeignKey(b => b.UserId);

            modelBuilder.Entity<Basket>()
                .HasOne(c => c.AppUser)
                .WithMany(u => u.Baskets)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Order)
                .WithMany(o => o.Payments)
                .HasForeignKey(p => p.OrderId);

            modelBuilder.Entity<Auction>()
                .HasOne(a => a.Product)
                .WithMany(p => p.Auctions)
                .HasForeignKey(a => a.ProductId);

            modelBuilder.Entity<AuctionResult>()
                .HasOne(ar => ar.Auction)
                .WithOne(a => a.AuctionResult)
                .HasForeignKey<AuctionResult>(ar => ar.AuctionId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Bid>()
                .HasOne(b => b.Auction)
                .WithMany(a => a.Bids)
                .HasForeignKey(b => b.AuctionId);

            modelBuilder.Entity<BasketItem>()
                .HasOne(ci => ci.Basket)
                .WithMany(c => c.BasketItems)
                .HasForeignKey(ci => ci.BasketId);

            modelBuilder.Entity<BasketItem>()
                .HasOne(ci => ci.Product)
                .WithMany(p => p.BasketItems)
                .HasForeignKey(ci => ci.ProductId);
        }
    }
}
