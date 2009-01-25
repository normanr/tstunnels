namespace TSTunnels.Server
{
	partial class Server
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
			this.components = new System.ComponentModel.Container();
			this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.eventLogListBox = new System.Windows.Forms.ListBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.remoteRadioButton = new System.Windows.Forms.RadioButton();
			this.localRadioButton = new System.Windows.Forms.RadioButton();
			this.destinationTextBox = new System.Windows.Forms.TextBox();
			this.addButton = new System.Windows.Forms.Button();
			this.removeButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.forwardedPortsListBox = new System.Windows.Forms.ListBox();
			this.sourceTextBox = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// backgroundWorker1
			// 
			this.backgroundWorker1.WorkerReportsProgress = true;
			this.backgroundWorker1.WorkerSupportsCancellation = true;
			this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
			this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
			this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// eventLogListBox
			// 
			this.eventLogListBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.eventLogListBox.FormattingEnabled = true;
			this.eventLogListBox.Location = new System.Drawing.Point(3, 16);
			this.eventLogListBox.Name = "eventLogListBox";
			this.eventLogListBox.Size = new System.Drawing.Size(650, 199);
			this.eventLogListBox.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.eventLogListBox);
			this.groupBox1.Location = new System.Drawing.Point(12, 207);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(656, 219);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Event log";
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.remoteRadioButton);
			this.groupBox2.Controls.Add(this.localRadioButton);
			this.groupBox2.Controls.Add(this.destinationTextBox);
			this.groupBox2.Controls.Add(this.addButton);
			this.groupBox2.Controls.Add(this.removeButton);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.forwardedPortsListBox);
			this.groupBox2.Controls.Add(this.sourceTextBox);
			this.groupBox2.Location = new System.Drawing.Point(12, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(656, 189);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Port forwarding";
			// 
			// remoteRadioButton
			// 
			this.remoteRadioButton.AutoSize = true;
			this.remoteRadioButton.Location = new System.Drawing.Point(494, 163);
			this.remoteRadioButton.Name = "remoteRadioButton";
			this.remoteRadioButton.Size = new System.Drawing.Size(62, 17);
			this.remoteRadioButton.TabIndex = 5;
			this.remoteRadioButton.Text = "Re&mote";
			this.remoteRadioButton.UseVisualStyleBackColor = true;
			// 
			// localRadioButton
			// 
			this.localRadioButton.AutoSize = true;
			this.localRadioButton.Checked = true;
			this.localRadioButton.Location = new System.Drawing.Point(437, 163);
			this.localRadioButton.Name = "localRadioButton";
			this.localRadioButton.Size = new System.Drawing.Size(51, 17);
			this.localRadioButton.TabIndex = 5;
			this.localRadioButton.TabStop = true;
			this.localRadioButton.Text = "&Local";
			this.localRadioButton.UseVisualStyleBackColor = true;
			// 
			// destinationTextBox
			// 
			this.destinationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.destinationTextBox.Location = new System.Drawing.Point(281, 162);
			this.destinationTextBox.Name = "destinationTextBox";
			this.destinationTextBox.Size = new System.Drawing.Size(150, 20);
			this.destinationTextBox.TabIndex = 4;
			// 
			// addButton
			// 
			this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.addButton.Location = new System.Drawing.Point(562, 160);
			this.addButton.Name = "addButton";
			this.addButton.Size = new System.Drawing.Size(88, 23);
			this.addButton.TabIndex = 3;
			this.addButton.Text = "A&dd";
			this.addButton.UseVisualStyleBackColor = true;
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			// 
			// removeButton
			// 
			this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.removeButton.Location = new System.Drawing.Point(562, 19);
			this.removeButton.Name = "removeButton";
			this.removeButton.Size = new System.Drawing.Size(88, 23);
			this.removeButton.TabIndex = 3;
			this.removeButton.Text = "&Remove";
			this.removeButton.UseVisualStyleBackColor = true;
			this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
			// 
			// label4
			// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(212, 165);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(63, 13);
			this.label4.TabIndex = 2;
			this.label4.Text = "Destination:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 165);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Source:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 146);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Add new forwarded port:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "Forwarded ports:";
			// 
			// forwardedPortsListBox
			// 
			this.forwardedPortsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.forwardedPortsListBox.FormattingEnabled = true;
			this.forwardedPortsListBox.Location = new System.Drawing.Point(9, 48);
			this.forwardedPortsListBox.Name = "forwardedPortsListBox";
			this.forwardedPortsListBox.Size = new System.Drawing.Size(641, 95);
			this.forwardedPortsListBox.TabIndex = 1;
			// 
			// sourceTextBox
			// 
			this.sourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.sourceTextBox.Location = new System.Drawing.Point(56, 162);
			this.sourceTextBox.Name = "sourceTextBox";
			this.sourceTextBox.Size = new System.Drawing.Size(150, 20);
			this.sourceTextBox.TabIndex = 4;
			// 
			// Server
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 438);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Server";
			this.Text = "TS Tunnels";
			this.Load += new System.EventHandler(this.Server_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Server_FormClosed);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.ComponentModel.BackgroundWorker backgroundWorker1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.ListBox eventLogListBox;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListBox forwardedPortsListBox;
		private System.Windows.Forms.TextBox sourceTextBox;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox destinationTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.RadioButton remoteRadioButton;
		private System.Windows.Forms.RadioButton localRadioButton;
	}
}

