using System;

namespace Docs.Document;

public class Date
{
	public int Year { get; set; }
	public int Month { get; set; }
	public int Day { get; set; }

	public bool FullMonthRange { get; set; }

	public Date()
	{
		Year = DateTime.Now.Year;
		Month = DateTime.Now.Month;
	}

	public Date(int year, int month, bool fullMonthRange = false)
	{
		Year = year;
		Month = month;
		Day = DateTime.DaysInMonth(year, month);
		FullMonthRange = fullMonthRange;
	}

	public Date(int year, int month, int day)
	{
		Year = year;
		Month = month;
		Day = day;
		FullMonthRange = false;
	}

	public void SetLastDayOfMonth() => Day = DateTime.DaysInMonth(Year, Month);

	public override string ToString() =>
		FullMonthRange ? $"{Year} {Month:D2} 01 - {Day:D2}" : $"{Year}-{Month:D2}-{Day:D2}";
}
