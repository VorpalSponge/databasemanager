using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/**
 * Ground work obtained from tutorial at https://blog.tigrangasparian.com/2012/02/09/getting-started-with-sqlite-in-c-part-one/
 * 
 * Leverages NuGet package developed by SQLite Team at https://system.data.sqlite.org/index.html/doc/trunk/www/index.wiki
 **/
namespace MusicDatabaseManager
{
    /*
     * Due to multithread nature of GUI applications, may have to integrate locks or other simultaneously access control
     */
    public class DatabaseManager
    {

        //Singleton pattern. We only want a single database manager to interact with
        private static DatabaseManager _instance = null;
        public static DatabaseManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new DatabaseManager();
                }
                return _instance;
            }
        }

        /**
         * Database schema needs to be altered slightly due to relationships between elements.
         *    Artists have many albums.
         *    Albums belong to one artist (restriction for simplifying our project).
         *    Albums have many tracks.
         *    Tracks belong to one artist.
         *    Tracks belong to one album.
         *    Tracks belong to many playlists.
         *    Playlists have many tracks.
         **/
        public static void Test()
        {
            //Tests for DatabaseManager

            //Initialize database and connection
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();


            //Establish tables
            string table_albums = "create table Albums (AlbumID int NOT NULL, ArtistID int, AlbumName varchar(20), ReleaseDate DATE, Genre varchar(20), PRIMARY KEY (AlbumID))";
            string table_artists = "create table Artists (ArtistID int NOT NULL, FirstName varchar(20), LastName varchar(20), StartYear int, PRIMARY KEY (ArtistID))";
            string table_tracks = "create table Tracks (TID int NOT NULL, TrackName varchar(20), Length int, AlbumID int, ArtistID int, PRIMARY KEY (TID))";
            string table_playlists = "create table Playlists (PID int NOT NULL, Name varchar(20), PRIMARY KEY (PID))";
            string table_isonplaylist = "create table IsOnPlaylist (TID int NOT NULL, PID int NOT NULL, PRIMARY KEY (TID,PID))";
            List<string> tableQueries = new List<string>()
            {
                table_albums, table_artists, table_tracks, table_playlists, table_isonplaylist
            };


            //Populate the database with test data
            string album_init = "insert into Albums (AlbumID, ArtistID, AlbumName, ReleaseDate, Genre) values (0, 0, \"Gonna Graduate Soon!\", 2018-04-08, \"Pop\")";
            string artist_init = "insert into Artists (ArtistID, FirstName, LastName, StartYear) values (0, \"Alec\", \"Soto\", 2018)";
            string track_init = "insert into Tracks (TID, TrackName, Length, AlbumID, ArtistID) values (0, \"Working on a Project!\", 60, 0, 0)";
            string playlist_init = "insert into Playlists (PID, Name) values (0, \"Project working music\")";
            string isonplaylist_init = "insert into IsOnPlaylist (TID, PID) values (0,0)";
            List<string> initializationQueries = new List<string>()
            {
                album_init,artist_init,track_init,playlist_init,isonplaylist_init
            };


            //Execute population queries on the database
            SQLiteCommand command;
            foreach (string query in tableQueries)
            {
                command = new SQLiteCommand(query, m_dbConnection);
                command.ExecuteNonQuery();
            }

            foreach (string query in initializationQueries)
            {
                command = new SQLiteCommand(query, m_dbConnection);
                command.ExecuteNonQuery();
            }


            //Attempt to read from populated test data
            SQLiteDataReader reader;
            string simple_select_all = "select * from Tracks";
            command = new SQLiteCommand(simple_select_all, m_dbConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine(reader["TrackName"]);   
            }


            string inner_join_select_all = @"select * from Tracks 
                inner join Artists on Tracks.ArtistID=Artists.ArtistID 
                inner join Albums on Tracks.AlbumID=Albums.AlbumID 
                inner join IsOnPlayList on Tracks.TID=IsOnPlayList.TID 
                inner join Playlists on IsOnPlayList.PID=Playlists.PID";
            command = new SQLiteCommand(inner_join_select_all, m_dbConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("TrackID:" + reader["TID"]);
                Console.WriteLine("TrackName:" + reader["TrackName"]);
                Console.WriteLine("ArtistName:" + reader["FirstName"]);
                Console.WriteLine("AlbumName:" + reader["AlbumName"]);

            }

            string where_select_all = @"select * from Tracks, Artists where Tracks.ArtistID=Artists.ArtistID";
            command = new SQLiteCommand(where_select_all, m_dbConnection);
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("TrackID:"+reader["TID"]);
                Console.WriteLine("TrackName:" + reader["TrackName"]);
                Console.WriteLine("ArtistName:" + reader["FirstName"]);

            }
        }
    }
}
