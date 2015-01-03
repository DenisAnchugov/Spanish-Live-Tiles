using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage;
using LiveSpanish.WindowsPhone.DataAccess.Entities;
using Newtonsoft.Json;

namespace LiveSpanish.WindowsPhone.DataAccess
{
    public class SettingsService
    {
        private readonly ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;

        public async Task UpdateSelectedSets(List<VocabularySetEnum> selectedSets)
        {
            var selected = await Task.Factory.StartNew(() => JsonConvert.SerializeObject(selectedSets));
            settings.Values["SetsSelected"] = selected;
        }

        public async Task<List<VocabularySetEnum>> RetrieveSelectedSets()
        {
            if (settings.Values.ContainsKey("SetsSelected"))
            {
                return await Task.Factory.StartNew(() => JsonConvert.DeserializeObject<List<VocabularySetEnum>>(settings.Values["SetsSelected"].ToString()));
            }
            return new List<VocabularySetEnum>();
        }
    }
}
