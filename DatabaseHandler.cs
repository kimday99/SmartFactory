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

        public void InsertObject(int obNum, string obUrl, string obPoor, string obStatus,string obDate)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO Object (Ob_Num, Ob_Url, Ob_Poor, Ob_Status, Ob_Date) VALUES (@obNum, @obUrl, @obPoor, @obStatus, @obDate)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@obNum", obNum);
                        cmd.Parameters.AddWithValue("@obUrl", obUrl);
                        cmd.Parameters.AddWithValue("@obPoor", obPoor);
                        cmd.Parameters.AddWithValue("@obStatus", obStatus);
                        cmd.Parameters.AddWithValue("@obDate", obDate);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error inserting object into database: {ex.Message}");
                }
            }
        }
        public List<Dictionary<string, object>> SelectObjects(string status = null, string poor = null)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // 기본 쿼리 작성
                    string query = "SELECT * FROM Object WHERE 1=1";

                    // 조건에 따라 쿼리 수정
                    if (!string.IsNullOrEmpty(status))
                    {
                        query += " AND Ob_Status = @status";
                    }
                    if (!string.IsNullOrEmpty(poor))
                    {
                        query += " AND Ob_Poor = @poor";
                    }

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        // 조건에 따라 파라미터 추가
                        if (!string.IsNullOrEmpty(status))
                        {
                            cmd.Parameters.AddWithValue("@status", status);
                        }
                        if (!string.IsNullOrEmpty(poor))
                        {
                            cmd.Parameters.AddWithValue("@poor", poor);
                        }

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Dictionary<string, object> row = new Dictionary<string, object>();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    row.Add(reader.GetName(i), reader.GetValue(i));
                                }

                                results.Add(row);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error selecting objects from database: {ex.Message}");
                }
            }

            return results;
        }



    }
}
