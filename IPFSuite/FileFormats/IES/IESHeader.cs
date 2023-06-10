using System;

namespace IPFSuite.FileFormats.IES
{
	// Token: 0x02000038 RID: 56
	public struct IESHeader
	{
		// Token: 0x040001EC RID: 492
		public uint Unknown;

		// Token: 0x040001ED RID: 493
		public uint DataOffset;

		// Token: 0x040001EE RID: 494
		public uint ResourceOffset;

		// Token: 0x040001EF RID: 495
		public uint FileSize;

		// Token: 0x040001F0 RID: 496
		public string Name;

		// Token: 0x040001F1 RID: 497
		public ushort ColumnCount;

		// Token: 0x040001F2 RID: 498
		public ushort RowCount;

		// Token: 0x040001F3 RID: 499
		public ushort NumberColumnCount;

		// Token: 0x040001F4 RID: 500
		public ushort StringColumnCount;
	}
}
