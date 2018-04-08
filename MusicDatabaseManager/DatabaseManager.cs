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

        public static void Main(string[] args)
        {
            //Tests for DatabaseManager
            SQLiteConnection.CreateFile("MyDatabase.sqlite");
            SQLiteConnection m_dbConnection;
            m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
            m_dbConnection.Open();
        }
    }
}
