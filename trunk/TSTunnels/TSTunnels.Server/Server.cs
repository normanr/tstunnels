using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
		}

		private IntPtr mHandle = IntPtr.Zero;
		private BinaryReader reader;
		private void Server_Load(object sender, EventArgs e)
		{
			mHandle = WtsApi32.WTSVirtualChannelOpen(IntPtr.Zero, -1, ChannelMessage.ChannelName);
			if (mHandle == IntPtr.Zero)
			{
				textBox2.SelectedText = new Win32Exception() + Environment.NewLine;
				return;
			}

			try
			{
				var stream = WtsApi32.WTSVirtualChannelQuery_WTSVirtualFileHandle(mHandle);
				reader = new BinaryReader(new BufferedStream(stream));
			}
			catch (Win32Exception ex)
			{
				textBox2.Text = ex.ToString();
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

		private void button1_Click(object sender, EventArgs e)
		{
			WriteMessage(new HelloRequest());
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			WriteMessage(new HelloRequest());
		}

		private readonly Dictionary<int, Action<int>> remoteServers = new Dictionary<int, Action<int>>();
		private void CreateClientPort(string listenAddress, int listenPort, string connectAddress, int connectPort)
		{
			var listenIndex = ++ConnectionCount;
			remoteServers[listenIndex] = index => new ConnectRequest(index, connectAddress, connectPort).Process(this);
			WriteMessage(new ListenRequest(listenIndex, listenAddress, listenPort));
		}

		private void CreateServerPort(string listenAddress, int listenPort, string connectAddress, int connectPort)
		{
			TcpListenerHelper.Start(listenAddress, listenPort, client =>
			{
				var streamIndex = ++ConnectionCount;
				Streams[streamIndex] = client.GetStream();
				WriteMessage(new ConnectRequest(streamIndex, connectAddress, connectPort));
			});
		}

		private bool portsCreated;
		public void MessageReceived(ChannelMessage msg)
		{
			switch (msg.Type)
			{
				default:
					textBox2.SelectedText = msg + Environment.NewLine;
					break;
				case MessageType.HelloReponse:
					{
						timer1.Enabled = false;
						textBox2.SelectedText = msg + Environment.NewLine;
						if (!portsCreated)
						{
							portsCreated = true;
							CreateServerPort("127.0.0.1", 2222, "ssh", 22);
							CreateClientPort("127.0.0.1", 2222, "ssh", 22);
						}
					}
					break;
				case MessageType.ListenResponse:
					{
						var result = (ListenResponse)msg;
						var server = remoteServers[result.ListenIndex];
						server(result.StreamIndex);
					}
					break;
				case MessageType.ConnectResponse:
					((ConnectResponse)msg).Process(this);
					break;
				case MessageType.StreamData:
					((StreamData)msg).Process(this);
					break;
				case MessageType.StreamError:
					((StreamError)msg).Process(this);
					break;
			}
		}

		#region Implementation of IStreamServer

		public int ConnectionCount { get; set; }
		public IDictionary<int, Stream> Streams { get; private set; }

		public void WriteMessage(ChannelMessage msg)
		{
			var data = msg.ToByteArray();
			int written;
			var ret = WtsApi32.WTSVirtualChannelWrite(mHandle, data, data.Length, out written);
			if (ret) return;
			textBox2.SelectedText = new Win32Exception() + Environment.NewLine;
			return;
		}

		#endregion
	}
}
