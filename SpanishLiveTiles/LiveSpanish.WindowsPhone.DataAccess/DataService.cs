using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Storage;
using LiveSpanish.WindowsPhone.DataAccess.Entities;
using SQLite;


namespace LiveSpanish.WindowsPhone.DataAccess
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


        public async Task<WordsContainer> GetTableAsync()
        {
            var data = new SettingsService();
            List<VocabularySetEnum> sets = await data.RetrieveSelectedSets();
            await CopyDatabaseAsync();
            const string databasePath = "Vocabulary.sqlite";
            var conn = new SQLiteAsyncConnection(databasePath);
            List<ExpressionEntity> wordsTotal = new List<ExpressionEntity>();

            foreach (var set in sets)
            {
                var result = await conn.QueryAsync<ExpressionEntity>("Select * FROM " + set.ToString());
                wordsTotal.AddRange(result);
            }
            return GetNextFive(wordsTotal);
        }

        public WordsContainer GetNextFive(List<ExpressionEntity> list)
        {
            var fiveExpressions = new WordsContainer();
            for (var i = 0; i < 5; i++)
            {
                fiveExpressions.LongWords = (from expression in list
                                             where expression.ExpressionLength <= 21
                                             select expression).OrderBy(x => Guid.NewGuid()).Take(5).ToList();

                fiveExpressions.ShortWords = (from expression in list
                                              where expression.ExpressionLength <= 16
                                              select expression).OrderBy(x => Guid.NewGuid()).Take(5).ToList();
            }
            return fiveExpressions;
        }
    }
}
