using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace TSTunnels.UI
{
	class Win32Api
	{
		[DllImport("Kernel32.dll", SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string moduleName);

		[DllImport("User32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr LoadIcon(IntPtr hInst, string iconId);

		private const string IDI_APPLICATION = "#32512";

		public static Icon GetApplicationIcon()
		{
			var icon = LoadIcon(GetModuleHandle(null), IDI_APPLICATION);
			// vshost.exe doesn't have our application icon, so we could fail at this point
			return icon != IntPtr.Zero ? Icon.FromHandle(icon) : SystemIcons.Application;
		}
	}
}
