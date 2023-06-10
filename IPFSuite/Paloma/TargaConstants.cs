using System;

namespace Paloma
{
	// Token: 0x0200002F RID: 47
	internal static class TargaConstants
	{
		// Token: 0x040001B1 RID: 433
		internal const int HeaderByteLength = 18;

		// Token: 0x040001B2 RID: 434
		internal const int FooterByteLength = 26;

		// Token: 0x040001B3 RID: 435
		internal const int FooterSignatureOffsetFromEnd = 18;

		// Token: 0x040001B4 RID: 436
		internal const int FooterSignatureByteLength = 16;

		// Token: 0x040001B5 RID: 437
		internal const int FooterReservedCharByteLength = 1;

		// Token: 0x040001B6 RID: 438
		internal const int ExtensionAreaAuthorNameByteLength = 41;

		// Token: 0x040001B7 RID: 439
		internal const int ExtensionAreaAuthorCommentsByteLength = 324;

		// Token: 0x040001B8 RID: 440
		internal const int ExtensionAreaJobNameByteLength = 41;

		// Token: 0x040001B9 RID: 441
		internal const int ExtensionAreaSoftwareIDByteLength = 41;

		// Token: 0x040001BA RID: 442
		internal const int ExtensionAreaSoftwareVersionLetterByteLength = 1;

		// Token: 0x040001BB RID: 443
		internal const int ExtensionAreaColorCorrectionTableValueLength = 256;

		// Token: 0x040001BC RID: 444
		internal const string TargaFooterASCIISignature = "TRUEVISION-XFILE";
	}
}
