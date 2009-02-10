using System;
using System.Collections.Generic;
using System.Text;

namespace TSTunnels.Server
{
	public class ConnectedEventArgs : EventArgs
	{
		public string MachineName { get; private set; }

		public ConnectedEventArgs(string MachineName)
		{
			this.MachineName = MachineName;
		}
	}
}
