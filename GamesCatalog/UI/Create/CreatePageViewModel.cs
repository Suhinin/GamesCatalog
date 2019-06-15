using System;
using System.Threading.Tasks;
using GamesCatalog.Data.GamesProvider;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

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
        private readonly IPageDialogService _dialogService;

        public CreatePageViewModel(INavigationService navigationService, IGamesProvider gamesProvider, IPageDialogService dialogService)
        {
            _navigationService = navigationService;
            _gamesProvider = gamesProvider;
            _dialogService = dialogService;

            AddCommand = new DelegateCommand(async () => await AddGame());
        }

        private async Task AddGame()
        {
            await _gamesProvider.AddAsync(Title, Detail, Author, Image);
            await _navigationService.GoBackAsync();

            ShowDelayedMessage();
        }

        private void ShowDelayedMessage()
        {
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(5));
                Device.BeginInvokeOnMainThread(async () =>
                    await _dialogService.DisplayAlertAsync("Info", "New game added", "OK"));
            });
        }
    }
}
