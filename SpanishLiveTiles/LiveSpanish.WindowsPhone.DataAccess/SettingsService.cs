using Windows.Storage;

namespace LiveSpanish.WindowsPhone.DataAccess
{
    class SettingsService
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
    }
}
