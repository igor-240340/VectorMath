using System;
using UnityEngine;

namespace MyMath
{
    public struct Vec3 : IFormattable
    {
        public float x;
        public float y;
        public float z;

        public Vec3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static float Dot(Vec3 a, Vec3 b)
        {
            return a.x * b.x + a.y * b.y + a.z * b.z;
        }

        public static Vec3 Cross(Vec3 a, Vec3 b)
        {
            return new Vec3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
        }

        public float Length()
        {
            return Mathf.Sqrt(x * x + y * y + z * z);
        }

        public Polar3D ToPolar3D()
        {
            float theta = Mathf.Acos(Mathf.Clamp(y / Length(), -1, 1));
            float phi = Mathf.Atan2(z, x);
            if (phi < 0)
                phi += Mathf.PI * 2;

            return new Polar3D(Length(), theta, phi);
        }

        public Vector3 ToVector3()
        {
            return new Vector3(x, y, z);
        }

        public Vec3 Normalize()
        {
            return this / Length();
        }

        public static Vec3 operator -(Vec3 vectorA, Vec3 vectorB)
        {
            return new Vec3(vectorA.x - vectorB.x, vectorA.y - vectorB.y, vectorA.z - vectorB.z);
        }

        public static Vec3 operator *(float scalar, Vec3 vector)
        {
            return vector * scalar;
        }

        public static Vec3 operator *(Vec3 vector, float scalar)
        {
            return new Vec3(vector.x * scalar, vector.y * scalar, vector.z * scalar);
        }

        public static Vec3 operator -(Vec3 vector)
        {
            return new Vec3(-vector.x, -vector.y, -vector.z);
        }

        public static Vec3 operator +(Vec3 vectorA, Vec3 vectorB)
        {
            return new Vec3(vectorA.x + vectorB.x, vectorA.y + vectorB.y, vectorA.z + vectorB.z);
        }

        public static Vec3 operator /(Vec3 vector, float scalar)
        {
            float inv = 1f / scalar;
            return new Vec3(vector.x * inv, vector.y * inv, vector.z * inv);
        }

        public static Vec3 Zero => new Vec3(0, 0, 0);

        public static Vec3 Up => new Vec3(0, 1, 0);

        public static Vec3 Down => new Vec3(0, -1, 0);

        public static Vec3 Left => new Vec3(-1, 0, 0);

        public static Vec3 Right => new Vec3(1, 0, 0);

        public static Vec3 Forward => new Vec3(0, 0, 1);

        public static Vec3 Back => new Vec3(0, 0, -1);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"({x},{y},{z})";
        }
    }
}