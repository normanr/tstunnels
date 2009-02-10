using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace TSTunnels.Server
{
	class WtsApi32
	{
		[DllImport("Wtsapi32.dll", SetLastError = true)]
		public static extern IntPtr WTSVirtualChannelOpen(IntPtr server, int sessionId, [MarshalAs(UnmanagedType.LPStr)] string virtualName);

		[DllImport("Wtsapi32.dll", SetLastError = true)]
		public static extern bool WTSVirtualChannelQuery(IntPtr channelHandle, WtsVirtualClass Class, out IntPtr data, out int bytesReturned);
		
		[DllImport("Wtsapi32.dll")]
		public static extern void WTSFreeMemory(IntPtr memory);

		[DllImport("Wtsapi32.dll", SetLastError = true)]
		public static extern bool WTSVirtualChannelWrite(IntPtr channelHandle, byte[] data, int length, out int bytesWritten);

		[DllImport("Wtsapi32.dll", SetLastError = true)]
		public static extern bool WTSVirtualChannelRead(IntPtr channelHandle, int TimeOut, IntPtr data, int length, out int bytesRead);

		[DllImport("Wtsapi32.dll")]
		public static extern bool WTSVirtualChannelClose(IntPtr channelHandle);

		public static Stream WTSVirtualChannelQuery_WTSVirtualFileHandle(IntPtr channelHandle)
		{
			int len;
			IntPtr buffer;
			var b = WTSVirtualChannelQuery(channelHandle, WtsVirtualClass.WTSVirtualFileHandle, out buffer, out len);
			if (!b) throw new Win32Exception();
			var fileHandle = new SafeFileHandle(Marshal.ReadIntPtr(buffer), true);
			WTSFreeMemory(buffer);

			return new FileStream(fileHandle, FileAccess.ReadWrite, 0x1000, true);
		}
	}

	public enum WtsVirtualClass
	{
		WTSVirtualClient = 0,
		WTSVirtualFileHandle = 1
	}

}