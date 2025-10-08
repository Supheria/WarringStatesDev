using System;
using Godot;

namespace HexaMap;

public partial class HexaCell : Node3D
{
	private Label3D? Content { get; set; }

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Content = GetNode<Label3D>("Content");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta) { }

	public void SetContent(string content)
	{
		if (Content is not null)
			Content.Text = content;
	}
}
