using Microsoft.EntityFrameworkCore;
using Social_Network_API.Entities;

namespace Social_Network_API.Database
{
    
    public class MyDBContext:DbContext
    {
        
        public MyDBContext(DbContextOptions<MyDBContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(e=>e.Property(o =>o.Age).HasColumnType("tinyint").HasConversion(
            v => (byte)v,
            v => (int)v));
            modelBuilder.Entity<User>(e => e.Property(o => o.Name).HasColumnType("VARCHAR(50)"));
            modelBuilder.Entity<User>(e => e.Property(o => o.Email).HasColumnType("VARCHAR(320)"));
            modelBuilder.Entity<User>(e => e.Property(o => o.Password).HasColumnType("CHAR(64)"));
            modelBuilder.Entity<User>(e => e.Property(o => o.CreatedDate).HasColumnType("BIGINT").HasColumnName("CreatedDate"));
        }


    }
}
