using MusicDatabaseManager.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace MusicDatabaseManager
{
    /// <summary>
    /// Interaction logic for LibraryView.xaml
    /// </summary>
    public partial class LibraryView : TabItem
    {
        private DatabaseManager dm;

        private ObservableCollection<string> _subListSource;
        private ObservableCollection<string> SubListSource
        {
            get { if (_subListSource == null) _subListSource = new ObservableCollection<string>(); return _subListSource; }
            set { _subListSource = value; OnPropertyChanged(); }
        }


        public LibraryView()
        {
            InitializeComponent();
            dm = DatabaseManager.Instance;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //C# 6 null-safe operator. No need to check for event listeners
            //If there are no listeners, this will be a noop
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void LibraryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrackNameTextBlock.Text = "None";
            LengthTextBlock.Text = "None";
            ArtistTextBlock.Text = "None";
            AlbumTextBlock.Text = "None";
            SubListSource.Clear();
            LibrarySelectedView.SelectedItem = null;
            LibrarySelectedView.ItemsSource = null;
            TracksDataGrid.ItemsSource = null;
            
            string current_selection = LibraryListView.SelectedValue.ToString();
            if (current_selection.Equals("Music"))
            {
                Console.WriteLine("Music detected.");
                //Display artists and its tracks.
                DataTable artists = dm.runSelectQuery("select * from Artists");
                foreach (DataRow row in artists.Rows)
                {
                    SubListSource.Add(row["FirstName"] + " " +row["LastName"]);
                }
            }
            else
            {
                DataTable playlists = dm.runSelectQuery("select * from Playlists");
                foreach (DataRow row in playlists.Rows)
                {
                    SubListSource.Add(row["PlaylistName"] as string);
                }
            }
            LibrarySelectedView.ItemsSource = SubListSource;
        }

        private void LibrarySelectedView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TrackNameTextBlock.Text = "None";
            LengthTextBlock.Text = "None";
            ArtistTextBlock.Text = "None";
            AlbumTextBlock.Text = "None";
            TracksDataGrid.ItemsSource = null;
            string current_mode = LibraryListView.SelectedValue.ToString();
            string current_selection = "";
            try
            {
                current_selection = LibrarySelectedView.SelectedValue.ToString();
            }
            catch
            {
                Console.WriteLine("Error from SelectedView changed selection.");
                return;
            }

            if (current_mode.Equals("Music"))
            {
                //get all tracks associated with artist
                string[] names = current_selection.Split(' ');
                string firstName = names[0];
                string lastName = names[1];
                DataTable results = dm.runSelectQuery("select Tracks.TrackName, Tracks.Length, Albums.AlbumName, Artists.FirstName, Artists.LastName from Tracks, Albums, Artists where Tracks.ArtistID=Artists.ArtistID AND Tracks.AlbumID = Albums.AlbumID AND Artists.FirstName=\"" + firstName + "\" and Artists.LastName=\"" + lastName + "\"");
                TracksDataGrid.ItemsSource = results.DefaultView;
            } else
            {
                Console.WriteLine("Attempting to population Playlists");
                //get all tracks on the playlist
                DataTable results = dm.runSelectQuery("select Tracks.TrackName, Tracks.Length, Albums.AlbumName, Artists.FirstName, Artists.LastName from Tracks, Albums, Artists, Playlists, IsOnPlaylist where Playlists.PlaylistName=\""+current_selection+"\" AND Playlists.PID=IsOnPlaylist.PID AND Tracks.TID=IsOnPlaylist.TID AND Tracks.AlbumID=Albums.AlbumID AND Tracks.ArtistID=Artists.ArtistID");
                TracksDataGrid.ItemsSource = results.DefaultView;
            }

        }

        private void TracksDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                DataRow selectedRow = (TracksDataGrid.SelectedItem as DataRowView).Row;
                TrackNameTextBlock.Text = selectedRow["TrackName"] as string;
                LengthTextBlock.Text = (selectedRow["Length"] as Int32?).ToString();
                ArtistTextBlock.Text = (string)selectedRow["FirstName"] + " " + (string)selectedRow["LastName"];
                AlbumTextBlock.Text = (string)selectedRow["AlbumName"];
            } catch
            {
                TrackNameTextBlock.Text = "None";
                LengthTextBlock.Text = "None";
                ArtistTextBlock.Text = "None";
                AlbumTextBlock.Text = "None";
            }
            
        }
    }
}
