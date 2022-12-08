//using Gameloop.Vdf;
//using Gameloop.Vdf.JsonConverter;
//using Newtonsoft.Json.Linq;
using Force.Crc32;
using Microsoft.Win32;
using SteamLibraryManager.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
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
using Path = System.IO.Path;

namespace SteamLibraryManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        App app = App.Current as App;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Valve\Steam"); //сокрашяем программу + вставляем путь к стиму из regedit
            app.steamPATH = key.GetValue("SteamPath").ToString(); // записываем в переменую steamPATH путь стима

            List<string> userDirList = Directory.GetDirectories(Path.Combine(app.steamPATH, "userdata")).ToList<string>();

            List<string> userIdList = new List<string>(); ;

            foreach (string dir in userDirList)
                userIdList.Add(new DirectoryInfo(dir).Name);

            userIdList.Remove("0");

            MainPage m = new MainPage();

            Frame frame = new Frame();
            frame.Navigate(m);

            MainFrame.Navigate(m);

            //this.NavigationService.Navigate


            m.comboBox1.ItemsSource = userIdList;

            if (userIdList.Count != 0)
                m.comboBox1.SelectedItem = userIdList[0];

            if (userIdList.Count == 1)
                m.autoloadButton1();
        }
        /*
             Language: russian
             SteamExe: d:/games/steam/steam.exe
             SteamPath: d:/games/steam
             SuppressAutoRun: 0
             Restart: 0
             RunningAppID: 0
             BigPictureInForeground: 0
             SourceModInstallPath: D:\Games\Steam\steamapps\sourcemods
             Rate: 30000
             AlreadyRetriedOfflineMode: 0
             AutoLoginUser: nweionx
             DWriteEnable: 1
             StartupMode: 0
             PseudoUUID: e2aaa7c8a09a7841
             LastGameNameUsed: Surusu Tomouko
             */
        /*foreach (var item in key.GetValueNames())
            {
                textBox1.Text += item + ": " + key.GetValue(item).ToString() + "\n";
            }*/

    }
}
