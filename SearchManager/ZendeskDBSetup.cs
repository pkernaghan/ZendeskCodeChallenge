using System;
using System.Data;
using System.Data.SqlClient;
using ZendeskSearchManager.Exception;

namespace ZendeskSearchManager
{
    public static class ZendeskDBSetup
    {
        private static SqlConnection myConn;

        static ZendeskDBSetup()
        {
            if (myConn == null)
            {
                SetupSearchDataDb();
            }
        }

        public static void SetupSearchDataDb()
        {
            String str;
            myConn = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE MyDatabase ON PRIMARY " +
                  "(NAME = MyDatabase_Data, " +
                  "FILENAME = 'C:\\MyDatabaseData.mdf', " +
                  "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                  "LOG ON (NAME = MyDatabase_Log, " +
                  "FILENAME = 'C:\\MyDatabaseLog.ldf', " +
                  "SIZE = 1MB, " +
                  "MAXSIZE = 5MB, " +
                  "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);

            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {
                throw new ZendeskSearchManagerException(@"Error: Critical Initializing Zendesk Search Database.");
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
        }

        public static void DisposeOfDb(string dbName)
        {
            throw new NotImplementedException();
        }

    }
}
