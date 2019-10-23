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
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        // https://www.wpf-tutorial.com/audio-video/playing-audio/
        private MediaPlayer mp = new MediaPlayer();
        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dataGrid.SelectedItem as DataRowView;
            if (rowView != null)
            {
                // Extract the song ID from the selected song
                int songId = Convert.ToInt32(rowView.Row.ItemArray[0]);
                // Console.WriteLine("Selected song " + songId);

                Song theSong = musicLib.GetSong(songId);
                // PlaySound(theSong.Filename);
                Uri uri = new Uri(theSong.Filename);
                mp.Open(uri);
                mp.Play();
            }

        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowView = dataGrid.SelectedItem as DataRowView;
            if (rowView != null)
            {
                // Extract the song ID from the selected song
                int songId = Convert.ToInt32(rowView.Row.ItemArray[0]);
                // Console.WriteLine("Selected song " + songId);

                Song theSong = musicLib.GetSong(songId);
                
                Uri uri = new Uri(theSong.Filename);
                mp.Open(uri);
                mp.Stop();
            }
        }
    }
}
