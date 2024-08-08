using Docs.Application;
using Godot;

namespace Docs.Views;

public partial class InfoView : View
{
	[Export] private Label titleLabel;
	[Export] private Label contentLabel;
	[Export] private Button okButton;

	private string okView;

	public override void _Ready() =>
		okButton.Pressed += () => Global.ViewController.ShowView(okView);

	public override void ViewEnabled(object data)
	{
		if (data is string[] info && info.Length == 3)
		{
			titleLabel.Text = info[0];
			contentLabel.Text = info[1];
			okView = info[2];
		}
		else
		{
			titleLabel.Text = "Info";
			contentLabel.Text = "Invalid info data provided.";
			okView = "main_menu";
		}
	}
}
