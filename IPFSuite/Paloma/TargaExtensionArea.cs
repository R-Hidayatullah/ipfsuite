using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paloma
{
	// Token: 0x0200003A RID: 58
	public class TargaExtensionArea
	{
		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000CD RID: 205 RVA: 0x0000B95C File Offset: 0x00009B5C
		public int ExtensionSize
		{
			get
			{
				return this.intExtensionSize;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x0000B974 File Offset: 0x00009B74
		protected internal void SetExtensionSize(int intExtensionSize)
		{
			this.intExtensionSize = intExtensionSize;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000CF RID: 207 RVA: 0x0000B980 File Offset: 0x00009B80
		public string AuthorName
		{
			get
			{
				return this.strAuthorName;
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000B998 File Offset: 0x00009B98
		protected internal void SetAuthorName(string strAuthorName)
		{
			this.strAuthorName = strAuthorName;
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x0000B9A4 File Offset: 0x00009BA4
		public string AuthorComments
		{
			get
			{
				return this.strAuthorComments;
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x0000B9BC File Offset: 0x00009BBC
		protected internal void SetAuthorComments(string strAuthorComments)
		{
			this.strAuthorComments = strAuthorComments;
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000B9C8 File Offset: 0x00009BC8
		public DateTime DateTimeStamp
		{
			get
			{
				return this.dtDateTimeStamp;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000B9E0 File Offset: 0x00009BE0
		protected internal void SetDateTimeStamp(DateTime dtDateTimeStamp)
		{
			this.dtDateTimeStamp = dtDateTimeStamp;
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x0000B9EC File Offset: 0x00009BEC
		public string JobName
		{
			get
			{
				return this.strJobName;
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000BA04 File Offset: 0x00009C04
		protected internal void SetJobName(string strJobName)
		{
			this.strJobName = strJobName;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000D7 RID: 215 RVA: 0x0000BA10 File Offset: 0x00009C10
		public TimeSpan JobTime
		{
			get
			{
				return this.dtJobTime;
			}
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x0000BA28 File Offset: 0x00009C28
		protected internal void SetJobTime(TimeSpan dtJobTime)
		{
			this.dtJobTime = dtJobTime;
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x0000BA34 File Offset: 0x00009C34
		public string SoftwareID
		{
			get
			{
				return this.strSoftwareID;
			}
		}

		// Token: 0x060000DA RID: 218 RVA: 0x0000BA4C File Offset: 0x00009C4C
		protected internal void SetSoftwareID(string strSoftwareID)
		{
			this.strSoftwareID = strSoftwareID;
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000DB RID: 219 RVA: 0x0000BA58 File Offset: 0x00009C58
		public string SoftwareVersion
		{
			get
			{
				return this.strSoftwareVersion;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000BA70 File Offset: 0x00009C70
		protected internal void SetSoftwareVersion(string strSoftwareVersion)
		{
			this.strSoftwareVersion = strSoftwareVersion;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000DD RID: 221 RVA: 0x0000BA7C File Offset: 0x00009C7C
		public Color KeyColor
		{
			get
			{
				return this.cKeyColor;
			}
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000BA94 File Offset: 0x00009C94
		protected internal void SetKeyColor(Color cKeyColor)
		{
			this.cKeyColor = cKeyColor;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000DF RID: 223 RVA: 0x0000BAA0 File Offset: 0x00009CA0
		public int PixelAspectRatioNumerator
		{
			get
			{
				return this.intPixelAspectRatioNumerator;
			}
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000BAB8 File Offset: 0x00009CB8
		protected internal void SetPixelAspectRatioNumerator(int intPixelAspectRatioNumerator)
		{
			this.intPixelAspectRatioNumerator = intPixelAspectRatioNumerator;
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E1 RID: 225 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		public int PixelAspectRatioDenominator
		{
			get
			{
				return this.intPixelAspectRatioDenominator;
			}
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000BADC File Offset: 0x00009CDC
		protected internal void SetPixelAspectRatioDenominator(int intPixelAspectRatioDenominator)
		{
			this.intPixelAspectRatioDenominator = intPixelAspectRatioDenominator;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x0000BAE8 File Offset: 0x00009CE8
		public float PixelAspectRatio
		{
			get
			{
				float result;
				if (this.intPixelAspectRatioDenominator > 0)
				{
					result = (float)this.intPixelAspectRatioNumerator / (float)this.intPixelAspectRatioDenominator;
				}
				else
				{
					result = 0f;
				}
				return result;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x0000BB24 File Offset: 0x00009D24
		public int GammaNumerator
		{
			get
			{
				return this.intGammaNumerator;
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x0000BB3C File Offset: 0x00009D3C
		protected internal void SetGammaNumerator(int intGammaNumerator)
		{
			this.intGammaNumerator = intGammaNumerator;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x0000BB48 File Offset: 0x00009D48
		public int GammaDenominator
		{
			get
			{
				return this.intGammaDenominator;
			}
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000BB60 File Offset: 0x00009D60
		protected internal void SetGammaDenominator(int intGammaDenominator)
		{
			this.intGammaDenominator = intGammaDenominator;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public float GammaRatio
		{
			get
			{
				float result;
				if (this.intGammaDenominator > 0)
				{
					float num = (float)this.intGammaNumerator / (float)this.intGammaDenominator;
					result = (float)Math.Round((double)num, 1);
				}
				else
				{
					result = 1f;
				}
				return result;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000BBB0 File Offset: 0x00009DB0
		public int ColorCorrectionOffset
		{
			get
			{
				return this.intColorCorrectionOffset;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x0000BBC8 File Offset: 0x00009DC8
		protected internal void SetColorCorrectionOffset(int intColorCorrectionOffset)
		{
			this.intColorCorrectionOffset = intColorCorrectionOffset;
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000EB RID: 235 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		public int PostageStampOffset
		{
			get
			{
				return this.intPostageStampOffset;
			}
		}

		// Token: 0x060000EC RID: 236 RVA: 0x0000BBEC File Offset: 0x00009DEC
		protected internal void SetPostageStampOffset(int intPostageStampOffset)
		{
			this.intPostageStampOffset = intPostageStampOffset;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		public int ScanLineOffset
		{
			get
			{
				return this.intScanLineOffset;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000BC10 File Offset: 0x00009E10
		protected internal void SetScanLineOffset(int intScanLineOffset)
		{
			this.intScanLineOffset = intScanLineOffset;
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000EF RID: 239 RVA: 0x0000BC1C File Offset: 0x00009E1C
		public int AttributesType
		{
			get
			{
				return this.intAttributesType;
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x0000BC34 File Offset: 0x00009E34
		protected internal void SetAttributesType(int intAttributesType)
		{
			this.intAttributesType = intAttributesType;
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x0000BC40 File Offset: 0x00009E40
		public List<int> ScanLineTable
		{
			get
			{
				return this.intScanLineTable;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000BC58 File Offset: 0x00009E58
		public List<Color> ColorCorrectionTable
		{
			get
			{
				return this.cColorCorrectionTable;
			}
		}

		// Token: 0x04000200 RID: 512
		private int intExtensionSize = 0;

		// Token: 0x04000201 RID: 513
		private string strAuthorName = string.Empty;

		// Token: 0x04000202 RID: 514
		private string strAuthorComments = string.Empty;

		// Token: 0x04000203 RID: 515
		private DateTime dtDateTimeStamp = DateTime.Now;

		// Token: 0x04000204 RID: 516
		private string strJobName = string.Empty;

		// Token: 0x04000205 RID: 517
		private TimeSpan dtJobTime = TimeSpan.Zero;

		// Token: 0x04000206 RID: 518
		private string strSoftwareID = string.Empty;

		// Token: 0x04000207 RID: 519
		private string strSoftwareVersion = string.Empty;

		// Token: 0x04000208 RID: 520
		private Color cKeyColor = Color.Empty;

		// Token: 0x04000209 RID: 521
		private int intPixelAspectRatioNumerator = 0;

		// Token: 0x0400020A RID: 522
		private int intPixelAspectRatioDenominator = 0;

		// Token: 0x0400020B RID: 523
		private int intGammaNumerator = 0;

		// Token: 0x0400020C RID: 524
		private int intGammaDenominator = 0;

		// Token: 0x0400020D RID: 525
		private int intColorCorrectionOffset = 0;

		// Token: 0x0400020E RID: 526
		private int intPostageStampOffset = 0;

		// Token: 0x0400020F RID: 527
		private int intScanLineOffset = 0;

		// Token: 0x04000210 RID: 528
		private int intAttributesType = 0;

		// Token: 0x04000211 RID: 529
		private List<int> intScanLineTable = new List<int>();

		// Token: 0x04000212 RID: 530
		private List<Color> cColorCorrectionTable = new List<Color>();
	}
}
