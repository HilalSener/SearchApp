using SearchApp.Interfaces;
using SearchApp.Models;
using System.Threading.Tasks;

namespace SearchApp.Services
{
    public class SearchService : ISearchService
    {
        private readonly IApiClient _apiClient;

        public SearchService()
        {
            _apiClient = new ApiClient();
        }

        public async Task<SearchModel> GetItemAsync(int page = 1, string query = null)
        {
            string url = $"search?page={page}&query={query}";
            SearchModel result = await _apiClient.GetAsync<SearchModel>(url);
            return result;
        }
    }
}
