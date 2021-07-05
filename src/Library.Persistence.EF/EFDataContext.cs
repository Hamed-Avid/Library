using Library.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Persistence.EF
{
    public class EFDataContext : DbContext
    {
        public EFDataContext(DbContextOptions<EFDataContext> options)
               : base(options)
        {
        }
        //public EFDataContext(string connectionString)
        //    : this(new DbContextOptionsBuilder<EFDataContext>().UseSqlServer(connectionString).Options)
        //{
        //}
        //private EFDataContext(DbContextOptions<EFDataContext> options) : base(options) { }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.ApplyConfigurationsFromAssembly(typeof(EFDataContext).Assembly);
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=Library;Trusted_Connection=True;");
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Trustee> Trusteeship { get; set; }
    }
}
