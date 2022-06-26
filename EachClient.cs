using System;
using System.Windows.Forms;

namespace Client
{
    public partial class EachClient : Form
    {
        Client client;
        /// <summary>
        /// starting an each client window for each client
        /// </summary>
        /// <param name="client"></param>
        /// <param name="clientName"></param>
        public EachClient(Client client, string clientName)
        {
            InitializeComponent();
            this.client = client;
            this.clientName.Text = clientName;
        }
        /// <summary>
        /// update username forbidden uris according to text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateFU_Click(object sender, EventArgs e)
        {
            client.Send(clientName.Text + ",UpdateFU," + textBox1.Text);
        }
        /// <summary>
        /// cancaling all the client connection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCon_Click(object sender, EventArgs e)
        {
            client.Send("stop," + clientName.Text + ",connction");
        }
        /// <summary>
        /// sending add time and the text box text
        /// adding time to the client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            client.Send("addt," + textBox2.Text + "," + clientName.Text);
        }
        /// <summary>
        /// send to server to return the history of specific client
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            client.Send("RefreshHis," + clientName.Text);
            textBox3.Text = client.Recv();
        }
    }
}

