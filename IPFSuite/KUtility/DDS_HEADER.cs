using System;

namespace KUtility
{
	// Token: 0x02000005 RID: 5
	internal class DDS_HEADER
	{
		// Token: 0x04000019 RID: 25
		public int dwSize;

		// Token: 0x0400001A RID: 26
		public int dwFlags;

		// Token: 0x0400001B RID: 27
		public int dwHeight;

		// Token: 0x0400001C RID: 28
		public int dwWidth;

		// Token: 0x0400001D RID: 29
		public int dwPitchOrLinearSize;

		// Token: 0x0400001E RID: 30
		public int dwDepth;

		// Token: 0x0400001F RID: 31
		public int dwMipMapCount;

		// Token: 0x04000020 RID: 32
		public int[] dwReserved1 = new int[11];

		// Token: 0x04000021 RID: 33
		public DDS_PIXELFORMAT ddspf = new DDS_PIXELFORMAT();

		// Token: 0x04000022 RID: 34
		public int dwCaps;

		// Token: 0x04000023 RID: 35
		public int dwCaps2;

		// Token: 0x04000024 RID: 36
		public int dwCaps3;

		// Token: 0x04000025 RID: 37
		public int dwCaps4;

		// Token: 0x04000026 RID: 38
		public int dwReserved2;
	}
}
