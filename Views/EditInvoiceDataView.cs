using System;
using Docs.Application;
using Docs.Document;
using Docs.Invoice;
using Godot;

namespace Docs.Views;

public partial class EditInvoiceDataView : View
{
	[Export] private LineEdit shortNameLineEdit;

	[ExportGroup("Start")]
	[Export] private LineEdit startYearLineEdit;
	[Export] private OptionButton startMonthOptionButton;
	[Export] private LineEdit startSeriesLineEdit;

	[ExportGroup("Seller")]
	[Export] private LineEdit sellerNameLineEdit;
	[Export] private LineEdit sellerPersonalNoLineEdit;
	[Export] private LineEdit sellerAddressCityLineEdit;
	[Export] private LineEdit sellerAddressPostalCodeLineEdit;
	[Export] private LineEdit sellerAddressStreetLineEdit;
	[Export] private LineEdit sellerAddressBuildingLineEdit;
	[Export] private LineEdit sellerBankAccNoLineEdit;
	[Export] private LineEdit sellerActivityCertificateNoLineEdit;

	[ExportGroup("Buyer")]
	[Export] private LineEdit buyerNameLineEdit;
	[Export] private LineEdit buyerAddressCityLineEdit;
	[Export] private LineEdit buyerAddressPostalCodeLineEdit;
	[Export] private LineEdit buyerAddressStreetLineEdit;
	[Export] private LineEdit buyerAddressBuildingLineEdit;
	[Export] private LineEdit buyerCompanyCodeLineEdit;

	[ExportGroup("Services")]
	[Export] private OptionButton serviceTypeOptionButton;
	[Export] private Button servicesButton;

	[ExportGroup("Buttons")]
	[Export] private Button backButton;
	[Export] private Button deleteButton;
	[Export] private Button saveButton;

	[ExportGroup("Other")]
	[Export] private ScrollContainer scrollContainer;

	private bool IsNewInvoice { get; set; }
	private InvoiceData Invoice { get; set; }

	public override void _Ready()
	{
		shortNameLineEdit.TextChanged += OnShortNameTextChanged;

		startYearLineEdit.TextChanged += OnStartYearTextChanged;
		startMonthOptionButton.ItemSelected += OnStartMonthItemSelected;
		startSeriesLineEdit.TextChanged += OnStartSeriesTextChanged;

		sellerNameLineEdit.TextChanged += OnSellerNameTextChanged;
		sellerPersonalNoLineEdit.TextChanged += OnSellerPersonalNoTextChanged;
		sellerAddressCityLineEdit.TextChanged += OnSellerAddressCityTextChanged;
		sellerAddressPostalCodeLineEdit.TextChanged += OnSellerAddressPostalCodeTextChanged;
		sellerAddressStreetLineEdit.TextChanged += OnSellerAddressStreetTextChanged;
		sellerAddressBuildingLineEdit.TextChanged += OnSellerAddressBuildingTextChanged;
		sellerBankAccNoLineEdit.TextChanged += OnSellerBankAccNoTextChanged;
		sellerActivityCertificateNoLineEdit.TextChanged += OnSellerActivityCertificateNoTextChanged;

		buyerNameLineEdit.TextChanged += OnBuyerNameTextChanged;
		buyerAddressCityLineEdit.TextChanged += OnBuyerAddressCityTextChanged;
		buyerAddressPostalCodeLineEdit.TextChanged += OnBuyerAddressPostalCodeTextChanged;
		buyerAddressStreetLineEdit.TextChanged += OnBuyerAddressStreetTextChanged;
		buyerAddressBuildingLineEdit.TextChanged += OnBuyerAddressBuildingTextChanged;
		buyerCompanyCodeLineEdit.TextChanged += OnBuyerCompanyCodeTextChanged;

		serviceTypeOptionButton.ItemSelected += OnServiceTypeItemSelected;
		servicesButton.Pressed += OnServicesButtonPressed;

		backButton.Pressed += BackToMainMenu;
		deleteButton.Pressed += OnDeleteButtonPressed;
		saveButton.Pressed += SaveInvoice;
	}

	#region Event Handlers
	private void OnShortNameTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.ShortName = newText;
	}

	private void OnStartYearTextChanged(string newText)
	{
		if (Invoice != null && short.TryParse(newText, out short year))
			Invoice.StartDate.Year = year;
	}

	private void OnStartMonthItemSelected(long i)
	{
		if (Invoice != null)
			Invoice.StartDate.Month = (int)i + 1;
	}

	private void OnStartSeriesTextChanged(string newText)
	{
		if (Invoice != null && int.TryParse(newText, out int series))
			Invoice.StartSeries = series;
	}

	private void OnSellerNameTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.SellerName = newText;
	}

	private void OnSellerPersonalNoTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.SellerPersonalNo = newText;
	}

	private void OnSellerAddressCityTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.SellerAddress.City = newText;
	}

	private void OnSellerAddressPostalCodeTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.SellerAddress.PostalCode = newText;
	}

	private void OnSellerAddressStreetTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.SellerAddress.Street = newText;
	}

	private void OnSellerAddressBuildingTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.SellerAddress.Building = newText;
	}

	private void OnSellerBankAccNoTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.SellerBankAccount = newText;
	}

	private void OnSellerActivityCertificateNoTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.SellerActivityCertificateNo = newText;
	}

	private void OnBuyerNameTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.BuyerName = newText;
	}

	private void OnBuyerAddressCityTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.BuyerAddress.City = newText;
	}

	private void OnBuyerAddressPostalCodeTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.BuyerAddress.PostalCode = newText;
	}

	private void OnBuyerAddressStreetTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.BuyerAddress.Street = newText;
	}

	private void OnBuyerAddressBuildingTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.BuyerAddress.Building = newText;
	}

	private void OnBuyerCompanyCodeTextChanged(string newText)
	{
		if (Invoice != null)
			Invoice.OtherData.BuyerCompanyCode = newText;
	}

	private void OnServiceTypeItemSelected(long index)
	{
		if (Invoice != null)
			Invoice.OtherData.ServiceType = (ServiceType)(int)index;
	}

	private void OnServicesButtonPressed()
	{
		if (Invoice == null)
			return;

		Global.ViewController.ShowView("edit_invoice_services", Invoice);
	}

	private void OnDeleteButtonPressed()
	{
		if (Invoice == null)
			return;

		Global.ViewController.ShowView("confirm", new object[] {
			"Ištrinti Sąskaitą Faktūrą",
			$"Ar tikrai norite ištrinti sąskaitą faktūrą '{Invoice.ShortName}'?",
			"edit_invoice_data", DeleteInvoice });
	}
	#endregion

	public override void ViewEnabled(object data)
	{
		if (data is object[] dataArray && dataArray.Length == 2 &&
			dataArray[0] is InvoiceData invoice && dataArray[1] is bool isNewInvoice)
		{
			IsNewInvoice = isNewInvoice;
			Invoice = invoice;
			SetFieldValuesFromInvoiceData();
		}

		deleteButton.Visible = !IsNewInvoice;
		scrollContainer.ScrollVertical = 0;
	}

	private void SetFieldValuesFromInvoiceData()
	{
		shortNameLineEdit.Text = Invoice.ShortName ?? "";

		startYearLineEdit.Text = Invoice.StartDate.Year.ToString();
		startMonthOptionButton.Selected = Invoice.StartDate.Month - 1;
		startSeriesLineEdit.Text = Invoice.StartSeries.ToString();

		sellerNameLineEdit.Text = Invoice.OtherData.SellerName ?? "";
		sellerPersonalNoLineEdit.Text = Invoice.OtherData.SellerPersonalNo ?? "";
		sellerAddressCityLineEdit.Text = Invoice.OtherData.SellerAddress.City ?? "";
		sellerAddressPostalCodeLineEdit.Text = Invoice.OtherData.SellerAddress.PostalCode ?? "";
		sellerAddressStreetLineEdit.Text = Invoice.OtherData.SellerAddress.Street ?? "";
		sellerAddressBuildingLineEdit.Text = Invoice.OtherData.SellerAddress.Building ?? "";
		sellerBankAccNoLineEdit.Text = Invoice.OtherData.SellerBankAccount ?? "";
		sellerActivityCertificateNoLineEdit.Text = Invoice.OtherData.SellerActivityCertificateNo ?? "";

		buyerNameLineEdit.Text = Invoice.OtherData.BuyerName ?? "";
		buyerAddressCityLineEdit.Text = Invoice.OtherData.BuyerAddress.City ?? "";
		buyerAddressPostalCodeLineEdit.Text = Invoice.OtherData.BuyerAddress.PostalCode ?? "";
		buyerAddressStreetLineEdit.Text = Invoice.OtherData.BuyerAddress.Street ?? "";
		buyerAddressBuildingLineEdit.Text = Invoice.OtherData.BuyerAddress.Building ?? "";
		buyerCompanyCodeLineEdit.Text = Invoice.OtherData.BuyerCompanyCode ?? "";

		serviceTypeOptionButton.Selected = (int)Invoice.OtherData.ServiceType;
	}

	private void Clear()
	{
		Invoice = null;

		shortNameLineEdit.Text = string.Empty;

		startYearLineEdit.Text = DateTime.Now.Year.ToString();
		startMonthOptionButton.Selected = DateTime.Now.Month - 1;
		startSeriesLineEdit.Text = "1";

		sellerNameLineEdit.Text = string.Empty;
		sellerPersonalNoLineEdit.Text = string.Empty;
		sellerAddressCityLineEdit.Text = string.Empty;
		sellerAddressPostalCodeLineEdit.Text = string.Empty;
		sellerAddressStreetLineEdit.Text = string.Empty;
		sellerAddressBuildingLineEdit.Text = string.Empty;
		sellerBankAccNoLineEdit.Text = string.Empty;
		sellerActivityCertificateNoLineEdit.Text = string.Empty;

		buyerNameLineEdit.Text = string.Empty;
		buyerAddressCityLineEdit.Text = string.Empty;
		buyerAddressPostalCodeLineEdit.Text = string.Empty;
		buyerAddressStreetLineEdit.Text = string.Empty;
		buyerAddressBuildingLineEdit.Text = string.Empty;
		buyerCompanyCodeLineEdit.Text = string.Empty;

		serviceTypeOptionButton.Selected = (int)ServiceType.OneTime;
	}

	private bool ValidateInputs()
	{
		if (string.IsNullOrEmpty(shortNameLineEdit.Text) ||
			string.IsNullOrEmpty(startYearLineEdit.Text) ||
			!short.TryParse(startYearLineEdit.Text, out _) ||
			string.IsNullOrEmpty(startSeriesLineEdit.Text) ||
			!int.TryParse(startSeriesLineEdit.Text, out _) ||
			string.IsNullOrEmpty(sellerNameLineEdit.Text) ||
			string.IsNullOrEmpty(sellerPersonalNoLineEdit.Text) ||
			string.IsNullOrEmpty(sellerAddressCityLineEdit.Text) ||
			string.IsNullOrEmpty(sellerAddressStreetLineEdit.Text) ||
			string.IsNullOrEmpty(sellerAddressBuildingLineEdit.Text) ||
			string.IsNullOrEmpty(sellerAddressBuildingLineEdit.Text) ||
			string.IsNullOrEmpty(sellerBankAccNoLineEdit.Text) ||
			string.IsNullOrEmpty(sellerActivityCertificateNoLineEdit.Text) ||
			string.IsNullOrEmpty(buyerNameLineEdit.Text) ||
			string.IsNullOrEmpty(buyerNameLineEdit.Text) ||
			string.IsNullOrEmpty(buyerAddressCityLineEdit.Text) ||
			string.IsNullOrEmpty(buyerAddressStreetLineEdit.Text) ||
			string.IsNullOrEmpty(buyerAddressBuildingLineEdit.Text) ||
			string.IsNullOrEmpty(buyerCompanyCodeLineEdit.Text) ||
			Invoice.Services.Count == 0)
			return false;

		return true;
	}

	private void BackToMainMenu()
	{
		Clear();
		Global.ViewController.ShowView("main_menu");
	}

	private void SaveInvoice()
	{
		if (Invoice == null)
			return;

		if (!ValidateInputs())
		{
			Global.ViewController.ShowView("info", new string[] {
				"Klaidingi duomenys",
				"Užpildykite visus laukus ir įveskite bent vieną paslaugą",
				"edit_invoice_data"
			});
			return;
		}

		if (IsNewInvoice)
			Global.InvoiceController.AddInvoice(Invoice);
		else
			Global.InvoiceController.SaveInvoices();
		Clear();
		Global.ViewController.ShowView("info", new string[] { "Išsaugota", "", "main_menu" });
	}

	private void DeleteInvoice()
	{
		if (Invoice == null)
			return;

		Global.InvoiceController.RemoveInvoice(Invoice);
		Clear();
		Global.ViewController.ShowView("info", new string[] { "Ištrinta", "", "main_menu" });
	}
}
