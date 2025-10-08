using System;
using Godot;

namespace HexaMap;

public partial class HexaCell : Node3D
{
	public void SetContent(string content)
	{
		var label = GetNode<Label3D>("Content");
		label.Text = content;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready() { }

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }
}
