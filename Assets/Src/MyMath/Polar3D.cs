using System;
using UnityEngine;

namespace MyMath
{
    public struct Polar3D : IFormattable
    {
        public float radius;
        public float theta;
        public float phi;

        public Polar3D(float radius, float theta, float phi)
        {
            this.radius = radius;
            this.theta = theta;
            this.phi = phi;
        }

        public Vec3 ToVec3()
        {
            float y = Mathf.Cos(theta) * radius;

            float projLength = Mathf.Sin(theta) * radius; // Length of vector projection onto plane XZ
            float x = Mathf.Cos(phi) * projLength;
            float z = Mathf.Sin(phi) * projLength;

            return new Vec3(x, y, z);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return $"({radius}, {theta * Mathf.Rad2Deg}, {phi * Mathf.Rad2Deg})";
        }
    }
}