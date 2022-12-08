using Force.Crc32;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
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
using VDFParser;
using VDFParser.Models;
using static SteamLibraryManager.App;
using Path = System.IO.Path;

namespace SteamLibraryManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        App app = App.Current as App;
        public MainPage()
        {
            InitializeComponent();
            //app.mainPage = this;
        } 

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            listBox2.ItemsSource = new List<Object>();
            listBox2.ItemsSource = app.Steamlibrary;

            //listBox2.ItemsSource = app.Steamlibrary;

            //listBox2.UpdateLayout();
        }

        public void autoloadButton1()
        {
            Button_Click(null, null);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            app.userId = comboBox1.SelectedItem.ToString();
            string configdir = Path.Combine(app.steamPATH, "userdata", app.userId, "config");
            string shortcutsFile = Path.Combine(configdir, "shortcuts.vdf");

            if (File.Exists(shortcutsFile))
            {
                VDFEntry[] vDFEntries = VDFParser.VDFParser.Parse(shortcutsFile);

                foreach (var item in vDFEntries)
                {
                    //textBox1.Text += item.ToString()+"\n";

                    var stringValue = $"{item.Exe + item.AppName}";
                    var byteArray = Encoding.UTF8.GetBytes(stringValue);
                    var thing = Crc32Algorithm.Compute(byteArray);
                    var longThing = (ulong)thing;
                    longThing = (longThing | 0x80000000);

                    string image = item.Icon;

                    if (image == string.Empty)
                        image = Path.Combine(AppContext.BaseDirectory, "empty-icon.png");


                    app.Steamlibrary.Add(new App.LibItem
                    {
                        Id = longThing,
                        Image = image,

                        AppName = item.AppName,
                        Exe = item.Exe,
                        StartDir = item.StartDir,
                        Icon = item.Icon,
                        ShortcutPath = item.ShortcutPath,
                        LaunchOptions = item.LaunchOptions,
                        IsHidden = item.IsHidden,
                        AllowDesktopConfig = item.AllowDesktopConfig,
                        AllowOverlay = item.AllowOverlay,
                        OpenVR = item.OpenVR,
                        Devkit = item.Devkit,
                        DevkitGameID = item.DevkitGameID,
                        LastPlayTime = item.LastPlayTime,
                        Tags = item.Tags

                    });
                }

                listBox2.ItemsSource = app.Steamlibrary;
            }
        }
        private void LoadCurImage(ulong Id)
        {
            string configdir = Path.Combine(app.steamPATH, "userdata", app.userId, "config", "grid");

            string curGrid = Path.Combine(configdir, Id + ".png");
            if (File.Exists(curGrid))
                CurGrid.Source = app.bitmapFile(curGrid);
            else
            {
                curGrid = Path.Combine(configdir, Id + ".jpg");
                if (File.Exists(curGrid))
                {
                    CurGrid.Source = app.bitmapFile(curGrid);
                }
                else
                    CurGrid.Source = null;
            }

            string curPoster = Path.Combine(configdir, Id + "p.png");
            if (File.Exists(curPoster))
                CurPoster.Source = app.bitmapFile(curPoster);
            else
            {
                curPoster = Path.Combine(configdir, Id + "p.jpg");
                if (File.Exists(curPoster))
                {
                    CurPoster.Source = app.bitmapFile(curPoster);
                }
                else
                    CurPoster.Source = null;
            }

            string curHero = Path.Combine(configdir, Id + "_hero.png");
            if (File.Exists(curHero))
                CurHero.Source = app.bitmapFile(curHero);
            else
            {
                curHero = Path.Combine(configdir, Id + "_hero.jpg");
                if (File.Exists(curHero))
                {
                    CurHero.Source = app.bitmapFile(curHero);
                }
                else
                    CurHero.Source = null;
            }

            string curLogo = Path.Combine(configdir, Id + "_logo.png");
            if (File.Exists(curLogo))
                CurLogo.Source = app.bitmapFile(curLogo);
            else
            {
                curLogo = Path.Combine(configdir, Id + "_logo.jpg");
                if (File.Exists(curLogo))
                {
                    CurLogo.Source = app.bitmapFile(curLogo);
                }
                else
                    CurLogo.Source = null;
            }

            string curIcon = Path.Combine(configdir, Id + "_icon.png");
            if (File.Exists(curIcon))
                CurIcon.Source = app.bitmapFile(curIcon);
            else
            {
                curIcon = Path.Combine(configdir, Id + "_icon.jpg");
                if (File.Exists(curIcon))
                {
                    CurIcon.Source = app.bitmapFile(curIcon);
                }
                else
                    CurIcon.Source = null;
            }
        }
        private void listBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox2.SelectedItem != null)
                LoadCurImage((listBox2.SelectedItem as App.LibItem).Id);
        }
        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new GameAddPage());
        }
        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem as App.LibItem != null)
                if ((listBox2.SelectedItem as App.LibItem).Id != 0)
                    this.NavigationService.Navigate(new GameDeletePage((listBox2.SelectedItem as App.LibItem).Id));
        }
        private void RenameGame_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem as App.LibItem != null)
                if ((listBox2.SelectedItem as App.LibItem).Id != 0)
                    this.NavigationService.Navigate(new GameRenamePage((listBox2.SelectedItem as App.LibItem).Id));
        }
        private void PathGame_Click(object sender, RoutedEventArgs e)
        {
            if (listBox2.SelectedItem as App.LibItem != null)
                if ((listBox2.SelectedItem as App.LibItem).Id != 0)
                    this.NavigationService.Navigate(new GamePathPage((listBox2.SelectedItem as App.LibItem).Id));
        }

        private void SaveLib_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Save Library?", "Question", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                string configdir = Path.Combine(app.steamPATH, "userdata", app.userId, "config");
                string shortcutsFile = Path.Combine(configdir, "shortcuts.vdf");

                FileInfo file = new FileInfo(shortcutsFile);
                FileInfo Backupfile = new FileInfo(Path.Combine(configdir, "shortcuts.vdf.backup"));

                if (file.Exists)
                    file.CopyTo(Backupfile.FullName, true);

                VDFEntry[] vDFEntries = app.Steamlibrary.ToArray();
                byte[] filedata = VDFSerializer.Serialize(vDFEntries);
                System.IO.File.WriteAllBytes(shortcutsFile, filedata);

                MessageBox.Show("Library saved successfully!");
                //
                //do yes stuff
            }
        }





    }
}
