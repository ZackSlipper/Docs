using Docs.Invoice;
using Docs.Views;
using Godot;

namespace Docs.Application;

public partial class Global : Node
{
    private static Global Instance { get; set; }

	[Export] private ViewController	viewController;
	public static ViewController ViewController => Instance.viewController;

	[Export] private InvoiceController invoiceController;
	public static InvoiceController InvoiceController => Instance.invoiceController;

	public override void _Ready() =>
		Instance = this;
}
