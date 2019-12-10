using Newtonsoft.Json.Linq;
using ProfileMicroservice.CacheManager;
using ProfileMicroservice.HttpService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProfileMicroservice.Repository
{
    public class ProfileServiceRepository : IProfileServiceRepository
    {
        private readonly IHttpClientContext _httpClient;
        private readonly ICacheManager _cacheManager;
        public static readonly string Version = "1.0";
        public ProfileServiceRepository(IHttpClientContext httpClient,
                                            ICacheManager cacheManager
                                            )
        {
            _httpClient = httpClient;
            _cacheManager = cacheManager;
        }

        public async Task<bool> CreateProfile(JObject jsonData)
        {
            var response = await _httpClient.AuthorizedPostAsync<JObject>(String.Format("profiles", Version), jsonData);
            if (response != null) return true; else return false;
        }

        public bool DeleteProfilebyId(Guid ProfileId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<JObject>> GetAllProfiles(string FirstName, string LastName)
        {
            return await _httpClient.AuthorizedGetAsync<IEnumerable<JObject>>(String.Format("profiles?firstname={0}&lastname={1}",  FirstName, LastName));
        }

        public async Task<JObject> GetProfilebyId(Guid profileId)
        {
            return await _httpClient.AuthorizedGetAsync<JObject>(String.Format("profiles/{0}", profileId));
        }

        public async Task<bool> UpdateProfile(JObject jsonData, Guid ProfileId)
        {
            return await _httpClient.AuthorizedPutAsync(String.Format("profiles"), jsonData);
        }
    }
}
