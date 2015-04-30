using System;
using Windows.UI.Popups;
using Windows.UI.StartScreen;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using LiveSpanish.WindowsPhone.DataAccess.Entities;

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

         async void SaveChangesAsyncButton_Click(object sender, RoutedEventArgs e)
        {
            if (SecondaryTile.Exists(TileDependancyProvider.GetSecondaryTileId))
            {
                var message = new MessageDialog("Vocabulary was added in rotation.", "Success");
                await message.ShowAsync();      
            }
            else
            {
                var message =
                    new MessageDialog(
                        "Vocabulary was added in rotation now pin tile to the start screen. Tiles will get updated after each device use.", "Success");
                message.Commands.Add(new UICommand("pin tile", PinTileAsync));
                message.DefaultCommandIndex = 0;
                await message.ShowAsync();
            }        
        }


        async void PinTileAsync(IUICommand command)
        {
            var logo = new Uri("ms-appx:///Assets/SmallLogo.scale-240.png");
            var tileActivationArguments = "Live Tile Spanish" + " was pinned at = " + DateTime.Now.ToLocalTime().ToString();
            var displayName = "Live Tile Spanish";
            var newTileDesiredSize = TileSize.Square150x150;

            SecondaryTile secondaryTile = new SecondaryTile(TileDependancyProvider.GetSecondaryTileId,
                                                            displayName,
                                                            tileActivationArguments,
                                                            logo,
                                                            newTileDesiredSize);

            secondaryTile.VisualElements.Wide310x150Logo = new Uri("ms-appx:///Assets/WideLogo.scale-240.png");
            
            await secondaryTile.RequestCreateAsync();
        }
    }
}
