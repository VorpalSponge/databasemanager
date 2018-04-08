using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicDatabaseManager.Models
{
    class Artist : ListableDatabaseObject
    {
        private int _artistID;
        public int ArtistID { get { return _artistID; } set { _artistID = value; OnPropertyChanged(); } }
        private string _firstName;
        public string FirstName { get { return _firstName; } set { _firstName = value; OnPropertyChanged(); } }
        private string _lastName;
        public string LastName { get { return _lastName; } set { _firstName = value;  OnPropertyChanged(); } }
        private int _year;
        public int Year { get { return _year; } set { _year = value; OnPropertyChanged(); } }
    }
}
