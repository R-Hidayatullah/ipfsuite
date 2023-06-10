using System;
using System.Collections.Generic;
using System.Drawing;

namespace Paloma
{
	// Token: 0x02000038 RID: 56
	public class TargaHeader
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600009F RID: 159 RVA: 0x0000B49C File Offset: 0x0000969C
		public byte ImageIDLength
		{
			get
			{
				return this.bImageIDLength;
			}
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000B4B4 File Offset: 0x000096B4
		protected internal void SetImageIDLength(byte bImageIDLength)
		{
			this.bImageIDLength = bImageIDLength;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x0000B4C0 File Offset: 0x000096C0
		public ColorMapType ColorMapType
		{
			get
			{
				return this.eColorMapType;
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x0000B4D8 File Offset: 0x000096D8
		protected internal void SetColorMapType(ColorMapType eColorMapType)
		{
			this.eColorMapType = eColorMapType;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x0000B4E4 File Offset: 0x000096E4
		public ImageType ImageType
		{
			get
			{
				return this.eImageType;
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000B4FC File Offset: 0x000096FC
		protected internal void SetImageType(ImageType eImageType)
		{
			this.eImageType = eImageType;
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x0000B508 File Offset: 0x00009708
		public short ColorMapFirstEntryIndex
		{
			get
			{
				return this.sColorMapFirstEntryIndex;
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000B520 File Offset: 0x00009720
		protected internal void SetColorMapFirstEntryIndex(short sColorMapFirstEntryIndex)
		{
			this.sColorMapFirstEntryIndex = sColorMapFirstEntryIndex;
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000B52C File Offset: 0x0000972C
		public short ColorMapLength
		{
			get
			{
				return this.sColorMapLength;
			}
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000B544 File Offset: 0x00009744
		protected internal void SetColorMapLength(short sColorMapLength)
		{
			this.sColorMapLength = sColorMapLength;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x0000B550 File Offset: 0x00009750
		public byte ColorMapEntrySize
		{
			get
			{
				return this.bColorMapEntrySize;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000B568 File Offset: 0x00009768
		protected internal void SetColorMapEntrySize(byte bColorMapEntrySize)
		{
			this.bColorMapEntrySize = bColorMapEntrySize;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000AB RID: 171 RVA: 0x0000B574 File Offset: 0x00009774
		public short XOrigin
		{
			get
			{
				return this.sXOrigin;
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000B58C File Offset: 0x0000978C
		protected internal void SetXOrigin(short sXOrigin)
		{
			this.sXOrigin = sXOrigin;
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000B598 File Offset: 0x00009798
		public short YOrigin
		{
			get
			{
				return this.sYOrigin;
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000B5B0 File Offset: 0x000097B0
		protected internal void SetYOrigin(short sYOrigin)
		{
			this.sYOrigin = sYOrigin;
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000B5BC File Offset: 0x000097BC
		public short Width
		{
			get
			{
				return this.sWidth;
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000B5D4 File Offset: 0x000097D4
		protected internal void SetWidth(short sWidth)
		{
			this.sWidth = sWidth;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000B5E0 File Offset: 0x000097E0
		public short Height
		{
			get
			{
				return this.sHeight;
			}
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x0000B5F8 File Offset: 0x000097F8
		protected internal void SetHeight(short sHeight)
		{
			this.sHeight = sHeight;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x0000B604 File Offset: 0x00009804
		public byte PixelDepth
		{
			get
			{
				return this.bPixelDepth;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x0000B61C File Offset: 0x0000981C
		protected internal void SetPixelDepth(byte bPixelDepth)
		{
			this.bPixelDepth = bPixelDepth;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x0000B628 File Offset: 0x00009828
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000B640 File Offset: 0x00009840
		protected internal byte ImageDescriptor
		{
			get
			{
				return this.bImageDescriptor;
			}
			set
			{
				this.bImageDescriptor = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000B64C File Offset: 0x0000984C
		public FirstPixelDestination FirstPixelDestination
		{
			get
			{
				FirstPixelDestination result;
				if (this.eVerticalTransferOrder == VerticalTransferOrder.UNKNOWN || this.eHorizontalTransferOrder == HorizontalTransferOrder.UNKNOWN)
				{
					result = FirstPixelDestination.UNKNOWN;
				}
				else if (this.eVerticalTransferOrder == VerticalTransferOrder.BOTTOM && this.eHorizontalTransferOrder == HorizontalTransferOrder.LEFT)
				{
					result = FirstPixelDestination.BOTTOM_LEFT;
				}
				else if (this.eVerticalTransferOrder == VerticalTransferOrder.BOTTOM && this.eHorizontalTransferOrder == HorizontalTransferOrder.RIGHT)
				{
					result = FirstPixelDestination.BOTTOM_RIGHT;
				}
				else if (this.eVerticalTransferOrder == VerticalTransferOrder.TOP && this.eHorizontalTransferOrder == HorizontalTransferOrder.LEFT)
				{
					result = FirstPixelDestination.TOP_LEFT;
				}
				else
				{
					result = FirstPixelDestination.TOP_RIGHT;
				}
				return result;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x0000B6E4 File Offset: 0x000098E4
		public VerticalTransferOrder VerticalTransferOrder
		{
			get
			{
				return this.eVerticalTransferOrder;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x0000B6FC File Offset: 0x000098FC
		protected internal void SetVerticalTransferOrder(VerticalTransferOrder eVerticalTransferOrder)
		{
			this.eVerticalTransferOrder = eVerticalTransferOrder;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000BA RID: 186 RVA: 0x0000B708 File Offset: 0x00009908
		public HorizontalTransferOrder HorizontalTransferOrder
		{
			get
			{
				return this.eHorizontalTransferOrder;
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000B720 File Offset: 0x00009920
		protected internal void SetHorizontalTransferOrder(HorizontalTransferOrder eHorizontalTransferOrder)
		{
			this.eHorizontalTransferOrder = eHorizontalTransferOrder;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000B72C File Offset: 0x0000992C
		public byte AttributeBits
		{
			get
			{
				return this.bAttributeBits;
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000B744 File Offset: 0x00009944
		protected internal void SetAttributeBits(byte bAttributeBits)
		{
			this.bAttributeBits = bAttributeBits;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000B750 File Offset: 0x00009950
		public string ImageIDValue
		{
			get
			{
				return this.strImageIDValue;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x0000B768 File Offset: 0x00009968
		protected internal void SetImageIDValue(string strImageIDValue)
		{
			this.strImageIDValue = strImageIDValue;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000B774 File Offset: 0x00009974
		public List<Color> ColorMap
		{
			get
			{
				return this.cColorMap;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000B78C File Offset: 0x0000998C
		public int ImageDataOffset
		{
			get
			{
				int num = 18;
				num += (int)this.bImageIDLength;
				int num2 = 0;
				byte b = this.bColorMapEntrySize;
				switch (b)
				{
				case 15:
					num2 = 2;
					break;
				case 16:
					num2 = 2;
					break;
				default:
					if (b != 24)
					{
						if (b == 32)
						{
							num2 = 4;
						}
					}
					else
					{
						num2 = 3;
					}
					break;
				}
				return num + (int)this.sColorMapLength * num2;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000B7EC File Offset: 0x000099EC
		public int BytesPerPixel
		{
			get
			{
				return (int)(this.bPixelDepth / 8);
			}
		}

		// Token: 0x040001EB RID: 491
		private byte bImageIDLength = 0;

		// Token: 0x040001EC RID: 492
		private ColorMapType eColorMapType = ColorMapType.NO_COLOR_MAP;

		// Token: 0x040001ED RID: 493
		private ImageType eImageType = ImageType.NO_IMAGE_DATA;

		// Token: 0x040001EE RID: 494
		private short sColorMapFirstEntryIndex = 0;

		// Token: 0x040001EF RID: 495
		private short sColorMapLength = 0;

		// Token: 0x040001F0 RID: 496
		private byte bColorMapEntrySize = 0;

		// Token: 0x040001F1 RID: 497
		private short sXOrigin = 0;

		// Token: 0x040001F2 RID: 498
		private short sYOrigin = 0;

		// Token: 0x040001F3 RID: 499
		private short sWidth = 0;

		// Token: 0x040001F4 RID: 500
		private short sHeight = 0;

		// Token: 0x040001F5 RID: 501
		private byte bPixelDepth = 0;

		// Token: 0x040001F6 RID: 502
		private byte bImageDescriptor = 0;

		// Token: 0x040001F7 RID: 503
		private VerticalTransferOrder eVerticalTransferOrder = VerticalTransferOrder.UNKNOWN;

		// Token: 0x040001F8 RID: 504
		private HorizontalTransferOrder eHorizontalTransferOrder = HorizontalTransferOrder.UNKNOWN;

		// Token: 0x040001F9 RID: 505
		private byte bAttributeBits = 0;

		// Token: 0x040001FA RID: 506
		private string strImageIDValue = string.Empty;

		// Token: 0x040001FB RID: 507
		private List<Color> cColorMap = new List<Color>();
	}
}
