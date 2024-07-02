using ImGuiNET;
using UnityEngine;
using MyMath;

namespace Src.Demos._2D
{
    public class DivByScalarDemo : Demo2D, IDemo
    {
        private Mesh vectorAMesh = new();
        private Mesh scaledVectorAMesh = new();

        private Vector2 inputA = Vector2.one;
        private Vector2 scaledInputA;

        private Vec2 vectorA;
        private Vec2 scaledVectorA;
        private float factor = 1.5f;

        public DivByScalarDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec2(inputA.x, inputA.y);
            scaledVectorA = vectorA / factor;
            scaledInputA = new Vector2(scaledVectorA.x, scaledVectorA.y);

            BuildArrowMeshForVector(scaledVectorAMesh, scaledVectorA);
            BuildArrowMeshForVector(vectorAMesh, vectorA);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(scaledVectorAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat2("Vector A", ref inputA);
            ImGui.InputFloat("factor", ref factor);

            ImGui.Separator();
            ImGui.InputFloat2("Vector A / factor", ref scaledInputA);
        }
    }
}