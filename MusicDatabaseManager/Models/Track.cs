using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDatabaseManager.Models
{
    public class Track : ListableDatabaseObject
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
        private object v1;
        private object v2;
        private object v3;
        private object v4;
        private object v5;

        public int AlbumID { get { return _albumID; } set { _albumID = value; OnPropertyChanged(); } }
        public Track(int tid, string trackname, int length, int artistid, int albumid)
        {
            TID = tid;
            TrackName = trackname;
            Length = length;
            ArtistID = artistid;
            AlbumID = albumid;
        }
        public Track(string tid, string trackname, string length, string artistid, string albumid)
        {
            TID = Int32.Parse(tid);
            TrackName = trackname;
            Length = Int32.Parse(length);
            ArtistID = Int32.Parse(artistid);
            AlbumID = Int32.Parse(albumid);
        }
    }
}
