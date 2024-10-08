using Docs.Application;
using Docs.Invoice;
using Godot;

namespace Docs.Views;

public partial class MainMenuView : View
{
	[Export] private OptionButton invoiceOptions;
	[Export] private Button generateButton;
	[Export] private Button editButton;
	[Export] private ColorRect editSeparator;
	[Export] private Button newButton;
	[Export] private Button exitButton;

	private InvoiceData SelectedInvoice { get; set; }

	public override void _Ready()
	{
		UpdateInvoiceOptions();

		generateButton.Pressed += () =>
			Global.ViewController.ShowView("generate_invoice",
				Global.InvoiceController.Invoices[invoiceOptions.Selected]);
		editButton.Pressed += () =>
			Global.ViewController.ShowView("edit_invoice_data",
				new object[] { Global.InvoiceController.Invoices[invoiceOptions.Selected], false });
		newButton.Pressed += () =>
			Global.ViewController.ShowView("edit_invoice_data",
				new object[] { new InvoiceData(), true });
		exitButton.Pressed += Main.Quit;

		invoiceOptions.ItemSelected += index =>
			SelectedInvoice = Global.InvoiceController.Invoices[(int)index];
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
			editSeparator.Visible = false;
			return;
		}

		invoiceOptions.GetParent<Control>().Visible = true;
		generateButton.GetParent<Control>().Visible = true;
		editButton.GetParent<Control>().Visible = true;
		editSeparator.Visible = true;

		foreach (var invoice in Global.InvoiceController.Invoices)
			invoiceOptions.AddItem(invoice.ShortName);

		int index = Global.InvoiceController.Invoices.IndexOf(SelectedInvoice);
		invoiceOptions.Selected = index >= 0 ? index : 0;
	}
}
