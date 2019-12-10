using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProfileMicroservice.CQRS.Queries.Contracts;
using ProfileMicroservice.Model;
using ProfileMicroservice.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMicroservice.CQRS.Queries.ViewModels
{
    public class ProfileInformationQueries : IProfileInformationQueries
    {
        private readonly IProfileServiceRepository _profileServiceRepository;
        public ProfileInformationQueries(IProfileServiceRepository profileServiceRepository)
        {
            _profileServiceRepository = profileServiceRepository;
        }
        public async Task<IEnumerable<JObject>> GetProfiles(string FirstName, string LastName)
        {
            return await _profileServiceRepository.GetAllProfiles(FirstName, LastName);
        }

        public async Task<JObject> GetProfileById(Guid ProfileId)
        {
            return await _profileServiceRepository.GetProfilebyId(ProfileId);
        }
    }
}
