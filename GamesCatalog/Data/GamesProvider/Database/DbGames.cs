using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using GamesCatalog.Data.GamesProvider.Domain;
using SQLite;

namespace GamesCatalog.Data.GamesProvider.Database
{
    public class DbGames : IDbGames
    {
        private SQLiteAsyncConnection _sqlite;

        public DbGames()
        {
            var localFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = Path.Combine(localFolderPath, "database.db3");

            _sqlite = new SQLiteAsyncConnection(dbPath);
        }

        public void CreateTable()
        {
            _sqlite.GetConnection().CreateTable<GameModel>();
        }

        public async Task<IList<GameModel>> GetAllAsync()
        {
            return await _sqlite.Table<GameModel>().ToListAsync();
        }

        public async Task InsertAsync(GameModel game)
        {
            await _sqlite.InsertAsync(game);
        }

        public async Task<IList<GameModel>> QueryAsync(string query, params object[] args)
        {
            return await _sqlite.QueryAsync<GameModel>(query, args);
        }
    }
}
