using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using SoftwareAssuranceMaturityModel.Application.Common.Models.User;
using SoftwareAssuranceMaturityModel.Application.Common.Sercurity;
using SoftwareAssuranceMaturityModel.Application.Common.Validators;
using SoftwareAssuranceMaturityModel.Application.Common.Managers;
using SoftwareAssuranceMaturityModel.Application.Actions.Session;

namespace SoftwareAssuranceMaturityModel.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<IValidator<UserRegistrationDto>, UserRegistrationDtoValidator>();
            services.AddScoped<Authentication>();

            services.AddTransient<CreateSession>();
            services.AddTransient<SurveyManager>();

            return services;
        }
    }
}