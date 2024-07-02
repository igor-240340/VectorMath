using ImGuiNET;
using MyMath;
using UnityEngine;

namespace Src.Demos._3D
{
    public class SubtractionDemo : Demo3D, IDemo
    {
        private Vec3 vectorA;
        private Vec3 vectorB;
        private Vec3 vectorC;

        private Mesh vectorAMesh = new();
        private Mesh vectorBMesh = new();
        private Mesh vectorCMesh = new();

        private Vector3 inputA = Vector3.up / 2;
        private Vector3 inputB = Vector3.right;
        private Vector3 inputC = Vector3.zero;

        public SubtractionDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec3(inputA.x, inputA.y, inputA.z);
            vectorB = new Vec3(inputB.x, inputB.y, inputB.z);

            vectorC = vectorA - vectorB;
            inputC = new Vector3(vectorC.x, vectorC.y, vectorC.z);

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(vectorBMesh, vectorB);
            BuildArrowMeshForVector(vectorCMesh, vectorC);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(vectorBMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(vectorCMesh, new Vector3(vectorB.x, vectorB.y, vectorB.z), Quaternion.identity,
                blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat3("Vector A", ref inputA);
            ImGui.InputFloat3("Vector B", ref inputB);

            ImGui.Separator();
            ImGui.InputFloat3("Vector A - B", ref inputC);
        }
    }
}