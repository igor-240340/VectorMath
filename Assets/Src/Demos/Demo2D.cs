using MyMath;
using UnityEngine;

namespace Src.Demos
{
    public abstract class Demo2D
    {
        private const float arrowHeadHeight = 0.15f;
        private const float arrowHeadWidth = 0.15f;
        private const float arrowWidth = 0.05f;

        protected Material blueMaterial;
        protected Material greyMaterial;
        protected Material magentaMaterial;

        protected Demo2D(Material blue, Material grey, Material magenta)
        {
            blueMaterial = blue;
            greyMaterial = grey;
            magentaMaterial = magenta;
        }

        protected void BuildArrowMeshForVector(Mesh mesh, Vec2 v)
        {
            Vector2 vector = new Vector2(v.x, v.y);
            Vector2 origin = Vector3.zero;

            Vector2 normal = CalculateNormal(vector);

            Vector2 arrowHeadBase = vector - vector.normalized * arrowHeadHeight;

            Vector3[] vertices = new Vector3[7];

            vertices[0] = origin + normal * (arrowWidth / 2);
            vertices[1] = origin - normal * (arrowWidth / 2);

            vertices[2] = origin + arrowHeadBase - normal * (arrowWidth / 2);
            vertices[3] = origin + arrowHeadBase + normal * (arrowWidth / 2);

            vertices[4] = origin + arrowHeadBase + normal * (arrowWidth / 2 + arrowHeadWidth / 2);
            vertices[5] = origin + arrowHeadBase - normal * (arrowWidth / 2 + arrowHeadWidth / 2);

            vertices[6] = origin + vector;

            mesh.Clear();

            mesh.vertices = vertices;
            mesh.uv = null;
            mesh.triangles = new[]
            {
                0, 1, 2,
                0, 2, 3,
                4, 5, 6
            };
            mesh.normals = new[]
            {
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward,
                -Vector3.forward
            };
        }

        private Vector2 CalculateNormal(Vector2 a)
        {
            Vector2 normal = Vector2.zero;

            if (a.y == 0)
            {
                normal.y = -(a.x / Mathf.Abs(a.x)); // Take inverse sign of x
                normal.x = -(a.y * normal.y) / a.x;
            }
            else
            {
                normal.x = a.y / Mathf.Abs(a.y); // Take sign of y
                normal.y = -(a.x * normal.x) / a.y;
            }

            return normal.normalized;
        }
    }
}