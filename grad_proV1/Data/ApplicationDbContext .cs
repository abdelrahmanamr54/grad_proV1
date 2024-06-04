using grad_proV1.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace grad_proV1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
        {

        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var builder = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", true, true)
               .Build()
               .GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<Laptop>()
            //    .HasOne(l => l.brand)
            //    .WithMany()
            //    .HasForeignKey(l => l.BrandId);
            modelBuilder.Entity<Product>()
    .HasOne(p => p.vendor)
    .WithMany(v => v.products)

    .HasForeignKey(p => p.vendorId).OnDelete(DeleteBehavior.Restrict)
   ; // or DeleteBehavior.NoAction
            modelBuilder.Entity<Vendor>()
 .HasOne(p => p.subcategory)
 .WithMany(v => v.vendors)


 .HasForeignKey(p => p.subcategoyId).OnDelete(DeleteBehavior.Restrict)
; // or DeleteBehavior.NoAction
            modelBuilder.Entity<BookingItem>()
 .HasOne(p => p.Provider)
 .WithMany(v => v.BookingItems)

 .HasForeignKey(p => p.ProviderId).OnDelete(DeleteBehavior.Restrict)
;
        }
        public DbSet<Vendor> vendors { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<SubCategory> subCategories { get; set; }
        public DbSet<Provider> providers { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<UserLoginDTO> UserLoginDTO { get; set; } = default!;
        public DbSet<Order> Orders { get; set; }
        public DbSet<EnrollmentCode> EnrollmentCodes { get; set; }
        public DbSet<BookingItem> BookingItems { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ApplicationUserDTO> applicationUserDTOs { get; set; }
        //  public DbSet<ProductDTO> productsDTO { get; set; } = default!;

    }
}
