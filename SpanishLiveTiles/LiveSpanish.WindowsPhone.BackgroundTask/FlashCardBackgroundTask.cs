using System.Collections.Generic;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using LiveSpanish.WindowsPhone.DataAccess;
using LiveSpanish.WindowsPhone.DataAccess.Entities;

namespace LiveSpanish.WindowsPhone.BackgroundTask
{
    public sealed class FlashCardBackgroundTask : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            // Get a deferral, to prevent the task from closing prematurely 
            // while asynchronous code is still running.
            BackgroundTaskDeferral deferral = taskInstance.GetDeferral();

            var dataProvider = new DataService();
            var words = await dataProvider.GetTableAsync();
            UpdateTile(words);

            // Inform the system that the task is finished.
            deferral.Complete();
        }

        private static void UpdateTile(IEnumerable<ExpressionEntity> words)
        {
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();

            int itemCount = 0;

            foreach (var item in words)
            {
                XmlDocument tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText01);

                tileXml.GetElementsByTagName("text")[0].InnerText = item.Expression;
                tileXml.GetElementsByTagName("text")[1].InnerText = item.Translation;
                updater.Update(new TileNotification(tileXml));

                if (itemCount++ > 5) break;
            }
        }
    }
}
