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
        Mesh.SetSurfaceOverrideMaterial(
            0,
            new StandardMaterial3D { AlbedoColor = Colors.GreenYellow }
        );
        
        
        // Mesh.CreateTrimeshCollision();
        var body = new StaticBody3D();
        // var id = body.CreateShapeOwner(new GodotObject());
        var shape = Mesh.Mesh.CreateTrimeshShape();
        // shape.BackfaceCollision = false;
        var collision = new CollisionShape3D();
        collision.Shape = shape;
        body.AddChild(collision);
        // body.ShapeOwnerAddShape(id, shape);
        Mesh.AddChild(body);
        
        
        AddChild(Mesh);
    }

    public override void _Process(double delta) { }

    private void CreateCell(int x, int z)
    {
        var pos = new Vector3
        {
            X = (x + z * 0.5f) * HexagonMetrics.InnerRadius * 2f,
            Y = 0f,
            Z = z * HexagonMetrics.OuterRadius * 1.5f,
        };
        if (CellPrefab is null)
            throw new NullReferenceException();
        var cell = CellPrefab.Instantiate<HexagonCell>();
        cell.Position = pos;
        cell.Coordinate = HexagonCoordinate.FromOffsetCoordinate(x, z);
        cell.Content = cell.Coordinate.ToString();
        AddChild(cell);
        Cells.Add(cell);
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        // if (@event is InputEventMouseButton mouse)
        // {
        //     var camera = GetViewport().GetCamera3D();
        //     var inputRay = 
        // }
        
    }
}
