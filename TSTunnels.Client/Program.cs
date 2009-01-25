using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using TSTunnels.Client.WtsApi32;
using TSTunnels.Common.Messages;

namespace TSTunnels.Client
{
	public static class Program
	{
		private const string registryAddins = @"Software\Microsoft\Terminal Server Client\Default\AddIns\";

		static Program()
		{
			AppDomain.CurrentDomain.AssemblyResolve += delegate(object sender, ResolveEventArgs args)
			{
				try
				{
					var location = typeof(Program).Assembly.Location;
					return Assembly.LoadFrom(Path.Combine(Path.GetDirectoryName(location), args.Name.Split(',')[0] + ".dll"));
				}
				catch (Exception)
				{
					return null;
				}
			};
		}

		[ExportDllAttribute.ExportDll("DllRegisterServer", CallingConvention.StdCall)]
		public static void DllRegisterServer()
		{
			using (var key = Registry.CurrentUser.CreateSubKey(registryAddins + ChannelMessage.ChannelName))
			{
				Debug.Assert(key != null);
				var location = typeof(Client).Assembly.Location;
				Debug.Assert(location != null);
				key.SetValue("Name", location);
			}
		}

		[ExportDllAttribute.ExportDll("DllUnregisterServer", CallingConvention.StdCall)]
		public static void DllUnregisterServer()
		{
			Registry.CurrentUser.DeleteSubKey(registryAddins + ChannelMessage.ChannelName);
		}

		private static Client client;

		[ExportDllAttribute.ExportDll("VirtualChannelEntry", CallingConvention.StdCall)]
		public static bool VirtualChannelEntry(ref ChannelEntryPoints entry)
		{
			client = new Client(entry);
			return client.VirtualChannelInit();
		}
	}
}
