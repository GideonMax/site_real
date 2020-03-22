using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;

namespace site_real
{
    public class DBHandler : IDisposable
    {
        readonly OleDbConnection Con;
        public DBHandler()
        {
            string cs = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            Con = new OleDbConnection(cs);
            Con.InfoMessage += Con_InfoMessage;
            Con.StateChange += Con_StateChange;
            Con.Open();
        }

        private void Con_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            //Console.WriteLine("hello");
        }

        private void Con_InfoMessage(object sender, OleDbInfoMessageEventArgs e)
        {
            Console.WriteLine("con infomessage");
        }

        public void Dispose()
        {
            
            Con.Close();
            Con.Dispose();
            Console.WriteLine("db.dispose");
        }
        #region users
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
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = (int)reader["ID"];
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
                    ret = (int)reader["ID"];
                }
            }
            command.Dispose();
            return ret;
        }
        /// <summary>
        /// Given the ID of a user, this function checks if they are an admin.
        /// </summary>
        /// <param name="index">The user's ID which you can get using GetUser</param>
        /// <returns>Is the user an admin.</returns>
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
                    ret = (bool)reader["is_admin"];
                }
            }
            cmd.Dispose();
            return ret;
        }
        /// <summary>
        /// Adds a new user to the database, and returns their ID;
        /// </summary>
        /// <param name="name">The user's name</param>
        /// <param name="password">The user's password</param>
        /// <param name="is_admin">Is the user an admin</param>
        /// <returns>The user's ID</returns>
        public int Adduser(string name, string password, bool is_admin = false)
        {
            if (GetUser(name) != 0 || CheckPassword(password))
            {
                return 0;
            }
            string command = "INSERT INTO [Users] ([user_name],[user_password],[is_admin])"
                + " VALUES (@u_name,@u_password,@is_admin);";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@u_name", name);
            cmd.Parameters.AddWithValue("@u_password", password);
            cmd.Parameters.AddWithValue("@is_admin", is_admin);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new OleDbCommand("SELECT @@IDENTITY", Con);
            int ret = (int)cmd.ExecuteScalar();
            cmd.Dispose();
            return ret;
        }
        /// <summary>
        /// Checks if a password already exists, returns true if it does and false if it does not.
        /// </summary>
        /// <param name="password">The password to check</param>
        /// <returns>Does the password already exist</returns>
        public bool CheckPassword(string password)
        {
            string command = "SELECT COUNT(*) AS [amount] FROM [Users] WHERE [user_password]=?;";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("", password);
            bool ret = false;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = (int)reader["amount"] != 0;
                }
            }
            cmd.Dispose();
            return ret;
        }
        #endregion
        #region data
        /// <summary>
        /// Lets you get Data using indexing.<br></br>
        /// for example:
        /// <code>string AdminKey = db.Data["admin_key"];</code><br></br>
        /// <code>db.Data["something important idk"] = "noooooo";</code>
        /// </summary>
        public IndexedProperty<string,string> Data
        {
            get
            {
                return new IndexedProperty<string, string>(
                    key => GetData(key),
                    (string key, string value) => { SetData(key, value); }
                    );
            }
        }


        public bool DoesDataKeyExist(string key)
        {

            string command = "SELECT COUNT(*) AS [amount] FROM [Data] WHERE [key]=@Key;";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Key", key);
            bool ret = false;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = (int)reader["amount"] != 0;
                }
            }
            cmd.Dispose();
            return ret;
        }
        /// <summary>
        /// This method returns data from the data table using a key
        /// </summary>
        /// <param name="key">the key</param>
        /// <returns>the requested data</returns>
        string GetData(string key)
        {
            if (!DoesDataKeyExist(key)) return null;
            string command = "SELECT [value] FROM [Data] WHERE [key]=@key";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@key", key);
            string ret = null;
            using (OleDbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = (string)reader["value"];
                }
            }
            cmd.Dispose();
            return ret;
        }
        /// <summary>
        /// This function sets a key's value.
        /// </summary>
        /// <param name="key">The key</param>
        /// <param name="value">The value</param>
        void SetData(string key, string value)
        {
            if (DoesDataKeyExist(key))
            {
                AddData(key, value);
            }
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
        void AddData(string key, string value)
        {
            string command = "INSERT INTO [Data] ([key],[value]) VALUES(@key,@value)";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        #endregion
        #region countries

        /// <summary>
        /// Lets you get a country's CountryInfo using indexing.<br></br>
        /// for example:
        /// <code>CountryInfo America = db.Countries["US"];</code><br></br>
        /// <code>db.Countries["ILS"] = SomeFunctionOrSomethingThatReturnsACountryInfo();;</code>
        /// </summary>
        public IndexedProperty<string, CountryInfo> Countries
        {
            get
            {
                return new IndexedProperty<string, CountryInfo>(
                    (string code) => { return GetCountryInfoByCode(code); },
                    (string code, CountryInfo info) => { SetCountryInfoByCode(code, info); }
                    );
            }
        }


        public bool DoesCountryExist(string code)
        {
            string command = "SELECT COUNT(*) AS [amount] FROM [countries] WHERE [code]=?;";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("", code);
            bool ret = false;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = (int)reader["amount"] != 0;
                }
            }
            cmd.Dispose();
            return ret;
        }
        CountryInfo GetCountryInfoByCode(string code)
        {
            if (!DoesCountryExist(code)) return null;
            CountryInfo info = new CountryInfo();
            string command = "SELECT [article] FROM [countries] WHERE [code]=@Name";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Name", code);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    info.OfficialArticle= reader["article"].ToString().Replace("\n","<br>");
                }
            }
            cmd.Dispose();
            command = "SELECT [user_article] FROM [countries] WHERE [code]=@Name";
            cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Name", code);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    info.UserArticle = reader["user_article"].ToString().Replace("\n", "<br>");
                }
            }
            cmd.Dispose();
            command = "SELECT [country_name] FROM [countries] WHERE [code]=@Name";
            cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Name", code);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    info.CountryName= reader["country_name"].ToString();
                }
            }
            cmd.Dispose();
            return info;
        }
        void SetCountryInfoByCode(string code, CountryInfo info)
        {
            if (!DoesCountryExist(code))
            {
                AddCountryInfoByCode(code, info);
                return;
            }
            if (info.OfficialArticle != null)
            {
                string command = "UPDATE [countries] SET [article]=@Article WHERE [code]=@Code;";
                OleDbCommand cmd = new OleDbCommand(command, Con);
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@Article", info.OfficialArticle);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }
            if (info.UserArticle != null)
            {
                string command = "UPDATE [countries] SET [user_article]=@Article WHERE [code]=@Code;";
                OleDbCommand cmd = new OleDbCommand(command, Con);
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@Article", info.OfficialArticle);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
            }


        }
        void AddCountryInfoByCode(string code, CountryInfo info)
        {
            string command = "INSERT INTO [countries] ([code],[country_name],[article],[user_article])" +
                " VALUES (@Code,@Name,@Article,@UserArticle);";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Code", code);
            cmd.Parameters.AddWithValue("@Name",info.CountryName);
            cmd.Parameters.AddWithValue("@Article", info.OfficialArticle);
            cmd.Parameters.AddWithValue("@UserArticle", info.UserArticle);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            return;
        }






        /// <summary>
        /// Returns all the country names according to the database.
        /// Not reccomended.
        /// </summary>
        /// <returns>All the names</returns>
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
        /// <summary>
        /// Returns all the country codes listed in our database.
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Deprecated.
        /// Gets a country's article by its name.
        /// </summary>
        /// <param name="name">The country's name</param>
        /// <returns>The country's article</returns>
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
        /// <summary>
        /// Deprecated.
        /// Gets a country's article given its code.
        /// </summary>
        /// <param name="name">The country's code</param>
        /// <returns>its article</returns>
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
        #endregion
        #region text
        /// <summary>
        /// Lets you get a text using indexing.<br></br>
        /// for example:
        /// <code>string MainText = db.Texts["main"];</code><br></br>
        /// <code>db.Texts["main"] = "this is an example";</code>
        /// </summary>
        public IndexedProperty<string,string> Texts
        {
            get
            {
                return new IndexedProperty<string, string>(
                    key => GetText(key),
                    (key, value) => { SetText(key, value); }
                    );
            }
        }

        public bool DoesTextExist(string key)
        {
            string command = "SELECT COUNT(*) AS [amount] FROM [Texts] WHERE [key]=@Key;";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Key", key);
            bool ret = false;
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ret = (int)reader["amount"] != 0;
                }
            }
            cmd.Dispose();
            return ret;
        }

        public string GetText(string key)
        {
            if (!DoesTextExist(key)) return null;
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
            if (!DoesTextExist(key))
            {
                AddText(key, value);
                return;
            }
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
        #endregion
    }

    /// <summary>
    /// This class contains information about a certain country:
    /// including its name, official article written by admins and a user article written by users.
    /// </summary>
    public class CountryInfo
    {
        public string CountryName = null;
        public string OfficialArticle = null;
        public string UserArticle = null;
    } 
}