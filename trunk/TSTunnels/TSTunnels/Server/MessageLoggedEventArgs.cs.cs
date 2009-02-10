using System;
using System.Collections.Generic;
using System.Text;

namespace TSTunnels.Server
{
	public class MessageLoggedEventArgs : EventArgs
	{
		public string Message { get; private set; }

		public MessageLoggedEventArgs(string Message)
		{
			this.Message = Message;
		}
	}
}
