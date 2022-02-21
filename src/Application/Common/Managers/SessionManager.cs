using Microsoft.EntityFrameworkCore;
using Mapster;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Models.Session;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Domain.Enums;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class SessionManager
    {
        public List<Session> Sessions { get; set; }
        public List<SessionRecord> SessionRecords { get; set; } = new();

        public SessionManager(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            Sessions = _applicationDbContext.Sessions.ToList();

            for (int i = 0; i < Sessions.Count; i++)
            {
                int totalRespondent = _applicationDbContext.Batches
                    .Where(x => x.Id == Sessions[i].Id).ToList().Count;

                var sessionRecord = new SessionRecord(
                    Sessions[i].Id,
                    Sessions[i].Name,
                    Sessions[i].StartDate,
                    Sessions[i].EndDate,
                    Sessions[i].Flag,
                    totalRespondent
                );

                SessionRecords.Add(sessionRecord);
            }
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

        public bool IsSessionOnGoing(int sessionsIndex)
        {
            return Sessions[sessionsIndex].Flag == SessionFlag.OnGoing;
        }

        public int GetTotalRespondent(int sessionsIndex)
        {
            var batches = _applicationDbContext.Batches.ToList();
            return batches.Count;
        }


    }
}