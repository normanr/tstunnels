using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using TSTunnels.Common;
using TSTunnels.Common.Messages;

namespace TSTunnels.Server
{
	public partial class Server : Form, IStreamServer
	{
		public Server()
		{
			InitializeComponent();
			Streams = new Dictionary<int, Stream>();
			Listeners = new Dictionary<int, TcpListener>();
		}

		private IntPtr mHandle = IntPtr.Zero;
		private BinaryReader reader;
		private void Server_Load(object sender, EventArgs e)
		{
			mHandle = WtsApi32.WTSVirtualChannelOpen(IntPtr.Zero, -1, ChannelMessage.ChannelName);
			if (mHandle == IntPtr.Zero)
			{
				Log(new Win32Exception());
				return;
			}

			try
			{
				var stream = WtsApi32.WTSVirtualChannelQuery_WTSVirtualFileHandle(mHandle);
				reader = new BinaryReader(new BufferedStream(stream));
			}
			catch (Win32Exception ex)
			{
				Log(ex);
				return;
			}

			backgroundWorker1.RunWorkerAsync();
			timer1.Enabled = true;
		}

		private void Server_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (reader != null) reader.Close();
			var ret = WtsApi32.WTSVirtualChannelClose(mHandle);
			backgroundWorker1.CancelAsync();
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			while (!backgroundWorker1.CancellationPending)
			{
				try
				{
					var len = reader.ReadInt32();
					var buff = reader.ReadBytes(len);
					backgroundWorker1.ReportProgress(0, buff);
				}
				catch (OperationCanceledException)
				{
					return;
				}
			}
		}

		private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
		{
			if (backgroundWorker1.CancellationPending) return;
			MessageReceived(ChannelMessage.FromByteArray((byte[])e.UserState));
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			var r = e.Result;
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			WriteMessage(new HelloRequest());
		}

		private delegate void CreatePort(string listenAddress, int listenPort, string connectAddress, int connectPort);
		enum ForwardDirection
		{
			Local,
			Remote
		}
		class ForwardedPort
		{
			public ForwardDirection Direction;
			public string ListenEndPoint;
			public string ConnectEndPoint;
			public int StreamIndex;
			public MethodInvoker Remove;

			public override string ToString()
			{
				return Direction + "\t" + ListenEndPoint + "\t" + ConnectEndPoint;
			}
		}
		class ClientListener
		{
			public Action<ListenResponse> ListenResponse;
			public Action<StreamError> StreamError;
			public Action<AcceptRequest> AcceptRequest;
		}
		private readonly Dictionary<int, ClientListener> clientListeners = new Dictionary<int, ClientListener>();
		private void CreateClientPort(string listenAddress, int listenPort, string connectAddress, int connectPort)
		{
			var listenIndex = ++ConnectionCount;
			ForwardedPort forwardedPort = null;
			var listener = new ClientListener
			{
				ListenResponse = response =>
				{
					Log("Local port forwarding from " + listenAddress + ":" + listenPort + " enabled");
					forwardedPort = new ForwardedPort
					{
						Direction = ForwardDirection.Local,
						ListenEndPoint = listenAddress + ":" + listenPort,
						ConnectEndPoint = connectAddress + ":" + connectPort,
						StreamIndex = listenIndex,
						Remove = () =>
						{
							Log("Cancelling local port " + listenAddress + ":" + listenPort + " forwarding to " + connectAddress + ":" + connectPort + " requested");
							WriteMessage(new StreamError(listenIndex, new EndOfStreamException().ToString()));
						},
					};
					forwardedPortsListBox.Items.Add(forwardedPort);
				},
				StreamError = error =>
				{
					if (forwardedPort == null)
					{
						Log("Local port forwarding from " + listenAddress + ":" + listenPort + " failed: " + error.Exception);
						lock (clientListeners)
						{
							if (clientListeners.ContainsKey(listenIndex)) clientListeners.Remove(listenIndex);
						}
					}
					else
					{
						Log("Cancelled local port " + listenAddress + ":" + listenPort + " forwarding to " + connectAddress + ":" + connectPort);
						forwardedPortsListBox.Items.Remove(forwardedPort);
					}
				},
				AcceptRequest = request =>
				{
					Log("Opening forwarded connection " + request.RemoteEndPoint + " to " +
						connectAddress + ":" + connectPort);
					new ConnectRequest(request.StreamIndex, connectAddress, connectPort).Process(this);
				},
			};
			lock (clientListeners)
			{
				clientListeners[listenIndex] = listener;
			}
			Log("Requesting local port " + listenAddress + ":" + listenPort + " forward to " + connectAddress + ":" + connectPort);
			WriteMessage(new ListenRequest(listenIndex, listenAddress, listenPort));
		}

		private void CreateServerPort(string listenAddress, int listenPort, string connectAddress, int connectPort)
		{
			try
			{
				var listenIndex = ++ConnectionCount;
				ForwardedPort forwardedPort = null;
				Listeners[listenIndex] = TcpListenerHelper.Start(listenAddress, listenPort, client =>
				{
					var streamIndex = ++ConnectionCount;
					Log("Attempting to forward remote port " + client.Client.RemoteEndPoint + " to " + connectAddress + ":" + connectPort);
					Streams[streamIndex] = client.GetStream();
					WriteMessage(new ConnectRequest(streamIndex, connectAddress, connectPort));
				}, exception => Invoke(new MethodInvoker(() =>
				{
					if (forwardedPort == null)
					{
						Log("Remote port " + listenAddress + ":" + listenPort + " forwarding to " + connectAddress + ":" + connectPort + " exception: " + exception);
					}
					else
					{
						Log("Cancelled remote port " + listenAddress + ":" + listenPort + " forwarding to " + connectAddress + ":" + connectPort);
						forwardedPortsListBox.Items.Remove(forwardedPort);
					}
				})));
				Log("Remote port " + listenAddress + ":" + listenPort + " forwarding to " + connectAddress + ":" + connectPort);
				forwardedPort = new ForwardedPort
				{
					Direction = ForwardDirection.Remote,
					ListenEndPoint = listenAddress + ":" + listenPort,
					ConnectEndPoint = connectAddress + ":" + connectPort,
					StreamIndex = listenIndex,
					Remove = () =>
					{
						Log("Cancelling remote port " + listenAddress + ":" + listenPort + " forwarding to " + connectAddress + ":" + connectPort);
						new StreamError(listenIndex, new EndOfStreamException().ToString()).Process(this);
					},
				};
				forwardedPortsListBox.Items.Add(forwardedPort);
			}
			catch (Exception ex)
			{
				Log("Remote port " + listenAddress + ":" + listenPort + " forwarding to " + connectAddress + ":" + connectPort + " failed: " + ex);
			}
		}

		private void Log(object message)
		{
			if (InvokeRequired)
			{
				Invoke(new Action<object>(Log), new[] {"Non-UI-Thread: " + message});
				return;
			}
			eventLogListBox.SelectedIndex = eventLogListBox.Items.Add(DateTime.Now + "\t" + message);
		}

		public void MessageReceived(ChannelMessage msg)
		{
			switch (msg.Type)
			{
				default:
					Log("Unknown message: " + msg);
					break;
				case MessageType.HelloResponse:
					{
						timer1.Enabled = false;
						var response = (HelloResponse)msg;
						Log(response.MachineName + " connected to " + Environment.MachineName);
					}
					break;
				case MessageType.ListenResponse:
					{
						var response = (ListenResponse)msg;
						ClientListener listener = null;
						lock (clientListeners)
						{
							if (clientListeners.ContainsKey(response.ListenIndex)) listener = clientListeners[response.ListenIndex];
						}
						if (listener != null) listener.ListenResponse(response);
					}
					break;
				case MessageType.AcceptRequest:
					{
						var request = (AcceptRequest)msg;
						ClientListener listener = null;
						lock (clientListeners)
						{
							if (clientListeners.ContainsKey(request.ListenIndex)) listener = clientListeners[request.ListenIndex];
						}
						if (listener != null) listener.AcceptRequest(request);
					}
					break;
				case MessageType.ConnectResponse:
					((ConnectResponse)msg).Process(this);
					break;
				case MessageType.StreamData:
					((StreamData)msg).Process(this);
					break;
				case MessageType.StreamError:
					{
						var error = (StreamError)msg;
						ClientListener listener = null;
						lock (clientListeners)
						{
							if (clientListeners.ContainsKey(error.StreamIndex)) listener = clientListeners[error.StreamIndex];
						}
						if (listener != null)
						{
							listener.StreamError(error);
						}
						else
						{
							((StreamError)msg).Process(this);
						}
					}
					break;
			}
		}

		#region Implementation of IStreamServer

		public int ConnectionCount { get; set; }
		public IDictionary<int, Stream> Streams { get; private set; }
		public IDictionary<int, TcpListener> Listeners { get; private set; }

		public void WriteMessage(ChannelMessage msg)
		{
			var data = msg.ToByteArray();
			int written;
			var ret = WtsApi32.WTSVirtualChannelWrite(mHandle, data, data.Length, out written);
			if (ret) return;
			Log(new Win32Exception());
			return;
		}

		#endregion

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
				if (connectEndPoint.Length != 2)
				{
					MessageBox.Show("You need to specify a destination address in the form \"host.name:port\"", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				var connectAddress = connectEndPoint[0];
				var connectPort = int.Parse(connectEndPoint[1]);
				(localRadioButton.Checked ? (CreatePort)CreateClientPort : CreateServerPort)(listenAddress, listenPort, connectAddress, connectPort);
			}
			catch (Exception ex)
			{
				Log(ex);
			}
		}

		private void removeButton_Click(object sender, EventArgs e)
		{
			var forwardedPort = forwardedPortsListBox.SelectedItem as ForwardedPort;
			if (forwardedPort != null) forwardedPort.Remove();
		}
	}
}
