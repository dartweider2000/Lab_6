using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _3dObjects
{
    class Сircle : _3dObject
    {
        private const double Radius = 45;
        private const double Height = 70;
        public Сircle(int numSegments) : base(numSegments)
        {
            NumSegments = numSegments;
        }

        protected override void Build(int numSegments)
        {
            Center = new Vector3d(0, 0, Height / 2);
            Face top = new Face();
            Face bottom = new Face();
            top.Center = bottom.Center = Center;

            for (int i = 0; i < numSegments; i++)
            {
                double x = Radius * Math.Cos(2 * Math.PI * i / numSegments);
                double y = Radius * Math.Sin(2 * Math.PI * i / numSegments);
                double xn = Radius * Math.Cos(2 * Math.PI * (i + 1) / numSegments);
                double yn = Radius * Math.Sin(2 * Math.PI * (i + 1) / numSegments);
                top.Add(new Segment(new Vector3d(x, y, 0), new Vector3d(xn, yn, 0)));
                bottom.Add(new Segment(new Vector3d(x, y, Height), new Vector3d(xn, yn, Height)));
                Face side = new Face();
                side.Center = Center;
                side.Add(new Segment(new Vector3d(x, y, 0), new Vector3d(xn, yn, 0)));
                side.Add(new Segment(new Vector3d(x, y, Height), new Vector3d(xn, yn, Height)));
                side.Add(new Segment(new Vector3d(x, y, 0), new Vector3d(x, y, Height)));
                side.Add(new Segment(new Vector3d(xn, yn, 0), new Vector3d(xn, yn, Height)));
                Faces.Add(side);
            }
            Faces.Add(top);
            Faces.Add(bottom);
        }
    }
}
