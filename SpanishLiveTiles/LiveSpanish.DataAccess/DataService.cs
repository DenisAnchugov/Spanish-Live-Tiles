using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Storage;
using LiveSpanish.DataAccess.Entities;
using SQLite;


namespace LiveSpanish.DataAccess
{
    public class DataService
    {      
        public async Task CopyDatabaseAsync()
        {
            var isDatabaseExisting = false;

            try
            {
                StorageFile storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync("Vocabulary.sqlite");
                isDatabaseExisting = true;
            }
            catch
            {
                isDatabaseExisting = false;
            }

            if (!isDatabaseExisting)
            {
                StorageFile databaseFile = await Package.Current.InstalledLocation.GetFileAsync("Vocabulary.sqlite");
                await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
            }
        }

        public async Task<bool> DoesDbExist(string databaseName)
        {
            var dbexist = true;
            try
            {
                var storageFile = await ApplicationData.Current.LocalFolder.GetFileAsync(databaseName);
            }
            catch
            {
                dbexist = false;
            }

            return dbexist;
        }

        public async Task<List<ExpressionEntity>> GetTable()
        {
            await CopyDatabaseAsync();
            const string databasePath = "Vocabulary.sqlite";
            var conn = new SQLiteAsyncConnection(databasePath);
            var result = await conn.QueryAsync<ExpressionEntity>("Select * FROM Numbers");
            return GetNextFive(result);
        }

        public List<ExpressionEntity> GetNextFive(List<ExpressionEntity> list)
        {
            var fiveExpressions = new List<ExpressionEntity>();
            var rnd = new Random();
            for (var i = 0; i < 5; i++)
            {              
                var expression = list[rnd.Next(list.Count)];
                fiveExpressions.Add(expression);
            }
            return fiveExpressions;
        }
    }
}
