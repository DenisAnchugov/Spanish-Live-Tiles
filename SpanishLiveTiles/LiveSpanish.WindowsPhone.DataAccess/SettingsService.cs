using System.Collections.Generic;
using Windows.Storage;
using LiveSpanish.WindowsPhone.DataAccess.Entities;

namespace LiveSpanish.WindowsPhone.DataAccess
{
    public class SettingsService
    {
        private readonly ApplicationDataContainer settings = ApplicationData.Current.LocalSettings;

        //public State LoadState()
        //{
        //    if (settings.Values.ContainsKey("Category") && settings.Values.ContainsKey("WeekNumber"))
        //    {
        //        return new State((String)settings.Values["Category"], (Int32)settings.Values["WeekNumber"]);
        //    }
        //    return new State();
        //}

        //public void SaveCategoryState(string category)
        //{
        //    settings.Values["Category"] = category;
        //}
        //public void SaveWeekNumberState(int weekNumber)
        //{
        //    settings.Values["WeekNumber"] = weekNumber;
        //}
        public void UpdateSelectedSets(List<VocabularySetEnum> selectedSets)
        {
            settings.Values["SetsSelected"] = selectedSets;
        }

        public List<VocabularySetEnum> RetrieveSelectedSets()
        {
            return (List<VocabularySetEnum>)settings.Values["SetsSelected"];
        }
    }
}
