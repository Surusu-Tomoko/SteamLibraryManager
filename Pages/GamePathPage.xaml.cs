using System;
using System.Collections.Generic;
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

namespace SteamLibraryManager.Pages
{
    /// <summary>
    /// Логика взаимодействия для GamePathPage.xaml
    /// </summary>
    public partial class GamePathPage : Page
    {
        App app = App.Current as App;
        ulong GameId = 0;

        public GamePathPage(ulong gameid)
        {
            InitializeComponent();

            GameId = gameid;
        }
    }
}
