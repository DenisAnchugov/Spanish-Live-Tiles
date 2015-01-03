using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace LiveSpanish.WindowsPhone
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Required;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
                       
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
           var message = new MessageDialog("Now pin the tile to start screen. Use big square, or wide tile size. Words will appear after turning the screen on.", "Success");
           message.ShowAsync();
        }
    }
}
