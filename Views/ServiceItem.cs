using Docs.Document;
using Godot;

namespace Docs.Views;

public partial class ServiceItem : Control
{
	[Export] private Label indexLabel;
	[Export] private TextEdit nameTextEdit;
	[Export] private LineEdit priceLineEdit;
	[Export] private Button removeButton;

	public Service Service { get; private set; }

	private int index;
	public int Index
	{
		get => index;
		set
		{
			index = value;
			indexLabel.Text = $"{value}.";
		}
	}

	public void Init(int index, Service service, EditInvoiceServicesView view)
	{
		Index = index;
		Service = service;

		nameTextEdit.Text = service.Name;
		priceLineEdit.Text = service.Price.ToString();

		removeButton.Pressed += () => view.RemoveServiceItem(this);
		nameTextEdit.TextChanged += OnNameTextChanged;
		priceLineEdit.TextChanged += OnPriceTextChanged;
	}

	private void OnNameTextChanged() =>
		Service.Name = nameTextEdit.Text;

	private void OnPriceTextChanged(string newText)
	{
		if (decimal.TryParse(newText, out decimal price))
			Service.Price = price;
	}
}
