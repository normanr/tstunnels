using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class StreamData : ChannelMessage
	{
		public readonly int StreamIndex;
		public readonly byte[] Data;

		public StreamData(int StreamIndex, byte[] Data)
			: base(MessageType.StreamData)
		{
			this.StreamIndex = StreamIndex;
			this.Data = Data;
		}

		protected StreamData(BinaryReader reader)
			: base(reader)
		{
			StreamIndex = reader.ReadInt32();
			var length = reader.ReadInt32();
			Data = reader.ReadBytes(length);
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(StreamIndex);
			writer.Write(Data.Length);
			writer.Write(Data);
		}

		public void Process(IStreamServer server)
		{
			Stream stream;
			lock (server.Streams)
			{
				if (!server.Streams.ContainsKey(StreamIndex)) return;
				stream = server.Streams[StreamIndex];
			}
			try
			{
				stream.Write(Data, 0, Data.Length);
			}
			catch (Exception ex)
			{
				server.WriteMessage(new StreamError(StreamIndex, ex.ToString()));
			}
		}
	}
}
