using System;
using System.Collections.Generic;
using System.IO;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class ConnectResponse : ChannelMessage
	{
		public readonly int StreamIndex;
		public readonly string LocalEndPoint;
		public readonly string RemoteEndPoint;

		public ConnectResponse(int StreamIndex, string RemoteEndPoint, string LocalEndPoint)
			: base(MessageType.ConnectResponse)
		{
			this.StreamIndex = StreamIndex;
			this.LocalEndPoint = LocalEndPoint;
			this.RemoteEndPoint = RemoteEndPoint;
		}

		protected ConnectResponse(BinaryReader reader)
			: base(reader)
		{
			StreamIndex = reader.ReadInt32();
			LocalEndPoint = reader.ReadString();
			RemoteEndPoint = reader.ReadString();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(StreamIndex);
			writer.Write(LocalEndPoint);
			writer.Write(RemoteEndPoint);
		}

		public override string ToString()
		{
			return base.ToString() + ": " + RemoteEndPoint + " to " + LocalEndPoint;
		}

		public void Process(IStreamServer server)
		{
			new StreamPump(server.Streams[StreamIndex],
				data => server.WriteMessage(new StreamData(StreamIndex, data)),
				ex =>
				{
					server.Log("Forwarded port closed: " + RemoteEndPoint + " to " + LocalEndPoint);
					server.WriteMessage(new StreamError(StreamIndex, ex));
				}).Pump();
		}
	}
}
