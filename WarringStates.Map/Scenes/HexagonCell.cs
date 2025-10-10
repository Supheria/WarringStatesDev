using Godot;

namespace WarringStates.Map;

public sealed partial class HexagonCell : Node3D
{
	public string Content
	{
		get => _content;
		set
		{
			_content = value;
			if (ContentLabel is not null)
				ContentLabel.Text = _content;
		}
	}
	private string _content = "Empty";
	private Label3D? ContentLabel { get; set; }
	public HexagonCoordinate Coordinate { get; set; }

	public override void _Ready()
	{
		ContentLabel = GetNode<Label3D>(nameof(Content));
		ContentLabel.Text = Content;
	}

	public override void _Process(double delta) { }
}
