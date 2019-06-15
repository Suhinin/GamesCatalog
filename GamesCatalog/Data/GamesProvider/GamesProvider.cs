using System.Collections.Generic;
using System.Threading.Tasks;
using GamesCatalog.Data.GamesProvider.Database;
using GamesCatalog.Data.GamesProvider.Domain;

namespace GamesCatalog.Data.GamesProvider
{
    public class GamesProvider : IGamesProvider
    {
        private readonly IDbGames _dbGames;

        public GamesProvider(IDbGames dbGames)
        {
            _dbGames = dbGames;
        }

        public void Init()
        {
            _dbGames.CreateTable();
        }

        public async Task<IList<GameModel>> GetAllAsync()
        {
            return await _dbGames.GetAllAsync();
        }

        public async Task AddAsync(string title, string detail, string author, string image)
        {
            var game = new GameModel
            {
                Title = title,
                Detail = detail,
                Author = author,
                Image = image
            };
            await _dbGames.InsertAsync(game);
        }

        public async Task<IList<GameModel>> SearchAsync(string titleOrAuthor)
        {
            var query = $"select * from GameModel where Title like ? or Author like ?";
            var expression = $"{titleOrAuthor}%";

            return await _dbGames.QueryAsync(query, expression, expression);
        }
    }
}
