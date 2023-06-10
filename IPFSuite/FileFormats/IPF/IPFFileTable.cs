using System;

namespace IPFSuite.FileFormats.IPF
{
	// Token: 0x02000032 RID: 50
	public class IPFFileTable
	{
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000FB RID: 251 RVA: 0x0000ADA5 File Offset: 0x00008FA5
		// (set) Token: 0x060000FC RID: 252 RVA: 0x0000ADAD File Offset: 0x00008FAD
		public int idx { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000FD RID: 253 RVA: 0x0000ADB6 File Offset: 0x00008FB6
		// (set) Token: 0x060000FE RID: 254 RVA: 0x0000ADBE File Offset: 0x00008FBE
		public ushort fileNameLength { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000FF RID: 255 RVA: 0x0000ADC7 File Offset: 0x00008FC7
		// (set) Token: 0x06000100 RID: 256 RVA: 0x0000ADCF File Offset: 0x00008FCF
		public ushort containerNameLength { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000101 RID: 257 RVA: 0x0000ADD8 File Offset: 0x00008FD8
		// (set) Token: 0x06000102 RID: 258 RVA: 0x0000ADE0 File Offset: 0x00008FE0
		public uint fileSizeCompressed { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000ADE9 File Offset: 0x00008FE9
		// (set) Token: 0x06000104 RID: 260 RVA: 0x0000ADF1 File Offset: 0x00008FF1
		public uint fileSizeUncompressed { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000105 RID: 261 RVA: 0x0000ADFA File Offset: 0x00008FFA
		// (set) Token: 0x06000106 RID: 262 RVA: 0x0000AE02 File Offset: 0x00009002
		public uint filePointer { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000107 RID: 263 RVA: 0x0000AE0B File Offset: 0x0000900B
		// (set) Token: 0x06000108 RID: 264 RVA: 0x0000AE13 File Offset: 0x00009013
		public uint crc32 { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000AE1C File Offset: 0x0000901C
		// (set) Token: 0x0600010A RID: 266 RVA: 0x0000AE24 File Offset: 0x00009024
		public string containerName { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600010B RID: 267 RVA: 0x0000AE2D File Offset: 0x0000902D
		// (set) Token: 0x0600010C RID: 268 RVA: 0x0000AE35 File Offset: 0x00009035
		public string fileName { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600010D RID: 269 RVA: 0x0000AE3E File Offset: 0x0000903E
		// (set) Token: 0x0600010E RID: 270 RVA: 0x0000AE46 File Offset: 0x00009046
		public string directoryName { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600010F RID: 271 RVA: 0x0000AE4F File Offset: 0x0000904F
		// (set) Token: 0x06000110 RID: 272 RVA: 0x0000AE57 File Offset: 0x00009057
		public byte[] content { get; set; }
	}
}
