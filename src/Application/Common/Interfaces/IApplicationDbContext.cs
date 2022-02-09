using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Domain.Entities;

namespace SoftwareAssuranceMaturityModel.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Session> Sessions { get; }
        DbSet<Batch> Batches { get; }
        DbSet<Respond> Responds { get; }

        void SaveChanges();
    }
}