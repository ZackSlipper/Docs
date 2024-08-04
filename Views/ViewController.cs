using System;
using System.Collections.Generic;
using Godot;

namespace Docs.Views;

public partial class ViewController : Node
{
    [Export] private Godot.Collections.Array<View> viewList;

	private Dictionary<string, View> Views { get; } = new();

	public string CurrentView { get; private set; }
	public string PreviousView { get; private set; }

	public event Action<string> ViewChanged;

	public override void _Ready()
	{
		foreach (var view in viewList)
			Views.Add(view.Name, view);
	}

	public void ShowView(string viewName, object data = null)
	{
		if (!Views.ContainsKey(viewName))
		{
			GD.PrintErr($"View {viewName} not found.");
			return;
		}
		else if (CurrentView == viewName)
			return;

		HideAllViews();
		Views[viewName].Visible = true;
		PreviousView = CurrentView;
		CurrentView = viewName;

		Views[viewName].ViewEnabled(data);
		ViewChanged?.Invoke(viewName);
	}

	private void HideAllViews()
	{
		foreach (var view in Views.Values)
		{
			if (view.Visible)
			{
				view.Visible = false;
				view.ViewDisabled();
			}
		}
	}
}
