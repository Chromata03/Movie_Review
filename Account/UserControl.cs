using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Movie_Review.Account {
    public class UserControl {

        private byte[] salt;
        private string message;


        public UserControl() { }

        // Hashing
        protected static byte[] GenerateSalt() {
            byte[] salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider()) {
                rng.GetNonZeroBytes(salt);
            }
            return salt;
        }

        // Data Manipulation
        public int InsertUser(SqlConnection connection, SqlTransaction transaction, string username, string hashedPassword) {
            string insertUserQuery = @"
                INSERT INTO [users] (username, password, keys, created)
                VALUES (@Username, @Password, @Salt, @CreatedOn);
                SELECT SCOPE_IDENTITY(); -- Retrieve the generated userId
            ";

            using (SqlCommand insertUserCommand = new SqlCommand(insertUserQuery, connection, transaction)) {
                insertUserCommand.Parameters.AddWithValue("@Username", username);
                insertUserCommand.Parameters.AddWithValue("@Password", GenerateHash(hashedPassword));
                insertUserCommand.Parameters.AddWithValue("@Salt", salt);
                insertUserCommand.Parameters.AddWithValue("@CreatedOn", DateTime.Now);

                return Convert.ToInt32(insertUserCommand.ExecuteScalar());
            }
        }

        public void InsertUserInfo(SqlConnection connection, SqlTransaction transaction, int userId, string name, string email) {
            string insertUserInfoQuery = @"
                INSERT INTO userInfo (id, name, email, last_login)
                VALUES (@UserId, @Name, @Email, @Logged);
            ";

            using (SqlCommand insertUserInfoCommand = new SqlCommand(insertUserInfoQuery, connection, transaction)) {
                insertUserInfoCommand.Parameters.AddWithValue("@UserId", userId);
                insertUserInfoCommand.Parameters.AddWithValue("@Name", name);
                insertUserInfoCommand.Parameters.AddWithValue("@Email", email);
                insertUserInfoCommand.Parameters.AddWithValue("@Logged", DateTime.Now);

                insertUserInfoCommand.ExecuteNonQuery();
            }
        }
        
        public Dictionary<string, object> User(SqlConnection connection, string username, string password) {
            Dictionary<string, object> userDetails = new Dictionary<string, object>();
            string getUserQuery = "SELECT keys, password, id FROM users WHERE Username = @Username";

            using (SqlCommand getUserCommand = new SqlCommand(getUserQuery, connection)) {
                getUserCommand.Parameters.AddWithValue("@Username", username);
                SqlDataReader reader = getUserCommand.ExecuteReader();

                if (reader.Read()) {
                    salt = (byte[])reader["keys"];
                    string hashedPassword = GenerateHash(password, salt);

                    string saltedPassword = reader["password"].ToString();
                    if (saltedPassword.Equals(hashedPassword)) {
                        int userId = Convert.ToInt32(reader["id"]);
                        reader.Close();
                        userDetails = StoreUserDetailsInSession(connection, userId, username);
                        userDetails.Add("state", "logged");
                    }
                    else {
                        userDetails.Add("username", username);
                    }
                }
            }
            return userDetails;
        }

        private Dictionary<string, object> StoreUserDetailsInSession(SqlConnection connection, int userId, string username) {
            SqlCommand command = new SqlCommand("SELECT name, email FROM userInfo WHERE id = @UserId", connection);
            command.Parameters.AddWithValue("@UserId", userId);
            SqlDataReader dataReader = command.ExecuteReader();
            Dictionary<string, object> userDetails = new Dictionary<string, object>();

            if (dataReader.Read()) {
                userDetails.Add("id", userId);
                userDetails.Add("name", dataReader["name"].ToString());
                userDetails.Add("email", dataReader["email"].ToString());
                userDetails.Add("username", username);
                dataReader.Close();
            }
            return userDetails;
        }

        public int UpdateTable(SqlConnection connection, string table, int userId, Dictionary<string, object> param) {
            string[] keys = param.Keys.ToArray();
            string[] items = param.Values.Select(value => value.ToString()).ToArray();
            string data = "";
            int result = -1;

            for (int i = 0; i < keys.Length; i++) {
                data += keys[i] + " = @" + keys[i];
                if (i < keys.Length - 1) {
                    data += ", ";
                }
            }

            string query = "UPDATE " + table + " SET " + data + " WHERE id = @userId";

            using (SqlCommand command = new SqlCommand(query, connection)) {
                command.Parameters.AddWithValue("@userId", userId);
                foreach (var kvp in param) {
                    command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                }
                result = command.ExecuteNonQuery();
            }
            return result;
        }
        // Modifier
        private string ComputeHash(string input) {
            using (var hmac = new HMACSHA256(salt)) {
                byte[] hashedBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(input));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public string GenerateHash(string password) {
            salt = GenerateSalt();
            return ComputeHash(password);
        }

        public string GenerateHash(string password, byte[] salt) {
            this.salt = salt;
            return ComputeHash(password);
        }

        public void Reseed(SqlConnection connection) {
            string reseed = "DBCC CHECKIDENT ('dbo.users', RESEED, 1);";
            new SqlCommand(reseed, connection).ExecuteNonQuery();
        }

        public bool MessageIsEmpty {
            get {
                return string.IsNullOrEmpty(message);
            }
        }

        public void SetMessage(HtmlGenericControl headerContainer, Label headerMsg, string message) {
            this.message = message;
            headerContainer.Visible = true;
            headerMsg.Text = message;
        }

        public void LogError(string message) {
            try {
                string errorPath = HttpContext.Current.Server.MapPath("~/App_Data/ErrorLog");
                if (!File.Exists(errorPath)) {
                    using (StreamWriter createFileWriter = File.CreateText(errorPath)) {
                        createFileWriter.WriteLine("Error Log created at: " + DateTime.Now);
                    }
                }

                using (StreamWriter writer = File.AppendText(errorPath)) {
                    writer.WriteLine();
                    writer.WriteLineAsync(DateTime.Now + " " + message);
                }
            }
            catch (Exception e) {
                // Handle the exception, optionally log it elsewhere
                Console.WriteLine("Log was not successful");
            }
        }

        public byte[] Salt { get { return salt; } }
    }
}
