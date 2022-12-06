using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dObjects
{
    struct Vector2d
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Length {
            get {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
            }
        }
        public Vector2d(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return X + " " + Y;
        }
    }
}
