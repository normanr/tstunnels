using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;

namespace TSTunnels.Common.Messages
{
	[Serializable]
	public class StreamError : ChannelMessage
	{
		public readonly int StreamIndex;
		public readonly string Exception;

		public StreamError(int StreamIndex, string Exception)
			: base(MessageType.StreamError)
		{
			this.StreamIndex = StreamIndex;
			this.Exception = Exception;
		}

		protected StreamError(BinaryReader reader)
			: base(reader)
		{
			StreamIndex = reader.ReadInt32();
			Exception = reader.ReadString();
		}

		protected override void Serialize(BinaryWriter writer)
		{
			base.Serialize(writer);
			writer.Write(StreamIndex);
			writer.Write(Exception);
		}

		public override string ToString()
		{
			return base.ToString() + ": " + Exception;
		}

		public void Process(IStreamServer server)
		{
			ProcessStreams(server.Streams);
			ProcessListeners(server.Listeners);
		}

		private void ProcessStreams(IDictionary<int, Stream> streams)
		{
			Stream stream;
			lock (streams)
			{
				if (!streams.ContainsKey(StreamIndex)) return;
				stream = streams[StreamIndex];
			}
			if (Exception.Length != 0)
			{
				try
				{
					var data = Encoding.UTF8.GetBytes(Exception);
					stream.Write(data, 0, data.Length);
				}
				catch (IOException)
				{
				}
			}
			try
			{
				stream.Close();
			}
			catch (IOException)
			{
			}
			lock (streams)
			{
				if (streams.ContainsKey(StreamIndex)) streams.Remove(StreamIndex);
			}
		}

		private void ProcessListeners(IDictionary<int, TcpListener> listeners)
		{
			TcpListener listener;
			lock (listeners)
			{
				if (!listeners.ContainsKey(StreamIndex)) return;
				listener = listeners[StreamIndex];
			}
			try
			{
				listener.Stop();
			}
			catch (IOException)
			{
			}
			lock (listeners)
			{
				if (listeners.ContainsKey(StreamIndex)) listeners.Remove(StreamIndex);
			}
		}
	}
}
