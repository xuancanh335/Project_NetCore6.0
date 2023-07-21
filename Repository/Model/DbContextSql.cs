using Common.Commons;
using Microsoft.EntityFrameworkCore;

namespace Repository.EF
{
    public partial class DbContextSql : DbContext
    {
        private string connectionString = ConfigHelper.Get("ConnectionStrings", "DefaultConnection");
        public DbContextSql()
        {
        }

        public DbContextSql(DbContextOptions<DbContextSql> options)
            : base(options)
        {
        }
        public virtual DbSet<BCC01_User> BCC01_User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
