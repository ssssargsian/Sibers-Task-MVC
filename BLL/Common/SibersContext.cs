using System.Data.Entity;
using Repository.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repository.Common
{
    public class SibersContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Projects { get; set; }

        static SibersContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<SibersContext>());
        }

        public SibersContext() : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasMany(c => c.Projects)
                .WithRequired(c => c.ProjectManager).HasForeignKey(c=> c.ProjectManagerId);

            modelBuilder.Entity<Employee>()
                .HasMany(c => c.Projects)
                .WithMany(c => c.Employees);
            base.OnModelCreating(modelBuilder);
        }
    }
}
