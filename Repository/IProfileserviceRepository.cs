using Newtonsoft.Json.Linq;
using ProfileMicroservice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProfileMicroservice.Repository
{
    public interface IProfileServiceRepository
    {
        Task<bool> CreateProfile(JObject jsonData);

        Task<bool> UpdateProfile(JObject jsonData, Guid ProfileId);

        Task<IEnumerable<JObject>> GetAllProfiles(string FirstName, string LastName);

        Task<JObject> GetProfilebyId(Guid profileId);

        bool DeleteProfilebyId(Guid ProfileId);
    }
}
