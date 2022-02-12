namespace SoftwareAssuranceMaturityModel.Domain.Enums
{
    public enum UserRole : byte
    {
        Restricted = 0,
        Supervisor = 1,
        Administrator = 2,
        Developer = 99
    }
}
