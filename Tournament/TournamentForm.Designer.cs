namespace Tournament
{
    partial class TournamentForm
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartTournament = new System.Windows.Forms.Button();
            this.lstbxAvailableAi = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rtxtStrategyDescription = new System.Windows.Forms.RichTextBox();
            this.lblStrategyDescription = new System.Windows.Forms.Label();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.lblNickname = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnUnloadFromGame = new System.Windows.Forms.Button();
            this.btnLoadToGame = new System.Windows.Forms.Button();
            this.lstbxLoadedAi = new System.Windows.Forms.ListBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartTournament
            // 
            this.btnStartTournament.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnStartTournament.Location = new System.Drawing.Point(0, 290);
            this.btnStartTournament.Name = "btnStartTournament";
            this.btnStartTournament.Size = new System.Drawing.Size(1200, 51);
            this.btnStartTournament.TabIndex = 1;
            this.btnStartTournament.Text = "Start tournament";
            this.btnStartTournament.UseVisualStyleBackColor = true;
            this.btnStartTournament.Click += new System.EventHandler(this.btnStartTournament_Click);
            // 
            // lstbxAvailableAi
            // 
            this.lstbxAvailableAi.FormattingEnabled = true;
            this.lstbxAvailableAi.ItemHeight = 16;
            this.lstbxAvailableAi.Location = new System.Drawing.Point(12, 12);
            this.lstbxAvailableAi.Name = "lstbxAvailableAi";
            this.lstbxAvailableAi.Size = new System.Drawing.Size(420, 276);
            this.lstbxAvailableAi.TabIndex = 2;
            this.lstbxAvailableAi.SelectedIndexChanged += new System.EventHandler(this.lstbxAvailableAi_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rtxtStrategyDescription);
            this.panel1.Controls.Add(this.lblStrategyDescription);
            this.panel1.Controls.Add(this.txtNickname);
            this.panel1.Controls.Add(this.lblNickname);
            this.panel1.Location = new System.Drawing.Point(438, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(202, 276);
            this.panel1.TabIndex = 3;
            // 
            // rtxtStrategyDescription
            // 
            this.rtxtStrategyDescription.Enabled = false;
            this.rtxtStrategyDescription.Location = new System.Drawing.Point(3, 91);
            this.rtxtStrategyDescription.Name = "rtxtStrategyDescription";
            this.rtxtStrategyDescription.ReadOnly = true;
            this.rtxtStrategyDescription.Size = new System.Drawing.Size(196, 181);
            this.rtxtStrategyDescription.TabIndex = 3;
            this.rtxtStrategyDescription.Text = "";
            // 
            // lblStrategyDescription
            // 
            this.lblStrategyDescription.Location = new System.Drawing.Point(3, 65);
            this.lblStrategyDescription.Name = "lblStrategyDescription";
            this.lblStrategyDescription.Size = new System.Drawing.Size(196, 23);
            this.lblStrategyDescription.TabIndex = 2;
            this.lblStrategyDescription.Text = "StrategyDescription:";
            this.lblStrategyDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtNickname
            // 
            this.txtNickname.Enabled = false;
            this.txtNickname.Location = new System.Drawing.Point(3, 40);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.ReadOnly = true;
            this.txtNickname.Size = new System.Drawing.Size(196, 22);
            this.txtNickname.TabIndex = 1;
            this.txtNickname.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblNickname
            // 
            this.lblNickname.Location = new System.Drawing.Point(3, 14);
            this.lblNickname.Name = "lblNickname";
            this.lblNickname.Size = new System.Drawing.Size(196, 23);
            this.lblNickname.TabIndex = 0;
            this.lblNickname.Text = "Nickname:";
            this.lblNickname.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnUnloadFromGame);
            this.panel2.Controls.Add(this.btnLoadToGame);
            this.panel2.Location = new System.Drawing.Point(646, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(51, 276);
            this.panel2.TabIndex = 4;
            // 
            // btnUnloadFromGame
            // 
            this.btnUnloadFromGame.Enabled = false;
            this.btnUnloadFromGame.Location = new System.Drawing.Point(3, 143);
            this.btnUnloadFromGame.Name = "btnUnloadFromGame";
            this.btnUnloadFromGame.Size = new System.Drawing.Size(45, 31);
            this.btnUnloadFromGame.TabIndex = 1;
            this.btnUnloadFromGame.Text = "<";
            this.btnUnloadFromGame.UseVisualStyleBackColor = true;
            this.btnUnloadFromGame.Click += new System.EventHandler(this.btnUnloadFromGame_Click);
            // 
            // btnLoadToGame
            // 
            this.btnLoadToGame.Enabled = false;
            this.btnLoadToGame.Location = new System.Drawing.Point(3, 78);
            this.btnLoadToGame.Name = "btnLoadToGame";
            this.btnLoadToGame.Size = new System.Drawing.Size(45, 32);
            this.btnLoadToGame.TabIndex = 0;
            this.btnLoadToGame.Text = ">";
            this.btnLoadToGame.UseVisualStyleBackColor = true;
            this.btnLoadToGame.Click += new System.EventHandler(this.btnLoadToGame_Click);
            // 
            // lstbxLoadedAi
            // 
            this.lstbxLoadedAi.FormattingEnabled = true;
            this.lstbxLoadedAi.ItemHeight = 16;
            this.lstbxLoadedAi.Location = new System.Drawing.Point(703, 12);
            this.lstbxLoadedAi.Name = "lstbxLoadedAi";
            this.lstbxLoadedAi.Size = new System.Drawing.Size(485, 276);
            this.lstbxLoadedAi.TabIndex = 0;
            // 
            // TournamentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 341);
            this.Controls.Add(this.lstbxLoadedAi);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lstbxAvailableAi);
            this.Controls.Add(this.btnStartTournament);
            this.Name = "TournamentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.TournamentForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.ListBox lstbxLoadedAi;

        private System.Windows.Forms.Button btnLoadToGame;
        private System.Windows.Forms.Button btnUnloadFromGame;

        private System.Windows.Forms.Panel panel2;

        private System.Windows.Forms.Label lblStrategyDescription;
        private System.Windows.Forms.RichTextBox rtxtStrategyDescription;

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNickname;
        private System.Windows.Forms.TextBox txtNickname;

        private System.Windows.Forms.ListBox lstbxAvailableAi;

        private System.Windows.Forms.Button btnStartTournament;

        #endregion
    }    
}

