using UnityEngine;
using MyMath;
using ImGuiNET;

namespace Src.Demos._3D
{
    public class DotProductDemo : Demo3D, IDemo
    {
        private bool showAProjB;
        private bool showBProjA;

        private float angleInDeg;

        private Vec3 vectorA;
        private Vec3 vectorB;
        private Vec3 vectorC;

        private Mesh vectorAMesh = new();
        private Mesh vectorBMesh = new();

        private Mesh vectorAProjBMesh = new();
        private Mesh vectorBProjAMesh = new();

        private Mesh angleMesh = new();

        private Vector3 inputA = new(0.3f, 0.7f, 0);
        private Vector3 inputB = Vector3.right;

        public DotProductDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec3(inputA.x, inputA.y, inputA.z);
            vectorB = new Vec3(inputB.x, inputB.y, inputB.z);

            float dotProd = Vec3.Dot(vectorA, vectorB);
            float vectorALen = vectorA.Length();
            float vectorBLen = vectorB.Length();
            float angleInRad = Mathf.Acos(dotProd / (vectorALen * vectorBLen));
            angleInDeg = angleInRad * Mathf.Rad2Deg;

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(vectorBMesh, vectorB);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
            Graphics.DrawMesh(vectorBMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);

            if (showAProjB)
            {
                float projLen = dotProd / vectorBLen;
                Vec3 vectorAProjB = projLen * vectorB.Normalize();
                BuildArrowMeshForVector(vectorAProjBMesh, vectorAProjB);
                Graphics.DrawMesh(vectorAProjBMesh, Vector3.zero, Quaternion.identity, magentaMaterial, 0);
            }

            if (showBProjA)
            {
                float projLen = dotProd / vectorALen;
                Vec3 vectorBProjA = projLen * vectorA.Normalize();
                BuildArrowMeshForVector(vectorBProjAMesh, vectorBProjA);
                Graphics.DrawMesh(vectorBProjAMesh, Vector3.zero, Quaternion.identity, magentaMaterial, 0);
            }

            Basis2D basis = CreateBasisFromVectors(vectorA, vectorB);
            Vector3 basisNormal = Vector3.Cross(basis.x.ToVector3(), basis.y.ToVector3());

            // The basis is positive if a vector product looks away from the camera
            float dotCamAndNormal = Vector3.Dot(basisNormal, Camera.main.transform.position);
            if (dotCamAndNormal > 0)
                basis.y = -basis.y;

            float angleOffset = 0;
            float dotBPerpA = Vec3.Dot(vectorB, basis.y);
            if (dotBPerpA < 0)
                angleOffset = 2 * Mathf.PI - angleInRad;

            BuildAngleMesh(angleInRad, basis, ref angleMesh, angleOffset);
            Graphics.DrawMesh(angleMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat3("Vector A", ref inputA);
            ImGui.InputFloat3("Vector B", ref inputB);

            ImGui.Separator();
            ImGui.InputFloat("Angle (deg)", ref angleInDeg);

            ImGui.Separator();
            ImGui.Checkbox("A proj B", ref showAProjB);
            ImGui.Checkbox("B proj A", ref showBProjA);
        }
    }
}