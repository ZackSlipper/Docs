using System;

namespace Docs.Document;

public class Date
{
    public int Year { get; set; }
	public int Month { get; set; }
	public int Day { get; set; }

	public bool FullMonthRange { get; set; }

	public Date(int year, int month, bool fullMonthRange = false)
	{
		Year = year;
		Month = month;
		Day = DateTime.DaysInMonth(year, month);
		FullMonthRange = fullMonthRange;
	}

	public override string ToString() =>
		FullMonthRange ? $"{Year} {Month:D2} 01 - {Day:D2}" : $"{Year}-{Month:D2}-{Day:D2}";
}
