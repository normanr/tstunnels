using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TSTunnels.Server;

namespace TSTunnels.UI
{
	public partial class UI : Form
	{
		private readonly Server.Server Server;

		public UI()
		{
			InitializeComponent();
			Icon = Win32Api.GetApplicationIcon();
			Server = new Server.Server();
			Server.Connected += Server_Connected;
			Server.ForwardedPortAdded += Server_ForwardedPortAdded;
			Server.ForwardedPortRemoved += Server_ForwardedPortRemoved;
			Server.MessageLogged += Server_MessageLogged;
		}

		private void UI_Load(object sender, EventArgs e)
		{
			Server.Connect();
		}

		private bool CloseInProgress;
		private void UI_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (CloseInProgress || forwardedPortsListBox.Items.Count == 0) return;
			CloseInProgress = true;
			e.Cancel = true;
			AppendLogMessage("Cancelling all port forwardings");
			foreach (ForwardedPort forwardedPort in forwardedPortsListBox.Items)
			{
				forwardedPort.Remove();
			}
		}

		private void UI_FormClosed(object sender, FormClosedEventArgs e)
		{
			Server.Disconnect();
		}

		private void Server_ForwardedPortAdded(object sender, ForwardedPortEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ForwardedPortEventArgs>(Server_ForwardedPortAdded), new[] { sender, e });
				return;
			}
			forwardedPortsListBox.Items.Add(e.ForwardedPort);
			sourceTextBox.Text = string.Empty;
			destinationTextBox.Text = string.Empty;
		}

		private void Server_ForwardedPortRemoved(object sender, ForwardedPortEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ForwardedPortEventArgs>(Server_ForwardedPortRemoved), new[] { sender, e });
				return;
			}
			forwardedPortsListBox.Items.Remove(e.ForwardedPort);
			sourceTextBox.Text = e.ForwardedPort.ListenEndPoint;
			destinationTextBox.Text = e.ForwardedPort.ConnectEndPoint;
			(e.ForwardedPort.Direction == ForwardDirection.Local ? localRadioButton : remoteRadioButton).Checked = true;
			if (CloseInProgress && forwardedPortsListBox.Items.Count == 0) Close();
		}

		public void Server_Connected(object sender, ConnectedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<ConnectedEventArgs>(Server_Connected), new[] { sender, e });
				return;
			}
			portForwardingGroupBox.Enabled = true;
		}

		public void Server_MessageLogged(object sender, MessageLoggedEventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventHandler<MessageLoggedEventArgs>(Server_MessageLogged), new[] { sender, e });
				return;
			}
			if (!portForwardingGroupBox.Enabled && e.Message == "RDP Virtual channel Write Failed: Incorrect Function") return;
			AppendLogMessage(e.Message);
		}

		private void AppendLogMessage(object message)
		{
			eventLogListBox.SelectedIndex = eventLogListBox.Items.Add(DateTime.Now + "\t" + message);
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			try
			{
				var listenEndPoint = sourceTextBox.Text.Split(':');
				if (listenEndPoint[0].Length == 0 || listenEndPoint.Length > 2)
				{
					MessageBox.Show("You need to specify a source address in the form \"[host.name:]port\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				var listenAddress = listenEndPoint.Length > 1 ? listenEndPoint[0] : "127.0.0.1";
				var listenPort = int.Parse(listenEndPoint.Length > 1 ? listenEndPoint[1] : listenEndPoint[0]);
				var connectEndPoint = destinationTextBox.Text.Split(':');
				if (connectEndPoint[0].Length == 0 || connectEndPoint.Length > 2)
				{
					MessageBox.Show("You need to specify a destination address in the form \"[host.name:]port\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				var connectAddress = connectEndPoint.Length > 1 ? connectEndPoint[0] : "127.0.0.1";
				var connectPort = int.Parse(connectEndPoint.Length > 1 ? connectEndPoint[1] : connectEndPoint[0]);
				(localRadioButton.Checked ? (Server.Server.CreatePort)Server.CreateClientPort : Server.CreateServerPort)(listenAddress, listenPort, connectAddress, connectPort);
			}
			catch (Exception ex)
			{
				AppendLogMessage(ex);
			}
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			var forwardedPort = forwardedPortsListBox.SelectedItem as ForwardedPort;
			if (forwardedPort != null) forwardedPort.Remove();
		}
	}
}
