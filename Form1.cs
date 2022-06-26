using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Data.SqlClient;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Net.Mail;

namespace Server
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// constructor start listening
        /// </summary>
        public Form1()
        {
            StartListening();
        }
        /// <summary>
        /// Bind the socket to the local ip and listen for incoming connections.
        /// </summary>
        private static void StartListening()
        {
            //IPAddress localAddress = IPAddress.Parse("10.67.201.75");
            IPAddress localAddress = IPAddress.Parse("127.0.0.1");
            TcpListener server = new TcpListener(localAddress, 1234);
            server.Start();
            while (true)
            {
                Console.WriteLine("Waiting for a connection...");
                NetworkStream clientsStream = new NetworkStream(server.AcceptSocket());
                Encryption encryption = new Encryption(clientsStream);
                Req req = new Req(DateTime.Now, clientsStream, "ping");
                if (req.Possible())
                {
                    new Thread(() => RecvLogin(clientsStream, encryption)).Start();
                }  
            }
        }
        /// <summary>
        /// sending data before creating a client
        /// </summary>
        /// <param name="clientsStream"></param>
        /// <param name="data"></param>
        /// <param name="encryption"></param>
        private static void Send(NetworkStream clientsStream, string data,Encryption encryption)
        {
            byte[] byteData;
            data = encryption.Decrypte(data);
            string s = data.Length.ToString();
            string Send = s.PadLeft(10, '0') + data;
            byteData = Encoding.ASCII.GetBytes(Send);
            clientsStream.Write(byteData, 0, byteData.Length);

        }
        /// <summary>
        /// recv login so no client will be able to pass the login
        /// gets networkstream to specific client and encrtyption
        /// </summary>
        /// <param name="clientsStream"></param>
        /// <param name="encryption"></param>
        private static void RecvLogin(NetworkStream clientsStream,Encryption encryption)
        {
            Req req = new Req(DateTime.Now, clientsStream, "login");
            if (req.Possible())
            {
                string s = ReceivingLogin(clientsStream, encryption);

                if (s.Contains("Restored"))
                {
                    string[] words = s.Split(',');
                    string SendBack = Restore(s);
                    if (SendBack.Contains("doesnt"))
                    {
                        Send(clientsStream, SendBack, encryption);
                        RecvLogin(clientsStream, encryption);
                    }
                    else
                    {
                        Send(clientsStream, "You have recived an email", encryption);
                        RecvEmailCode(int.Parse(SendBack), clientsStream, encryption, words[1]);
                    }

                }
                else if (s.Contains("forgot"))
                {
                    string[] words = s.Split(',');
                    string SendBack = ForgotUser(s);
                    if (SendBack.Contains("doesnt"))
                    {
                        Send(clientsStream, SendBack, encryption);
                        RecvLogin(clientsStream, encryption);
                    }
                    else
                    {
                        Send(clientsStream, "You have recived an email", encryption);
                        RecvEmailCode(int.Parse(SendBack), clientsStream, encryption, words[1]);
                    }

                }
                else if (s.Contains("getin"))
                {
                    string SendBack = Geting(s);
                    Send(clientsStream, SendBack, encryption);
                    if (!SendBack.Contains("username"))
                    {
                        string[] words = s.Split(',');
                        string username = words[1];
                        Client client = new Client(clientsStream, username, encryption);
                        Random random = new Random();
                        int rnd = random.Next(100, 999);
                        Email(SQL.Mail(username), rnd);
                        RecvEmailCode(rnd, clientsStream, encryption, username);




                        Receiver(client);
                    }
                    else
                        RecvLogin(clientsStream, encryption);
                }
                else if (s.Contains("capcha"))
                {
                    string SendBack = Captcha();
                    Send(clientsStream, SendBack, encryption);
                    if (!RecvCaptcha(SendBack, clientsStream, encryption))
                    {
                        return;
                    }
                    RecvLogin(clientsStream, encryption);
                }
                else if (s.Contains("Register"))
                {
                    string SendBack = Register(s);
                    Send(clientsStream, SendBack, encryption);
                    if (SendBack.Contains("congratulations"))
                    {
                        string[] words = s.Split(',');
                        string username = words[1];
                        int age = int.Parse(words[3]);
                        Client client = new Client(clientsStream, username, age, encryption);
                        SQL.AddUser(client);
                        Receiver(client);
                    }
                    else
                        RecvLogin(clientsStream, encryption);
                }
                else
                {
                    RecvLogin(clientsStream, encryption);
                }
            }
           
        }
        /// <summary>
        /// recv from client string and checks whether its a real user
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        private static string Geting(string rec)
        {
            string[] words = rec.Split(',');
            string username = words[1];
            string password = words[2];
            Random rnd = new Random();

            if (SQL.IsRealUser("Username", username, password))
            {
                return "congratulations you'r in";
                
            }
            else
            {
                return "your username or password are incorrect";
            }
        }
        /// <summary>
        /// a new user wants to register, checks whether the username and mail dont exist
        /// and if needs to the supervisor correctency
        /// insert into data base with hashed password and sql injection check
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        private static string Register(string rec)
        {
            string[] words = rec.Split(',');
            string username = words[1];
            string password = words[2];
            int age = int.Parse(words[3]);
            string email = words[4];
            string name = words[5];
            string Supervisor = words[6];

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);


            // generate a 8-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }



            var s = Encoding.ASCII.GetString(salt);
            salt = Encoding.ASCII.GetBytes(s);
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            string SupervisorAge = "";
            
            if (Supervisor != "")
            {
                SQL.TryApprove(username, Supervisor);
                if (SupervisorApproval(Supervisor,username))
                {
                    connection.Open();
                    SqlCommand CheckSupervisor = new SqlCommand();
                    CheckSupervisor.Connection = connection;

                    CheckSupervisor.CommandText = "SELECT Age FROM DataUsers WHERE username = '" + Supervisor+"'";
                    SqlDataReader reader = CheckSupervisor.ExecuteReader();

                    while (reader.Read())
                    {
                        SupervisorAge = reader["Age"].ToString();
                    }
                    connection.Close();
                }
                else
                {
                    return "your supervisor isnt good";
                }
                
            }
            else
            {
                SupervisorAge = "0";
            }
            if (SQL.Check("username", username))
            {
                return "your username is already taken";
            }
            else if (SQL.Check("MailAddress", email))
            {
                return "your email is alredy taken";
            }
            else if (!email.Contains("@")||(!email.Contains(".")))
            {
                return "your email isnt an email";
            }
            else if (age < 18 && (int.Parse(SupervisorAge) < 18))
            {
                SQL.Approved(username + ",approved,decided", Supervisor);
                return "your supervisor isnt good";

            }
            else
            {
                
                SqlCommand add = new SqlCommand();
                add.Connection = connection;
                add.CommandText = "INSERT INTO DataUsers(Username, Password, MailAddress, Age, Supervisor,FullName,Salt) " +
                    " VALUES(@username, '" + hashedPassword + "', @email, " + age + ",@Supervisor, @name,'"+ s + "') ";
                add.Parameters.AddWithValue("@username", username);
                add.Parameters.AddWithValue("@name", name);
                add.Parameters.AddWithValue("@email", email);
                add.Parameters.AddWithValue("@Supervisor", Supervisor); 
                

                
                connection.Open();
                add.ExecuteNonQuery();
                connection.Close();
                return "congratulations you got new user";
                
            }



        }
        /// <summary>
        /// checks whether the mail match the username
        /// send mail code
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        private static string Restore(string rec)
        {
            string[] words = rec.Split(',');
            string username = words[1];
            string mail = words[2];
            Random random = new Random();
            int rnd = random.Next(100, 999);


            string MailAddress = SQL.Mail(username);
            if (MailAddress.Trim()==mail)
            {
                Email(mail, rnd);
                return rnd.ToString();
            }
            else
            {
                return "ur mail doesnt match your username";
            }

        }
        /// <summary>
        /// send code to mail
        /// gets the code and the destination email
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="rnd"></param>
        private static void Email(string destination, int rnd)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("yoavcyberr@gmail.com");
            message.To.Add(new MailAddress(destination));
            message.Subject = "Test";  
            message.Body = rnd.ToString();
            smtp.Port = 587;
            smtp.Host = "smtp.gmail.com"; //for gmail host  
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential("yoavcyberr@gmail.com", "yoav200410");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            

            try
            {
                smtp.Send(message);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                    ex.ToString());
            }


        }
        /// <summary>
        /// checks if the code sent to mail correct
        /// gets the code, the stream to recv the code the encryption and username
        /// </summary>
        /// <param name="code"></param>
        /// <param name="clientsStream"></param>
        /// <param name="encryption"></param>
        /// <param name="username"></param>
        private static void RecvEmailCode(int code, NetworkStream clientsStream,Encryption encryption,string username)
        {
            string s = ReceivingLogin(clientsStream, encryption);
            
            int codeFromC = int.Parse(s.Split(',')[0]);
            if (codeFromC == code)
            {
                Client client = new Client(clientsStream, username ,encryption);
                client.Send("you'r in");
                Receiver(client);
                
            }
            Send(clientsStream, "falseCode",encryption);
            RecvEmailCode(code,clientsStream,encryption,username);

        }
        /// <summary>
        /// receive string from client and activate the correct operation
        /// depends on the string
        /// </summary>
        /// <param name="client"></param>
        private static void Receiver(Client client)
        {
            Req req = new Req(DateTime.Now, client.clientStream(), "receive");
            if (req.Possible())
            {
                string s = client.Recv();
                if (s.Contains("SendPer"))
                {
                    client.Send(client.Per());
                }
                if (s.Contains("web"))
                {
                    string URI = s.Split(',')[1];
                    string message = client.Username() + "^" + URI;
                    string supervisor = SQL.SuperVisor(client.Username());
                    SQL.AddWebMessage(supervisor, message);
                }
                if (s.Contains("time"))
                {
                    string clientuser = client.Username();
                    string supervisor = SQL.SuperVisor(clientuser);
                    SQL.AddTimeMessage(supervisor, clientuser);
                }
                if (s.Contains("Return Urls"))
                {
                    client.Send(SQL.MyUrls(client.Username()));
                }
                if (s.Contains("UpdateFU"))
                {
                    string[] URLS = s.Split(',');
                    SQL.AddUrls(URLS[0], URLS[2]);
                }
                if (s.Contains("Stop"))
                {
                    SQL.AddUrls(s.Split(',')[1], "");

                }
                if (s.Contains("decided"))
                {
                    SQL.Approved(s, client.Username());
                }
                if (s.Contains("RefreshReq"))
                {
                    SQL.CheckMessages(client);
                }
                if (s.Contains("RefreshHis"))
                {
                    SQL.RefreshHistory(client, s.Split(",")[1]);
                }
                if (s.Contains("agree"))
                {
                    SQL.RemoveURL(s.Split(',')[0], s.Split(',')[1]);
                }
                if (s.Contains("addt"))
                {
                    SQL.AddTime(s.Split(',')[2], s.Split(',')[1]);
                }
                if (s.Contains("returnt"))
                {
                    client.Send(SQL.Time(client.Username()).Trim());
                }
                if (s.Contains("history"))
                {
                    SQL.HistoryAdd(client.Username(), s.Split(".")[1]);
                }
                if (s.Contains("sendClients"))
                {
                    foreach (var Undervisor in SQL.UnderVisors(client.Username()))
                    {
                        if (Undervisor != "")
                        {
                            client.Send("EachClient," + Undervisor);
                        }

                    }

                }
            }

            Receiver(client);

        }
        /// <summary>
        /// checks if the supervisor approved already his undervisor request
        /// </summary>
        /// <param name="supervisor"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        private static bool SupervisorApproval(string supervisor, string username)
        {
            string answer = SQL.AprovalCheck(username, supervisor);
            if (answer.Contains('0'))
            {
                return false;
            }

            return true;

        }
        /// <summary>
        /// checks whether the mail match the username
        /// send mail code
        /// </summary>
        /// <param name="rec"></param>
        /// <returns></returns>
        private static string ForgotUser(string rec)
        {
            string[] words = rec.Split(',');
            string password = words[1];
            string mail = words[2];
            Random random = new Random();
            int rnd = random.Next(100, 999);


            string salt = SQL.Salt(mail);

            byte[] bytesalt = Encoding.ASCII.GetBytes(salt);

            if (SQL.Hash(password,bytesalt) == SQL.Password(mail).Trim())
            {
                Email(mail, rnd);
                return rnd.ToString();
            }
            else
            {
                return "ur mail doesnt match your password";
            }

        }
        /// <summary>
        /// making a random string in purple color
        /// to verify a human 
        /// </summary>
        /// <returns></returns>
        private static string Captcha()
        {
            Random rnd = new Random();
            String capcha = "";
            int total = 0;
            do
            {
                int chr = rnd.Next(48, 123);
                if ((chr >= 48 && chr <= 57) || (chr >= 65 && chr <= 90) || (chr >= 97 && chr <= 122))
                {
                    capcha = capcha + (char)chr;
                }
                total++;
                if (total == 7)
                    break;

            } while (true);
            return capcha;
        }
        /// <summary>
        /// receiving a captcha code from client (client stream)
        /// decrypting it and compering to captcha code
        /// </summary>
        /// <param name="captcha"></param>
        /// <param name="clientsStream"></param>
        /// <param name="encryption"></param>
        /// <returns></returns>
        private static bool RecvCaptcha(string captcha, NetworkStream clientsStream, Encryption encryption)
        {
            string s = ReceivingLogin(clientsStream, encryption);
            if (s== captcha)
            {
                Send(clientsStream, "true", encryption);
                
                return true;
                

                
            }
            string NEWcaptcha = Captcha();
            Send(clientsStream, NEWcaptcha, encryption);
            RecvCaptcha(NEWcaptcha, clientsStream, encryption);
            return false;

        }
        /// <summary>
        /// receiving data from client before making him a client
        /// receiving clientstream and encryption to decrtypt the data
        /// </summary>
        /// <param name="clientsStream"></param>
        /// <param name="encryption"></param>
        /// <returns></returns>
        private static string ReceivingLogin(NetworkStream clientsStream, Encryption encryption)
        {
            byte[] recved = new byte[10];
            clientsStream.Read(recved, 0, 10);
            string str = System.Text.Encoding.Default.GetString(recved);
            int length = int.Parse(str);
            byte[] massage = null;
            byte[] byteMassage = new byte[length];

            for (int i = 0; i < length; length = length - 16)
            {
                massage = new byte[16];
                if (length < 16)
                {
                    massage = new byte[length];
                }
                clientsStream.Read(massage, 0, massage.Length);
                massage.CopyTo(byteMassage, byteMassage.Length - length);
            }

            return System.Text.Encoding.Default.GetString(encryption.Encrypte(byteMassage));
        }

    }
}
