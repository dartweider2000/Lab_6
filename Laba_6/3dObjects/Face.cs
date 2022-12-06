using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dObjects
{
    class Face
    {
        public List<Segment> Segments { get; set; }
        public Vector3d Center { get; set; }
        public Face()
        {
            Segments = new List<Segment>();
        }
        public Vector3d GetNormal()
        {
            Vector3d normal = new Vector3d();
            if (Segments.Count > 1)
            {
                Vector3d point1 = Segments[0].Begin;
                Vector3d point2 = Segments[0].End;
                Vector3d point3 = Segments[1].Begin != point1 && Segments[1].Begin != point2 ? Segments[1].Begin : Segments[1].End;

                normal.X = (point2.Y - point1.Y) * (point3.Z - point1.Z) - (point3.Y - point1.Y) * (point2.Z - point1.Z);
                normal.Y = -((point2.X - point1.X) * (point3.Z - point1.Z) - (point3.X - point1.X) * (point2.Z - point1.Z));
                normal.Z = (point2.X - point1.X) * (point3.Y - point1.Y) - (point3.X - point1.X) * (point2.Y - point1.Y);

                if (Math.Cos(Vector3d.CalcAngle(normal, new Vector3d(point1.X - Center.X, point1.Y - Center.Y, point1.Z - Center.Z))) < 0) normal *= -1;

            }
            return normal;
        }
        public void Add(Segment segment)
        {
            Segments.Add(segment);
        }
    }
}
