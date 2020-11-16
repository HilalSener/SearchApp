using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SearchApp.Models
{
    public class SearchModel
    {
        public ObservableCollection<ProductModel> Products { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
        public int PageCount { get; set; }
    }
}
