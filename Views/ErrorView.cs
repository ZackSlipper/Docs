using System;
using Docs.Application;
using Godot;
using Environment = System.Environment;

namespace Docs.Views;

public partial class ErrorView : View
{
	[Export] private Label contentLabel;
	[Export] private Button backButton;
	[Export] private bool criticalError;

	public override void _Ready() =>
		backButton.Pressed += OnBackButtonPressed;

	public void OnBackButtonPressed()
	{
		if (criticalError)
			Main.Quit();
		else
			Global.ViewController.ShowPreviousView();
	}

	public override void ViewEnabled(object data) =>
		contentLabel.Text = data is Exception ex ?
			$"{ex.Message}{Environment.NewLine}{ex.StackTrace}" :
			(data?.ToString() ?? "No error message provided.");
}