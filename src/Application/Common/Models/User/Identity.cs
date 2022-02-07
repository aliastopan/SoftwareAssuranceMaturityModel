using SoftwareAssuranceMaturityModel.Domain.Enums;

namespace SoftwareAssuranceMaturityModel.Application.Common.Models.User
{
    public record struct Identity(
        Guid Id,
        string Username,
        string Password,
        UserRole Role,
        UserFlag Flag,
        string? AuthType
    );
}