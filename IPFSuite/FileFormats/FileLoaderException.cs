using System;

namespace IPFSuite.FileFormats
{
	// Token: 0x0200000C RID: 12
	public class FileLoaderException : Exception
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000037C4 File Offset: 0x000019C4
		public string ErrorMsg
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000037DC File Offset: 0x000019DC
		public FileLoaderException(string error)
		{
			this._error = error;
		}

		// Token: 0x040000B4 RID: 180
		private string _error;
	}
}
