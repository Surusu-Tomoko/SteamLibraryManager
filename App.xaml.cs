using SteamLibraryManager.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using VDFParser.Models;

namespace SteamLibraryManager
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {

        public class LibItem : VDFEntry
        {
            public ulong Id { get; set; }
            //public string AppName { get; set; } // 
            //public string Exe { get; set; } // 
            //public string[] Tags { get; set; } // 
            public string Image { get; set; } // путь к изображению
        }

        //public LibItem[] Steamlibrary = null;

        public ObservableCollection<LibItem> Steamlibrary = new ObservableCollection<LibItem>();

        public string steamPATH = ""; //создаём переменую steamPATH

        public string userId = "0";

        //public MainPage mainPage = null;

        public BitmapImage bitmapFile(string UriString)
        {
            using (var stream = File.OpenRead(UriString))
            {
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = stream;
                bmp.EndInit();
                return bmp;
            }
        }

    }


}
