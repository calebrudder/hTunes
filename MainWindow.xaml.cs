using Microsoft.Win32;
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

    // Reference for LINQ and Datatable
    // https://www.codecompiled.com/query-datatable-using-linq-in-csharp/
    // https://stackoverflow.com/questions/10855/linq-query-on-a-datatable
    // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/how-to-query-for-sentences-that-contain-a-specified-set-of-words-linq

    // Resource for Media Player
    // https://www.wpf-tutorial.com/dialogs/the-openfiledialog/

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MusicLib musicLib = new MusicLib();
        private DataTable table;
        private About about;
        private MediaPlayer mediaPlayer = new MediaPlayer();
        private Point startPoint;


        public MainWindow()
        {
            InitializeComponent();
            // musicLib = new MusicLib();
            musicLib.PrintAllTables();

            DataTable table = musicLib.Songs;
 
            // Bind the data source
            dataGrid.ItemsSource = table.DefaultView;

            List<string> playlists = new List<string>();
            playlists.Add("All Music");
            playlists.AddRange(musicLib.Playlists);

            Console.Write(playlists);
            playlistList.ItemsSource = playlists;
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
            if (songId != -1 || songId != null)
            {
                Song s = musicLib.GetSong(songId);
                string name = s.Title;
                musicLib.DeleteSong(songId);

                MessageBox.Show(name + " has been removed from the library.");
                musicLib.Save();
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
            playTheSong();
        }

        private void stopTheSong()
        {
            // Get song id
            int songId = findSelectedRowInDataGrid();

            // Get the song itself
            Song theSong = musicLib.GetSong(songId);

            if (theSong != null)
            {
                // Stop song using media player
                mediaPlayer.Open(new Uri(theSong.Filename));
                mediaPlayer.Stop();
            }
        }

        private void playTheSong()
        {
            // Get song id
            int songId = findSelectedRowInDataGrid();

            // Get the song itself
            Song theSong = musicLib.GetSong(songId);


            // Play song using media player
            if (theSong != null)
            {
                mediaPlayer.Open(new Uri(theSong.Filename));
                mediaPlayer.Play();
            }
        }
        private void Search_Text_Box_GotFocus(object sender, RoutedEventArgs e)
        {
            Search_Text_Box.Text = "";
        }
        
        private void Search_Text_Box_KeyUp(object sender, KeyEventArgs e)
        {
            string text = Search_Text_Box.Text;

            DataTable allTheSongs = musicLib.Songs;

            var results = from a in allTheSongs.AsEnumerable()
                          where 
                            a.Field<string>("title").ToUpper().Contains(text.ToUpper()) ||
                            a.Field<string>("artist").ToUpper().Contains(text.ToUpper()) ||
                            a.Field<string>("genre").ToUpper().Contains(text.ToUpper()) ||
                            a.Field<string>("album").ToUpper().Contains(text.ToUpper())
                          select a;

            DataTable dt = results.CopyToDataTable<DataRow>();
            dataGrid.ItemsSource = dt.DefaultView;
        }

        private void Add_Song_Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Media Files (*.mp3;*.m4a;*.wma;*.wav)|*.mp3;*.m4a;*.wma;*.wav|MP3 (*.mp3)|*.mp3|M4A (*.m4a)|*.m4a|Windows Media Audio (*.wma)|*wma|Wave Files (*.wav)|*.wav|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                Song s = musicLib.AddSong(openFileDialog.FileName);
                musicLib.Save();
                int sID = s.Id;
            }

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
        private void addPlaylistBtn_Clicked(object sender, RoutedEventArgs e)
        {
            AddPlaylist addPlaylistWindow = new AddPlaylist();
            addPlaylistWindow.Owner = this;
            addPlaylistWindow.ShowDialog();
            if (addPlaylistWindow.DialogResult == true)
            {
                string newPlaylistName = addPlaylistWindow.newPlaylistName;
                if (musicLib.PlaylistExists(newPlaylistName))
                {
                    MessageBox.Show("There is already a playlist with that name");
                }
                else
                {
                    musicLib.AddPlaylist(newPlaylistName);
                    musicLib.Save();
                    List<string> updatedPlaylists = new List<string>();
                    updatedPlaylists.Add("All Music");
                    updatedPlaylists.AddRange(musicLib.Playlists);
                    playlistList.ItemsSource = updatedPlaylists;
                }
            }
        }

        private void playButton_Click(object sender, RoutedEventArgs e)
        {
            playTheSong();
        }

        private void stopButton_Click(object sender, RoutedEventArgs e)
        {
            stopTheSong();
        }

        private void TextBlock_Drop(object sender, DragEventArgs e)
        {
            // Initiate dragging the text from the textbox
            string songId = "";
            foreach (DataGridCellInfo data in dataGrid.SelectedCells)
            {
               DataRowView dvr = (DataRowView)data.Item;
               songId = (dvr[0].ToString());

            }

            TextBlock txtblock = (TextBlock)sender;
            string playlistName = txtblock.Text;
            int song = Int32.Parse(songId);
            musicLib.AddSongToPlaylist(song, playlistName);
            musicLib.Save();
            
        }

        private void DataGrid_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;

            // Start the drag-drop if mouse has moved far enough
            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {


                DragDrop.DoDragDrop(dataGrid, dataGrid, DragDropEffects.Move);
            }

        }

        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Store the mouse position
            startPoint = e.GetPosition(null);

        }
    }
}
