using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Domain.Entities;

namespace SoftwareAssuranceMaturityModel.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Session> Sessions { get; }
        void SaveChanges();
    }
}