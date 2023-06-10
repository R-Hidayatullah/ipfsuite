using System;
using System.IO;

namespace IPFSuite.FileFormats.IPF
{
    // Token: 0x02000034 RID: 52
    internal class PkwareTraditionalEncryptionData
    {
        // Token: 0x06000121 RID: 289 RVA: 0x0000AED7 File Offset: 0x000090D7
        public PkwareTraditionalEncryptionData(string password)
        {
            this.Initialize(password);
        }

        // Token: 0x17000056 RID: 86
        // (get) Token: 0x06000122 RID: 290 RVA: 0x0000AF00 File Offset: 0x00009100
        private byte MagicByte
        {
            get
            {
                ushort t = (ushort)((ushort)(_Keys[2] & 65535U) | 2);
                return (byte)(t * (t ^ 1) >> 8);
            }
        }

        // Token: 0x06000123 RID: 291 RVA: 0x0000AF28 File Offset: 0x00009128
        public byte[] Decrypt(byte[] cipherText, int length)
        {
            if (length > cipherText.Length)
            {
                throw new ArgumentOutOfRangeException("length", "Bad length during Decryption: the length parameter must be smaller than or equal to the size of the destination array.");
            }
            byte[] plainText = new byte[length];
            for (int i = 0; i < length; i++)
            {
                if (i % 2 != 0)
                {
                    plainText[i] = cipherText[i];
                }
                else
                {
                    byte c = cipherText[i];
                    ushort j = (ushort)((ushort)(_Keys[2] & 65535U) | 2);
                    c ^= (byte)(j * (j ^ 1) >> 8 & 255);
                    this.UpdateKeys(c);
                    plainText[i] = c;
                }
            }
            return plainText;
        }

        // Token: 0x06000124 RID: 292 RVA: 0x0000AFA0 File Offset: 0x000091A0
        public byte[] Encrypt(byte[] plainText, int length)
        {
            if (plainText == null)
            {
                throw new ArgumentNullException("plaintext");
            }
            if (length > plainText.Length)
            {
                throw new ArgumentOutOfRangeException("length", "Bad length during Encryption: The length parameter must be smaller than or equal to the size of the destination array.");
            }
            byte[] cipherText = new byte[length];
            for (int i = 0; i < length; i++)
            {
                byte C = plainText[i];
                cipherText[i] = (byte)(plainText[i] ^ MagicByte);
                this.UpdateKeys(C);
            }
            return cipherText;
        }

        // Token: 0x06000125 RID: 293 RVA: 0x0000B000 File Offset: 0x00009200
        private void Initialize(string password)
        {
            foreach (char p in password)
            {
                this.UpdateKeys((byte)p);
            }
        }

        // Token: 0x06000126 RID: 294 RVA: 0x0000B030 File Offset: 0x00009230
        private void UpdateKeys(byte byteValue)
        {
            this._Keys[0] = (uint)PkwareTraditionalEncryptionData.crc32.ComputeCrc32((int)this._Keys[0], byteValue);
            this._Keys[1] = this._Keys[1] + (uint)((byte)this._Keys[0]);
            this._Keys[1] = this._Keys[1] * 134775813U + 1U;
            this._Keys[2] = (uint)PkwareTraditionalEncryptionData.crc32.ComputeCrc32((int)this._Keys[2], (byte)(this._Keys[1] >> 24));
        }

        // Token: 0x040001DB RID: 475
        private static readonly PkwareTraditionalEncryptionData.CRC32 crc32 = new PkwareTraditionalEncryptionData.CRC32();

        // Token: 0x040001DC RID: 476
        private readonly uint[] _Keys = new uint[]
        {
            305419896U,
            591751049U,
            878082192U
        };

        // Token: 0x02000057 RID: 87
        internal class CRC32
        {
            // Token: 0x060001A9 RID: 425 RVA: 0x0000D4B0 File Offset: 0x0000B6B0
            static CRC32()
            {
                uint dwPolynomial = 3988292384U;
                PkwareTraditionalEncryptionData.CRC32.crc32Table = new uint[256];
                for (uint i = 0U; i < 256U; i += 1U)
                {
                    uint dwCrc = i;
                    for (uint j = 8U; j > 0U; j -= 1U)
                    {
                        if ((dwCrc & 1U) == 1U)
                        {
                            dwCrc = (dwCrc >> 1 ^ dwPolynomial);
                        }
                        else
                        {
                            dwCrc >>= 1;
                        }
                    }
                    PkwareTraditionalEncryptionData.CRC32.crc32Table[(int)i] = dwCrc;
                }
            }

            // Token: 0x17000073 RID: 115
            // (get) Token: 0x060001AA RID: 426 RVA: 0x0000D50A File Offset: 0x0000B70A
            public long TotalBytesRead
            {
                get
                {
                    return this.totalBytesRead;
                }
            }

            // Token: 0x17000074 RID: 116
            // (get) Token: 0x060001AB RID: 427 RVA: 0x0000D512 File Offset: 0x0000B712
            public int Crc32Result
            {
                get
                {
                    return (int)(~(int)this.runningCrc32Result);
                }
            }

            // Token: 0x060001AC RID: 428 RVA: 0x0000D51B File Offset: 0x0000B71B
            public int GetCrc32(Stream input)
            {
                return this.GetCrc32AndCopy(input, null);
            }

            // Token: 0x060001AD RID: 429 RVA: 0x0000D528 File Offset: 0x0000B728
            public int GetCrc32AndCopy(Stream input, Stream output)
            {
                if (input == null)
                {
                    throw new Exception("The input stream must not be null.");
                }
                byte[] buffer = new byte[8192];
                int readSize = 8192;
                this.totalBytesRead = 0L;
                int count = input.Read(buffer, 0, readSize);
                if (output != null)
                {
                    output.Write(buffer, 0, count);
                }
                this.totalBytesRead += (long)count;
                while (count > 0)
                {
                    this.SlurpBlock(buffer, 0, count);
                    count = input.Read(buffer, 0, readSize);
                    if (output != null)
                    {
                        output.Write(buffer, 0, count);
                    }
                    this.totalBytesRead += (long)count;
                }
                return (int)(~(int)this.runningCrc32Result);
            }

            // Token: 0x060001AE RID: 430 RVA: 0x0000D5BC File Offset: 0x0000B7BC
            public int ComputeCrc32(int W, byte B)
            {
                return this._InternalComputeCrc32((uint)W, B);
            }

            // Token: 0x060001AF RID: 431 RVA: 0x0000D5C6 File Offset: 0x0000B7C6
            internal int _InternalComputeCrc32(uint W, byte B)
            {
                return (int)(PkwareTraditionalEncryptionData.CRC32.crc32Table[(int)((W ^ (uint)B) & 255U)] ^ W >> 8);
            }

            // Token: 0x060001B0 RID: 432 RVA: 0x0000D5DC File Offset: 0x0000B7DC
            public void SlurpBlock(byte[] block, int offset, int count)
            {
                if (block == null)
                {
                    throw new Exception("The data buffer must not be null.");
                }
                for (int i = 0; i < count; i++)
                {
                    int x = offset + i;
                    this.runningCrc32Result = (this.runningCrc32Result >> 8 ^ PkwareTraditionalEncryptionData.CRC32.crc32Table[(int)((uint)block[x] ^ (this.runningCrc32Result & 255U))]);
                }
                this.totalBytesRead += (long)count;
            }

            // Token: 0x060001B1 RID: 433 RVA: 0x0000D63C File Offset: 0x0000B83C
            private uint gf2_matrix_times(uint[] matrix, uint vec)
            {
                uint sum = 0U;
                int i = 0;
                while (vec != 0U)
                {
                    if ((vec & 1U) == 1U)
                    {
                        sum ^= matrix[i];
                    }
                    vec >>= 1;
                    i++;
                }
                return sum;
            }

            // Token: 0x060001B2 RID: 434 RVA: 0x0000D668 File Offset: 0x0000B868
            private void gf2_matrix_square(uint[] square, uint[] mat)
            {
                for (int i = 0; i < 32; i++)
                {
                    square[i] = this.gf2_matrix_times(mat, mat[i]);
                }
            }

            // Token: 0x060001B3 RID: 435 RVA: 0x0000D690 File Offset: 0x0000B890
            public void Combine(int crc, int length)
            {
                uint[] even = new uint[32];
                uint[] odd = new uint[32];
                if (length == 0)
                {
                    return;
                }
                uint crc2 = ~this.runningCrc32Result;
                odd[0] = 3988292384U;
                uint row = 1U;
                for (int i = 1; i < 32; i++)
                {
                    odd[i] = row;
                    row <<= 1;
                }
                this.gf2_matrix_square(even, odd);
                this.gf2_matrix_square(odd, even);
                uint len2 = (uint)length;
                do
                {
                    this.gf2_matrix_square(even, odd);
                    if ((len2 & 1U) == 1U)
                    {
                        crc2 = this.gf2_matrix_times(even, crc2);
                    }
                    len2 >>= 1;
                    if (len2 == 0U)
                    {
                        break;
                    }
                    this.gf2_matrix_square(odd, even);
                    if ((len2 & 1U) == 1U)
                    {
                        crc2 = this.gf2_matrix_times(odd, crc2);
                    }
                    len2 >>= 1;
                }
                while (len2 != 0U);
                crc2 ^= (uint)crc;
                this.runningCrc32Result = ~crc2;
            }

            // Token: 0x0400026A RID: 618
            private const int BUFFER_SIZE = 8192;

            // Token: 0x0400026B RID: 619
            private static readonly uint[] crc32Table;

            // Token: 0x0400026C RID: 620
            private uint runningCrc32Result = uint.MaxValue;

            // Token: 0x0400026D RID: 621
            private long totalBytesRead;
        }
    }
}
