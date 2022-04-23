using System.Data.SqlClient;

namespace ybcDatabaseClass
{
    public class DatabaseClass
    {
        SqlConnection conn;
        SqlCommand cmd;
        public DatabaseClass(string dbstring) 
        {
            conn = new SqlConnection();
            cmd = new SqlCommand();
            OpenDB(dbstring);
        }

        private void OpenDB(string dbstring)
        {
            string dbtext = dbstring;
            //HttpContext.Current.Response.Write(dbtext);
            //HttpContext.Current.Response.End();
            conn.ConnectionString = dbtext;
            conn.Open();
        }
        
        public SqlDataReader CallSelect(string sqltext)
        {
            cmd.CommandText = sqltext;
            cmd.Connection = conn;
            SqlDataReader sd = cmd.ExecuteReader();
            return sd;

        }
        public void CallInsert(string sqltext)
        {           
            
            cmd.Connection = conn;
            cmd.CommandText = sqltext;
            cmd.ExecuteNonQuery();
            CloseDB();
        }
        public void CloseDB()
        {
            conn.Close();
            cmd.Dispose();
            cmd = null;
            conn = null;
        }
    }
}
