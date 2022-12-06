using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3dObjects
{
    class Horizon
    {
        public Line Line { get; private set; }
        public double Height {
            get {
                return Line.Y1;
            }
            set {
                Line.Y1 = value;
                Line.Y2 = value;
            }
        }
        public Horizon(double width, double height)
        {
            Line = new Line();
            Line.Stroke = Brushes.Red;
            Line.X1 = 0;
            Line.X2 = width;
            Line.Y1 = height / 2;
            Line.Y2 = height / 2;
        }
    }
}
