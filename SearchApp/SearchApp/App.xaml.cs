using Autofac;
using SearchApp.Interfaces;
using SearchApp.Services;
using Xamarin.Forms;

namespace SearchApp
{
    public partial class App : Application
    {
        public static IContainer Container { get; private set; }
        protected ContainerBuilder ContainerBuilder { get; private set; }

        public App()
        {
            InitializeComponent();

            ContainerBuilder = new ContainerBuilder();
            RegisterDependencies();
            Container = ContainerBuilder.Build();

            MainPage = new Views.SearchPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected void RegisterDependencies()
        {
            ContainerBuilder.RegisterType<ApiClient>().As<IApiClient>();
            ContainerBuilder.RegisterType<SearchService>().As<ISearchService>();
        }
    }
}
