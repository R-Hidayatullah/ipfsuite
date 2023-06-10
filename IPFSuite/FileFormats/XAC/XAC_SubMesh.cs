using System;
using System.Collections.Generic;

namespace IPFSuite.FileFormats.XAC
{
	// Token: 0x02000020 RID: 32
	public class XAC_SubMesh
	{
		// Token: 0x06000030 RID: 48 RVA: 0x00003C90 File Offset: 0x00001E90
		public XAC_SubMesh()
		{
			this.Positions = new List<XAC_Vector3D>();
			this.Normals = new List<XAC_Vector3D>();
			this.Tangents = new List<XAC_Vector4D>();
			this.Bitangents = new List<XAC_Vector4D>();
			this.UVSets = new List<List<XAC_Vector2D>>();
			this.InfluenceRangeIndices = new List<int>();
			this.Indices = new List<int>();
			this.Colors128 = new List<List<XAC_Color>>();
			this.Colors32 = new List<List<XAC_Color8>>();
			this.MaterialID = -1;
		}

		// Token: 0x0400011E RID: 286
		public List<XAC_Vector3D> Positions;

		// Token: 0x0400011F RID: 287
		public List<XAC_Vector3D> Normals;

		// Token: 0x04000120 RID: 288
		public List<XAC_Vector4D> Tangents;

		// Token: 0x04000121 RID: 289
		public List<XAC_Vector4D> Bitangents;

		// Token: 0x04000122 RID: 290
		public List<List<XAC_Vector2D>> UVSets;

		// Token: 0x04000123 RID: 291
		public List<int> InfluenceRangeIndices;

		// Token: 0x04000124 RID: 292
		public List<int> Indices;

		// Token: 0x04000125 RID: 293
		public List<List<XAC_Color>> Colors128;

		// Token: 0x04000126 RID: 294
		public List<List<XAC_Color8>> Colors32;

		// Token: 0x04000127 RID: 295
		public int MaterialID;
	}
}
