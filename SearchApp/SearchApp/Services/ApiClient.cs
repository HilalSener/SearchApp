using Newtonsoft.Json;
using SearchApp.Interfaces;
using SearchApp.Utils;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SearchApp.Services
{
    public class ApiClient : IApiClient
    {
        private readonly HttpClient httpClient;
        private bool IsConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;

        public ApiClient()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(Constants.BaseUrl);
        }

        public async Task<T> GetAsync<T>(string url)
        {
            try
            {
                if (IsConnected)
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        string strJson = await response.Content.ReadAsStringAsync();
                        if (!string.IsNullOrWhiteSpace(strJson))
                        {
                            var result = JsonConvert.DeserializeObject<T>(strJson);
                            return result;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return default(T);
        }
    }
}
