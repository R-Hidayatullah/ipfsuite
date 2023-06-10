using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace Paloma
{
	// Token: 0x02000037 RID: 55
	public class TargaImage : IDisposable
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00009A14 File Offset: 0x00007C14
		public TargaImage()
		{
			this.objTargaFooter = new TargaFooter();
			this.objTargaHeader = new TargaHeader();
			this.objTargaExtensionArea = new TargaExtensionArea();
			this.bmpTargaImage = null;
			this.bmpImageThumbnail = null;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00009ABC File Offset: 0x00007CBC
		public TargaHeader Header
		{
			get
			{
				return this.objTargaHeader;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000088 RID: 136 RVA: 0x00009AD4 File Offset: 0x00007CD4
		public TargaExtensionArea ExtensionArea
		{
			get
			{
				return this.objTargaExtensionArea;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00009AEC File Offset: 0x00007CEC
		public TargaFooter Footer
		{
			get
			{
				return this.objTargaFooter;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00009B04 File Offset: 0x00007D04
		public TGAFormat Format
		{
			get
			{
				return this.eTGAFormat;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00009B1C File Offset: 0x00007D1C
		public Bitmap Image
		{
			get
			{
				return this.bmpTargaImage;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00009B34 File Offset: 0x00007D34
		public Bitmap Thumbnail
		{
			get
			{
				return this.bmpImageThumbnail;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00009B4C File Offset: 0x00007D4C
		public string FileName
		{
			get
			{
				return this.strFileName;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00009B64 File Offset: 0x00007D64
		public int Stride
		{
			get
			{
				return this.intStride;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00009B7C File Offset: 0x00007D7C
		public int Padding
		{
			get
			{
				return this.intPadding;
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00009B94 File Offset: 0x00007D94
		~TargaImage()
		{
			this.Dispose(false);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00009BC8 File Offset: 0x00007DC8
		public TargaImage(string strFileName) : this()
		{
			if (!(Path.GetExtension(strFileName).ToLower() == ".tga"))
			{
				throw new Exception("Error loading file, file '" + strFileName + "' must have an extension of '.tga'.");
			}
			if (!File.Exists(strFileName))
			{
				throw new Exception("Error loading file, could not find file '" + strFileName + "' on disk.");
			}
			this.strFileName = strFileName;
			byte[] array = File.ReadAllBytes(this.strFileName);
			if (array != null && array.Length > 0)
			{
				this.LoadTargaFromBytes(array);
				return;
			}
			throw new Exception("Error loading file, could not read file from disk.");
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00009C79 File Offset: 0x00007E79
		public TargaImage(byte[] content) : this()
		{
			this.LoadTargaFromBytes(content);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00009C8C File Offset: 0x00007E8C
		private void LoadTargaFromBytes(byte[] content)
		{
			if (content != null && content.Length > 0)
			{
				using (MemoryStream memoryStream = new MemoryStream(content))
				{
					if (memoryStream == null || memoryStream.Length <= 0L || !memoryStream.CanSeek)
					{
						throw new Exception("Error loading file, could not read file from disk.");
					}
					using (BinaryReader binaryReader = new BinaryReader(memoryStream))
					{
						this.LoadTGAFooterInfo(binaryReader);
						this.LoadTGAHeaderInfo(binaryReader);
						this.LoadTGAExtensionArea(binaryReader);
						this.LoadTGAImage(binaryReader);
					}
				}
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00009D54 File Offset: 0x00007F54
		private void LoadTGAFooterInfo(BinaryReader binReader)
		{
			if (binReader != null && binReader.BaseStream != null && binReader.BaseStream.Length > 0L && binReader.BaseStream.CanSeek)
			{
				try
				{
					binReader.BaseStream.Seek(-18L, SeekOrigin.End);
					string @string = Encoding.ASCII.GetString(binReader.ReadBytes(16));
					char[] trimChars = new char[1];
					string text = @string.TrimEnd(trimChars);
					if (string.Compare(text, "TRUEVISION-XFILE") == 0)
					{
						this.eTGAFormat = TGAFormat.NEW_TGA;
						binReader.BaseStream.Seek(-26L, SeekOrigin.End);
						int extensionAreaOffset = binReader.ReadInt32();
						int developerDirectoryOffset = binReader.ReadInt32();
						binReader.ReadBytes(16);
						string string2 = Encoding.ASCII.GetString(binReader.ReadBytes(1));
						trimChars = new char[1];
						string reservedCharacter = string2.TrimEnd(trimChars);
						this.objTargaFooter.SetExtensionAreaOffset(extensionAreaOffset);
						this.objTargaFooter.SetDeveloperDirectoryOffset(developerDirectoryOffset);
						this.objTargaFooter.SetSignature(text);
						this.objTargaFooter.SetReservedCharacter(reservedCharacter);
					}
					else
					{
						this.eTGAFormat = TGAFormat.ORIGINAL_TGA;
					}
				}
				catch (Exception ex)
				{
					this.ClearAll();
					throw ex;
				}
				return;
			}
			this.ClearAll();
			throw new Exception("Error loading file, could not read file from disk.");
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00009EB4 File Offset: 0x000080B4
		private void LoadTGAHeaderInfo(BinaryReader binReader)
		{
			if (binReader != null && binReader.BaseStream != null && binReader.BaseStream.Length > 0L && binReader.BaseStream.CanSeek)
			{
				try
				{
					binReader.BaseStream.Seek(0L, SeekOrigin.Begin);
					this.objTargaHeader.SetImageIDLength(binReader.ReadByte());
					this.objTargaHeader.SetColorMapType((ColorMapType)binReader.ReadByte());
					this.objTargaHeader.SetImageType((ImageType)binReader.ReadByte());
					this.objTargaHeader.SetColorMapFirstEntryIndex(binReader.ReadInt16());
					this.objTargaHeader.SetColorMapLength(binReader.ReadInt16());
					this.objTargaHeader.SetColorMapEntrySize(binReader.ReadByte());
					this.objTargaHeader.SetXOrigin(binReader.ReadInt16());
					this.objTargaHeader.SetYOrigin(binReader.ReadInt16());
					this.objTargaHeader.SetWidth(binReader.ReadInt16());
					this.objTargaHeader.SetHeight(binReader.ReadInt16());
					byte b = binReader.ReadByte();
					byte b2 = b;
					if (b2 <= 16)
					{
						if (b2 != 8 && b2 != 16)
						{
							goto IL_135;
						}
					}
					else if (b2 != 24 && b2 != 32)
					{
						goto IL_135;
					}
					this.objTargaHeader.SetPixelDepth(b);
					byte b3 = binReader.ReadByte();
					this.objTargaHeader.SetAttributeBits((byte)Utilities.GetBits(b3, 0, 4));
					this.objTargaHeader.SetVerticalTransferOrder((VerticalTransferOrder)Utilities.GetBits(b3, 5, 1));
					this.objTargaHeader.SetHorizontalTransferOrder((HorizontalTransferOrder)Utilities.GetBits(b3, 4, 1));
					if (this.objTargaHeader.ImageIDLength > 0)
					{
						byte[] bytes = binReader.ReadBytes((int)this.objTargaHeader.ImageIDLength);
						TargaHeader targaHeader = this.objTargaHeader;
						string @string = Encoding.ASCII.GetString(bytes);
						char[] trimChars = new char[1];
						targaHeader.SetImageIDValue(@string.TrimEnd(trimChars));
					}
					goto IL_1EA;
					IL_135:
					this.ClearAll();
					throw new Exception("Targa Image only supports 8, 16, 24, or 32 bit pixel depths.");
				}
				catch (Exception ex)
				{
					this.ClearAll();
					throw ex;
				}
				IL_1EA:
				if (this.objTargaHeader.ColorMapType == ColorMapType.COLOR_MAP_INCLUDED)
				{
					if (this.objTargaHeader.ImageType == ImageType.UNCOMPRESSED_COLOR_MAPPED || this.objTargaHeader.ImageType == ImageType.RUN_LENGTH_ENCODED_COLOR_MAPPED)
					{
						if (this.objTargaHeader.ColorMapLength <= 0)
						{
							this.ClearAll();
							throw new Exception("Image Type requires a Color Map and Color Map Length is zero.");
						}
						try
						{
							for (int i = 0; i < (int)this.objTargaHeader.ColorMapLength; i++)
							{
								byte b2 = this.objTargaHeader.ColorMapEntrySize;
								switch (b2)
								{
								case 15:
								{
									byte[] array = binReader.ReadBytes(2);
									this.objTargaHeader.ColorMap.Add(Utilities.GetColorFrom2Bytes(array[1], array[0]));
									break;
								}
								case 16:
								{
									byte[] array2 = binReader.ReadBytes(2);
									this.objTargaHeader.ColorMap.Add(Utilities.GetColorFrom2Bytes(array2[1], array2[0]));
									break;
								}
								default:
									if (b2 != 24)
									{
										if (b2 != 32)
										{
											this.ClearAll();
											throw new Exception("TargaImage only supports ColorMap Entry Sizes of 15, 16, 24 or 32 bits.");
										}
										int alpha = Convert.ToInt32(binReader.ReadByte());
										int blue = Convert.ToInt32(binReader.ReadByte());
										int green = Convert.ToInt32(binReader.ReadByte());
										int red = Convert.ToInt32(binReader.ReadByte());
										this.objTargaHeader.ColorMap.Add(Color.FromArgb(alpha, red, green, blue));
									}
									else
									{
										int blue = Convert.ToInt32(binReader.ReadByte());
										int green = Convert.ToInt32(binReader.ReadByte());
										int red = Convert.ToInt32(binReader.ReadByte());
										this.objTargaHeader.ColorMap.Add(Color.FromArgb(red, green, blue));
									}
									break;
								}
							}
						}
						catch (Exception ex)
						{
							this.ClearAll();
							throw ex;
						}
					}
				}
				else if (this.objTargaHeader.ImageType == ImageType.UNCOMPRESSED_COLOR_MAPPED || this.objTargaHeader.ImageType == ImageType.RUN_LENGTH_ENCODED_COLOR_MAPPED)
				{
					this.ClearAll();
					throw new Exception("Image Type requires a Color Map and there was not a Color Map included in the file.");
				}
				return;
			}
			this.ClearAll();
			throw new Exception("Error loading file, could not read file from disk.");
		}

		// Token: 0x06000096 RID: 150 RVA: 0x0000A330 File Offset: 0x00008530
		private void LoadTGAExtensionArea(BinaryReader binReader)
		{
			if (binReader != null && binReader.BaseStream != null && binReader.BaseStream.Length > 0L && binReader.BaseStream.CanSeek)
			{
				if (this.objTargaFooter.ExtensionAreaOffset > 0)
				{
					try
					{
						binReader.BaseStream.Seek((long)this.objTargaFooter.ExtensionAreaOffset, SeekOrigin.Begin);
						this.objTargaExtensionArea.SetExtensionSize((int)binReader.ReadInt16());
						TargaExtensionArea targaExtensionArea = this.objTargaExtensionArea;
						string @string = Encoding.ASCII.GetString(binReader.ReadBytes(41));
						char[] trimChars = new char[1];
						targaExtensionArea.SetAuthorName(@string.TrimEnd(trimChars));
						TargaExtensionArea targaExtensionArea2 = this.objTargaExtensionArea;
						string string2 = Encoding.ASCII.GetString(binReader.ReadBytes(324));
						trimChars = new char[1];
						targaExtensionArea2.SetAuthorComments(string2.TrimEnd(trimChars));
						short num = binReader.ReadInt16();
						short num2 = binReader.ReadInt16();
						short num3 = binReader.ReadInt16();
						short hours = binReader.ReadInt16();
						short minutes = binReader.ReadInt16();
						short seconds = binReader.ReadInt16();
						string text = string.Concat(new string[]
						{
							num.ToString(),
							"/",
							num2.ToString(),
							"/",
							num3.ToString(),
							" "
						});
						string text2 = text;
						text = string.Concat(new string[]
						{
							text2,
							hours.ToString(),
							":",
							minutes.ToString(),
							":",
							seconds.ToString()
						});
						DateTime dateTimeStamp;
						if (DateTime.TryParse(text, out dateTimeStamp))
						{
							this.objTargaExtensionArea.SetDateTimeStamp(dateTimeStamp);
						}
						TargaExtensionArea targaExtensionArea3 = this.objTargaExtensionArea;
						string string3 = Encoding.ASCII.GetString(binReader.ReadBytes(41));
						trimChars = new char[1];
						targaExtensionArea3.SetJobName(string3.TrimEnd(trimChars));
						hours = binReader.ReadInt16();
						minutes = binReader.ReadInt16();
						seconds = binReader.ReadInt16();
						TimeSpan jobTime = new TimeSpan((int)hours, (int)minutes, (int)seconds);
						this.objTargaExtensionArea.SetJobTime(jobTime);
						TargaExtensionArea targaExtensionArea4 = this.objTargaExtensionArea;
						string string4 = Encoding.ASCII.GetString(binReader.ReadBytes(41));
						trimChars = new char[1];
						targaExtensionArea4.SetSoftwareID(string4.TrimEnd(trimChars));
						float num4 = (float)binReader.ReadInt16() / 100f;
						string string5 = Encoding.ASCII.GetString(binReader.ReadBytes(1));
						trimChars = new char[1];
						string str = string5.TrimEnd(trimChars);
						this.objTargaExtensionArea.SetSoftwareID(num4.ToString("F2") + str);
						int alpha = (int)binReader.ReadByte();
						int red = (int)binReader.ReadByte();
						int blue = (int)binReader.ReadByte();
						int green = (int)binReader.ReadByte();
						this.objTargaExtensionArea.SetKeyColor(Color.FromArgb(alpha, red, green, blue));
						this.objTargaExtensionArea.SetPixelAspectRatioNumerator((int)binReader.ReadInt16());
						this.objTargaExtensionArea.SetPixelAspectRatioDenominator((int)binReader.ReadInt16());
						this.objTargaExtensionArea.SetGammaNumerator((int)binReader.ReadInt16());
						this.objTargaExtensionArea.SetGammaDenominator((int)binReader.ReadInt16());
						this.objTargaExtensionArea.SetColorCorrectionOffset(binReader.ReadInt32());
						this.objTargaExtensionArea.SetPostageStampOffset(binReader.ReadInt32());
						this.objTargaExtensionArea.SetScanLineOffset(binReader.ReadInt32());
						this.objTargaExtensionArea.SetAttributesType((int)binReader.ReadByte());
						if (this.objTargaExtensionArea.ScanLineOffset > 0)
						{
							binReader.BaseStream.Seek((long)this.objTargaExtensionArea.ScanLineOffset, SeekOrigin.Begin);
							for (int i = 0; i < (int)this.objTargaHeader.Height; i++)
							{
								this.objTargaExtensionArea.ScanLineTable.Add(binReader.ReadInt32());
							}
						}
						if (this.objTargaExtensionArea.ColorCorrectionOffset > 0)
						{
							binReader.BaseStream.Seek((long)this.objTargaExtensionArea.ColorCorrectionOffset, SeekOrigin.Begin);
							for (int i = 0; i < 256; i++)
							{
								alpha = (int)binReader.ReadInt16();
								red = (int)binReader.ReadInt16();
								blue = (int)binReader.ReadInt16();
								green = (int)binReader.ReadInt16();
								this.objTargaExtensionArea.ColorCorrectionTable.Add(Color.FromArgb(alpha, red, green, blue));
							}
						}
					}
					catch (Exception ex)
					{
						this.ClearAll();
						throw ex;
					}
				}
				return;
			}
			this.ClearAll();
			throw new Exception("Error loading file, could not read file from disk.");
		}

		// Token: 0x06000097 RID: 151 RVA: 0x0000A7E0 File Offset: 0x000089E0
		private byte[] LoadImageBytes(BinaryReader binReader)
		{
			byte[] result = null;
			if (binReader == null || binReader.BaseStream == null || binReader.BaseStream.Length <= 0L || !binReader.BaseStream.CanSeek)
			{
				this.ClearAll();
				throw new Exception("Error loading file, could not read file from disk.");
			}
			if (this.objTargaHeader.ImageDataOffset > 0)
			{
				byte[] array = new byte[this.intPadding];
				binReader.BaseStream.Seek((long)this.objTargaHeader.ImageDataOffset, SeekOrigin.Begin);
				int num = (int)this.objTargaHeader.Width * this.objTargaHeader.BytesPerPixel;
				int num2 = num * (int)this.objTargaHeader.Height;
				if (this.objTargaHeader.ImageType == ImageType.RUN_LENGTH_ENCODED_BLACK_AND_WHITE || this.objTargaHeader.ImageType == ImageType.RUN_LENGTH_ENCODED_COLOR_MAPPED || this.objTargaHeader.ImageType == ImageType.RUN_LENGTH_ENCODED_TRUE_COLOR)
				{
					int i = 0;
					int num3 = 0;
					while (i < num2)
					{
						byte b = binReader.ReadByte();
						int bits = Utilities.GetBits(b, 7, 1);
						int num4 = Utilities.GetBits(b, 0, 7) + 1;
						if (bits == 1)
						{
							byte[] array2 = binReader.ReadBytes(this.objTargaHeader.BytesPerPixel);
							for (int j = 0; j < num4; j++)
							{
								foreach (byte item in array2)
								{
									this.row.Add(item);
								}
								num3 += array2.Length;
								i += array2.Length;
								if (num3 == num)
								{
									this.rows.Add(this.row);
									this.row = new List<byte>();
									num3 = 0;
								}
							}
						}
						else if (bits == 0)
						{
							int num5 = num4 * this.objTargaHeader.BytesPerPixel;
							for (int j = 0; j < num5; j++)
							{
								this.row.Add(binReader.ReadByte());
								i++;
								num3++;
								if (num3 == num)
								{
									this.rows.Add(this.row);
									this.row = new List<byte>();
									num3 = 0;
								}
							}
						}
					}
				}
				else
				{
					for (int j = 0; j < (int)this.objTargaHeader.Height; j++)
					{
						for (int l = 0; l < num; l++)
						{
							this.row.Add(binReader.ReadByte());
						}
						this.rows.Add(this.row);
						this.row = new List<byte>();
					}
				}
				bool flag = false;
				bool flag2 = false;
				switch (this.objTargaHeader.FirstPixelDestination)
				{
				case FirstPixelDestination.UNKNOWN:
				case FirstPixelDestination.BOTTOM_RIGHT:
					flag = true;
					flag2 = false;
					break;
				case FirstPixelDestination.TOP_LEFT:
					flag = false;
					flag2 = true;
					break;
				case FirstPixelDestination.TOP_RIGHT:
					flag = false;
					flag2 = false;
					break;
				case FirstPixelDestination.BOTTOM_LEFT:
					flag = true;
					flag2 = true;
					break;
				}
				MemoryStream memoryStream2;
				MemoryStream memoryStream = memoryStream2 = new MemoryStream();
				try
				{
					if (flag)
					{
						this.rows.Reverse();
					}
					for (int j = 0; j < this.rows.Count; j++)
					{
						if (flag2)
						{
							this.rows[j].Reverse();
						}
						byte[] array4 = this.rows[j].ToArray();
						memoryStream.Write(array4, 0, array4.Length);
						memoryStream.Write(array, 0, array.Length);
					}
					result = memoryStream.ToArray();
				}
				finally
				{
					if (memoryStream2 != null)
					{
						((IDisposable)memoryStream2).Dispose();
					}
				}
				return result;
			}
			this.ClearAll();
			throw new Exception("Error loading file, No image data in file.");
		}

		// Token: 0x06000098 RID: 152 RVA: 0x0000AC08 File Offset: 0x00008E08
		private void LoadTGAImage(BinaryReader binReader)
		{
			this.intStride = (this.objTargaHeader.Width * (short)this.objTargaHeader.PixelDepth + 31 & -32) >> 3;
			this.intPadding = this.intStride - (int)((this.objTargaHeader.Width * (short)this.objTargaHeader.PixelDepth + 7) / 8);
			byte[] value = this.LoadImageBytes(binReader);
			this.ImageByteHandle = GCHandle.Alloc(value, GCHandleType.Pinned);
			if (this.bmpTargaImage != null)
			{
				this.bmpTargaImage.Dispose();
			}
			if (this.bmpImageThumbnail != null)
			{
				this.bmpImageThumbnail.Dispose();
			}
			PixelFormat pixelFormat = this.GetPixelFormat();
			this.bmpTargaImage = new Bitmap((int)this.objTargaHeader.Width, (int)this.objTargaHeader.Height, this.intStride, pixelFormat, this.ImageByteHandle.AddrOfPinnedObject());
			this.LoadThumbnail(binReader, pixelFormat);
			if (this.objTargaHeader.ColorMap.Count > 0)
			{
				ColorPalette palette = this.bmpTargaImage.Palette;
				for (int i = 0; i < this.objTargaHeader.ColorMap.Count; i++)
				{
					if (this.objTargaExtensionArea.AttributesType == 0 || this.objTargaExtensionArea.AttributesType == 1)
					{
						palette.Entries[i] = Color.FromArgb(255, (int)this.objTargaHeader.ColorMap[i].R, (int)this.objTargaHeader.ColorMap[i].G, (int)this.objTargaHeader.ColorMap[i].B);
					}
					else
					{
						palette.Entries[i] = this.objTargaHeader.ColorMap[i];
					}
				}
				this.bmpTargaImage.Palette = palette;
				if (this.bmpImageThumbnail != null)
				{
					this.bmpImageThumbnail.Palette = palette;
				}
			}
			else if (this.objTargaHeader.PixelDepth == 8 && (this.objTargaHeader.ImageType == ImageType.UNCOMPRESSED_BLACK_AND_WHITE || this.objTargaHeader.ImageType == ImageType.RUN_LENGTH_ENCODED_BLACK_AND_WHITE))
			{
				ColorPalette palette = this.bmpTargaImage.Palette;
				for (int i = 0; i < 256; i++)
				{
					palette.Entries[i] = Color.FromArgb(i, i, i);
				}
				this.bmpTargaImage.Palette = palette;
				if (this.bmpImageThumbnail != null)
				{
					this.bmpImageThumbnail.Palette = palette;
				}
			}
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000AED8 File Offset: 0x000090D8
		private PixelFormat GetPixelFormat()
		{
			PixelFormat result = PixelFormat.Undefined;
			byte pixelDepth = this.objTargaHeader.PixelDepth;
			if (pixelDepth <= 16)
			{
				if (pixelDepth != 8)
				{
					if (pixelDepth == 16)
					{
						if (this.Format == TGAFormat.NEW_TGA)
						{
							switch (this.objTargaExtensionArea.AttributesType)
							{
							case 0:
							case 1:
							case 2:
								result = PixelFormat.Format16bppRgb555;
								break;
							case 3:
								result = PixelFormat.Format16bppArgb1555;
								break;
							}
						}
						else
						{
							result = PixelFormat.Format16bppRgb555;
						}
					}
				}
				else
				{
					result = PixelFormat.Format8bppIndexed;
				}
			}
			else if (pixelDepth != 24)
			{
				if (pixelDepth == 32)
				{
					if (this.Format == TGAFormat.NEW_TGA)
					{
						switch (this.objTargaExtensionArea.AttributesType)
						{
						case 0:
						case 3:
							result = PixelFormat.Format32bppArgb;
							break;
						case 1:
						case 2:
							result = PixelFormat.Format32bppRgb;
							break;
						case 4:
							result = PixelFormat.Format32bppPArgb;
							break;
						}
					}
					else
					{
						result = PixelFormat.Format32bppRgb;
					}
				}
			}
			else
			{
				result = PixelFormat.Format24bppRgb;
			}
			return result;
		}

		// Token: 0x0600009A RID: 154 RVA: 0x0000AFEC File Offset: 0x000091EC
		private void LoadThumbnail(BinaryReader binReader, PixelFormat pfPixelFormat)
		{
			byte[] array = null;
			if (binReader != null && binReader.BaseStream != null && binReader.BaseStream.Length > 0L && binReader.BaseStream.CanSeek)
			{
				if (this.ExtensionArea.PostageStampOffset > 0)
				{
					binReader.BaseStream.Seek((long)this.ExtensionArea.PostageStampOffset, SeekOrigin.Begin);
					int num = (int)binReader.ReadByte();
					int num2 = (int)binReader.ReadByte();
					int num3 = (num * (int)this.objTargaHeader.PixelDepth + 31 & -32) >> 3;
					int num4 = num3 - (num * (int)this.objTargaHeader.PixelDepth + 7) / 8;
					List<List<byte>> list = new List<List<byte>>();
					List<byte> list2 = new List<byte>();
					byte[] array2 = new byte[num4];
					bool flag = false;
					bool flag2 = false;
					MemoryStream memoryStream2;
					MemoryStream memoryStream = memoryStream2 = new MemoryStream();
					try
					{
						int num5 = num * (int)(this.objTargaHeader.PixelDepth / 8);
						int num6 = num5 * num2;
						for (int i = 0; i < num2; i++)
						{
							for (int j = 0; j < num5; j++)
							{
								list2.Add(binReader.ReadByte());
							}
							list.Add(list2);
							list2 = new List<byte>();
						}
						switch (this.objTargaHeader.FirstPixelDestination)
						{
						case FirstPixelDestination.UNKNOWN:
						case FirstPixelDestination.BOTTOM_RIGHT:
							flag2 = true;
							flag = false;
							break;
						case FirstPixelDestination.TOP_RIGHT:
							flag2 = false;
							flag = false;
							break;
						}
						if (flag2)
						{
							list.Reverse();
						}
						for (int i = 0; i < list.Count; i++)
						{
							if (flag)
							{
								list[i].Reverse();
							}
							byte[] array3 = list[i].ToArray();
							memoryStream.Write(array3, 0, array3.Length);
							memoryStream.Write(array2, 0, array2.Length);
						}
						array = memoryStream.ToArray();
					}
					finally
					{
						if (memoryStream2 != null)
						{
							((IDisposable)memoryStream2).Dispose();
						}
					}
					if (array != null && array.Length > 0)
					{
						this.ThumbnailByteHandle = GCHandle.Alloc(array, GCHandleType.Pinned);
						this.bmpImageThumbnail = new Bitmap(num, num2, num3, pfPixelFormat, this.ThumbnailByteHandle.AddrOfPinnedObject());
					}
				}
				else if (this.bmpImageThumbnail != null)
				{
					this.bmpImageThumbnail.Dispose();
					this.bmpImageThumbnail = null;
				}
			}
			else if (this.bmpImageThumbnail != null)
			{
				this.bmpImageThumbnail.Dispose();
				this.bmpImageThumbnail = null;
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000B2B8 File Offset: 0x000094B8
		private void ClearAll()
		{
			if (this.bmpTargaImage != null)
			{
				this.bmpTargaImage.Dispose();
				this.bmpTargaImage = null;
			}
			if (this.ImageByteHandle.IsAllocated)
			{
				this.ImageByteHandle.Free();
			}
			if (this.ThumbnailByteHandle.IsAllocated)
			{
				this.ThumbnailByteHandle.Free();
			}
			this.objTargaHeader = new TargaHeader();
			this.objTargaExtensionArea = new TargaExtensionArea();
			this.objTargaFooter = new TargaFooter();
			this.eTGAFormat = TGAFormat.UNKNOWN;
			this.intStride = 0;
			this.intPadding = 0;
			this.rows.Clear();
			this.row.Clear();
			this.strFileName = string.Empty;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x0000B380 File Offset: 0x00009580
		public static Bitmap LoadTargaImage(string sFileName)
		{
			Bitmap result = null;
			using (TargaImage targaImage = new TargaImage(sFileName))
			{
				result = new Bitmap(targaImage.Image);
			}
			return result;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000B3D0 File Offset: 0x000095D0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000B3E4 File Offset: 0x000095E4
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.bmpTargaImage != null)
					{
						this.bmpTargaImage.Dispose();
					}
					if (this.bmpImageThumbnail != null)
					{
						this.bmpImageThumbnail.Dispose();
					}
					bool flag = 1 == 0;
					if (this.ImageByteHandle.IsAllocated)
					{
						this.ImageByteHandle.Free();
					}
					flag = (1 == 0);
					if (this.ThumbnailByteHandle.IsAllocated)
					{
						this.ThumbnailByteHandle.Free();
					}
				}
			}
			this.disposed = true;
		}

		// Token: 0x040001DD RID: 477
		private TargaHeader objTargaHeader = null;

		// Token: 0x040001DE RID: 478
		private TargaExtensionArea objTargaExtensionArea = null;

		// Token: 0x040001DF RID: 479
		private TargaFooter objTargaFooter = null;

		// Token: 0x040001E0 RID: 480
		private Bitmap bmpTargaImage = null;

		// Token: 0x040001E1 RID: 481
		private Bitmap bmpImageThumbnail = null;

		// Token: 0x040001E2 RID: 482
		private TGAFormat eTGAFormat = TGAFormat.UNKNOWN;

		// Token: 0x040001E3 RID: 483
		private string strFileName = string.Empty;

		// Token: 0x040001E4 RID: 484
		private int intStride = 0;

		// Token: 0x040001E5 RID: 485
		private int intPadding = 0;

		// Token: 0x040001E6 RID: 486
		private GCHandle ImageByteHandle;

		// Token: 0x040001E7 RID: 487
		private GCHandle ThumbnailByteHandle;

		// Token: 0x040001E8 RID: 488
		private List<List<byte>> rows = new List<List<byte>>();

		// Token: 0x040001E9 RID: 489
		private List<byte> row = new List<byte>();

		// Token: 0x040001EA RID: 490
		private bool disposed = false;
	}
}
