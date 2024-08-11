using Docs.Application;
using Godot;
using System;

namespace Docs.Views;

public partial class ConfirmationView : View
{
	[Export] private Label titleLabel;
	[Export] private Label contentLabel;
	[Export] private Button okButton;
	[Export] private Button cancelButton;

	private Action OkAction { get; set; }
	private string CancelView { get; set; }

	public override void _Ready()
	{
		okButton.Pressed += () => OkAction?.Invoke();
		cancelButton.Pressed += () => Global.ViewController.ShowView(CancelView);
	}

	public override void ViewEnabled(object data)
	{
		if (data is object[] dataArray && dataArray.Length == 4 && dataArray[0] is string title &&
			dataArray[1] is string content && dataArray[2] is string cancelView &&
			dataArray[3] is Action okAction)
		{
			titleLabel.Text = title;
			contentLabel.Text = content;
			CancelView = cancelView;
			OkAction = okAction;
		}
		else
			throw new ArgumentException("Invalid info data provided.");
	}
}
