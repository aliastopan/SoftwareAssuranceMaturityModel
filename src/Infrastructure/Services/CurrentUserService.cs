using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Domain.Entities;

namespace SoftwareAssuranceMaturityModel.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public Guid UserId { get; private set; }

        public void SetCurrentUser(Guid id)
        {
            UserId = id;
        }
    }
}