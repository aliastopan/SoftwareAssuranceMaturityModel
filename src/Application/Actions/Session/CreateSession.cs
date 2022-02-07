using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
using SoftwareAssuranceMaturityModel.Application.Common.Models.Session;
using SessionEntity = SoftwareAssuranceMaturityModel.Domain.Entities.Session;
using Mapster;

namespace SoftwareAssuranceMaturityModel.Application.Actions.Session
{
    public class CreateSession
    {
        public CreateSession(IApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        private readonly IApplicationDbContext _appDbContext;

        public async Task Add(NewSessionDto newSessionDto)
        {
            SessionEntity session = newSessionDto.Adapt<SessionEntity>();

            await _appDbContext.Sessions.AddAsync(session);
            _appDbContext.SaveChanges();
        }

    }
}