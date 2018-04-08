using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDatabaseManager.Models
{
    class Playlist : ListableDatabaseObject
    {
        private int _pid;
        public int PID { get { return _pid; } set { _pid = value; OnPropertyChanged(); } }
        private string _playlistName;
        public string PlaylistName { get { return _playlistName; } set { _playlistName = value; OnPropertyChanged(); } }
    }
}
