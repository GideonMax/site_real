using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;

namespace site_real.App_Code
{
    public class DBHandler : IDisposable
    {
        readonly OleDbConnection Con;
        public DBHandler()
        {
            string cs = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            Con = new OleDbConnection(cs);
            Con.Open();
        }
        public void Dispose()
        {
            Con.Dispose();
        }
        /// <summary>
        /// This method returns a user's ID given a username
        /// </summary>
        /// <param name="name">the user's name</param>
        /// <returns>ID</returns>
        public int GetUser(string name)
        {
            string cmd = "SELECT [ID] FROM [Users] WHERE [user_name]= ? AND [is_active]";
            OleDbCommand command = new OleDbCommand(cmd, Con);
            command.Parameters.AddWithValue("", name);
            int ret = 0;
            using(OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret= (int)reader["ID"];
                }
            }
            command.Dispose();
            return ret;
        }
        /// <summary>
        /// This method returns a user's ID given a username and a password
        /// </summary>
        /// <param name="name">the user's name</param>
        /// <param name="password">the user's password</param>
        /// <returns>ID</returns>
        public int GetUser(string name, string password)
        {
            string cmd = "SELECT [ID] FROM [Users] WHERE [is_active] AND [user_name]=@Name AND [user_password]=@Password";
            OleDbCommand command = new OleDbCommand(cmd, Con);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Password", password);
            int ret = 0;
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret= (int)reader["ID"];
                }
            }
            command.Dispose();
            return ret;
        }
        /// <summary>
        /// This method returns data from the data table using a key
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the requested data</returns>
        public string GetData(string key)
        {
            string command = "SELECT [value] FROM [Data] WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@key", key);
            string ret = null;
            using(OleDbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret= (string)reader["value"];
                }
            }
            cmd.Dispose();
            return ret;
        }
        /// <summary>
        /// this function sets a key's value
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value"></param>
        public void SetData(string key, string value)
        {
            string command = "UPDATE [Data] SET [value]=@value WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        /// <summary>
        /// adds a new data pair to the database
        /// </summary>
        /// <param name="key">the pair's key</param>
        /// <param name="value">the pair's value</param>
        public void AddData(string key, string value)
        {
            string command = "INSERT INTO [Data] ([key],[value]) VALUES(@key,@value)";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsAdmin(int index)
        {
            string command = "SELECT [is_admin] FROM[Users] WHERE [ID]=?";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("", index);
            bool ret = false;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret= (bool)reader["is_admin"];
                }
            }
            cmd.Dispose();
            return ret;
        }
        public int Adduser(string name,string password,bool is_admin=false)
        {
            if (GetUser(name) != 0 || CheckPassword(password))
            {
                return 0;
            }
            string command = "INSERT INTO [Users]([user_name],[user_password],[is_admin])"
                +" VALUES (@u_name,@u_password,@is_admin);";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@u_name", name);
            cmd.Parameters.AddWithValue("@u_password", password);
            cmd.Parameters.AddWithValue("@is_admin", is_admin);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new OleDbCommand("SELECT @@IDENTITY", Con);
            int ret= (int)cmd.ExecuteScalar();
            cmd.Dispose();
            return ret;
        }
        public string[] GetAllCountryNames()
        {
            int amount = 0;
            string command = "SELECT COUNT(*) as[amount] FROM [countries]";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    amount = (int)reader["amount"];
                }
            }
            cmd.Dispose();
            if (amount == 0) return null;
            int index = 0;
            string[] names = new string[amount];
            command = "SELECT [country_name] FROM [countries]";
            cmd = new OleDbCommand(command, Con);
            using(DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    names[index] = reader["country_name"].ToString();
                    index++;
                }
            }
            cmd.Dispose();
            return names;
        }
        public string[] GetAllCountryCodes()
        {
            int amount = 0;
            string command = "SELECT COUNT(*) as[amount] FROM [countries]";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    amount = (int)reader["amount"];
                }
            }
            cmd.Dispose();
            if (amount == 0) return null;
            int index = 0;
            string[] names = new string[amount];
            command = "SELECT [code] FROM [countries]";
            cmd = new OleDbCommand(command, Con);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    names[index] = reader["code"].ToString();
                    index++;
                }
            }
            cmd.Dispose();
            return names;
        }
        public string GetArticleByCountryName(string name)
        {
            string command = "SELECT [article] FROM [countries] WHERE [country_name]=@Name";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Name", name);
            string ret = null;
            using(DbDataReader reader= cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = reader["article"].ToString();
                }
            }
            cmd.Dispose();
            return ret;
        }
        public string GetArticleByCountryCode(string name)
        {
            string command = "SELECT [article] FROM [countries] WHERE [code]=@Name";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Name", name);
            string ret = null;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = reader["article"].ToString();
                }
            }
            cmd.Dispose();
            ret = ret.Replace("\n", "<br>");
            return ret;
        }
        public bool CheckPassword(string password)
        {
            string command = "SELECT COUNT(*) AS [amount] FROM [Users] WHERE [user_password]=?;";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("", password);
            bool ret=false;
            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if(reader.Read())
                {
                    if( (int)reader["amount"]==0)
                    {
                        ret= false;
                    }
                    else
                    {
                        ret= true;
                    }
                }
            }
            cmd.Dispose();
            return ret;
        }
        public string GetText(string key)
        {
            string command = "SELECT [_text] FROM [Texts] WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@key", key);
            string ret = null;
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret= (string)reader["_text"];
                }
            }
            cmd.Dispose();
            return ret;
        }
        public void SetText(string key, string value)
        {
            string command = "UPDATE [Texts] SET [_text]=@value WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        public void AddText(string key, string value)
        {
            string command = "INSERT INTO [Texts] ([key],[_text]) VALUES(@key,@value)";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
    }
}