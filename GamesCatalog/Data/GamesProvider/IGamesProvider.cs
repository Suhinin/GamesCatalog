using System.Collections.Generic;
using System.Threading.Tasks;
using GamesCatalog.Data.GamesProvider.Domain;

namespace GamesCatalog.Data.GamesProvider
{
    public interface IGamesProvider
    {
        void Init();

        Task<IList<GameModel>> GetAllAsync();

        Task AddAsync(string title, string detail, string author, string image);

        Task<IList<GameModel>> SearchAsync(string titleOrAuthor);
    }
}
