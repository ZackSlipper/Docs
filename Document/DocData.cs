using System.Linq;
using Newtonsoft.Json;

namespace Docs.Document;

public class DocData
{
	[JsonIgnore] public int Series { get; set; }
	[JsonIgnore] public Date Date { get; set; } = new();

	//Seller
	public string SellerName { get; set; }
	public string SellerPersonalNo { get; set; }
	public Address SellerAddress { get; set; } = new();
	public string SellerBankAccount { get; set; }
	public string SellerActivityCertificateNo { get; set; }

	//Buyer
	public string BuyerName { get; set; }
	public Address BuyerAddress { get; set; } = new();
	public string BuyerCompanyCode { get; set; }

	public ServiceType ServiceType { get; set; }
	[JsonIgnore] public Service[] Services { get; set; }
	[JsonIgnore] public int RepeatingServiceCount { get; set; }
	[JsonIgnore] public decimal ServiceTotalPrice { get; private set; }
	[JsonIgnore] public string ServiceTotalPriceInWords { get; private set; }

	public DocData(int series, Date date, string sellerName, string sellerPersonalNo, Address sellerAddress, string sellerBankAccount,
	string sellerActivityCertificateNo, string buyerName, Address buyerAddress, string buyerCompanyCode, Service[] services)
	{
		Series = series;
		Date = date;

		SellerName = sellerName;
		SellerPersonalNo = sellerPersonalNo;
		SellerAddress = sellerAddress;
		SellerBankAccount = sellerBankAccount;
		SellerActivityCertificateNo = sellerActivityCertificateNo;

		BuyerName = buyerName;
		BuyerAddress = buyerAddress;
		BuyerCompanyCode = buyerCompanyCode;

		Services = services;
		ServiceTotalPrice = services.Sum(s => s.Price * (ServiceType == ServiceType.Repeating ? RepeatingServiceCount : 1));
		ServiceTotalPriceInWords = PriceToWords.Convert(ServiceTotalPrice);
	}

	public DocData() { }

	public void RecalculateTotalPrice()
	{
		ServiceTotalPrice = Services?.Sum(s => s.Price * (ServiceType == ServiceType.Repeating ? RepeatingServiceCount : 1)) ?? 0;
		ServiceTotalPriceInWords = PriceToWords.Convert(ServiceTotalPrice);
	}
}
