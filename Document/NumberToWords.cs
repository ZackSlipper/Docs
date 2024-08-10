using System.Text;

namespace Docs.Document;

public static class NumberToWords
{
	public static string Convert(int number)
	{
		if (number == 0)
			return "nulis";

		int thousands = number / 1000;
		int millions = number / 1000000;

		string fullHundreds = FullHundreds(number % 1000);
		string fullThousands = FullHundreds(thousands % 1000, "tūkstantis", "tūkstančiai", "tūkstančių");
		string fullMillions = FullHundreds(millions % 1000, "milijonas", "milijonai", "milijonų");

		StringBuilder sb = new();
		if (millions > 0)
			sb.Append($"{fullMillions}");
		if (thousands > 0)
			sb.Append($"{fullThousands} ");
		sb.Append(fullHundreds);

		return sb.ToString().Trim();
	}

	private static string UpTo20(int number) => number switch
	{
		1 => "vienas",
		2 => "du",
		3 => "trys",
		4 => "keturi",
		5 => "penki",
		6 => "šeši",
		7 => "septyni",
		8 => "aštuoni",
		9 => "devyni",
		10 => "dešimt",
		11 => "vienuolika",
		12 => "dvylika",
		13 => "trylika",
		14 => "keturiolika",
		15 => "penkiolika",
		16 => "šešiolika",
		17 => "septyniolika",
		18 => "aštuoniolika",
		19 => "devyniolika",
		_ => "",
	};

	private static string Tens(int number) => number switch
	{
		2 => "dvidešimt",
		3 => "trisdešimt",
		4 => "keturiasdešimt",
		5 => "penkiasdešimt",
		6 => "šešiasdešimt",
		7 => "septyniasdešimt",
		8 => "aštuoniasdešimt",
		9 => "devyniasdešimt",
		_ => "",
	};

	private static string FullHundreds(int number, string onePrefix = "", string tenPrefix = "", string finalPrefix = "")
	{
		int hundreds = number / 100 % 10;
		int tens = number / 10 % 10;
		int ones = number % 10;

		string tensStr = tens < 2 ? UpTo20(tens * 10 + ones) : $"{Tens(tens)}{(ones == 0 ? "" : $" {UpTo20(ones)}")}";
		string hundredsStr = hundreds > 0 ?
								(hundreds == 1 ? "šimtas" : $"{UpTo20(hundreds)} šimtai") :
								"";


		string prefix = "";
		if (number > 0)
		{
			if (number % 10 == 0 || number % 100 > 10 && number % 100 < 20)
				prefix = finalPrefix;
			else if (number % 10 == 1)
				prefix = onePrefix;
			else
				prefix = tenPrefix;
		}

		bool hasPrefixAndIsOne = !string.IsNullOrWhiteSpace(prefix) && number % 10 == 1;
		return $"{hundredsStr}{(tens + ones == 0 || hasPrefixAndIsOne ? "" : $" {tensStr}")}{(string.IsNullOrWhiteSpace(prefix) ? "" : $" {prefix}")}";
	}
}
