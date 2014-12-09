using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using LiveSpanish.DataAccess.Entities;

namespace LiveSpanish.DataAccess
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
