using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MusicDatabaseManager.Models;
using System.Collections.ObjectModel;

namespace MusicDatabaseManager
{
    /// <summary>
    /// Interaction logic for PrimaryView.xaml
    /// </summary>
    public partial class PrimaryView : Page, INotifyPropertyChanged
    {
        private DatabaseManager dm;
        private ObservableCollection<Track> _tracks = new ObservableCollection<Track>();
        public ObservableCollection<Track> Tracks
        {
            get { return _tracks; }
            set { _tracks = value; OnPropertyChanged(); }
        }
        public PrimaryView()
        {
            InitializeComponent();
            dm = DatabaseManager.Instance;
            PrimaryListView.DataContext = Tracks;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ObservableCollection<Track> results = dm.runTrackSelectQuery();
            //Console.WriteLine("Received tracks # " + Tracks.Count);
            //Tracks = results;
            //UpdateLayout();

            ObservableCollection<Track> results = dm.runTrackSelectQuery();
            Console.WriteLine("Received tracks # " + Tracks.Count);
            foreach( Track t in results)
            {
                Tracks.Add(t);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            //C# 6 null-safe operator. No need to check for event listeners
            //If there are no listeners, this will be a noop
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    }
