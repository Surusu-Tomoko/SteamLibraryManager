using Force.Crc32;
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
    /// Логика взаимодействия для RenameGamePage.xaml
    /// </summary>
    public partial class GameRenamePage : Page
    {
        App app = App.Current as App;

        ulong GameId = 0;

        public GameRenamePage(ulong gameid)
        {
            InitializeComponent();

            GameId = gameid;

            string configdir = Path.Combine(app.steamPATH, "userdata", app.userId, "config", "grid");

            string curPoster = Path.Combine(configdir, GameId + "p.png");
            if (File.Exists(curPoster))
                CurPoster.Source = new BitmapImage(new Uri(curPoster));
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

            NewGameName.Text = app.Steamlibrary.First(x => x.Id == GameId).AppName;
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            //app.Steamlibrary.Remove();

            app.Steamlibrary.First(x => x.Id == GameId).AppName = NewGameName.Text;

            App.LibItem item = app.Steamlibrary.First(x => x.Id == GameId);

            var stringValue = $"{item.Exe + item.AppName}";
            var byteArray = Encoding.UTF8.GetBytes(stringValue);
            var thing = Crc32Algorithm.Compute(byteArray);
            var longThing = (ulong)thing;
            longThing = (longThing | 0x80000000);

            ulong newGameId = longThing;

            app.Steamlibrary.First(x => x.Id == GameId).Id = newGameId;

            string configdir = Path.Combine(app.steamPATH, "userdata", app.userId, "config", "grid");

            string curGrid = Path.Combine(configdir, GameId + ".png");
            if (File.Exists(curGrid))
                System.IO.File.Move(curGrid, Path.Combine(configdir, newGameId + ".png"));
            else
            {
                curGrid = Path.Combine(configdir, GameId + ".jpg");
                if (File.Exists(curGrid))
                    System.IO.File.Move(curGrid, Path.Combine(configdir, newGameId + ".jpg"));
            }

            string curPoster = Path.Combine(configdir, GameId + "p.png");
            if (File.Exists(curPoster))
                System.IO.File.Move(curPoster, Path.Combine(configdir, newGameId + "p.png"));
            else
            {
                curPoster = Path.Combine(configdir, GameId + "p.jpg");
                if (File.Exists(curPoster))
                    System.IO.File.Move(curPoster, Path.Combine(configdir, newGameId + "p.jpg"));
            }

            string curHero = Path.Combine(configdir, GameId + "_hero.png");
            if (File.Exists(curHero))
                System.IO.File.Move(curHero, Path.Combine(configdir, newGameId + "_hero.png"));
            else
            {
                curHero = Path.Combine(configdir, GameId + "_hero.jpg");
                if (File.Exists(curHero))
                    System.IO.File.Move(curHero, Path.Combine(configdir, newGameId + "_hero.jpg"));
            }

            string curLogo = Path.Combine(configdir, GameId + "_logo.png");
            if (File.Exists(curLogo))
                System.IO.File.Move(curLogo, Path.Combine(configdir, newGameId + "_logo.png"));
            else
            {
                curLogo = Path.Combine(configdir, GameId + "_logo.jpg");
                if (File.Exists(curLogo))
                    System.IO.File.Move(curLogo, Path.Combine(configdir, newGameId + "_logo.jpg"));
            }

            string curIcon = Path.Combine(configdir, GameId + "_icon.png");
            if (File.Exists(curIcon))
                System.IO.File.Move(curLogo, Path.Combine(curIcon, newGameId + "_icon.png"));
            else
            {
                curIcon = Path.Combine(configdir, GameId + "_icon.jpg");
                if (File.Exists(curIcon))
                    System.IO.File.Move(curLogo, Path.Combine(curIcon, newGameId + "_icon.jpg"));
            }

            this.NavigationService.GoBack();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }


    }
}
