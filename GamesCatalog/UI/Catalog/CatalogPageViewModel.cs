using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GamesCatalog.Data.GamesProvider;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace GamesCatalog.UI.Catalog
{
    public class CatalogPageViewModel : BindableBase, INavigatedAware
    {
        private ObservableCollection<Game> _games;
        public ObservableCollection<Game> Games
        {
            get => _games;
            set => SetProperty(ref _games, value);
        }

        public DelegateCommand AddCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IGamesProvider _gamesProvider;

        public CatalogPageViewModel(INavigationService navigationService, IGamesProvider gamesProvider)
        {
            _navigationService = navigationService;
            _gamesProvider = gamesProvider;

            _games = new ObservableCollection<Game>();
            AddCommand = new DelegateCommand(async() => await AddGame());
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // empty
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            RefreshList();
        }

        private async Task AddGame()
        {
            await _navigationService.NavigateAsync("CreatePage");
        }

        private void RefreshList()
        {
            _games.Clear();
            foreach (var game in _gamesProvider.GetAll())
            {
                _games.Add(new Game 
                {
                    Title = game.Title,
                    Detail = game.Detail,
                    Image = game.Image
                });
            }
        }
    }
}
