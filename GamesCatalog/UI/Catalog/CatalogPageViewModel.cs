﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using GamesCatalog.Data.GamesProvider;
using GamesCatalog.Data.GamesProvider.Domain;
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

        private string _searchExpression;
        public string SearchExpression
        {
            get => _searchExpression;
            set => SetProperty(ref _searchExpression, value);
        }

        public DelegateCommand AddCommand { get; private set; }
        public DelegateCommand<string> SearchCommand { get; private set; }

        private readonly INavigationService _navigationService;
        private readonly IGamesProvider _gamesProvider;

        public CatalogPageViewModel(INavigationService navigationService, IGamesProvider gamesProvider)
        {
            _navigationService = navigationService;
            _gamesProvider = gamesProvider;

            _games = new ObservableCollection<Game>();
            AddCommand = new DelegateCommand(async() => await AddGame());
            SearchCommand = new DelegateCommand<string>(async (arg) => await SearchAsync(arg));
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            // empty
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Task.Run(async() => await SearchAsync(SearchExpression));
        }

        private async Task AddGame()
        {
            await _navigationService.NavigateAsync("CreatePage");
        }

        private async Task SearchAsync(string expression)
        {
            var task = expression == null 
                ? _gamesProvider.GetAllAsync() 
                : _gamesProvider.SearchAsync(expression);

            var games = await task;
            RefreshList(games);
        }

        private void RefreshList(IList<GameModel> games)
        {
            _games.Clear();
            foreach (var game in games)
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
