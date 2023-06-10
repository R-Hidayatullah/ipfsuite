using System;
using System.Collections.Generic;

namespace IPFSuite.FileFormats.XAC
{
	// Token: 0x02000021 RID: 33
	public class XAC_Mesh
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003D10 File Offset: 0x00001F10
		public XAC_Mesh()
		{
			this.SubMeshes = new List<XAC_SubMesh>();
			this.Owner = null;
		}

		// Token: 0x04000128 RID: 296
		public XAC_Node Owner;

		// Token: 0x04000129 RID: 297
		public List<XAC_SubMesh> SubMeshes;
	}
}
