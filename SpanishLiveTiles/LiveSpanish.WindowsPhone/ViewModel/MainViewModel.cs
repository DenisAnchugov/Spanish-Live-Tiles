using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveSpanish.WindowsPhone.DataAccess;
using LiveSpanish.WindowsPhone.DataAccess.Entities;

namespace LiveSpanish.WindowsPhone.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<SetSelectionEntity> setSelections;

        public RelayCommand UpdateSetsCommand { get; set; }
        public ObservableCollection<SetSelectionEntity> SetSelections
        {
            get { return setSelections; }
            set
            {
                if (value == setSelections) return;
                setSelections = value;
                RaisePropertyChanged("SetSelections");
            }
        }

        public MainViewModel()
        {
            UpdateSetsCommand = new RelayCommand(async () => await UpdateSets());
            SetSelections = new ObservableCollection<SetSelectionEntity>();
            LoadSets();          
        }

        private async void LoadSets()
        {
            var data = new SettingsService();
            var sets = await data.RetrieveSelectedSets();
            
            foreach (var value in Enum.GetNames(typeof (VocabularySetEnum)))
            {
                var isSelected = false;
                foreach (var vocabularySetEnum in sets)
                {
                    if (value == vocabularySetEnum.ToString())
                    {
                        isSelected = true;
                    }
                }

                SetSelections.Add(new SetSelectionEntity()
                {
                    IsSelected = isSelected,
                    SetEnum = (VocabularySetEnum) Enum.Parse(typeof (VocabularySetEnum), value)
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
                taskBuilder.AddCondition(new SystemCondition(SystemConditionType.UserPresent));
                taskBuilder.Register();

            }
        }

        public async Task UpdateSets()
        {
            var selectedSets = (from selection in SetSelections where selection.IsSelected select selection.SetEnum).ToList();
            var data = new SettingsService();
            await data.UpdateSelectedSets(selectedSets);
            await RegisterBackgroundTask();
        }
        }
}