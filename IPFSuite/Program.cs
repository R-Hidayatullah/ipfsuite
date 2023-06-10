using System;
using System.Windows.Forms;

namespace IPFSuite
{
	// Token: 0x0200002E RID: 46
	internal static class Program
	{
		// Token: 0x06000085 RID: 133 RVA: 0x000099F8 File Offset: 0x00007BF8
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new fMain());
		}
	}
}
