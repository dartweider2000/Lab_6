using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3dObjects
{
    class AxesCoordinates
    {
        protected double SizeAxes { get; set; }
        protected double Scale;
        public bool Back {
            set {
                if (value)
                {
                    ColorBack = Brushes.Blue;
                    ColorFront = Brushes.Gray;
                    ThicknessFront = 1;
                    ThicknessBack = 2;
                }
                else
                {
                    ColorBack = Brushes.Gray;
                    ColorFront = Brushes.Blue;
                    ThicknessFront = 2;
                    ThicknessBack = 1;
                }
            }
        }
        protected Brush ColorFront;
        protected Brush ColorBack;
        protected double ThicknessFront;
        protected double ThicknessBack;
        public Vector3d PointView { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public _3dObject Object { get; set; }
        public Vector2d PositionCenter { get; set; }
        public Dictionary<string, Vector3d> Axes { get; set; }
        public Dictionary<string, double> K { get; set; }
        public AxesCoordinates()
        {
            Axes = new Dictionary<string, Vector3d>();
            K = new Dictionary<string, double>();
            SizeAxes = 100;
            Back = false;
            Reset();
        }
        public void Reset()
        {
            Object = null;
            ResetAxes();
        }
        private void ResetAxes()
        {
            Axes.Clear();
            Axes.Add("X", new Vector3d(-1, 0, 0));
            Axes.Add("Y", new Vector3d(0, 1, 0));
            Axes.Add("Z", new Vector3d(0, 0, 1));
            K.Clear();
            K.Add("X", 1);
            K.Add("Y", 1);
            K.Add("Z", 1);
        }
        public List<Line> GetLinesAxes()
        {
            ChangeSize();
            return getLinesAxes();
        }
        protected virtual List<Line> getLinesAxes()
        {
            List<Line> lines = new List<Line>();
            Line lineX = new Line();
            Line lineY = new Line();
            Line lineZ = new Line();
            lineX.Stroke = Brushes.Black;
            lineY.Stroke = Brushes.Black;
            lineZ.Stroke = Brushes.Black;

            lineX.X1 = PositionCenter.X;
            lineX.Y1 = PositionCenter.Y;
            lineY.X1 = PositionCenter.X;
            lineY.Y1 = PositionCenter.Y;
            lineZ.X1 = PositionCenter.X;
            lineZ.Y1 = PositionCenter.Y;

            lineX.X2 = PositionCenter.X + Axes["X"].X * SizeAxes * Scale * K["X"];
            lineX.Y2 = PositionCenter.Y - Axes["X"].Z * SizeAxes * Scale * K["X"];

            lineY.X2 = PositionCenter.X + Axes["Y"].X * SizeAxes * Scale * K["Y"];
            lineY.Y2 = PositionCenter.Y - Axes["Y"].Z * SizeAxes * Scale * K["Y"];

            lineZ.X2 = PositionCenter.X + Axes["Z"].X * SizeAxes * Scale * K["Z"];
            lineZ.Y2 = PositionCenter.Y - Axes["Z"].Z * SizeAxes * Scale * K["Z"];

            lines.Add(lineX);
            lines.Add(lineY);
            lines.Add(lineZ);
            return lines;
        }
        public List<Line> GetLinesObject()
        {
            ChangeSize();
            return getLinesObject();
        }
        protected virtual List<Line> getLinesObject()
        {
            List<Line> lines = new List<Line>();
            foreach (Face face in Object.Faces)
            {
                //normals
                //Vector2d norBegin = GetPosOnScreen(face.Segments[0].Begin);
                //Vector2d norEnd = GetPosOnScreen(face.Segments[0].Begin + face.GetNormal() * 0.01);
                //Line nor = new Line();
                //nor.Stroke = Brushes.Orange;
                //nor.X1 = PositionCenter.X + norBegin.X * Scale;
                //nor.Y1 = PositionCenter.Y - norBegin.Y * Scale;
                //nor.X2 = PositionCenter.X + norEnd.X * Scale;
                //nor.Y2 = PositionCenter.Y - norEnd.Y * Scale;
                //lines.Add(nor);

                foreach (Segment segment in face.Segments)
                {
                    Line line = new Line();

                    Vector2d begin = GetPosOnScreen(segment.Begin);
                    Vector2d end = GetPosOnScreen(segment.End);

                    line.X1 = PositionCenter.X + begin.X * Scale;
                    line.Y1 = PositionCenter.Y - begin.Y * Scale;
                    line.X2 = PositionCenter.X + end.X * Scale;
                    line.Y2 = PositionCenter.Y - end.Y * Scale;

                    if (Math.Cos(CalcAngle(face.GetNormal(), PointView)) < 0)
                    {
                        line.Stroke = ColorBack;
                        line.StrokeThickness = ThicknessBack;
                    }
                    else
                    {
                        line.Stroke = ColorFront;
                        line.StrokeThickness = ThicknessFront;
                    }

                    var l = lines.Find(x => x.X1 == line.X1 && x.X2 == line.X2 && x.Y1 == line.Y1 && x.Y2 == line.Y2);
                    if (l != null)
                    {
                        if (line.Stroke == Brushes.Blue)
                        {
                            l.Stroke = Brushes.Blue;
                            l.StrokeThickness = 2;
                        }
                    }
                    else lines.Add(line);
                }
            }

            lines.Sort(new LineComparer(Brushes.Gray));
            return lines;
        }
        public double CalcAngle(Vector3d vect1, Vector3d vect2)
        {
            return Math.Acos((vect1.X * vect2.X + vect1.Y * vect2.Y + vect1.Z * vect2.Z) / (vect1.Length * vect2.Length));
        }
        private void ChangeSize()
        {
            double scale;
            if (Height < Width) scale = Height / 2 / SizeAxes;
            else scale = Width / 2 / SizeAxes;
            Scale = scale == 0 ? 1 : scale;
        }
        protected virtual Vector2d GetPosOnScreen(Vector3d point)
        {
            Vector2d vect = new Vector2d();

            vect.X += point.X * Math.Cos(CalcAngle(new Vector3d(1, 0, 0), Axes["X"])) * K["X"];
            vect.X += point.Y * Math.Cos(CalcAngle(new Vector3d(1, 0, 0), Axes["Y"])) * K["Y"];
            vect.X += point.Z * Math.Cos(CalcAngle(new Vector3d(1, 0, 0), Axes["Z"])) * K["Z"];

            vect.Y += point.X * Math.Cos(CalcAngle(new Vector3d(0, 0, 1), Axes["X"])) * K["X"];
            vect.Y += point.Y * Math.Cos(CalcAngle(new Vector3d(0, 0, 1), Axes["Y"])) * K["Y"];
            vect.Y += point.Z * Math.Cos(CalcAngle(new Vector3d(0, 0, 1), Axes["Z"])) * K["Z"];

            return vect;
        }
    }
}