using Microsoft.EntityFrameworkCore;
using Examples.EfCore.Entities.Base;
using Examples.EfCore.Entities.Library;

namespace Examples.EfCore.Contexts
{
    public class LibraryContext : DbContext
    {
        public DbSet<Book>? Book { get; set; }

        public DbSet<Publisher>? Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=localhost;database=mysqlefcore;user=root;password=");


            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=mssqlefcore; User Id=sa; Password=yourStrong(!)Password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publisher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasIndex(e => e.Isbn);
                entity
                .Property(e => e.Title)
                .IsRequired();
                entity
                .HasOne(d => d.Publisher)
                .WithMany(p => p.Books);
            });
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            var utcNow = DateTime.UtcNow;

            foreach (var entry in entries)
            {
                if (entry.Entity is BaseEntity trackable)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            trackable.Created = utcNow;
                            trackable.Modified = utcNow;
                            break;
                        case EntityState.Modified:
                            trackable.Modified = utcNow;
                            entry.Property("Modified").IsModified = false;
                            break;

                    }
                }
            }

        }
    }
}