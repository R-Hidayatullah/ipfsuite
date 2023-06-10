using System;
using System.Collections.Generic;
using System.IO;
using IPFSuite.Extensions;

namespace IPFSuite.FileFormats.FSB
{
	// Token: 0x0200000E RID: 14
	public class FSB
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000037FC File Offset: 0x000019FC
		public List<FSB.Sample> Samples
		{
			get
			{
				return this._samples;
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003814 File Offset: 0x00001A14
		public FSB(Stream stream)
		{
			this.reader = new BinaryReader(stream);
			this.readHeader();
			this.readFiles();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000384C File Offset: 0x00001A4C
		private void readFiles()
		{
			this.reader.BaseStream.Seek((long)((ulong)this._fileTableOffset), SeekOrigin.Begin);
			uint fileOffset = this._fileOffset;
			int num = 0;
			while ((long)num < (long)((ulong)this._numFiles))
			{
				FSB.Sample sample = new FSB.Sample();
				sample.Frequency = 44100U;
				sample.Channels = 1U;
				sample.Bits = 16U;
				uint num2 = this.reader.Read<uint>();
				sample.Offset = (num2 >> 8) * 64U + this._fileOffset;
				sample.Type = (byte)(num2 & 255U);
				sample.Samples = this.reader.Read<uint>() >> 2;
				sample.Codec = this._sampleFormat;
				if ((sample.Type & 32) != 0)
				{
					sample.Channels = 2U;
				}
				byte b = sample.Type;
				long position;
				while ((b & 1) != 0)
				{
					num2 = this.reader.Read<uint>();
					b = (byte)(num2 & 1U);
					uint num3 = (num2 & 16777215U) >> 1;
					num2 >>= 24;
					position = this.reader.BaseStream.Position;
					uint num4 = num2;
					switch (num4)
					{
					case 2U:
						sample.Channels = (uint)this.reader.Read<byte>();
						break;
					case 3U:
						break;
					case 4U:
						sample.Frequency = this.reader.Read<uint>();
						break;
					default:
						switch (num4)
						{
						case 8U:
							sample.UnknownBytes3 = this.reader.ReadBytes((int)num3);
							break;
						case 9U:
							break;
						case 10U:
							sample.UnknownBytes2 = this.reader.ReadBytes((int)num3);
							break;
						default:
							if (num4 == 22U)
							{
								sample.UnknownBytes1 = this.reader.ReadBytes((int)num3);
							}
							break;
						}
						break;
					}
					IL_194:
					this.reader.BaseStream.Seek(position + (long)((ulong)num3), SeekOrigin.Begin);
					continue;
					goto IL_194;
				}
				if (num > 0)
				{
					sample.Offset += 32U;
					FSB.Sample sample2 = this._samples[num - 1];
					sample2.Size = sample.Offset - sample2.Offset;
					if ((long)num == (long)((ulong)(this._numFiles - 1U)))
					{
						sample.Size = (uint)this.reader.BaseStream.Length - sample.Offset;
					}
				}
				position = this.reader.BaseStream.Position;
				this.reader.BaseStream.Seek((long)((ulong)this._nameTableOffset + (ulong)((long)(4 * num))), SeekOrigin.Begin);
				this.reader.BaseStream.Seek((long)((ulong)(this._nameTableOffset + this.reader.Read<uint>())), SeekOrigin.Begin);
				sample.Name = this.reader.ReadCString(1024);
				this.reader.BaseStream.Seek(position, SeekOrigin.Begin);
				this._samples.Add(sample);
				num++;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00003B34 File Offset: 0x00001D34
		private void readHeader()
		{
			string text = this.reader.ReadCString(4);
			if (text.Substring(0, 3) != "FSB")
			{
				throw new FSB_LoaderException("Input file is not an FSB");
			}
			if (text != "FSB5")
			{
				throw new FSB_LoaderException("Unable to load FSB files of this version");
			}
			int num = this.reader.Read<int>();
			if (num != 1)
			{
				throw new FSB_LoaderException("Parser cannot read FSB files of this sub version");
			}
			this._numFiles = this.reader.Read<uint>();
			this._sampleHeaderSize = this.reader.Read<uint>();
			this._nameTableSize = this.reader.Read<uint>();
			this._sampleDataCompressedSize = this.reader.Read<uint>();
			FSB.SampleFormat sampleFormat = (FSB.SampleFormat)this.reader.Read<uint>();
			if (!Enum.IsDefined(typeof(FSB.SampleFormat), sampleFormat))
			{
				throw new FSB_LoaderException("Unknown sample format");
			}
			this._sampleFormat = sampleFormat;
			this._headerUnknown = this.reader.Read<FSB.HeaderUnknown1>();
			this._fileTableOffset = (uint)this.reader.BaseStream.Position;
			this._nameTableOffset = this._fileTableOffset + this._sampleHeaderSize;
			this._fileOffset = this._nameTableOffset + this._nameTableSize;
		}

		// Token: 0x040000B5 RID: 181
		private const int MAX_NAMELEN = 1024;

		// Token: 0x040000B6 RID: 182
		private BinaryReader reader;

		// Token: 0x040000B7 RID: 183
		private uint _numFiles = 0U;

		// Token: 0x040000B8 RID: 184
		private FSB.SampleFormat _sampleFormat;

		// Token: 0x040000B9 RID: 185
		private uint _sampleHeaderSize;

		// Token: 0x040000BA RID: 186
		private uint _nameTableSize;

		// Token: 0x040000BB RID: 187
		private uint _sampleDataCompressedSize;

		// Token: 0x040000BC RID: 188
		private FSB.HeaderUnknown1 _headerUnknown;

		// Token: 0x040000BD RID: 189
		private uint _fileTableOffset;

		// Token: 0x040000BE RID: 190
		private uint _nameTableOffset;

		// Token: 0x040000BF RID: 191
		private uint _fileOffset;

		// Token: 0x040000C0 RID: 192
		private List<FSB.Sample> _samples = new List<FSB.Sample>();

		// Token: 0x0200000F RID: 15
		public enum SampleFormat : uint
		{
			// Token: 0x040000C2 RID: 194
			Vorbis = 15U
		}

		// Token: 0x02000010 RID: 16
		private struct HeaderUnknown1
		{
			// Token: 0x040000C3 RID: 195
			private uint field_0;

			// Token: 0x040000C4 RID: 196
			private uint field_4;

			// Token: 0x040000C5 RID: 197
			private uint field_8;

			// Token: 0x040000C6 RID: 198
			private uint field_C;

			// Token: 0x040000C7 RID: 199
			private uint field_10;

			// Token: 0x040000C8 RID: 200
			private uint field_14;

			// Token: 0x040000C9 RID: 201
			private uint field_18;

			// Token: 0x040000CA RID: 202
			private uint field_1C;
		}

		// Token: 0x02000011 RID: 17
		public class Sample
		{
			// Token: 0x040000CB RID: 203
			public uint Frequency;

			// Token: 0x040000CC RID: 204
			public uint Channels;

			// Token: 0x040000CD RID: 205
			public uint Bits;

			// Token: 0x040000CE RID: 206
			public uint Offset;

			// Token: 0x040000CF RID: 207
			public uint Samples;

			// Token: 0x040000D0 RID: 208
			public FSB.SampleFormat Codec;

			// Token: 0x040000D1 RID: 209
			public byte Type;

			// Token: 0x040000D2 RID: 210
			public uint Size;

			// Token: 0x040000D3 RID: 211
			public string Name;

			// Token: 0x040000D4 RID: 212
			public byte[] UnknownBytes1;

			// Token: 0x040000D5 RID: 213
			public byte[] UnknownBytes2;

			// Token: 0x040000D6 RID: 214
			public byte[] UnknownBytes3;
		}
	}
}
