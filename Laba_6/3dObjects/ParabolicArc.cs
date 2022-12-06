using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dObjects
{
    class ParabolicArc : _3dObject
    {
        private const double MaxX = 70;
        private const double Height = 50;
        public ParabolicArc(int numSegments) : base(numSegments)
        {
            NumSegments = numSegments;
        }

        protected override void Build(int numSegments)
        {
            double MaxY = Equation(MaxX);
            Center = new Vector3d(0, Equation(MaxX / 2), Height / 2);

            Face top = new Face();
            Face bottom = new Face();
            top.Center = bottom.Center = Center;

            double HeightSeg = MaxX * 2 / (numSegments - 1);
            double MinY = Equation(HeightSeg / 2);

            top.Add(new Segment(new Vector3d(-MaxX, MaxY, Height), new Vector3d(MaxX, MaxY, Height)));
            bottom.Add(new Segment(new Vector3d(-MaxX, MaxY, 0), new Vector3d(MaxX, MaxY, 0)));
            top.Add(new Segment(new Vector3d(-HeightSeg / 2, MinY, Height), new Vector3d(HeightSeg / 2, MinY, Height)));
            bottom.Add(new Segment(new Vector3d(-HeightSeg / 2, MinY, 0), new Vector3d(HeightSeg / 2, MinY, 0)));

            Face sideFirst = new Face();
            sideFirst.Add(new Segment(new Vector3d((HeightSeg / 2), Equation(HeightSeg / 2), Height), new Vector3d((HeightSeg / 2), Equation(HeightSeg / 2), 0)));
            sideFirst.Add(new Segment(new Vector3d(-(HeightSeg / 2), Equation(HeightSeg / 2), Height), new Vector3d(-(HeightSeg / 2), Equation(HeightSeg / 2), 0)));
            sideFirst.Add(new Segment(new Vector3d((HeightSeg / 2), Equation(HeightSeg / 2), 0), new Vector3d(-(HeightSeg / 2), Equation(HeightSeg / 2), 0)));
            sideFirst.Add(new Segment(new Vector3d((HeightSeg / 2), Equation(HeightSeg / 2), Height), new Vector3d(-(HeightSeg / 2), Equation(HeightSeg / 2), Height)));

            Face sideLast = new Face();
            sideLast.Add(new Segment(new Vector3d(-MaxX, MaxY, Height), new Vector3d(-MaxX, MaxY, 0)));
            sideLast.Add(new Segment(new Vector3d(MaxX, MaxY, Height), new Vector3d(MaxX, MaxY, 0)));
            sideLast.Add(new Segment(new Vector3d(MaxX, MaxY, 0), new Vector3d(-MaxX, MaxY, 0)));
            sideLast.Add(new Segment(new Vector3d(MaxX, MaxY, Height), new Vector3d(-MaxX, MaxY, Height)));

            for (int i = 0; i < numSegments / 2 - 1; i++)
            {
                double x = HeightSeg / 2 + i * HeightSeg;
                double y = Equation(x);
                double xn = HeightSeg / 2 + (i + 1) * HeightSeg;
                double yn = Equation(xn);
                top.Add(new Segment(new Vector3d(x, y, Height), new Vector3d(xn, yn, Height)));
                top.Add(new Segment(new Vector3d(-x, y, Height), new Vector3d(-xn, yn, Height)));

                bottom.Add(new Segment(new Vector3d(x, y, 0), new Vector3d(xn, yn, 0)));
                bottom.Add(new Segment(new Vector3d(-x, y, 0), new Vector3d(-xn, yn, 0)));

                Face side1 = new Face();
                side1.Add(new Segment(new Vector3d(x, y, Height), new Vector3d(x, y, 0)));
                side1.Add(new Segment(new Vector3d(xn, yn, Height), new Vector3d(xn, yn, 0)));
                side1.Add(new Segment(new Vector3d(x, y, Height), new Vector3d(xn, yn, Height)));
                side1.Add(new Segment(new Vector3d(x, y, 0), new Vector3d(xn, yn, 0)));

                Face side2 = new Face();
                side2.Add(new Segment(new Vector3d(-x, y, Height), new Vector3d(-x, y, 0)));
                side2.Add(new Segment(new Vector3d(-xn, yn, Height), new Vector3d(-xn, yn, 0)));
                side2.Add(new Segment(new Vector3d(-x, y, Height), new Vector3d(-xn, yn, Height)));
                side2.Add(new Segment(new Vector3d(-x, y, 0), new Vector3d(-xn, yn, 0)));

                Faces.Add(side1);
                Faces.Add(side2);
            }
            Faces.Add(sideLast);
            Faces.Add(sideFirst);
            Faces.Add(top);
            Faces.Add(bottom);
        }

        private double Equation(double x)
        {
            return 0.015f * x * x;
        }
    }
}
