using Microsoft.EntityFrameworkCore;
using OptimizationLabs.Shared.Entities;

namespace OptimizationLabs.API.Contexts
{
    public class DataContext : DbContext
    {
        public DbSet<Car> Cars { get; set; }
        
        public DbSet<Item> Items { get; set; }
        
        public DbSet<Delivery> Deliveries { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Car>(car => car.HasKey(x => x.Id));

            builder.Entity<Car>()
                .HasMany<Delivery>(x => x.Deliveries)
                .WithOne(x => x.Car)
                .HasForeignKey(x => x.CarId);

            builder.Entity<Item>(item => item.HasKey(x => x.Id));

            builder.Entity<Item>()
                .HasMany<Delivery>(x => x.Deliveries)
                .WithOne(x => x.Item)
                .HasForeignKey(x => x.ItemId);
            
            builder.Entity<Delivery>(delivery => delivery.HasKey(x => x.Id));
        }
    }
}