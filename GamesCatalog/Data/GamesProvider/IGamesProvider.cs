using System;
using System.Collections.Generic;
using GamesCatalog.Data.GamesProvider.Domain;

namespace GamesCatalog.Data.GamesProvider
{
    public interface IGamesProvider
    {
        IList<GameModel> GetAll();

        void Add(string title, string detail, string author, string image);

        IList<GameModel> Search(string titleOrAuthor);
    }
}
