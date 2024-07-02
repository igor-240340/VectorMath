using ImGuiNET;
using UnityEngine;
using MyMath;

namespace Src.Demos._2D
{
    public class SubtractionDemo : Demo2D, IDemo
    {
        private Mesh vectorAMesh = new();
        private Mesh vectorBMesh = new();
        private Mesh vectorCMesh = new();

        private Vector2 inputA = Vector2.one;
        private Vector2 inputB = new(2, -1);
        private Vector2 inputC;

        private Vec2 vectorA;
        private Vec2 vectorB;
        private Vec2 vectorC;

        public SubtractionDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec2(inputA.x, inputA.y);
            vectorB = new Vec2(inputB.x, inputB.y);

            vectorC = vectorA - vectorB;
            inputC = new Vector2(vectorC.x, vectorC.y);

            BuildArrowMeshForVector(vectorCMesh, vectorC);
            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(vectorBMesh, vectorB);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(vectorBMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(vectorCMesh, new Vector3(vectorB.x, vectorB.y), Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat2("Vector A", ref inputA);
            ImGui.InputFloat2("Vector B", ref inputB);

            ImGui.Separator();
            ImGui.InputFloat2("Vector A - B", ref inputC);
        }
    }
}