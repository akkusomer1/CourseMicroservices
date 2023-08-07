using CourseMicroservices.Services.Order.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using orderAlies = CourseMicroservices.Services.Order.Domain.OrderAggregate.FreeCourse.Services.Order.Domain.OrderAggregate;

namespace CourseMicroservices.Services.Order.Instrastructure
{
    public class OrderDbContext:DbContext
    {
        private const string DEFAULT_SCHEMA="ordering";
        public OrderDbContext(DbContextOptions<OrderDbContext> options):base(options) 
        {
            
        }

        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<orderAlies.Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<orderAlies.Order>().ToTable("Orders", DEFAULT_SCHEMA);
            modelBuilder.Entity<OrderItem>().ToTable("OrderItems", DEFAULT_SCHEMA);
            modelBuilder.Entity<orderAlies.Order>().OwnsOne(o => o.Address).WithOwner();
            base.OnModelCreating(modelBuilder);
        }
    }
}
