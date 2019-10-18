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

namespace hTunes
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            List<Song> allSongs = new List<Song>();

            // Loop through all the songs and get info to put into allSongs List
            allSongs.Add(new Song
            {
                Id = 1,
                Title = "Something",
                Album =
                "an album",
                Artist = "Toby Mac",
                Genre = "Rock"
            });
            dataGrid.ItemsSource = allSongs;


        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
