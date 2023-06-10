using System;
using System.Windows;
using System.Windows.Media.Media3D;
using IPFSuite.FileFormats.XAC;

namespace IPFSuite
{
	// Token: 0x0200003F RID: 63
	public static class XAC_Extensions
	{
		// Token: 0x06000107 RID: 263 RVA: 0x0000D2D4 File Offset: 0x0000B4D4
		public static Point ToPoint(this XAC_Vector2D v)
		{
			return new Point
			{
				X = (double)v.X,
				Y = (double)v.Y
			};
		}

		// Token: 0x06000108 RID: 264 RVA: 0x0000D310 File Offset: 0x0000B510
		public static Vector3D ToVector3D(this XAC_Vector3D v)
		{
			return new Vector3D
			{
				X = (double)v.X,
				Y = (double)v.Y,
				Z = (double)v.Z
			};
		}

		// Token: 0x06000109 RID: 265 RVA: 0x0000D35C File Offset: 0x0000B55C
		public static Point3D ToPoint3D(this XAC_Vector3D v)
		{
			return new Point3D
			{
				X = (double)v.X,
				Y = (double)v.Y,
				Z = (double)v.Z
			};
		}

		// Token: 0x0600010A RID: 266 RVA: 0x0000D3A8 File Offset: 0x0000B5A8
		public static Quaternion ToQuaternion(this XAC_Quaternion q)
		{
			return new Quaternion
			{
				X = (double)q.X,
				Y = (double)q.Y,
				Z = (double)q.Z,
				W = (double)q.W
			};
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000D404 File Offset: 0x0000B604
		public static Matrix3D ToMatrix3D(this XAC_Matrix44 m)
		{
			return new Matrix3D((double)m.Axis[0].X, (double)m.Axis[0].Y, (double)m.Axis[0].Z, 0.0, (double)m.Axis[1].X, (double)m.Axis[1].Y, (double)m.Axis[1].Z, 0.0, (double)m.Axis[2].X, (double)m.Axis[2].Y, (double)m.Axis[2].Z, 0.0, (double)m.Position.X, (double)m.Position.Y, (double)m.Position.Z, (double)m.Position.W);
		}
	}
}
