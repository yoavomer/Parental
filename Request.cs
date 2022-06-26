using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Client
{
    public partial class Request : Form
    {
        Client client;
        string URI;
        /// <summary>
        /// if it doesnt recevied uri knowing its time receiving client
        /// </summary>
        /// <param name="client"></param>
        public Request(Client client)
        {
            InitializeComponent();
            this.client = client;
            this.askSuper.Click += new
            System.EventHandler(this.Time_Click);
        }
        /// <summary>
        /// receiving client and uri and classify into uri request
        /// </summary>
        /// <param name="client"></param>
        /// <param name="URI"></param>
        public Request(Client client, string URI)
        {
            InitializeComponent();
            this.client = client;
            this.URI = URI;
            this.askSuper.Click += new
            System.EventHandler(this.Connect_this_site_Click);
        }
        /// <summary>
        /// send to ask geting this uri from supervisor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Connect_this_site_Click(object sender,
        EventArgs e)
        {
            client.Send(" web," + this.URI);
            this.Close();
        }
        /// <summary>
        /// ask for more time from supervisor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Time_Click(object sender, EventArgs e)
        {
            client.Send(" got ran out of time, do you want to add him some more time ? ");
            this.Close();
        }
    }
}