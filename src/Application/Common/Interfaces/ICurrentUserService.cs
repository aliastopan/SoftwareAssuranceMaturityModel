using SoftwareAssuranceMaturityModel.Domain.Entities;

namespace SoftwareAssuranceMaturityModel.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        void SetCurrentUser(Guid id);
    }
}