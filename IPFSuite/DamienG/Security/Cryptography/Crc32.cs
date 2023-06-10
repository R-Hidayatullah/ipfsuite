using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace DamienG.Security.Cryptography
{
	// Token: 0x02000002 RID: 2
	public sealed class Crc32 : HashAlgorithm
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public Crc32() : this(3988292384U, uint.MaxValue)
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002064 File Offset: 0x00000264
		public Crc32(uint polynomial, uint seed)
		{
			this.table = Crc32.InitializeTable(polynomial);
			this.hash = seed;
			this.seed = seed;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002096 File Offset: 0x00000296
		public override void Initialize()
		{
			this.hash = this.seed;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020A5 File Offset: 0x000002A5
		protected override void HashCore(byte[] buffer, int start, int length)
		{
			this.hash = Crc32.CalculateHash(this.table, this.hash, buffer, start, length);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020C4 File Offset: 0x000002C4
		protected override byte[] HashFinal()
		{
			byte[] array = Crc32.UInt32ToBigEndianBytes(~this.hash);
			this.HashValue = array;
			return array;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x000020EC File Offset: 0x000002EC
		public override int HashSize
		{
			get
			{
				return 32;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public static uint Compute(byte[] buffer)
		{
			return Crc32.Compute(uint.MaxValue, buffer);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000211C File Offset: 0x0000031C
		public static uint Compute(uint seed, byte[] buffer)
		{
			return Crc32.Compute(3988292384U, seed, buffer);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000213C File Offset: 0x0000033C
		public static uint Compute(uint polynomial, uint seed, byte[] buffer)
		{
			return ~Crc32.CalculateHash(Crc32.InitializeTable(polynomial), seed, buffer, 0, buffer.Length);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002160 File Offset: 0x00000360
		private static uint[] InitializeTable(uint polynomial)
		{
			uint[] result;
			if (polynomial == 3988292384U && Crc32.defaultTable != null)
			{
				result = Crc32.defaultTable;
			}
			else
			{
				uint[] array = new uint[256];
				for (int i = 0; i < 256; i++)
				{
					uint num = (uint)i;
					for (int j = 0; j < 8; j++)
					{
						if ((num & 1U) == 1U)
						{
							num = (num >> 1 ^ polynomial);
						}
						else
						{
							num >>= 1;
						}
					}
					array[i] = num;
				}
				if (polynomial == 3988292384U)
				{
					Crc32.defaultTable = array;
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002208 File Offset: 0x00000408
		private static uint CalculateHash(uint[] table, uint seed, IList<byte> buffer, int start, int size)
		{
			uint num = seed;
			for (int i = start; i < size - start; i++)
			{
				num = (num >> 8 ^ table[(int)((UIntPtr)((uint)buffer[i] ^ (num & 255U)))]);
			}
			return num;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002248 File Offset: 0x00000448
		private static byte[] UInt32ToBigEndianBytes(uint uint32)
		{
			byte[] bytes = BitConverter.GetBytes(uint32);
			if (BitConverter.IsLittleEndian)
			{
				Array.Reverse(bytes);
			}
			return bytes;
		}

		// Token: 0x04000001 RID: 1
		public const uint DefaultPolynomial = 3988292384U;

		// Token: 0x04000002 RID: 2
		public const uint DefaultSeed = 4294967295U;

		// Token: 0x04000003 RID: 3
		private static uint[] defaultTable;

		// Token: 0x04000004 RID: 4
		private readonly uint seed;

		// Token: 0x04000005 RID: 5
		private readonly uint[] table;

		// Token: 0x04000006 RID: 6
		private uint hash;
	}
}
