using UserEntity = SoftwareAssuranceMaturityModel.Domain.Entities.User;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Common.Models.User;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using Mapster;

namespace SoftwareAssuranceMaturityModel.Application.Actions.User
{
    public class CreateUser
    {
        private readonly IUserDbContext _userDbContext;

        public CreateUser(IUserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public async Task InsertAsync(UserRegistrationDto userRegistration)
        {
            var user = userRegistration.Adapt<UserEntity>();
            await _userDbContext.Users.AddAsync(user);
            _userDbContext.SaveChanges();
        }
    }
}