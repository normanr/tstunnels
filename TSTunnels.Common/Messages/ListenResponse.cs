using System;
using System.IO;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class ListenResponse : ChannelMessage
	{
		public readonly int ListenIndex;
		public readonly int StreamIndex;

		public ListenResponse(int ListenIndex, int StreamIndex)
			: base(MessageType.ListenResponse)
		{
			this.ListenIndex = ListenIndex;
			this.StreamIndex = StreamIndex;
		}

		protected ListenResponse(BinaryReader reader)
			: base(reader)
		{
			ListenIndex = reader.ReadInt32();
			StreamIndex = reader.ReadInt32();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(ListenIndex);
			writer.Write(StreamIndex);
		}
	}
}