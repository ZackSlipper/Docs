using System;
using System.IO;
using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;

namespace Docs.Document;

public class DocBuilder
{
	public const int Cm = 567;

	private XWPFDocument Document { get; }

	public DocBuilder()
	{
		Document = new();
	}

	public void Build(DocData data)
	{
		//Page setup
		CT_SectPr section = new();
		Document.Document.body.sectPr = section;

		//Orientation
		section.pgSz.orient = ST_PageOrientation.portrait;

		//Size
		section.pgSz.code = "A4";

		//Margins - Narrow
		section.pgMar = new()
		{
			top = Cm * 2,
			right = Cm * 3,
			bottom = Cm * 2,
			left = Cm * 3,
		};

		Document.CreateParagraph("Sąskaita faktūra", ParagraphAlignment.CENTER);
		Document.CreateParagraph($"Serija RG Nr.{data.Series}", ParagraphAlignment.CENTER, Cm / 2);

		Document.CreateParagraph(data.Date.ToString(), ParagraphAlignment.CENTER, (int)(Cm * 1.5f));

		Document.CreateParagraph("Pardavėjas:", isBold: true);
		Document.CreateParagraph(data.SellerName.Trim());
		Document.CreateParagraph($"Asmens kodas {data.SellerPersonalNo.Trim()}");
		Document.CreateParagraph($"Adresas {data.SellerAddress}");
		Document.CreateParagraph($"A/s {data.SellerBankAccount.Trim()}");
		Document.CreateParagraph(
			$"Individualios veiklos pažymėjimas Nr.{data.SellerActivityCertificateNo.Trim()}",
			spacingAfter: Cm / 2);

		Document.CreateParagraph("Pirkėjas:", isBold: true);
		Document.CreateParagraph(data.BuyerName.Trim());
		Document.CreateParagraph(data.BuyerAddress.ToString());
		Document.CreateParagraph($"Įmonės kodas {data.BuyerCompanyCode.Trim()}", spacingAfter: (int)(Cm * 1.5f));


		//Table
		XWPFTable table = Document.CreateTable(2 + data.Services.Length, 4);
		table.Width = (int)(Cm * 8.57);

		table.GetRow(0).GetCell(0).GetCTTc().AddNewTcPr().tcW = new CT_TblWidth() { w = "2.24cm", type = ST_TblWidth.dxa };
		table.GetRow(0).GetCell(1).GetCTTc().AddNewTcPr().tcW = new CT_TblWidth() { w = "6.84cm", type = ST_TblWidth.dxa };
		table.GetRow(0).GetCell(2).GetCTTc().AddNewTcPr().tcW = new CT_TblWidth() { w = "3.93cm", type = ST_TblWidth.dxa };
		table.GetRow(0).GetCell(3).GetCTTc().AddNewTcPr().tcW = new CT_TblWidth() { w = "2.96cm", type = ST_TblWidth.dxa };

		table.GetRow(0).GetCell(0).AddParagraph("Eil. Nr.");
		table.GetRow(0).GetCell(1).AddParagraph("Pavadinimas");
		table.GetRow(0).GetCell(2).AddParagraph("Laikotarpis");
		table.GetRow(0).GetCell(3).AddParagraph("Suma, Eur");

		for (int i = 0; i < data.Services.Length; i++)
		{
			Service service = data.Services[i];

			table.GetRow(i + 1).GetCell(0).AddParagraph($"{i + 1}.");

			string[] nameLines = service.Name.Split(Environment.NewLine);
			foreach (string line in nameLines)
				table.GetRow(i + 1).GetCell(1).AddParagraph(line.Trim(), ParagraphAlignment.LEFT);

			table.GetRow(i + 1).GetCell(2).AddParagraph(service.Date.ToString());
			if (data.ServiceType == ServiceType.Repeating)
				table.GetRow(i + 1).GetCell(2).AddParagraph($"(Kartai {data.RepeatingServiceCount})");

			table.GetRow(i + 1).GetCell(3).AddParagraph((service.Price * (data.ServiceType == ServiceType.Repeating ? data.RepeatingServiceCount : 1)).ToPriceText());
		}

		int lastRow = data.Services.Length + 1;
		table.GetRow(lastRow).GetCell(2).AddParagraph("Iš viso:", isBold: true);
		table.GetRow(lastRow).GetCell(3).AddParagraph(data.ServiceTotalPrice.ToPriceText());


		Document.CreateParagraph($"Suma žodžiais: {data.ServiceTotalPriceInWords}", spacingBefore: Cm / 2, spacingAfter: (int)(Cm * 1.5f));

		Document.CreateParagraph($"Sąskaitą išrašė: {data.SellerName.Trim()}").AddSignLine();
	}

	public void Save(string path) =>
		Document.Write(new FileStream(path, FileMode.Create));
}
