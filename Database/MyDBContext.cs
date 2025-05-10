using Microsoft.EntityFrameworkCore;
using Social_Network_API.Converters;
using Social_Network_API.Entities;
using Social_Network_API.Enums;

namespace Social_Network_API.Database
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options)
            : base(options) {

            Console.WriteLine(Database.EnsureCreated());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>(e => e.Property(o => o.Name).HasColumnType("VARCHAR(50)"));
            modelBuilder.Entity<User>(e => e.Property(o => o.Email).HasColumnType("VARCHAR(320)"));
            modelBuilder.Entity<User>(e => e.Property(o => o.Password).HasColumnType("CHAR(64)"));
            modelBuilder.Entity<User>(
                e =>
                    e.Property(o => o.CreatedDate)
                        .HasColumnType("BIGINT")
                        .HasColumnName("CreatedDate")
            );
            modelBuilder.Entity<User>(
                e =>
                    e.Property(o => o.Role)
                        .HasColumnName("Role")
                        .HasConversion(new UserRoleConverter())
                        .HasDefaultValue(UserRole.User)
            );
            modelBuilder.Entity<User>(
                e => e.Property(o => o.Salt).HasColumnName("Salt").HasColumnType("CHAR(24)")
            );
            modelBuilder.Entity<User>(
                e =>
                    e.Property(o => o.HasAvatar)
                        .HasColumnName("HasAvatar")
                        .HasColumnType("tinyint(1)")
                        .HasDefaultValue((byte)0)
                        .IsRequired()
            );

            // Subscription relations
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Followers)
                .WithOne(s => s.Following)
                .HasForeignKey(u => u.FollowingId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<User>()
                .HasMany(u => u.Following)
                .WithOne(s => s.Follower)
                .HasForeignKey(u => u.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Subscription>().ToTable("subscriptions");
            modelBuilder.Entity<Subscription>().HasKey(x => x.Id);
            modelBuilder.Entity<Subscription>().ToTable("subscriptions");
            modelBuilder
                .Entity<Subscription>()
                .HasOne(s => s.Follower)
                .WithMany(u => u.Following)
                .HasForeignKey(s => s.FollowerId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder
                .Entity<Subscription>()
                .HasOne(s => s.Following)
                .WithMany(u => u.Followers)
                .HasForeignKey(s => s.FollowingId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
