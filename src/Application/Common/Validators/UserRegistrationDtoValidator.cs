using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Common.Models.User;
using FluentValidation;

namespace SoftwareAssuranceMaturityModel.Application.Common.Validators
{
    public class UserRegistrationDtoValidator : AbstractValidator<UserRegistrationDto>
    {
        private readonly IUserDbContext _userDbContext = default!;

        public UserRegistrationDtoValidator(IUserDbContext userDbContext)
        {
            _userDbContext = userDbContext;

            RuleFor(prop => prop.Username)
                .NotEmpty().WithMessage("Username is required.")
                .Length(6, 32).WithMessage("Username should be between 6 and 32 character.");

            When(prop => prop.Username is not null, () => {
                RuleFor(prop => prop.Username)
                    .Must(UsernameAvailable!)
                    .WithMessage("Username is already taken.");
            });

            RuleFor(prop => prop.Password)
                .NotEmpty().WithMessage("Password is required.");

            When(prop => !String.IsNullOrWhiteSpace(prop.Password), () => {
                RuleFor(prop => prop.ConfirmPassword)
                    .NotEmpty().WithMessage("Confirmation password is required.");

                When(prop => !String.IsNullOrWhiteSpace(prop.ConfirmPassword), () => {
                    RuleFor(prop => prop.ConfirmPassword)
                        .Equal(prop => prop.Password)
                        .WithMessage("Password doesn't match.");
                });
            });

            RuleFor(prop => prop.Email)
                .NotEmpty()
                .EmailAddress().WithMessage("Invalid email format.");
        }

        private bool UsernameAvailable(string username)
        {
            var user = _userDbContext.Users.FirstOrDefault(search =>
                search.Username == username
            );

            return user is null;
        }
    }
}