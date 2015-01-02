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

        private static void UpdateTile(WordsContainer words)
        {
            
            var updater = TileUpdateManager.CreateTileUpdaterForApplication();
            updater.EnableNotificationQueue(true);
            updater.Clear();
            

            for (int i = 0; i < 5; i++)
            {
                XmlDocument tileSquareXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText02);
                XmlDocument tileWideXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWide310x150Text01);

                tileSquareXml.GetElementsByTagName("text")[0].InnerText = words.ShortWords[i].Expression;
                tileSquareXml.GetElementsByTagName("text")[1].InnerText = words.ShortWords[i].Translation;
                tileWideXml.GetElementsByTagName("text")[0].InnerText = words.LongWords[i].Expression;
                tileWideXml.GetElementsByTagName("text")[1].InnerText = words.LongWords[i].Translation;

                updater.Update(new TileNotification(tileSquareXml));
                updater.Update(new TileNotification(tileWideXml));

            }           
        }
    }
}
