using Autofac;
using SearchApp.Interfaces;
using SearchApp.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SearchApp.ViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        int Page = 1;
        int ItemCount = 0;

        private readonly ISearchService _searchService;

        private string _query = null;
        public string Query
        {
            get { return _query; }
            set
            {
                _query = value;
                OnPropertyChanged();
            }
        }

        private SearchModel _item;
        public SearchModel Item
        {
            get { return _item; }
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ProductModel> Products { get; private set; } = new ObservableCollection<ProductModel>();

        public ICommand LoadMoreDataCommand => new Command(async () => await GetNextPageOfDataAsync());
        public ICommand SearchCommand => new Command<string>(async (string query) =>
        {
            await SearchAsync(query);
        });

        public async Task SearchAsync(string query)
        {
            ResetPage();
            Query = query;
            Item = await _searchService.GetItemAsync(1, Query);
            AddData();
        }

        public SearchViewModel()
        {
            using (ILifetimeScope scope = App.Container.BeginLifetimeScope())
            {
                _searchService = scope.Resolve<ISearchService>();
            }

            Task.Run(async () => await GetFirstPage());
        }

        public async Task GetFirstPage()
        {
            Item = await _searchService.GetItemAsync();

            AddData();
        }

        public void AddData()
        {
            if (Item != null)
            {
                ItemCount = Item.PageSize;
                Page = Item.CurrentPage;
            }

            foreach (var product in Item.Products)
            {
                Products.Add(product);
            }
        }

        public async Task GetNextPageOfDataAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                if (ItemCount < Item.TotalResults)
                {
                    Item = await _searchService.GetItemAsync(++Page, Query);

                    foreach (var product in Item.Products)
                    {
                        Products.Add(product);
                    }

                    ItemCount += Item.PageSize;
                }
            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                });
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void ResetPage()
        {
            Page = 1;
            ItemCount = 0;
            Products.Clear();
            Query = null;
        }
    }
}
