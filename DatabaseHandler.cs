using MySql.Data.MySqlClient;
using System;

namespace btnproject
{
    public class DatabaseHandler
    {
        private readonly string connectionString;

        public DatabaseHandler(string server, string database, string username, string password)
        {
            connectionString = $"Server={server};Database={database};User ID={username};Password={password};";
        }

        public void InsertObject(int obNum, string obName, string obUrl, string obType)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO OBJECT (OB_NUM, OB_NAME, OB_URL, OB_TYPE) VALUES (@obNum, @obName, @obUrl, @obType)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@obNum", obNum);
                        cmd.Parameters.AddWithValue("@obName", obName);
                        cmd.Parameters.AddWithValue("@obUrl", obUrl);
                        cmd.Parameters.AddWithValue("@obType", obType);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inserting object into database: {ex.Message}");
                }
            }
        }

        public void InsertObjectLog(int obNum, string obState, string obPoor, DateTime obDate, string obColor)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO OBJECTLOG (OB_NUM, OB_STATE, OB_POOR, OB_DATE, OB_COLOR) VALUES (@obNum, @obState, @obPoor, @obDate, @obColor)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@obNum", obNum);
                        cmd.Parameters.AddWithValue("@obState", obState);
                        cmd.Parameters.AddWithValue("@obPoor", obPoor ?? (object)DBNull.Value);
                        cmd.Parameters.AddWithValue("@obDate", obDate);
                        cmd.Parameters.AddWithValue("@obColor", obColor);

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inserting object log into database: {ex.Message}");
                }
            }
        }
    }
}
