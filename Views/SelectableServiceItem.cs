using System;
using Docs.Document;
using Godot;

namespace Docs.Views;
public partial class SelectableServiceItem : Control
{
	[Export] private Label indexLabel;
	[Export] private Label nameLabel;
	[Export] private Label priceLabel;
	[Export] private LineEdit dayLineEdit;
	[Export] private Button moveUpButton;
	[Export] private Button moveDownButton;
	[Export] private Button removeButton;

	public Service Service { get; private set; }

	public int Index { get; private set; }

	public void Init(int index, Service service, InvoiceServicesView view)
	{
		Index = index;
		Service = service;

		indexLabel.Text = $"{index}.";
		nameLabel.Text = service.Name;
		priceLabel.Text = service.Price.ToString();
		dayLineEdit.Text = service.Date.Day.ToString();

		moveUpButton.Pressed += () => view.MoveServiceItem(this, true);
		moveDownButton.Pressed += () => view.MoveServiceItem(this, false);
		removeButton.Pressed += () => view.RemoveServiceItem(this);

		dayLineEdit.TextChanged += OnDayTextChanged;
	}

	private void OnDayTextChanged(string newText)
	{
		if (!int.TryParse(newText, out int day) || day < 1)
		{
			dayLineEdit.Modulate = new Color(1, 0, 0);
			Service.Date.Day = 1;
			return;
		}
		else if (day > DateTime.DaysInMonth(Service.Date.Year, Service.Date.Month))
		{
			day = DateTime.DaysInMonth(Service.Date.Year, Service.Date.Month);
			dayLineEdit.Text = day.ToString();
		}
		dayLineEdit.Modulate = new Color(1, 1, 1);
		Service.Date.Day = day;
	}
}
