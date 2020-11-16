using System.Threading.Tasks;

namespace SearchApp.Interfaces
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(string url);
    }
}
