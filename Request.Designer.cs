namespace Client
{
    partial class Request
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
            this.askSuper = new System.Windows.Forms.Button();
            this.moreTime = new System.Windows.Forms.Button();
            this.connect_this_site = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // askSuper
            // 
            this.askSuper.Location = new System.Drawing.Point(280, 120);
            this.askSuper.Name = "askSuper";
            this.askSuper.Size = new System.Drawing.Size(200, 86);
            this.askSuper.TabIndex = 0;
            this.askSuper.Text = "wanna ask your supervisor if you can use this site?";
            this.askSuper.UseVisualStyleBackColor = true;
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
            // Request
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.askSuper);
            this.Name = "Request";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button askSuper;
        private System.Windows.Forms.Button moreTime;
        private System.Windows.Forms.Button connect_this_site;
    }
}

