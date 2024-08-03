using System.Linq;

namespace Docs.Document;

public class DocData
{
	public int Series { get; set; }
	public Date Date { get; set; }

	//Seller
	public string SellerName { get; set; }
	public string SellerPersonalNo { get; set; }
	public Address SellerAddress { get; set; }
	public string SellerBankAccount { get; set; }
	public string SellerActivityCertificateNo { get; set; }

	//Buyer
	public string BuyerName { get; set; }
	public Address BuyerAddress { get; set; }
	public string BuyerCompanyCode { get; set; }


	public Service[] Services { get; set; }
	public decimal ServiceTotalPrice { get; private set; }
	public string ServiceTotalPriceInWords { get; private set; }

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
		ServiceTotalPrice = services.Sum(s => s.Price);
		ServiceTotalPriceInWords = PriceToWords.Convert(ServiceTotalPrice);
	}

	public DocData() { }

	public void RecalculateTotalPrice()
	{
		ServiceTotalPrice = Services.Sum(s => s.Price);
		ServiceTotalPriceInWords = PriceToWords.Convert(ServiceTotalPrice);
	}
}
