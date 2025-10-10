using System;
using System.Collections.Generic;
using Godot;

namespace WarringStates.Map;

public sealed partial class HexagonGrid : Node3D
{
    [Export]
    public int Width { get; set; } = 6;

    [Export]
    public int Height { get; set; } = 6;

    [Export]
    public PackedScene? CellPrefab { get; set; }

    [Export]
    public Material? CellMeterial { get; set; }

    private List<HexagonCell> Cells { get; } = [];

    private HexagonMesh? Mesh { get; set; }

    public override void _Ready()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var z = 0; z < Height; z++)
            {
                CreateCell(x, z);
            }
        }
        Mesh = new HexagonMesh();
        Mesh.Triangulate(Cells);
        AddChild(Mesh);

        // GetViewport().SetDebugDraw(Viewport.DebugDrawEnum.Wireframe);
        Mesh.SetSurfaceOverrideMaterial(0, CellMeterial);

        var shader = new ShaderMaterial() { Shader = new() { Code = ShaderCode } };
        shader.SetShaderParameter("color", Colors.GreenYellow);
        Mesh.SetSurfaceOverrideMaterial(1, shader);
        GD.Print(Mesh.GetSurfaceOverrideMaterialCount());
        // if (Mesh.GetSurfaceOverrideMaterial(index) is not StandardMaterial3D material)
        // {
        //     GD.Print("not standard 3d material");
        //     return;
        // }
        // material.AlbedoColor = Colors.GreenYellow;
        // Mesh.SetSurfaceOverrideMaterial(0, new);
    }

    public override void _Process(double delta) { }

    private void CreateCell(int x, int z)
    {
        var pos = new Vector3
        {
            X = (x + z * 0.5f - z / 2) * HexagonMetrics.InnerRadius * 2f,
            Y = 0f,
            Z = z * HexagonMetrics.OuterRadius * 1.5f,
        };
        if (CellPrefab is null)
            throw new NullReferenceException();
        var cell = CellPrefab.Instantiate<HexagonCell>();
        cell.Position = pos;
        AddChild(cell);
        cell.SetContent($"{x}\n{z}");
        Cells.Add(cell);
    }

    private static string ShaderCode { get; } =
        "shader_type spatial;\nrender_mode unshaded,wireframe,cull_disabled;\n\nuniform vec4 albedo : source_color = vec4(0.0,0.0,0.0,1.0);\nuniform float outline_width : hint_range(0.0, 10.0, 0.1) = 3.0;\n\n\nvoid vertex() {\n\tvec4 clip_position = PROJECTION_MATRIX * (MODELVIEW_MATRIX * vec4(VERTEX, 1.0));\n\tvec3 clip_normal = mat3(PROJECTION_MATRIX) * (mat3(MODELVIEW_MATRIX) * NORMAL);\n\t\n\tvec2 offset = normalize(clip_normal.xy) / VIEWPORT_SIZE * \n\t\tclip_position.w * outline_width * 2.0;\n\t\n\t\n\t\n\tclip_position.xy += offset;\n\t\n\tPOSITION = clip_position;\n\t\n\t\n\t\n}\n\n\nvoid fragment() {\n\tALBEDO = albedo.rgb;\n}\n";
}
