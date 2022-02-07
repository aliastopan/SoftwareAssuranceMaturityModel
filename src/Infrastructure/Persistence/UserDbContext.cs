using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Infrastructure.Persistence.Configurations;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Domain.Entities;

namespace SoftwareAssuranceMaturityModel.Infrastructure.Persistence
{
    public class UserDbContext : DbContext, IUserDbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            :base(options)
        { }

        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            new UserConfiguration().Configure(builder.Entity<User>());
            base.OnModelCreating(builder);
        }

        public new void SaveChanges()
            => base.SaveChanges();
    }
}