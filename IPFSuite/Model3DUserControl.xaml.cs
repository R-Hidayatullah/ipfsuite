using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Threading;
using IPFSuite.FileFormats.IES;
using IPFSuite.FileFormats.IPF;
using IPFSuite.FileFormats.XAC;
using KUtility;
using Paloma;

namespace IPFSuite
{
    // Token: 0x02000017 RID: 23
    public partial class Model3DUserControl : UserControl
    {
        // Token: 0x060000AC RID: 172 RVA: 0x000075CC File Offset: 0x000057CC
        public Model3DUserControl()
        {
            this.InitializeComponent();
            this.modelGroup = new Model3DGroup();
            this.camera = new PerspectiveCamera();
            this.camera.FieldOfView = this.cameraFOV;
            this.camera.Position = new Point3D(0.0, 25.0, 200.0);
            this.camera.LookDirection = new Vector3D(0.0, 0.0, -1.0);
            this.mainViewport.Camera = this.camera;
            Vector3D direction = new Vector3D(-1.0, -1.0, -1.0);
            direction.Normalize();
            DirectionalLight value = new DirectionalLight(Colors.White, direction);
            this.modelGroup.Children.Add(value);
            this.timer = new DispatcherTimer();
            this.timer.Interval = TimeSpan.FromMilliseconds(16.666);
            this.timer.Tick += this.Update;
            this.timer.Start();
            this.axisRotation = new AxisAngleRotation3D(new Vector3D(0.0, 1.0, 0.0), 1.0);
            ModelVisual3D modelVisual3D = new ModelVisual3D();
            modelVisual3D.Content = this.modelGroup;
            this.mainViewport.Children.Add(modelVisual3D);
            this.initMaterials();
        }

        // Token: 0x060000AD RID: 173 RVA: 0x0000779C File Offset: 0x0000599C
        private void mouseWheel(object sender, MouseWheelEventArgs e)
        {
            this.zoom *= 1f + (float)e.Delta / 2400f;
            this.zoom = Math.Max(0.01f, Math.Min(20f, this.zoom));
            this.setCamera();
        }

        // Token: 0x060000AE RID: 174 RVA: 0x000077EF File Offset: 0x000059EF
        public void Update(object sender, EventArgs e)
        {
            if (this.modelGroup.Children.Count != 0)
            {
                this.axisRotation.Angle += 0.5;
            }
        }

        // Token: 0x060000AF RID: 175 RVA: 0x00007820 File Offset: 0x00005A20
        private void initMaterials()
        {
            this._materialColors.Add(System.Windows.Media.Brushes.Gray);
            foreach (System.Windows.Media.Brush brush in this._materialColors)
            {
                this._materials.Add(new DiffuseMaterial(brush));
            }
        }

        // Token: 0x060000B0 RID: 176 RVA: 0x00007890 File Offset: 0x00005A90
        private BitmapSource bitmapToBitmapSource(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                throw new ArgumentNullException("bitmap");
            }
            Rectangle rect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
            BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.ReadWrite, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            BitmapSource result;
            try
            {
                int bufferSize = rect.Width * rect.Height * 4;
                result = BitmapSource.Create(bitmap.Width, bitmap.Height, (double)bitmap.HorizontalResolution, (double)bitmap.VerticalResolution, PixelFormats.Bgra32, null, bitmapData.Scan0, bufferSize, bitmapData.Stride);
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }
            return result;
        }

        // Token: 0x060000B1 RID: 177 RVA: 0x00007930 File Offset: 0x00005B30
        private MeshGeometry3D createSubMeshGeometry(XAC_SubMesh subMesh)
        {
            MeshGeometry3D meshGeometry3D = new MeshGeometry3D();
            for (int i = 0; i < subMesh.Positions.Count; i++)
            {
                Point3D value = subMesh.Positions[i].ToPoint3D();
                meshGeometry3D.Positions.Add(value);
            }
            for (int j = 0; j < subMesh.Indices.Count; j++)
            {
                int value2 = subMesh.Indices[j];
                meshGeometry3D.TriangleIndices.Add(value2);
            }
            for (int k = 0; k < subMesh.Normals.Count; k++)
            {
                Vector3D value3 = subMesh.Normals[k].ToVector3D();
                meshGeometry3D.Normals.Add(value3);
            }
            if (subMesh.UVSets.Count > 0)
            {
                for (int l = 0; l < subMesh.UVSets[0].Count; l++)
                {
                    meshGeometry3D.TextureCoordinates.Add(subMesh.UVSets[0][l].ToPoint());
                }
            }
            meshGeometry3D.Freeze();
            return meshGeometry3D;
        }

        // Token: 0x060000B2 RID: 178 RVA: 0x00007A3C File Offset: 0x00005C3C
        private Model3DGroup buildModel(XAC_Node node, List<Bitmap> textures, int shaderMaterialOffset)
        {
            Model3DGroup model3DGroup = new Model3DGroup();
            Model3DGroup result;
            if (node == null)
            {
                result = model3DGroup;
            }
            else
            {
                Vector3D scale = node.Scale.ToVector3D();
                node.ScaleRotation.ToQuaternion();
                Quaternion quaternion2 = node.Rotation.ToQuaternion();
                Matrix3D matrix = node.Transform.ToMatrix3D();
                Vector3D offset = node.Position.ToVector3D();
                matrix.Translate(offset);
                matrix.Rotate(quaternion2);
                matrix.Scale(scale);
                model3DGroup.Transform = new MatrixTransform3D(matrix);
                if (node.VisualMesh != null)
                {
                    foreach (XAC_SubMesh xac_SubMesh in node.VisualMesh.SubMeshes)
                    {
                        Geometry3D geometry = this.createSubMeshGeometry(xac_SubMesh);
                        Material material = this._materials[xac_SubMesh.MaterialID % this._materials.Count];
                        if (xac_SubMesh.MaterialID >= shaderMaterialOffset && textures != null)
                        {
                            Bitmap bitmap = textures[xac_SubMesh.MaterialID - shaderMaterialOffset];
                            if (bitmap != null)
                            {
                                material = new DiffuseMaterial(new ImageBrush(this.bitmapToBitmapSource(bitmap)));
                            }
                        }
                        GeometryModel3D geometryModel3D = new GeometryModel3D(geometry, material);
                        geometryModel3D.BackMaterial = geometryModel3D.Material;
                        model3DGroup.Children.Add(geometryModel3D);
                    }
                }
                foreach (XAC_Node node2 in node.Children)
                {
                    model3DGroup.Children.Add(this.buildModel(node2, textures, shaderMaterialOffset));
                }
                result = model3DGroup;
            }
            return result;
        }

        // Token: 0x060000B3 RID: 179 RVA: 0x00007BEC File Offset: 0x00005DEC
        private List<Bitmap> loadTextures(XAC xac, string xacName, string ipfFolder)
        {
            Console.WriteLine("load");
            if (xac.MaterialTotals.NumFXMaterials != 0U)
            {
                IPF ipf = null;
                try
                {
                    ipf = new IPF(ipfFolder + "\\ies_client.ipf");
                }
                catch
                {
                    return null;
                }
                ipf.LoadSync();
                Console.WriteLine("load ipf");
                int i = 0;
                while (i < ipf.FileTable.Length && !(ipf.FileTable[i].fileName == "xac.ies"))
                {
                    i++;
                }
                if (i < ipf.FileTable.Length)
                {
                    byte[] content = ipf.Extract(i);
                    ipf.Close();
                    FileIes iesfile;
                    try
                    {
                        iesfile = new FileIes(content);
                    }
                    catch
                    {
                        return null;
                    }
                    Console.WriteLine("load mesh");
                    using (List<IesRow>.Enumerator enumerator = iesfile.Rows.GetEnumerator())
                    {
                        while (enumerator.MoveNext())
                        {
                            IesRow iesRow = enumerator.Current;
                            string mesh_name = (string)iesRow["Mesh"];
                            if (mesh_name.Length != 0 && mesh_name.Split(new char[]
                            {
                                '\\'
                            }).Last<string>().Equals(xacName, StringComparison.InvariantCultureIgnoreCase))
                            {
                                string path_name = (string)iesRow["Path"];
                                if (path_name.Length == 0)
                                {
                                    return null;
                                }
                                string[] array = path_name.Split(new char[]
                                {
                                    '\\'
                                });
                                string text4 = array[0];
                                string text5 = string.Join("\\", array.Skip(1).ToArray<string>());
                                if (text5 == string.Empty)
                                {
                                    text5 = "\\";
                                }
                                IPF ipf2;
                                try
                                {
                                    ipf2 = new IPF(ipfFolder + "\\" + text4 + ".ipf");
                                }
                                catch
                                {
                                    return null;
                                }
                                ipf2.LoadSync();
                                List<string> list2 = new List<string>();
                                foreach (XAC_ShaderMaterial xac_ShaderMaterial in xac.ShaderMaterials)
                                {
                                    string item = null;
                                    foreach (KeyValuePair<string, string> keyValuePair in xac_ShaderMaterial.StringProperties)
                                    {
                                        if (keyValuePair.Key == "DiffuseTex")
                                        {
                                            item = text5 + keyValuePair.Value.ToLower().Replace(".tga", ".dds");
                                            break;
                                        }
                                    }
                                    list2.Add(item);
                                }


                                Bitmap[] array2 = new Bitmap[xac.MaterialTotals.NumFXMaterials];
                                int num4 = 0;
                                int num5 = 0;
                                IPFFileTable[] fileTable = ipf2.FileTable;
                                for (int j = 0; j < fileTable.Length; j++)
                                {
                                    IPFFileTable ipffileTable = fileTable[j];
                                    string fileName = ipffileTable.directoryName + "\\" + ipffileTable.fileName.ToLower();
                                    if (list2.Contains(fileName, StringComparer.CurrentCultureIgnoreCase))
                                    {
                                        int num6 = Array.FindIndex<string>(list2.ToArray(), (string t) => t.IndexOf(fileName, StringComparison.InvariantCultureIgnoreCase) >= 0);
                                        string text6 = fileName.Split(new char[]
                                        {
                                            '.'
                                        }).Last<string>();
                                        byte[] array3 = ipf2.Extract(num4);
                                        Bitmap bitmap = null;
                                        try
                                        {
                                            string text7 = text6;
                                            if (text7 != null)
                                            {
                                                if (text7 == "dds")
                                                {
                                                    bitmap = new DDSImage(array3).images[0];
                                                    goto IL_375;
                                                }
                                                if (text7 == "tga")
                                                {
                                                    bitmap = new TargaImage(array3).Image;
                                                    goto IL_375;
                                                }
                                            }
                                            using (MemoryStream memoryStream = new MemoryStream(array3))
                                            {
                                                bitmap = new Bitmap(memoryStream);
                                            }
                                        IL_375:;
                                        }
                                        catch (Exception)
                                        {
                                            bitmap = null;
                                        }
                                        array2[num6] = bitmap;
                                        num5++;
                                    }
                                    if (num5 == list2.Count)
                                    {
                                        break;
                                    }
                                    num4++;
                                }
                                ipf2.Close();
                                return array2.ToList<Bitmap>();
                            }
                        }
                        goto IL_3DF;
                    }
                }
                ipf.Close();
            IL_3DF:
                return null;
            }
            return null;
        }

        // Token: 0x060000B4 RID: 180 RVA: 0x000080A4 File Offset: 0x000062A4
        public void SetModel(string filename, XAC xac)
        {
            if (this.lastModel != null)
            {
                this.modelGroup.Children.Remove(this.lastModel);
                this.lastModel = null;
            }

            if (xac != null)
            {
                List<Bitmap> textures = this.loadTextures(xac, filename, fMain.IpfDirectory);
                uint numStandardMaterials = xac.MaterialTotals.NumStandardMaterials;
                Model3DGroup model3DGroup = new Model3DGroup();
                foreach (XAC_Node node2 in xac.RootNodes.FindAll((XAC_Node node) => node.IsVisible()))
                {
                    model3DGroup.Children.Add(this.buildModel(node2, textures, (int)numStandardMaterials));
                }
                this.axisRotation.Angle = 180.0;
                this.zoom = 1f;
                model3DGroup.Transform = new RotateTransform3D(this.axisRotation);
                this.modelGroup.Children.Add(model3DGroup);
                this.viewRect = model3DGroup.Bounds;
                this.setCamera();
                this.lastModel = model3DGroup;
            }
        }

        // Token: 0x060000B5 RID: 181 RVA: 0x000081D0 File Offset: 0x000063D0
        private void setCamera()
        {
            Rect3D rect3D = this.viewRect;
            Vector3D vector3D = new Vector3D(rect3D.Location.X, rect3D.Location.Y, rect3D.Location.Z);
            Vector3D vector = new Vector3D(rect3D.Size.X, rect3D.Size.Y, rect3D.Size.Z) + vector3D;
            Vector3D vector3D2 = (vector3D + vector) / 2.0;
            double num = (vector.Y - vector3D.Y) / Math.Tan(this.cameraFOV);
            this.camera.Position = new Point3D(vector3D2.X, vector3D2.Y, vector3D2.Z + (double)this.zoom * num);
        }

        // Token: 0x0400013D RID: 317
        private Model3DGroup modelGroup;

        // Token: 0x0400013E RID: 318
        private PerspectiveCamera camera;

        // Token: 0x0400013F RID: 319
        private double cameraFOV = 60.0;

        // Token: 0x04000140 RID: 320
        private DispatcherTimer timer;

        // Token: 0x04000141 RID: 321
        private AxisAngleRotation3D axisRotation;

        // Token: 0x04000142 RID: 322
        private Model3D lastModel;

        // Token: 0x04000143 RID: 323
        private float zoom = 1f;

        // Token: 0x04000144 RID: 324
        private Rect3D viewRect;

        // Token: 0x04000145 RID: 325
        private Material _stdMaterial = new DiffuseMaterial(System.Windows.Media.Brushes.Gray);

        // Token: 0x04000146 RID: 326
        private List<Material> _materials = new List<Material>();

        // Token: 0x04000147 RID: 327
        private List<System.Windows.Media.Brush> _materialColors = new List<System.Windows.Media.Brush>();
    }
}
