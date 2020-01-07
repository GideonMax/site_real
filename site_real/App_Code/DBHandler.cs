using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;

namespace site_real.App_Code
{
    public class DBHandler : IDisposable
    {
        private OleDbConnection con;
        public DBHandler()
        {
            string cs = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            con = new OleDbConnection(cs);
            con.Open();
        }
        public void Dispose()
        {
            con.Dispose();
        }
        /// <summary>
        /// This method returns a user's ID given a username
        /// </summary>
        /// <param name="name">the user's name</param>
        /// <returns>ID</returns>
        public int getuser(string name)
        {
            string cmd = "SELECT [ID] FROM [Users] WHERE [user_name]= ? AND [is_active]";
            OleDbCommand command = new OleDbCommand(cmd, con);
            command.Parameters.AddWithValue("", name);
            using(OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (int)reader["ID"];
                }
            }
            return 0;
        }
        /// <summary>
        /// This method returns a user's ID given a username and a password
        /// </summary>
        /// <param name="name">the user's name</param>
        /// <param name="password">the user's password</param>
        /// <returns>ID</returns>
        public int getuser(string name, string password)
        {
            string cmd = "SELECT [ID] FROM [Users] WHERE [is_active] AND [user_name]=@Name AND [user_password]=@Password";
            OleDbCommand command = new OleDbCommand(cmd, con);
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Password", password);
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (int)reader["ID"];
                }
            }
            return 0;
        }
        /// <summary>
        /// This method returns data from the data table using a key
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the requested data</returns>
        public string getData(string key)
        {
            string command = "SELECT [value] FROM [Data] WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("@key", key);
            using(OleDbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (string)reader["value"];
                }
            }
            return null;
        }
        /// <summary>
        /// this function sets a key's value
        /// </summary>
        /// <param name="key">the key</param>
        /// <param name="value"></param>
        public void setData(string key, string value)
        {
            string command = "UPDATE [Data] SET [value]=@value WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.ExecuteNonQuery();
        }
        public void addData(string key, string value)
        {
            string command = "INSERT INTO [Data] ([key],[value]) VALUES(@key,@value)";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.ExecuteNonQuery();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool isAdmin(int index)
        {
            string command = "SELECT [is_admin] FROM[Users] WHERE [ID]=?";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("", index);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (bool)reader["is_admin"];
                }
            }
            return false;
        }
        public int adduser(string name,string password,bool is_admin=false)
        {
            if (getuser(name) != 0 || check_password(password))
            {
                return 0;
            }
            string command = "INSERT INTO [Users]([user_name],[user_password],[is_admin])"
                +" VALUES (@u_name,@u_password,@is_admin);";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("@u_name", name);
            cmd.Parameters.AddWithValue("@u_password", password);
            cmd.Parameters.AddWithValue("@is_admin", is_admin);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("SELECT @@IDENTITY", con);
            return (int)cmd.ExecuteScalar();
        }
        public string[] get_all_country_names()
        {
            int amount = 0;
            string command = "SELECT COUNT(*) as[amount] FROM [countries]";
            OleDbCommand cmd = new OleDbCommand(command, con);
            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    amount = (int)reader["amount"];
                }
            }
            if (amount == 0) return null;
            int index = 0;
            string[] names = new string[amount];
            command = "SELECT [country_name] FROM [countries]";
            cmd = new OleDbCommand(command, con);
            using(DbDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    names[index] = reader["country_name"].ToString();
                    index++;
                }
            }
            return names;
        }
        public string GetArticleByCountryName(string name)
        {
            string command = "SELECT [article] FROM [countries] WHERE [country_name]=@Name";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("@Name", name);
            string ret = null;
            using(DbDataReader reader= cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = reader["article"].ToString();
                }
            }
            return ret;
        }
        public bool check_password(string password)
        {
            string command = "SELECT COUNT(*) AS [amount] FROM [Users] WHERE [user_password]=?;";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("", password);
            using(DbDataReader reader = cmd.ExecuteReader())
            {
                if(reader.Read())
                {
                    if( (int)reader["amount"]==0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        public string getText(string key)
        {
            string command = "SELECT [_text] FROM [Texts] WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("@key", key);
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    return (string)reader["_text"];
                }
            }
            return null;
        }
        public void setText(string key, string value)
        {
            string command = "UPDATE [Texts] SET [_text]=@value WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.ExecuteNonQuery();
        }
        public void addText(string key, string value)
        {
            string command = "INSERT INTO [Texts] ([key],[_text]) VALUES(@key,@value)";
            OleDbCommand cmd = new OleDbCommand(command, con);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.ExecuteNonQuery();
        }
    }
}