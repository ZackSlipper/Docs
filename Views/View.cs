using Godot;

namespace Docs.Views;

public partial class View : Control
{
    [Export] private string name;
	public new string Name => name;

	public virtual void ViewEnabled(object data) { }
	public virtual void ViewDisabled() { }
}
