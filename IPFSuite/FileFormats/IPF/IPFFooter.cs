using System;
using System.ComponentModel;

namespace IPFSuite.FileFormats.IPF
{
	// Token: 0x02000033 RID: 51
	public class IPFFooter
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000112 RID: 274 RVA: 0x0000AE60 File Offset: 0x00009060
		// (set) Token: 0x06000113 RID: 275 RVA: 0x0000AE68 File Offset: 0x00009068
		[DisplayName("Version to patch")]
		[Description("This is a specific version to override. Set to 0 to overwrite all versions.")]
		public uint VersionToPatch { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000114 RID: 276 RVA: 0x0000AE71 File Offset: 0x00009071
		// (set) Token: 0x06000115 RID: 277 RVA: 0x0000AE79 File Offset: 0x00009079
		[DisplayName("Version")]
		[Description("This is the version of this ipf file.")]
		public uint NewVersion { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000116 RID: 278 RVA: 0x0000AE82 File Offset: 0x00009082
		// (set) Token: 0x06000117 RID: 279 RVA: 0x0000AE8A File Offset: 0x0000908A
		public ushort FileCount { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000AE93 File Offset: 0x00009093
		// (set) Token: 0x06000119 RID: 281 RVA: 0x0000AE9B File Offset: 0x0000909B
		public ushort Padding1 { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000AEA4 File Offset: 0x000090A4
		// (set) Token: 0x0600011B RID: 283 RVA: 0x0000AEAC File Offset: 0x000090AC
		public uint FileTablePointer { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000AEB5 File Offset: 0x000090B5
		// (set) Token: 0x0600011D RID: 285 RVA: 0x0000AEBD File Offset: 0x000090BD
		public uint FooterPointer { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000AEC6 File Offset: 0x000090C6
		// (set) Token: 0x0600011F RID: 287 RVA: 0x0000AECE File Offset: 0x000090CE
		public uint Compression { get; set; }
	}
}
