using System;
using System.IO;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class HelloRequest : ChannelMessage
	{
		public readonly string MachineName;

		public HelloRequest()
			: base(MessageType.HelloRequest)
		{
			MachineName = Environment.MachineName;
		}

		protected HelloRequest(BinaryReader reader)
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

		public void Process(IStreamServer server)
		{
			server.WriteMessage(new HelloResponse());
		}
	}
}