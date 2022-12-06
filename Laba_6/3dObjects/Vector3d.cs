using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3dObjects
{
    struct Vector3d
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double Length {
            get {
                return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2) + Math.Pow(Z, 2));
            }
        }
        public Vector3d(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static bool operator !=(Vector3d a, Vector3d b)
        {
            return !(a == b);
        }
        public static bool operator ==(Vector3d a, Vector3d b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }
        public static Vector3d operator *(Vector3d a, double b)
       => new Vector3d(a.X * b, a.Y * b, a.Z * b);
        public static Vector3d operator +(Vector3d a, Vector3d b)
=> new Vector3d(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        static public double CalcAngle(Vector3d vect1, Vector3d vect2)
        {
            return Math.Acos((vect1.X * vect2.X + vect1.Y * vect2.Y + vect1.Z * vect2.Z) / (vect1.Length * vect2.Length));
        }
        static public bool operator >(Vector3d a, Vector3d b)
        {
            return !(a < b);
        }
        static public bool operator <(Vector3d a, Vector3d b)
        {
            if (a.Z < b.Z) return true;
            else if (a.Z == b.Z && a.Y < b.Y) return true;
            else if (a.Z == b.Z && a.Y == b.Y && a.X < b.X) return true;
            return false;
        }
        public override string ToString()
        {
            return X + " " + Y + " " + Z;
        }
    }
}
