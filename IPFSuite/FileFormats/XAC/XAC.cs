using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IPFSuite.Extensions;

namespace IPFSuite.FileFormats.XAC
{
	// Token: 0x02000024 RID: 36
	public class XAC
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00003E0C File Offset: 0x0000200C
		public XAC_MaterialTotals MaterialTotals
		{
			get
			{
				return this._materialTotals;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00003E24 File Offset: 0x00002024
		public List<XAC_ShaderMaterial> ShaderMaterials
		{
			get
			{
				return this._shaderMaterials;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00003E3C File Offset: 0x0000203C
		public List<XAC_Node> RootNodes
		{
			get
			{
				return this._rootNodes;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00003E54 File Offset: 0x00002054
		public List<XAC_Mesh> Meshes
		{
			get
			{
				return this._meshes;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003E6C File Offset: 0x0000206C
		public XAC(Stream stream)
		{
			this.stream = stream;
			this.reader = new BinaryReader(this.stream);
			this._rootNodes = new List<XAC_Node>();
			this._nodes = new List<XAC_Node>();
			this._meshes = new List<XAC_Mesh>();
			this._shaderMaterials = new List<XAC_ShaderMaterial>();
			this._materials = new List<XAC_Material>();
			this.readHeader();
			this.readChunks();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003EE0 File Offset: 0x000020E0
		private void readMesh()
		{
			int num = this.reader.Read<int>();
			int num2 = this.reader.Read<int>();
			int num3 = this.reader.Read<int>();
			int num4 = this.reader.Read<int>();
			int num5 = this.reader.Read<int>();
			int num6 = this.reader.Read<int>();
			bool flag = this.reader.Read<byte>() == 1;
			this.reader.BaseStream.Seek(3L, SeekOrigin.Current);
			XAC_Mesh xac_Mesh = new XAC_Mesh();
			if (num < 0 || num >= this._nodes.Count<XAC_Node>())
			{
				throw new XAC_LoaderException("Mesh references invalid node");
			}
			xac_Mesh.Owner = this._nodes[num];
			List<XAC_Vector3D> list = new List<XAC_Vector3D>();
			List<XAC_Vector3D> list2 = new List<XAC_Vector3D>();
			List<XAC_Vector4D> list3 = new List<XAC_Vector4D>();
			List<XAC_Vector4D> list4 = new List<XAC_Vector4D>();
			List<List<XAC_Vector2D>> list5 = new List<List<XAC_Vector2D>>();
			List<int> list6 = new List<int>();
			List<List<XAC_Color>> list7 = new List<List<XAC_Color>>();
			List<List<XAC_Color8>> list8 = new List<List<XAC_Color8>>();
			if (!flag)
			{
				if (xac_Mesh.Owner.VisualMesh != null)
				{
					throw new XAC_LoaderException("Node referenced by mesh already has a visual mesh");
				}
				xac_Mesh.Owner.VisualMesh = xac_Mesh;
			}
			else
			{
				if (xac_Mesh.Owner.CollisionMesh != null)
				{
					throw new XAC_LoaderException("Node referenced by mesh already has a collision mesh");
				}
				xac_Mesh.Owner.CollisionMesh = xac_Mesh;
			}
			for (int i = 0; i < num6; i++)
			{
				uint num7 = this.reader.Read<uint>();
				int num8 = this.reader.Read<int>();
				bool flag2 = this.reader.Read<byte>() != 0;
				bool flag3 = this.reader.Read<byte>() != 0;
				this.reader.BaseStream.Seek(2L, SeekOrigin.Current);
				switch (num7)
				{
				case 0U:
					if (list.Count<XAC_Vector3D>() != 0)
					{
						throw new XAC_LoaderException("Redefinition of mesh positions");
					}
					if (num8 != 12)
					{
						throw new XAC_LoaderException("Mesh uses an unknown element type");
					}
					for (int j = 0; j < num3; j++)
					{
						list.Add(this.reader.Read<XAC_Vector3D>());
					}
					break;
				case 1U:
					if (list2.Count<XAC_Vector3D>() != 0)
					{
						throw new XAC_LoaderException("Redefinition of mesh normals");
					}
					if (num8 != 12)
					{
						throw new XAC_LoaderException("Mesh uses an unknown element type");
					}
					for (int j = 0; j < num3; j++)
					{
						list2.Add(this.reader.Read<XAC_Vector3D>());
					}
					break;
				case 2U:
				{
					if (num8 != 16)
					{
						throw new XAC_LoaderException("Mesh uses an unknown element type");
					}
					List<XAC_Vector4D> list9;
					if (list3.Count<XAC_Vector4D>() == 0)
					{
						list9 = list3;
					}
					else
					{
						if (list4.Count<XAC_Vector4D>() != 0)
						{
							this.reader.BaseStream.Seek((long)(num8 * num3), SeekOrigin.Current);
							break;
						}
						list9 = list4;
					}
					for (int j = 0; j < num3; j++)
					{
						list9.Add(this.reader.Read<XAC_Vector4D>());
					}
					break;
				}
				case 3U:
				{
					if (num8 != 8)
					{
						throw new XAC_LoaderException("Mesh uses an unknown element type");
					}
					List<XAC_Vector2D> list10 = new List<XAC_Vector2D>();
					for (int j = 0; j < num3; j++)
					{
						list10.Add(this.reader.Read<XAC_Vector2D>());
					}
					list5.Add(list10);
					break;
				}
				case 4U:
				{
					if (num8 != 4)
					{
						throw new XAC_LoaderException("Mesh uses an unknown element type");
					}
					List<XAC_Color8> list11 = new List<XAC_Color8>();
					for (int j = 0; j < num3; j++)
					{
						list11.Add(this.reader.Read<XAC_Color8>());
					}
					list8.Add(list11);
					break;
				}
				case 5U:
					if (list6.Count<int>() != 0)
					{
						throw new XAC_LoaderException("Redefinition of mesh influence range indices");
					}
					if (num8 != 4)
					{
						throw new XAC_LoaderException("Mesh uses an unknown element type");
					}
					for (int j = 0; j < num3; j++)
					{
						list6.Add(this.reader.Read<int>());
					}
					break;
				case 6U:
				{
					if (num8 != 16)
					{
						throw new XAC_LoaderException("Mesh uses an unknown element type");
					}
					List<XAC_Color> list12 = new List<XAC_Color>();
					for (int j = 0; j < num3; j++)
					{
						list12.Add(this.reader.Read<XAC_Color>());
					}
					list7.Add(list12);
					break;
				}
				default:
					throw new XAC_LoaderException("Unknown vertex element usage");
				}
			}
			if (list7.Count<List<XAC_Color>>() != 0 && list8.Count<List<XAC_Color8>>() != 0)
			{
				throw new XAC_LoaderException("Mesh is using both 32-bit and 128-bit colors");
			}
			int num9 = 0;
			int num10 = 0;
			for (int i = 0; i < num5; i++)
			{
				int num11 = this.reader.Read<int>();
				int num12 = this.reader.Read<int>();
				int num13 = this.reader.Read<int>();
				int num14 = this.reader.Read<int>();
				if (num12 <= 0 || num9 + num12 > num3)
				{
					throw new XAC_LoaderException("Invalid number of vertices in sub mesh");
				}
				if (num11 <= 0 || num10 + num11 > num4 || num11 % 3 != 0)
				{
					throw new XAC_LoaderException("Invalid number of indices in sub mesh");
				}
				if (num13 < 0 || (long)num13 >= (long)((ulong)this._materialTotals.NumTotalMaterials))
				{
					throw new XAC_LoaderException("Invalid material ID in sub mesh");
				}
				if (num14 < 0)
				{
					throw new XAC_LoaderException("Invalid number of bones in sub mesh");
				}
				XAC_SubMesh xac_SubMesh = new XAC_SubMesh();
				xac_SubMesh.MaterialID = num13;
				if (list.Count<XAC_Vector3D>() != 0)
				{
					xac_SubMesh.Positions = list.GetRange(num9, num12);
				}
				if (list2.Count<XAC_Vector3D>() != 0)
				{
					xac_SubMesh.Normals = list2.GetRange(num9, num12);
				}
				if (list3.Count<XAC_Vector4D>() != 0)
				{
					xac_SubMesh.Tangents = list3.GetRange(num9, num12);
				}
				if (list4.Count<XAC_Vector4D>() != 0)
				{
					xac_SubMesh.Bitangents = list4.GetRange(num9, num12);
				}
				xac_SubMesh.InfluenceRangeIndices = list6.GetRange(num9, num12);
				for (int j = 0; j < list8.Count<List<XAC_Color8>>(); j++)
				{
					xac_SubMesh.Colors32.Add(list8[j].GetRange(num9, num12));
				}
				for (int j = 0; j < list7.Count<List<XAC_Color>>(); j++)
				{
					xac_SubMesh.Colors128.Add(list7[j].GetRange(num9, num12));
				}
				for (int j = 0; j < list5.Count<List<XAC_Vector2D>>(); j++)
				{
					xac_SubMesh.UVSets.Add(list5[j].GetRange(num9, num12));
				}
				for (int j = 0; j < num11; j++)
				{
					xac_SubMesh.Indices.Add(this.reader.Read<int>());
				}
				this.reader.BaseStream.Seek((long)(4 * num14), SeekOrigin.Current);
				num9 += num12;
				num10 += num11;
				xac_Mesh.SubMeshes.Add(xac_SubMesh);
			}
			if (num9 != num3)
			{
				throw new XAC_LoaderException("Number of vertices in mesh doesn't equal total number of vertices in sub meshes");
			}
			if (num10 != num4)
			{
				throw new XAC_LoaderException("Number of indices in mesh doesn't equal total number of indices in sub meshes");
			}
			this._meshes.Add(xac_Mesh);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000046BC File Offset: 0x000028BC
		private void readNodeHierarchy()
		{
			int num = this.reader.Read<int>();
			int num2 = this.reader.Read<int>();
			for (int i = 0; i < num; i++)
			{
				XAC_Node xac_Node = new XAC_Node
				{
					ID = i
				};
				xac_Node.Rotation = this.reader.Read<XAC_Quaternion>();
				xac_Node.ScaleRotation = this.reader.Read<XAC_Quaternion>();
				xac_Node.Position = this.reader.Read<XAC_Vector3D>();
				xac_Node.Scale = this.reader.Read<XAC_Vector3D>();
				this.reader.BaseStream.Seek(12L, SeekOrigin.Current);
				xac_Node.UnknownIndex1 = this.reader.Read<int>();
				xac_Node.UnknownIndex2 = this.reader.Read<int>();
				int num3 = this.reader.Read<int>();
				int num4 = this.reader.Read<int>();
				xac_Node.IncludeBoundsCalc = this.reader.Read<int>();
				xac_Node.Transform = default(XAC_Matrix44);
				xac_Node.Transform.Axis = new XAC_Vector4D[3];
				xac_Node.Transform.Axis[0] = this.reader.Read<XAC_Vector4D>();
				xac_Node.Transform.Axis[1] = this.reader.Read<XAC_Vector4D>();
				xac_Node.Transform.Axis[2] = this.reader.Read<XAC_Vector4D>();
				xac_Node.Transform.Position = this.reader.Read<XAC_Vector4D>();
				xac_Node.ImportanceFactor = this.reader.Read<float>();
				int size = (int)this.reader.Read<uint>();
				xac_Node.Name = this.reader.ReadCString(size);
				if (num3 == -1)
				{
					this._rootNodes.Add(xac_Node);
					xac_Node.Parent = null;
				}
				else
				{
					xac_Node.Parent = this._nodes[num3];
					this._nodes[num3].Children.Add(xac_Node);
				}
				this._nodes.Add(xac_Node);
			}
			if (this._rootNodes.Count != num2)
			{
				throw new XAC_LoaderException("Invalid node hierarchy - wrong number of root nodes");
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000048FC File Offset: 0x00002AFC
		private void readMaterialTotals()
		{
			this._materialTotals.NumTotalMaterials = this.reader.Read<uint>();
			this._materialTotals.NumStandardMaterials = this.reader.Read<uint>();
			this._materialTotals.NumFXMaterials = this.reader.Read<uint>();
			if (this._materialTotals.NumTotalMaterials != this._materialTotals.NumFXMaterials + this._materialTotals.NumStandardMaterials)
			{
				throw new XAC_LoaderException("Invalid material totals");
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004980 File Offset: 0x00002B80
		private void readMaterial()
		{
			XAC_Material item;
			item.AmbientColor = this.reader.Read<XAC_Color>();
			item.DiffuseColor = this.reader.Read<XAC_Color>();
			item.SpecularColor = this.reader.Read<XAC_Color>();
			item.EmissiveColor = this.reader.Read<XAC_Color>();
			item.Shine = this.reader.Read<float>();
			item.ShineStrength = this.reader.Read<float>();
			item.Opacity = this.reader.Read<float>();
			item.IOR = this.reader.Read<float>();
			item.DoubleSided = (this.reader.Read<byte>() != 0);
			item.Wireframe = (this.reader.Read<byte>() != 0);
			this.reader.BaseStream.Seek(1L, SeekOrigin.Current);
			item.NumLayers = this.reader.Read<byte>();
			int size = (int)this.reader.Read<uint>();
			item.Name = this.reader.ReadCString(size);
			item.Layers = new List<XAC_Material.Layer>();
			for (int i = 0; i < (int)item.NumLayers; i++)
			{
				XAC_Material.Layer item2 = default(XAC_Material.Layer);
				item2.Amount = this.reader.Read<float>();
				item2.UOffset = this.reader.Read<float>();
				item2.VOffset = this.reader.Read<float>();
				item2.UTiling = this.reader.Read<float>();
				item2.VTiling = this.reader.Read<float>();
				item2.Rotation = this.reader.Read<float>();
				item2.MaterialId = this.reader.Read<ushort>();
				item2.MapType = this.reader.Read<byte>();
				this.reader.BaseStream.Seek(1L, SeekOrigin.Current);
				int size2 = (int)this.reader.Read<uint>();
				item.Name = this.reader.ReadCString(size2);
				item.Layers.Add(item2);
			}
			this._materials.Add(item);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004BA8 File Offset: 0x00002DA8
		private void readChunks()
		{
			while (this.reader.BaseStream.Position < this.reader.BaseStream.Length)
			{
				uint num = this.reader.Read<uint>();
				uint num2 = this.reader.Read<uint>();
				uint num3 = this.reader.Read<uint>();
				long position = this.reader.BaseStream.Position;
				switch (num)
				{
				case 1U:
					this.readMesh();
					break;
				case 3U:
					this.readMaterial();
					break;
				case 5U:
					this.readShaderMaterial();
					break;
				case 11U:
					this.readNodeHierarchy();
					break;
				case 13U:
					this.readMaterialTotals();
					break;
				}
				this.reader.BaseStream.Seek(position + (long)((ulong)num2), SeekOrigin.Begin);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004CA8 File Offset: 0x00002EA8
		private void readShaderMaterial()
		{
			int num = this.reader.Read<int>();
			int num2 = this.reader.Read<int>();
			int num3 = this.reader.Read<int>();
			int num4 = this.reader.Read<int>();
			int num5 = this.reader.Read<int>();
			int num6 = this.reader.Read<int>();
			if (num3 != 0 || num5 != 0)
			{
				throw new XAC_LoaderException("Properties of unknown size in shader configuration");
			}
			int num7 = this.reader.Read<int>();
			string name = this.reader.ReadCString(num7);
			num7 = this.reader.Read<int>();
			string shaderName = this.reader.ReadCString(num7);
			XAC_ShaderMaterial xac_ShaderMaterial = new XAC_ShaderMaterial
			{
				Name = name,
				ShaderName = shaderName
			};
			for (int i = 0; i < num; i++)
			{
				num7 = this.reader.Read<int>();
				string key = this.reader.ReadCString(num7);
				int value = this.reader.Read<int>();
				xac_ShaderMaterial.IntProperties.Add(new KeyValuePair<string, int>(key, value));
			}
			for (int i = 0; i < num2; i++)
			{
				num7 = this.reader.Read<int>();
				string key = this.reader.ReadCString(num7);
				float value2 = this.reader.Read<float>();
				xac_ShaderMaterial.FloatProperties.Add(new KeyValuePair<string, float>(key, value2));
			}
			for (int i = 0; i < num4; i++)
			{
				num7 = this.reader.Read<int>();
				string key = this.reader.ReadCString(num7);
				bool value3 = this.reader.Read<byte>() != 0;
				xac_ShaderMaterial.BoolProperties.Add(new KeyValuePair<string, bool>(key, value3));
			}
			num7 = this.reader.Read<int>();
			this.reader.BaseStream.Seek((long)num7, SeekOrigin.Current);
			for (int i = 0; i < num6; i++)
			{
				num7 = this.reader.Read<int>();
				string key = this.reader.ReadCString(num7);
				num7 = this.reader.Read<int>();
				string value4 = this.reader.ReadCString(num7);
				xac_ShaderMaterial.StringProperties.Add(new KeyValuePair<string, string>(key, value4));
			}
			this._shaderMaterials.Add(xac_ShaderMaterial);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00004F18 File Offset: 0x00003118
		private void readHeader()
		{
			this._header.Identifier = this.reader.ReadCString(4);
			this._header.MajorVersion = this.reader.ReadByte();
			this._header.MinorVersion = this.reader.ReadByte();
			this._header.BigEndian = (this.reader.ReadByte() == 1);
			this._header.MultiplyOrder = this.reader.ReadByte();
			if (this._header.Identifier != "XAC " && this._header.Identifier != "XPM ")
			{
				throw new XAC_LoaderException("Invalid file identifier");
			}
			if (this._header.BigEndian)
			{
				throw new XAC_LoaderException("Unsupported byte order");
			}
		}

		// Token: 0x0400013F RID: 319
		private Stream stream;

		// Token: 0x04000140 RID: 320
		private BinaryReader reader;

		// Token: 0x04000141 RID: 321
		public XAC_Header _header;

		// Token: 0x04000142 RID: 322
		private List<XAC_Material> _materials;

		// Token: 0x04000143 RID: 323
		private XAC_MaterialTotals _materialTotals;

		// Token: 0x04000144 RID: 324
		private List<XAC_Node> _rootNodes;

		// Token: 0x04000145 RID: 325
		private List<XAC_Node> _nodes;

		// Token: 0x04000146 RID: 326
		private List<XAC_Mesh> _meshes;

		// Token: 0x04000147 RID: 327
		private List<XAC_ShaderMaterial> _shaderMaterials;
	}
}
