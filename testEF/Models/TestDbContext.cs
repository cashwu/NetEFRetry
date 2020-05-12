using System.Data.Entity;

namespace testEF.Models
{
    public class TestDbContext : DbContext
    {
        public TestDbContext(string connectionString)
            : base(connectionString)
        {
            Database.SetInitializer<TestDbContext>(null);
        }

        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                        .Map(a =>
                        {
                            a.ToTable("Person2");
                        });
        }
    }
}