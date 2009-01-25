using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TSTunnels.Common
{
	public class StreamPump
	{
		const int bufferSize = 10000;
		private readonly Stream stream;
		private readonly Action<byte[]> writeCallback;
		private readonly Action<Exception> errorCallback;
		private readonly byte[] buf = new byte[bufferSize];

		public StreamPump(Stream stream, Action<byte[]> writeCallback, Action<Exception> errorCallback)
		{
			this.stream = stream;
			this.writeCallback = writeCallback;
			this.errorCallback = errorCallback;
		}

		public void Pump()
		{
			try
			{
				stream.BeginRead(buf, 0, bufferSize, ReadCallback, null);
			}
			catch (Exception ex)
			{
				errorCallback(ex);
			}
		}

		private void ReadCallback(IAsyncResult ar)
		{
			try
			{
				var bytesReceived = stream.EndRead(ar);
				if (bytesReceived > 0)
				{
					var data = new byte[bytesReceived];
					Buffer.BlockCopy(buf, 0, data, 0, bytesReceived);
					writeCallback(data);
					Pump();
				}
				else
				{
					errorCallback(new EndOfStreamException());
					return;
				}
			}
			catch (Exception ex)
			{
				errorCallback(ex);
			}
		}
	}
}
