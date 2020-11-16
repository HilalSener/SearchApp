using SearchApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SearchApp.Interfaces
{
    public interface ISearchService
    {
        Task<SearchModel> GetItemAsync(int page = 1, string query = null);
    }
}
