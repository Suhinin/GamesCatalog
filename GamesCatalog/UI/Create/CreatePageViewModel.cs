using System.Threading.Tasks;
using GamesCatalog.Data.GamesProvider;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

namespace GamesCatalog.UI.Create
{
    public class CreatePageViewModel : BindableBase
    {
        public string Title { get; set; }
        public string Detail { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }

        public DelegateCommand AddCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IGamesProvider _gamesProvider;

        public CreatePageViewModel(INavigationService navigationService, IGamesProvider gamesProvider)
        {
            _navigationService = navigationService;
            _gamesProvider = gamesProvider;

            AddCommand = new DelegateCommand(async () => await AddGame());
        }

        private async Task AddGame()
        {
            _gamesProvider.Add(Title, Detail, Author, Image);
            await _navigationService.GoBackAsync();
        }
    }
}
