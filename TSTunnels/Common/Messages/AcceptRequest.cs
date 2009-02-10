using System;
using System.IO;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class AcceptRequest : ChannelMessage
	{
		public readonly int ListenIndex;
		public readonly int StreamIndex;
		public readonly string RemoteEndPoint;

		public AcceptRequest(int ListenIndex, int StreamIndex, string RemoteEndPoint)
			: base(MessageType.AcceptRequest)
		{
			this.ListenIndex = ListenIndex;
			this.StreamIndex = StreamIndex;
			this.RemoteEndPoint = RemoteEndPoint;
		}

		protected AcceptRequest(BinaryReader reader)
			: base(reader)
		{
			ListenIndex = reader.ReadInt32();
			StreamIndex = reader.ReadInt32();
			RemoteEndPoint = reader.ReadString();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(ListenIndex);
			writer.Write(StreamIndex);
			writer.Write(RemoteEndPoint);
		}
	}
}