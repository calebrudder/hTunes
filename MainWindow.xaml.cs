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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MusicLib musicLib;
        private About about;
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

        private void About_Button_Click(object sender, RoutedEventArgs e)
        {
            about = new About();
            about.ShowDialog();
        }

        private void Search_Text_Box_GotFocus(object sender, RoutedEventArgs e)
        {
            Search_Text_Box.Text = "";
        }

        private void Search_Text_Box_KeyUp(object sender, KeyEventArgs e)
        {
            string text = Search_Text_Box.Text;

            //TODO: Search xml for songs containing text
        }

        private void Add_Song_Button_Click(object sender, RoutedEventArgs e)
        {
            //https://www.wpf-tutorial.com/dialogs/the-openfiledialog/

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
        private void MenuItemRename_Click(object sender, RoutedEventArgs e)
        {

        }
        private void MenuItemDelete_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
