using System;
using System.Collections.Generic;
using System.Linq;
using GamesCatalog.Data.GamesProvider.Domain;

namespace GamesCatalog.Data.GamesProvider
{
    public class GamesProvider : IGamesProvider
    {
        private readonly List<GameModel> _games = new List<GameModel>();

        public IList<GameModel> GetAll()
        {
            return _games;
        }

        public void Add(string title, string detail, string author, string image)
        {
            _games.Add(new GameModel 
            {
                Title = title, 
                Detail = detail,
                Author = author,
                Image = image
            });
        }

        public IList<GameModel> Search(string title, string author)
        {
            return _games.Where(g => g.Title.Contains(title) || g.Author.Contains(author)).ToList();
        }
    }
}
