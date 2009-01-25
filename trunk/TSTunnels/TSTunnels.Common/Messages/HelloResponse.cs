using System;
using System.IO;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class HelloResponse : ChannelMessage
	{
		public readonly string MachineName;

		public HelloResponse()
			: base(MessageType.HelloResponse)
		{
			MachineName = Environment.MachineName;
		}

		protected HelloResponse(BinaryReader reader)
			: base(reader)
		{
			MachineName = reader.ReadString();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(MachineName);
		}

		public override string ToString()
		{
			return base.ToString() + ": " + MachineName;
		}
	}
}
