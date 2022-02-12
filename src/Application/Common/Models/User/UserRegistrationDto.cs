using SoftwareAssuranceMaturityModel.Domain.Enums;

namespace SoftwareAssuranceMaturityModel.Application.Common.Models.User
{
    public class UserRegistrationDto
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public UserRole Role { get; set; }
        public UserFlag Flag { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        public Dictionary<string, int> UserRoles = Enum
            .GetValues(typeof(UserRole))
            .Cast<UserRole>()
            .ToDictionary(key => key.ToString(), value => (int)value);

        public Dictionary<string, int> UserFlags = Enum
            .GetValues(typeof(UserFlag))
            .Cast<UserFlag>()
            .ToDictionary(key => key.ToString(), value => (int)value);
    }
}