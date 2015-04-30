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
    public class ExpressionProvider
    {
        const string DatabasePath = "Vocabulary.sqlite";

        public ExpressionProvider()
        {
            PrepareDatabase();
        }

        async void PrepareDatabase()
        {
            if (await DatabaseExists()) return;
            var databaseFile = await Package.Current.InstalledLocation.GetFileAsync(DatabasePath);
            await databaseFile.CopyAsync(ApplicationData.Current.LocalFolder);
        }

        async Task<bool> DatabaseExists()
        {
            try
            {
                var databaseFile = await ApplicationData.Current.LocalFolder.GetFileAsync(DatabasePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<ExpressionContainer> GetTableAsync()
        {
            var selectedSets = await SettingsProvider.RetrieveSelectedSetsAsync();

            var conn = new SQLiteAsyncConnection(DatabasePath);
            var allExpressions = new List<ExpressionEntity>();

            foreach (var set in selectedSets)
            {
                var result = await conn.QueryAsync<ExpressionEntity>("Select * FROM " + set);
                allExpressions.AddRange(result);
            }
            return GetExpressions(allExpressions);
        }

        ExpressionContainer GetExpressions(List<ExpressionEntity> list)
        {
            var expressions = new ExpressionContainer();
            //Gets expressions of each type for each tile type. 4 in total.            
            expressions.LongExpression = (from expression in list
                                          where expression.ExpressionLength <= 21
                                          select expression).OrderBy(x => Guid.NewGuid()).First();

            expressions.ShortExpression = (from expression in list
                                           where expression.ExpressionLength <= 12
                                           select expression).OrderBy(x => Guid.NewGuid()).First();

            return expressions;
        }
    }
}