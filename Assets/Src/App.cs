using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ImGuiNET;

public class App : MonoBehaviour
{
    [SerializeField] private Material blueMaterial;
    [SerializeField] private Material greyMaterial;
    [SerializeField] private Material magentaMaterial;

    private List<IDemo> demos2d = new();
    private List<IDemo> demos3d = new();

    private IDemo activeDemo;

    private void OnEnable()
    {
        ImGuiUn.Layout += OnLayout;
    }

    private void OnDisable()
    {
        ImGuiUn.Layout -= OnLayout;
    }

    private void Start()
    {
        CreateDemos();
    }

    private void CreateDemos()
    {
        demos2d.Add(new Src.Demos._2D.AdditionDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos2d.Add(new Src.Demos._2D.NegatingDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos2d.Add(new Src.Demos._2D.MulByScalarDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos2d.Add(new Src.Demos._2D.SubtractionDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos2d.Add(new Src.Demos._2D.LengthDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos2d.Add(new Src.Demos._2D.DivByScalarDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos2d.Add(new Src.Demos._2D.UnitVectorDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos2d.Add(new Src.Demos._2D.DotProductDemo(blueMaterial, greyMaterial, magentaMaterial));

        demos3d.Add(new Src.Demos._3D.AdditionDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos3d.Add(new Src.Demos._3D.NegatingDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos3d.Add(new Src.Demos._3D.MulByScalarDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos3d.Add(new Src.Demos._3D.SubtractionDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos3d.Add(new Src.Demos._3D.LengthDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos3d.Add(new Src.Demos._3D.DivByScalarDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos3d.Add(new Src.Demos._3D.UnitVectorDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos3d.Add(new Src.Demos._3D.DotProductDemo(blueMaterial, greyMaterial, magentaMaterial));
        demos3d.Add(new Src.Demos._3D.CrossProductDemo(blueMaterial, greyMaterial, magentaMaterial));
    }

    private void Update()
    {
        activeDemo?.OnUpdate();
    }

    // ImGui
    private void OnLayout()
    {
        ShowMainMenuBar();

        if (activeDemo != null)
        {
            ImGui.Begin(activeDemo.ToString().Split('.').Reverse().ToArray()[0]);
            activeDemo.OnImgui();
            ImGui.End();
        }
    }

    private void ShowMainMenuBar()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("2D demos"))
            {
                foreach (var demo in demos2d)
                {
                    if (ImGui.MenuItem(demo.ToString().Split('.').Reverse().ToArray()[0]))
                    {
                        Camera.main.orthographic = true;
                        Camera.main.transform.position = new Vector3(0, 0, -10);

                        activeDemo = demo;
                    }
                }

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("3D demos"))
            {
                foreach (var demo in demos3d)
                {
                    if (ImGui.MenuItem(demo.ToString().Split('.').Reverse().ToArray()[0]))
                    {
                        Camera.main.orthographic = false;
                        Camera.main.transform.position = new Vector3(0, 0, -6);

                        activeDemo = demo;
                    }
                }

                ImGui.EndMenu();
            }

            ImGui.EndMainMenuBar();
        }
    }
}