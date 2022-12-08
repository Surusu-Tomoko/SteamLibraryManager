using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Path = System.IO.Path;


namespace SteamLibraryManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для DeletePage.xaml
    /// </summary>
    public partial class GameDeletePage : Page
    {

        App app = App.Current as App;

        ulong GameId = 0;

        public GameDeletePage(ulong gameid)
        {
            InitializeComponent();

            GameId = gameid;

            DeleteGameLabel.Content = "Вы действительно хотите удалить \"" + app.Steamlibrary.First(x => x.Id == GameId).AppName + "\"?";

            string configdir = Path.Combine(app.steamPATH, "userdata", app.userId, "config", "grid");

            string curPoster = Path.Combine(configdir, GameId + "p.png");
            if (File.Exists(curPoster))
                CurPoster.Source = app.bitmapFile(curPoster);
            else
            {
                curPoster = Path.Combine(configdir, GameId + "p.jpg");
                if (File.Exists(curPoster))
                {
                    CurPoster.Source = app.bitmapFile(curPoster);
                }
                else
                    CurPoster.Source = null;
            }

        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            //app.Steamlibrary[]

            //app.Steamlibrary.First(x => x.Id == GameId).

            app.Steamlibrary.Remove(app.Steamlibrary.First(x => x.Id == GameId));

            this.NavigationService.GoBack();
        }
        
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

    }
}
