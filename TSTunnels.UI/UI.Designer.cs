namespace TSTunnels.UI
{
	partial class UI
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
			this.eventLogListBox = new System.Windows.Forms.ListBox();
			this.eventLogGroupBox = new System.Windows.Forms.GroupBox();
			this.portForwardingGroupBox = new System.Windows.Forms.GroupBox();
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
			this.eventLogGroupBox.SuspendLayout();
			this.portForwardingGroupBox.SuspendLayout();
			this.SuspendLayout();
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
			// eventLogGroupBox
			// 
			this.eventLogGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.eventLogGroupBox.Controls.Add(this.eventLogListBox);
			this.eventLogGroupBox.Location = new System.Drawing.Point(12, 207);
			this.eventLogGroupBox.Name = "eventLogGroupBox";
			this.eventLogGroupBox.Size = new System.Drawing.Size(656, 219);
			this.eventLogGroupBox.TabIndex = 1;
			this.eventLogGroupBox.TabStop = false;
			this.eventLogGroupBox.Text = "Event log";
			// 
			// portForwardingGroupBox
			// 
			this.portForwardingGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.portForwardingGroupBox.Controls.Add(this.remoteRadioButton);
			this.portForwardingGroupBox.Controls.Add(this.localRadioButton);
			this.portForwardingGroupBox.Controls.Add(this.destinationTextBox);
			this.portForwardingGroupBox.Controls.Add(this.addButton);
			this.portForwardingGroupBox.Controls.Add(this.removeButton);
			this.portForwardingGroupBox.Controls.Add(this.label4);
			this.portForwardingGroupBox.Controls.Add(this.label3);
			this.portForwardingGroupBox.Controls.Add(this.label2);
			this.portForwardingGroupBox.Controls.Add(this.label1);
			this.portForwardingGroupBox.Controls.Add(this.forwardedPortsListBox);
			this.portForwardingGroupBox.Controls.Add(this.sourceTextBox);
			this.portForwardingGroupBox.Enabled = false;
			this.portForwardingGroupBox.Location = new System.Drawing.Point(12, 12);
			this.portForwardingGroupBox.Name = "portForwardingGroupBox";
			this.portForwardingGroupBox.Size = new System.Drawing.Size(656, 189);
			this.portForwardingGroupBox.TabIndex = 0;
			this.portForwardingGroupBox.TabStop = false;
			this.portForwardingGroupBox.Text = "Port forwarding";
			// 
			// remoteRadioButton
			// 
			this.remoteRadioButton.AutoSize = true;
			this.remoteRadioButton.Location = new System.Drawing.Point(494, 163);
			this.remoteRadioButton.Name = "remoteRadioButton";
			this.remoteRadioButton.Size = new System.Drawing.Size(62, 17);
			this.remoteRadioButton.TabIndex = 6;
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
			this.addButton.TabIndex = 7;
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
			this.removeButton.TabIndex = 10;
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
			this.label4.TabIndex = 3;
			this.label4.Text = "Destination:";
			// 
			// label3
			// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 165);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(44, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Source:";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 146);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(123, 13);
			this.label2.TabIndex = 0;
			this.label2.Text = "Add new forwarded port:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(86, 13);
			this.label1.TabIndex = 8;
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
			this.forwardedPortsListBox.TabIndex = 9;
			// 
			// sourceTextBox
			// 
			this.sourceTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.sourceTextBox.Location = new System.Drawing.Point(56, 162);
			this.sourceTextBox.Name = "sourceTextBox";
			this.sourceTextBox.Size = new System.Drawing.Size(150, 20);
			this.sourceTextBox.TabIndex = 2;
			// 
			// UI
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(680, 438);
			this.Controls.Add(this.portForwardingGroupBox);
			this.Controls.Add(this.eventLogGroupBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "UI";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Remote Desktop Tunnels";
			this.Load += new System.EventHandler(this.UI_Load);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UI_FormClosed);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UI_FormClosing);
			this.eventLogGroupBox.ResumeLayout(false);
			this.portForwardingGroupBox.ResumeLayout(false);
			this.portForwardingGroupBox.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ListBox eventLogListBox;
		private System.Windows.Forms.GroupBox eventLogGroupBox;
		private System.Windows.Forms.GroupBox portForwardingGroupBox;
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

