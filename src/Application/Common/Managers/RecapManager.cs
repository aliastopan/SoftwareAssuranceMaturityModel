using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Domain.Enums;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;

namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class RecapManager
    {
        public RecapManager(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        private IApplicationDbContext _applicationDbContext;

    }
}