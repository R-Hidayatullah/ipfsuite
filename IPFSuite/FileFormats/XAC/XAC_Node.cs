using System;
using System.Collections.Generic;

namespace IPFSuite.FileFormats.XAC
{
	// Token: 0x02000022 RID: 34
	public class XAC_Node
	{
		// Token: 0x06000032 RID: 50 RVA: 0x00003D2D File Offset: 0x00001F2D
		public XAC_Node()
		{
			this.Parent = null;
			this.Children = new List<XAC_Node>();
			this.VisualMesh = null;
			this.CollisionMesh = null;
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003D58 File Offset: 0x00001F58
		public bool IsVisible()
		{
			bool result;
			if (this.VisualMesh != null)
			{
				result = true;
			}
			else
			{
				foreach (XAC_Node xac_Node in this.Children)
				{
					if (xac_Node.IsVisible())
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x0400012A RID: 298
		public int ID;

		// Token: 0x0400012B RID: 299
		public XAC_Quaternion Rotation;

		// Token: 0x0400012C RID: 300
		public XAC_Quaternion ScaleRotation;

		// Token: 0x0400012D RID: 301
		public XAC_Vector3D Position;

		// Token: 0x0400012E RID: 302
		public XAC_Vector3D Scale;

		// Token: 0x0400012F RID: 303
		public int UnknownIndex1;

		// Token: 0x04000130 RID: 304
		public int UnknownIndex2;

		// Token: 0x04000131 RID: 305
		public int IncludeBoundsCalc;

		// Token: 0x04000132 RID: 306
		public XAC_Matrix44 Transform;

		// Token: 0x04000133 RID: 307
		public float ImportanceFactor;

		// Token: 0x04000134 RID: 308
		public string Name;

		// Token: 0x04000135 RID: 309
		public XAC_Node Parent;

		// Token: 0x04000136 RID: 310
		public IList<XAC_Node> Children;

		// Token: 0x04000137 RID: 311
		public XAC_Mesh VisualMesh;

		// Token: 0x04000138 RID: 312
		public XAC_Mesh CollisionMesh;
	}
}
