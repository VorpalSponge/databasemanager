using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDatabaseManager.Models
{
    class Track : ListableDatabaseObject
    {
        private int _tid;
        public int TID { get { return _tid; } set { _tid = value; OnPropertyChanged(); } }
        private string _trackName;
        public string TrackName { get { return _trackName; } set { _trackName = value; OnPropertyChanged(); } }
        private int _length;
        public int Length { get { return _length; } set { _length = value; OnPropertyChanged(); } }
        private int _artistID;
        public int ArtistID { get { return _artistID; } set { _artistID = value; OnPropertyChanged(); } }
        private int _albumID;
        public int AlbumID { get { return _albumID; } set { _albumID = value; OnPropertyChanged(); } }
    }
}
