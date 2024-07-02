using ImGuiNET;
using UnityEngine;
using MyMath;

namespace Src.Demos._2D
{
    public class UnitVectorDemo : Demo2D, IDemo
    {
        private Mesh vectorAMesh = new();
        private Mesh unitVectorAMesh = new();

        private Vector2 inputA = new(3, 2);
        private Vector2 unitInputA;

        private Vec2 vectorA;
        private Vec2 unitVectorA;

        public UnitVectorDemo(Material blue, Material grey, Material magenta) : base(blue, grey, magenta)
        {
        }

        public void OnUpdate()
        {
            vectorA = new Vec2(inputA.x, inputA.y);
            unitVectorA = vectorA.Normalize();
            unitInputA = new Vector2(unitVectorA.x, unitVectorA.y);

            BuildArrowMeshForVector(vectorAMesh, vectorA);
            BuildArrowMeshForVector(unitVectorAMesh, unitVectorA);

            Graphics.DrawMesh(vectorAMesh, Vector3.zero, Quaternion.identity, greyMaterial, 0);
            Graphics.DrawMesh(unitVectorAMesh, Vector3.zero, Quaternion.identity, blueMaterial, 0);
        }

        public void OnImgui()
        {
            ImGui.InputFloat2("Vector A", ref inputA);

            ImGui.Separator();
            ImGui.InputFloat2("unit Vector A", ref unitInputA);
        }
    }
}