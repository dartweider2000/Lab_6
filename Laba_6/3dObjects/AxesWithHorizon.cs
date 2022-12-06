using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3dObjects
{
    class AxesWithHorizon : AxesCoordinates
    {
        public Horizon Horizon { get; set; }
        private Vector3d shifts;
        public Vector3d Shifts {
            get {
                return shifts;
            }
            set {
                Horizon.Height += shifts.Z;
                Horizon.Height -= value.Z;
                shifts = value;
            }
        }
        public AxesWithHorizon(double width, double height)
        {
            Horizon = new Horizon(width, height);
            Shifts = new Vector3d(0, 0, 0);
        }
        protected override List<Line> getLinesAxes()
        {
            List<Line> lines = new List<Line>();
            Line lineX = new Line();
            Line lineY = new Line();
            Line lineZ = new Line();
            lineX.Stroke = Brushes.Black;
            lineY.Stroke = Brushes.Black;
            lineZ.Stroke = Brushes.Black;

            Vector2d begin = GetPosOnScreen(new Vector3d());

            Vector2d end = GetPosOnScreen(Axes["X"] * SizeAxes * K["X"]);
            lineX.X1 = PositionCenter.X + begin.X * Scale;
            lineX.Y1 = PositionCenter.Y - begin.Y * Scale;
            lineX.X2 = PositionCenter.X + end.X * Scale;
            lineX.Y2 = PositionCenter.Y - end.Y * Scale;

            end = GetPosOnScreen(Axes["Y"] * SizeAxes * K["Y"]);
            lineY.X1 = PositionCenter.X + begin.X * Scale;
            lineY.Y1 = PositionCenter.Y - begin.Y * Scale;
            lineY.X2 = PositionCenter.X + end.X * Scale;
            lineY.Y2 = PositionCenter.Y - end.Y * Scale;

            end = GetPosOnScreen(Axes["Z"] * SizeAxes * K["Z"]);
            lineZ.X1 = PositionCenter.X + begin.X * Scale;
            lineZ.Y1 = PositionCenter.Y - begin.Y * Scale;
            lineZ.X2 = PositionCenter.X + end.X * Scale;
            lineZ.Y2 = PositionCenter.Y - end.Y * Scale;

            lines.Add(lineX);
            lines.Add(lineY);
            lines.Add(lineZ);

            return lines;
        }
        protected override List<Line> getLinesObject()
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

                    if (Math.Cos(CalcAngle(face.GetNormal(), new Vector3d(Shifts.X - face.Segments[0].Begin.X, Shifts.Y - face.Segments[0].Begin.Y, Shifts.Z - face.Segments[0].Begin.Z))) < 0)
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
        protected override Vector2d GetPosOnScreen(Vector3d point)
        {
            Vector2d vect = new Vector2d();
            double k = (10 - Shifts.Y) / (point.Y - Shifts.Y);
            double x = k * (point.X - Shifts.X) + Shifts.X;
            double z = k * (point.Z - Shifts.Z) + Shifts.Z;

            vect.X = x;
            vect.Y = z;
            
            return vect;
        }
        public void ToShiftX(double shift)
        {
            Shifts = new Vector3d(shift, Shifts.Y, Shifts.Z);
        }
        public void ToShiftY(double shift)
        {
            Shifts = new Vector3d(Shifts.X, shift, Shifts.Z);
        }
        public void ToShiftZ(double shift)
        {
            Shifts = new Vector3d(Shifts.X, Shifts.Y, shift);
        }
    }
}