using System.Threading.Tasks;

namespace ProfileMicroservice.HttpService
{
    public interface IHttpClientContext
    {
        Task<T> AuthorizedGetAsync<T>(string uri);
        Task<T> AuthorizedPostAsync<T>(string uri, object data);
        Task<bool> AuthorizedPutAsync(string uri, object data);
    }
}