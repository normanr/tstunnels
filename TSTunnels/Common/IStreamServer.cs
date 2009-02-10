using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using TSTunnels.Common.Messages;

namespace TSTunnels.Common
{
	public interface IStreamServer
	{
		void MessageReceived(ChannelMessage msg);
		int ConnectionCount { get; set; }
		IDictionary<int, Stream> Streams { get; }
		IDictionary<int, TcpListener> Listeners { get; }
		bool WriteMessage(ChannelMessage msg);
		void Log(object message);
	}
}
