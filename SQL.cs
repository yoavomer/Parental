using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Data.SqlClient;
using System.Text;

namespace Server
{
    static class SQL
    {
        /// <summary>
        /// receive column and string and check whether the string exist in column
        /// </summary>
        /// <param name="column"></param>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool Check(string column, string s)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT COUNT (*) FROM DataUsers WHERE " + column + " = '" + s + "'", connection);

            connection.Open();
            string sql = "";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                sql += rdr[""].ToString();
            }
            connection.Close();
            return (!sql.Equals("0"));
        }
        /// <summary>
        /// return supervisor of username receive
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string SuperVisor(string username) 
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Supervisor FROM DataUsers WHERE Username = '" + username + "'", connection);

            connection.Open();
            string superVisor = "";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                superVisor += rdr["Supervisor"].ToString();
            }
            connection.Close();
            return superVisor;
        }
        /// <summary>
        /// adds to supervisor a Uri request with url and the client who sent it
        /// </summary>
        /// <param name="supervisor"></param>
        /// <param name="URI"></param>
        public static void AddWebMessage(string supervisor,string URI)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand sql = new SqlCommand("SELECT URL FROM "+ supervisor + "WHERE Username = '," + URI.Split('^')[0]+" '" , connection);

            connection.Open();
            string oldMessages = "";
            SqlDataReader rdr1 = sql.ExecuteReader();
            while (rdr1.Read())
            {
                oldMessages += rdr1["URL"].ToString();
            }

            URI += oldMessages;
            connection.Close();
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE "+ supervisor + "SET URL = '" + URI.Split('^')[1] + "' WHERE Username = '," + URI.Split('+')[0] + "'", connection);

            SqlDataReader rdr2 = cmd.ExecuteReader();
            while (rdr2.Read())
            {
                oldMessages += rdr2[""].ToString();
            }
            connection.Close();
        }
        /// <summary>
        /// adds to supervisor a time request with the client who sent it
        /// </summary>
        /// <param name="supervisor"></param>
        /// <param name="TimeMessageSenders"></param>
        public static void AddTimeMessage(string supervisor,string TimeMessageSenders)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand sql = new SqlCommand("UPDATE " + supervisor + " SET Time = '1' WHERE Username = '," + TimeMessageSenders + "'", connection);
            SqlDataReader rdr2 = sql.ExecuteReader();



            connection.Close();
        }
        /// <summary>
        /// creating a data base to receive clients 
        /// </summary>
        /// <param name="client"></param>
        public static void Addult(Client client)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand add = new SqlCommand();

            add.CommandText = ("CREATE TABLE " + client.Username() + "( Username varchar(255), Approval CHAR (10), Time bit, URL varchar(255), History varchar(255))");
            add.Connection = connection;
            connection.Open();
            add.ExecuteReader();
            connection.Close();

        }
        /// <summary>
        /// cleans the time messages for clients
        /// </summary>
        /// <param name="username"></param>
        public static void CleanTime(string username)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("UPDATE "+username+" SET TimeMessageSenders = Null;", connection);
            
            connection.Open();
            SqlDataReader rdr = cmd.ExecuteReader();
            connection.Close();
        }
        /// <summary>
        /// returning a username received email from data base
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string Mail(string username)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT MailAddress FROM DataUsers WHERE Username = '" + username + "'", connection);

            connection.Open();
            string MailAddress = "";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                MailAddress += rdr["MailAddress"].ToString();
            }
            connection.Close();
            return MailAddress;
        }
        /// <summary>
        /// return client forbidden urls
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string MyUrls(string username)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT ForbiddenUrls FROM ForbiddenUrls WHERE Username = '" + username + "'", connection);

            connection.Open();
            string ForbiddenUrls = "";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                ForbiddenUrls += rdr["ForbiddenUrls"].ToString();
            }
            connection.Close();
            return ForbiddenUrls.Trim();
        }
        /// <summary>
        /// adding a user to data base or creating a data base depends on his age 
        /// </summary>
        /// <param name="client"></param>
        public static void AddUser(Client client)
        {
            if (client.Per().Contains("adult"))
            {
                Addult(client);
            }
            else
            {
                string connectionString = 
                    @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
                SqlConnection connection = new SqlConnection(connectionString);

                SqlCommand add1 = new SqlCommand();
                add1.CommandText = "INSERT INTO ForbiddenUrls(Username)  VALUES(@username) ";
                add1.Parameters.AddWithValue("@username", client.Username());
                add1.Connection = connection;
                connection.Open();
                add1.ExecuteReader();
                connection.Close();
            }
           
        }
        /// <summary>
        /// check whether got approved already by supervisor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="supervisor"></param>
        /// <returns></returns>
        public static string AprovalCheck(string username, string supervisor)
        {
            string Askers = "";
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            while (!(Askers.Contains('1')))
            {
                SqlCommand Cmd = new SqlCommand("SELECT Approval FROM " + supervisor + " WHERE Username = '," + username+"'", connection);
                connection.Open();
                SqlDataReader rdr1 = Cmd.ExecuteReader();
                while (rdr1.Read())
                {
                    Askers += rdr1["Approval"].ToString();
                }
                connection.Close();
                Askers = Askers.Trim();
            }
            return Askers;

        }
        /// <summary>
        /// adds a forbidden url to client
        /// </summary>
        /// <param name="username"></param>
        /// <param name="URL"></param>
        public static void AddUrls(string username,string URL)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd;
            if (URL == "")
            {
                cmd = new SqlCommand("UPDATE ForbiddenUrls SET ForbiddenUrls = NULL WHERE Username = '" + username + "'", connection);
            }
            else
            {
                cmd = new SqlCommand("UPDATE ForbiddenUrls SET ForbiddenUrls = '" + URL + "' WHERE Username = '" + username + "'", connection);
            }
            connection.Open();
            SqlDataReader rdr2 = cmd.ExecuteReader();
            connection.Close();
        }
        /// <summary>
        /// adds a ask for approval from supervisor
        /// </summary>
        /// <param name="username"></param>
        /// <param name="superuser"></param>
        public static void TryApprove(string username,string superuser)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand add2 = new SqlCommand();
            add2.CommandText = "INSERT INTO " + superuser + "(Username, Approval)  VALUES (@username, '') ";
            add2.Parameters.AddWithValue("@username", ','+username);
            add2.Connection = connection;
            connection.Open();
            add2.ExecuteReader();
            connection.Close();
        }
        /// <summary>
        /// got aproved by super visor, updating the data base
        /// </summary>
        /// <param name="decided"></param>
        /// <param name="superuser"></param>
        public static void Approved(string decided, string superuser)
        {
            string username = decided.Split(',')[0];
            string finalDec = decided.Split(',')[1];

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            if (finalDec.Contains("approved"))
            {
                SqlCommand add2 = new SqlCommand();
                add2.CommandText = "UPDATE " + superuser + " SET Approval = '1' WHERE Username = '," + username + "'";
                add2.Parameters.AddWithValue("@username", username);
                add2.Connection = connection;
                connection.Open();
                add2.ExecuteReader();
                connection.Close();
            }
            else
            {
                SqlCommand add2 = new SqlCommand();
                add2.CommandText = "DELETE FROM " + superuser + " WHERE Username = '" + username+"'" ;
                add2.Parameters.AddWithValue("@username", username);
                add2.Connection = connection;
                connection.Open();
                add2.ExecuteReader();
                connection.Close();
            }
        }
        /// <summary>
        /// return a string array of the undervisor of username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string[] UnderVisors(string username)
        {
            
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);

            SqlCommand approvedCmd = new SqlCommand("SELECT Username FROM " + username + " WHERE Approval = 1 ", connection);
            connection.Open();
            string approved = "";
            SqlDataReader rdr = approvedCmd.ExecuteReader();
            while (rdr.Read())
            {
                approved += rdr["Username"].ToString();
            }
            connection.Close();
            string[] users = approved.Split(',');
            return users;
        }
        /// <summary>
        /// remove specific forbidden url from client
        /// </summary>
        /// <param name="username"></param>
        /// <param name="URL"></param>
        public static void RemoveURL(string username, string URL)
        {
            string urls = MyUrls(username);
            AddUrls(username, urls.Replace(URL, ""));
        }
        /// <summary>
        /// removes url request from username
        /// </summary>
        /// <param name="username"></param>
        /// <param name="url"></param>
        public static void RemoveURLReq(string username, string url)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand sql = new SqlCommand("SELECT URL FROM " + SuperVisor(username.Split(',')[1]) + "WHERE Username = '" + username + " '", connection);

            connection.Open();
            string oldUrl = "";
            SqlDataReader rdr1 = sql.ExecuteReader();
            while (rdr1.Read())
            {
                oldUrl += rdr1["URL"].ToString();
            }
            connection.Close();
            oldUrl = oldUrl.Replace(url, "");
            connection.Open();
            SqlCommand cmd;
            if (oldUrl == "")
            {
                cmd = new SqlCommand("UPDATE " + SuperVisor(username.Split(',')[1]) + "SET URL = NULL WHERE Username = '" + username + "'", connection);
            }
            else
            {
                cmd = new SqlCommand("UPDATE " + SuperVisor(username.Split(',')[1]) + "SET URL = '"+ oldUrl + "' WHERE Username = '" + username + "'", connection);
            }

            SqlDataReader rdr2 = cmd.ExecuteReader();
            while (rdr2.Read())
            {
                oldUrl += rdr2[""].ToString();
            }
            connection.Close();
        }
        /// <summary>
        /// removes a time request from username
        /// </summary>
        /// <param name="username"></param>
        public static void RemoveTimeReq(string username)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand cmd = new SqlCommand("UPDATE " + SuperVisor(username.Split(',')[1]) + "SET Time = '0' WHERE Username = '," + username + "'", connection);
            string oldUrl = "";
            SqlDataReader rdr2 = cmd.ExecuteReader();
            while (rdr2.Read())
            {
                oldUrl += rdr2[""].ToString();
            }
            connection.Close();
        }
        /// <summary>
        /// return the time undervisor can use the internet
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static string Time(string username)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Time FROM ForbiddenUrls WHERE Username = '" + username + "'", connection);

            connection.Open();
            string Time = "";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Time += rdr["Time"].ToString();
            }
            connection.Close();
            return Time;
        }
        /// <summary>
        /// add to undervisor time to data base he can use the internet
        /// </summary>
        /// <param name="username"></param>
        /// <param name="time"></param>
        public static void AddTime(string username,string time)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd;
            cmd = new SqlCommand("UPDATE ForbiddenUrls SET Time = '" + time + "' WHERE Username = '" + username + "'", connection);
            connection.Open();
            SqlDataReader rdr2 = cmd.ExecuteReader();
            connection.Close();
        }
        /// <summary>
        /// adults need to recieve messages so this 
        /// checking messages every time they refresh
        /// </summary>
        /// <param name="client"></param>
        public static void CheckMessages(Client client)
        {
            string timeMessages = " got ran out of time, do you want to add him some more time?";
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand approvalCmd = new SqlCommand("SELECT Username FROM " + client.Username() + " WHERE Approval != 1 ", connection);
            connection.Open();
            string approvalAskers = "";
            SqlDataReader rdr0 = approvalCmd.ExecuteReader();
            while (rdr0.Read())
            {
                approvalAskers += rdr0["Username"].ToString();
            }
            connection.Close();
            if (approvalAskers != "")
            {
                string[] users = approvalAskers.Split(',');
                foreach (var user in users)
                {
                    if (user != "")
                        client.Send("approval," + user);
                }
            }
            else
            {
                SqlCommand timeCmd = new SqlCommand("SELECT Username FROM " + client.Username() + " WHERE Time = 1", connection);
                connection.Open();
                string timeAskers = "";
                SqlDataReader rdr1 = timeCmd.ExecuteReader();
                while (rdr1.Read())
                {
                    timeAskers += rdr1["Time"].ToString();
                }
                connection.Close();
                if (timeAskers != "")
                {
                    string[] users = timeAskers.Split(',');
                    foreach (var user in users)
                    {
                        client.Send(user + timeMessages);
                        SQL.RemoveTimeReq(user);

                    }
                }
            }



            SqlCommand URIcmd = new SqlCommand("SELECT URL, Username FROM " + client.Username() + " WHERE URL != ''", connection);
            connection.Open();
            string URL = "";
            string URIAskers = "";
            SqlDataReader rdr2 = URIcmd.ExecuteReader();
            while (rdr2.Read())
            {
                URL += "," + rdr2["URL"].ToString();
                URIAskers += rdr2["Username"].ToString();
            }
            connection.Close();
            if (URIAskers != "")
            {
                string[] usersURI = URIAskers.Split(',');
                foreach (var userURI in usersURI)
                {
                    if (userURI != "")
                    {
                        string[] urls = URL.Split(',');
                        foreach (var url in urls)
                        {
                            if (url != "")
                            {
                                client.Send(URIAskers + "," + url + ",agree?");
                                SQL.RemoveURLReq(URIAskers, url);
                            }
                        }
                    }


                }
            }


        }
        /// <summary>
        /// refreshes the history and sent to super visor
        /// gets a client so it van send and username to look for history
        /// </summary>
        /// <param name="client"></param>
        /// <param name="user"></param>
        public static void RefreshHistory(Client client,string user)
        {
            
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand("SELECT History FROM " + client.Username() + " WHERE Username = ',"+user+"'", connection);
            connection.Open();
            string history = "";
            SqlDataReader rdr0 = sqlCommand.ExecuteReader();
            while (rdr0.Read())
            {
                history += rdr0["History"].ToString();
            }
            connection.Close();
            client.Send(history);
        }
        /// <summary>
        /// checks if username and password match
        /// </summary>
        /// <param name="column"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool IsRealUser(string column, string username, string password)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlCommand cmd = new SqlCommand();
            var salted = new SqlCommand();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            salted.Connection = connection;
            salted.CommandText = "SELECT Salt FROM DataUsers WHERE " + column + " = '" + username + "'";
            string sql1 = "";
            SqlDataReader rdr = salted.ExecuteReader();
            while (rdr.Read())
            {
                sql1 += rdr["Salt"].ToString();
            }
            byte[] salt = Encoding.ASCII.GetBytes(sql1);



            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
           
            string hashed = Hash(password, salt);
            connection.Close();
            connection.Open();


            cmd.Connection = connection;
            cmd.CommandText = "SELECT Password FROM DataUsers WHERE " + column + " = '" + username + "'";
            SqlDataReader reader = cmd.ExecuteReader();
            string sql2 = "";



            while (reader.Read())
            {
                sql2 += reader["Password"].ToString();
            }
            connection.Close();


            return (sql2.Trim() == hashed);
        }
        /// <summary>
        /// gets salt and string and return the string hashed with the salt
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string Hash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            
        }
        /// <summary> 
        ///return salt where mail receive
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static string Salt(string mail)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Salt FROM DataUsers WHERE MailAddress = '" + mail + "'", connection);

            connection.Open();
            string superVisor = "";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                superVisor += rdr["Salt"].ToString();
            }
            connection.Close();
            return superVisor;
        }
        /// <summary>
        /// return password where mail receive
        /// </summary>
        /// <param name="mail"></param>
        /// <returns></returns>
        public static string Password(string mail)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("SELECT Password FROM DataUsers WHERE MailAddress = '" + mail + "'", connection);

            connection.Open();
            string superVisor = "";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                superVisor += rdr["Password"].ToString();
            }
            connection.Close();
            return superVisor;
        }
        /// <summary>
        /// adds a history so super visor can see
        /// gets username and history
        /// </summary>
        /// <param name="username"></param>
        /// <param name="history"></param>
        public static void HistoryAdd(string username, string history)
        {
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd;
            cmd = new SqlCommand("UPDATE "+SuperVisor(username)+" SET History = '" + history + "' WHERE Username = '," + username + "'", connection);
            connection.Open();
            SqlDataReader rdr2 = cmd.ExecuteReader();
            connection.Close();
        }

    }
}
