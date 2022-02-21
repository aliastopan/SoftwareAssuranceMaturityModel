using SoftwareAssuranceMaturityModel.Domain.Enums;

namespace SoftwareAssuranceMaturityModel.Application.Common.Models.Session
{
    public readonly record struct SessionRecord(
        int Id,
        string? Name,
        DateOnly StartDate,
        DateOnly EndDate,
        SessionFlag Flag,
        int TotalRespondents
    );
}