using System.Text.Json;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Domain.Enums;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Common.Models.User;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class AccountManager
    {
        private IUserDbContext _userDbContext;

        public AccountManager(IUserDbContext userDbContext)
        {
            _userDbContext = userDbContext;
        }

        public void AccountCheck()
        {
            var users = _userDbContext.Users.ToList();

            Console.WriteLine($"Total User: {users.Count}");
        }

        public void AddBulk_DO_NOT_CALL()
        {
            string source = Marshal.GetDataStorage("1000-users.json", @"..\..\..\");
            using (StreamReader reader = new StreamReader(source))
            {
                string json = reader.ReadToEnd();
                List<UserFragment> userFragments = JsonSerializer.Deserialize<List<UserFragment>>(json)!;

                List<User> users = new();

                Console.WriteLine($"Total {userFragments.Count}");

                for (int i = 0; i < userFragments.Count; i++)
                {
                    var user = new User{
                        Username = userFragments[i].Username,
                        Role = UserRole.None,
                        Flag = UserFlag.Verified
                    };
                    user.Password = Cryptograph.GetHash(userFragments[i].Password!, user.Id.ToString());
                    users.Add(user);

                }
                _userDbContext.Users.AddRange(users);
                _userDbContext.SaveChanges();
            }
        }
    }
}