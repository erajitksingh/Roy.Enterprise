using Microsoft.EntityFrameworkCore;
using Roy.Enterprise.API.Entities;

namespace Roy.Enterprise.API.Helpers
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<UserAttendance> UserAttendance { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.Id).IsRequired();
                entity.Property(x => x.FirstName);
                entity.Property(x => x.LastName);
                entity.Property(x => x.Photo);
                entity.Property(x => x.Username);
                entity.Property(x => x.PasswordHash);
                entity.Property(x => x.PasswordSalt);
                entity.Property(x => x.MobileNo);
                entity.Property(x => x.EmailId);
                entity.Property(x => x.Address);
                entity.Property(x => x.CreatedBy);
                entity.Property(x => x.CreatedDate);
                entity.Property(x => x.ModifiedBy);
                entity.Property(x => x.ModifiedDate);
                entity.Property(x => x.LoginDateTime);
                entity.Property(x => x.LogoutDateTime);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.Id).IsRequired();
                entity.Property(x => x.OrderId).IsRequired();
                entity.Property(x => x.Notes);
                entity.Property(x => x.DeliveredLocationStart);
                entity.Property(x => x.DeliveredLocationEnd);
                entity.Property(x => x.Status);
                entity.Property(x => x.CreatedBy);
                entity.Property(x => x.CreatedDate);
                entity.Property(x => x.ModifiedBy);
                entity.Property(x => x.ModifiedDate);
            });

            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.Id).IsRequired();
                entity.Property(x => x.Quantity);
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.Id).IsRequired();
                entity.Property(x => x.ProductLogo);
                entity.Property(x => x.Name);
                entity.Property(x => x.Description);
                entity.Property(x => x.CreatedBy);
                entity.Property(x => x.CreatedDate);
                entity.Property(x => x.ModifiedBy);
                entity.Property(x => x.ModifiedDate);
            });

            modelBuilder.Entity<UserAttendance>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(x => x.Id).IsRequired();
                entity.Property(x => x.UserId).IsRequired();
                entity.Property(x => x.LocationStart);
                entity.Property(x => x.LocationEnd);
                entity.Property(x => x.LoginDateTime);
                entity.Property(x => x.LogoutDateTime);
            });
        }
    }
}
