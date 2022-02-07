using SoftwareAssuranceMaturityModel.Domain.Enums;

namespace SoftwareAssuranceMaturityModel.Domain.Entities
{
    public class User
    {
        public User()
        {
            Id = Guid.NewGuid();
            Role = UserRole.Restricted;
            Flag = UserFlag.Unverified;
        }

        public Guid Id { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public UserRole Role { get; set; }
        public UserFlag Flag { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Avatar { get; set; }
        
    }
}