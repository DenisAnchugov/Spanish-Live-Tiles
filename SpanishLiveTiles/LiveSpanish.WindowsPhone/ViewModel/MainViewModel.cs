using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LiveSpanish.WindowsPhone.DataAccess.Entities;

namespace LiveSpanish.WindowsPhone.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {

        public RelayCommand UpdateSetsCommand { get; set; }
        private ObservableCollection<SetSelectionEntity> setSelections;

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
            SetSelections = new ObservableCollection<SetSelectionEntity>();
            foreach (string value in Enum.GetNames(typeof (VocabularySetEnum)))
            {
                SetSelections.Add(new SetSelectionEntity()
                {
                    IsSelected = false,
                    SetEnum = (VocabularySetEnum) Enum.Parse(typeof (VocabularySetEnum), value)
                });
            }
        }
    }
}