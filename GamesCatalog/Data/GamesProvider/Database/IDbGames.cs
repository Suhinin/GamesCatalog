using System.Collections.Generic;
using System.Threading.Tasks;
using GamesCatalog.Data.GamesProvider.Domain;

namespace GamesCatalog.Data.GamesProvider.Database
{
    public interface IDbGames
    {
        void CreateTable();

        Task InsertAsync(GameModel game);

        Task<IList<GameModel>> GetAllAsync();

        Task<IList<GameModel>> QueryAsync(string query, params object[] args);
    }
}
