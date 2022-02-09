using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Domain.Enums;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class SessionManager
    {
        public SessionManager(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private IApplicationDbContext _applicationDbContext { get; set; }

        public async Task<Result> SessionCheck()
        {
            var result = await _applicationDbContext.Sessions.LastOrDefaultAsync<Session>();
            if(result is null)
                return Result.Fail(ErrorMessage.NO_SESSION_FOUND);

            if(result.Flag != SessionFlag.OnGoing)
                return Result.Fail(ErrorMessage.NO_SESSION_FOUND);
            else
                return Result.Ok();

        }


    }
}