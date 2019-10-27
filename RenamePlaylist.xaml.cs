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
    /// Interaction logic for RenamePlaylist.xaml
    /// </summary>
    public partial class RenamePlaylist : Window
    {
        public string updatedPlaylistName;
        public RenamePlaylist()
        {
            InitializeComponent();
        }

        private void OkButton_Clicked(object sender, RoutedEventArgs e)
        {
            updatedPlaylistName = playlistNameBox.Text.Trim();
            if (updatedPlaylistName == "" || updatedPlaylistName == null)
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
    }
}
