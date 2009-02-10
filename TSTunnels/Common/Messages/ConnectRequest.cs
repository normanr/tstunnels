using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Sockets;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class ConnectRequest : ChannelMessage
	{
		public readonly int StreamIndex;
		public readonly string LocalEndPoint;
		public readonly string Address;
		public readonly int Port;

		public ConnectRequest(int StreamIndex, string LocalEndPoint, string Address, int Port)
			: base(MessageType.ConnectRequest)
		{
			this.StreamIndex = StreamIndex;
			this.LocalEndPoint = LocalEndPoint;
			this.Address = Address;
			this.Port = Port;
		}

		protected ConnectRequest(BinaryReader reader)
			: base(reader)
		{
			StreamIndex = reader.ReadInt32();
			LocalEndPoint = reader.ReadString();
			Address = reader.ReadString();
			Port = reader.ReadInt32();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(StreamIndex);
			writer.Write(LocalEndPoint);
			writer.Write(Address);
			writer.Write(Port);
		}

		public void Process(IStreamServer server)
		{
			var client = new TcpClient();
			client.BeginConnect(Address, Port, ar =>
			{
				try
				{
					client.EndConnect(ar);
					server.Streams[StreamIndex] = client.GetStream();
					var connectResult = new ConnectResponse(StreamIndex, LocalEndPoint, client.Client.RemoteEndPoint.ToString());
					connectResult.Process(server);
					server.WriteMessage(connectResult);
				}
				catch (Exception ex)
				{
					Debug.Print(DateTime.Now + " " + Environment.MachineName + ": " + ex);
					server.WriteMessage(new StreamError(StreamIndex, ex));
				}
			}, null);
		}
	}
}