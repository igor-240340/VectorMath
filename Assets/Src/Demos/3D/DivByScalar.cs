using UnityEngine;
using MyMath;
using ImGuiNET;

namespace Src.Demos._3D
{
    public class DivByScalarDemo : Demo3D, IDemo
    {
        private float factor = 2;

        private Vec3 vectorA;
        private Vec3 vectorScaledA;

        private Mesh vectorAMesh = new();
        private Mesh vectorScaledAMesh = new();

        private Vector3 inputA = new(1, 0.5f, 1);
        private Vector3 inputScaledA;

        public DivByScalarDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec3(inputA.x, inputA.y, inputA.z);
            vectorScaledA = vectorA / factor;
            inputScaledA = new Vector3(vectorScaledA.x, vectorScaledA.y, vectorScaledA.z);

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(vectorScaledAMesh, vectorScaledA);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(vectorScaledAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat3("Vector A", ref inputA);
            ImGui.InputFloat("factor", ref factor);

            ImGui.Separator();
            ImGui.InputFloat3("Vector A / factor", ref inputScaledA);
        }
    }
}