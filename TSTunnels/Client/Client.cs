using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using TSTunnels.Client.WtsApi32;
using TSTunnels.Common;
using TSTunnels.Common.Messages;

namespace TSTunnels.Client
{
	public class Client : IStreamServer
	{
		private readonly ChannelEntryPoints entryPoints;
		private readonly ChannelInitEventDelegate channelInitEventDelegate;
		private readonly ChannelOpenEventDelegate channelOpenEventDelegate;

		private IntPtr Channel;
		private int OpenChannel;

		public Client(ChannelEntryPoints entry)
		{
			entryPoints = entry;
			channelInitEventDelegate = VirtualChannelInitEventProc;
			channelOpenEventDelegate = VirtualChannelOpenEvent;
			Streams = new Dictionary<int, Stream>();
			Listeners = new Dictionary<int, TcpListener>();
		}

		public bool VirtualChannelInit()
		{
			var cd = new ChannelDef[1];
			cd[0] = new ChannelDef { name = ChannelMessage.ChannelName };
			var ret = entryPoints.VirtualChannelInit(ref Channel, cd, 1, 1, channelInitEventDelegate);
			if (ret != ChannelReturnCodes.Ok)
			{
				MessageBox.Show("TSTunnels: RDP Virtual channel Init Failed.\n" + ret, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			return true;
		}

		private void VirtualChannelInitEventProc(IntPtr initHandle, ChannelEvents Event, byte[] data, int dataLength)
		{
			Debug.Print(DateTime.Now + " " + Environment.MachineName + ": VirtualChannelInitEventProc: " + Event);
			switch (Event)
			{
				case ChannelEvents.Initialized:
					break;
				case ChannelEvents.Connected:
					var ret = entryPoints.VirtualChannelOpen(initHandle, ref OpenChannel, ChannelMessage.ChannelName, channelOpenEventDelegate);
					if (ret != ChannelReturnCodes.Ok)
					{
						MessageBox.Show("TSTunnels: Open of RDP virtual channel failed.\n" + ret, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
					{
						/*main = new frmMain(entryPoints, OpenChannel);
						main.Show();
						main.Hide();
						string servername = System.Text.Encoding.Unicode.GetString(data);
						servername = servername.Substring(0, servername.IndexOf('\0'));
						main.Text = "TS addin in C#: " + servername;*/
					}
					break;
				case ChannelEvents.V1Connected:
					MessageBox.Show("TSTunnels: Connecting to a non Windows 2000 Terminal Server.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					break;
				case ChannelEvents.Disconnected:
					//main.RealClosing = true;
					//main.Close();
					break;
				case ChannelEvents.Terminated:
					break;
			}
		}

		private MemoryStream memoryStream;
		public void VirtualChannelOpenEvent(int openHandle, ChannelEvents Event, IntPtr pData, int dataLength, uint totalLength, ChannelFlags dataFlags)
		{
			Debug.Print(DateTime.Now + " " + Environment.MachineName + ": VirtualChannelOpenEvent: " + Event);
			switch (Event)
			{
				case ChannelEvents.DataReceived:
					var data = new byte[dataLength];
					Marshal.Copy(pData, data, 0, dataLength);
					switch (dataFlags & ChannelFlags.Only)
					{
						case ChannelFlags.Only:
							MessageReceived(ChannelMessage.FromByteArray(data));
							break;
						case ChannelFlags.First:
							memoryStream = new MemoryStream((int)totalLength);
							memoryStream.Write(data, 0, data.Length);
							break;
						case ChannelFlags.Middle:
							if (memoryStream != null)
							{
								memoryStream.Write(data, 0, data.Length);
							}
							break;
						case ChannelFlags.Last:
							if (memoryStream != null)
							{
								memoryStream.Write(data, 0, data.Length);
								memoryStream.Position = 0;
								MessageReceived(ChannelMessage.FromStream(memoryStream));
								memoryStream = null;
							}
							break;
					}
					break;
				case ChannelEvents.WriteCanceled:
				case ChannelEvents.WriteComplete:
					/*
					 * The VirtualChannelWrite function is asynchronous. When the write operation has been completed,
					 * your VirtualChannelOpenEvent function receives a CHANNEL_EVENT_WRITE_COMPLETE notification.
					 * Until that notification is received, the caller must not free or reuse the pData buffer passed to VirtualChannelWrite
					 */
					Marshal.FreeHGlobal(pData);
					break;
			}
		}

		#region Implementation of IStreamServer

		public void MessageReceived(ChannelMessage msg)
		{
			switch (msg.Type)
			{
				case MessageType.HelloRequest:
					((HelloRequest)msg).Process(this);
					break;
				case MessageType.ConnectRequest:
					((ConnectRequest)msg).Process(this);
					break;
				case MessageType.ListenRequest:
					((ListenRequest)msg).Process(this);
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

		public int ConnectionCount { get; set; }
		public IDictionary<int, Stream> Streams { get; private set; }
		public IDictionary<int, TcpListener> Listeners { get; private set; }

		public bool WriteMessage(ChannelMessage msg)
		{
			var data = msg.ToByteArray();
			var len = 4 + data.Length;
			var ptr = Marshal.AllocHGlobal(len);
			Marshal.WriteInt32(ptr, 0, data.Length);
			Marshal.Copy(data, 0, new IntPtr(ptr.ToInt32() + 4), data.Length);
			var ret = entryPoints.VirtualChannelWrite(OpenChannel, ptr, (uint)len, ptr);
			return ret == ChannelReturnCodes.Ok;
		}

		public void Log(object message)
		{
		}

		#endregion
	}
}
