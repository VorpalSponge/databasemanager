using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MusicDatabaseManager
{
    /// <summary>
    /// Interaction logic for AddItemView.xaml
    /// </summary>
    public partial class AddItemView : TabItem
    {

        private DatabaseManager dm;
        private ObservableCollection<ComboBoxItem> _tables = new ObservableCollection<ComboBoxItem>();
        public ObservableCollection<ComboBoxItem> Tables
        { get { return _tables; } set { _tables = value; } }

        private ObservableCollection<string> _artists = new ObservableCollection<string>();
        public ObservableCollection<string> Artists
        { get { return _artists; } set { _artists = value; } }

        private ObservableCollection<string> _albums = new ObservableCollection<string>();
        public ObservableCollection<string> Albums
        { get { return _albums; } set { _albums = value; } }

        public AddItemView()
        {
            InitializeComponent();
            List<string> tableNames = new List<string>() { "Tracks", "Albums", "Artists", "Playlists", "IsOnPlaylist" };
            ComboBoxItem cbi;
            foreach (string name in tableNames)
            {
                cbi = new ComboBoxItem();
                cbi.Content = name;
                Tables.Add(cbi);
            }
            dm = DatabaseManager.Instance;
            TableSelectComboBox.ItemsSource = Tables;
        }


        private void OnTableSelectionChanged(object sender, RoutedEventArgs e)
        {
            string selected = TableSelectComboBox.SelectedValue.ToString();
            if (selected.Equals("Tracks"))
            {
                LastNameTextBox.IsEnabled = false;
                YearTextBox.IsEnabled = false;
                GenreNameTextBox.IsEnabled = false;

                DataTable artists = dm.runSelectQuery("select Artists.FirstName from Artists");
                Artists.Clear();
                foreach (DataRow row in artists.Rows)
                {
                    Artists.Add(row["FirstName"] as string);
                }
                ArtistSelectComboBox.ItemsSource = Artists;

                DataTable albums = dm.runSelectQuery("select Albums.AlbumName from Albums");
                Albums.Clear();
                foreach (DataRow row in albums.Rows)
                {
                    Albums.Add(row["AlbumName"] as string);
                }
                AlbumSelectComboBox.ItemsSource = Albums;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            DataTable result = dm.runSelectQuery("select Artists.ArtistID from Artists where Artists.FirstName=\"" + ArtistSelectComboBox.SelectedValue.ToString() + "\"");
            string artistID = (result.Rows[0]["ArtistID"] as Int32?).ToString();
            result = dm.runSelectQuery("select Albums.AlbumID from Albums where Albums.AlbumName=\"" + AlbumSelectComboBox.SelectedValue.ToString() + "\"");
            string albumID = (result.Rows[0]["AlbumID"] as Int32?).ToString();
            string query = "insert into "+ TableSelectComboBox.SelectedValue.ToString() + "(TID, TrackName, Length, AlbumID, ArtistID) values ("+rnd.Next(1,1000)+", \""+NameTextBox.Text+"\", "+LengthTextBox.Text+", "+albumID+", "+artistID+")";
            dm.runQueryWithNoResult(query);
        }
    }
}
