using System;
using System.Collections.Generic;
using System.IO;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class ConnectResponse : ChannelMessage
	{
		public readonly int StreamIndex;
		public readonly string RemoteEndPoint;

		public ConnectResponse(int StreamIndex, string RemoteEndPoint)
			: base(MessageType.ConnectResponse)
		{
			this.StreamIndex = StreamIndex;
			this.RemoteEndPoint = RemoteEndPoint;
		}

		protected ConnectResponse(BinaryReader reader)
			: base(reader)
		{
			StreamIndex = reader.ReadInt32();
			RemoteEndPoint = reader.ReadString();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(StreamIndex);
			writer.Write(RemoteEndPoint);
		}

		public override string ToString()
		{
			return base.ToString() + ": " + RemoteEndPoint;
		}

		public void Process(IStreamServer server)
		{
			new StreamPump(server.Streams[StreamIndex],
				data => server.WriteMessage(new StreamData(StreamIndex, data)),
				ex => server.WriteMessage(new StreamError(StreamIndex, ex.ToString()))).Pump();
		}
	}
}
