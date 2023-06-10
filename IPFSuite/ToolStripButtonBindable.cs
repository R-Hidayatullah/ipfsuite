using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace IPFSuite
{
	// Token: 0x0200003C RID: 60
	public class ToolStripButtonBindable : ToolStripButton, IBindableComponent, IComponent, IDisposable
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x0000BE8C File Offset: 0x0000A08C
		public ControlBindingsCollection DataBindings
		{
			get
			{
				if (this.dataBindings == null)
				{
					this.dataBindings = new ControlBindingsCollection(this);
				}
				return this.dataBindings;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000BEC0 File Offset: 0x0000A0C0
		// (set) Token: 0x060000FA RID: 250 RVA: 0x0000BEF3 File Offset: 0x0000A0F3
		public BindingContext BindingContext
		{
			get
			{
				if (this.bindingContext == null)
				{
					this.bindingContext = new BindingContext();
				}
				return this.bindingContext;
			}
			set
			{
				this.bindingContext = value;
			}
		}

		// Token: 0x04000213 RID: 531
		private ControlBindingsCollection dataBindings;

		// Token: 0x04000214 RID: 532
		private BindingContext bindingContext;
	}
}
