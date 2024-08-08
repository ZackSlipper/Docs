using System;
using System.Collections.Generic;
using Godot;

namespace Docs.Views;

public partial class ViewController : Node
{
	private Dictionary<string, View> Views { get; } = new();

	public string CurrentView { get; private set; }
	public string PreviousView { get; private set; }

	public event Action<string> ViewChanged;

	public override void _Ready()
	{
		foreach (Node viewNode in GetChildren())
		{
			View view = viewNode as View;

			Views.Add(view.Name, view);
			if (view.Visible)
				CurrentView = view.Name;
		}
	}

	public void ShowPreviousView()
	{
		if (string.IsNullOrEmpty(PreviousView))
			return;

		ShowView(PreviousView);
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
