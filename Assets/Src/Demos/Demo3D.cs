using MyMath;
using UnityEngine;

namespace Src.Demos
{
    public abstract class Demo3D
    {
        private float angleRadius = 0.2f;
    
        // Proportions of Unity's gizmo arrows
        private const float cylRadius = 0.02f;
        private const float coneRadius = cylRadius * 4;
        private const float coneHeight = coneRadius * 2 * 1.4f;
        private float cylHeight;

        protected Material blueMaterial;
        protected Material greyMaterial;
        protected Material magentaMaterial;

        protected Demo3D(Material blue, Material grey, Material magenta)
        {
            blueMaterial = blue;
            greyMaterial = grey;
            magentaMaterial = magenta;
        }

        protected void BuildArrowMeshForVector(Mesh mesh, Vec3 vector)
        {
            int j = 1; // Vert/normal index
            int k = 0; // Triangle index

            const int cylBaseVertCount = 12;

            const int cylVertCount = cylBaseVertCount * 8 + 2;
            int cylLastVertIndex = cylVertCount - 1;
            const int cylNormCount = cylVertCount;
            const int cylTriangleCount = cylVertCount * 4 * 3;

            const int conBaseVertCount = 12;

            const int conVertCount = conBaseVertCount * 5 + 2;
            const int conNormCount = conVertCount;
            const int conTriangleCount = conVertCount * 2 * 3;

            Vector3[] vertices = new Vector3[cylVertCount + conVertCount];
            Vector3[] normals = new Vector3[cylNormCount + conNormCount];
            int[] triangles = new int[cylTriangleCount + conTriangleCount];

            Polar3D yPolar = vector.ToPolar3D();
            Vec3 x = Rotate(Basis3D.Standard.x, yPolar.theta, yPolar.phi);
            Vec3 z = Vec3.Cross(x, vector).Normalize();
            Vec3 y = vector.Normalize();
            Basis3D basis = new Basis3D(x, y, z); // A basis with the current vector as Y axis

            BuildCylinder(ref vertices, ref normals, ref triangles, ref j, ref k, cylLastVertIndex, vector, basis);
            BuildCone(ref vertices, ref normals, ref triangles, ref j, ref k, basis);

            mesh.Clear();

            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = triangles;
        }

        protected void BuildAngleMesh(float radAngle, Basis2D basis, ref Mesh mesh, float radAngleOffset = 0)
        {
            int triangleCount = Mathf.CeilToInt(radAngle * (24 / (Mathf.PI * 2)));
            int vertexCount = triangleCount + 2;
            float radStep = radAngle / triangleCount;

            if (vertexCount < 0)
                Debug.Log($"vertexCount: {vertexCount}, angle: {radAngle}, triangle count: {triangleCount}");

            Vector3[] vertices = new Vector3[vertexCount];
            Vector3[] normals = new Vector3[vertexCount];
            int[] triangles = new int[triangleCount * 3];

            vertices[0] = Vector3.zero;
            normals[0] = -Vector3.forward;
            float curAngle = 0;
            for (int v = 1, t = 0; v < vertexCount; curAngle = v * radStep, v++, t += 3)
            {
                // Calculate in the new basis
                vertices[v] = new Vector3(Mathf.Cos(curAngle + radAngleOffset) * angleRadius,
                    Mathf.Sin(curAngle + radAngleOffset) * angleRadius);
                // Get coordinates in standard basis for rendering
                vertices[v] = RecalcInBasis(vertices[v], basis);
                normals[v] = normals[0];

                if (t < triangleCount * 3)
                {
                    triangles[t] = 0;
                    triangles[t + 1] = v + 1;
                    triangles[t + 2] = v;
                }
            }

            mesh.Clear();
            
            mesh.vertices = vertices;
            mesh.normals = normals;
            mesh.triangles = triangles;
        }

        protected Basis2D CreateBasisFromVectors(Vec3 x, Vec3 y)
        {
            Vec3 unitX = x.Normalize();
            Vec3 projYOnX = Vec3.Dot(y, unitX) * unitX;
            Vec3 perpToX = y - projYOnX;

            return new Basis2D(unitX, perpToX.Normalize());
        }

        private Vec3 Rotate(Vec3 v, float theta, float phi)
        {
            Polar3D vPolar = v.ToPolar3D();

            float newTheta = vPolar.theta + theta;
            if (newTheta > Mathf.PI)
            {
                newTheta = Mathf.PI - (newTheta - Mathf.PI);
                phi += Mathf.PI; // Flip into the second semiplane
            }

            float newPhi = vPolar.phi + phi;
            if (newPhi > Mathf.PI * 2)
                newPhi -= Mathf.PI * 2;

            return new Polar3D(vPolar.radius, newTheta, newPhi).ToVec3();
        }

        private void BuildCylinder(ref Vector3[] vertices, ref Vector3[] normals, ref int[] triangles, ref int j,
            ref int k, int cylLastVertIndex, Vec3 vector, Basis3D basis)
        {
            const int baseVertCount = 12;
            const float r = cylRadius;
            float h = cylHeight = vector.Length() - coneHeight;

            // Base center
            vertices[0] = RecalcInBasis(Vector3.zero, basis);
            normals[0] = RecalcInBasis(Vector3.down, basis);

            // Cap center
            vertices[cylLastVertIndex] = RecalcInBasis(Vector3.up * h, basis);
            normals[cylLastVertIndex] = RecalcInBasis(Vector3.up, basis);

            float angleStep = 2 * Mathf.PI / baseVertCount;
            for (int i = 0; i < baseVertCount; i++, j += 8, k += 12) // Calculates 8 vertices and 4 triangles (4 * 3)
            {
                float angle = angleStep * i;
                Vector3 curBaseVertex = new Vector3(Mathf.Cos(angle) * r, 0, Mathf.Sin(angle) * r);
                Vector3 nextBaseVertex =
                    new Vector3(Mathf.Cos(angle + angleStep) * r, 0, Mathf.Sin(angle + angleStep) * r);

                // Base triangle
                vertices[j] = RecalcInBasis(curBaseVertex, basis);
                normals[j] = RecalcInBasis(Vector3.down, basis);

                vertices[j + 1] = RecalcInBasis(nextBaseVertex, basis);
                normals[j + 1] = RecalcInBasis(Vector3.down, basis);

                triangles[k] = 0; // Base center vertex
                triangles[k + 1] = j;
                triangles[k + 2] = j + 1;

                // Edge
                Vector3 currentCapVertex = new Vector3(curBaseVertex.x, h, curBaseVertex.z);
                Vector3 nextCapVertex = new Vector3(nextBaseVertex.x, h, nextBaseVertex.z);
                Vector3 edgeNormal = Vector3.Cross(Vector3.up, -curBaseVertex + nextBaseVertex).normalized;

                vertices[j + 2] = RecalcInBasis(curBaseVertex, basis);
                normals[j + 2] = RecalcInBasis(edgeNormal, basis);

                vertices[j + 3] = RecalcInBasis(currentCapVertex, basis);
                normals[j + 3] = RecalcInBasis(edgeNormal, basis);

                vertices[j + 4] = RecalcInBasis(nextCapVertex, basis);
                normals[j + 4] = RecalcInBasis(edgeNormal, basis);

                vertices[j + 5] = RecalcInBasis(nextBaseVertex, basis);
                normals[j + 5] = RecalcInBasis(edgeNormal, basis);

                // triangle 1
                triangles[k + 3] = j + 2;
                triangles[k + 4] = j + 3;
                triangles[k + 5] = j + 5;

                // triangle 2
                triangles[k + 6] = j + 5;
                triangles[k + 7] = j + 3;
                triangles[k + 8] = j + 4;

                // Cap triangle
                vertices[j + 6] = RecalcInBasis(currentCapVertex, basis);
                normals[j + 6] = RecalcInBasis(Vector3.up, basis);

                vertices[j + 7] = RecalcInBasis(nextCapVertex, basis);
                normals[j + 7] = RecalcInBasis(Vector3.up, basis);

                triangles[k + 9] = cylLastVertIndex; // Cap center vertex
                triangles[k + 10] = j + 7;
                triangles[k + 11] = j + 6;
            }
        }

        private void BuildCone(ref Vector3[] vertices, ref Vector3[] normals, ref int[] triangles, ref int j, ref int k,
            Basis3D basis)
        {
            const int baseVertCount = 12;
            const float r = coneRadius;
            const float h = coneHeight;

            int t = j; // The first vertex index

            // Base center
            vertices[j] = RecalcInBasis(Vector3.up * cylHeight, basis);
            normals[j] = RecalcInBasis(Vector3.down, basis);
            j++;

            float angleStep = 2 * Mathf.PI / baseVertCount;
            for (int i = 0; i < baseVertCount; i++, j += 5, k += 6) // Calculates 8 vertices and 2 triangles (2 * 3)
            {
                float angle = angleStep * i;
                Vector3 curBaseVertex = new Vector3(Mathf.Cos(angle) * r, cylHeight, Mathf.Sin(angle) * r);
                Vector3 nextBaseVertex = new Vector3(Mathf.Cos(angle + angleStep) * r, cylHeight,
                    Mathf.Sin(angle + angleStep) * r);

                // Base triangle
                vertices[j] = RecalcInBasis(curBaseVertex, basis);
                normals[j] = RecalcInBasis(Vector3.down, basis);

                vertices[j + 1] = RecalcInBasis(nextBaseVertex, basis);
                normals[j + 1] = RecalcInBasis(Vector3.down, basis);

                triangles[k] = t; // Base center vertex
                triangles[k + 1] = j;
                triangles[k + 2] = j + 1;

                // Edge
                Vector3 topVertex = new Vector3(0, cylHeight + coneHeight, 0);
                Vector3 edgeNormal =
                    Vector3.Cross(topVertex - curBaseVertex, nextBaseVertex - curBaseVertex).normalized;

                vertices[j + 2] = RecalcInBasis(curBaseVertex, basis);
                normals[j + 2] = RecalcInBasis(edgeNormal, basis);

                vertices[j + 3] = RecalcInBasis(topVertex, basis);
                normals[j + 3] = RecalcInBasis(edgeNormal, basis);

                vertices[j + 4] = RecalcInBasis(nextBaseVertex, basis);
                normals[j + 4] = RecalcInBasis(edgeNormal, basis);

                // triangle
                triangles[k + 3] = j + 2;
                triangles[k + 4] = j + 3;
                triangles[k + 5] = j + 4;
            }
        }

        private Vector3 RecalcInBasis(Vector3 v, Basis3D basis)
        {
            return (v.x * basis.x + v.y * basis.y + v.z * basis.z).ToVector3();
        }

        private Vector3 RecalcInBasis(Vector3 v, Basis2D basis)
        {
            return (v.x * basis.x + v.y * basis.y).ToVector3();
        }
    }
}