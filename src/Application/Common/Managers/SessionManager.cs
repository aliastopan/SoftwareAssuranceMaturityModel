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
            var result = await _applicationDbContext.Sessions
                .OrderBy(x => x.Id)
                .LastOrDefaultAsync<Session>();

            if(result is null)
                return Result.Fail(ErrorMessage.NO_SESSION_FOUND);

            if(result.Flag != SessionFlag.OnGoing)
                return Result.Fail(ErrorMessage.NO_SESSION_FOUND);
            else
                return Result.Ok();
        }

        public async Task<Result<Session>> GetCurrentSession()
        {
            var result = await _applicationDbContext.Sessions
                .OrderBy(x => x.Id)
                .LastOrDefaultAsync<Session>();

            if(result is null)
                return Result.Fail<Session>(ErrorMessage.NO_SESSION_FOUND);

            if(result.Flag != SessionFlag.OnGoing)
                return Result.Fail<Session>(ErrorMessage.NO_SESSION_FOUND);
            else
                return Result.Ok<Session>(result);
        }

        public Result<List<Session>> GetAllSession()
        {
            var sessions = _applicationDbContext.Sessions.ToList();

            if(sessions is null)
                return Result.Fail<List<Session>>(ErrorMessage.NO_SESSION_FOUND);
            else
                return Result.Ok<List<Session>>(sessions);
        }


    }
}