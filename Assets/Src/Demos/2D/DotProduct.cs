using ImGuiNET;
using UnityEngine;
using MyMath;

namespace Src.Demos._2D
{
    public class DotProductDemo : Demo2D, IDemo
    {
        private bool showAProjB;
        private bool showBProjA;

        private float angleInDeg;

        private Mesh vectorAMesh = new();
        private Mesh vectorBMesh = new();
        private Mesh vectorAProjBMesh = new();
        private Mesh vectorBProjAMesh = new();

        private Vector2 inputA = Vector2.one;
        private Vector2 inputB = new(2, -1);

        private Vec2 vectorA;
        private Vec2 vectorB;

        public DotProductDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec2(inputA.x, inputA.y);
            vectorB = new Vec2(inputB.x, inputB.y);

            float dotProd = Vec2.Dot(vectorA, vectorB);
            float vectorALen = vectorA.Length();
            float vectorBLen = vectorB.Length();
            angleInDeg = Mathf.Acos(Mathf.Clamp(dotProd / (vectorALen * vectorBLen), -1, 1)) * Mathf.Rad2Deg;

            if (showAProjB)
            {
                float projLen = dotProd / vectorBLen;
                Vec2 vectorAProjB = projLen * vectorB.Normalize();
                BuildArrowMeshForVector(vectorAProjBMesh, vectorAProjB);
                Graphics.DrawMesh(vectorAProjBMesh, Vector3.zero, Quaternion.identity, magentaMaterial, 0);
            }

            if (showBProjA)
            {
                float projLen = dotProd / vectorALen;
                Vec2 vectorBProjA = projLen * vectorA.Normalize();
                BuildArrowMeshForVector(vectorBProjAMesh, vectorBProjA);
                Graphics.DrawMesh(vectorBProjAMesh, Vector3.zero, Quaternion.identity, magentaMaterial, 0);
            }

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(vectorBMesh, vectorB);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
            Graphics.DrawMesh(vectorBMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat2("Vector A", ref inputA);
            ImGui.InputFloat2("Vector B", ref inputB);

            ImGui.Separator();
            ImGui.InputFloat("Angle (deg)", ref angleInDeg);

            ImGui.Separator();
            ImGui.Checkbox("A proj B", ref showAProjB);
            ImGui.Checkbox("B proj A", ref showBProjA);
        }
    }
}