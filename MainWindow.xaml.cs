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
    // Reference for mediaPlayer:
    // https://www.wpf-tutorial.com/audio-video/playing-audio/

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MusicLib musicLib;
        private DataTable table;
        private About about;
        private MediaPlayer mediaPlayer = new MediaPlayer();

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
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void About_Button_Click(object sender, RoutedEventArgs e)
        {
            about = new About();
            about.ShowDialog();
        }

        private void Delete_MenuItemClick(object sender, RoutedEventArgs e)
        {
            // Get the song id 
            int songId = findSelectedRowInDataGrid();

            // Remove the song from all playlist
            if (songId != -1)
            {
                musicLib.DeleteSong(songId);
            }
        }

        private int findSelectedRowInDataGrid()
        {
            DataRowView rowView = dataGrid.SelectedItem as DataRowView;
            if (rowView != null)
            {
                // Extract the song ID from the selected song
                int songId = Convert.ToInt32(rowView.Row.ItemArray[0]);
                return songId;
            }
            else
            {
                return -1;
            }

        }

        private void Play_MenuItemClick(object sender, RoutedEventArgs e)
        {

            // Get song id
            int songId = findSelectedRowInDataGrid();

            // Get the song itself
            Song theSong = musicLib.GetSong(songId);

            // Play song using media player
            mediaPlayer.Open(new Uri(theSong.Filename));
            mediaPlayer.Play();
        }
    }
}
