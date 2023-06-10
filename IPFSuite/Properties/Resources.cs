using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace IPFSuite.Properties
{
	// Token: 0x02000041 RID: 65
	[CompilerGenerated]
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	internal class Resources
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000E5AD File Offset: 0x0000C7AD
		internal Resources()
		{
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000E5B8 File Offset: 0x0000C7B8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("IPFSuite.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600011B RID: 283 RVA: 0x0000E604 File Offset: 0x0000C804
		// (set) Token: 0x0600011C RID: 284 RVA: 0x0000E61B File Offset: 0x0000C81B
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000232 RID: 562
		private static ResourceManager resourceMan;

		// Token: 0x04000233 RID: 563
		private static CultureInfo resourceCulture;
	}
}
