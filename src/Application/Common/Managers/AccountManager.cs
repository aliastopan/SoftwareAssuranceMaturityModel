using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SoftwareAssuranceMaturityModel.Domain.Entities;
using SoftwareAssuranceMaturityModel.Domain.Enums;
using SoftwareAssuranceMaturityModel.Application.Common.Helpers;
using SoftwareAssuranceMaturityModel.Application.Common.Interfaces;
namespace SoftwareAssuranceMaturityModel.Application.Common.Managers
{
    public class AccountManager
    {
        private void AddBulk()
        {
            string source = Marshal.GetDataStorage("1000-users.json", @"..\..\..\");
            using (StreamReader reader = new StreamReader(source))
            {

            }
        }
    }
}