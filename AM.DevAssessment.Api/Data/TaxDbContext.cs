using AM.DevAssessment.Api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace AM.DevAssessment.Api.Data
{
    public class TaxDbContext : DbContext
    {
        public TaxDbContext(DbContextOptions<TaxDbContext> options) : base(options) { }

        public DbSet<CalculatedTaxAudit> CalculatedTaxAudits { get; set; }
    }
}
