using ImGuiNET;
using UnityEngine;
using MyMath;

namespace Src.Demos._2D
{
    public class LengthDemo : Demo2D, IDemo
    {
        private Mesh vectorAMesh = new();

        private Vector2 inputA = new(1, 1);

        private Vec2 vectorA;

        public LengthDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec2(inputA.x, inputA.y);

            BuildArrowMeshForVector(vectorAMesh, vectorA);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat2("Vector A", ref inputA);

            ImGui.Separator();
            ImGui.Text($"Length: {vectorA.Length()}");
        }
    }
}