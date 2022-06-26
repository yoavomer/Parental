using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;
namespace Client
{
    public partial class Login : Form
    {
        public Client client;
        /// <summary>
        /// constructor gets a client object
        /// </summary>
        /// <param name="client"></param>
        public Login(Client client)
        {
            InitializeComponent();
            this.client = client;
            client.Send("capcha");
            CAPCHA.Text = client.Recv();
        }
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.usernametxb = new System.Windows.Forms.TextBox();
            this.passwordtxb = new System.Windows.Forms.TextBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.RegisterBTN = new System.Windows.Forms.Button();
            this.CAPCHA = new System.Windows.Forms.Label();
            this.capcha_label = new System.Windows.Forms.Label();
            this.capchaTxt = new System.Windows.Forms.TextBox();
            this.EmailRestore = new System.Windows.Forms.Button();
            this.SuspendLayout();
            //
            // label1
            //
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5,
            53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "username";
            //
            // label2
            //
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5,
            131);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "password";
            //
            // usernametxb
            //
            this.usernametxb.Location = new
            System.Drawing.Point(126, 53);
            this.usernametxb.Margin = new
            System.Windows.Forms.Padding(3, 5, 3, 5);
            this.usernametxb.Name = "usernametxb";
            this.usernametxb.Size = new System.Drawing.Size(124,
            31);
            this.usernametxb.TabIndex = 2;
            //
            // passwordtxb
            //
            this.passwordtxb.Location = new
            System.Drawing.Point(126, 131);
            this.passwordtxb.Margin = new
            System.Windows.Forms.Padding(3, 5, 3, 5);
            this.passwordtxb.Name = "passwordtxb";
            this.passwordtxb.PasswordChar = '*';
            this.passwordtxb.Size = new System.Drawing.Size(124,
            31);
            this.passwordtxb.TabIndex = 3;
            //
            // loginBtn
            //
            this.loginBtn.Location = new System.Drawing.Point(126,
            333);
            this.loginBtn.Margin = new
            System.Windows.Forms.Padding(3, 5, 3, 5);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(124, 31);
            this.loginBtn.TabIndex = 4;
            this.loginBtn.Text = "LOGIN";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new
            System.EventHandler(this.LoginBtn_Click);
            //
            // RegisterBTN
            //
            this.RegisterBTN.Location = new
            System.Drawing.Point(294, 333);
            this.RegisterBTN.Name = "RegisterBTN";
            this.RegisterBTN.Size = new System.Drawing.Size(124,
            31);
            this.RegisterBTN.TabIndex = 5;
            this.RegisterBTN.Text = "register";
            this.RegisterBTN.UseVisualStyleBackColor = true;
            this.RegisterBTN.Click += new
            System.EventHandler(this.RegisterBTN_Click);
            //
            // CAPCHA
            //
            this.CAPCHA.AutoSize = true;
            this.CAPCHA.ForeColor = System.Drawing.Color.Purple;
            this.CAPCHA.Location = new System.Drawing.Point(150,
            214);
            this.CAPCHA.Name = "CAPCHA";
            this.CAPCHA.Size = new System.Drawing.Size(81, 25);
            this.CAPCHA.TabIndex = 1;
            this.CAPCHA.Text = "CAPCHA";
            //
            // capcha_label
            //
            this.capcha_label.AutoSize = true;
            this.capcha_label.Location = new
            System.Drawing.Point(5, 262);
            this.capcha_label.Name = "capcha_label";
            this.capcha_label.Size = new System.Drawing.Size(67,
            25);
            this.capcha_label.TabIndex = 1;
            this.capcha_label.Text = "capcha";
            //
            // capchaTxt
            //
            this.capchaTxt.Location = new
            System.Drawing.Point(126, 262);
            this.capchaTxt.Margin = new
            System.Windows.Forms.Padding(3, 5, 3, 5);
            this.capchaTxt.Name = "capchaTxt";
            this.capchaTxt.Size = new System.Drawing.Size(124,
            31);
            this.capchaTxt.TabIndex = 3;
            //
            // EmailRestore
            //
            this.EmailRestore.Location = new
            System.Drawing.Point(466, 319);
            this.EmailRestore.Name = "EmailRestore";
            this.EmailRestore.Size = new System.Drawing.Size(124,
            59);
            this.EmailRestore.TabIndex = 6;
            this.EmailRestore.Text = "forgot password?";
            this.EmailRestore.UseVisualStyleBackColor = true;
            this.EmailRestore.Click += new
            System.EventHandler(this.EmailRestore_Click);
            //
            // Login
            //
            this.AutoScaleDimensions = new
            System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode =
            System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 702);
            this.Controls.Add(this.EmailRestore);
            this.Controls.Add(this.capchaTxt);
            this.Controls.Add(this.capcha_label);
            this.Controls.Add(this.CAPCHA);
            this.Controls.Add(this.RegisterBTN);
            this.Controls.Add(this.loginBtn);
            this.Controls.Add(this.passwordtxb);
            this.Controls.Add(this.usernametxb);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Location = new System.Drawing.Point(560, 330);
            this.Margin = new System.Windows.Forms.Padding(3, 5,
            3, 5);
            this.Name = "Login";
            this.Text = "Login";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox usernametxb;
        private System.Windows.Forms.TextBox passwordtxb;
        private Button RegisterBTN;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.TextBox EmailReg;
        private System.Windows.Forms.TextBox NameReg;
        private System.Windows.Forms.TextBox UsernameReg;
        private System.Windows.Forms.TextBox PasswordReg;
        private System.Windows.Forms.Label EmailLabel;
        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Label PasswordLabel;
        private System.Windows.Forms.TextBox ageReg;
        private System.Windows.Forms.Label ageLabel;
        private System.Windows.Forms.TextBox supervisorReg;
        private System.Windows.Forms.Label supervisorLabel;
        private Label CAPCHA;
        private Label capcha_label;
        private Button EmailRestore;
        private TextBox capchaTxt;
        /// <summary>
        /// sending to the sever the username and password
        /// recving and checking whether it is correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoginBtn_Click(object sender, EventArgs e)
        {
            Socket handler = new
            Socket(AddressFamily.InterNetwork, SocketType.Stream,
            ProtocolType.Tcp);
            string username = usernametxb.Text;
            string password = passwordtxb.Text;
            client.Send(capchaTxt.Text);
            if (client.Recv().Contains("true"))
            {
                client.Send("getin," + username + "," +
                password);
                string s = client.Recv();
                if (s.Contains("you'r in") ||
                s.Contains("congratulations"))
                {
                    //client.Send("Authentication,"+username);
                    loginBtn.Click -= LoginBtn_Click;
                    loginBtn.Click += EmailRestore_thierd_Click;
                    Controls.Remove(RegisterBTN);
                    Controls.Remove(EmailRestore);
                    Controls.Remove(label2);
                    Controls.Remove(passwordtxb);
                    label1.Text = "your email code";
                }
                else
                {
                    MessageBox.Show(s);
                }
            }
            else
            {
                MessageBox.Show("your captcha is incorrect");
                CAPCHA.Text = client.Recv();
            }
        }
        /// <summary>
        /// chenging the forms into login form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RegisterBTN_Click(object sender, EventArgs e)
        {
            this.EmailReg = new System.Windows.Forms.TextBox();
            this.NameReg = new System.Windows.Forms.TextBox();
            this.UsernameReg = new System.Windows.Forms.TextBox();
            this.PasswordReg = new System.Windows.Forms.TextBox();
            this.ageReg = new System.Windows.Forms.TextBox();
            this.supervisorReg = new
            System.Windows.Forms.TextBox();
            this.EmailReg.AutoSize = true;
            this.EmailReg.Location = new System.Drawing.Point(200,
            50);
            this.EmailReg.Name = "EmailReg";
            this.EmailReg.Size = new System.Drawing.Size(89, 25);
            this.EmailReg.TabIndex = 0;
            this.EmailReg.Text = "";
            this.Controls.Add(this.EmailReg);
            this.NameReg.AutoSize = true;
            this.NameReg.Location = new System.Drawing.Point(200,
            90);
            this.NameReg.Name = "NameReg";
            this.NameReg.Size = new System.Drawing.Size(89, 25);
            this.NameReg.TabIndex = 0;
            this.NameReg.Text = "";
            this.Controls.Add(this.NameReg);
            this.UsernameReg.AutoSize = true;
            this.UsernameReg.Location = new
            System.Drawing.Point(200, 130);
            this.UsernameReg.Name = "UsernameReg";
            this.UsernameReg.Size = new System.Drawing.Size(89,
            25);
            this.UsernameReg.TabIndex = 0;
            this.UsernameReg.Text = "";
            this.Controls.Add(this.UsernameReg);
            this.PasswordReg.AutoSize = true;
            this.PasswordReg.Location = new
            System.Drawing.Point(200, 170);
            this.PasswordReg.Name = "PasswordReg";
            this.PasswordReg.Size = new System.Drawing.Size(89,
            25);
            this.PasswordReg.TabIndex = 0;
            this.PasswordReg.Text = "";
            this.Controls.Add(this.PasswordReg);
            this.ageReg.AutoSize = true;
            this.ageReg.Location = new System.Drawing.Point(550,
            50);
            this.ageReg.Name = "ageReg";
            this.ageReg.Size = new System.Drawing.Size(89, 25);
            this.ageReg.TabIndex = 0;
            this.ageReg.Text = "";
            this.Controls.Add(this.ageReg);
            this.supervisorReg.AutoSize = true;
            this.supervisorReg.Location = new
            System.Drawing.Point(550, 90);
            this.supervisorReg.Name = "supervisorReg";
            this.supervisorReg.Size = new System.Drawing.Size(89,
            25);
            this.supervisorReg.TabIndex = 0;
            this.supervisorReg.Text = "";
            this.Controls.Add(this.supervisorReg);
            this.EmailLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.PasswordLabel = new System.Windows.Forms.Label();
            this.ageLabel = new System.Windows.Forms.Label();
            this.supervisorLabel = new
            System.Windows.Forms.Label();
            this.EmailLabel.AutoSize = true;
            this.EmailLabel.Location = new
            System.Drawing.Point(50, 50);
            this.EmailLabel.Name = "EmailLabel";
            this.EmailLabel.Size = new System.Drawing.Size(89,
            25);
            this.EmailLabel.TabIndex = 0;
            this.EmailLabel.Text = "Email";
            this.Controls.Add(this.EmailLabel);
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(50,
            90);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(89, 25);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Name";
            this.Controls.Add(this.NameLabel);
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new
            System.Drawing.Point(50, 130);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(89,
            25);
            this.UsernameLabel.TabIndex = 0;
            this.UsernameLabel.Text = "Username";
            this.Controls.Add(this.UsernameLabel);
            this.PasswordLabel.AutoSize = true;
            this.PasswordLabel.Location = new
            System.Drawing.Point(50, 170);
            this.PasswordLabel.Name = "PasswordLabel";
            this.PasswordLabel.Size = new System.Drawing.Size(89,
            25);
            this.PasswordLabel.TabIndex = 0;
            this.PasswordLabel.Text = "Password";
            this.Controls.Add(this.PasswordLabel);
            this.ageLabel.AutoSize = true;
            this.ageLabel.Location = new System.Drawing.Point(400,
            50);
            this.ageLabel.Name = "ageLabel";
            this.ageLabel.Size = new System.Drawing.Size(89, 25);
            this.ageLabel.TabIndex = 0;
            this.ageLabel.Text = "age";
            this.Controls.Add(this.ageLabel);
            this.supervisorLabel.AutoSize = true;
            this.supervisorLabel.Location = new
            System.Drawing.Point(400, 90);
            this.supervisorLabel.Name = "supervisorLabel";
            this.supervisorLabel.Size = new
            System.Drawing.Size(89, 25);
            this.supervisorLabel.TabIndex = 0;
            this.supervisorLabel.Text = "supervisor";
            this.Controls.Add(this.supervisorLabel);
            Controls.Remove(label1);
            Controls.Remove(label2);
            Controls.Remove(usernametxb);
            Controls.Remove(passwordtxb);
            Controls.Remove(loginBtn);
            Controls.Remove(EmailRestore);
            RegisterBTN.Click -= RegisterBTN_Click;
            RegisterBTN.Click += RegisterBTN_Second_Click;
        }
        /// <summary>
        /// sending to the sever the username, password, mail,
        ///name, age and supervisor if needs
        /// recving and checking whether it is correct
        /// checking captch
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RegisterBTN_Second_Click(object sender, EventArgs e)
        {
            client.Send(capchaTxt.Text);
            if (client.Recv().Contains("true"))
            {
                string username = UsernameReg.Text;
                string password = PasswordReg.Text;
                string name = NameReg.Text;
                string MailAddress = EmailReg.Text;
                string age = ageReg.Text;
                string supervisor = supervisorReg.Text;
                if (supervisor != null)
                    client.Send("Register," + username + "," +
                    password + "," + age + "," + MailAddress + "," + name + "," +
                    supervisor);
                else
                    client.Send("Register," + username + "," +
                    password + "," + age + "," + MailAddress + "," + name);
                string s = client.Recv();
                if (s.Contains("you'r in") ||
                s.Contains("congratulations"))
                {
                    this.Close();
                    new Thread(() => Application.Run(new
                    Form1(client))).Start();
                }
                else
                {
                    MessageBox.Show(s);
                }
            }
            else
            {
                MessageBox.Show("your captcha is incorrect");
                CAPCHA.Text = client.Recv();
            }
        }
        /// <summary>
        /// chenging the forms into email restore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailRestore_Click(object sender, EventArgs
        e)
        {
            label1.Text = "username";
            label2.Text = "email";
            EmailRestore.Click -= EmailRestore_Click;
            EmailRestore.Click += EmailRestore_second_Click;
            Controls.Remove(loginBtn);
            Controls.Remove(RegisterBTN);
        }
        /// <summary>
        ///changing the forms into receve code
        ///sending to server mail and username
        ///receving whether it is correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailRestore_second_Click(object sender,
        EventArgs e)
        {
            client.Send(capchaTxt.Text);
            if (client.Recv().Contains("true"))
            {
                client.Send("forgot," + usernametxb.Text + "," +
                passwordtxb.Text);
                string recv = client.Recv();
                if (recv.Contains("match"))
                {
                    MessageBox.Show(recv);
                }
                else
                {
                    Controls.Remove(label2);
                    Controls.Remove(passwordtxb);
                    label1.Text = "your email code";
                    EmailRestore.Click -=
                    UsernameForgot_second_Click;
                    EmailRestore.Click +=
                    UsernameForgot_thierd_Click;
                }
            }
            else
            {
                MessageBox.Show("your captcha is incorrect");
                CAPCHA.Text = client.Recv();
            }
        }
        /// <summary>
        /// receving code and sending it to server
        /// receving if its correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailRestore_thierd_Click(object sender,
        EventArgs e)
        {
            client.Send(usernametxb.Text);
            if (client.RecvCode().Contains("you'r"))
            {
                this.Close();
                new Thread(() => Application.Run(new
                Form1(client))).Start();
            }
            else
            {
                MessageBox.Show("ur code isn't correct");
            }
        }
        /// <summary>
        /// send the password and email to server
        /// chenging the forms into email restore
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsernameForgot_Click(object sender, EventArgs
        e)
        {
            label1.Text = "password";
            label2.Text = "email";
            EmailRestore.Click -= UsernameForgot_Click;
            EmailRestore.Click += UsernameForgot_second_Click;
            Controls.Remove(loginBtn);
            Controls.Remove(RegisterBTN);
        }
        /// <summary>
        ///chenging the forms into receve code
        ///sending to server mail and password
        ///receving whether it is correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsernameForgot_second_Click(object sender,
        EventArgs e)
        {
            client.Send(capchaTxt.Text);
            if (client.Recv().Contains("true"))
            {
                client.Send("forgot," + usernametxb.Text + "," +
                passwordtxb.Text);
                string recv = client.Recv();
                if (recv.Contains("match"))
                {
                    MessageBox.Show(recv);
                }
                else
                {
                    Controls.Remove(label2);
                    Controls.Remove(passwordtxb);
                    label1.Text = "your email code";
                    EmailRestore.Click -=
                    UsernameForgot_second_Click;
                    EmailRestore.Click +=
                    UsernameForgot_thierd_Click;
                }
            }
            else
            {
                MessageBox.Show("your captcha is incorrect");
                CAPCHA.Text = client.Recv();
            }
        }
        /// <summary>
        /// receving code and sending it to server
        /// receving if its correct
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UsernameForgot_thierd_Click(object sender,
        EventArgs e)
        {
            client.Send(usernametxb.Text);
            if (client.RecvCode().Contains("true"))
            {
                this.Close();
                new Thread(() => Application.Run(new
                Form1(client))).Start();
            }
            else
            {
                MessageBox.Show("ur code isn't correct");
            }
        }
    }
}
