using GamesCatalog.Data.GamesProvider;
using GamesCatalog.Data.GamesProvider.Database;
using GamesCatalog.UI.Catalog;
using GamesCatalog.UI.Create;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using Xamarin.Forms;

namespace GamesCatalog
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer)
        {
            // empty
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            Container.Resolve<IGamesProvider>().Init();

            await NavigationService.NavigateAsync("/NavigationPage/CatalogPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IGamesProvider, GamesProvider>();
            containerRegistry.RegisterSingleton<IDbGames, DbGames>();
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<CatalogPage, CatalogPageViewModel>();
            containerRegistry.RegisterForNavigation<CreatePage, CreatePageViewModel>();
        }
    }
}
