using Microsoft.EntityFrameworkCore;
using Examples.EfCore.Entities.Base;
using Examples.EfCore.Entities.Form;

namespace Examples.EfCore.Contexts
{
    public class FormContext : DbContext
    {
        public DbSet<Question>? Question { get; set; }

        public DbSet<Questionnaire>? Questionnaire { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseMySQL("server=localhost;database=mysqlefcore;user=root;password=");
            optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=efcore_forms; User Id=sa; Password=yourStrong(!)Password");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Questionnaire>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.Property(q => q.Name).IsRequired();
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasKey(q => q.Id);
                entity.HasOne(q => q.Questionnaire).WithMany(q => q.Questions);
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