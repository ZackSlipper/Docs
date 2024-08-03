using NPOI.XWPF.UserModel;

namespace Docs.Document;

public static class BuilderExtensions
{
	public static void SetText(this XWPFParagraph paragraph, string text, bool isBold = false, int fontSize = 12,
		string fontFamily = "Times New Roman", string lang = "lt-LT")
	{
		XWPFRun run = paragraph.CreateRun();
		run.SetText(text);
		run.FontSize = fontSize;
		run.FontFamily = fontFamily;
		run.IsBold = isBold;
		run.Lang = lang;
	}

	public static XWPFParagraph CreateParagraph(this XWPFDocument document, string text, ParagraphAlignment alignment = ParagraphAlignment.LEFT,
		int spacingAfter = 0, int spacingBefore = 0, bool isBold = false, int fontSize = 12, string fontFamily = "Times New Roman", string lang = "lt-LT")
	{
		XWPFParagraph paragraph = document.CreateParagraph();
		paragraph.Alignment = alignment;
		paragraph.SpacingAfter = spacingAfter;
		paragraph.SpacingBefore = spacingBefore;
		paragraph.SetText(text, isBold, fontSize, fontFamily, lang);

		return paragraph;
	}

	public static void AddSignLine(this XWPFParagraph paragraph)
	{
		XWPFRun run = paragraph.CreateRun();
		for (int i = 0; i < 6; i++)
			run.AddTab();
		run.FontSize = 12;
		run.Underline = UnderlinePatterns.Single;
	}

	public static XWPFParagraph AddParagraph(this XWPFTableCell cell, string text, ParagraphAlignment alignment = ParagraphAlignment.CENTER,
		int spacingAfter = 0, int spacingBefore = 0, bool isBold = false, int fontSize = 12, string fontFamily = "Times New Roman", string lang = "lt-LT")
	{
		XWPFParagraph paragraph;
		if (cell.Paragraphs.Count == 1 && string.IsNullOrWhiteSpace(cell.Paragraphs[0].Text))
			paragraph = cell.Paragraphs[0];
		else
			paragraph = cell.AddParagraph();

		paragraph.Alignment = alignment;
		paragraph.SpacingAfter = spacingAfter;
		paragraph.SpacingBefore = spacingBefore;
		paragraph.SetText(text, isBold, fontSize, fontFamily, lang);

		return paragraph;
	}

	public static string ToPriceText(this decimal price) =>
		price.ToString("0.00").Replace(".", ",");
}
