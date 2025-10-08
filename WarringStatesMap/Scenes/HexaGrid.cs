using System;
using Godot;

namespace HexaMap;

public partial class HexaGrid : Node3D
{
	[Export]
	public int Width { get; set; } = 6;

	[Export]
	public int Height { get; set; } = 6;

	[Export]
	public PackedScene CellPrefab { get; set; }

	// private HexaCell[] Cells { get; set; }

	public override void _Ready()
	{
		// Cells = new HexaCell[Width * Height];
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
		var edgeLen = 2f; // TODO: use a member variant of the cell
		var pos = new Vector3
		{
			X = x * edgeLen,
			Y = 0f,
			Z = z * edgeLen,
		};
		var cell = CellPrefab.Instantiate<HexaCell>();
		cell.Position = pos;
		cell.SetContent($"{x}\n{z}");
		AddChild(cell);

		// var label = new Label3D();
		// label.Shaded = false;
		// label.Text = $"{x}\n{z}";
		// label.Position = pos;
		// label.RotateX(float.DegreesToRadians(-90));
		// AddChild(label);
	}
}
