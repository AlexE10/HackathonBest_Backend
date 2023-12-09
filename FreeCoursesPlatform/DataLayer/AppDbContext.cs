
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // string configurationString = ConfigurationManager.ConnectionStrings["MsSqlServerConnectionString"].ConnectionString;

            optionsBuilder
                .UseSqlServer("Server=localhost;Database=CoursesPlatform;User Id=user;Password=pass;Encrypt=False")
                .LogTo(Console.WriteLine);

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("User");
            //modelBuilder.Entity<GamingPubGamingPlatform>()
            //.HasKey(gp => new { gp.GamingPubId, gp.GamingPlatformId });
            modelBuilder.Entity<User>()
            .HasMany(u => u.Courses)
            .WithMany(c => c.Users);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<User> Users { get; set;}
        public DbSet<Category> Categories { get; set; }
    }
}