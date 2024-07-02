using ImGuiNET;
using MyMath;
using UnityEngine;

namespace Src.Demos._3D
{
    public class NegatingDemo : Demo3D, IDemo
    {
        private Vec3 vectorA;
        private Vec3 vectorInvA;

        private Mesh vectorAMesh = new();
        private Mesh vectorInvAMesh = new();

        private Vector3 inputA = new(1, 0.5f, 0);
        private Vector3 inputInvA;

        public NegatingDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec3(inputA.x, inputA.y, inputA.z);
            vectorInvA = -vectorA;
            inputInvA = new Vector3(vectorInvA.x, vectorInvA.y, vectorInvA.z);

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(vectorInvAMesh, vectorInvA);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(vectorInvAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat3("Vector A", ref inputA);

            ImGui.Separator();
            ImGui.InputFloat3("Vector -A", ref inputInvA);
        }
    }
}