using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
namespace Client
{
    public partial class Response : Form
    {
        Client client;
        string username;
        string URI;
        /// <summary>
        ///constructor of the form, gets a client object
        ///and string to approve or disapprove
        ///checking in the string and classify it
        /// </summary>
        /// <param name="response"></param>
        /// <param name="client"></param>
        public Response(string response, Client client)
        {
            InitializeComponent();
            this.client = client;
            label1.Text = response;
            username = response.Split(',')[1];
            if (response.Contains("time"))
            {
                this.accept.Click += Time_Click;
                this.decline.Click += Time_Not_Click;
                this.accept.Click -= Accept_Click;
                this.Controls.Add(this.time_add);
                time_add.Text = "add here the amount";
            }
            else if (response.Contains("agree"))
            {
                URI = response.Split(',')[2];
                label1.Text = username + " wants to log to " + URI
                + " do you agree?";
                this.accept.Click += Connect_this_site_Click;
                this.decline.Click += Not_Connect_this_site_Click;
                this.accept.Click -= Accept_Click;
            }
            else if (response.Contains("approved"))
            {
                this.accept.Click += Accept_Click;
                this.decline.Click += Decline_Click;
            }
        }
        /// <summary>
        /// agreeing to get into specific uri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_this_site_Click(object sender,
        EventArgs e)
        {
            client.Send(username + "," + URI + ",agree");
            this.Close();
        }
        /// <summary>
        /// declining geting into specific uri
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Not_Connect_this_site_Click(object sender,
        EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// adds the time in the time text box to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_Click(object sender, EventArgs e)
        {
            client.Send(time_add.Text);
            this.Close();
        }
        /// <summary>
        /// declining give more time
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_Not_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// declint this username
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Decline_Click(object sender, EventArgs e)
        {
            client.Send(username + ",declined,decided");
            this.Close();
        }
        /// <summary>
        /// accapt this client as undervisor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Accept_Click(object sender, EventArgs e)
        {
            client.Send(username + ",approved,decided");
            this.Close();
            new Thread(new ThreadStart(EachStart)).Start();
        }
        /// <summary>
        /// if accaptence open a window to each client
        /// </summary>
        public void EachStart()
        {
            Application.Run(new EachClient(client, username));
        }
    }
}
