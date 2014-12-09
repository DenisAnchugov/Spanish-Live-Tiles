using System;
using System.Collections.Generic;
using Windows.ApplicationModel.Background;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641
using LiveSpanish.WindowsPhone.DataAccess.Entities;

namespace LiveSpanish.WindowsPhone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.RegisterBackgroundTask();           
        }

        private async void RegisterBackgroundTask()
        {
            var backgroundAccessStatus = await BackgroundExecutionManager.RequestAccessAsync();
            if (backgroundAccessStatus == BackgroundAccessStatus.AllowedMayUseActiveRealTimeConnectivity ||
                backgroundAccessStatus == BackgroundAccessStatus.AllowedWithAlwaysOnRealTimeConnectivity)
            {
                foreach (var task in BackgroundTaskRegistration.AllTasks)
                {
                    if (task.Value.Name == "FlashCardBackgroundTask")
                    {
                        task.Value.Unregister(true);
                    }
                }

                var taskBuilder = new BackgroundTaskBuilder { Name = "FlashCardBackgroundTask", TaskEntryPoint = "LiveSpanish.WindowsPhone.BackgroundTask.FlashCardBackgroundTask" };
                taskBuilder.SetTrigger(new TimeTrigger(15, false));
                BackgroundTaskRegistration registration = taskBuilder.Register();
            }
        }

        private void AppBarButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            List<VocabularySetEnum> selectedSets = new List<VocabularySetEnum>();
            foreach (var uiElement in LayoutGrid.Children )
            {
                var checkBox = (CheckBox) uiElement;
                var isChecked = checkBox.IsChecked;
                if (isChecked != null && (bool) isChecked)
                {
                    //Nasty one, needs refactoring.
                    switch (checkBox.Name)
                    {
                        case "1":
                            selectedSets.Add(VocabularySetEnum.Colours);
                            break;
                        case "2":
                            selectedSets.Add(VocabularySetEnum.Numbers);
                            break;
                    }
                }
            }
        }
    }
}
