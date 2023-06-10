using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IPFSuite.FileFormats.IES
{
	// Token: 0x02000037 RID: 55
	public class IESFile : IDisposable
	{
		// Token: 0x06000134 RID: 308 RVA: 0x0000B18D File Offset: 0x0000938D
		public IESFile(Stream stream)
		{
			this.stream = stream;
			this.reader = new BinaryReader(this.stream);
			this.xorKey = 1;
			this.ReadHeader();
			this.ReadColumns();
			this.ReadRows();
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000B1C6 File Offset: 0x000093C6
		public IESFile(byte[] content)
		{
			this.stream = new MemoryStream(content);
			this.reader = new BinaryReader(this.stream);
			this.xorKey = 1;
			this.ReadHeader();
			this.ReadColumns();
			this.ReadRows();
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000B204 File Offset: 0x00009404
		private void ReadHeader()
		{
			this.Header = default(IESHeader);
			this.Header.Name = Encoding.UTF8.GetString(this.reader.ReadBytes(128));
			this.Header.Unknown = this.reader.ReadUInt32();
			this.Header.DataOffset = this.reader.ReadUInt32();
			this.Header.ResourceOffset = this.reader.ReadUInt32();
			this.Header.FileSize = this.reader.ReadUInt32();
			this.reader.ReadUInt16();
			this.Header.RowCount = this.reader.ReadUInt16();
			this.Header.ColumnCount = this.reader.ReadUInt16();
			this.Header.NumberColumnCount = this.reader.ReadUInt16();
			this.Header.StringColumnCount = this.reader.ReadUInt16();
			this.reader.ReadUInt16();
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000B30C File Offset: 0x0000950C
		private void ReadColumns()
		{
			this.Columns = new List<IESColumn>();
			this.reader.BaseStream.Seek((long)(-(long)((ulong)this.Header.ResourceOffset - (ulong)this.Header.DataOffset)), SeekOrigin.End);
			for (int i = 0; i < (int)this.Header.ColumnCount; i++)
			{
				IESColumn item = default(IESColumn);
				item.Name = this.DecryptString(this.reader.ReadBytes(64), null);
				item.Name2 = this.DecryptString(this.reader.ReadBytes(64), null);
				item.Type = (IESColumnType)this.reader.ReadUInt16();
				item.Unknown = this.reader.ReadUInt32();
				item.Position = this.reader.ReadUInt16();
				this.Columns.Add(item);
			}
			this.Columns.Sort();
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000B3F8 File Offset: 0x000095F8
		private void ReadRows()
		{
			this.Rows = new List<object>();
			this.reader.BaseStream.Seek((long)(-(long)((ulong)this.Header.ResourceOffset)), SeekOrigin.End);
			for (int i = 0; i < (int)this.Header.RowCount; i++)
			{
				this.reader.ReadUInt32();
				ushort count = this.reader.ReadUInt16();
				this.reader.ReadBytes((int)count);
				List<object> list = new List<object>();
				for (int j = 0; j < this.Columns.Count; j++)
				{
					if (this.Columns[j].IsNumber)
					{
						float num = this.reader.ReadSingle();
						uint num2 = uint.MaxValue;
						try
						{
							num2 = (uint)num;
						}
						catch
						{
							num = float.NaN;
						}
						if (Math.Abs(num - num2) < 1.401298E-45f)
						{
							list.Add(num2);
						}
						else
						{
							list.Add(num);
						}
					}
					else
					{
						ushort num3 = this.reader.ReadUInt16();
						string item = "";
						if (num3 > 0)
						{
							item = this.DecryptString(this.reader.ReadBytes((int)num3), null);
						}
						list.Add(item);
					}
				}
				this.Rows.Add(list);
				this.reader.BaseStream.Seek((long)((ulong)this.Header.StringColumnCount), SeekOrigin.Current);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000B56C File Offset: 0x0000976C
		private string DecryptString(byte[] data, Encoding encoding = null)
		{
			byte[] array = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				array[i] = (byte)(data[i] ^ xorKey);
			}
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			return encoding.GetString(array).TrimEnd(new char[]
			{
				'\u0001'
			});
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000B5BE File Offset: 0x000097BE
		public void Dispose()
		{
			if (this.reader != null)
			{
				this.reader.Close();
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x0600013B RID: 315 RVA: 0x0000B5D3 File Offset: 0x000097D3
		// (set) Token: 0x0600013C RID: 316 RVA: 0x0000B5DB File Offset: 0x000097DB
		public List<IESColumn> Columns { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x0600013D RID: 317 RVA: 0x0000B5E4 File Offset: 0x000097E4
		// (set) Token: 0x0600013E RID: 318 RVA: 0x0000B5EC File Offset: 0x000097EC
		public List<object> Rows { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600013F RID: 319 RVA: 0x0000B5F5 File Offset: 0x000097F5
		// (set) Token: 0x06000140 RID: 320 RVA: 0x0000B5FD File Offset: 0x000097FD
		private Stream stream { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000141 RID: 321 RVA: 0x0000B606 File Offset: 0x00009806
		// (set) Token: 0x06000142 RID: 322 RVA: 0x0000B60E File Offset: 0x0000980E
		private BinaryReader reader { get; set; }

		// Token: 0x040001E8 RID: 488
		public IESHeader Header;

		// Token: 0x040001EB RID: 491
		private byte xorKey;
	}
}
