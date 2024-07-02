using ImGuiNET;
using UnityEngine;
using MyMath;

namespace Src.Demos._2D
{
    public class MulByScalarDemo : Demo2D, IDemo
    {
        private Mesh vectorAMesh = new();
        private Mesh scaledVectorAMesh = new();

        private Vector2 inputA = Vector2.one;
        private Vector2 scaledInputA = Vector2.one;

        private Vec2 vectorA;
        private float factor = 2;
        private Vec2 scaledVectorA;

        public MulByScalarDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec2(inputA.x, inputA.y);
            scaledVectorA = vectorA * factor;
            scaledInputA = new Vector2(scaledVectorA.x, scaledVectorA.y);

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(scaledVectorAMesh, scaledVectorA);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(scaledVectorAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat2("Vector A", ref inputA);
            ImGui.InputFloat("factor", ref factor);

            ImGui.Separator();
            ImGui.InputFloat2("Vector A * factor", ref scaledInputA);
        }
    }
}