using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using SereniaBLPLib;

namespace KUtility
{
	// Token: 0x02000004 RID: 4
	public class DDSImage
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002418 File Offset: 0x00000618
		public DDSImage(byte[] rawdata)
		{
			using (MemoryStream memoryStream = new MemoryStream(rawdata))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					this.dwMagic = binaryReader.ReadInt32();
					if (this.dwMagic != 542327876)
					{
						throw new Exception("This is not a DDS!");
					}
					this.Read_DDS_HEADER(this.header, binaryReader);
					if ((this.header.ddspf.dwFlags & 4) != 0 && this.header.ddspf.dwFourCC == 808540228)
					{
						throw new Exception("DX10 not supported yet!");
					}
					int num = 1;
					if ((this.header.dwFlags & 131072) != 0)
					{
						num = this.header.dwMipMapCount;
					}
					Func<uint, bool> func = (uint x) => x != 0U && (x & x - 1U) == 0U;
					if (!func((uint)this.header.dwWidth) || !func((uint)this.header.dwHeight))
					{
						num = 1;
					}
					this.images = new Bitmap[num];
					this.bdata = binaryReader.ReadBytes(this.header.dwPitchOrLinearSize);
					for (int i = 0; i < num; i++)
					{
						int w = (int)((double)this.header.dwWidth / Math.Pow(2.0, (double)i));
						int h = (int)((double)this.header.dwHeight / Math.Pow(2.0, (double)i));
						if ((this.header.ddspf.dwFlags & 64) != 0)
						{
							this.images[i] = this.readLinearImage(this.bdata, w, h);
						}
						else if ((this.header.ddspf.dwFlags & 4) != 0)
						{
							this.images[i] = this.readBlockImage(this.bdata, w, h);
						}
						else if ((this.header.ddspf.dwFlags & 4) == 0 && this.header.ddspf.dwRGBBitCount == 16 && this.header.ddspf.dwRBitMask == 255 && this.header.ddspf.dwGBitMask == 65280 && this.header.ddspf.dwBBitMask == 0 && this.header.ddspf.dwABitMask == 0)
						{
							this.images[i] = this.UncompressV8U8(this.bdata, w, h);
						}
					}
				}
			}
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002744 File Offset: 0x00000944
		private Bitmap readBlockImage(byte[] data, int w, int h)
		{
			int dwFourCC = this.header.ddspf.dwFourCC;
			Bitmap result;
			if (dwFourCC != 827611204)
			{
				if (dwFourCC != 861165636)
				{
					if (dwFourCC != 894720068)
					{
						throw new Exception(string.Format("0x{0} texture compression not implemented.", this.header.ddspf.dwFourCC.ToString("X")));
					}
					result = this.UncompressDXT5(data, w, h);
				}
				else
				{
					result = this.UncompressDXT3(data, w, h);
				}
			}
			else
			{
				result = this.UncompressDXT1(data, w, h);
			}
			return result;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000027D0 File Offset: 0x000009D0
		private Bitmap UncompressDXT3(byte[] data, int w, int h)
		{
			w = ((w < 4) ? 4 : w);
			h = ((h < 4) ? 4 : h);
			Bitmap bitmap = new Bitmap(w, h);
			byte[] array;
			DXTDecompression.decompressImage(out array, w, h, data, 2);
			Rectangle rect = new Rectangle(0, 0, w, h);
			BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
			IntPtr scan = bitmapData.Scan0;
			Marshal.Copy(array, 0, scan, array.Length);
			bitmap.UnlockBits(bitmapData);
			return bitmap;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002848 File Offset: 0x00000A48
		private Bitmap UncompressDXT1(byte[] data, int w, int h)
		{
			Bitmap bitmap = new Bitmap((w < 4) ? 4 : w, (h < 4) ? 4 : h);
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					for (int i = 0; i < h; i += 4)
					{
						for (int j = 0; j < w; j += 4)
						{
							this.DecompressBlockDXT1(j, i, binaryReader.ReadBytes(8), bitmap);
						}
					}
				}
			}
			return bitmap;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000290C File Offset: 0x00000B0C
		private void DecompressBlockDXT1(int x, int y, byte[] blockStorage, Bitmap image)
		{
			ushort num = (ushort)((int)blockStorage[0] | (int)blockStorage[1] << 8);
			ushort num2 = (ushort)((int)blockStorage[2] | (int)blockStorage[3] << 8);
			int num3 = (num >> 11) * 255 + 16;
			byte b = (byte)((num3 / 32 + num3) / 32);
			num3 = ((num & 2016) >> 5) * 255 + 32;
			byte b2 = (byte)((num3 / 64 + num3) / 64);
			num3 = (int)((num & 31) * 255 + 16);
			byte b3 = (byte)((num3 / 32 + num3) / 32);
			num3 = (num2 >> 11) * 255 + 16;
			byte b4 = (byte)((num3 / 32 + num3) / 32);
			num3 = ((num2 & 2016) >> 5) * 255 + 32;
			byte b5 = (byte)((num3 / 64 + num3) / 64);
			num3 = (int)((num2 & 31) * 255 + 16);
			byte b6 = (byte)((num3 / 32 + num3) / 32);
			uint num4 = (uint)((int)blockStorage[4] | (int)blockStorage[5] << 8 | (int)blockStorage[6] << 16 | (int)blockStorage[7] << 24);
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					Color color = Color.FromArgb(0);
					byte b7 = (byte)(num4 >> 2 * (4 * i + j) & 3U);
					if (num > num2)
					{
						switch (b7)
						{
						case 0:
							color = Color.FromArgb(255, (int)b, (int)b2, (int)b3);
							break;
						case 1:
							color = Color.FromArgb(255, (int)b4, (int)b5, (int)b6);
							break;
						case 2:
							color = Color.FromArgb(255, (int)((2 * b + b4) / 3), (int)((2 * b2 + b5) / 3), (int)((2 * b3 + b6) / 3));
							break;
						case 3:
							color = Color.FromArgb(255, (int)((b + 2 * b4) / 3), (int)((b2 + 2 * b5) / 3), (int)((b3 + 2 * b6) / 3));
							break;
						}
					}
					else
					{
						switch (b7)
						{
						case 0:
							color = Color.FromArgb(255, (int)b, (int)b2, (int)b3);
							break;
						case 1:
							color = Color.FromArgb(255, (int)b4, (int)b5, (int)b6);
							break;
						case 2:
							color = Color.FromArgb(255, (int)((b + b4) / 2), (int)((b2 + b5) / 2), (int)((b3 + b6) / 2));
							break;
						case 3:
							color = Color.FromArgb(255, 0, 0, 0);
							break;
						}
					}
					image.SetPixel(x + j, y + i, color);
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x00002B7C File Offset: 0x00000D7C
		private Bitmap UncompressDXT5(byte[] data, int w, int h)
		{
			Bitmap bitmap = new Bitmap((w < 4) ? 4 : w, (h < 4) ? 4 : h);
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					for (int i = 0; i < h; i += 4)
					{
						for (int j = 0; j < w; j += 4)
						{
							this.DecompressBlockDXT5(j, i, binaryReader.ReadBytes(16), bitmap);
						}
					}
				}
			}
			return bitmap;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002C40 File Offset: 0x00000E40
		private void DecompressBlockDXT5(int x, int y, byte[] blockStorage, Bitmap image)
		{
			byte b = blockStorage[0];
			byte b2 = blockStorage[1];
			int num = 2;
			uint num2 = (uint)((int)blockStorage[num + 2] | (int)blockStorage[num + 3] << 8 | (int)blockStorage[num + 4] << 16 | (int)blockStorage[num + 5] << 24);
			ushort num3 = (ushort)((int)blockStorage[num] | (int)blockStorage[num + 1] << 8);
			ushort num4 = (ushort)((int)blockStorage[8] | (int)blockStorage[9] << 8);
			ushort num5 = (ushort)((int)blockStorage[10] | (int)blockStorage[11] << 8);
			int num6 = (num4 >> 11) * 255 + 16;
			byte b3 = (byte)((num6 / 32 + num6) / 32);
			num6 = ((num4 & 2016) >> 5) * 255 + 32;
			byte b4 = (byte)((num6 / 64 + num6) / 64);
			num6 = (int)((num4 & 31) * 255 + 16);
			byte b5 = (byte)((num6 / 32 + num6) / 32);
			num6 = (num5 >> 11) * 255 + 16;
			byte b6 = (byte)((num6 / 32 + num6) / 32);
			num6 = ((num5 & 2016) >> 5) * 255 + 32;
			byte b7 = (byte)((num6 / 64 + num6) / 64);
			num6 = (int)((num5 & 31) * 255 + 16);
			byte b8 = (byte)((num6 / 32 + num6) / 32);
			uint num7 = (uint)((int)blockStorage[12] | (int)blockStorage[13] << 8 | (int)blockStorage[14] << 16 | (int)blockStorage[15] << 24);
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < 4; j++)
				{
					int num8 = 3 * (4 * i + j);
					int num9;
					if (num8 <= 12)
					{
						num9 = (num3 >> num8 & 7);
					}
					else if (num8 == 15)
					{
						num9 = (int)((long)(num3 >> 15) | (long)((ulong)(num2 << 1 & 6U)));
					}
					else
					{
						num9 = (int)(num2 >> num8 - 16 & 7U);
					}
					byte alpha;
					if (num9 == 0)
					{
						alpha = b;
					}
					else if (num9 == 1)
					{
						alpha = b2;
					}
					else if (b > b2)
					{
						alpha = (byte)(((8 - num9) * (int)b + (num9 - 1) * (int)b2) / 7);
					}
					else if (num9 == 6)
					{
						alpha = 0;
					}
					else if (num9 == 7)
					{
						alpha = byte.MaxValue;
					}
					else
					{
						alpha = (byte)(((6 - num9) * (int)b + (num9 - 1) * (int)b2) / 5);
					}
					byte b9 = (byte)(num7 >> 2 * (4 * i + j) & 3U);
					Color color = default(Color);
					switch (b9)
					{
					case 0:
						color = Color.FromArgb((int)alpha, (int)b3, (int)b4, (int)b5);
						break;
					case 1:
						color = Color.FromArgb((int)alpha, (int)b6, (int)b7, (int)b8);
						break;
					case 2:
						color = Color.FromArgb((int)alpha, (int)((2 * b3 + b6) / 3), (int)((2 * b4 + b7) / 3), (int)((2 * b5 + b8) / 3));
						break;
					case 3:
						color = Color.FromArgb((int)alpha, (int)((b3 + 2 * b6) / 3), (int)((b4 + 2 * b7) / 3), (int)((b5 + 2 * b8) / 3));
						break;
					}
					image.SetPixel(x + j, y + i, color);
				}
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002F58 File Offset: 0x00001158
		private Bitmap UncompressV8U8(byte[] data, int w, int h)
		{
			Bitmap bitmap = new Bitmap(w, h);
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					for (int i = 0; i < h; i++)
					{
						for (int j = 0; j < w; j++)
						{
							sbyte b = binaryReader.ReadSByte();
							sbyte b2 = binaryReader.ReadSByte();
							byte maxValue = byte.MaxValue;
							bitmap.SetPixel(j, i, Color.FromArgb((int)(sbyte.MaxValue - b), (int)(sbyte.MaxValue - b2), (int)maxValue));
						}
					}
				}
			}
			return bitmap;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000302C File Offset: 0x0000122C
		private Bitmap readLinearImage(byte[] data, int w, int h)
		{
			Bitmap bitmap = new Bitmap(w, h);
			using (MemoryStream memoryStream = new MemoryStream(data))
			{
				using (BinaryReader binaryReader = new BinaryReader(memoryStream))
				{
					for (int i = 0; i < h; i++)
					{
						for (int j = 0; j < w; j++)
						{
							bitmap.SetPixel(j, i, Color.FromArgb(binaryReader.ReadInt32()));
						}
					}
				}
			}
			return bitmap;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000030E4 File Offset: 0x000012E4
		private void Read_DDS_HEADER(DDS_HEADER h, BinaryReader r)
		{
			h.dwSize = r.ReadInt32();
			h.dwFlags = r.ReadInt32();
			h.dwHeight = r.ReadInt32();
			h.dwWidth = r.ReadInt32();
			h.dwPitchOrLinearSize = r.ReadInt32();
			h.dwDepth = r.ReadInt32();
			h.dwMipMapCount = r.ReadInt32();
			for (int i = 0; i < 11; i++)
			{
				h.dwReserved1[i] = r.ReadInt32();
			}
			this.Read_DDS_PIXELFORMAT(h.ddspf, r);
			h.dwCaps = r.ReadInt32();
			h.dwCaps2 = r.ReadInt32();
			h.dwCaps3 = r.ReadInt32();
			h.dwCaps4 = r.ReadInt32();
			h.dwReserved2 = r.ReadInt32();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000031B4 File Offset: 0x000013B4
		private void Read_DDS_PIXELFORMAT(DDS_PIXELFORMAT p, BinaryReader r)
		{
			p.dwSize = r.ReadInt32();
			p.dwFlags = r.ReadInt32();
			p.dwFourCC = r.ReadInt32();
			p.dwRGBBitCount = r.ReadInt32();
			p.dwRBitMask = r.ReadInt32();
			p.dwGBitMask = r.ReadInt32();
			p.dwBBitMask = r.ReadInt32();
			p.dwABitMask = r.ReadInt32();
		}

		// Token: 0x04000007 RID: 7
		private const int DDPF_ALPHAPIXELS = 1;

		// Token: 0x04000008 RID: 8
		private const int DDPF_ALPHA = 2;

		// Token: 0x04000009 RID: 9
		private const int DDPF_FOURCC = 4;

		// Token: 0x0400000A RID: 10
		private const int DDPF_RGB = 64;

		// Token: 0x0400000B RID: 11
		private const int DDPF_YUV = 512;

		// Token: 0x0400000C RID: 12
		private const int DDPF_LUMINANCE = 131072;

		// Token: 0x0400000D RID: 13
		private const int DDSD_MIPMAPCOUNT = 131072;

		// Token: 0x0400000E RID: 14
		private const int FOURCC_DXT1 = 827611204;

		// Token: 0x0400000F RID: 15
		private const int FOURCC_DX10 = 808540228;

		// Token: 0x04000010 RID: 16
		private const int FOURCC_DXT5 = 894720068;

		// Token: 0x04000011 RID: 17
		private const int FOURCC_DXT3 = 861165636;

		// Token: 0x04000012 RID: 18
		public int dwMagic;

		// Token: 0x04000013 RID: 19
		private DDS_HEADER header = new DDS_HEADER();

		// Token: 0x04000014 RID: 20
		private DDS_HEADER_DXT10 header10 = null;

		// Token: 0x04000015 RID: 21
		public byte[] bdata;

		// Token: 0x04000016 RID: 22
		public byte[] bdata2;

		// Token: 0x04000017 RID: 23
		public Bitmap[] images;
	}
}
