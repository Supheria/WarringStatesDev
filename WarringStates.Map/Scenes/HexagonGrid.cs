using System;
using Godot;

namespace WarringStates.Map;

public partial class HexagonGrid : Node3D
{
    [Export]
    public int Width { get; set; } = 6;

    [Export]
    public int Height { get; set; } = 6;

    [Export]
    public PackedScene? CellPrefab { get; set; }

    public override void _Ready()
    {
        for (var x = 0; x < Width; x++)
        {
            for (var z = 0; z < Height; z++)
            {
                // TODO: Cells is useless here
                CreateCell(x, z);
            }
        }
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
    }
}
