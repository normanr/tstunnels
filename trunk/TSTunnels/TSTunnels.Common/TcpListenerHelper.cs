using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace TSTunnels.Common
{
	public static class TcpListenerHelper
	{
		public static void Start(string listenAddress, int listenPort, Action<TcpClient> acceptDelegate)
		{
			var listener = new TcpListener(IPAddress.Parse(listenAddress), listenPort);
			listener.Start();
			listener.BeginAcceptTcpClient(listener_AcceptTcpClient, (Converter<IAsyncResult, TcpListener>)delegate(IAsyncResult ar)
			{
				acceptDelegate(listener.EndAcceptTcpClient(ar));
				return listener;
			});
		}

		private static void listener_AcceptTcpClient(IAsyncResult ar)
		{
			var callback = (Converter<IAsyncResult, TcpListener>)ar.AsyncState;
			var listener = callback(ar);
			listener.BeginAcceptTcpClient(listener_AcceptTcpClient, callback);
		}
	}
}
