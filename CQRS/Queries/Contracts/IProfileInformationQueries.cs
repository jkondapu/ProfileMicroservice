using Newtonsoft.Json.Linq;
using ProfileMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMicroservice.CQRS.Queries.Contracts
{
    public interface IProfileInformationQueries
    {
        Task<IEnumerable<JObject>> GetProfiles(string FirstName, string LastName);
        Task<JObject> GetProfileById(Guid ProfileId);
    }
}
