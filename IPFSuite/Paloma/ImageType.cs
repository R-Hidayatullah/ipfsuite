﻿using System;

namespace Paloma
{
	// Token: 0x02000032 RID: 50
	public enum ImageType : byte
	{
		// Token: 0x040001C5 RID: 453
		NO_IMAGE_DATA,
		// Token: 0x040001C6 RID: 454
		UNCOMPRESSED_COLOR_MAPPED,
		// Token: 0x040001C7 RID: 455
		UNCOMPRESSED_TRUE_COLOR,
		// Token: 0x040001C8 RID: 456
		UNCOMPRESSED_BLACK_AND_WHITE,
		// Token: 0x040001C9 RID: 457
		RUN_LENGTH_ENCODED_COLOR_MAPPED = 9,
		// Token: 0x040001CA RID: 458
		RUN_LENGTH_ENCODED_TRUE_COLOR,
		// Token: 0x040001CB RID: 459
		RUN_LENGTH_ENCODED_BLACK_AND_WHITE
	}
}