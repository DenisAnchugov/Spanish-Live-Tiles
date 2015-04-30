using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.UI.StartScreen;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveSpanish.WindowsPhone.DataAccess;
using LiveSpanish.WindowsPhone.DataAccess.Entities;

namespace LiveSpanish.WindowsPhone.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        ObservableCollection<SelectionEntity> expressionsSets;

        public RelayCommand UpdateSelectedSetsCommand { get; private set; }
        public ObservableCollection<SelectionEntity> ExpressionsSets
        {
            get { return expressionsSets; }
            private set
            {
                if (value == expressionsSets) return;
                expressionsSets = value;
                RaisePropertyChanged();
            }
        }

        public MainViewModel()
        {
            UpdateSelectedSetsCommand = new RelayCommand(async () => await UpdateSets());
            ExpressionsSets = new ObservableCollection<SelectionEntity>();
            LoadSetsAsync();
        }

        async void LoadSetsAsync()
        {
            var setsSelected = await SettingsProvider.RetrieveSelectedSetsAsync();
            
            foreach (var types in Enum.GetNames(typeof (VocabularySetEnum)))
            {
                var isSelected = false;
                foreach (var vocabularySetEnum in setsSelected)
                {
                    if (types == vocabularySetEnum.ToString())
                    {
                        isSelected = true;
                    }
                }

                ExpressionsSets.Add(new SelectionEntity()
                {
                    IsSelected = isSelected,
                    SetEnum = (VocabularySetEnum) Enum.Parse(typeof (VocabularySetEnum), types)
                });
            }
        }

        private static async Task RegisterBackgroundTask()
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

                var taskBuilder = new BackgroundTaskBuilder { Name = "FlashCardBackgroundTask", TaskEntryPoint = "LiveSpanish.WindowsPhone.BackgroundTask.FlashCardBackgroundTask"};
                taskBuilder.SetTrigger(new SystemTrigger(SystemTriggerType.UserPresent, false));
                //taskBuilder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
                taskBuilder.Register();

            }
        }

        async Task UpdateSets()
        {
            var selectedSets = (from selection in ExpressionsSets where selection.IsSelected select selection.SetEnum).ToList();
            await SettingsProvider.UpdateSelectedSetsAsync(selectedSets);
            await RegisterBackgroundTask();          
        }
    }
}