using System;

namespace IPFSuite.FileFormats.XAC
{
	// Token: 0x0200001C RID: 28
	public struct XAC_Header
	{
		// Token: 0x04000100 RID: 256
		public string Identifier;

		// Token: 0x04000101 RID: 257
		public byte MajorVersion;

		// Token: 0x04000102 RID: 258
		public byte MinorVersion;

		// Token: 0x04000103 RID: 259
		public bool BigEndian;

		// Token: 0x04000104 RID: 260
		public byte MultiplyOrder;
	}
}
