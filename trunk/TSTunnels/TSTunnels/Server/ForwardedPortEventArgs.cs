using System;
using System.Collections.Generic;
using System.Text;

namespace TSTunnels.Server
{
	public class ForwardedPortEventArgs : EventArgs
	{
		public ForwardedPort ForwardedPort { get; private set; }

		public ForwardedPortEventArgs(ForwardedPort forwardedPort)
		{
			ForwardedPort = forwardedPort;
		}
	}
}
