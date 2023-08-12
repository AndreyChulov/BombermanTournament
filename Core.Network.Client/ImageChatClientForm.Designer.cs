namespace ImageChat.Client
{
    partial class ImageChatClientForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.statusBar = new System.Windows.Forms.StatusBar();
            this.txtBxServerLocatorService = new System.Windows.Forms.TextBox();
            this.txtBxServerLocatorServerStatus = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.statusBar, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 450);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // statusBar
            // 
            this.statusBar.Location = new System.Drawing.Point(3, 433);
            this.statusBar.Name = "statusBar";
            this.statusBar.Size = new System.Drawing.Size(794, 14);
            this.statusBar.TabIndex = 0;
            this.statusBar.Text = "status";
            // 
            // txtBxServerLocatorService
            // 
            this.txtBxServerLocatorService.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxServerLocatorService.Enabled = false;
            this.txtBxServerLocatorService.Location = new System.Drawing.Point(3, 3);
            this.txtBxServerLocatorService.Name = "txtBxServerLocatorService";
            this.txtBxServerLocatorService.Size = new System.Drawing.Size(391, 22);
            this.txtBxServerLocatorService.TabIndex = 1;
            // 
            // txtBxServerLocatorServerStatus
            // 
            this.txtBxServerLocatorServerStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBxServerLocatorServerStatus.Location = new System.Drawing.Point(400, 3);
            this.txtBxServerLocatorServerStatus.Name = "txtBxServerLocatorServerStatus";
            this.txtBxServerLocatorServerStatus.Size = new System.Drawing.Size(391, 22);
            this.txtBxServerLocatorServerStatus.TabIndex = 2;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.AutoSize = true;
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.txtBxServerLocatorService, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.txtBxServerLocatorServerStatus, 1, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(794, 28);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // ImageChatClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ImageChatClientForm";
            this.Text = "ImageChatClientForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;

        private System.Windows.Forms.TextBox txtBxServerLocatorServerStatus;

        private System.Windows.Forms.TextBox txtBxServerLocatorService;

        private System.Windows.Forms.StatusBar statusBar;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        #endregion
    }
}