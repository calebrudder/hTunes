using System;
using System.Collections.Generic;
using System.Data;
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

            /*
            // Loop through all the songs and get info to put into allSongs List
            allSongs.Add(new Song
            {
                Id = 1,
                Title = "Something",
                Album = "an album",
                Artist = "Toby Mac",
                Genre = "Rock",
                AlbumImage = "http://www.harding.edu/fmccown/images/broncos.gif"
            });
            dataGrid.ItemsSource = allSongs;
            */

            // TODO: Get songs from music.xml (?)
            // TODO: Put those songs in table   

            /*
            DataTable table = new DataTable("Song");
            table.Columns.Add(new DataColumn("Id", typeof(int)));
            table.Columns.Add(new DataColumn("Title", typeof(string)));
            table.Columns.Add(new DataColumn("Artist", typeof(string)));
            table.Columns.Add(new DataColumn("AlbumImage", typeof(string)));

            DataRow row = table.NewRow();
            row["Id"] = 1;
            row["Title"] = "Good Vibrations";
            row["Artist"] = "The Beach Boys";
            row["AlbumImage"] = "http://www.harding.edu/fmccown/images/broncos.gif";
            table.Rows.Add(row);

            row = table.NewRow();
            row["Id"] = 2;
            row["Title"] = "Love Me Tender";
            row["Artist"] = "Elvis Presley";
            row["AlbumImage"] = "http://www.harding.edu/fmccown/images/cowboys.png";
            table.Rows.Add(row);
            */



            MusicLib musicLib = new MusicLib();
            DataTable table = musicLib.SongsForPlaylist("Cool Stuff!");

            // Bind the data source
            dataGrid.ItemsSource = table.DefaultView;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
