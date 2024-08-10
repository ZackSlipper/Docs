using Godot;

namespace Docs.Application;

public partial class Main : Node
{
	private static Main Instance { get; set; }

	public override void _Ready() => Instance = this;

	private void QuitInternal() => GetTree().Quit();
	public static void Quit() => Instance.QuitInternal();
}
