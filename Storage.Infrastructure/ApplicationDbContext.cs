using Microsoft.EntityFrameworkCore;
using File = Storage.Domain.Models.File;
namespace Storage.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<File> Files { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<File>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<File>()
                .OwnsMany(f => f.Tags, t =>
                {
                    t.WithOwner().HasForeignKey("FileId");
                    t.Property<Guid>("Id");
                    t.HasKey("Id");
                });

            base.OnModelCreating(modelBuilder);
        }
    }

}
