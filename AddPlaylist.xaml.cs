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
using System.Windows.Shapes;

namespace hTunes
{
    /// <summary>
    /// Interaction logic for AddPlaylist.xaml
    /// </summary>
    public partial class AddPlaylist : Window
    {
        public string newPlaylistName;

        public AddPlaylist()
        {
            InitializeComponent();
        }

        private void OkButton_Clicked(object sender, RoutedEventArgs e)
        {
            newPlaylistName = playlistNameBox.Text.Trim();
            if (newPlaylistName == ""|| newPlaylistName == null)
            {
                MessageBox.Show("Please enter a playlist name.");
                playlistNameBox.Text = "";
            }
            else
            {
                DialogResult = true;
                Close();
            }
        }

        //private void CancelButton_Clicked(object sender, RoutedEventArgs e)
        //{
        //    Close();
        //}
    }
}
