using System;

namespace SereniaBLPLib
{
	// Token: 0x0200000A RID: 10
	public class DXTDecompression
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00003258 File Offset: 0x00001458
		private static void decompress(ref byte[] rgba, byte[] block, int flags)
		{
			byte[] array = new byte[8];
			if ((flags & 6) != 0)
			{
				Array.Copy(block, 8, array, 0, 8);
			}
			else
			{
				Array.Copy(block, 0, array, 0, 8);
			}
			DXTDecompression.decompressColor(ref rgba, array, (flags & 1) != 0);
			if ((flags & 2) != 0)
			{
				DXTDecompression.DecompressAlphaDxt3(ref rgba, block);
			}
			else if ((flags & 4) != 0)
			{
				DXTDecompression.DecompressAlphaDxt5(ref rgba, block);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x000032C8 File Offset: 0x000014C8
		private static void DecompressAlphaDxt3(ref byte[] rgba, byte[] block)
		{
			for (int i = 0; i < 8; i++)
			{
				byte b = block[i];
				byte b2 = (byte)(b & 15);
				byte b3 = (byte)(b & 240);
				rgba[8 * i + 3] = (byte)((int)b2 | (int)b2 << 4);
				rgba[8 * i + 7] = (byte)((int)b3 | b3 >> 4);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003320 File Offset: 0x00001520
		private static void DecompressAlphaDxt5(ref byte[] rgba, byte[] block)
		{
			int num = (int)block[0];
			int num2 = (int)block[1];
			byte[] array = new byte[8];
			array[0] = (byte)num;
			array[1] = (byte)num2;
			if (num <= num2)
			{
				for (int i = 1; i < 5; i++)
				{
					array[1 + i] = (byte)(((5 - i) * num + i * num2) / 5);
				}
				array[6] = 0;
				array[7] = byte.MaxValue;
			}
			else
			{
				for (int i = 1; i < 7; i++)
				{
					array[i + 1] = (byte)(((7 - i) * num + i * num2) / 7);
				}
			}
			byte[] array2 = new byte[16];
			int num3 = 2;
			byte[] array3 = array2;
			int num4 = 0;
			for (int i = 0; i < 2; i++)
			{
				int num5 = 0;
				for (int j = 0; j < 3; j++)
				{
					int num6 = (int)block[num3++];
					num5 |= num6 << 8 * j;
				}
				for (int j = 0; j < 8; j++)
				{
					int num7 = num5 >> 3 * j & 7;
					array3[num4++] = (byte)num7;
				}
			}
			for (int i = 0; i < 16; i++)
			{
				rgba[4 * i + 3] = array[(int)array2[i]];
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000347C File Offset: 0x0000167C
		private static void decompressColor(ref byte[] rgba, byte[] block, bool isDxt1)
		{
			byte[] array = new byte[16];
			int num = DXTDecompression.unpack565(block, 0, ref array, 0);
			int num2 = DXTDecompression.unpack565(block, 2, ref array, 4);
			for (int i = 0; i < 3; i++)
			{
				int num3 = (int)array[i];
				int num4 = (int)array[4 + i];
				if (isDxt1 && num <= num2)
				{
					array[8 + i] = (byte)((num3 + num4) / 2);
					array[12 + i] = 0;
				}
				else
				{
					array[8 + i] = (byte)((2 * num3 + num4) / 3);
					array[12 + i] = (byte)((num3 + 2 * num4) / 3);
				}
			}
			array[11] = byte.MaxValue;
			array[15] = (byte)((isDxt1 && num <= num2) ? 0 : byte.MaxValue);
			byte[] array2 = new byte[16];
			for (int i = 0; i < 4; i++)
			{
				byte b = block[4 + i];
				array2[i * 4] = ((byte)(b & 3));
				array2[1 + i * 4] = (byte)(b >> 2 & 3);
				array2[2 + i * 4] = (byte)(b >> 4 & 3);
				array2[3 + i * 4] = (byte)(b >> 6 & 3);
			}
			for (int i = 0; i < 16; i++)
			{
				byte b2 = (byte)(4 * array2[i]);
				for (int j = 0; j < 4; j++)
				{
					rgba[4 * i + j] = array[(int)b2 + j];
				}
			}
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000035EC File Offset: 0x000017EC
		private static int unpack565(byte[] packed, int packed_offset, ref byte[] colour, int colour_offset)
		{
			int num = (int)packed[packed_offset] | (int)packed[1 + packed_offset] << 8;
			byte b = (byte)(num >> 11 & 31);
			byte b2 = (byte)(num >> 5 & 63);
			byte b3 = (byte)(num & 31);
			colour[colour_offset] = (byte)((int)b << 3 | b >> 2);
			colour[1 + colour_offset] = (byte)((int)b2 << 2 | b2 >> 4);
			colour[2 + colour_offset] = (byte)((int)b3 << 3 | b3 >> 2);
			colour[3 + colour_offset] = byte.MaxValue;
			return num;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003658 File Offset: 0x00001858
		public static void decompressImage(out byte[] argb, int width, int height, byte[] blocks, int flags)
		{
			argb = new byte[width * height * 4];
			int num = 0;
			int num2 = ((flags & 1) != 0) ? 8 : 16;
			for (int i = 0; i < height; i += 4)
			{
				for (int j = 0; j < width; j += 4)
				{
					byte[] sourceArray = new byte[64];
					int num3 = 0;
					byte[] array = new byte[num2];
					if (blocks.Length != num)
					{
						Array.Copy(blocks, num, array, 0, num2);
						DXTDecompression.decompress(ref sourceArray, array, flags);
						byte[] array2 = new byte[4];
						for (int k = 0; k < 4; k++)
						{
							for (int l = 0; l < 4; l++)
							{
								int num4 = j + l;
								int num5 = i + k;
								if (num4 < width && num5 < height)
								{
									int num6 = 4 * (width * num5 + num4);
									Array.Copy(sourceArray, num3, array2, 0, 4);
									num3 += 4;
									argb[num6] = array2[2];
									argb[num6 + 1] = array2[1];
									argb[num6 + 2] = array2[0];
									argb[num6 + 3] = array2[3];
								}
								else
								{
									num3 += 4;
								}
							}
						}
						num += num2;
					}
				}
			}
		}

		// Token: 0x0200000B RID: 11
		public enum DXTFlags
		{
			// Token: 0x040000B1 RID: 177
			DXT1 = 1,
			// Token: 0x040000B2 RID: 178
			DXT3,
			// Token: 0x040000B3 RID: 179
			DXT5 = 4
		}
	}
}
