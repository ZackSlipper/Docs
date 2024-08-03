using System;

namespace Docs.Document;

public class Date
{
    public int Year { get; }
	public int Month { get; }
	public int Day { get; }

	public bool FullMonthRange { get; }

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
