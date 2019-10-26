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
        private MusicLib musicLib;
        public MainWindow()
        {
            InitializeComponent();
            musicLib = new MusicLib();
            musicLib.PrintAllTables();


            // TODO: Get songs from music.xml (?)
            // TODO: Put those songs in table   

            //DataTable table = musicLib.SongsForPlaylist("Cool stuff!");
            DataTable table = musicLib.Songs;

            // Found string[] for songs
            // string allSongs[] = musicLib.SongIds;  

            // Bind the data source
            dataGrid.ItemsSource = table.DefaultView;

            List<string> playlists = new List<string>();
            playlists.Add("All Music");
            playlists.AddRange(musicLib.Playlists);

            playlistList.ItemsSource = playlists;
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void playlistList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataTable table;
            string playlistName = (sender as ListBox).SelectedItem.ToString();
            if (playlistName == "All Music")
            {
                table = musicLib.Songs;
                dataGrid.ItemsSource = table.DefaultView;
            }
            else
            {
                table = musicLib.SongsForPlaylist(playlistName);
                dataGrid.ItemsSource = table.DefaultView;
            }
        }
    }
}
