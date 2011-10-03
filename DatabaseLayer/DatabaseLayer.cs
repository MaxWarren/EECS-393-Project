using System;
using System.Data.SqlClient;

namespace DatabaseLayer
{
    /// <summary>
    /// Static class that interfaces with the database
    /// </summary>
    public static class DatabaseLayer
    {
        /// <summary>
        /// Connection to the database
        /// </summary>
        private static SqlConnection dbConnection;

        /// <summary>
        /// Connection string for connecting to the database
        /// </summary>
        private static string connString;

        /// <summary>
        /// Initialize the database connection
        /// </summary>
        static DatabaseLayer()
        {
            // Set connection string
            connString = string.Format("user id={0};password={1};server={2};Trusted_Connection=yes;database={3};connection timeout={4}",
                Properties.Settings.Default.user_id,
                Properties.Settings.Default.password,
                Properties.Settings.Default.server,
                Properties.Settings.Default.database,
                Properties.Settings.Default.timeout);

            dbConnection = new SqlConnection(connString);

            try // Try to open the connection
            {
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                Environment.Exit(1); // TODO fix this
            }

        }
    }
}
