using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace _3dObjects
{
    class LineComparer : IComparer<Line>
    {
        public Brush Color { get; set; }
        public LineComparer(Brush color)
        {
            Color = color;
        }
        public int Compare(Line x, Line y)
        {
            if (x.Stroke == Color && y.Stroke == Color) return 0;
            else if (x.Stroke == Color) return -1;
            else if (y.Stroke == Color) return 1;
            else return 0;
        }
    }
}
