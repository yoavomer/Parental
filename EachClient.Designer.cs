namespace Client
{
    partial class EachClient
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
            this.clientName = new System.Windows.Forms.Label();
            this.UpdateFU = new System.Windows.Forms.Button();
            this.CancelCon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // clientName
            // 
            this.clientName.Font = new System.Drawing.Font("Showcard Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.clientName.Location = new System.Drawing.Point(323, 24);
            this.clientName.Name = "clientName";
            this.clientName.Size = new System.Drawing.Size(166, 49);
            this.clientName.TabIndex = 0;
            this.clientName.Text = "Client";
            // 
            // UpdateFU
            // 
            this.UpdateFU.Location = new System.Drawing.Point(158, 304);
            this.UpdateFU.Name = "UpdateFU";
            this.UpdateFU.Size = new System.Drawing.Size(112, 34);
            this.UpdateFU.TabIndex = 2;
            this.UpdateFU.Text = "UpdateFU";
            this.UpdateFU.UseVisualStyleBackColor = true;
            this.UpdateFU.Click += new System.EventHandler(this.UpdateFU_Click);
            // 
            // CancelCon
            // 
            this.CancelCon.Location = new System.Drawing.Point(342, 380);
            this.CancelCon.Name = "CancelCon";
            this.CancelCon.Size = new System.Drawing.Size(112, 34);
            this.CancelCon.TabIndex = 3;
            this.CancelCon.Text = "CancelCon";
            this.CancelCon.UseVisualStyleBackColor = true;
            this.CancelCon.Click += new System.EventHandler(this.CancelCon_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(158, 75);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "ForbiddenUrls";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(31, 103);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(370, 195);
            this.textBox1.TabIndex = 5;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(563, 380);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(112, 34);
            this.button1.TabIndex = 6;
            this.button1.Text = "add time";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(542, 343);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(150, 31);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(418, 103);
            this.textBox3.Multiline = true;
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(370, 195);
            this.textBox3.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(563, 304);
            this.button2.Name = "RefresHis";
            this.button2.Size = new System.Drawing.Size(112, 34);
            this.button2.TabIndex = 8;
            this.button2.Text = "RefreshHis";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // EachClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CancelCon);
            this.Controls.Add(this.UpdateFU);
            this.Controls.Add(this.clientName);
            this.Name = "EachClient";
            this.RightToLeftLayout = true;
            this.Text = "EachClient";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label clientName;
        private System.Windows.Forms.Button UpdateFU;
        private System.Windows.Forms.Button CancelCon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button2;
    }
}

