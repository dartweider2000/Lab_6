using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dObjects
{
    abstract class _3dObject
    {
        public List<Face> Faces { get; protected set; }
        private Vector3d center;
        public Vector3d Center {
            get {
                return center;
            }
            set {
                foreach (Face face in Faces) face.Center = value;
                center = value;
            }
        }
        protected int NumSegments;

        public _3dObject(int numSegments)
        {
            Faces = new List<Face>();
            Build(numSegments);
        }

        protected abstract void Build(int numSegments);
        public void Reset()
        {
            Faces.Clear();
            Build(NumSegments);
        }
        public void ToReflectX()
        {
            Center = new Vector3d(-Center.X, Center.Y, Center.Z);
            foreach (Face face in Faces)
            {
                foreach (Segment seg in face.Segments)
                {
                    seg.Begin = new Vector3d(-seg.Begin.X, seg.Begin.Y, seg.Begin.Z);
                    seg.End = new Vector3d(-seg.End.X, seg.End.Y, seg.End.Z);
                }
            }
        }
        public void ToReflectY()
        {
            Center = new Vector3d(Center.X, -Center.Y, Center.Z);
            foreach (Face face in Faces)
            {
                foreach (Segment seg in face.Segments)
                {
                    seg.Begin = new Vector3d(seg.Begin.X, -seg.Begin.Y, seg.Begin.Z);
                    seg.End = new Vector3d(seg.End.X, -seg.End.Y, seg.End.Z);
                }
            }
        }
        public void ToReflectZ()
        {
            Center = new Vector3d(Center.X, Center.Y, -Center.Z);
            foreach (Face face in Faces)
            {
                foreach (Segment seg in face.Segments)
                {
                    seg.Begin = new Vector3d(seg.Begin.X, seg.Begin.Y, -seg.Begin.Z);
                    seg.End = new Vector3d(seg.End.X, seg.End.Y, -seg.End.Z);
                }
            }
        }
        public void ToRotateX(double rotateX)
        {
            rotateX = -rotateX * 2 * Math.PI / 360;

            Center = ToRotatePointX(Center, rotateX);
            foreach (Face face in Faces)
            {
                foreach (Segment seg in face.Segments)
                {
                    seg.Begin = ToRotatePointX(seg.Begin, rotateX);
                    seg.End = ToRotatePointX(seg.End, rotateX);
                }
            }
        }
        public void ToRotateY(double rotateY)
        {
            rotateY = -rotateY * 2 * Math.PI / 360;

            Center = ToRotatePointY(Center, rotateY);
            foreach (Face face in Faces)
            {
                foreach (Segment seg in face.Segments)
                {
                    seg.Begin = ToRotatePointY(seg.Begin, rotateY);
                    seg.End = ToRotatePointY(seg.End, rotateY);
                }
            }
        }
        public void ToRotateZ(double rotateZ)
        {
            rotateZ = -rotateZ * 2 * Math.PI / 360;

            Center = ToRotatePointZ(Center, rotateZ);
            foreach (Face face in Faces)
            {
                foreach (Segment seg in face.Segments)
                {
                    seg.Begin = ToRotatePointZ(seg.Begin, rotateZ);
                    seg.End = ToRotatePointZ(seg.End, rotateZ);
                }
            }
        }
        private Vector3d ToRotatePointX(Vector3d point, double rotateX)
        {
            Vector3d pointOnScreen = new Vector3d();

            pointOnScreen.X = point.X;
            pointOnScreen.Y = point.Y * Math.Cos(rotateX) - point.Z * Math.Sin(rotateX);
            pointOnScreen.Z = point.Y * Math.Sin(rotateX) + point.Z * Math.Cos(rotateX);

            return pointOnScreen;
        }
        private Vector3d ToRotatePointY(Vector3d point, double rotateY)
        {
            Vector3d pointOnScreen = new Vector3d();

            pointOnScreen.X = point.X * Math.Cos(rotateY) + point.Z * Math.Sin(rotateY);
            pointOnScreen.Y = point.Y;
            pointOnScreen.Z = point.Z * Math.Cos(rotateY) - point.X * Math.Sin(rotateY);

            return pointOnScreen;
        }
        private Vector3d ToRotatePointZ(Vector3d point, double rotateZ)
        {
            Vector3d pointOnScreen = new Vector3d();

            pointOnScreen.X = point.X * Math.Cos(rotateZ) - point.Y * Math.Sin(rotateZ);
            pointOnScreen.Y = point.X * Math.Sin(rotateZ) + point.Y * Math.Cos(rotateZ);
            pointOnScreen.Z = point.Z;

            return pointOnScreen;
        }
    }
}
