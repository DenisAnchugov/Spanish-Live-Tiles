using System.Collections.Generic;
using Windows.ApplicationModel.Background;
using Windows.UI.Notifications;
using LiveSpanish.WindowsPhone.DataAccess;
using LiveSpanish.WindowsPhone.DataAccess.Entities;

namespace LiveSpanish.WindowsPhone.BackgroundTask
{
    public sealed class FlashCardBackgroundTask : IBackgroundTask
    {
        readonly List<TileUpdater> tileUpdaters;

        public FlashCardBackgroundTask()
        {
            tileUpdaters = new List<TileUpdater>
            {
                TileUpdateManager.CreateTileUpdaterForApplication(),
                TileUpdateManager.CreateTileUpdaterForSecondaryTile(TileDependancyProvider.GetSecondaryTileId)
            };

            foreach (var tileUpdater in tileUpdaters)
            {
                tileUpdater.EnableNotificationQueue(true);
            }
        }

        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var dataProvider = new ExpressionProvider();
            var words = await dataProvider.GetTableAsync();
            UpdateTiles(words);

            deferral.Complete();
        }

        void UpdateTiles(ExpressionContainer expression)
        {
            foreach (var tileUpdater in tileUpdaters)
            {
                tileUpdater.Clear();
                tileUpdater.Update(CreateTile(expression.ShortExpression, TileTemplateType.TileSquareText02));
                tileUpdater.Update(CreateTile(expression.LongExpression, TileTemplateType.TileWide310x150Text01));
            }
        }

        TileNotification CreateTile(ExpressionEntity expression, TileTemplateType type)
        {
            var tileXml = TileUpdateManager.GetTemplateContent(type);

            tileXml.GetElementsByTagName("text")[0].InnerText = expression.Expression;
            tileXml.GetElementsByTagName("text")[1].InnerText = expression.Translation;
            return new TileNotification(tileXml);
        }
    }
}
