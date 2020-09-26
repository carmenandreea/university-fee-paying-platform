using Microsoft.EntityFrameworkCore;

namespace TaxationApi.Models
{
    public class TaxationDbContext : DbContext
    {
        public TaxationDbContext(DbContextOptions<TaxationDbContext> options)
           : base(options)
        {
        }


        public DbSet<Payer> Payers { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<AdminUser> AdminUsers { get; set; }
        public DbSet<TaxValue> TaxValues { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }

}