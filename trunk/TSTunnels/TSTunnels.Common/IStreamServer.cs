using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TSTunnels.Common.Messages;

namespace TSTunnels.Common
{
	public interface IStreamServer
	{
		int ConnectionCount { get; set; }
		IDictionary<int, Stream> Streams { get; }
		void WriteMessage(ChannelMessage msg);
	}
}
