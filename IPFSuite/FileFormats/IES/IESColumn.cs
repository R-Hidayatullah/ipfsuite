using System;

namespace IPFSuite.FileFormats.IES
{
	// Token: 0x02000035 RID: 53
	public struct IESColumn : IComparable<IESColumn>
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000B0BC File Offset: 0x000092BC
		public bool IsNumber
		{
			get
			{
				return this.Type == IESColumnType.Float;
			}
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000B0C8 File Offset: 0x000092C8
		public int CompareTo(IESColumn other)
		{
			int result;
			if (this.Type == other.Type || (this.Type == IESColumnType.String && other.Type == IESColumnType.String2) || (this.Type == IESColumnType.String2 && other.Type == IESColumnType.String))
			{
				result = this.Position.CompareTo(other.Position);
			}
			else if (this.Type < other.Type)
			{
				result = -1;
			}
			else
			{
				result = 1;
			}
			return result;
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600012A RID: 298 RVA: 0x0000B138 File Offset: 0x00009338
		// (set) Token: 0x0600012B RID: 299 RVA: 0x0000B140 File Offset: 0x00009340
		public string Name { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600012C RID: 300 RVA: 0x0000B149 File Offset: 0x00009349
		// (set) Token: 0x0600012D RID: 301 RVA: 0x0000B151 File Offset: 0x00009351
		public string Name2 { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600012E RID: 302 RVA: 0x0000B15A File Offset: 0x0000935A
		// (set) Token: 0x0600012F RID: 303 RVA: 0x0000B162 File Offset: 0x00009362
		public IESColumnType Type { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000130 RID: 304 RVA: 0x0000B16B File Offset: 0x0000936B
		// (set) Token: 0x06000131 RID: 305 RVA: 0x0000B173 File Offset: 0x00009373
		public ushort Position { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000132 RID: 306 RVA: 0x0000B17C File Offset: 0x0000937C
		// (set) Token: 0x06000133 RID: 307 RVA: 0x0000B184 File Offset: 0x00009384
		public uint Unknown { get; set; }
	}
}
