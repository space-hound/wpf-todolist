using System;
using System.Data;
using System.Data.SQLite;

namespace TaskDoTo.DataBase
{
    public class DBConnection
    {
        #region static fields
        //the database file, 
        private static CFile database = new CFile("taskdata");
        //database conection
        private static string connection_string = $"Data Source={database.FilePath};Version=3;";
        private static SQLiteConnection connection = null;
        //instance of this
        private static DBConnection instance = null;
        #endregion


        #region constructor
        //private default constructor
        private DBConnection()
        {
            if(connection is null)
            {
                if (!database.Exists())
                {
                    SQLiteConnection.CreateFile(database.FullName);
                }

                connection = new SQLiteConnection(connection_string);

                this.Modify($"CREATE TABLE IF NOT EXISTS tasks (id INTEGER, title TEXT, description TEXT, status INTEGER)");
                this.Modify($"CREATE TABLE IF NOT EXISTS links (id INTEGER, href TEXT, name TEXT)");

            }
        }

        //get instance
        public static DBConnection Init()
        {
            if(instance is null)
            {
                instance = new DBConnection();
            }

            return instance;
        } 
        #endregion

        #region properties
        //get instance connection
        public SQLiteConnection Connection
        {
            get
            {
                return connection;
            }
        }

        //open connction 
        private void Open()
        {
            connection.Open();
        }
        //open connction 
        private void Close()
        {
            connection.Close();
        }

        //use for update, insert, delete
        public void Modify(string sql)
        {
            this.Open();

            SQLiteCommand command = new SQLiteCommand(sql, this.Connection);
            command.ExecuteNonQuery();

            this.Close();
        }
        //use for select
        public DataTable Select(string sql, string name = "_default_")
        {
            DataSet result;

            if (name == "_default_")
                result = new DataSet();
            else
                result = new DataSet(name);

            this.Open();

            SQLiteDataAdapter adpt = new SQLiteDataAdapter(sql, this.Connection);
            adpt.Fill(result);

            this.Close();

            return result.Tables[0];
        }

        public static ulong LastId(string tableName)
        {
            string sql = $"SELECT id FROM {tableName} WHERE id = (SELECT max(id) FROM {tableName})";
            SQLiteConnection tempConn = new SQLiteConnection(connection_string);
            tempConn.Open();
            SQLiteCommand command = new SQLiteCommand(sql, tempConn);
            SQLiteDataReader reader = command.ExecuteReader();
            ulong id = 0;
            while (reader.Read())
                id = Convert.ToUInt64(reader["id"]);
            reader.Close();
            tempConn.Clone();

            return id;
        }
        #endregion

    }
}
