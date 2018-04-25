using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<TabItem> _tabs = new ObservableCollection<TabItem>();
        public ObservableCollection<TabItem> Tabs
        {
            get {
                return _tabs;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Tabs.Add(new LibraryView());
            Tabs.Add(new AddItemView());
            Tabs.Add(new PrimaryView());
            Tabs.Add(new DatabaseView());
            PrimaryTabControl.ItemsSource = Tabs;


        }
    }
}
