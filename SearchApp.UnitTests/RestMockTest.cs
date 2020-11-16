using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using SearchApp.Models;

namespace SearchApp.UnitTests
{
    [TestClass]
    public class RestMockTest
    {
        private readonly string Url = "https://bdk0sta2n0.execute-api.eu-west-1.amazonaws.com/ios-assignment/";

        [TestMethod]
        public void LiveGetItemBySearchTest()
        {
            IRestClient restClient = new RestClient(Url);
            var service = new DataServices(restClient);
            var item = service.GetItemAsync(1, "apple");

            Assert.IsNotNull(item);
            Assert.IsTrue(item.Result.Products.Count > 0);
            Assert.IsTrue(item.Result.Products.All(x => x.ProductName.Contains("apple")));
        }
    }

    public interface ISearchService
    {
        Task<SearchModel> GetItemAsync(int page = 1, string query = null);
    }

    public class DataServices : ISearchService
    {
        private readonly IRestClient _restClient;

        public DataServices(IRestClient restClient)
        {
            _restClient = restClient;
        }

        public async Task<SearchModel> GetItemAsync(int page = 1, string query = null)
        {
            SearchModel result = null;
            var restRequest = new RestRequest($"search?page={page}&query={query}", Method.GET);
            var restResponse = _restClient.Execute<SearchModel>(restRequest);
            if (restResponse.StatusCode.Equals(HttpStatusCode.OK))
            {
                result = JsonConvert.DeserializeObject<SearchModel>(restResponse.Content.ToLower());
            }

            return result;
        }
    }
}