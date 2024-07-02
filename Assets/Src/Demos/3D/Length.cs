using ImGuiNET;
using MyMath;
using UnityEngine;

namespace Src.Demos._3D
{
    public class LengthDemo : Demo3D, IDemo
    {
        private Vec3 vectorA;
        private Vec3 vectorInvA;

        private Mesh vectorAMesh = new();

        private Vector3 inputA = new(1, 0.5f, 1);

        public LengthDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec3(inputA.x, inputA.y, inputA.z);

            BuildArrowMeshForVector(vectorAMesh, vectorA);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat3("Vector A", ref inputA);

            ImGui.Separator();
            ImGui.Text($"Length: {vectorA.Length()}");
        }
    }
}