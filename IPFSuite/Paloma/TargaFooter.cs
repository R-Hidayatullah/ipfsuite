using System;

namespace Paloma
{
	// Token: 0x02000039 RID: 57
	public class TargaFooter
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000B89C File Offset: 0x00009A9C
		public int ExtensionAreaOffset
		{
			get
			{
				return this.intExtensionAreaOffset;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000B8B4 File Offset: 0x00009AB4
		protected internal void SetExtensionAreaOffset(int intExtensionAreaOffset)
		{
			this.intExtensionAreaOffset = intExtensionAreaOffset;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000B8C0 File Offset: 0x00009AC0
		public int DeveloperDirectoryOffset
		{
			get
			{
				return this.intDeveloperDirectoryOffset;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x0000B8D8 File Offset: 0x00009AD8
		protected internal void SetDeveloperDirectoryOffset(int intDeveloperDirectoryOffset)
		{
			this.intDeveloperDirectoryOffset = intDeveloperDirectoryOffset;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000B8E4 File Offset: 0x00009AE4
		public string Signature
		{
			get
			{
				return this.strSignature;
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000B8FC File Offset: 0x00009AFC
		protected internal void SetSignature(string strSignature)
		{
			this.strSignature = strSignature;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000B908 File Offset: 0x00009B08
		public string ReservedCharacter
		{
			get
			{
				return this.strReservedCharacter;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000B920 File Offset: 0x00009B20
		protected internal void SetReservedCharacter(string strReservedCharacter)
		{
			this.strReservedCharacter = strReservedCharacter;
		}

		// Token: 0x040001FC RID: 508
		private int intExtensionAreaOffset = 0;

		// Token: 0x040001FD RID: 509
		private int intDeveloperDirectoryOffset = 0;

		// Token: 0x040001FE RID: 510
		private string strSignature = string.Empty;

		// Token: 0x040001FF RID: 511
		private string strReservedCharacter = string.Empty;
	}
}
