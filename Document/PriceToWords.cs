namespace Docs.Document;

public static class PriceToWords
{
	public static string Convert(decimal price)
	{
		int euros = (int)price;
		int cents = (int)((price - euros) * 100);

		string eurosInWords = NumberToWords.Convert(euros);

		//Capitalize the first letter
		eurosInWords = char.ToUpper(eurosInWords[0]) + eurosInWords.Substring(1);

		string euroPrefix = euros % 10 == 0 || euros % 100 > 10 && euros % 100 < 20 ? "eurų" : (euros % 10 == 1 ? "euras" : "eurai");
		string centsPrefix = cents % 10 == 0 || cents % 100 > 10 && cents % 100 < 20 ? "centų" : (cents % 10 == 1 ? "centas" : "centai");

		return $"{eurosInWords} {euroPrefix}, {cents:00} {centsPrefix}.";
	}
}
