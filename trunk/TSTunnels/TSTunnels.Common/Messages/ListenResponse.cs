using System;
using System.IO;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class ListenResponse : ChannelMessage
	{
		public readonly int ListenIndex;

		public ListenResponse(int ListenIndex)
			: base(MessageType.ListenResponse)
		{
			this.ListenIndex = ListenIndex;
		}

		protected ListenResponse(BinaryReader reader)
			: base(reader)
		{
			ListenIndex = reader.ReadInt32();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(ListenIndex);
		}
	}
}