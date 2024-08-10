using System.Collections.Generic;
using Docs.Application;
using Docs.Document;
using Docs.Invoice;
using Godot;

namespace Docs.Views;

public partial class InvoiceServicesView : View
{
	[Export] private OptionButton invoiceOptions;
	[Export] private Button addSelectedServiceButton;
	[Export] private Button backButton;
	[Export] private ScrollContainer scrollContainer;

	[ExportGroup("Service Item")]
	[Export] private PackedScene selectableServiceItemPrefab;
	[Export] private Control selectableServiceItemParent;

	private InvoiceData Invoice { get; set; }
	private List<SelectableServiceItem> ServiceItems { get; } = new();

	public override void _Ready()
	{
		addSelectedServiceButton.Pressed += AddNewServiceItem;
		backButton.Pressed += () =>
			Global.ViewController.ShowView("generate_invoice");
		scrollContainer.GetVScrollBar().Changed += ScrollToBottom;
	}

	public override void ViewEnabled(object data)
	{
		if (data is InvoiceData invoiceData)
		{
			Invoice = invoiceData;
			AddServiceOptions();
		}
		else
			throw new System.ArgumentException(
				"Data must be of type InvoiceData", nameof(data));

		UpdateServiceItems();
	}

	public override void ViewDisabled()
	{
		ClearServiceItems();
		Invoice = null;
	}

	private void ClearServiceItems()
	{
		foreach (var serviceItem in ServiceItems)
		{
			serviceItem.Hide();
			serviceItem.QueueFree();
		}

		ServiceItems.Clear();
	}

	private void ScrollToBottom() =>
		scrollContainer.ScrollVertical = (int)scrollContainer.GetVScrollBar().MaxValue;

	private void AddNewServiceItem()
	{
		Service selectedServiceOption = Invoice.Services[invoiceOptions.Selected];
		Service service = new(selectedServiceOption.Name,
			new(Invoice.OtherData.Date.Year, Invoice.OtherData.Date.Month, 1),
			selectedServiceOption.Price);
		Invoice.SelectedServices.Add(service);
		AddServiceItem(service);
	}

	private void AddServiceItem(Service service)
	{
		SelectableServiceItem serviceItem =
			selectableServiceItemPrefab.Instantiate<SelectableServiceItem>();
		selectableServiceItemParent.AddChild(serviceItem);
		ServiceItems.Add(serviceItem);
		serviceItem.Init(ServiceItems.Count, service, this);
	}

	public void RemoveServiceItem(SelectableServiceItem serviceItem)
	{
		Invoice.SelectedServices.Remove(serviceItem.Service);
		UpdateServiceItems();
	}

	public void MoveServiceItem(SelectableServiceItem serviceItem, bool up)
	{
		Invoice.SelectedServices.Remove(serviceItem.Service);

		int newIndex = up ? serviceItem.Index - 2 : serviceItem.Index;
		if (newIndex < 0)
			newIndex = Invoice.SelectedServices.Count;
		else if (newIndex > Invoice.SelectedServices.Count)
			newIndex = 0;

		Invoice.SelectedServices.Insert(newIndex, serviceItem.Service);

		UpdateServiceItems();
	}

	public void UpdateServiceItems()
	{
		ClearServiceItems();

		foreach (var service in Invoice.SelectedServices)
			AddServiceItem(service);
	}

	private void AddServiceOptions()
	{
		invoiceOptions.Clear();
		foreach (var service in Invoice.Services)
			invoiceOptions.AddItem(service.Name);
	}
}
