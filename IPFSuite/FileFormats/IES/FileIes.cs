using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace IPFSuite.FileFormats.IES
{
	// Token: 0x02000039 RID: 57
	public class FileIes : IDisposable
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000B617 File Offset: 0x00009817
		// (set) Token: 0x06000144 RID: 324 RVA: 0x0000B61F File Offset: 0x0000981F
		public List<IesColumn> Columns { get; private set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000145 RID: 325 RVA: 0x0000B628 File Offset: 0x00009828
		// (set) Token: 0x06000146 RID: 326 RVA: 0x0000B630 File Offset: 0x00009830
		public IesHeader Header { get; private set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000147 RID: 327 RVA: 0x0000B639 File Offset: 0x00009839
		// (set) Token: 0x06000148 RID: 328 RVA: 0x0000B641 File Offset: 0x00009841
		public List<IesRow> Rows { get; private set; }

		// Token: 0x06000149 RID: 329 RVA: 0x0000B64A File Offset: 0x0000984A
		public FileIes(Stream stream)
		{
			this._stream = stream;
			this._reader = new BinaryReader(this._stream);
			this._xorKey = 1;
			this.ReadHeader();
			this.ReadColumns();
			this.ReadRows();
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000B683 File Offset: 0x00009883
		public FileIes(byte[] content) : this(new MemoryStream(content))
		{
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000B694 File Offset: 0x00009894
		private string DecryptString(byte[] data, Encoding encoding = null)
		{
			if (encoding == null)
			{
				encoding = Encoding.UTF8;
			}
			byte[] bytes = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
			{
				bytes[i] = (byte)(data[i] ^ _xorKey);
			}
			return encoding.GetString(bytes).TrimEnd(new char[]
			{
				'\u0001'
			});
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000B6E6 File Offset: 0x000098E6
		public void Dispose()
		{
			if (this._reader != null)
			{
				this._reader.Close();
			}
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000B6FC File Offset: 0x000098FC
		private void ReadColumns()
		{
            _stream.Seek((-((long)this.Header.ResourceOffset) - this.Header.DataOffset), SeekOrigin.End);

            this.Columns = new List<IesColumn>();
            for (int i = 0; i < this.Header.ColumnCount; i++)
            {
                var item = new IesColumn();
                item.Name = this.DecryptString(_reader.ReadBytes(0x40), null);
                item.Name2 = this.DecryptString(_reader.ReadBytes(0x40), null);
                item.Type = (ColumnType)_reader.ReadUInt16();
                _reader.ReadUInt32();
                item.Position = _reader.ReadUInt16();

                // Duplicates
                var old = item.Name;
                for (int j = 1; this.Columns.Exists(a => a.Name == item.Name); ++j)
                    item.Name = old + "_" + j;

                this.Columns.Add(item);
            }
            this.Columns.Sort();
        }

		// Token: 0x0600014E RID: 334 RVA: 0x0000B85C File Offset: 0x00009A5C
		private void ReadHeader()
		{
			this.Header = new IesHeader();
			this.Header.Name = Encoding.UTF8.GetString(this._reader.ReadBytes(128));
			this._reader.ReadUInt32();
			this.Header.DataOffset = this._reader.ReadUInt32();
			this.Header.ResourceOffset = this._reader.ReadUInt32();
			this.Header.FileSize = this._reader.ReadUInt32();
			this._reader.ReadUInt16();
			this.Header.RowCount = this._reader.ReadUInt16();
			this.Header.ColumnCount = this._reader.ReadUInt16();
			this.Header.NumberColumnCount = this._reader.ReadUInt16();
			this.Header.StringColumnCount = this._reader.ReadUInt16();
			this._reader.ReadUInt16();
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000B958 File Offset: 0x00009B58
		private void ReadRows()
		{
			this._reader.BaseStream.Seek((long)(-(long)((ulong)this.Header.ResourceOffset)), SeekOrigin.End);
			this.Rows = new List<IesRow>();
			for (int i = 0; i < (int)this.Header.RowCount; i++)
			{
				this._reader.ReadUInt32();
				ushort count = this._reader.ReadUInt16();
				this._reader.ReadBytes((int)count);
				IesRow item = new IesRow();
				for (int j = 0; j < this.Columns.Count; j++)
				{
					IesColumn column = this.Columns[j];
					if (column.IsNumber)
					{
						float nan = this._reader.ReadSingle();
						uint maxValue = uint.MaxValue;
						try
						{
							maxValue = (uint)nan;
						}
						catch
						{
							nan = float.NaN;
						}
						if (Math.Abs(nan - maxValue) < 1.401298E-45f)
						{
							item.Add(column.Name, maxValue);
						}
						else
						{
							item.Add(column.Name, nan);
						}
					}
					else
					{
						ushort length = this._reader.ReadUInt16();
						string str = "";
						if (length > 0)
						{
							str = this.DecryptString(this._reader.ReadBytes((int)length), null);
						}
						item.Add(column.Name, str);
					}
				}
				this.Rows.Add(item);
				this._reader.BaseStream.Seek((long)((ulong)this.Header.StringColumnCount), SeekOrigin.Current);
			}
		}

		// Token: 0x040001F5 RID: 501
		private Stream _stream;

		// Token: 0x040001F6 RID: 502
		private BinaryReader _reader;

		// Token: 0x040001F7 RID: 503
		private byte _xorKey;
	}
}
