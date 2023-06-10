using System;
using System.Collections.Generic;

namespace IPFSuite.FileFormats.IES
{
	// Token: 0x0200003D RID: 61
	public class IesRow : Dictionary<string, object>
	{
		// Token: 0x0600016C RID: 364 RVA: 0x0000BC24 File Offset: 0x00009E24
		public float GetFloat(string name)
		{
			if (!base.ContainsKey(name))
			{
				throw new ArgumentException("Unknown field: " + name);
			}
			if (base[name] is float)
			{
				return (float)base[name];
			}
			if (base[name] is uint)
			{
				return (uint)base[name];
			}
			throw new ArgumentException(name + " is not numeric");
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000BC93 File Offset: 0x00009E93
		public uint GetUInt(string name)
		{
			return (uint)this.GetInt(name);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000BC9C File Offset: 0x00009E9C
		public int GetInt(string name)
		{
			if (!base.ContainsKey(name))
			{
				throw new ArgumentException("Unknown field: " + name);
			}
			if (base[name] is float)
			{
				return (int)((float)base[name]);
			}
			if (base[name] is uint)
			{
				return (int)((uint)base[name]);
			}
			throw new ArgumentException(name + " is not numeric");
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000BD0C File Offset: 0x00009F0C
		public string GetString(string name)
		{
			if (!base.ContainsKey(name))
			{
				throw new ArgumentException("Unknown field: " + name);
			}
			if (base[name] is string)
			{
				return (string)base[name];
			}
			throw new ArgumentException(name + " is not a string");
		}
	}
}
