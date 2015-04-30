using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using LiveSpanish.WindowsPhone.DataAccess.Entities;
using Newtonsoft.Json;

namespace LiveSpanish.WindowsPhone.DataAccess
{
    public static class SettingsProvider
    {
        static readonly ApplicationDataContainer Settings = ApplicationData.Current.LocalSettings;

        public static async Task UpdateSelectedSetsAsync(List<VocabularySetEnum> selectedSets)
        {
            var selected = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(selectedSets));
            Settings.Values["SetsSelected"] = selected;
        }

        public static async Task<List<VocabularySetEnum>> RetrieveSelectedSetsAsync()
        {
            if (Settings.Values.ContainsKey("SetsSelected"))
            {
                return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<VocabularySetEnum>>(Settings.Values["SetsSelected"].ToString()));
            }
            return new List<VocabularySetEnum>();
        }
    }
}
