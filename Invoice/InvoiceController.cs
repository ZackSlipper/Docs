using System;
using System.Collections.Generic;
using Docs.Application;
using Godot;

namespace Docs.Invoice;

public partial class InvoiceController : Node
{
	public List<InvoiceData> Invoices { get; private set; } = new();

	public override void _Ready() => LoadInvoices();

	public void AddInvoice(InvoiceData invoice)
	{
		Invoices.Add(invoice);
		SaveInvoices();
	}

	private void LoadInvoices()
	{
		try
		{
			Invoices = FileManager.LoadInvoiceData();
		}
		catch (Exception ex)
		{
			Global.ViewController.ShowView("critical_error", ex);
		}
	}

	public void SaveInvoices()
	{
		try
		{
			FileManager.SaveInvoiceData(Invoices);
		}
		catch (Exception ex)
		{
			Global.ViewController.ShowView("critical_error", ex);
		}
	}
}
