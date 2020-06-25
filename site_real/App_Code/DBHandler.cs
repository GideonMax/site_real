using System;
using System.Collections.Generic;
using System.Web;
using System.Configuration;
using System.Data.Common;
using System.Data.OleDb;
using System.Security.Cryptography;

namespace site_real
{
    public static class DBHandler
    {
        const int SaltLength = 27;
        const int HashLength = 41;
        const int HashRepetitions = 1021;
        static OleDbConnection Con;
        static int AmountOfConnections = 0;
        public static void Open()
        {
            if (AmountOfConnections == 0)
            {
                _open();
            }
            AmountOfConnections++;
        }
        public static void Close()
        {
            AmountOfConnections--;
            if (AmountOfConnections < 0)
            {
                AmountOfConnections = 0;
                return;
            }
            if (AmountOfConnections == 0)
            {
                _close();
            }
        }
        static void _open()
        {
            string cs = ConfigurationManager.ConnectionStrings["Database"].ConnectionString;
            Con = new OleDbConnection(cs);
            Con.Open();
        }
        static void _close()
        {
            Con.Close();
            Con.Dispose();
        }
        #region users
        /// <summary>
        /// This method returns a user's ID given a username
        /// </summary>
        /// <param name="name">the user's name</param>
        /// <returns>ID</returns>
        public static int GetUser(string name)
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
       static byte[] Hash(string Password, byte[] salt)
       {
            byte[] ret = null;
            using (Rfc2898DeriveBytes Hasher = new Rfc2898DeriveBytes(Password, salt, HashRepetitions))
            {
                ret = Hasher.GetBytes(HashLength);
            }
            return ret;
       }
        /// <summary>
        /// This method returns a user's ID given a username and a password
        /// </summary>
        /// <param name="name">the user's name</param>
        /// <param name="password">the user's password</param>
        /// <returns>ID</returns>
        public static int GetUser(string name, string password)
        {
            string cmd = "SELECT [ID],[hash],[salt] FROM [Users] WHERE [is_active] AND [user_name]=@Name";

            OleDbCommand command = new OleDbCommand(cmd, Con);
            command.Parameters.AddWithValue("@Name", name);
            int ret = 0;
            using (OleDbDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    byte[] hash = Hash(password, (byte[])reader["salt"]);
                    if(CompareByteArrays(hash,(byte[])reader["hash"]))
                        ret = (int)reader["ID"];
                }
            }
            command.Dispose();
            return ret;
        }
        static bool CompareByteArrays(byte[] a, byte[] b)
        {
            if (a.Length != b.Length) return false;
            for(int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
        /// <summary>
        /// Given the ID of a user, this function checks if they are an admin.
        /// </summary>
        /// <param name="index">The user's ID which you can get using GetUser</param>
        /// <returns>Is the user an admin.</returns>
        public static bool IsAdmin(int index)
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
        /// Adds a new user to the database, and returns their ID
        /// </summary>
        /// <param name="name">The user's name</param>
        /// <param name="password">The user's password</param>
        /// <param name="is_admin">Is the user an admin</param>
        /// <returns>The user's ID</returns>
        public static int Adduser(string name, string password, bool IsAdmin = false)
        {
            if (GetUser(name) != 0)
            {
                return 0;
            }
            byte[] Salt = new byte[SaltLength];
            using (RNGCryptoServiceProvider CryptographicProvider = new RNGCryptoServiceProvider())
                CryptographicProvider.GetNonZeroBytes(Salt);
            byte[] hash = Hash(password, Salt);
            string command = "INSERT INTO [Users] ([user_name],[hash],[salt],[is_admin])"
                + " VALUES (@UserName,@Hash,@Salt,@IsAdmin);";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@UserName", name);
            cmd.Parameters.AddWithValue("@Hash", hash);
            cmd.Parameters.AddWithValue("@Salt", Salt);
            cmd.Parameters.AddWithValue("@IsAdmin", IsAdmin);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            cmd = new OleDbCommand("SELECT @@IDENTITY", Con);
            int ret = (int)cmd.ExecuteScalar();
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
        public static IndexedProperty<string,string> Data
        {
            get
            {
                return new IndexedProperty<string, string>(
                    key => GetData(key),
                    (string key, string value) => { SetData(key, value); }
                    );
            }
        }


        public static bool DoesDataKeyExist(string key)
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
        static string GetData(string key)
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
        static void SetData(string key, string value)
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
        static void AddData(string key, string value)
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
        static public IndexedProperty<string, CountryInfo> Countries
        {
            get
            {
                return new IndexedProperty<string, CountryInfo>(
                    (string code) => { return GetCountryInfoByCode(code); },
                    (string code, CountryInfo info) => { SetCountryInfoByCode(code, info); }
                    );
            }
        }


        public static bool DoesCountryExist(string code)
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
        static CountryInfo GetCountryInfoByCode(string code)
        {
            if (!DoesCountryExist(code)) return null;
            CountryInfo info = new CountryInfo();
            string command = "SELECT [ID],[article],[user_article],[country_name] FROM [countries] WHERE [code]=@Name";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Name", code);
            using (DbDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    info.OfficialArticle= reader["article"].ToString().Replace("\n","<br>");
                    info.UserArticle = reader["user_article"].ToString().Replace("\n", "<br>");
                    info.CountryName = reader["country_name"].ToString();
                    info.ID = (int)reader["ID"];
                }
            }
            cmd.Dispose();
            return info;
        }
        static void SetCountryInfoByCode(string code, CountryInfo info)
        {
            if (!DoesCountryExist(code))
            {
                AddCountryInfoByCode(code, info);
                return;
            }
            if (info.IsAdminRequest&&info.CountryName!=null)
            {
                string command = "UPDATE [countries] SET [country_name]=@Name WHERE [code]=@Code;";
                OleDbCommand cmd = new OleDbCommand(command, Con);
                cmd.Parameters.AddWithValue("@Code", code);
                cmd.Parameters.AddWithValue("@Name", info.CountryName);
                cmd.ExecuteNonQuery();
                cmd.Dispose();
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
        static void AddCountryInfoByCode(string code, CountryInfo info)
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
        public static string[] GetAllCountryNames()
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
        public static string[] GetAllCountryCodes()
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
        public static string GetArticleByCountryName(string name)
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
        public static string GetArticleByCountryCode(string name)
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
        #region CountryForum
        public static void AddNewComment(Comment c)
        {
            AddNewComment(c.Country, c.UserId, c.Body);
        }
        public static void AddNewComment(int Country, int User, string Body)
        {
            string command = "INSERT INTO [CountryForums] ([CountryID],[UserID],[Body])" +
                " VALUES (@Country,@User,@Body)";
            
            using (OleDbCommand cmd = new OleDbCommand(command, Con))
            {
                cmd.Parameters.AddWithValue("@Country", Country);
                cmd.Parameters.AddWithValue("@User", User);
                cmd.Parameters.AddWithValue("@Body", Body);
                cmd.ExecuteNonQuery();
            }
        }
        public static Comment[] GetCountryComments(int Country)
        {
            string command = "SELECT [CommentID],[UserID],[user_name],[Body]" +
                " FROM [CountryForums] AS C" +
                " INNER JOIN [Users] AS U ON C.[UserID]=U.[ID]" +
                " WHERE [is_active] AND [CountryID]=@Country AND [Exists]" +
                " ORDER BY [CreationDate] ASC";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@Country", Country);
            List<Comment> ret = new List<Comment>();
            using (OleDbDataReader Reader = cmd.ExecuteReader())
            {
                while (Reader.Read())
                {
                    Comment c = new Comment();
                    c.ID = (int)Reader["CommentID"];
                    c.Country = Country;
                    c.UserId = (int)Reader["UserID"];
                    c.UserName = (string)Reader["user_name"];
                    c.Body = (string)Reader["Body"];
                    ret.Add(c);
                }
            }
            cmd.Dispose();
            return ret.ToArray();
        }
        #endregion
        #region text
        /// <summary>
        /// Lets you get a text using indexing.<br></br>
        /// for example:
        /// <code>string MainText = db.Texts["main"];</code><br></br>
        /// <code>db.Texts["main"] = "this is an example";</code>
        /// </summary>
        public static IndexedProperty<string,string> Texts
        {
            get
            {
                return new IndexedProperty<string, string>(
                    key => GetText(key),
                    (key, value) => { SetText(key, value); }
                    );
            }
        }

        public static bool DoesTextExist(string key)
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

        public static string GetText(string key)
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
        public static void SetText(string key, string value)
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
        public static void AddText(string key, string value)
        {
            string command = "INSERT INTO [Texts] ([key],[_text]) VALUES(@key,@value)";
            OleDbCommand cmd = new OleDbCommand(command, Con);
            cmd.Parameters.AddWithValue("@key", key);
            cmd.Parameters.AddWithValue("@value", value);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
        }
        #endregion
        #region BugReports
        /// <summary>
        /// gets the ID Title and user name of every open bug report
        /// </summary>
        /// <returns></returns>
        public static DbDataReader GetAllBugReports()
        {
            string sql = "SELECT S.[ID], [Title], [user_name]" +
                " FROM [BugReports] AS S" +
                " INNER JOIN [Users] AS U ON S.[User]=U.[ID]" +
                " WHERE not [Closed]" +
                " ORDER BY [creation_time] DESC";
            OleDbCommand cmd = new OleDbCommand(sql, Con);
            return cmd.ExecuteReader();
        }
        /// <summary>
        /// Makes a new bug report
        /// </summary>
        /// <param name="Title">the report's title</param>
        /// <param name="Body">the report's body</param>
        /// <param name="User">The ID of the user</param>
        /// <returns></returns>
        public static int OpenBugReport(string Title, string Body, int User)
        {
            string sql = "INSERT INTO [BugReports] ([Title], [ReportBody], [User])" +
                " VALUES (@Title, @Body, @UserID)";
            OleDbCommand cmd = new OleDbCommand(sql, Con);
            cmd.Parameters.AddWithValue("@Title", Title);
            cmd.Parameters.AddWithValue("@Body", Body);
            cmd.Parameters.AddWithValue("@UserID", User);
            cmd.ExecuteNonQuery();
            cmd = new OleDbCommand("SELECT @@IDENTITY", Con);
            int id = (int)cmd.ExecuteScalar();
            return id;
        }
        #endregion
    }

    /// <summary>
    /// This class contains information about a certain country:
    /// including its name, official article written by admins and a user article written by users.
    /// </summary>
    public class CountryInfo
    {
        public int ID;
        public string CountryName = null;
        public string OfficialArticle = null;
        public string UserArticle = null;
        public bool IsAdminRequest = false;
    } 
    public class Comment
    {
        public int ID;
        public int Country;
        public int UserId;
        public string UserName;
        public string Body;
    }
}