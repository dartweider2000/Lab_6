using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dObjects
{
    class LogarithmicSpiral : _3dObject
    {
        private const double Height = 50;
        private const double a = 5f;
        private const double b = 1f;
        public LogarithmicSpiral(int numSegments) : base(numSegments)
        {
            NumSegments = numSegments;
        }

        protected override void Build(int numSegments)
        {
            double maxal = Math.PI * (numSegments - 2) / (numSegments - 1);
            double maxy = Equation(maxal) * Math.Sin(maxal);
            Center = new Vector3d(0, maxy / 2, Height / 2);
            Face top = new Face();
            Face bottom = new Face();
            for (int i = 0; i < numSegments - 1; i++)
            {
                double alpha = Math.PI * i / (numSegments - 1);
                double alphan = Math.PI * (i + 1) / (numSegments - 1);
                double x = Equation(alpha) * Math.Cos(alpha);
                double y = Equation(alpha) * Math.Sin(alpha);
                double xn = Equation(alphan) * Math.Cos(alphan);
                double yn = Equation(alphan) * Math.Sin(alphan);
                Face side = new Face();
                side.Add(new Segment(new Vector3d(x, y, 0), new Vector3d(xn, yn, 0)));
                bottom.Add(new Segment(new Vector3d(x, y, 0), new Vector3d(xn, yn, 0)));
                side.Add(new Segment(new Vector3d(x, y, 50), new Vector3d(xn, yn, 50)));
                top.Add(new Segment(new Vector3d(x, y, 50), new Vector3d(xn, yn, 50)));
                side.Add(new Segment(new Vector3d(x, y, 0), new Vector3d(x, y, 50)));
                side.Add(new Segment(new Vector3d(xn, yn, 0), new Vector3d(xn, yn, 50)));
                Faces.Add(side);
            }
            double xb = Equation(0) * Math.Cos(0);
            double yb = Equation(0) * Math.Sin(0);
            double xe = Equation(Math.PI) * Math.Cos(Math.PI);
            double ye = Equation(Math.PI) * Math.Sin(Math.PI);
            Face sideLast = new Face();
            sideLast.Add(new Segment(new Vector3d(xb, yb, 0), new Vector3d(xe, ye, 0)));
            bottom.Add(new Segment(new Vector3d(xb, yb, 0), new Vector3d(xe, ye, 0)));
            sideLast.Add(new Segment(new Vector3d(xb, yb, 50), new Vector3d(xe, ye, 50)));
            top.Add(new Segment(new Vector3d(xb, yb, 50), new Vector3d(xe, ye, 50)));
            sideLast.Add(new Segment(new Vector3d(xe, ye, 0), new Vector3d(xe, ye, 50)));
            sideLast.Add(new Segment(new Vector3d(xb, yb, 0), new Vector3d(xb, yb, 50)));
            Faces.Add(sideLast);
            Faces.Add(top);
            Faces.Add(bottom);
        }
        private double Equation(double alpha)
        {
            return a * Math.Pow(Math.E, b * alpha);
        }
    }
}
