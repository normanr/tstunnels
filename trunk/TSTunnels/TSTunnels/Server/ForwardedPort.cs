using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace TSTunnels.Server
{
	public enum ForwardDirection
	{
		Local,
		Remote
	}

	public class ForwardedPort
	{
		public ForwardDirection Direction;
		public string ListenEndPoint;
		public string ConnectEndPoint;
		public MethodInvoker Remove;

		public override string ToString()
		{
			return Direction + "\t" + ListenEndPoint + "\t" + ConnectEndPoint;
		}
	}
}
