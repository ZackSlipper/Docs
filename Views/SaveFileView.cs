using System;
using Docs.Application;
using Godot;

namespace Docs.Views;

public partial class SaveFileView : View
{
	private FileDialog FileDialog { get; set; }
	private Action<string> SaveAction { get; set; }


	public override void _Ready()
	{
		FileDialog = GetChild<FileDialog>(0);
		FileDialog.FileSelected += OnFileSelected;
		FileDialog.Canceled += () => Global.ViewController.ShowPreviousView();
		FileDialog.Filters = new[] { "*.docx;Document File" };
	}

	private void OnFileSelected(string path)
	{
		if (path == "")
		{
			Global.ViewController.ShowPreviousView();
			return;
		}

		SaveAction?.Invoke(path);
	}

	public override void ViewEnabled(object data)
	{
		if (data is object[] dataArray && dataArray.Length == 2 &&
			dataArray[0] is Action<string> saveAction && dataArray[1] is string fileName)
		{
			SaveAction = saveAction;
			FileDialog.CurrentDir = FileManager.DocumentsPath;
			FileDialog.CurrentFile = fileName;
			FileDialog.Popup();
		}
		else
			throw new("Invalid data type. Expected Action<string>.");
	}
}
