using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace IPFSuite.Extensions
{
	// Token: 0x02000003 RID: 3
	public static class BinaryReaderExtensions
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002278 File Offset: 0x00000478
		public static T Read<T>(this BinaryReader br)
		{
			byte[] array = new byte[Marshal.SizeOf(typeof(T))];
			array = br.ReadBytes(array.Length);
			GCHandle gchandle = GCHandle.Alloc(array, GCHandleType.Pinned);
			T result = (T)((object)Marshal.PtrToStructure(gchandle.AddrOfPinnedObject(), typeof(T)));
			gchandle.Free();
			return result;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022D8 File Offset: 0x000004D8
		public static string ReadStringAtOffset(this BinaryReader br, int offset, int size)
		{
			long position = br.BaseStream.Position;
			br.BaseStream.Seek(Convert.ToInt64(offset), SeekOrigin.Begin);
			string text = Encoding.UTF8.GetString(br.ReadBytes(size));
			while (text.EndsWith("\0"))
			{
				text = text.Substring(0, text.Length - 1);
			}
			br.BaseStream.Seek(position, SeekOrigin.Begin);
			return text;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x0000234C File Offset: 0x0000054C
		public static string ReadCString(this BinaryReader br, int size)
		{
			string text = "";
			int i;
			for (i = 0; i < size; i++)
			{
				byte b = br.ReadByte();
				if (b == 0)
				{
					break;
				}
				text += (char)b;
			}
			if (i < size)
			{
				br.ReadBytes(size - i - 1);
			}
			return text;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023B8 File Offset: 0x000005B8
		public static string ReadCString(this BinaryReader br)
		{
			string text = "";
			for (;;)
			{
				byte b = br.ReadByte();
				if (b == 0)
				{
					break;
				}
				text += (char)b;
			}
			return text;
		}
	}
}
