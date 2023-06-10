using System;

namespace IPFSuite.FileFormats.IES
{
	// Token: 0x0200003A RID: 58
	public class IesHeader
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0000BAE4 File Offset: 0x00009CE4
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000BAEC File Offset: 0x00009CEC
		public uint DataOffset { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000152 RID: 338 RVA: 0x0000BAF5 File Offset: 0x00009CF5
		// (set) Token: 0x06000153 RID: 339 RVA: 0x0000BAFD File Offset: 0x00009CFD
		public uint ResourceOffset { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000BB06 File Offset: 0x00009D06
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000BB0E File Offset: 0x00009D0E
		public uint FileSize { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000BB17 File Offset: 0x00009D17
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000BB1F File Offset: 0x00009D1F
		public string Name { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000BB28 File Offset: 0x00009D28
		// (set) Token: 0x06000159 RID: 345 RVA: 0x0000BB30 File Offset: 0x00009D30
		public ushort ColumnCount { get; set; }

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x0600015A RID: 346 RVA: 0x0000BB39 File Offset: 0x00009D39
		// (set) Token: 0x0600015B RID: 347 RVA: 0x0000BB41 File Offset: 0x00009D41
		public ushort RowCount { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000BB4A File Offset: 0x00009D4A
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000BB52 File Offset: 0x00009D52
		public ushort NumberColumnCount { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015E RID: 350 RVA: 0x0000BB5B File Offset: 0x00009D5B
		// (set) Token: 0x0600015F RID: 351 RVA: 0x0000BB63 File Offset: 0x00009D63
		public ushort StringColumnCount { get; set; }
	}
}
