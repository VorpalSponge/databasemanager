using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDatabaseManager.Models
{
    class Album : ListableDatabaseObject
    {
        private int _albumID;
        public int AlbumID { get { return _albumID; } set { _albumID = value; OnPropertyChanged(); } }
        private int _artistID;
        public int ArtistID { get { return _artistID; } set { _artistID = value; OnPropertyChanged(); } }
        private string _albumName;
        public string AlbumName { get { return _albumName; } set { _albumName = value; OnPropertyChanged(); } }
        private string _date;
        public string Date { get { return _date; } set { _date = value; OnPropertyChanged(); } }
    }
}
