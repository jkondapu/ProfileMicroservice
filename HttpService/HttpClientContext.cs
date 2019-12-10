using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ProfileMicroservice.CacheManager;
using ProfileMicroservice.ExceptionHandler;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using ProfileMicroservice.Model;
using System.Threading;

namespace ProfileMicroservice.HttpService
{
    public class HttpClientContext : IHttpClientContext
    {
        private readonly HttpClient _httpClient;
        private readonly ICacheManager _cacheManager;
        private readonly IConfiguration _configuration;
        public HttpClientContext(HttpClient httpClient,
                                 ICacheManager cacheManager,
                                 IConfiguration configuration)
        {
            httpClient.BaseAddress = new Uri(configuration["WebApiBaseUrl"]);


            _httpClient = httpClient;
            _cacheManager = cacheManager;
            _configuration = configuration;
        }

        public async Task<T> AuthorizedGetAsync<T>(string uri)
        {
            AddAuthorizationHeaderAsync();

            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                throw new ProfileExceptionHandler(response);

            var stringResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(stringResponse,
                 new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }
        public async Task<T> AuthorizedPostAsync<T>(string uri, object data)
        {
            AddAuthorizationHeaderAsync();

            HttpResponseMessage response = await _httpClient.PostAsync(uri,
               data != null ? new StringContent(JsonConvert.SerializeObject(data,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                Encoding.UTF8,
                "application/json") : null
                ).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new ProfileExceptionHandler(response);

            var stringResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(stringResponse);
        }
        public async Task<bool> AuthorizedPutAsync(string uri, object data)
        {
            AddAuthorizationHeaderAsync();
            HttpResponseMessage response = await _httpClient.PutAsync(uri,
                data != null ? new StringContent(JsonConvert.SerializeObject(data,
                new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                Encoding.UTF8,
                "application/json") : null
                ).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                throw new ProfileExceptionHandler(response);
            return response.IsSuccessStatusCode;
        }
        private void AddAuthorizationHeaderAsync()
        {
            _httpClient.DefaultRequestHeaders.AcceptLanguage.Add(new StringWithQualityHeaderValue("en-US"));
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var token = _cacheManager.Get<string>("UserToken");
            if (token == null)
                token = CreateAccessTokenAsync();//"88db68e1-339a-4d4b-ae44-dfd761f1647b";//

            _httpClient.DefaultRequestHeaders.Remove("Authorization");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "OAuth " + token);

        }

        private string CreateAccessTokenAsync()
        {
            var basicToken = _configuration["FusionLogin:FusionKey"] + ":" + _configuration["FusionLogin:FusionSecret"];
            var encbuff = Encoding.UTF8.GetBytes(basicToken);
            var basicTokenEncoded = Convert.ToBase64String(encbuff);

            _httpClient.DefaultRequestHeaders.Add("Authorization", "Basic " + basicTokenEncoded);

            var data = "grant_type=password&username=" + _configuration["FusionLogin:SystemUser"] + "&password=" + _configuration["FusionLogin:SystemPassword"] + "&response_type=token";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "authorization/tokens");
            request.Content = new StringContent(data,
                                                Encoding.UTF8,
                                                "application/x-www-form-urlencoded");
            var response = _httpClient.SendAsync(request).GetAwaiter().GetResult();

            if (!response.IsSuccessStatusCode)
                throw new ProfileExceptionHandler(response);

            var stringResponse = response.Content.ReadAsStringAsync();
            var _accessToken = JsonConvert.DeserializeObject<AccessMaintainerToken>(stringResponse.Result);

            _cacheManager.Add("UserToken", _accessToken.AccessToken, TimeSpan.FromMinutes(5));
            return _accessToken.AccessToken;
        }
    }


}
