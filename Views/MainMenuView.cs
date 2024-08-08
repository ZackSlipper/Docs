using Docs.Application;
using Docs.Invoice;
using Godot;

namespace Docs.Views;

public partial class MainMenuView : View
{
	[Export] private OptionButton invoiceOptions;
	[Export] private Button generateButton;
	[Export] private Button editButton;
	[Export] private Button newButton;
	[Export] private Button exitButton;

	public override void _Ready()
	{
		UpdateInvoiceOptions();

		newButton.Pressed += () =>
			Global.ViewController.ShowView("edit_invoice_data",
				new object[] { new InvoiceData(), true });
		editButton.Pressed += () =>
			Global.ViewController.ShowView("edit_invoice_data",
				new object[] { Global.InvoiceController.Invoices[invoiceOptions.Selected], false });
		exitButton.Pressed += Main.Quit;
	}

	public override void ViewEnabled(object data) => UpdateInvoiceOptions();

	public void UpdateInvoiceOptions()
	{
		invoiceOptions.Clear();

		if (Global.InvoiceController.Invoices.Count == 0)
		{
			invoiceOptions.GetParent<Control>().Visible = false;
			generateButton.GetParent<Control>().Visible = false;
			editButton.GetParent<Control>().Visible = false;
			return;
		}

		invoiceOptions.GetParent<Control>().Visible = true;
		generateButton.GetParent<Control>().Visible = true;
		editButton.GetParent<Control>().Visible = true;
		foreach (var invoice in Global.InvoiceController.Invoices)
			invoiceOptions.AddItem(invoice.ShortName);
	}
}
