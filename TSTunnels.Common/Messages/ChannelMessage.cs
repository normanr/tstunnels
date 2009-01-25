using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Runtime.Serialization;

namespace TSTunnels.Common.Messages
{
	public delegate void WriteMessage(ChannelMessage msg);

	public abstract class ChannelMessage
	{
		//ATTENTION: name should have 7 or less chars
		public const string ChannelName = "TSTnls";

		public readonly MessageType Type;

		protected ChannelMessage(MessageType Type)
		{
			this.Type = Type;
		}

		protected ChannelMessage(BinaryReader reader)
		{
			Type = (MessageType)reader.ReadInt32();
		}

		protected virtual void Serialize(BinaryWriter writer)
		{
			writer.Write((int)Type);
		}

		public byte[] ToByteArray()
		{
			if (Type == MessageType.Unknown)
				throw new ArgumentException();
			using (var stream = new MemoryStream())
			{
				using (var writer = new BinaryWriter(stream))
				{
					writer.Write(GetType().FullName);
					Serialize(writer);
				}
				return stream.ToArray();
			}
		}

		public static ChannelMessage FromByteArray(byte[] data)
		{
			using (var stream = new MemoryStream(data))
			{
				return FromStream(stream);
			}
		}

		public static ChannelMessage FromStream(Stream stream)
		{
			using (var reader = new BinaryReader(stream))
			{
				var type = System.Type.GetType(reader.ReadString());
				return (ChannelMessage)type.InvokeMember(null, BindingFlags.CreateInstance | BindingFlags.Instance | BindingFlags.NonPublic, null, null, new object[] { reader });
			}
		}
	}
}
