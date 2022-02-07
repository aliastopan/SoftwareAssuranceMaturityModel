using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Domain.Entities;

namespace SoftwareAssuranceMaturityModel.Application.Common.Interfaces
{
    public interface IUserDbContext
    {
        DbSet<User> Users { get; }

        void SaveChanges();
    }
}