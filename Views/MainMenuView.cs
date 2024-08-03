using Application;
using Godot;

namespace Docs.Views;

public partial class MainMenuView : Node
{
	[Export] private OptionButton invoiceOptions;
	[Export] private Button generateButton;
	[Export] private Button editButton;
	[Export] private Button newButton;
	[Export] private Button exitButton;

	public override void _Ready()
	{
		exitButton.Pressed += Main.Quit;
	}
}
