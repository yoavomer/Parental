namespace Client
{
    partial class Response
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.accept = new System.Windows.Forms.Button();
            this.moreTime = new System.Windows.Forms.Button();
            this.connect_this_site = new System.Windows.Forms.Button();
            this.decline = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.time_add = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // accept
            // 
            this.accept.Location = new System.Drawing.Point(144, 232);
            this.accept.Name = "accept";
            this.accept.Size = new System.Drawing.Size(133, 81);
            this.accept.TabIndex = 0;
            this.accept.Text = "accept";
            this.accept.UseVisualStyleBackColor = true;
            this.accept.Click += new System.EventHandler(this.Accept_Click);
            // 
            // moreTime
            // 
            this.moreTime.Location = new System.Drawing.Point(400, 200);
            this.moreTime.Name = "moreTime";
            this.moreTime.Size = new System.Drawing.Size(200, 70);
            this.moreTime.TabIndex = 0;
            this.moreTime.Text = "more time?";
            this.moreTime.Click += new System.EventHandler(this.Time_Click);
            // 
            // connect_this_site
            // 
            this.connect_this_site.Location = new System.Drawing.Point(150, 200);
            this.connect_this_site.Name = "connect_this_site";
            this.connect_this_site.Size = new System.Drawing.Size(200, 70);
            this.connect_this_site.TabIndex = 0;
            this.connect_this_site.Text = "want to connect this site?";
            this.connect_this_site.UseVisualStyleBackColor = true;
            this.connect_this_site.Click += new System.EventHandler(this.Connect_this_site_Click);
            // 
            // decline
            // 
            this.decline.Location = new System.Drawing.Point(465, 232);
            this.decline.Name = "decline";
            this.decline.Size = new System.Drawing.Size(133, 81);
            this.decline.TabIndex = 0;
            this.decline.Text = "decline";
            this.decline.UseVisualStyleBackColor = true;
            this.decline.Click += new System.EventHandler(this.Decline_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(338, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // time_add
            // 
            this.time_add.Location = new System.Drawing.Point(144, 180);
            this.time_add.Name = "time_add";
            this.time_add.Size = new System.Drawing.Size(133, 31);
            this.time_add.TabIndex = 2;
            // 
            // Response
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.decline);
            this.Controls.Add(this.accept);
            this.Name = "Response";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button accept;
        private System.Windows.Forms.Button moreTime;
        private System.Windows.Forms.Button connect_this_site;
        private System.Windows.Forms.Button decline;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox time_add;
    }
}

