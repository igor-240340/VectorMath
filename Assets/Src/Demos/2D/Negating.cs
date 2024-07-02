using ImGuiNET;
using UnityEngine;
using MyMath;

namespace Src.Demos._2D
{
    public class NegatingDemo : Demo2D, IDemo
    {
        private Mesh vectorAMesh = new();
        private Mesh vectorInvAMesh = new();

        private Vector2 inputA = Vector2.one;
        private Vector2 inputInvA;

        private Vec2 vectorA;
        private Vec2 vectorInvA;

        public NegatingDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec2(inputA.x, inputA.y);
            vectorInvA = -vectorA;
            inputInvA = new Vector2(vectorInvA.x, vectorInvA.y);

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(vectorInvAMesh, vectorInvA);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(vectorInvAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat2("Vector A", ref inputA);

            ImGui.Separator();
            ImGui.InputFloat2("Vector -A", ref inputInvA);
        }
    }
}