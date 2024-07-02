using UnityEngine;
using MyMath;
using ImGuiNET;

namespace Src.Demos._3D
{
    public class UnitVectorDemo : Demo3D, IDemo
    {
        private Vec3 vectorA;
        private Vec3 vectorAUnit;

        private Mesh vectorAMesh = new();
        private Mesh vectorAUnitMesh = new();

        private Vector3 inputA = new(1, 0.5f, 1);
        private Vector3 inputAUnit;

        public UnitVectorDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec3(inputA.x, inputA.y, inputA.z);
            vectorAUnit = vectorA.Normalize();
            inputAUnit = new Vector3(vectorAUnit.x, vectorAUnit.y, vectorAUnit.z);

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(vectorAUnitMesh, vectorAUnit);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(vectorAUnitMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat3("Vector A", ref inputA);
            ImGui.Separator();
            ImGui.InputFloat3("Vector A unit", ref inputAUnit);
        }
    }
}