using System;
using System.Collections.Generic;

namespace IPFSuite.FileFormats.XAC
{
	// Token: 0x0200001E RID: 30
	public struct XAC_Material
	{
		// Token: 0x04000108 RID: 264
		public XAC_Color AmbientColor;

		// Token: 0x04000109 RID: 265
		public XAC_Color DiffuseColor;

		// Token: 0x0400010A RID: 266
		public XAC_Color SpecularColor;

		// Token: 0x0400010B RID: 267
		public XAC_Color EmissiveColor;

		// Token: 0x0400010C RID: 268
		public float Shine;

		// Token: 0x0400010D RID: 269
		public float ShineStrength;

		// Token: 0x0400010E RID: 270
		public float Opacity;

		// Token: 0x0400010F RID: 271
		public float IOR;

		// Token: 0x04000110 RID: 272
		public bool DoubleSided;

		// Token: 0x04000111 RID: 273
		public bool Wireframe;

		// Token: 0x04000112 RID: 274
		public byte NumLayers;

		// Token: 0x04000113 RID: 275
		public string Name;

		// Token: 0x04000114 RID: 276
		public IList<XAC_Material.Layer> Layers;

		// Token: 0x0200001F RID: 31
		public struct Layer
		{
			// Token: 0x04000115 RID: 277
			public float Amount;

			// Token: 0x04000116 RID: 278
			public float UOffset;

			// Token: 0x04000117 RID: 279
			public float VOffset;

			// Token: 0x04000118 RID: 280
			public float UTiling;

			// Token: 0x04000119 RID: 281
			public float VTiling;

			// Token: 0x0400011A RID: 282
			public float Rotation;

			// Token: 0x0400011B RID: 283
			public ushort MaterialId;

			// Token: 0x0400011C RID: 284
			public byte MapType;

			// Token: 0x0400011D RID: 285
			public string Name;
		}
	}
}
