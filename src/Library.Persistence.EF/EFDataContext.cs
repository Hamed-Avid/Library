using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.EF
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(string connectionString)
            : this(new DbContextOptionsBuilder<EFDataContext>().UseSqlServer(connectionString).Options)
        {
        }
        private EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Trustee> Trusteeship { get; set; }
    }
}
