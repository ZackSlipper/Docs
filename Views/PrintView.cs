using System;
using Docs.Application;
using Godot;

namespace Docs.Views;

public partial class PrintView : View
{
	[Export] private OptionButton printers;
	[Export] private Button printButton;
	[Export] private Button cancelButton;

	private string DocumentPath { get; set; }
	private string ReturnView { get; set; }


	public override void _Ready()
	{
		printers.Clear();
		foreach (string printer in PrinterHelper.ListPrinters())
			printers.AddItem(printer);

		cancelButton.Pressed += () => Global.ViewController.ShowView(ReturnView);
		printButton.Pressed += OnPrintButtonPressed;
	}

	private void OnPrintButtonPressed()
	{
		if (DocumentPath == "")
		{
			Global.ViewController.ShowView(ReturnView);
			return;
		}

		try
		{
			PrinterHelper.PrintDocument(DocumentPath, printers.Text);
		}
		catch (Exception ex)
		{
			Global.ViewController.ShowView(ReturnView);
			Global.ViewController.ShowView("error", ex);
		}
		Global.ViewController.ShowView(ReturnView);
	}

	public override void ViewEnabled(object data)
	{
		if (data is object[] dataArray && dataArray.Length == 2 &&
			dataArray[0] is string documentPath && dataArray[1] is string returnView)
		{
			DocumentPath = documentPath;
			ReturnView = returnView;
		}
		else
			throw new("Invalid data types. Expected string, string.");
	}
}
