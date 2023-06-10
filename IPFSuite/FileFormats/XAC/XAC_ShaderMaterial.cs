using System;
using System.Collections.Generic;

namespace IPFSuite.FileFormats.XAC
{
	// Token: 0x02000023 RID: 35
	public class XAC_ShaderMaterial
	{
		// Token: 0x04000139 RID: 313
		public string Name;

		// Token: 0x0400013A RID: 314
		public string ShaderName;

		// Token: 0x0400013B RID: 315
		public List<KeyValuePair<string, int>> IntProperties = new List<KeyValuePair<string, int>>();

		// Token: 0x0400013C RID: 316
		public List<KeyValuePair<string, float>> FloatProperties = new List<KeyValuePair<string, float>>();

		// Token: 0x0400013D RID: 317
		public List<KeyValuePair<string, bool>> BoolProperties = new List<KeyValuePair<string, bool>>();

		// Token: 0x0400013E RID: 318
		public List<KeyValuePair<string, string>> StringProperties = new List<KeyValuePair<string, string>>();
	}
}
