using System;
using Docs.Application;
using Docs.Document;
using Docs.Invoice;
using Godot;

namespace Docs.Views;

public partial class GenerateInvoiceView : View
{
	[Export] private Label shortNameLabel;
	[Export] private LineEdit dateYearLineEdit;
	[Export] private OptionButton dateMonthOptionButton;
	[Export] private Label seriesLabel;
	[Export] private Button servicesButton;
	[Export] private LineEdit repeatingServiceCountLineEdit;
	[Export] private Label serviceCountLabel;
	[Export] private Label totalPriceLabel;
	[Export] private Button saveButton;
	[Export] private Button saveAndPrintButton;
	[Export] private Button backButton;

	private InvoiceData Invoice { get; set; }
	private Date Date => Invoice.OtherData.Date;

	public override void _Ready()
	{
		servicesButton.Pressed += () =>
			Global.ViewController.ShowView("invoice_services", Invoice);
		saveButton.Pressed += OnSaveButtonPressed;
		saveAndPrintButton.Pressed += OnSaveAndPrintButtonPressed;
		backButton.Pressed += () => Global.ViewController.ShowView("main_menu");

		dateYearLineEdit.TextChanged += OnDateYearTextChanged;
		dateMonthOptionButton.ItemSelected += OnDateMonthItemSelected;
		repeatingServiceCountLineEdit.TextChanged += OnRepeatingServiceCountTextChanged;
	}

	private void OnDateYearTextChanged(string newText)
	{
		if (int.TryParse(newText, out int year))
		{
			Date.Year = year;
			Date.SetLastDayOfMonth();
			RecalculateSeries();
		}
	}

	private void OnDateMonthItemSelected(long index)
	{
		Date.Month = (short)(index + 1);
		Date.SetLastDayOfMonth();
		RecalculateSeries();
	}

	private void OnRepeatingServiceCountTextChanged(string newText)
	{
		if (int.TryParse(newText, out int count))
		{
			Invoice.OtherData.RepeatingServiceCount = count;
			SetServiceValues();
		}
	}

	private bool ValidateServices()
	{
		if (Invoice.OtherData.ServiceType == ServiceType.Selectable && Invoice.SelectedServices.Count == 0)
		{
			Global.ViewController.ShowView("info",
				new string[] { "Klaida", "Pasirinkite bent vieną paslaugą.", "generate_invoice" });
			return false;
		}
		else if (Invoice.OtherData.ServiceType == ServiceType.Repeating && Invoice.OtherData.RepeatingServiceCount is <= 0 or > 500)
		{
			Global.ViewController.ShowView("info",
				new string[] { "Klaida", "Paslaugos kartai negali būti mažesni už 1 ir didesni už 500", "generate_invoice" });
			return false;
		}
		return true;
	}

	private void OnSaveButtonPressed()
	{
		if (!ValidateServices())
			return;

		Global.ViewController.ShowView("save_file", new object[]
		{
			SaveInvoiceAction, Invoice.ShortName
		});
	}

	private void OnSaveAndPrintButtonPressed()
	{
		if (!ValidateServices())
			return;

		Global.ViewController.ShowView("save_file", new object[]
		{
			SaveAndPrintInvoiceAction, Invoice.ShortName
		});
	}

	public override void ViewEnabled(object data)
	{
		if (data is InvoiceData invoiceData)
		{
			Invoice = invoiceData;
			Invoice.SelectedServices.Clear();
			Date.Year = DateTime.Now.Year;
			Date.Month = DateTime.Now.Month;
			Date.SetLastDayOfMonth();
			SetFieldValues();
			RecalculateSeries();
			OnRepeatingServiceCountTextChanged(repeatingServiceCountLineEdit.Text);

			servicesButton.Visible = Invoice.OtherData.ServiceType == ServiceType.Selectable;
			repeatingServiceCountLineEdit.GetParent<Control>().Visible = Invoice.OtherData.ServiceType == ServiceType.Repeating;
		}

		SetServiceValues();
	}

	private void SetFieldValues()
	{
		shortNameLabel.Text = Invoice.ShortName;
		dateYearLineEdit.Text = Date.Year.ToString();
		dateMonthOptionButton.Select(Date.Month - 1);
	}

	private void SetServiceValues()
	{
		SetupDocumentServices();
		Invoice.OtherData.RecalculateTotalPrice();
		serviceCountLabel.Text = Invoice.OtherData.Services.Length.ToString();
		totalPriceLabel.Text = $"{Invoice.OtherData.ServiceTotalPrice} Eur";
	}

	private void SetupDocumentServices()
	{
		if (Invoice.OtherData.ServiceType == ServiceType.Selectable)
		{
			foreach (Service service in Invoice.SelectedServices)
				service.Date = new(Date.Year, Date.Month, service.Date.Day);
			Invoice.OtherData.Services = Invoice.SelectedServices.ToArray();
		}
		else
		{
			foreach (Service service in Invoice.Services)
				service.Date = new(Date.Year, Date.Month, true);
			Invoice.OtherData.Services = Invoice.Services.ToArray();
		}
	}

	private void RecalculateSeries()
	{
		Invoice.OtherData.Series = Invoice.StartSeries +
			(Date.Year - Invoice.StartDate.Year) * 12 +
			Date.Month - Invoice.StartDate.Month;
		seriesLabel.Text = Invoice.OtherData.Series.ToString();
	}

	private bool SaveInvoice(string path)
	{
		try
		{
			DocBuilder builder = new();
			builder.Build(Invoice.OtherData);
			builder.Save(path);
		}
		catch (Exception ex)
		{
			Global.ViewController.ShowView("error", ex);
			return false;
		}
		return true;
	}

	private void SaveInvoiceAction(string path)
	{
		if (!SaveInvoice(path))
			return;

		Global.ViewController.ShowView("info",
			new string[] { "Sąskaita Faktūra Išsaugota", "", "main_menu" });
	}

	private void SaveAndPrintInvoiceAction(string path)
	{
		if (!SaveInvoice(path))
			return;

		//Print
		Global.ViewController.ShowView("print", new object[] { path, "generate_invoice" });
	}
}
