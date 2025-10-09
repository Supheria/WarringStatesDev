using Godot;

namespace WarringStates.Map;

public partial class HexagonCell : Node3D
{
	private Label3D? Content { get; set; }

	public override void _Ready()
	{
		Content = GetNode<Label3D>(nameof(Content));
	}

	public override void _Process(double delta) { }

	public void SetContent(string content)
	{
		if (Content is not null)
			Content.Text = content;
	}
}
