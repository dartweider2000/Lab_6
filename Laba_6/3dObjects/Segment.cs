using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace _3dObjects
{
    class Segment
    {
        public Vector3d Begin { get; set; }
        public Vector3d End { get; set; }
        public Segment(Vector3d begin, Vector3d end)
        {
            Begin = begin;
            End = end;
        }
    }
}
