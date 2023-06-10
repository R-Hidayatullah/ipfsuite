using System;

namespace IPFSuite.FileFormats.IES
{
	// Token: 0x0200003B RID: 59
	public class IesColumn : IComparable<IesColumn>
	{
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000BB6C File Offset: 0x00009D6C
		// (set) Token: 0x06000162 RID: 354 RVA: 0x0000BB74 File Offset: 0x00009D74
		public string Name { get; set; }

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000BB7D File Offset: 0x00009D7D
		// (set) Token: 0x06000164 RID: 356 RVA: 0x0000BB85 File Offset: 0x00009D85
		public string Name2 { get; set; }

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000BB8E File Offset: 0x00009D8E
		// (set) Token: 0x06000166 RID: 358 RVA: 0x0000BB96 File Offset: 0x00009D96
		public ColumnType Type { get; set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000BB9F File Offset: 0x00009D9F
		// (set) Token: 0x06000168 RID: 360 RVA: 0x0000BBA7 File Offset: 0x00009DA7
		public ushort Position { get; set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public bool IsNumber
		{
			get
			{
				return this.Type == ColumnType.Float;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000BBBC File Offset: 0x00009DBC
		public int CompareTo(IesColumn other)
		{
			if (this.Type == other.Type || (this.Type == ColumnType.String && other.Type == ColumnType.String2) || (this.Type == ColumnType.String2 && other.Type == ColumnType.String))
			{
				return this.Position.CompareTo(other.Position);
			}
			if (this.Type < other.Type)
			{
				return -1;
			}
			return 1;
		}
	}
}
