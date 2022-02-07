using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Infrastructure.Persistence.Configurations;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Domain.Entities;

namespace SoftwareAssuranceMaturityModel.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        { }

        public DbSet<Session> Sessions => Set<Session>();

        public new void SaveChanges()
            => base.SaveChanges();
    }
}