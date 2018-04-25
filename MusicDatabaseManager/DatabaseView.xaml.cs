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
    /// Interaction logic for DatabaseView.xaml
    /// </summary>
    public partial class DatabaseView : TabItem
    {
        private ObservableCollection<ComboBoxItem> _tables = new ObservableCollection<ComboBoxItem>();
        public ObservableCollection<ComboBoxItem> Tables
        { get { return _tables; } set { _tables = value; } }

        private DatabaseManager dm;

        public DatabaseView()
        {
            InitializeComponent();
            List<string> tableNames = new List<string>(){ "Tracks", "Albums", "Artists", "Playlists", "IsOnPlaylist"};
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

        private void OnSelectionChanged(object sender, RoutedEventArgs e)
        {
            DataTable results = dm.retrieveTable(TableSelectComboBox.SelectedValue.ToString());
            SQLResultDataGrid.ItemsSource = null;
            SQLResultDataGrid.ItemsSource = results.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string q = QueryTextbox.Text;
            Console.WriteLine("Received query: '" + q + "'");
            DataTable results = dm.runSelectQuery(q);
            SQLResultDataGrid.ItemsSource = null;
            SQLResultDataGrid.ItemsSource = results.DefaultView;
        }

        //TODO: Implement general database view to see all results
    }
}
