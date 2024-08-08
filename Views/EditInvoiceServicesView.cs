using System.Collections.Generic;
using Docs.Application;
using Docs.Document;
using Docs.Invoice;
using Godot;

namespace Docs.Views;

public partial class EditInvoiceServicesView : View
{
	[Export] private Button addServiceButton;
	[Export] private Button backButton;
	[Export] private ScrollContainer scrollContainer;

	[ExportGroup("Service Item")]
	[Export] private PackedScene serviceItemPrefab;
	[Export] private Control serviceItemParent;

	private bool IsNewInvoice { get; set; }
	private InvoiceData Invoice { get; set; }
	private List<ServiceItem> ServiceItems { get; } = new();
	private bool AddedNewService { get; set; }

	public override void _Ready()
	{
		addServiceButton.Pressed += AddNewServiceItem;
		backButton.Pressed += () =>
			Global.ViewController.ShowView("edit_invoice_data");
		scrollContainer.GetVScrollBar().Changed += ScrollToBottomIfNewService;
	}

	public override void ViewEnabled(object data)
	{
		if (data is InvoiceData invoiceData)
			Invoice = invoiceData;
		else
			throw new System.ArgumentException(
				"Data must be of type object[] and contain InvoiceData and bool.");

		AddInvoiceServices();
	}

	public override void ViewDisabled()
	{
		foreach (var serviceItem in ServiceItems)
		{
			serviceItem.Hide();
			serviceItem.QueueFree();
		}

		ServiceItems.Clear();
		Invoice = null;
		AddedNewService = false;
	}

	private void ScrollToBottomIfNewService()
	{
		if (!AddedNewService)
			return;

		scrollContainer.ScrollVertical = (int)scrollContainer.GetVScrollBar().MaxValue;
		AddedNewService = false;
	}

	private void AddNewServiceItem()
	{
		Service service = new();
		Invoice.Services.Add(service);
		AddServiceItem(service);
	}

	private void AddServiceItem(Service service)
	{
		ServiceItem serviceItem = serviceItemPrefab.Instantiate<ServiceItem>();
		serviceItemParent.AddChild(serviceItem);
		ServiceItems.Add(serviceItem);
		serviceItem.Init(ServiceItems.Count, service, this);
		AddedNewService = true;
	}

	public void RemoveServiceItem(ServiceItem serviceItem)
	{
		ServiceItems.Remove(serviceItem);
		Invoice.Services.Remove(serviceItem.Service);

		serviceItem.Hide();
		serviceItem.QueueFree();
		UpdateServiceIndexes();
	}

	public void UpdateServiceIndexes()
	{
		for (int i = 0; i < ServiceItems.Count; i++)
			ServiceItems[i].Index = i + 1;
	}

	private void AddInvoiceServices()
	{
		foreach (var service in Invoice.Services)
			AddServiceItem(service);
	}
}
