namespace Server
{
    partial class frm_Server
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblHint = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlIcon = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.lblUsername = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlEmoji = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.ptbEmoji = new System.Windows.Forms.PictureBox();
            this.ptbFile = new System.Windows.Forms.PictureBox();
            this.ptbImage = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pnlLeft.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.pnlIcon.SuspendLayout();
            this.pnlContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptbEmoji)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.pnlLeft.Controls.Add(this.pnlSidebar);
            this.pnlLeft.Controls.Add(this.pnlLogo);
            this.pnlLeft.Controls.Add(this.label5);
            this.pnlLeft.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(270, 599);
            this.pnlLeft.TabIndex = 0;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.White;
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 61);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(269, 538);
            this.pnlSidebar.TabIndex = 4;
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.pnlLogo.Controls.Add(this.label2);
            this.pnlLogo.Controls.Add(this.pictureBox1);
            this.pnlLogo.Controls.Add(this.label1);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(269, 61);
            this.pnlLogo.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(58, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 32);
            this.label2.TabIndex = 3;
            this.label2.Text = "Messenger";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(225)))));
            this.label1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label1.Location = new System.Drawing.Point(0, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(269, 1);
            this.label1.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(225)))));
            this.label5.Dock = System.Windows.Forms.DockStyle.Right;
            this.label5.Location = new System.Drawing.Point(269, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(1, 599);
            this.label5.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(270, 519);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(15);
            this.panel2.Size = new System.Drawing.Size(602, 80);
            this.panel2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblHint);
            this.panel1.Controls.Add(this.txtMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(15, 15);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.panel1.Size = new System.Drawing.Size(497, 50);
            this.panel1.TabIndex = 0;
            // 
            // lblHint
            // 
            this.lblHint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHint.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHint.ForeColor = System.Drawing.SystemColors.GrayText;
            this.lblHint.Location = new System.Drawing.Point(0, 0);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(482, 50);
            this.lblHint.TabIndex = 4;
            this.lblHint.Text = "Nhập tin nhắn";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblHint.Click += new System.EventHandler(this.lblHint_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtMessage.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMessage.Location = new System.Drawing.Point(0, 0);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(482, 50);
            this.txtMessage.TabIndex = 3;
            this.txtMessage.Visible = false;
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(512, 15);
            this.btnSend.Margin = new System.Windows.Forms.Padding(10, 3, 3, 3);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 50);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "GỬI";
            this.btnSend.UseVisualStyleBackColor = false;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // pnlIcon
            // 
            this.pnlIcon.BackColor = System.Drawing.Color.White;
            this.pnlIcon.Controls.Add(this.ptbEmoji);
            this.pnlIcon.Controls.Add(this.ptbFile);
            this.pnlIcon.Controls.Add(this.ptbImage);
            this.pnlIcon.Controls.Add(this.label4);
            this.pnlIcon.Controls.Add(this.label3);
            this.pnlIcon.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlIcon.Location = new System.Drawing.Point(270, 473);
            this.pnlIcon.Name = "pnlIcon";
            this.pnlIcon.Size = new System.Drawing.Size(602, 46);
            this.pnlIcon.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(225)))));
            this.label4.Dock = System.Windows.Forms.DockStyle.Top;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(602, 1);
            this.label4.TabIndex = 1;
            this.label4.Text = "label4";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(225)))));
            this.label3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.label3.Location = new System.Drawing.Point(0, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(602, 1);
            this.label3.TabIndex = 0;
            this.label3.Text = "label3";
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png";
            this.openFileDialog.InitialDirectory = "d:\\\\";
            this.openFileDialog.Title = "Chọn ảnh";
            // 
            // lblUsername
            // 
            this.lblUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(255)))));
            this.lblUsername.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblUsername.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsername.ForeColor = System.Drawing.Color.White;
            this.lblUsername.Location = new System.Drawing.Point(270, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.lblUsername.Size = new System.Drawing.Size(602, 60);
            this.lblUsername.TabIndex = 0;
            this.lblUsername.Text = "Phan Minh Hung";
            this.lblUsername.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlContent
            // 
            this.pnlContent.AutoScroll = true;
            this.pnlContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(143)))), ((int)(((byte)(147)))));
            this.pnlContent.Controls.Add(this.pnlEmoji);
            this.pnlContent.Controls.Add(this.pnlMain);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(270, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Size = new System.Drawing.Size(602, 413);
            this.pnlContent.TabIndex = 3;
            this.pnlContent.TabStop = true;
            // 
            // pnlEmoji
            // 
            this.pnlEmoji.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.pnlEmoji.AutoScroll = true;
            this.pnlEmoji.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(240)))), ((int)(((byte)(241)))));
            this.pnlEmoji.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlEmoji.Location = new System.Drawing.Point(90, 163);
            this.pnlEmoji.Name = "pnlEmoji";
            this.pnlEmoji.Padding = new System.Windows.Forms.Padding(10);
            this.pnlEmoji.Size = new System.Drawing.Size(215, 250);
            this.pnlEmoji.TabIndex = 0;
            this.pnlEmoji.Visible = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.White;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(602, 413);
            this.pnlMain.TabIndex = 1;
            // 
            // ptbEmoji
            // 
            this.ptbEmoji.Image = global::Server.Properties.Resources.ic_emoji;
            this.ptbEmoji.Location = new System.Drawing.Point(90, 11);
            this.ptbEmoji.Name = "ptbEmoji";
            this.ptbEmoji.Size = new System.Drawing.Size(25, 25);
            this.ptbEmoji.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbEmoji.TabIndex = 3;
            this.ptbEmoji.TabStop = false;
            this.ptbEmoji.Click += new System.EventHandler(this.ptbEmoji_Click);
            // 
            // ptbFile
            // 
            this.ptbFile.Image = global::Server.Properties.Resources.ic_file;
            this.ptbFile.Location = new System.Drawing.Point(50, 11);
            this.ptbFile.Name = "ptbFile";
            this.ptbFile.Size = new System.Drawing.Size(25, 25);
            this.ptbFile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbFile.TabIndex = 2;
            this.ptbFile.TabStop = false;
            this.ptbFile.Click += new System.EventHandler(this.ptbFile_Click);
            // 
            // ptbImage
            // 
            this.ptbImage.Image = global::Server.Properties.Resources.ic_picture;
            this.ptbImage.Location = new System.Drawing.Point(10, 11);
            this.ptbImage.Name = "ptbImage";
            this.ptbImage.Size = new System.Drawing.Size(25, 25);
            this.ptbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ptbImage.TabIndex = 0;
            this.ptbImage.TabStop = false;
            this.ptbImage.Click += new System.EventHandler(this.ptbImage_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Server.Properties.Resources.ic_chat;
            this.pictureBox1.Location = new System.Drawing.Point(12, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(40, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // frm_Server
            // 
            this.AcceptButton = this.btnSend;
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(872, 599);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.lblUsername);
            this.Controls.Add(this.pnlIcon);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlLeft);
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "frm_Server";
            this.Text = "Server";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Server_FormClosing);
            this.Load += new System.EventHandler(this.frm_Server_Load);
            this.SizeChanged += new System.EventHandler(this.frm_Server_SizeChanged);
            this.pnlLeft.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlIcon.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptbEmoji)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ptbImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnlIcon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox ptbImage;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label lblUsername;
        private System.Windows.Forms.Panel pnlContent;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.FlowLayoutPanel pnlEmoji;
        private System.Windows.Forms.PictureBox ptbFile;
        private System.Windows.Forms.PictureBox ptbEmoji;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Label label5;
    }
}

