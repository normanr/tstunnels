using System;
using System.IO;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class ListenRequest : ChannelMessage
	{
		public readonly int StreamIndex;
		public readonly string Address;
		public readonly int Port;

		public ListenRequest(int StreamIndex, string Address, int Port)
			: base(MessageType.ListenRequest)
		{
			this.StreamIndex = StreamIndex;
			this.Address = Address;
			this.Port = Port;
		}

		protected ListenRequest(BinaryReader reader)
			: base(reader)
		{
			StreamIndex = reader.ReadInt32();
			Address = reader.ReadString();
			Port = reader.ReadInt32();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(StreamIndex);
			writer.Write(Address);
			writer.Write(Port);
		}

		public void Process(IStreamServer server)
		{
			TcpListenerHelper.Start(Address, Port, client =>
			{
				var streamIndex = --server.ConnectionCount;
				server.Streams[streamIndex] = client.GetStream();
				server.WriteMessage(new ListenResponse(StreamIndex, streamIndex));
			});
		}
	}
}