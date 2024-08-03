using System.Linq;

namespace Docs.Document;

public class DocData
{
	public int Series { get; }
	public Date Date { get; }

	//Seller
	public string SellerName { get; }
	public string SellerPersonalNo { get; }
	public Address SellerAddress { get; }
	public string SellerBankAccount { get; }
	public string SellerActivityCertificateNo { get; }

	//Buyer
	public string BuyerName { get; }
	public Address BuyerAddress { get; }
	public string BuyerCompanyCode { get; }


	public Service[] Services { get; }
	public decimal ServiceTotalPrice { get; }
	public string ServiceTotalPriceInWords { get; }

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
}
