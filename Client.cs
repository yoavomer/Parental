using System.Text;
using System.Net.Sockets;
using System.Data.SqlClient;
using System;
using System.Threading;

namespace Server
{
    public class Client
    {
        NetworkStream clientsStream;
        string username;
        string per;
        Encryption encryption;
        /// <summary>
        /// receiving client stream name age and encryption object
        /// creating client object much more convenient
        /// checking age and seprerate to 4 groups
        /// </summary>
        /// <param name="clientsStream"></param>
        /// <param name="username"></param>
        /// <param name="age"></param>
        /// <param name="encryption"></param>
        public Client(NetworkStream clientsStream, string username, int age, Encryption encryption)
        {            
            this.clientsStream = clientsStream;
            this.username = username;
            switch (age)
            {
                case < 2:
                    this.per = "baby";
                    break;
                case < 5:
                    this.per = "little kid";
                    break;
                case < 12:
                    this.per = "kid";
                    break;
                case < 18:
                    this.per = "teen";
                    break;
                default:
                    this.per = "adult";
                    break;
            }
            this.encryption = encryption;
            new Thread(new ThreadStart(MidNight)).Start();
        }
        /// <summary>
        /// receiving client stream name and encryption object
        /// unlike the first consturctor this one goes to the data
        /// base himself to receive age
        /// creating client object much more convenient
        /// checking age and seprerate to 4 groups
        /// </summary>
        /// <param name="clientStream"></param>
        /// <param name="username"></param>
        /// <param name="encryption"></param>
        public Client(NetworkStream clientStream, string username, Encryption encryption)
        {
            
            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\יואב\Documents\fireWallDataBase.mdf;Integrated Security=True;Connect Timeout=30";
            SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = connection;
            cmd.CommandText = "SELECT Age FROM DataUsers WHERE Username = '" + username + "'";
            connection.Open();
            string sql = "";
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                sql += rdr["Age"].ToString();
            }
            connection.Close();
            int age = int.Parse(sql);
            this.clientsStream = clientStream;
            this.username = username;
            switch (age)
            {
                case < 2:
                    this.per = "baby";
                    break;
                case < 5:
                    this.per = "little kid";
                    break;
                case < 12:
                    this.per = "kid";
                    break;
                case < 18:
                    this.per = "teen";
                    break;
                default:
                    this.per = "adult";
                    break;
            }
            this.encryption = encryption; 
            new Thread(new ThreadStart(MidNight)).Start();
        }
        /// <summary>
        /// returning client's stream
        /// </summary>
        /// <returns></returns>
        public NetworkStream clientStream()
        {
            return this.clientsStream;
        }
        /// <summary>
        /// returning client's permision
        /// </summary>
        /// <returns></returns>
        public string Per()
        {
            return this.per;
        }
        /// <summary>
        /// returning username
        /// </summary>
        /// <returns></returns>
        public string Username()
        {
            return this.username;
        }
        /// <summary>
        /// receieve string data encrypting it and sending to the clients
        /// </summary>
        /// <param name="data"></param>
        public void Send(string data)
        {
            byte[] encryptbytedata = encryption.Encrypte(Encoding.ASCII.GetBytes(data));
            data = System.Text.Encoding.Default.GetString(encryptbytedata);
            string s = data.Length.ToString();
            string send = s.PadLeft(10, '0') + data;
            byte[] byteData = Encoding.ASCII.GetBytes(send);
            this.clientsStream.Write(byteData, 0, byteData.Length);

        }
        /// <summary>
        /// receving from client
        /// and decrypting it
        /// </summary>
        /// <returns></returns>
        public string Recv()
        {
            byte[] recved = new byte[10];

            this.clientStream().Read(recved, 0, 10);
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
                this.clientStream().Read(massage, 0, massage.Length);
                massage.CopyTo(byteMassage, byteMassage.Length - length);

            }
            string s = System.Text.Encoding.Default.GetString(encryption.Encrypte(byteMassage));
            return s;



        }
        /// <summary>
        /// at 00:00 clear the time messages
        /// </summary>
        public void MidNight()
        {
            while (true)
            {
                var isMid = DateTime.Now.Hour;
                if (isMid == 0)
                {
                    SQL.CleanTime(Username());
                }
            }
        }

    }
}
