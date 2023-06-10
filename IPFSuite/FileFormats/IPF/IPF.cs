using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DamienG.Security.Cryptography;

namespace IPFSuite.FileFormats.IPF
{
    // Token: 0x02000031 RID: 49
    public class IPF
    {
        // Token: 0x17000042 RID: 66
        // (get) Token: 0x060000E8 RID: 232 RVA: 0x0000A1B9 File Offset: 0x000083B9
        // (set) Token: 0x060000E9 RID: 233 RVA: 0x0000A1C1 File Offset: 0x000083C1
        public IPF.eMode Mode { get; set; }

        // Token: 0x060000EA RID: 234 RVA: 0x0000A1CC File Offset: 0x000083CC
        public IPF()
        {
            this.Footer = new IPFFooter();
            this.Footer.Compression = 101010256U;
            this.FileTable = new IPFFileTable[0];
            this._stream = new MemoryStream();
        }

        // Token: 0x060000EB RID: 235 RVA: 0x0000A258 File Offset: 0x00008458
        public IPF(string filename)
        {
            this.Footer = new IPFFooter();
            this.Mode = IPF.eMode.Open;
            this._stream = new FileStream(filename, FileMode.Open, FileAccess.ReadWrite);
            this._br = new BinaryReader(this._stream);
            this._bw = new BinaryWriter(this._stream);
        }

        // Token: 0x060000EC RID: 236 RVA: 0x0000A2F4 File Offset: 0x000084F4
        public bool LoadSync()
        {
            this._stream.Position = this._stream.Length - 24L;
            this.Footer.FileCount = this._br.ReadUInt16();
            this.Footer.FileTablePointer = this._br.ReadUInt32();
            this.Footer.Padding1 = this._br.ReadUInt16();
            this.Footer.FooterPointer = this._br.ReadUInt32();
            this.Footer.Compression = this._br.ReadUInt32();
            this.Footer.VersionToPatch = this._br.ReadUInt32();
            this.Footer.NewVersion = this._br.ReadUInt32();
            this._stream.Position = (long)((ulong)this.Footer.FileTablePointer);
            this.FileTable = new IPFFileTable[(int)this.Footer.FileCount];
            for (int i = 0; i < (int)this.Footer.FileCount; i++)
            {
                this.FileTable[i] = new IPFFileTable();
                this.FileTable[i].idx = i;
                this.FileTable[i].fileNameLength = this._br.ReadUInt16();
                this.FileTable[i].crc32 = this._br.ReadUInt32();
                this.FileTable[i].fileSizeCompressed = this._br.ReadUInt32();
                this.FileTable[i].fileSizeUncompressed = this._br.ReadUInt32();
                this.FileTable[i].filePointer = this._br.ReadUInt32();
                this.FileTable[i].containerNameLength = this._br.ReadUInt16();
                this.FileTable[i].containerName = new string(this._br.ReadChars((int)this.FileTable[i].containerNameLength));
                string path = new string(this._br.ReadChars((int)this.FileTable[i].fileNameLength));
                this.FileTable[i].fileName = Path.GetFileName(path);
                this.FileTable[i].directoryName = Path.GetDirectoryName(path);
            }
            SortedDictionary<uint, uint> sortedDictionary = new SortedDictionary<uint, uint>();
            foreach (IPFFileTable ipffileTable in this.FileTable)
            {
                sortedDictionary.Add(ipffileTable.filePointer, ipffileTable.fileSizeCompressed);
            }
            uint num = 0U;
            bool flag;
            do
            {
                flag = false;
                foreach (KeyValuePair<uint, uint> keyValuePair in sortedDictionary)
                {
                    if (keyValuePair.Key > num)
                    {
                        sortedDictionary.Add(num, keyValuePair.Key - num);
                        flag = true;
                        break;
                    }
                    num = keyValuePair.Key + keyValuePair.Value;
                }
            }
            while (flag);
            return true;
        }

        // Token: 0x060000ED RID: 237 RVA: 0x0000A5C8 File Offset: 0x000087C8
        public async Task<bool> Load()
        {
            return await Task.Factory.StartNew<bool>(() => this.LoadSync());

        }

        // Token: 0x060000EE RID: 238 RVA: 0x0000A60B File Offset: 0x0000880B
        public void Close()
        {
            if (this._bw != null)
            {
                this._bw.Dispose();
            }
            if (this._stream != null)
            {
                this._stream.Dispose();
            }
            if (this._br != null)
            {
                this._br.Dispose();
            }
        }

        // Token: 0x060000EF RID: 239 RVA: 0x0000A648 File Offset: 0x00008848
        private int FileExists(string filename)
        {
            if (this.FileTable != null)
            {
                using (IEnumerator<IPFFileTable> enumerator = (from file in this.FileTable
                                                               where Path.Combine(file.directoryName, file.fileName) == filename
                                                               select file).GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        return enumerator.Current.idx;
                    }
                }
                return -1;
            }
            return -1;
        }

        // Token: 0x060000F0 RID: 240 RVA: 0x0000A6C4 File Offset: 0x000088C4
        private bool IsSpecialExtension(string name)
        {
            string extension = Path.GetExtension(name);
            return this._specialExtensions.Contains(extension.ToLowerInvariant());
        }

        // Token: 0x060000F1 RID: 241 RVA: 0x0000A6EC File Offset: 0x000088EC
        public async Task<byte[]> ExtractAsync(int idx)
        {
            return await Task.Factory.StartNew<byte[]>(() => this.Extract(idx));
        }

        // Token: 0x060000F2 RID: 242 RVA: 0x0000A738 File Offset: 0x00008938
        public byte[] Extract(int idx)
        {
            byte[] result;
            if (this.FileTable.Length > idx)
            {
                IPFFileTable ipffileTable = this.FileTable[idx];
                if (this._stream.GetType() == typeof(MemoryStream))
                {
                    if (this.IsSpecialExtension(ipffileTable.fileName))
                    {
                        result = this.FileTable[idx].content;
                    }
                    else
                    {
                        result = this.decompress(this.FileTable[idx].content);
                    }
                }
                else
                {
                    this._stream.Position = (long)((ulong)ipffileTable.filePointer);
                    if (this.IsSpecialExtension(ipffileTable.fileName))
                    {
                        result = this._br.ReadBytes((int)this.FileTable[idx].fileSizeCompressed);
                    }
                    else
                    {
                        result = this.decompress(this._br.ReadBytes((int)this.FileTable[idx].fileSizeCompressed));
                    }
                }
            }
            else
            {
                result = new byte[0];
            }
            return result;
        }

        // Token: 0x060000F3 RID: 243 RVA: 0x0000A810 File Offset: 0x00008A10
        public void Add(string container, string filename, byte[] content)
        {
            uint num = 0U;
            uint crc = Crc32.Compute(content);
            uint fileSizeUncompressed = (uint)content.Length;
            if (!this.IsSpecialExtension(filename))
            {
                content = this.compress(content);
            }
            uint fileSizeCompressed = (uint)content.Length;
            int num2 = this.FileExists(filename);
            if (num2 == -1)
            {
                num2 = this.FileTable.Length;
                Array.Resize<IPFFileTable>(ref this.FileTable, this.FileTable.Length + 1);
                this.FileTable[num2] = new IPFFileTable();
                this.FileTable[num2].idx = num2;
                this.FileTable[num2].fileName = Path.GetFileName(filename);
                this.FileTable[num2].directoryName = Path.GetDirectoryName(filename);
                this.FileTable[num2].fileSizeUncompressed = fileSizeUncompressed;
                this.FileTable[num2].fileSizeCompressed = fileSizeCompressed;
                this.FileTable[num2].fileNameLength = (ushort)filename.Length;
                this.FileTable[num2].containerName = container;
                this.FileTable[num2].containerNameLength = (ushort)container.Length;
            }
            else
            {
                if (fileSizeCompressed <= this.FileTable[num2].fileSizeCompressed)
                {
                    num = this.FileTable[num2].filePointer;
                }
                this.FileTable[num2].fileSizeUncompressed = fileSizeUncompressed;
                this.FileTable[num2].fileSizeCompressed = fileSizeCompressed;
            }
            if (num == 0U)
            {
                bool flag = false;
                using (IEnumerator<KeyValuePair<uint, uint>> enumerator = (from gap in this._gaps
                                                                           where fileSizeCompressed < gap.Value
                                                                           select gap).GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        KeyValuePair<uint, uint> keyValuePair = enumerator.Current;
                        num = keyValuePair.Key;
                        flag = true;
                    }
                }
                if (!flag)
                {
                    num = this.Footer.FileTablePointer;
                    this.Footer.FileTablePointer = num + fileSizeCompressed;
                }
            }
            this.FileTable[num2].filePointer = num;
            this.FileTable[num2].crc32 = crc;
            if (this._stream.GetType() == typeof(MemoryStream))
            {
                this.FileTable[num2].content = content;
                return;
            }
            this._stream.Position = (long)((ulong)num);
            this._bw.Write(content);
            this._stream.Position = (long)((ulong)this.Footer.FileTablePointer);
            this.WriteFileTableAndFooter();
        }

        // Token: 0x060000F4 RID: 244 RVA: 0x0000AA6C File Offset: 0x00008C6C
        private void WriteFileTableAndFooter()
        {
            foreach (IPFFileTable ipffileTable in this.FileTable)
            {
                this._bw.Write(ipffileTable.fileNameLength);
                this._bw.Write(ipffileTable.crc32);
                this._bw.Write(ipffileTable.fileSizeCompressed);
                this._bw.Write(ipffileTable.fileSizeUncompressed);
                this._bw.Write(ipffileTable.filePointer);
                this._bw.Write(ipffileTable.containerNameLength);
                this._bw.Write(Encoding.UTF8.GetBytes(ipffileTable.containerName));
                this._bw.Write(Encoding.UTF8.GetBytes(Path.Combine(ipffileTable.directoryName, ipffileTable.fileName).Replace("\\", "/")));
            }
            uint value = (uint)this._stream.Position;
            this._bw.Write((ushort)this.FileTable.Length);
            this._bw.Write(this.Footer.FileTablePointer);
            this._bw.Write(this.Footer.Padding1);
            this._bw.Write(value);
            this._bw.Write(this.Footer.Compression);
            this._bw.Write(this.Footer.VersionToPatch);
            this._bw.Write(this.Footer.NewVersion);
        }

        // Token: 0x060000F5 RID: 245 RVA: 0x0000ABEC File Offset: 0x00008DEC
        public void Save(string filename)
        {
            using (FileStream fileStream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                this._bw = new BinaryWriter(fileStream);
                foreach (IPFFileTable ipffileTable in this.FileTable)
                {
                    this._stream.Position = (long)((ulong)ipffileTable.filePointer);
                    this._bw.Write(ipffileTable.content);
                }
                this.WriteFileTableAndFooter();
            }
        }

        // Token: 0x060000F6 RID: 246 RVA: 0x0000AC6C File Offset: 0x00008E6C
        private byte[] decompress(byte[] data)
        {
            if (this.Footer.NewVersion > 11000U || this.Footer.NewVersion == 0U)
            {
                data = new PkwareTraditionalEncryptionData("ofO1a0ueXA? [ÿs h %?").Decrypt(data, data.Length);
            }
            byte[] result;
            using (MemoryStream msOut = new MemoryStream())
            {
                using (MemoryStream msIn = new MemoryStream(data))
                {
                    using (DeflateStream deflate = new DeflateStream(msIn, CompressionMode.Decompress))
                    {
                        deflate.CopyTo(msOut);
                        result = msOut.ToArray();
                    }
                }
            }
            return result;
        }

        // Token: 0x060000F7 RID: 247 RVA: 0x0000AD1C File Offset: 0x00008F1C
        private byte[] compress(byte[] unzippedData)
        {
            byte[] result;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress))
                {
                    deflateStream.Write(unzippedData, 0, unzippedData.Length);
                }
                byte[] array = memoryStream.ToArray();
                if (array.Length >= unzippedData.Length)
                {
                    result = unzippedData;
                }
                else
                {
                    result = array;
                }
            }
            return result;
        }

        // Token: 0x17000043 RID: 67
        // (get) Token: 0x060000F8 RID: 248 RVA: 0x0000AD8C File Offset: 0x00008F8C
        // (set) Token: 0x060000F9 RID: 249 RVA: 0x0000AD94 File Offset: 0x00008F94
        public IPFFooter Footer { get; set; }

        // Token: 0x040001C1 RID: 449
        private const int IPF_FOOTER_LEN = 24;

        // Token: 0x040001C2 RID: 450
        private readonly Dictionary<uint, uint> _gaps = new Dictionary<uint, uint>();

        // Token: 0x040001C3 RID: 451
        private readonly Stream _stream;

        // Token: 0x040001C4 RID: 452
        private readonly BinaryReader _br;

        // Token: 0x040001C5 RID: 453
        private BinaryWriter _bw;

        // Token: 0x040001C7 RID: 455
        public IPFFileTable[] FileTable;

        // Token: 0x040001C8 RID: 456
        private readonly string[] _specialExtensions = new string[]
        {
            ".jpg",
            ".jpg",
            ".fsb",
            ".mp3",
            ".fdp",
            ".fev"
        };

        // Token: 0x02000051 RID: 81
        public enum eMode
        {
            // Token: 0x0400025A RID: 602
            None,
            // Token: 0x0400025B RID: 603
            Open,
            // Token: 0x0400025C RID: 604
            New
        }
    }
}
